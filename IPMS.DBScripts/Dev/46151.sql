IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_FinalTaskExecution]') AND type in (N'P'))
DROP PROCEDURE [dbo].[usp_FinalTaskExecution]
GO
CREATE PROCEDURE [dbo].[usp_FinalTaskExecution]
   @ResourseAloID    INT,
   @Valpkid          INT,
   @OperationType    NVARCHAR (4),
   @Usrid            INT
   WITH
   EXECUTE AS CALLER
AS
   BEGIN
      DECLARE @DynamicQuery AS NVARCHAR (MAX)
      DECLARE @MovementType   NVARCHAR (10)


      UPDATE dbo.ResourceAllocation
         SET TaskStatus = 'COMP'
       WHERE ResourceAllocationID = @ResourseAloID



      SET @MovementType =
             (SELECT CASE ServiceReferenceType
                        WHEN 'VTSR'
                        THEN
                           (SELECT MovementType
                              FROM dbo.ServiceRequest
                             WHERE ServiceRequestID = a.ServiceReferenceID)
                     END
                        MovementType
                FROM ResourceAllocation a
               WHERE ResourceAllocationID = @ResourseAloID);

      IF (@MovementType = 'ARMV' AND @OperationType = 'BRTH')
         BEGIN
            UPDATE VesselCall
               SET -- ATA  =  t.LastLineIn,  As per the Discussion(RanaDheer) ATA is Not Related to Task WExecution
                   ATB = t.LastLineIn
              FROM (SELECT LastLineIn
                      FROM ShiftingBerthingTaskExecution a
                     WHERE a.BerthingTaskExecutionID = @Valpkid) t
             WHERE VCN =
                      (SELECT q.vcn
                         FROM ResourceAllocation P
                              JOIN dbo.ServiceRequest q
                                 ON q.ServiceRequestID = p.ServiceReferenceID
                        WHERE p.ResourceAllocationID = @ResourseAloID
                       GROUP BY q.vcn)
         END

      IF (@OperationType = 'BRTH' AND @MovementType != 'SGMV')
         BEGIN
            UPDATE VesselCall
               SET ATB = t.LastLineIn,
                   FromPositionPortCode = t.FromBollardPortCode,
                   FromPositionQuayCode = t.FromBollardQuayCode,
                   FromPositionBerthCode = t.FromBollardBerthCode,
                   FromPositionBollardCode = t.FromBollardCode,
                   ToPositionPortCode = t.ToBollardPortCode,
                   ToPositionQuayCode = t.ToBollardQuayCode,
                   ToPositionBerthCode = t.ToBollardBerthCode,
                   ToPositionBollardCode = t.ToBollardCode
              FROM (SELECT a.LastLineIn,
                           a.FromBollardPortCode,
                           a.FromBollardQuayCode,
                           a.FromBollardBerthCode,
                           a.FromBollardCode,
                           a.ToBollardPortCode,
                           a.ToBollardQuayCode,
                           a.ToBollardBerthCode,
                           a.ToBollardCode
                      FROM ShiftingBerthingTaskExecution a
                           INNER JOIN ResourceAllocation b
                              ON b.ResourceAllocationID =
                                    a.ResourceAllocationID
                     WHERE     a.BerthingTaskExecutionID = @Valpkid
                           AND b.ResourceAllocationID = @ResourseAloID) t
             WHERE VesselCall.VCN =
                      (SELECT q.vcn
                         FROM ResourceAllocation P
                              JOIN dbo.ServiceRequest q
                                 ON q.ServiceRequestID = p.ServiceReferenceID
                        WHERE p.ResourceAllocationID = @ResourseAloID
                       GROUP BY q.vcn)

            UPDATE VesselCallMovement
               SET ATB = t.LastLineIn,
                   FromPositionPortCode = t.FromBollardPortCode,
                   FromPositionQuayCode = t.FromBollardQuayCode,
                   FromPositionBerthCode = t.FromBollardBerthCode,
                   FromPositionBollardCode = t.FromBollardCode,
                   ToPositionPortCode = t.ToBollardPortCode,
                   ToPositionQuayCode = t.ToBollardQuayCode,
                   ToPositionBerthCode = t.ToBollardBerthCode,
                   ToPositionBollardCode = t.ToBollardCode,
                   MooringBollardBowPortCode = t.MooringBollardBowPortCode,
                   MooringBollardBowQuayCode = t.MooringBollardBowQuayCode,
                   MooringBollardBowBerthCode = t.MooringBollardBowBerthCode,
                   MooringBollardBowBollardCode =
                      t.MooringBollardBowBollardCode,
                   MooringBollardStemPortCode = t.MooringBollardStemPortCode,
                   MooringBollardStemQuayCode = t.MooringBollardStemQuayCode,
                   MooringBollardStemBerthCode = t.MooringBollardStemBerthCode,
                   MooringBollardStemBollardCode =
                      t.MooringBollardStemBollardCode,
                   MovementStatus = 'BERT'
              FROM (SELECT a.LastLineIn,
                           a.FromBollardPortCode,
                           a.FromBollardQuayCode,
                           a.FromBollardBerthCode,
                           a.FromBollardCode,
                           a.ToBollardPortCode,
                           a.ToBollardQuayCode,
                           a.ToBollardBerthCode,
                           a.ToBollardCode,
                           a.MooringBollardBowPortcode,
                           a.MooringBollardBowQuayCode,
                           a.MooringBollardBowBerthCode,
                           a.MooringBollardBowBollardCode,
                           a.MooringBollardStemPortcode,
                           a.MooringBollardStemQuayCode,
                           a.MooringBollardStemBerthCode,
                           a.MooringBollardStemBollardCode,
                           b.ServiceReferenceID
                      FROM ShiftingBerthingTaskExecution a
                           INNER JOIN ResourceAllocation b
                              ON b.ResourceAllocationID =
                                    a.ResourceAllocationID
                     WHERE     a.BerthingTaskExecutionID = @Valpkid
                           AND b.ResourceAllocationID = @ResourseAloID) t
             WHERE     VesselCallMovement.ServiceRequestID =
                          T.ServiceReferenceID
                   AND VesselCallMovement.MovementStatus != 'SALD'
         END


      IF (@OperationType = 'BRTH' AND @MovementType != 'ARMV')
         BEGIN
            UPDATE VesselCallMovement
               SET ATUB = t.FirstLineOut
              FROM (SELECT FirstLineOut
                      FROM ShiftingBerthingTaskExecution a
                     WHERE a.BerthingTaskExecutionID = @Valpkid) t
             WHERE     VCN =
                          (SELECT q.vcn
                             FROM ResourceAllocation P
                                  JOIN dbo.ServiceRequest q
                                     ON q.ServiceRequestID =
                                           p.ServiceReferenceID
                            WHERE p.ResourceAllocationID = @ResourseAloID)
                   AND MovementStatus = 'BERT'
                   AND RecordStatus = 'A'
                   AND ATB IS NOT NULL
                   AND ATUB IS NULL
                   AND ServiceRequestID !=
                          (SELECT p.ServiceReferenceID
                             FROM ResourceAllocation P
                            WHERE p.ResourceAllocationID = @ResourseAloID)
         END

      IF (@MovementType = 'SGMV' AND @OperationType = 'BRTH')
         BEGIN
            UPDATE VesselCallMovement
               SET ATUB = t.FirstLineOut                                   --,
              --MovementStatus = 'BERT'
              FROM (SELECT FirstLineOut
                      FROM ShiftingBerthingTaskExecution a
                     WHERE a.BerthingTaskExecutionID = @Valpkid) t
             WHERE ServiceRequestID =
                      (SELECT ServiceReferenceID
                         FROM ResourceAllocation
                        WHERE ResourceAllocationID = @ResourseAloID)


            UPDATE VesselCall
               SET ATUB = t.FirstLineOut                                   --,
              --   ATD  = t.FirstLineOut   As per the Discussion(RanaDheer) ATA is Not Related to Task WExecution
              FROM (SELECT FirstLineOut
                      FROM ShiftingBerthingTaskExecution a
                     WHERE a.BerthingTaskExecutionID = @Valpkid) t
             WHERE VCN =
                      (SELECT q.vcn
                         FROM ResourceAllocation P
                              JOIN dbo.ServiceRequest q
                                 ON q.ServiceRequestID = p.ServiceReferenceID
                        WHERE p.ResourceAllocationID = @ResourseAloID)

            -- Added by sandeep
            UPDATE VesselCallMovement
               SET MovementStatus = 'SALD'
             WHERE VCN =
                      (SELECT q.vcn
                         FROM ResourceAllocation P
                              JOIN dbo.ServiceRequest q
                                 ON q.ServiceRequestID = p.ServiceReferenceID
                        WHERE p.ResourceAllocationID = @ResourseAloID)
         -- End
         END
   END
GO
