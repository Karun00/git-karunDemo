Insert Into PortGeneralConfig
(PortCode, ConfigName, ConfigValue, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, ConfigLabelName, GroupName) VALUES
('DB', 'SAPPosting', '7', 'A', 2, getDate(), 2, getDate(), 'SAP Posting ETA Days', 'SAPPosting')

GO

Insert Into SubCategory
(SubCatCode, SupCatCode, SubCatName, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) VALUES
('22', 'PSC', 'Fishing Trawler', 'A', 1, getDate(), 1,  getDate()), 
('23', 'PSC', 'Lay-up', 'A', 1,  getDate(), 1,  getDate())


GO

CREATE FUNCTION [dbo].[udf_GetArrivalReasonSAPCode] (
   @p_VCN    VARCHAR(MAX))
   RETURNS NVARCHAR (MAX)
   WITH
   EXEC AS CALLER
AS
   BEGIN
   
    DECLARE @result VARCHAR(100)
SELECT @result =
CASE WHEN ar.Reason = 'BUNK' THEN '02' 
WHEN ar.Reason = 'LABY' THEN '23' 
WHEN ar.Reason = 'REPA' THEN '14'
WHEN ar.Reason = 'DRYD' THEN '14'
WHEN ar.Reason = 'PASS' AND  
v.VesselType IN ('PASH','V090','V091','V092','V180') THEN '12'

WHEN (ar.Reason = 'LOAD' OR ar.Reason = 'DISC' OR ar.Reason = 'TRAN') AND  
v.VesselType IN ('BBLK','BC','V011','V012','V013','V014','V015','V024') THEN '01'

WHEN (ar.Reason = 'LOAD' OR ar.Reason = 'DISC' OR ar.Reason = 'TRAN') AND  
v.VesselType IN ('CTSH','V009','V013','V058','V061','V110','V176') THEN '04'

WHEN (ar.Reason = 'LOAD' OR ar.Reason = 'DISC' OR ar.Reason = 'TRAN') AND  
v.VesselType IN ('V048','V060','V061') THEN '06'

WHEN (ar.Reason = 'LOAD' OR ar.Reason = 'DISC' OR ar.Reason = 'TRAN') AND  
v.VesselType IN ('V183','YT') THEN '10'

WHEN (ar.Reason = 'LOAD' OR ar.Reason = 'DISC' OR ar.Reason = 'TRAN') AND  
v.VesselType IN ('OCT','OPT','V006','V016','V023','V025','V030','V038','V046','V051','V055','V056','V057','V083','V084','V099','V128','V129','V145','V147','V170','V172','V177','V178','V179') 
THEN '20'

WHEN (ar.Reason = 'LOAD' OR ar.Reason = 'DISC' OR ar.Reason = 'TRAN') AND  
v.VesselType IN ('FSH','V044','V045','V046','V047','V048','V152','V153','V154','V155','V135','V160') THEN '22'

ELSE '03' END 
from ArrivalNotification an 
join Vessel v on v.VesselID = an.VesselID
join ArrivalReason ar on ar.VCN = an.VCN
where an.VCN = @p_VCN
and ar.ArrivalReasonID = (select min(ArrivalReasonID) from ArrivalReason where VCN = @p_VCN)

RETURN  @result 

END
GO


CREATE PROCEDURE [dbo].[usp_GetAutoSAPVesselDetails]
 AS
BEGIN
  
  DECLARE @ETADays int
   
   SELECT @ETADays = ConfigValue from PortGeneralConfig where ConfigName = 'SAPPosting'  

      BEGIN
       SELECT DISTINCT AN.VCN,AN.PortCode,
            coalesce(VC.RecentAgentID,AN.AgentID) AgentID,
            AN.VesselID,
            AN.VoyageIn as VOYIN,
            AN.VoyageOut as VOYOUT,
            SA.OrganizationCode as VKORG,
            dbo.udf_FormatDateTime(coalesce(VC.ETA,AN.ETA),'YYYY-MM-DD') as STREDA,
            dbo.udf_FormatDateTime(coalesce(VC.ETD,AN.ETD),'YYYY-MM-DD') as STREDD,
            VSA.SAPAccountNo as KUNNR,
            dbo.udf_FormatDateTime(VC.BreakWaterIn,'YYYY-MM-DD') as AED,           
             REPLACE(CONVERT(VARCHAR(8),VC.BreakWaterIn,108),':','') as AET,
             dbo.udf_FormatDateTime(VC.BreakWaterOut,'YYYY-MM-DD') as DED,           
             REPLACE(CONVERT(VARCHAR(8),VC.BreakWaterOut,108),':','') as DET,
             NP.InternationalPortCode as PORTCALL,
            LP.InternationalPortCode as PORTORIGIN,
            AN.CreatedBy USERID,
            AN.CreatedDate,
            SUBSTRING(BR.ShortName, 1, 6) ZZBERTH,
            AN.PORTCODE PORTCODE,            
            [dbo].[udf_GetArrivalReasonSAPCode](AN.VCN) CODE
            from 
             ArrivalNotification AN
            left JOIN VesselCall VC ON VC.VCN = AN.VCN
               left join dbo.PortRegistry NP ON NP.PortCode = AN.NextPortOfCall
              left join dbo.PortRegistry LP ON LP.PortCode = AN.LastPortOfCall
            inner join SalesOrganization SA on AN.PortCode = SA.PortCode
            inner join Berth BR ON BR.PortCode = AN.PreferredPortCode and BR.QuayCode = AN.PreferredQuayCode AND BR.BerthCode = AN.PreferredBerthCode
            left join VesselSAPAccount VSA on AN.VesselID = VSA.VesselID
            left join SAPPosting sp on sp.ReferenceNo = an.VCN  and SP.MessageType='CRAR' 
            where 
             DATEADD(day,-@ETADays,CONVERT (DATE, VC.ETA)) <=  CONVERT (DATE, getDate()) and AN.RecordStatus = 'A'
            and SP.ReferenceNo IS NULL and DATENAME(YEAR,an.ETA) >= 2018
              order by AN.VCN asc   
      END   
      
      
  END
GO


ALTER PROCEDURE [dbo].[usp_GetToPostSapData]
   @A_vcn  VARCHAR (12),
   @MSGTYPE VARCHAR (6),
   @RevenueAgentAccNo VARCHAR (50)
AS
BEGIN
IF @MSGTYPE ='CRMO' 
      BEGIN 
	  select          
             
            sl.OrganizationCode SALESORGANIZATION,
            vs.SAPAccountNo SHIPTOPARTY,
			rp.SAPAccNo SOLDTOPARTY
            from VesselCall vc
            inner join dbo.ArrivalNotification ar ON ar.VCN = vc.VCN
            inner join SalesOrganization sl on ar.PortCode = sl.PortCode
            inner join VesselSAPAccount vs on ar.VesselID = vs.VesselID
			left join RevenuePosting rp on ar.VCN = rp.vcn
            where vc.VCN = @A_vcn and rp.RevenuePostingID in 
			(select max(RevenuePostingID) from RevenuePosting rpg where rpg.vcn=@A_vcn and rpg.SAPAccNo = @RevenueAgentAccNo)
      
      end
      
else
  BEGIN
            select DISTINCT AN.VCN,AN.PortCode,
            coalesce(VC.RecentAgentID,AN.AgentID) AgentID,
            AN.VesselID,
            AN.VoyageIn as VOYIN,
            AN.VoyageOut as VOYOUT,
            SA.OrganizationCode as VKORG,
            dbo.udf_FormatDateTime(coalesce(VC.ETA,AN.ETA),'YYYY-MM-DD') as STREDA,
            dbo.udf_FormatDateTime(coalesce(VC.ETD,AN.ETD),'YYYY-MM-DD') as STREDD,
            VSA.SAPAccountNo as KUNNR,
            dbo.udf_FormatDateTime(VC.BreakWaterIn,'YYYY-MM-DD') as AED,           
             REPLACE(CONVERT(VARCHAR(8),VC.BreakWaterIn,108),':','') as AET,
            dbo.udf_FormatDateTime(VC.BreakWaterOut,'YYYY-MM-DD') as DED,           
             REPLACE(CONVERT(VARCHAR(8),VC.BreakWaterOut,108),':','') as DET,           
             NP.InternationalPortCode as PORTCALL,
            LP.InternationalPortCode as PORTORIGIN,
            AN.CreatedBy,
            AN.CreatedDate,
            SUBSTRING(BR.ShortName, 1, 6) ZZBERTH
            from 
             ArrivalNotification AN
            left JOIN VesselCall VC ON VC.VCN = AN.VCN
               left join dbo.PortRegistry NP ON NP.PortCode = AN.NextPortOfCall
              left join dbo.PortRegistry LP ON LP.PortCode = AN.LastPortOfCall
            inner join SalesOrganization SA on AN.PortCode = SA.PortCode
             inner join Berth BR ON BR.PortCode = AN.PreferredPortCode and BR.QuayCode = AN.PreferredQuayCode AND BR.BerthCode = AN.PreferredBerthCode
            inner join VesselSAPAccount VSA on AN.VesselID = VSA.VesselID
            where AN.VCN=@A_vcn
  end
         
END
GO
