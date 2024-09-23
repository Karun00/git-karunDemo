--Old Procedure
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetVesselInformationGIS]') AND type in (N'P'))
DROP PROCEDURE [dbo].[usp_GetVesselInformationGIS]
GO
CREATE PROCEDURE [dbo].[usp_GetVesselInformationGIS]
@FromDate DATETIME,
@PortCode VARCHAR(4),
@UserType VARCHAR(4),
@UserID int
AS
BEGIN   

  
 DECLARE @ArvEntId int 
 DECLARE @ArvPHOId int 
 DECLARE @ArvISPS int 
 DECLARE @ArvIMDG int 
 DECLARE @ArvDHM int
  
  SET @ArvEntId = (select EntityID from Entity where EntityCode = 'ARVLNOT');
  SET @ArvPHOId = (select EntityID from Entity where EntityCode = 'PHAN');
  SET @ArvISPS = (select EntityID from Entity where EntityCode = 'ISPSAN');
  SET @ArvIMDG = (select EntityID from Entity where EntityCode = 'IMDGAN');
  SET @ArvDHM = (select EntityID from Entity where EntityCode = 'DHMAN');
  
IF @UserType = 'TO' 
  BEGIN
  select * from (
select vcm.VesselCallMovementID, vcm.VCN, vcm.FromPositionQuayCode QuayCode, fb.BerthName,ag.RegisteredName Agent,
                --CONVERT(DECIMAL,an.ArrDraft) Draft, CONVERT(DECIMAL,an.DepDraft) DeptDraft, 
                CONVERT(MONEY,an.ArrDraft) Draft, CONVERT(MONEY,an.DepDraft) DeptDraft,
                v.vesselName, vt.subCatName VesselType,  
                CONVERT(DECIMAL(18,2),v.LengthOverallInM) LOA, CONVERT(DECIMAL(18,2),v.LengthOverallInM) VesselLength,  
                v.IMONo, an.NominationDate, an.ETA, an.ETD, vcm.ETB, vcm.ETUB, vcm.ATB, vcm.ATUB, bs.SubCatName BerthingSide, 
                vcm.FromPositionPortCode FromPortCode, vcm.ToPositionPortCode ToPortCode, vcm.FromPositionQuayCode FromQuayCode, 
                vcm.ToPositionQuayCode ToQuayCode, vcm.FromPositionBerthCode FromBerthCode, vcm.ToPositionBerthCode ToBerthCode, 
                vcm.FromPositionBollardCode FromBollardCode, ISNULL(CONVERT(DECIMAL(18,2),frmbol.FromMeter), 0) FromBollardMeter, 
                frmbol.Coordinates FromCoordinates, frmbol.OffsetCoordinates FromOffsetCoordinates, frmbol.MidCoordinates FromMidCoordinates, vcm.ToPositionBollardCode ToBollardCode, 
                ISNULL(CONVERT(DECIMAL(18,2),tobol.FromMeter),0) ToBollardMeter, tobol.Coordinates ToCoordinates, tobol.OffsetCoordinates ToOffsetCoordinates, tobol.MidCoordinates ToMidCoordinates, pb.BerthName PreferredBerth, 
                ab.BerthName AlternateBerth, CONVERT(DECIMAL(18,2),frmbol.FromMeter) PositionX,
                CASE 
                                WHEN CONVERT(DATE,@FromDate)  = CONVERT(DATE,an.ETA) THEN 
                                                CONVERT(INT,DATEPART(HOUR, an.ETA))
                                ELSE 
                                                CONVERT(INT,DATEPART(HOUR, an.ETA)) + 24 
                                END PositionY,
                CASE 
                                WHEN vcm.ATB is null 
                                THEN vcm.ETB
                                ELSE vcm.ATB
                END BerthTime,
                CASE
                                WHEN vcm.ATUB is null 
                                THEN vcm.ETUB 
                                ELSE vcm.ATUB 
                END UnBerthTime,          
                CASE
                                WHEN va.VCN is null 
                                THEN CONVERT(bit,0)
                                ELSE CONVERT(bit,1) 
                END isVesselArrested,   
                CASE
                                WHEN vcm.MovementStatus = 'BERT'
                                THEN (Select sc.SubCatName from ServiceRequest sr join SubCategory sc on sr.SideAlongSideCode = sc.SubCatCode where VCN = vcm.VCN and vcm.ServiceRequestID = sr.ServiceRequestID)
                                WHEN vcm.MovementType = 'ARMV'
                                THEN (Select sc.SubCatName from [dbo].ArrivalNotification ar join SubCategory sc on ar.PreferredSideDock = sc.SubCatCode where VCN = vcm.VCN)
                                WHEN vcm.MovementType =  'SHMV'
                                THEN (Select sc.SubCatName from [dbo].ServiceRequest sr join SubCategory sc on sr.SideAlongSideCode = sc.SubCatCode where VCN = vcm.VCN and vcm.ServiceRequestID = sr.ServiceRequestID)                           
                END SideAlongSide,
				CASE vcm.MovementType
                                WHEN  'ARMV'
                                THEN an.Tidal
                                WHEN 'SHMV'
                                THEN CASE when sr.IsTidal = 'Y' THEN 'A' ELSE 'I' END
                                WHEN 'SGMV'
                                THEN CASE when sr.IsTidal = 'Y' THEN 'A' ELSE 'I' END
								WHEN 'WRMV'
                                THEN CASE when sr.IsTidal = 'Y' THEN 'A' ELSE 'I' END
                END Tidal,
                dbo.udf_CargoTypeCodeBasedOnVCN(vcm.VCN) AS ArrivalCommoditiesString,
                dbo.udf_CargoTypeBasedOnVCN(vcm.VCN) AS ArrivalCommoditiesNames,
                dbo.udf_GetArrivalReasonForVisitCode(vcm.VCN) AS ArrivalReasonsString,
                dbo.udf_GetArrivalReasonForVisit(vcm.VCN) AS ReasonforVisitName,
                vcm.MovementType,
                vcm.MovementStatus,  mt.SubCatName MovementTypeName, mbb.BollardName MooringBowBollard, 
                mbs.BollardName MooringStemBollard, CONVERT(INT,DATEDIFF(Hour, an.ETA, an.ETD)) VesselWidth,
  --CONVERT(INT,DATEDIFF(s, an.ETA, an.ETD)/3600) VesselWidth,
    (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvEntId and ReferenceID =  An.VCN) IsArrivaStatus,
                      (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvPHOId and ReferenceID =  An.VCN) IsPHANStatus,
                      (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvISPS and ReferenceID =  An.VCN)  IsISPSANStatus,
                      (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvIMDG and ReferenceID =  An.VCN) IsIMDGANStatus,
                      (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvDHM and ReferenceID =  An.VCN) IsTIDALStatus,
                      
                ISNULL(CONVERT(INT,vcm.IsBanked),0) IsBanked,vcm.DoubleBankedVessel,
				frmbol.BollardName FromBollardName, tobol.BollardName ToBollardName
from VesselCallMovement vcm 
join VesselCall vc on vcm.VCN = vc.VCN
join ArrivalNotification an on an.VCN = vcm.VCN
join Agent ag on ag.AgentID = an.AgentID
join Vessel v on v.VesselID = an.VesselID
join SubCategory vt on vt.SubCatCode = v.VesselType
join SubCategory bs on bs.SubCatCode = an.PreferredSideDock
join SubCategory mt on mt.SubCatCode = vcm.MovementType
Left join Berth fb on fb.PortCode = vcm.FromPositionPortCode and fb.QuayCode = vcm.FromPositionQuayCode and fb.BerthCode = vcm.FromPositionBerthCode
join Berth pb on pb.PortCode = an.PreferredPortCode and pb.QuayCode = an.PreferredQuayCode and pb.BerthCode = an.PreferredBerthCode
left join Berth ab on ab.PortCode = an.AlternatePortCode and ab.QuayCode = an.AlternateQuayCode and  ab.BerthCode = an.AlternateBerthCode
Left join Bollard frmbol on frmbol.PortCode = vcm.FromPositionPortCode and frmbol.QuayCode = vcm.FromPositionQuayCode and frmbol.BerthCode = vcm.FromPositionBerthCode and  frmbol.BollardCode = vcm.FromPositionBollardCode
Left join Bollard tobol on tobol.PortCode = vcm.ToPositionPortCode and tobol.QuayCode = vcm.ToPositionQuayCode and tobol.BerthCode = vcm.ToPositionBerthCode and tobol.BollardCode = vcm.ToPositionBollardCode
Left join Bollard mbb on mbb.PortCode = vcm.MooringBollardBowPortCode and mbb.QuayCode = vcm.MooringBollardBowQuayCode and mbb.BerthCode = vcm.MooringBollardBowBerthCode and mbb.BollardCode = vcm.MooringBollardBowBollardCode
Left join Bollard mbs ON mbs.PortCode = vcm.MooringBollardStemPortCode AND mbs.QuayCode = vcm.MooringBollardStemQuayCode AND mbs.BerthCode = vcm.MooringBollardStemBerthCode AND mbs.BollardCode = vcm.MooringBollardStemBollardCode
Left join VesselArrestImmobilizationSAMSA va ON va.VCN = an.VCN and va.VesselArrested = 'Y' and va.VesselReleased = 'N'
Left join ServiceRequest sr on vcm.ServiceRequestID = sr.ServiceRequestID
where an.PortCode = @PortCode and vcm.RecordStatus = 'A' and an.TerminalOperatorID = @UserID
)t where isnull(IsArrivaStatus,'NEW')  != 'WFRE' and isnull(IsPHANStatus,'NEW') != 'WFRE' 
and   isnull(IsISPSANStatus,'NEW') != 'WFRE' and isnull(IsIMDGANStatus,'NEW')  != 'WFRE' 
 and isnull(IsTIDALStatus,'NEW')  != 'WFRE' 

order by ETB desc



END
ELSE
BEGIN
  
  select * from (
select vcm.VesselCallMovementID, vcm.VCN, vcm.FromPositionQuayCode QuayCode, fb.BerthName,ag.RegisteredName Agent,
                --CONVERT(DECIMAL,an.ArrDraft) Draft, CONVERT(DECIMAL,an.DepDraft) DeptDraft, 
                CONVERT(MONEY,an.ArrDraft) Draft, CONVERT(MONEY,an.DepDraft) DeptDraft,
                v.vesselName, vt.subCatName VesselType,  
                CONVERT(DECIMAL(18,2),v.LengthOverallInM) LOA, CONVERT(DECIMAL(18,2),v.LengthOverallInM) VesselLength, v.IMONo, 
                an.NominationDate, an.ETA, an.ETD, vcm.ETB, vcm.ETUB, vcm.ATB, vcm.ATUB, bs.SubCatName BerthingSide, 
                vcm.FromPositionPortCode FromPortCode, vcm.ToPositionPortCode ToPortCode, vcm.FromPositionQuayCode FromQuayCode, 
                vcm.ToPositionQuayCode ToQuayCode, vcm.FromPositionBerthCode FromBerthCode, vcm.ToPositionBerthCode ToBerthCode, 
                vcm.FromPositionBollardCode FromBollardCode, ISNULL(CONVERT(DECIMAL(18,2),frmbol.FromMeter), 0) FromBollardMeter,
                frmbol.Coordinates FromCoordinates, frmbol.OffsetCoordinates FromOffsetCoordinates, frmbol.MidCoordinates FromMidCoordinates, vcm.ToPositionBollardCode ToBollardCode, 
                ISNULL(CONVERT(DECIMAL(18,2),tobol.FromMeter),0) ToBollardMeter, tobol.Coordinates ToCoordinates, tobol.OffsetCoordinates ToOffsetCoordinates, tobol.MidCoordinates ToMidCoordinates, pb.BerthName PreferredBerth, 
                ab.BerthName AlternateBerth, CONVERT(DECIMAL(18,2),frmbol.FromMeter) PositionX,
                CASE 
                                WHEN CONVERT(DATE,@FromDate)  = CONVERT(DATE,an.ETA) THEN 
                                                CONVERT(INT,DATEPART(HOUR, an.ETA))
                                ELSE 
                                                CONVERT(INT,DATEPART(HOUR, an.ETA)) + 24 
                                END PositionY,
                CASE 
                                WHEN vcm.ATB is null 
                                THEN vcm.ETB
                                ELSE vcm.ATB
                END BerthTime,
                CASE
                                WHEN vcm.ATUB is null 
                                THEN vcm.ETUB 
                                ELSE vcm.ATUB 
                END UnBerthTime,          
                CASE
                                WHEN va.VCN is null 
                                THEN CONVERT(bit,0)
                                ELSE CONVERT(bit,1) 
                END isVesselArrested,   
                CASE
                                WHEN vcm.MovementStatus = 'BERT'
                                THEN (Select sc.SubCatName from ServiceRequest sr join SubCategory sc on sr.SideAlongSideCode = sc.SubCatCode where VCN = vcm.VCN and vcm.ServiceRequestID = sr.ServiceRequestID)
                                WHEN vcm.MovementType = 'ARMV'
                                THEN (Select sc.SubCatName from [dbo].ArrivalNotification ar join SubCategory sc on ar.PreferredSideDock = sc.SubCatCode where VCN = vcm.VCN)
                                WHEN vcm.MovementType =  'SHMV'
                                THEN (Select sc.SubCatName from [dbo].ServiceRequest sr join SubCategory sc on sr.SideAlongSideCode = sc.SubCatCode where VCN = vcm.VCN and vcm.ServiceRequestID = sr.ServiceRequestID)                           
                END SideAlongSide,
				CASE vcm.MovementType
                                WHEN  'ARMV'
                                THEN an.Tidal
                                WHEN 'SHMV'
                                THEN CASE when sr.IsTidal = 'Y' THEN 'A' ELSE 'I' END
                                WHEN 'SGMV'
                                THEN CASE when sr.IsTidal = 'Y' THEN 'A' ELSE 'I' END
								WHEN 'WRMV'
                                THEN CASE when sr.IsTidal = 'Y' THEN 'A' ELSE 'I' END
                END Tidal,
                dbo.udf_CargoTypeCodeBasedOnVCN(vcm.VCN) AS ArrivalCommoditiesString,
                dbo.udf_CargoTypeBasedOnVCN(vcm.VCN) AS ArrivalCommoditiesNames,
                dbo.udf_GetArrivalReasonForVisitCode(vcm.VCN) AS ArrivalReasonsString,
                dbo.udf_GetArrivalReasonForVisit(vcm.VCN) AS ReasonforVisitName,
                vcm.MovementType,
                vcm.MovementStatus,  mt.SubCatName MovementTypeName, mbb.BollardName MooringBowBollard, 
                mbs.BollardName MooringStemBollard,CONVERT(INT,DATEDIFF(Hour, an.ETA, an.ETD)) VesselWidth,
  --CONVERT(INT,DATEDIFF(s, an.ETA, an.ETD)/3600) VesselWidth,
   (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvEntId and ReferenceID =  An.VCN) IsArrivaStatus,
                      (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvPHOId and ReferenceID =  An.VCN) IsPHANStatus,
                      (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvISPS and ReferenceID =  An.VCN)  IsISPSANStatus,
                      (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvIMDG and ReferenceID =  An.VCN) IsIMDGANStatus,
                      (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvDHM and ReferenceID =  An.VCN) IsTIDALStatus,
                
                ISNULL(CONVERT(INT,vcm.IsBanked),0) IsBanked,vcm.DoubleBankedVessel,
				frmbol.BollardName FromBollardName, tobol.BollardName ToBollardName
from VesselCallMovement vcm 
join VesselCall vc on vcm.VCN = vc.VCN
join ArrivalNotification an on an.VCN = vcm.VCN
join Agent ag on ag.AgentID = an.AgentID
join Vessel v on v.VesselID = an.VesselID
join SubCategory vt on vt.SubCatCode = v.VesselType
join SubCategory bs on bs.SubCatCode = an.PreferredSideDock
join SubCategory mt on mt.SubCatCode = vcm.MovementType
Left join Berth fb on fb.PortCode = vcm.FromPositionPortCode and fb.QuayCode = vcm.FromPositionQuayCode and fb.BerthCode = vcm.FromPositionBerthCode
join Berth pb on pb.PortCode = an.PreferredPortCode and pb.QuayCode = an.PreferredQuayCode and pb.BerthCode = an.PreferredBerthCode
left join Berth ab on ab.PortCode = an.AlternatePortCode and ab.QuayCode = an.AlternateQuayCode and  ab.BerthCode = an.AlternateBerthCode
Left join Bollard frmbol on frmbol.PortCode = vcm.FromPositionPortCode and frmbol.QuayCode = vcm.FromPositionQuayCode and frmbol.BerthCode = vcm.FromPositionBerthCode and  frmbol.BollardCode = vcm.FromPositionBollardCode
Left join Bollard tobol on tobol.PortCode = vcm.ToPositionPortCode and tobol.QuayCode = vcm.ToPositionQuayCode and tobol.BerthCode = vcm.ToPositionBerthCode and tobol.BollardCode = vcm.ToPositionBollardCode
Left join Bollard mbb on mbb.PortCode = vcm.MooringBollardBowPortCode and mbb.QuayCode = vcm.MooringBollardBowQuayCode and mbb.BerthCode = vcm.MooringBollardBowBerthCode and mbb.BollardCode = vcm.MooringBollardBowBollardCode
Left join Bollard mbs ON mbs.PortCode = vcm.MooringBollardStemPortCode AND mbs.QuayCode = vcm.MooringBollardStemQuayCode AND mbs.BerthCode = vcm.MooringBollardStemBerthCode AND mbs.BollardCode = vcm.MooringBollardStemBollardCode
Left join VesselArrestImmobilizationSAMSA va ON va.VCN = an.VCN and va.VesselArrested = 'Y' and va.VesselReleased = 'N'
Left join ServiceRequest sr on vcm.ServiceRequestID = sr.ServiceRequestID
where an.PortCode = @PortCode and vcm.RecordStatus = 'A'
)t where isnull(IsArrivaStatus,'NEW')  != 'WFRE' and isnull(IsPHANStatus,'NEW') != 'WFRE' 
                and   isnull(IsISPSANStatus,'NEW') != 'WFRE' 
                and isnull(IsIMDGANStatus,'NEW')  != 'WFRE' 
                and isnull(IsTIDALStatus,'NEW')  != 'WFRE' 
order by ETB desc


END
END
GO



-- New Procedure
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetVesselInformationGIS]') AND type in (N'P'))
DROP PROCEDURE [dbo].[usp_GetVesselInformationGIS]
GO
CREATE PROCEDURE [dbo].[usp_GetVesselInformationGIS]
@FromDate DATETIME,
@PortCode VARCHAR(4),
@UserType VARCHAR(4),
@UserID int
AS
BEGIN   

  
 DECLARE @ArvEntId int 
 DECLARE @ArvPHOId int 
 DECLARE @ArvISPS int 
 DECLARE @ArvIMDG int 
 DECLARE @ArvDHM int
  
  SET @ArvEntId = (select EntityID from Entity where EntityCode = 'ARVLNOT');
  SET @ArvPHOId = (select EntityID from Entity where EntityCode = 'PHAN');
  SET @ArvISPS = (select EntityID from Entity where EntityCode = 'ISPSAN');
  SET @ArvIMDG = (select EntityID from Entity where EntityCode = 'IMDGAN');
  SET @ArvDHM = (select EntityID from Entity where EntityCode = 'DHMAN');
  
IF @UserType = 'TO' 
  BEGIN
  select * from (
select vcm.VesselCallMovementID, vcm.VCN, vcm.FromPositionQuayCode QuayCode, fb.BerthName,ag.RegisteredName Agent,
                --CONVERT(DECIMAL,an.ArrDraft) Draft, CONVERT(DECIMAL,an.DepDraft) DeptDraft, 
                CONVERT(MONEY,an.ArrDraft) Draft, CONVERT(MONEY,an.DepDraft) DeptDraft,
                v.vesselName, vt.subCatName VesselType,  
                CONVERT(DECIMAL(18,2),v.LengthOverallInM) LOA, CONVERT(DECIMAL(18,2),v.LengthOverallInM) VesselLength,  
                v.IMONo, an.NominationDate, an.ETA, an.ETD, vcm.ETB, vcm.ETUB, vcm.ATB, vcm.ATUB, bs.SubCatName BerthingSide, 
                vcm.FromPositionPortCode FromPortCode, vcm.ToPositionPortCode ToPortCode, vcm.FromPositionQuayCode FromQuayCode, 
                vcm.ToPositionQuayCode ToQuayCode, vcm.FromPositionBerthCode FromBerthCode, vcm.ToPositionBerthCode ToBerthCode, 
                vcm.FromPositionBollardCode FromBollardCode, ISNULL(CONVERT(DECIMAL(18,2),frmbol.FromMeter), 0) FromBollardMeter, 
                frmbol.Coordinates FromCoordinates, frmbol.OffsetCoordinates FromOffsetCoordinates, frmbol.MidCoordinates FromMidCoordinates, vcm.ToPositionBollardCode ToBollardCode, 
                ISNULL(CONVERT(DECIMAL(18,2),tobol.FromMeter),0) ToBollardMeter, tobol.Coordinates ToCoordinates, tobol.OffsetCoordinates ToOffsetCoordinates, tobol.MidCoordinates ToMidCoordinates, pb.BerthName PreferredBerth, 
                ab.BerthName AlternateBerth, CONVERT(DECIMAL(18,2),frmbol.FromMeter) PositionX,
                CASE 
                                WHEN CONVERT(DATE,@FromDate)  = CONVERT(DATE,an.ETA) THEN 
                                                CONVERT(INT,DATEPART(HOUR, an.ETA))
                                ELSE 
                                                CONVERT(INT,DATEPART(HOUR, an.ETA)) + 24 
                                END PositionY,
                CASE 
                                WHEN vcm.ATB is null 
                                THEN vcm.ETB
                                ELSE vcm.ATB
                END BerthTime,
                CASE
                                WHEN vcm.ATUB is null 
                                THEN vcm.ETUB 
                                ELSE vcm.ATUB 
                END UnBerthTime,          
                CASE
                                WHEN va.VCN is null 
                                THEN CONVERT(bit,0)
                                ELSE CONVERT(bit,1) 
                END isVesselArrested,   
                CASE
                                WHEN vcm.MovementStatus = 'BERT'
                                THEN (Select sc.SubCatName from ServiceRequest sr join SubCategory sc on sr.SideAlongSideCode = sc.SubCatCode where VCN = vcm.VCN and vcm.ServiceRequestID = sr.ServiceRequestID)
                                WHEN vcm.MovementType = 'ARMV'
                                THEN (Select sc.SubCatName from [dbo].ArrivalNotification ar join SubCategory sc on ar.PreferredSideDock = sc.SubCatCode where VCN = vcm.VCN)
                                WHEN vcm.MovementType =  'SHMV'
                                THEN (Select sc.SubCatName from [dbo].ServiceRequest sr join SubCategory sc on sr.SideAlongSideCode = sc.SubCatCode where VCN = vcm.VCN and vcm.ServiceRequestID = sr.ServiceRequestID)                           
                END SideAlongSide,
				CASE vcm.MovementType
                                WHEN  'ARMV'
                                THEN an.Tidal
                                WHEN 'SHMV'
                                THEN CASE when sr.IsTidal = 'Y' THEN 'A' ELSE 'I' END
                                WHEN 'SGMV'
                                THEN CASE when sr.IsTidal = 'Y' THEN 'A' ELSE 'I' END
								WHEN 'WRMV'
                                THEN CASE when sr.IsTidal = 'Y' THEN 'A' ELSE 'I' END
                END Tidal,
                dbo.udf_CargoTypeCodeBasedOnVCN(vcm.VCN) AS ArrivalCommoditiesString,
                dbo.udf_CargoTypeBasedOnVCN(vcm.VCN) AS ArrivalCommoditiesNames,
                dbo.udf_GetArrivalReasonForVisitCode(vcm.VCN) AS ArrivalReasonsString,
                dbo.udf_GetArrivalReasonForVisit(vcm.VCN) AS ReasonforVisitName,
                vcm.MovementType,
                vcm.MovementStatus,  mt.SubCatName MovementTypeName, mbb.BollardName MooringBowBollard, 
                mbs.BollardName MooringStemBollard, CONVERT(INT,DATEDIFF(Hour, an.ETA, an.ETD)) VesselWidth,
  --CONVERT(INT,DATEDIFF(s, an.ETA, an.ETD)/3600) VesselWidth,
    (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvEntId and ReferenceID =  An.VCN) IsArrivaStatus,
                      (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvPHOId and ReferenceID =  An.VCN) IsPHANStatus,
                      (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvISPS and ReferenceID =  An.VCN)  IsISPSANStatus,
                      (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvIMDG and ReferenceID =  An.VCN) IsIMDGANStatus,
                      (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvDHM and ReferenceID =  An.VCN) IsTIDALStatus,
                      
                ISNULL(CONVERT(INT,vcm.IsBanked),0) IsBanked,vcm.DoubleBankedVessel,
				frmbol.BollardName FromBollardName, tobol.BollardName ToBollardName
from VesselCallMovement vcm 
join VesselCall vc on vcm.VCN = vc.VCN
join ArrivalNotification an on an.VCN = vcm.VCN
join Agent ag on ag.AgentID = an.AgentID
join Vessel v on v.VesselID = an.VesselID
join SubCategory vt on vt.SubCatCode = v.VesselType
join SubCategory bs on bs.SubCatCode = an.PreferredSideDock
join SubCategory mt on mt.SubCatCode = vcm.MovementType
Left join Berth fb on fb.PortCode = vcm.FromPositionPortCode and fb.QuayCode = vcm.FromPositionQuayCode and fb.BerthCode = vcm.FromPositionBerthCode
join Berth pb on pb.PortCode = an.PreferredPortCode and pb.QuayCode = an.PreferredQuayCode and pb.BerthCode = an.PreferredBerthCode
left join Berth ab on ab.PortCode = an.AlternatePortCode and ab.QuayCode = an.AlternateQuayCode and  ab.BerthCode = an.AlternateBerthCode
Left join Bollard frmbol on frmbol.PortCode = vcm.FromPositionPortCode and frmbol.QuayCode = vcm.FromPositionQuayCode and frmbol.BerthCode = vcm.FromPositionBerthCode and  frmbol.BollardCode = vcm.FromPositionBollardCode
Left join Bollard tobol on tobol.PortCode = vcm.ToPositionPortCode and tobol.QuayCode = vcm.ToPositionQuayCode and tobol.BerthCode = vcm.ToPositionBerthCode and tobol.BollardCode = vcm.ToPositionBollardCode
Left join Bollard mbb on mbb.PortCode = vcm.MooringBollardBowPortCode and mbb.QuayCode = vcm.MooringBollardBowQuayCode and mbb.BerthCode = vcm.MooringBollardBowBerthCode and mbb.BollardCode = vcm.MooringBollardBowBollardCode
Left join Bollard mbs ON mbs.PortCode = vcm.MooringBollardStemPortCode AND mbs.QuayCode = vcm.MooringBollardStemQuayCode AND mbs.BerthCode = vcm.MooringBollardStemBerthCode AND mbs.BollardCode = vcm.MooringBollardStemBollardCode
Left join VesselArrestImmobilizationSAMSA va ON va.VCN = an.VCN and va.VesselArrested = 'Y' and va.VesselReleased = 'N'
Left join ServiceRequest sr on vcm.ServiceRequestID = sr.ServiceRequestID
where an.PortCode = @PortCode and vcm.RecordStatus = 'A' and an.TerminalOperatorID = @UserID and vc.ATD IS NULL and vcm.MovementStatus NOT IN ('MPEN', 'SALD') and  vcm.MovementType != 'SGMV'
)t where isnull(IsArrivaStatus,'NEW')  != 'WFRE' and isnull(IsPHANStatus,'NEW') != 'WFRE' 
and   isnull(IsISPSANStatus,'NEW') != 'WFRE' and isnull(IsIMDGANStatus,'NEW')  != 'WFRE' 
 and isnull(IsTIDALStatus,'NEW')  != 'WFRE' 

order by ETB desc



END
ELSE
BEGIN
  
  select * from (
select vcm.VesselCallMovementID, vcm.VCN, vcm.FromPositionQuayCode QuayCode, fb.BerthName,ag.RegisteredName Agent,
                --CONVERT(DECIMAL,an.ArrDraft) Draft, CONVERT(DECIMAL,an.DepDraft) DeptDraft, 
                CONVERT(MONEY,an.ArrDraft) Draft, CONVERT(MONEY,an.DepDraft) DeptDraft,
                v.vesselName, vt.subCatName VesselType,  
                CONVERT(DECIMAL(18,2),v.LengthOverallInM) LOA, CONVERT(DECIMAL(18,2),v.LengthOverallInM) VesselLength, v.IMONo, 
                an.NominationDate, an.ETA, an.ETD, vcm.ETB, vcm.ETUB, vcm.ATB, vcm.ATUB, bs.SubCatName BerthingSide, 
                vcm.FromPositionPortCode FromPortCode, vcm.ToPositionPortCode ToPortCode, vcm.FromPositionQuayCode FromQuayCode, 
                vcm.ToPositionQuayCode ToQuayCode, vcm.FromPositionBerthCode FromBerthCode, vcm.ToPositionBerthCode ToBerthCode, 
                vcm.FromPositionBollardCode FromBollardCode, ISNULL(CONVERT(DECIMAL(18,2),frmbol.FromMeter), 0) FromBollardMeter,
                frmbol.Coordinates FromCoordinates, frmbol.OffsetCoordinates FromOffsetCoordinates, frmbol.MidCoordinates FromMidCoordinates, vcm.ToPositionBollardCode ToBollardCode, 
                ISNULL(CONVERT(DECIMAL(18,2),tobol.FromMeter),0) ToBollardMeter, tobol.Coordinates ToCoordinates, tobol.OffsetCoordinates ToOffsetCoordinates, tobol.MidCoordinates ToMidCoordinates, pb.BerthName PreferredBerth, 
                ab.BerthName AlternateBerth, CONVERT(DECIMAL(18,2),frmbol.FromMeter) PositionX,
                CASE 
                                WHEN CONVERT(DATE,@FromDate)  = CONVERT(DATE,an.ETA) THEN 
                                                CONVERT(INT,DATEPART(HOUR, an.ETA))
                                ELSE 
                                                CONVERT(INT,DATEPART(HOUR, an.ETA)) + 24 
                                END PositionY,
                CASE 
                                WHEN vcm.ATB is null 
                                THEN vcm.ETB
                                ELSE vcm.ATB
                END BerthTime,
                CASE
                                WHEN vcm.ATUB is null 
                                THEN vcm.ETUB 
                                ELSE vcm.ATUB 
                END UnBerthTime,          
                CASE
                                WHEN va.VCN is null 
                                THEN CONVERT(bit,0)
                                ELSE CONVERT(bit,1) 
                END isVesselArrested,   
                CASE
                                WHEN vcm.MovementStatus = 'BERT'
                                THEN (Select sc.SubCatName from ServiceRequest sr join SubCategory sc on sr.SideAlongSideCode = sc.SubCatCode where VCN = vcm.VCN and vcm.ServiceRequestID = sr.ServiceRequestID)
                                WHEN vcm.MovementType = 'ARMV'
                                THEN (Select sc.SubCatName from [dbo].ArrivalNotification ar join SubCategory sc on ar.PreferredSideDock = sc.SubCatCode where VCN = vcm.VCN)
                                WHEN vcm.MovementType =  'SHMV'
                                THEN (Select sc.SubCatName from [dbo].ServiceRequest sr join SubCategory sc on sr.SideAlongSideCode = sc.SubCatCode where VCN = vcm.VCN and vcm.ServiceRequestID = sr.ServiceRequestID)                           
                END SideAlongSide,
				CASE vcm.MovementType
                                WHEN  'ARMV'
                                THEN an.Tidal
                                WHEN 'SHMV'
                                THEN CASE when sr.IsTidal = 'Y' THEN 'A' ELSE 'I' END
                                WHEN 'SGMV'
                                THEN CASE when sr.IsTidal = 'Y' THEN 'A' ELSE 'I' END
								WHEN 'WRMV'
                                THEN CASE when sr.IsTidal = 'Y' THEN 'A' ELSE 'I' END
                END Tidal,
                dbo.udf_CargoTypeCodeBasedOnVCN(vcm.VCN) AS ArrivalCommoditiesString,
                dbo.udf_CargoTypeBasedOnVCN(vcm.VCN) AS ArrivalCommoditiesNames,
                dbo.udf_GetArrivalReasonForVisitCode(vcm.VCN) AS ArrivalReasonsString,
                dbo.udf_GetArrivalReasonForVisit(vcm.VCN) AS ReasonforVisitName,
                vcm.MovementType,
                vcm.MovementStatus,  mt.SubCatName MovementTypeName, mbb.BollardName MooringBowBollard, 
                mbs.BollardName MooringStemBollard,CONVERT(INT,DATEDIFF(Hour, an.ETA, an.ETD)) VesselWidth,
  --CONVERT(INT,DATEDIFF(s, an.ETA, an.ETD)/3600) VesselWidth,
   (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvEntId and ReferenceID =  An.VCN) IsArrivaStatus,
                      (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvPHOId and ReferenceID =  An.VCN) IsPHANStatus,
                      (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvISPS and ReferenceID =  An.VCN)  IsISPSANStatus,
                      (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvIMDG and ReferenceID =  An.VCN) IsIMDGANStatus,
                      (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvDHM and ReferenceID =  An.VCN) IsTIDALStatus,
                
                ISNULL(CONVERT(INT,vcm.IsBanked),0) IsBanked,vcm.DoubleBankedVessel,
				frmbol.BollardName FromBollardName, tobol.BollardName ToBollardName
from VesselCallMovement vcm 
join VesselCall vc on vcm.VCN = vc.VCN
join ArrivalNotification an on an.VCN = vcm.VCN
join Agent ag on ag.AgentID = an.AgentID
join Vessel v on v.VesselID = an.VesselID
join SubCategory vt on vt.SubCatCode = v.VesselType
join SubCategory bs on bs.SubCatCode = an.PreferredSideDock
join SubCategory mt on mt.SubCatCode = vcm.MovementType
Left join Berth fb on fb.PortCode = vcm.FromPositionPortCode and fb.QuayCode = vcm.FromPositionQuayCode and fb.BerthCode = vcm.FromPositionBerthCode
join Berth pb on pb.PortCode = an.PreferredPortCode and pb.QuayCode = an.PreferredQuayCode and pb.BerthCode = an.PreferredBerthCode
left join Berth ab on ab.PortCode = an.AlternatePortCode and ab.QuayCode = an.AlternateQuayCode and  ab.BerthCode = an.AlternateBerthCode
Left join Bollard frmbol on frmbol.PortCode = vcm.FromPositionPortCode and frmbol.QuayCode = vcm.FromPositionQuayCode and frmbol.BerthCode = vcm.FromPositionBerthCode and  frmbol.BollardCode = vcm.FromPositionBollardCode
Left join Bollard tobol on tobol.PortCode = vcm.ToPositionPortCode and tobol.QuayCode = vcm.ToPositionQuayCode and tobol.BerthCode = vcm.ToPositionBerthCode and tobol.BollardCode = vcm.ToPositionBollardCode
Left join Bollard mbb on mbb.PortCode = vcm.MooringBollardBowPortCode and mbb.QuayCode = vcm.MooringBollardBowQuayCode and mbb.BerthCode = vcm.MooringBollardBowBerthCode and mbb.BollardCode = vcm.MooringBollardBowBollardCode
Left join Bollard mbs ON mbs.PortCode = vcm.MooringBollardStemPortCode AND mbs.QuayCode = vcm.MooringBollardStemQuayCode AND mbs.BerthCode = vcm.MooringBollardStemBerthCode AND mbs.BollardCode = vcm.MooringBollardStemBollardCode
Left join VesselArrestImmobilizationSAMSA va ON va.VCN = an.VCN and va.VesselArrested = 'Y' and va.VesselReleased = 'N'
Left join ServiceRequest sr on vcm.ServiceRequestID = sr.ServiceRequestID
where an.PortCode = @PortCode and vcm.RecordStatus = 'A' and vc.ATD IS NULL and vcm.MovementStatus NOT IN ('MPEN', 'SALD') and  vcm.MovementType != 'SGMV'
)t where isnull(IsArrivaStatus,'NEW')  != 'WFRE' and isnull(IsPHANStatus,'NEW') != 'WFRE' 
                and   isnull(IsISPSANStatus,'NEW') != 'WFRE' 
                and isnull(IsIMDGANStatus,'NEW')  != 'WFRE' 
                and isnull(IsTIDALStatus,'NEW')  != 'WFRE' 
order by ETB desc


END
END
GO
