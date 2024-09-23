--After fixing of issue IS-4615 - Issue in Automated Resoure Allocation, The Berth To must indicate the berth the vessel is going to dock at Refer to Automated Slot 'mouse-over'
ALTER PROCEDURE [dbo].[usp_AutomatedResourceAllocationList]
   @PortCode VARCHAR (5), @SlotDate DATE
AS
   BEGIN
      SELECT ra.AllocationDate,
             an.VCN,
             ra.ServiceReferenceID,
             sub3.SubCatName MovementType,
             ISNULL (ra.ResourceID, 0) ResourceID,
             ra.ResourceAllocationID,
             ra.ServiceReferenceType,
             st.ServiceTypeName AS ServiceTypeName,
             ra.ResourceType,
             ra.TaskStatus,
             ra.RecordStatus,
             ra.CreatedBy,
             ra.CreatedDate,
             ra.ModifiedBy,
             ra.ModifiedDate,
             ra.AllocSlot,
             v.VesselName,
             u.FirstName,
             u.LastName,
             u.FirstName + ' ' + u.LastName [Name],
             u.FirstName + ' ' + u.LastName TugResourceName,
             ISNULL (st.IsCraft, 0) IsCraft,
             ISNULL (ra.CraftID, 0) CraftID,
             c.CraftName,
             ra.OperationType ServiceTypeCode,
             ra.StartTime,
             ra.EndTime,
             ra.AcknowledgeDate,
             an.AnyDangerousGoodsonBoard,
             vt.SubCatname VesselType,
             an.ETA,
             dbo.udf_CargoTypeBasedOnVCN (an.VCN) AS CargoType,
             vcm.MovementDateTime,
             dbo.udf_GetArrivalReasonForVisit (an.VCN) AS ReasonForVisit,
             ISNULL (v.LengthOverallInM, 0) LOA,
             ISNULL (BeamInM, 0) Beam,
             an.ArrDraft ArrivalDraft,
             ISNULL (v.GrossRegisteredTonnageInMT, 0) GRT,
             ISNULL (v.DeadWeightTonnageInMT, 0) DWT,
             an.Tidal TidalCondition,
             an.DaylightRestriction DayLightCondition,
             ISNULL (curbollard.BollardName, 'NA') FromBollard,
             ISNULL (tobollard.BollardName, 'NA') ToBollard,
             ISNULL (curberth.BerthName, 'NA') CurrentBerth,
             ISNULL (toberth.BerthName, 'NA') ToBerth,
             vcm.MovementType MovementTypeCode,
             sa.SubCatName SideAlongSide
        FROM ResourceAllocation ra
             INNER JOIN ServiceType st ON st.ServiceTypeCode = ra.OperationType
             INNER JOIN ServiceRequest sr ON sr.ServiceRequestID = ra.ServiceReferenceID
             INNER JOIN ArrivalNotification an ON an.VCN = sr.VCN
             INNER JOIN Vessel v ON v.VesselID = an.VesselID
             INNER JOIN SubCategory sub1 ON sub1.SubCatCode = ra.TaskStatus
             INNER JOIN SubCategory vt ON vt.SubCatCode = v.VesselType
             INNER JOIN VesselCallMovement vcm ON vcm.ServiceRequestID = sr.ServiceRequestID
             INNER JOIN SubCategory sub3 ON sub3.SubCatCode = sr.MovementType
             INNER JOIN VesselCall vc ON vc.VCN = an.VCN
             INNER JOIN SubCategory sa ON sa.SubCatCode = sr.SideAlongSideCode
             LEFT JOIN Users u ON u.UserID = ra.ResourceID AND u.UserType = 'EMP'
             LEFT JOIN Craft c ON c.CraftID = ra.CraftID
             LEFT JOIN Berth curberth
                ON     curberth.BerthCode = vcm.FromPositionBerthCode
                   AND curberth.QuayCode = vcm.FromPositionQuayCode
                   AND curberth.PortCode = @PortCode
             LEFT JOIN Berth toberth
                ON     toberth.BerthCode = vcm.ToPositionBerthCode
                   AND toberth.QuayCode = vcm.ToPositionQuayCode
                   AND toberth.PortCode = @PortCode
             LEFT JOIN Bollard curbollard
                ON     curbollard.BollardCode = vcm.FromPositionBollardCode
                   AND curbollard.BerthCode = vcm.FromPositionBerthCode
                   AND curbollard.QuayCode = vcm.FromPositionQuayCode
                   AND curbollard.PortCode = @PortCode
             LEFT JOIN Bollard tobollard
                ON     tobollard.BollardCode = vcm.ToPositionBollardCode
                   AND tobollard.BerthCode = vcm.ToPositionBerthCode
                   AND tobollard.QuayCode = vcm.ToPositionQuayCode
                   AND tobollard.PortCode = @PortCode
       WHERE     vcm.RecordStatus = 'A'
             AND ra.ServiceReferenceType = 'VTSR'
             AND ra.RecordStatus = 'A'
             AND an.PortCode = @PortCode
             AND CONVERT (DATE, ra.AllocationDate) BETWEEN CONVERT (DATE,@SlotDate) AND DATEADD (day,1,CONVERT (DATE,@SlotDate))
      ORDER BY ra.ServiceReferenceID
   END
GO