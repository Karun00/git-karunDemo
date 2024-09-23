IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetAutomatedSlotAllocationDet]') AND type in (N'P'))
DROP PROCEDURE [dbo].[usp_GetAutomatedSlotAllocationDet]
GO
CREATE PROCEDURE [dbo].[usp_GetAutomatedSlotAllocationDet]
	@TaskType VARCHAR(10),
	@SlotDate DATETIME,
	@PortCode VARCHAR(4)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
IF @TaskType = 'UNPLND' 
  BEGIN
		SELECT Distinct vcm.VesselCallMovementID, vcm.VCN,  vcm.ServiceRequestID,  vcm.FromPositionPortCode, vcm.FromPositionQuayCode, vcm.ToPositionPortCode,
			ISNULL(curberth.BerthName,'NA') CurrentBerth, vcm.SlotStatus, Convert(DATE, vcm.SlotDate) SlotDate, vcm.Slot, vcm.MovementStatus,
			v.vesselName VesselName, sub1.Subcatname  VesselType, vcm.MovementType MovementTypeCode, sub3.SubCatName MovementType, 
       ISNULL(vcmtoberth.BerthName,'NA') ToBerth -- ISNULL(toberth.BerthName,'NA') ToBerth 
			,ISNULL(curbollard.BollardName,'NA') FromBollard 
      ,ISNULL(vcmtobollard.BollardName,'NA') ToBollard --,ISNULL(tobollard.BollardName,'NA') ToBollard
			, an.ETA ETA,
			ISNULL(v.LengthOverallInM, 0) LOA, ISNULL(BeamInM, 0) Beam, an.ArrDraft ArrivalDraft, 
			ISNULL(v.GrossRegisteredTonnageInMT, 0) GRT, ISNULL(v.DeadWeightTonnageInMT, 0) DWT, an.LastPortOfCall PreviousPort, 
			an.Tidal TidalCondition, an.DaylightRestriction DayLightCondition ,	vcm.FromPositionBollardCode ToBollard1, vcm.ToPositionBollardCode ToBollard2, 
			vcm.MovementDateTime ,  vcm.ETB, vcm.ETUB, vcm.ATB, vcm.ATUB, vcm.CreatedBy, vcm.CreatedDate, vcm.ModifiedBy, vcm.ModifiedDate
			,dbo.udf_CargoTypeBasedOnVCN(vcm.VCN) AS CargoType
			,dbo.udf_GetArrivalReasonForVisit(vcm.VCN) AS ReasonForVisit 
			--,an.CreatedBy UserID
			,sr.CreatedBy UserID
			,an.AnyDangerousGoodsonBoard
			,vcm.SlotDate
			
		FROM 
		(
			SELECT vcm.* 			
				FROM vesselcallmovement vcm 
			WHERE vcm.VCN 
			IN 	(
					SELECT DISTINCT vcn FROM VesselCallMovement vcm1 
					WHERE (vcm1.MovementStatus = 'CONF' OR vcm1.MovementStatus = 'BERT' OR vcm1.MovementStatus = 'SALD') 
					AND vcm1.SlotStatus='PEND' 				
					AND CONVERT(DATE, vcm1.SlotDate) <= CONVERT(DATE, @SlotDate) 			
					AND vcm1.ServiceRequestID>0
				)
		   )  vcm
				INNER JOIN ServiceRequest sr on sr.ServiceRequestID = vcm.ServiceRequestID
				INNER JOIN ArrivalNotification an on an.VCN = sr.VCN
				INNER JOIN VesselCall VC on VC.VCN = an.VCN
				INNER JOIN Vessel V on V.VesselID = an.VesselID
				INNER JOIN SubCategory sub1 on sub1.SubCatCode = v.VesselType		
				INNER JOIN Subcategory sub3 on sub3.SubCatCode = vcm.MovementType
        LEFT JOIN Berth vcmtoberth on vcmtoberth.BerthCode=VCM.ToPositionBerthCode and vcmtoberth.QuayCode = VCM.ToPositionQuayCode and vcmtoberth.PortCode = @PortCode
        LEFT JOIN Bollard vcmtobollard on vcmtobollard.BollardCode=VCM.ToPositionBollardCode and vcmtobollard.BerthCode = VCM.ToPositionBerthCode and vcmtobollard.QuayCode = VCM.ToPositionQuayCode AND vcmtobollard.PortCode = @PortCode
        
				LEFT JOIN Berth curberth on curberth.BerthCode=VC.FromPositionBerthCode and curberth.QuayCode = VC.FromPositionQuayCode and curberth.PortCode = @PortCode
				LEFT JOIN Berth toberth on toberth.BerthCode=VC.ToPositionBerthCode and toberth.QuayCode = VC.ToPositionQuayCode and toberth.PortCode = @PortCode
				LEFT JOIN Bollard curbollard on curbollard.BollardCode= VC.FromPositionBollardCode and curbollard.BerthCode = VC.FromPositionBerthCode and curbollard.QuayCode = VC.FromPositionQuayCode AND curbollard.PortCode = @PortCode
				LEFT JOIN Bollard tobollard on tobollard.BollardCode=VC.ToPositionBollardCode and tobollard.BerthCode = VC.ToPositionBerthCode and tobollard.QuayCode = VC.ToPositionQuayCode AND tobollard.PortCode = @PortCode
	 WHERE vcm.RecordStatus = 'A' AND (vcm.MovementStatus = 'CONF'  OR vcm.MovementStatus = 'BERT'  OR vcm.MovementStatus = 'SALD') AND vcm.SlotStatus ='PEND'  AND sr.RecordStatus = 'A' AND vc.ATD != NULL
			AND CONVERT(DATE, vcm.SlotDate) <= CONVERT(DATE, @SlotDate)
		AND vcm.FromPositionPortCode = @PortCode	
	 ORDER BY vcm.SlotDate asc
	
END
ELSE
  BEGIN
  		SELECT Distinct vcm.VesselCallMovementID, vcm.VCN,  vcm.ServiceRequestID,  vcm.FromPositionPortCode, vcm.FromPositionQuayCode, vcm.ToPositionPortCode,
			ISNULL(curberth.BerthName,'NA') CurrentBerth,
     CASE WHEN vcm.MovementStatus = 'BERT' OR vcm.MovementStatus = 'SALD' THEN       
      'CMPL' ELSE vcm.SlotStatus END SlotStatus,       
        vcm.SlotDate SlotDate,     
      vcm.Slot, vcm.MovementStatus,
			v.vesselName VesselName, sub1.Subcatname  VesselType, vcm.MovementType MovementTypeCode, sub3.SubCatName MovementType, 
			ISNULL(vcmtoberth.BerthName,'NA') ToBerth -- ISNULL(toberth.BerthName,'NA') ToBerth 		
			,ISNULL(curbollard.BollardName,'NA') FromBollard 
      ,ISNULL(vcmtobollard.BollardName,'NA') ToBollard --,ISNULL(tobollard.BollardName,'NA') ToBollard
			, an.ETA ETA,
			ISNULL(v.LengthOverallInM, 0) LOA, ISNULL(BeamInM, 0) Beam, an.ArrDraft ArrivalDraft, 
			ISNULL(v.GrossRegisteredTonnageInMT, 0) GRT, ISNULL(v.DeadWeightTonnageInMT, 0) DWT, an.LastPortOfCall PreviousPort, 
			an.Tidal TidalCondition, an.DaylightRestriction DayLightCondition ,	vcm.FromPositionBollardCode ToBollard1, vcm.ToPositionBollardCode ToBollard2, 
			vcm.MovementDateTime ,  vcm.ETB, vcm.ETUB, vcm.ATB, vcm.ATUB, vcm.CreatedBy, vcm.CreatedDate, vcm.ModifiedBy, vcm.ModifiedDate
			,dbo.udf_CargoTypeBasedOnVCN(vcm.VCN) AS CargoType
			,dbo.udf_GetArrivalReasonForVisit(vcm.VCN) AS ReasonForVisit 		
			,sr.CreatedBy UserID
			,an.AnyDangerousGoodsonBoard
			,vcm.SlotDate
		FROM 
		(
			SELECT vcm.* 				
			FROM vesselcallmovement vcm 
		WHERE vcm.VCN 
		IN 	(
				SELECT DISTINCT vcn FROM VesselCallMovement vcm1 
				WHERE (vcm1.MovementStatus = 'CONF' OR vcm1.MovementStatus = 'BERT'  OR vcm1.MovementStatus = 'SALD') 
				AND vcm1.SlotStatus !='PEND' 
				AND CONVERT(DATE, vcm1.SlotDate) BETWEEN CONVERT(DATE, @SlotDate) AND DATEADD(day,1,CONVERT (DATE, @SlotDate))			
				AND vcm1.ServiceRequestID>0
			)
		)  vcm
			INNER JOIN ServiceRequest sr on sr.ServiceRequestID = vcm.ServiceRequestID
			INNER JOIN ArrivalNotification an on an.VCN = sr.VCN
			INNER JOIN VesselCall VC on VC.VCN = an.VCN
			INNER JOIN Vessel V on V.VesselID = an.VesselID
			INNER JOIN SubCategory sub1 on sub1.SubCatCode = v.VesselType			
			INNER JOIN Subcategory sub3 on sub3.SubCatCode = vcm.MovementType
       LEFT JOIN Berth vcmtoberth on vcmtoberth.BerthCode=VCM.ToPositionBerthCode and vcmtoberth.QuayCode = VCM.ToPositionQuayCode and vcmtoberth.PortCode = @PortCode
       LEFT JOIN Bollard vcmtobollard on vcmtobollard.BollardCode=VCM.ToPositionBollardCode and vcmtobollard.BerthCode = VCM.ToPositionBerthCode and vcmtobollard.QuayCode = VCM.ToPositionQuayCode AND vcmtobollard.PortCode = @PortCode
        
				LEFT JOIN Berth curberth on curberth.BerthCode=VC.FromPositionBerthCode and curberth.QuayCode = VC.FromPositionQuayCode and curberth.PortCode = @PortCode
				LEFT JOIN Berth toberth on toberth.BerthCode=VC.ToPositionBerthCode and toberth.QuayCode = VC.ToPositionQuayCode and toberth.PortCode = @PortCode
				LEFT JOIN Bollard curbollard on curbollard.BollardCode= VC.FromPositionBollardCode and curbollard.BerthCode = VC.FromPositionBerthCode and curbollard.QuayCode = VC.FromPositionQuayCode AND curbollard.PortCode = @PortCode
				LEFT JOIN Bollard tobollard on tobollard.BollardCode=VC.ToPositionBollardCode and tobollard.BerthCode = VC.ToPositionBerthCode and tobollard.QuayCode = VC.ToPositionQuayCode AND tobollard.PortCode = @PortCode
		WHERE vcm.RecordStatus = 'A' AND (vcm.MovementStatus = 'CONF' OR vcm.MovementStatus = 'BERT'  OR vcm.MovementStatus = 'SALD')
			 AND vcm.SlotStatus !='PEND' AND sr.RecordStatus = 'A'		
			  AND CONVERT(DATE, vcm.SlotDate) BETWEEN CONVERT(DATE, @SlotDate)			
			 AND DATEADD(day,1,CONVERT (DATE, @SlotDate)) AND vcm.Slot != '' 
			 AND vcm.FromPositionPortCode = @PortCode	
		ORDER BY VCM.SlotDate ASC
	
END
END
GO



