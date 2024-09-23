
CREATE TABLE dbo.AutomatedSlotBlocking (
AutomatedSlotBlockingID int IDENTITY(1, 1) NOT NULL,
FromDate datetime NOT NULL,
ToDate datetime NOT NULL,
SlotFrom nvarchar(20) NULL,
SlotTo nvarchar(20) NULL,
Reason nvarchar(4) NOT NULL,
Remarks nvarchar(500) NULL,
TotalSlots int NOT NULL,
PortCode nvarchar(2) NOT NULL,
RecordStatus	nchar(1) NULL,
CreatedBy	int NOT NULL,
CreatedDate	datetime NOT NULL,
ModifiedBy	int NULL, 
ModifiedDate	datetime NOT NULL)
GO
ALTER TABLE AutomatedSlotBlocking
ADD PRIMARY KEY (AutomatedSlotBlockingID);
GO
ALTER TABLE AutomatedSlotBlocking
ADD CONSTRAINT FK_AutomatedSlotBlocking_PortCode
FOREIGN KEY (PortCode) REFERENCES Port(PortCode);
GO
ALTER TABLE AutomatedSlotBlocking
ADD CONSTRAINT FK_AutomatedSlotBlocking_Reason
FOREIGN KEY (Reason) REFERENCES SubCategory(SubCatCode);
GO
ALTER TABLE AutomatedSlotBlocking
ADD CONSTRAINT FK_AutomatedSlotBlocking_CreatedBy
FOREIGN KEY (CreatedBy) REFERENCES Users(UserID);
GO
ALTER TABLE AutomatedSlotBlocking
ADD CONSTRAINT FK_AutomatedSlotBlocking_ModifiedBy
FOREIGN KEY (ModifiedBy) REFERENCES Users(UserID);
GO


Insert Into SuperCategory
(SupCatCode, SupCatName, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) VALUES
('RESN', 'Reason', 'A', 2, getDate(), 2, getDate())
GO
Insert Into SubCategory
(SubCatCode, SupCatCode, SubCatName, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) VALUES
('RWTH', 'RESN', 'Weather', 'A',2, getDate(), 2, getDate()),
('RSTM','RESN', 'Staff Meetings', 'A',2, getDate(), 2, getDate()),
('RHRM', 'RESN', 'Harbor Master', 'A',2, getDate(), 2, getDate())
GO


ALTER TABLE ServiceRequest 
ADD SlotPeriod	nvarchar(20) NULL;	
GO
ALTER TABLE ServiceRequest 
ADD PreferredDateTime DateTime NULL;	
GO
ALTER TABLE ServiceRequest 
ADD MovementSlot nvarchar(20) NULL;	
GO




ALTER PROCEDURE [dbo].[usp_GetAutomatedSlotAllocationDet]
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
			ISNULL(toberth.BerthName,'NA') ToBerth 
			,ISNULL(curbollard.BollardName,'NA') FromBollard ,ISNULL(tobollard.BollardName,'NA') ToBollard
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
				LEFT JOIN Berth curberth on curberth.BerthCode=VC.FromPositionBerthCode and curberth.QuayCode = VC.FromPositionQuayCode and curberth.PortCode = @PortCode
				LEFT JOIN Berth toberth on toberth.BerthCode=VC.ToPositionBerthCode and toberth.QuayCode = VC.ToPositionQuayCode and toberth.PortCode = @PortCode
				LEFT JOIN Bollard curbollard on curbollard.BollardCode= VC.FromPositionBollardCode and curbollard.BerthCode = VC.FromPositionBerthCode and curbollard.QuayCode = VC.FromPositionQuayCode AND curbollard.PortCode = @PortCode
				LEFT JOIN Bollard tobollard on tobollard.BollardCode=VC.ToPositionBollardCode and tobollard.BerthCode = VC.ToPositionBerthCode and tobollard.QuayCode = VC.ToPositionQuayCode AND tobollard.PortCode = @PortCode
	 WHERE vcm.RecordStatus = 'A' AND (vcm.MovementStatus = 'CONF'  OR vcm.MovementStatus = 'BERT'  OR vcm.MovementStatus = 'SALD') AND vcm.SlotStatus ='PEND'  AND sr.RecordStatus = 'A'
			AND CONVERT(DATE, vcm.SlotDate) <= CONVERT(DATE, @SlotDate)
		AND vcm.FromPositionPortCode = @PortCode	
	 ORDER BY vcm.SlotDate asc
	
END
ELSE
  BEGIN
  		SELECT Distinct vcm.VesselCallMovementID, vcm.VCN,  vcm.ServiceRequestID,  vcm.FromPositionPortCode, vcm.FromPositionQuayCode, vcm.ToPositionPortCode,
			ISNULL(curberth.BerthName,'NA') CurrentBerth, vcm.SlotStatus, 
        vcm.SlotDate SlotDate,     
      vcm.Slot, vcm.MovementStatus,
			v.vesselName VesselName, sub1.Subcatname  VesselType, vcm.MovementType MovementTypeCode, sub3.SubCatName MovementType, 
			ISNULL(toberth.BerthName,'NA') ToBerth 		
			,ISNULL(curbollard.BollardName,'NA') FromBollard ,ISNULL(tobollard.BollardName,'NA') ToBollard
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


INSERT INTO ENTITY
(EntityCode, Moduleid, Entityname, PageUrl, OrderNo, Tokens, HasWorkflow, HasMenuItem, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, PendingTaskColumns, ControllerName) VALUES
('AUTOBLCK', (select ModuleID from Module where ModuleName = 'Vessel Traffic Services'), 'Automated Slot Blocking', 'AutomatedSlotBlocking ', 8, null, 'N', 'Y', 'A', 2, getDate(), 2,  getDate(), NULL, 'AutomatedSlotBlocking')


Insert into ENTITYPRIVILEGE(ENTITYID, SUBCATCODE, RECORDSTATUS, CREATEDBY, CREATEDDATE, MODIFIEDBY, MODIFIEDDATE)
Values
((select entityid from entity where entitycode='AUTOBLCK'), 'EDIT', 'A', 2, getDate(), 2, getDate()),
((select entityid from entity where entitycode='AUTOBLCK'), 'VIEW', 'A', 2, getDate(), 2, getDate()),
((select entityid from entity where entitycode='AUTOBLCK'), 'ADD', 'A',2, getDate(), 2, getDate()),
((select entityid from entity where entitycode='AUTOBLCK'), 'VERF', 'A', 2, getDate(), 2, getDate())


Insert into ROLEPRIVILEGE(ROLEID, ENTITYID, SUBCATCODE, RECORDSTATUS, CREATEDBY, CREATEDDATE, MODIFIEDBY, MODIFIEDDATE)
Values
((select roleid from role where rolecode='ADMN'), (select entityid from entity where entitycode='AUTOBLCK'), 'VIEW', 'A',  2, getDate(), 2, getDate()),
((select roleid from role where rolecode='ADMN'), (select entityid from entity where entitycode='AUTOBLCK'), 'EDIT', 'A',  2, getDate(), 2, getDate()),
((select roleid from role where rolecode='ADMN'), (select entityid from entity where entitycode='AUTOBLCK'), 'ADD', 'A',  2, getDate(), 2, getDate())



