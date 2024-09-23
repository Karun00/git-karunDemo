
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
			--vcm.PrevBollard1 FromBollard1, vcm.PrevBollard2 FromBollard2
			,ISNULL(curbollard.BollardName,'NA') FromBollard ,ISNULL(tobollard.BollardName,'NA') ToBollard
			, an.ETA ETA,
			ISNULL(v.LengthOverallInM, 0) LOA, ISNULL(BeamInM, 0) Beam, an.ArrDraft ArrivalDraft, 
			ISNULL(v.GrossRegisteredTonnageInMT, 0) GRT, ISNULL(v.DeadWeightTonnageInMT, 0) DWT, an.LastPortOfCall PreviousPort, 
			an.Tidal TidalCondition, an.DaylightRestriction DayLightCondition ,	vcm.FromPositionBollardCode ToBollard1, vcm.ToPositionBollardCode ToBollard2, 
			vcm.MovementDateTime ,  vcm.ETB, vcm.ETUB, vcm.ATB, vcm.ATUB, vcm.CreatedBy, vcm.CreatedDate, vcm.ModifiedBy, vcm.ModifiedDate
			,dbo.udf_CargoTypeBasedOnVCN(vcm.VCN) AS CargoType--, 'NA' CargoType
			,dbo.udf_GetArrivalReasonForVisit(vcm.VCN) AS ReasonForVisit -- sub2.SubCatName ReasonForVisit,
			--,an.CreatedBy UserID
			,sr.CreatedBy UserID
			,an.AnyDangerousGoodsonBoard
			,vcm.SlotDate
			
		FROM 
		(
			SELECT vcm.* 
				--LAG(vcm.FromPositionBerthCode) OVER (ORDER BY vcm.VCN, vcm.ServiceRequestId) PrevBerth, 
				--LAG(vcm.FromPositionBollardCode) OVER (ORDER BY vcm.VCN, vcm.ServiceRequestId) PrevBollard1, 
				--LAG(vcm.ToPositionBollardCode) OVER (ORDER BY vcm.VCN, vcm.ServiceRequestId) PrevBollard2
				FROM vesselcallmovement vcm 
			WHERE vcm.VCN 
			IN 	(
					SELECT DISTINCT vcn FROM VesselCallMovement vcm1 
					WHERE (vcm1.MovementStatus = 'CONF' OR vcm1.MovementStatus = 'BERT' OR vcm1.MovementStatus = 'SALD') 
					AND vcm1.SlotStatus='PEND' 
					--AND CONVERT(DATE, vcm1.ETB) <= CONVERT(DATE, @SlotDate) 
					AND CONVERT(DATE, vcm1.SlotDate) <= CONVERT(DATE, @SlotDate) 
					--AND CONVERT(DATE, vcm1.MovementDateTime) <= CONVERT(DATE, @SlotDate) 
					AND vcm1.ServiceRequestID>0
				)
		   )  vcm
				INNER JOIN ServiceRequest sr on sr.ServiceRequestID = vcm.ServiceRequestID
				INNER JOIN ArrivalNotification an on an.VCN = sr.VCN
				INNER JOIN VesselCall VC on VC.VCN = an.VCN
				INNER JOIN Vessel V on V.VesselID = an.VesselID
				INNER JOIN SubCategory sub1 on sub1.SubCatCode = v.VesselType
				--INNER JOIN Subcategory sub2 on sub2.SubCatCode = an.ReasonForVisit
				INNER JOIN Subcategory sub3 on sub3.SubCatCode = vcm.MovementType
				LEFT JOIN Berth curberth on curberth.BerthCode=VC.FromPositionBerthCode and curberth.QuayCode = VC.FromPositionQuayCode and curberth.PortCode = @PortCode
				LEFT JOIN Berth toberth on toberth.BerthCode=VC.ToPositionBerthCode and toberth.QuayCode = VC.ToPositionQuayCode and toberth.PortCode = @PortCode
				LEFT JOIN Bollard curbollard on curbollard.BollardCode= VC.FromPositionBollardCode and curbollard.BerthCode = VC.FromPositionBerthCode and curbollard.QuayCode = VC.FromPositionQuayCode AND curbollard.PortCode = @PortCode
				LEFT JOIN Bollard tobollard on tobollard.BollardCode=VC.ToPositionBollardCode and tobollard.BerthCode = VC.ToPositionBerthCode and tobollard.QuayCode = VC.ToPositionQuayCode AND tobollard.PortCode = @PortCode
	 WHERE vcm.RecordStatus = 'A' AND (vcm.MovementStatus = 'CONF'  OR vcm.MovementStatus = 'BERT'  OR vcm.MovementStatus = 'SALD') AND vcm.SlotStatus ='PEND'  AND sr.RecordStatus = 'A'
		--AND CONVERT(DATE, vcm.ETB) <= CONVERT(DATE, @SlotDate)
		AND CONVERT(DATE, vcm.SlotDate) <= CONVERT(DATE, @SlotDate)
		AND vcm.FromPositionPortCode = @PortCode
		--AND CONVERT(DATE, vcm.MovementDateTime) <= CONVERT(DATE, @SlotDate)
	 --ORDER BY vcm.ETB asc
	 ORDER BY vcm.SlotDate asc
	 --ORDER BY vcm.MovementDateTime asc
END
ELSE
  BEGIN
  		SELECT Distinct vcm.VesselCallMovementID, vcm.VCN,  vcm.ServiceRequestID,  vcm.FromPositionPortCode, vcm.FromPositionQuayCode, vcm.ToPositionPortCode,
			ISNULL(curberth.BerthName,'NA') CurrentBerth, vcm.SlotStatus, Convert(DATE, vcm.SlotDate) SlotDate, vcm.Slot, vcm.MovementStatus,
			v.vesselName VesselName, sub1.Subcatname  VesselType, vcm.MovementType MovementTypeCode, sub3.SubCatName MovementType, 
			ISNULL(toberth.BerthName,'NA') ToBerth 
			--vcm.PrevBollard1 FromBollard1, vcm.PrevBollard2 FromBollard2
			,ISNULL(curbollard.BollardName,'NA') FromBollard ,ISNULL(tobollard.BollardName,'NA') ToBollard
			, an.ETA ETA,
			ISNULL(v.LengthOverallInM, 0) LOA, ISNULL(BeamInM, 0) Beam, an.ArrDraft ArrivalDraft, 
			ISNULL(v.GrossRegisteredTonnageInMT, 0) GRT, ISNULL(v.DeadWeightTonnageInMT, 0) DWT, an.LastPortOfCall PreviousPort, 
			an.Tidal TidalCondition, an.DaylightRestriction DayLightCondition ,	vcm.FromPositionBollardCode ToBollard1, vcm.ToPositionBollardCode ToBollard2, 
			vcm.MovementDateTime ,  vcm.ETB, vcm.ETUB, vcm.ATB, vcm.ATUB, vcm.CreatedBy, vcm.CreatedDate, vcm.ModifiedBy, vcm.ModifiedDate
			,dbo.udf_CargoTypeBasedOnVCN(vcm.VCN) AS CargoType--, 'NA' CargoType
			,dbo.udf_GetArrivalReasonForVisit(vcm.VCN) AS ReasonForVisit -- sub2.SubCatName ReasonForVisit,
			--,an.CreatedBy UserID
			,sr.CreatedBy UserID
			,an.AnyDangerousGoodsonBoard
			,vcm.SlotDate
		FROM 
		(
			SELECT vcm.* 
				--LAG(vcm.FromPositionBerthCode) OVER (ORDER BY vcm.VCN, vcm.ServiceRequestId) PrevBerth, 
				--LAG(vcm.FromPositionBollardCode) OVER (ORDER BY vcm.VCN, vcm.ServiceRequestId) PrevBollard1, 
				--LAG(vcm.ToPositionBollardCode) OVER (ORDER BY vcm.VCN, vcm.ServiceRequestId) PrevBollard2
			FROM vesselcallmovement vcm 
		WHERE vcm.VCN 
		IN 	(
				SELECT DISTINCT vcn FROM VesselCallMovement vcm1 
				WHERE (vcm1.MovementStatus = 'CONF' OR vcm1.MovementStatus = 'BERT'  OR vcm1.MovementStatus = 'SALD') 
				AND vcm1.SlotStatus !='PEND' 
				--AND CONVERT(DATE, vcm1.ETB) = CONVERT(DATE, GETDATE())
				--AND CONVERT(DATE, vcm1.ETB) BETWEEN CONVERT(DATE, @SlotDate) AND DATEADD(day,1,CONVERT (DATE, @SlotDate))
				AND CONVERT(DATE, vcm1.SlotDate) BETWEEN CONVERT(DATE, @SlotDate) AND DATEADD(day,1,CONVERT (DATE, @SlotDate))
				--AND CONVERT(DATE, vcm1.MovementDateTime) BETWEEN CONVERT(DATE, @SlotDate) AND DATEADD(day,1,CONVERT (DATE, @SlotDate))
				AND vcm1.ServiceRequestID>0
			)
		)  vcm
			INNER JOIN ServiceRequest sr on sr.ServiceRequestID = vcm.ServiceRequestID
			INNER JOIN ArrivalNotification an on an.VCN = sr.VCN
			INNER JOIN VesselCall VC on VC.VCN = an.VCN
			INNER JOIN Vessel V on V.VesselID = an.VesselID
			INNER JOIN SubCategory sub1 on sub1.SubCatCode = v.VesselType
			--INNER JOIN Subcategory sub2 on sub2.SubCatCode = an.ReasonForVisit
			INNER JOIN Subcategory sub3 on sub3.SubCatCode = vcm.MovementType
				LEFT JOIN Berth curberth on curberth.BerthCode=VC.FromPositionBerthCode and curberth.QuayCode = VC.FromPositionQuayCode and curberth.PortCode = @PortCode
				LEFT JOIN Berth toberth on toberth.BerthCode=VC.ToPositionBerthCode and toberth.QuayCode = VC.ToPositionQuayCode and toberth.PortCode = @PortCode
				LEFT JOIN Bollard curbollard on curbollard.BollardCode= VC.FromPositionBollardCode and curbollard.BerthCode = VC.FromPositionBerthCode and curbollard.QuayCode = VC.FromPositionQuayCode AND curbollard.PortCode = @PortCode
				LEFT JOIN Bollard tobollard on tobollard.BollardCode=VC.ToPositionBollardCode and tobollard.BerthCode = VC.ToPositionBerthCode and tobollard.QuayCode = VC.ToPositionQuayCode AND tobollard.PortCode = @PortCode
		WHERE vcm.RecordStatus = 'A' AND (vcm.MovementStatus = 'CONF' OR vcm.MovementStatus = 'BERT'  OR vcm.MovementStatus = 'SALD')
			 AND vcm.SlotStatus !='PEND' AND sr.RecordStatus = 'A'
			 --AND CONVERT(DATE, vcm.ETB) BETWEEN CONVERT(DATE, @SlotDate)
			  AND CONVERT(DATE, vcm.SlotDate) BETWEEN CONVERT(DATE, @SlotDate)
			 --AND CONVERT(DATE, vcm.MovementDateTime) BETWEEN CONVERT(DATE, @SlotDate)
			 AND DATEADD(day,1,CONVERT (DATE, @SlotDate)) AND vcm.Slot != '' 
			 AND vcm.FromPositionPortCode = @PortCode
		--ORDER BY VCM.ETB ASC
		ORDER BY VCM.SlotDate ASC
		--ORDER BY VCM.MovementDateTime ASC
END
END

GO


Update WorkflowTask  set HasRemarks = 'Y' where EntityID = (select EntityId from Entity where EntityCode='SERVREQ') and WorkflowTaskCode = 'WSRE'



INSERT INTO NotificationTemplate(NotificationTemplateCode, NotificationTemplateName, EntityID, WorkflowTaskCode, PortCode, IsEmail, EmailSubject,EmailTemplate, IsSMS, SMSTemplate, IsSysMessage, SysMessageTemplate, NotificationTemplateBase, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
SELECT 'SRCC','Service Request Cancellation Request', (select EntityId from Entity where EntityCode='SERVREQ'),'WFCC', NULL,'Y','Service Request Cancellation Request','<p style="margin-bottom: 12pt;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Greetings &nbsp;[UserName],</span></p><p style="margin-bottom: 12pt;"></p><br/><meta charset="utf-8"/><title>Integrated Port Management System</title><table align="center" border="0" cellpadding="0" cellspacing="0" width="550"><tbody><tr><td width="328" height="71" valign="bottom" bgcolor="#dddddd"><table width="548" border="0" align="center" cellpadding="0" cellspacing="0"><tbody><tr><td valign="bottom"><table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td height="39" colspan="2" bgcolor="#FFFFFF">&nbsp;</td></tr><tr style="background-color: #1d1d1d;"><td height="30"  style="font-family: Verdana;font-size: 14px;font-family: Open Sans, sans-serif;color: #fff; padding-left:2px;">Integrated Port Management System</td><td  style="font-family: Verdana;font-size: 12px;color: #f03225;text-align: right;font-weight: bold;"></td></tr></table></td><td width="17" height="69" valign="bottom" bgcolor="#dddddd"><img height="69" src="https://ipms.transnet.net/Content/Images/email-logo.jpg" width="114"/></td></tr></tbody></table></td></tr><tr><td style="font-family: Verdana;font-size: 12px;background-color: #aacdb3;padding: 25px;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">This is to inform you that your Service Request Cancellation Request has been raised with the below details.</span><br/><br/><table border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 12px;background-color: #c9ead1;border: 2px solid #88a790;padding: 6px 4px;color: #3d5343;" width="100%"><tbody><tr><td><span style="font-size: 18px;color: #3d5343;padding-bottom: 5px;display: block;">Service Request Details</span><table border="0" cellpadding="5" cellspacing="1" style="font-family: Verdana;font-size: 12px;" width="100%"><tbody><tr style="background-color: #aacdb3;"><td width="38%">VCN</td><td width="62%">%VCN%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Vessel Name</td><td width="62%">%VesselName%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Movement Type</td><td width="62%">%MovementName%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Movement Date & Time</td><td width="62%">%MovementDateTime%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Submitted Date & Time</td><td width="62%">%SubmittedDateTime%</td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td><p><img height="15" src="https://ipms.transnet.net/Content/Images/bottom-bar.jpg" width="550"/></p></td></tr></tbody></table><br/><table width="550" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 11px;"><tr><td height="30" colspan="2"style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Kind Regards</td></tr><tr><td width="140" valign="top"><img src="https://ipms.transnet.net/Content/Images/transet-logo-email-sign.png" width="128" height="119" /></td><td width="410"><p><strong>IPMS ADMIN </strong><br /></p><table width="100%" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 10px;"><tr><td width="23"><img width="23" height="16" src="https://ipms.transnet.net/Content/Images/email-temp-phone-icon.jpg"/></td><td height="25">(+27 86) 010 9330</td></tr><tr><td>&nbsp;</td><td height="25"><a href="http://www.transnet.net">www.transnet.net</a></td></tr></table><p>&nbsp;</p></td></tr></table>'
,'Y','This is to inform you that your Service Request Cancellation Request has been raised for Vessel Name %VesselName% VCN %VCN%.','Y','This is to inform you that your Service Request Cancellation Request has been raised for Vessel Name %VesselName% VCN %VCN%.','R','A',1,getDate(),1,getDate()

INSERT INTO NotificationPort(NotificationTemplateCode ,PortCode ,RecordStatus ,CreatedBy ,CreatedDate ,ModifiedBy ,ModifiedDate) SELECT 'SRCC',Portcode,'A' as RecordStatus,1 as CreatedBy, getDate() as CreatedDate,1 as ModifiedBy,getDate() as ModifiedDate from Port;

INSERT INTO NotificationRole(NotificationTemplateCode, RoleID, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) SELECT  'SRCC' as NotificationTemplateCode,Roleid,'A' as RecordStatus,1 as CreatedBy, getDate() as Createddate, 1 as ModifiedBy, getDate() as ModifiedDate from (select RoleID from Role where Rolecode in (select value from udf_SplitString('AGNT,VTC',',')))  A; 



INSERT INTO NotificationTemplate(NotificationTemplateCode, NotificationTemplateName, EntityID, WorkflowTaskCode, PortCode, IsEmail, EmailSubject,EmailTemplate, IsSMS, SMSTemplate, IsSysMessage, SysMessageTemplate, NotificationTemplateBase, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
SELECT 'SRSA','Service Request Cancel', (select EntityId from Entity where EntityCode='SERVREQ'),'WSSA', NULL,'Y','Service Request Cancel','<p style="margin-bottom: 12pt;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Greetings &nbsp;[UserName],</span></p><p style="margin-bottom: 12pt;"></p><br/><meta charset="utf-8"/><title>Integrated Port Management System</title><table align="center" border="0" cellpadding="0" cellspacing="0" width="550"><tbody><tr><td width="328" height="71" valign="bottom" bgcolor="#dddddd"><table width="548" border="0" align="center" cellpadding="0" cellspacing="0"><tbody><tr><td valign="bottom"><table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td height="39" colspan="2" bgcolor="#FFFFFF">&nbsp;</td></tr><tr style="background-color: #1d1d1d;"><td height="30"  style="font-family: Verdana;font-size: 14px;font-family: Open Sans, sans-serif;color: #fff; padding-left:2px;">Integrated Port Management System</td><td  style="font-family: Verdana;font-size: 12px;color: #f03225;text-align: right;font-weight: bold;"></td></tr></table></td><td width="17" height="69" valign="bottom" bgcolor="#dddddd"><img height="69" src="https://ipms.transnet.net/Content/Images/email-logo.jpg" width="114"/></td></tr></tbody></table></td></tr><tr><td style="font-family: Verdana;font-size: 12px;background-color: #aacdb3;padding: 25px;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">This is to inform you that your Service Request with the below details has been cancelled.</span><br/><br/><table border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 12px;background-color: #c9ead1;border: 2px solid #88a790;padding: 6px 4px;color: #3d5343;" width="100%"><tbody><tr><td><span style="font-size: 18px;color: #3d5343;padding-bottom: 5px;display: block;">Service Request Details</span><table border="0" cellpadding="5" cellspacing="1" style="font-family: Verdana;font-size: 12px;" width="100%"><tbody><tr style="background-color: #aacdb3;"><td width="38%">VCN</td><td width="62%">%VCN%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Vessel Name</td><td width="62%">%VesselName%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Movement Type</td><td width="62%">%MovementName%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Movement Date & Time</td><td width="62%">%MovementDateTime%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Submitted Date & Time</td><td width="62%">%SubmittedDateTime%</td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td><p><img height="15" src="https://ipms.transnet.net/Content/Images/bottom-bar.jpg" width="550"/></p></td></tr></tbody></table><br/><table width="550" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 11px;"><tr><td height="30" colspan="2"style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Kind Regards</td></tr><tr><td width="140" valign="top"><img src="https://ipms.transnet.net/Content/Images/transet-logo-email-sign.png" width="128" height="119" /></td><td width="410"><p><strong>IPMS ADMIN </strong><br /></p><table width="100%" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 10px;"><tr><td width="23"><img width="23" height="16" src="https://ipms.transnet.net/Content/Images/email-temp-phone-icon.jpg"/></td><td height="25">(+27 86) 010 9330</td></tr><tr><td>&nbsp;</td><td height="25"><a href="http://www.transnet.net">www.transnet.net</a></td></tr></table><p>&nbsp;</p></td></tr></table>'
,'Y','This  is inform you that %MovementName% Service Request has been Cancelled for VCN %VCN%,Vessel Name %VesselName%','Y','This  is inform you that %MovementName% Service Request has been Cancelled for VCN %VCN%,Vessel Name %VesselName%','R','A',1,getDate(),1,getDate()

INSERT INTO NotificationPort(NotificationTemplateCode ,PortCode ,RecordStatus ,CreatedBy ,CreatedDate ,ModifiedBy ,ModifiedDate) SELECT 'SRSA',Portcode,'A' as RecordStatus,1 as CreatedBy, getDate() as CreatedDate,1 as ModifiedBy,getDate() as ModifiedDate from Port;

INSERT INTO NotificationRole(NotificationTemplateCode, RoleID, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) SELECT  'SRSA' as NotificationTemplateCode,Roleid,'A' as RecordStatus,1 as CreatedBy, getDate() as Createddate, 1 as ModifiedBy, getDate() as ModifiedDate from (select RoleID from Role where Rolecode in (select value from udf_SplitString('AGNT,VTC',',')))  A; 


