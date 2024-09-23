ALTER PROCEDURE [dbo].[usp_GetScheduledTasksDetails]
   @portcode NVARCHAR (2), @puserid INT
   WITH
   EXECUTE AS CALLER
AS
   BEGIN
      SELECT a.ResourceAllocationID,
       a.ServiceReferenceType,
       c.SubCatName ServiceReferenceTypeName,       
        CASE a.ServiceReferenceType
          WHEN 'SUPP'
          THEN
             (SELECT V.VesselName
                FROM dbo.SuppServiceRequest SR
                inner join ArrivalNotification AN ON AN.VCN = SR.VCN
                inner join Vessel V ON V.VesselID = AN.VesselID
               WHERE SR.SuppServiceRequestID = a.ServiceReferenceID)
          ELSE
             (SELECT V.VesselName
                FROM dbo.ServiceRequest SR 
                inner join ArrivalNotification AN ON AN.VCN = SR.VCN
                inner join Vessel V ON V.VesselID = AN.VesselID
               WHERE SR.ServiceRequestID = a.ServiceReferenceID)
       END VesselName, 
       CASE a.ServiceReferenceType WHEN 'VTSR' THEN
        coalesce((select Berth from
             (SELECT case coalesce(VCM.MovementType,'SGMV') when 'SGMV' then '' else coalesce(TB.BerthName,'') END Berth
                FROM VesselCallMovement VCM
                   LEFT JOIN dbo.Berth FB
                      ON     FB.PortCode = VCM.FromPositionPortCode
                         AND FB.QuayCode = VCM.FromPositionQuayCode
                         AND FB.BerthCode = VCM.FromPositionBerthCode
                   LEFT JOIN dbo.Berth TB
                      ON     TB.PortCode = VCM.ToPositionPortCode
                         AND TB.QuayCode = VCM.ToPositionQuayCode
                         AND TB.BerthCode = VCM.ToPositionBerthCode
                   LEFT JOIN dbo.Bollard FD
                      ON     FD.PortCode = VCM.FromPositionPortCode
                         AND FD.QuayCode = VCM.FromPositionQuayCode
                         AND FD.BerthCode = VCM.FromPositionBerthCode
                         AND FD.BollardCode = VCM.FromPositionBollardCode
                   LEFT JOIN dbo.Bollard TD
                      ON     TD.PortCode = VCM.ToPositionPortCode
                         AND TD.QuayCode = VCM.ToPositionQuayCode
                         AND TD.BerthCode = VCM.ToPositionBerthCode
                         AND TD.BollardCode = VCM.ToPositionBollardCode
               WHERE VCM.ServiceRequestID = a.ServiceReferenceID) A),'') else 'NA' END FromBetrth,       
       CASE a.ServiceReferenceType
          WHEN 'SUPP'
          THEN
             (SELECT VCN
                FROM dbo.SuppServiceRequest
               WHERE SuppServiceRequestID = a.ServiceReferenceID)
          ELSE
             (SELECT VCN
                FROM dbo.ServiceRequest
               WHERE ServiceRequestID = a.ServiceReferenceID)
       END
          VCN,
       CASE a.ServiceReferenceType
          WHEN 'SUPP'
          THEN
             (SELECT y.SubCatName
                FROM dbo.SuppServiceRequest x
                     INNER JOIN SubCategory y ON y.SubCatCode = x.Servicetype
               WHERE SuppServiceRequestID = a.ServiceReferenceID)
          ELSE
             (SELECT y.SubCatName
                FROM dbo.ServiceRequest x
                     INNER JOIN SubCategory y
                        ON y.SubCatCode = x.MovementType
               WHERE ServiceRequestID = a.ServiceReferenceID)
       END
          MovementTypes,
       a.ServiceReferenceID,
       a.OperationType,
       b.ServiceTypeName OperationTypeName,
       a.ResourceID,
       a.ResourceType,
       CASE a.TaskStatus
          WHEN 'STRD'
          THEN
             0
          ELSE
             CASE
                WHEN (len (a.AllocSlot) = 11)
                THEN
                   CAST (
                      (  datediff (
                            mi,
                            getdate (),
                            DATEADD (
                               minute,
                               cast (
                                  substring (a.AllocSlot, 4, 2) AS NUMERIC),
                               (dateadd (
                                   hour,
                                   cast (
                                      substring (
                                         a.AllocSlot,
                                         1,
                                         (patindex ('%:%', a.AllocSlot)) - 1) AS NUMERIC),
                                   dateadd (dd,
                                            datediff (dd, 0, getDate ()),
                                            0)))))
                       / 60.0) AS NUMERIC (5, 2))
                ELSE
                   CAST (
                      (  datediff (
                            mi,
                            getdate (),
                            dateadd (
                               hour,
                               cast (
                                  substring (
                                     a.AllocSlot,
                                     1,
                                     (patindex ('%-%', a.AllocSlot)) - 1) AS NUMERIC),
                               dateadd (dd, datediff (dd, 0, getDate ()), 0)))
                       / 60.0) AS NUMERIC (5, 2))
             END
       END
          momentTimeDue,
       CONVERT (VARCHAR, a.StartTime, 120) AS StartTime,
       CONVERT (VARCHAR, a.EndTime, 120) AS EndTime,
       a.TaskStatus,
       e.SubCatName TaskStatusName,
       a.RecordStatus,
       CASE a.ServiceReferenceType
          WHEN 'SUPP'
          THEN (SELECT CASE WHEN (YVC.ATA IS NULL) THEN 'N' ELSE 'Y' END
                FROM SuppServiceRequest YSUP
                     LEFT JOIN VesselCall YVC ON YSUP.VCN = YVC.VCN
               WHERE YSUP.SuppServiceRequestID = a.ServiceReferenceID)
          ELSE 'Y'
       END
          IsExecute
  FROM dbo.ResourceAllocation a
       INNER JOIN ServiceType b ON b.ServiceTypeCode = a.OperationType
       INNER JOIN dbo.SubCategory c ON c.SubCatCode = a.ServiceReferenceType
       INNER JOIN dbo.SubCategory e ON e.SubCatCode = a.TaskStatus
 WHERE     (TaskStatus = 'CFRI' OR TaskStatus = 'ACCP' OR TaskStatus = 'STRD')
       AND a.ResourceID = @puserid
   END
GO
