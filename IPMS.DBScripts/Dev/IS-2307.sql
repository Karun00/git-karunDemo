CREATE FUNCTION [dbo].[udf_Wego_VesselCount] (@CargoType VARCHAR(4),@portCode  VARCHAR(4),
--@fromDate VARCHAR (25), @toDate VARCHAR (25)) 
@fromDate DATE, @toDate DATE) 
RETURNS INT
AS


 BEGIN
 DECLARE @Value INT
 
 IF(@CargoType = 'BULK')
 
     BEGIN
        SELECT @Value = Count(distinct ar.VCN) from 
         ArrivalNotification ar
         join ArrivalCommodity ac ON ac.VCN = ar.VCN 
         join VesselCall vc on ar.VCN = vc.VCN
         where ac.CargoType IN ('CTY8', 'CTY6', 'CTY5', 'CTY9', 'CTY7') 
         AND ar.RecordStatus = 'A' AND vc.ATD IS NOT NULL
          AND (ar.PortCode = @portCode OR @portCode IS NULL) 
         AND  CAST (vc.BreakWaterOut AS DATE) BETWEEN @fromDate AND @toDate
         -- AND dbo.udf_FormatDateTime(vc.BreakWaterOut,'yyyy-mm-dd') BETWEEN @fromDate AND @toDate 
      END
  ELSE      
        SELECT @Value = Count(distinct ar.VCN) from 
         ArrivalNotification ar
         join ArrivalCommodity ac ON ac.VCN = ar.VCN 
         join VesselCall vc on ar.VCN = vc.VCN
         where ac.CargoType = @CargoType 
         AND ar.RecordStatus = 'A' AND vc.ATD IS NOT NULL
          AND (ar.PortCode = @portCode OR @portCode IS NULL) 
          AND CAST (vc.BreakWaterOut AS DATE) BETWEEN @fromDate AND @toDate
        --  AND dbo.udf_FormatDateTime(vc.BreakWaterOut,'yyyy-mm-dd') BETWEEN   @fromDate AND @toDate 
      
       
RETURN @Value

END
GO



-------------------------------------------------------------------------------


CREATE FUNCTION [dbo].[udf_Wego_ResourceAllocTime] (@VCN VARCHAR(50),@MovementType  VARCHAR(4), @Flag  VARCHAR(2)) 
RETURNS varchar(50)
AS
 
-- DECLARE @VCN VARCHAR(50)
-- DECLARE @MovementType  VARCHAR(4)
-- DECLARE @Flag  VARCHAR(2) 
-- 
 BEGIN
--DECLARE @Value varchar(50)
 DECLARE @Value DATETIME

--SET @MovementType = 'SGMV'
--SET @VCN = 'VCNCT1800003'
--SET @Flag = ''

IF(@MovementType = 'ARMV')
BEGIN
  IF(@Flag = 'LI')
      SELECT  @Value =   
           min(SHF1.LastLineIn)
        FROM ArrivalNotification AN      
      INNER JOIN ServiceRequest sr ON SR.VCN = AN.VCN  
      INNER JOIN VesselCallmovement VCM ON VCM.VCN = AN.VCN and VCM.ServiceRequestID = sr.ServiceRequestID
      INNER JOIN ResourceAllocation R on SR.ServiceRequestID = R.ServiceReferenceID and R.ServiceReferenceType = 'VTSR' and R.OperationType = 'BRTH'
      INNER JOIN ShiftingBerthingTaskExecution SHF1 on R.ResourceAllocationID = SHF1.ResourceAllocationID  
      WHERE AN.VCN = @VCN AND sr.RecordStatus = 'A' AND R.RecordStatus = 'A'
       AND VCM.MovementType = 'ARMV'
  ELSE IF(@Flag = 'PB') 
      SELECT  @Value =   
           min(P1.PilotOnBoard)
        FROM ArrivalNotification AN      
      INNER JOIN ServiceRequest sr ON SR.VCN = AN.VCN  
      INNER JOIN VesselCallmovement VCM ON VCM.VCN = AN.VCN and VCM.ServiceRequestID = sr.ServiceRequestID
      INNER JOIN ResourceAllocation R on SR.ServiceRequestID = R.ServiceReferenceID and R.ServiceReferenceType = 'VTSR' and R.OperationType = 'PILT'
      INNER JOIN PilotageServiceRecording P1 on R.ResourceAllocationID = P1.ResourceAllocationID  
      WHERE AN.VCN = @VCN AND sr.RecordStatus = 'A' AND R.RecordStatus = 'A'
        AND VCM.MovementType = 'ARMV'  
     END
ELSE IF(@MovementType = 'SGMV')
         SELECT  @Value =   
           min(P1.PilotOnBoard)
        FROM ArrivalNotification AN      
      INNER JOIN ServiceRequest sr ON SR.VCN = AN.VCN  
      INNER JOIN VesselCallmovement VCM ON VCM.VCN = AN.VCN and VCM.ServiceRequestID = sr.ServiceRequestID
      INNER JOIN ResourceAllocation R on SR.ServiceRequestID = R.ServiceReferenceID and R.ServiceReferenceType = 'VTSR' and R.OperationType = 'PILT'
      INNER JOIN PilotageServiceRecording P1 on R.ResourceAllocationID = P1.ResourceAllocationID  
      WHERE AN.VCN = @VCN AND sr.RecordStatus = 'A' AND R.RecordStatus = 'A'
        AND VCM.MovementType = 'SGMV'

--      SELECT  @Value =   
--           max(P12.PilotOff)
--         FROM ArrivalNotification AN      
--      INNER JOIN ServiceRequest sr ON SR.VCN = AN.VCN  
--      INNER JOIN VesselCallmovement VCM ON VCM.VCN = AN.VCN and VCM.ServiceRequestID = sr.ServiceRequestID
--      INNER JOIN ResourceAllocation R on SR.ServiceRequestID = R.ServiceReferenceID and R.ServiceReferenceType = 'VTSR' and R.OperationType = 'PILT'
--      INNER JOIN PilotageServiceRecording P12 on R.ResourceAllocationID = P12.ResourceAllocationID  
--      WHERE AN.VCN = @VCN AND sr.RecordStatus = 'A' AND R.RecordStatus = 'A'
--       AND VCM.MovementType = 'SGMV' 
       
RETURN @Value

END
GO



-------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[usp_GetWegoDashBoardDetails]
  @FromDate DATE, @ToDate DATE,  @PortCode VARCHAR (50)
   WITH
   EXECUTE AS CALLER
AS
   BEGIN     

         SELECT 
s.CargoType,
s.CargoCode
 ,SUM(s.GRT) GRT
,SUM(LOA) LOA
,SUM(STAT) STAT
,SUM(VesselDelayAnchorage) VesselDelayAnchorage
,SUM(NPAManueringTime) NPAManueringTime
,SUM(PilotageIn) PilotageIn
,SUM(NPAManueringTime) + SUM(PilotageIn) MarineServiceTimeIn
--,sum(0.0) AdherenceRequested
,SUM(MarineServiceTimeOut) MarineServiceTimeOut
from
(SELECT wego.VCN
,v.GrossRegisteredTonnageInMT GRT
,v.LengthOverallInM LOA
,wego.SubCatName CargoType
,wego.CargoCode CargoCode
,ISNULL(CAST((datediff(mi,VC.BreakWaterIn,VC.BreakWaterOut)/ 60.0)as numeric(10,2)), '0.0') STAT
,ISNULL(CAST((datediff(mi,VC.PortLimitIn,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'PB'))/ 60.0)as numeric(10,2)), '0.0') VesselDelayAnchorage
,ISNULL(CAST((datediff(mi,VC.BreakWaterIn,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'LI'))/ 60.0)as numeric(10,2)), '0.0') NPAManueringTime
,ISNULL(CAST((datediff(mi,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'PB'),VC.BreakWaterIn)/ 60.0)as numeric(10,2)), '0.0') PilotageIn
,ISNULL(CAST((datediff(mi,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'SGMV', ''),VC.BreakWaterOut)/ 60.0)as numeric(10,2)), '0.0') MarineServiceTimeOut
,VC.BreakWaterIn
,VC.BreakWaterOut
,VC.PortLimitIn
,VC.PortLimitOut
,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'LI') LastLineIn 
,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'PB') PilotOnBoard 
,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'SGMV', '') SPilotOnBoard   
 from
(SELECT DISTINCT ar.VCN
,sb.SubCatName, sb.SubCatCode CargoCode
FROM ArrivalNotification ar
JOIN Vessel v on v.VesselID = ar.VesselID
JOIN ArrivalCommodity ac on ar.VCN = ac.VCN
JOIN SubCategory  sb ON sb.SubCatCode = ac.CargoType
JOIN VesselCall VCL on VCL.VCN = ar.VCN and VCL.FromPositionPortCode = @PortCode 
AND  CAST(VCL.BreakWaterOut AS DATE) BETWEEN @FromDate AND @ToDate
AND VCL.RecordStatus = 'A'
GROUP BY ar.VCN, sb.SubCatName, sb.SubCatCode
HAVING  sb.SubCatCode IN ('CTY2','CTY3','CTY1', 'CTY4','CTNC', 'CTBK', 'DG')) wego
INNER JOIN ArrivalNotification an on an.VCN = wego.VCN	
INNER JOIN VesselCall VC on VC.VCN = an.VCN
INNER JOIN Vessel V on V.VesselID = an.VesselID
WHERE VC.ATD IS NOT NULL AND an.RecordStatus = 'A' 
--AND dbo.udf_FormatDateTime(VC.BreakWaterOut,'yyyy-mm-dd') BETWEEN @FromDate AND @ToDate 
  AND  CAST (VC.BreakWaterOut AS DATE) BETWEEN @FromDate AND @ToDate
  AND (an.PortCode = @PortCode OR @PortCode IS NULL) 
) s
GROUP BY s.CargoType, s.CargoCode

UNION 
  SELECT 
 'Bulk' CargoType,
 'BULK' CargoCode,
-- s1.CargoType,
-- s1.CargoCode,
 ISNULL(SUM(s1.GRT), 0) GRT
,ISNULL(SUM(LOA), 0) LOA
,ISNULL(SUM(STAT), 0) STAT
,ISNULL(SUM(VesselDelayAnchorage), 0) VesselDelayAnchorage
,ISNULL(SUM(NPAManueringTime), 0) NPAManueringTime
,ISNULL(SUM(PilotageIn), 0) PilotageIn
,ISNULL(SUM(NPAManueringTime), 0) + ISNULL(SUM(PilotageIn), 0) MarineServiceTimeIn
--,sum(0.0) AdherenceRequested
,ISNULL(SUM(MarineServiceTimeOut), 0) MarineServiceTimeOut
from
(SELECT wego.VCN
,v.GrossRegisteredTonnageInMT GRT
,v.LengthOverallInM LOA
,wego.SubCatName CargoType
,wego.CargoCode CargoCode
,ISNULL(CAST((datediff(mi,VC.BreakWaterIn,VC.BreakWaterOut)/ 60.0)as numeric(10,2)), '0.0') STAT
,ISNULL(CAST((datediff(mi,VC.PortLimitIn,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'PB'))/ 60.0)as numeric(10,2)), '0.0') VesselDelayAnchorage
,ISNULL(CAST((datediff(mi,VC.BreakWaterIn,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'LI'))/ 60.0)as numeric(10,2)), '0.0') NPAManueringTime
,ISNULL(CAST((datediff(mi,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'PB'),VC.BreakWaterIn)/ 60.0)as numeric(10,2)), '0.0') PilotageIn
,ISNULL(CAST((datediff(mi,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'SGMV', ''),VC.BreakWaterOut)/ 60.0)as numeric(10,2)), '0.0') MarineServiceTimeOut
,VC.BreakWaterIn
,VC.BreakWaterOut
,VC.PortLimitIn
,VC.PortLimitOut
,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'LI') LastLineIn 
,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'PB') PilotOnBoard 
,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'SGMV', '') SPilotOnBoard   
 from
(SELECT DISTINCT ar.VCN
,sb.SubCatName, sb.SubCatCode CargoCode
FROM ArrivalNotification ar
JOIN Vessel v on v.VesselID = ar.VesselID
JOIN ArrivalCommodity ac on ar.VCN = ac.VCN
JOIN SubCategory  sb ON sb.SubCatCode = ac.CargoType
JOIN VesselCall VCL on VCL.VCN = ar.VCN 
where  sb.SubCatCode IN ('CTY8','CTY6','CTY5', 'CTY9','CTY7')
AND ar.PortCode = @PortCode AND  CAST(VCL.BreakWaterOut AS DATE) BETWEEN @FromDate AND @ToDate
AND VCL.RecordStatus = 'A'
) wego
INNER JOIN ArrivalNotification an on an.VCN = wego.VCN	
INNER JOIN VesselCall VC on VC.VCN = an.VCN
INNER JOIN Vessel V on V.VesselID = an.VesselID
where VC.ATD IS NOT NULL AND an.RecordStatus = 'A' AND VC.RecordStatus = 'A'
AND  CAST (VC.BreakWaterOut AS DATE) BETWEEN @FromDate AND @ToDate
AND (an.PortCode = @PortCode OR @PortCode IS NULL) 
--AND dbo.udf_FormatDateTime(VC.BreakWaterOut,'yyyy-mm-dd') BETWEEN @FromDate AND @ToDate 
) s1

  END
GO

-------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[usp_GetWegoVesselCargoType]  
    @fromDate DATE, @toDate DATE, @portCode VARCHAR (50)
   WITH
   EXECUTE AS CALLER
AS
   BEGIN    
   
   SELECT
   [dbo].[udf_Wego_VesselCount]('CTY2', @portCode, @fromDate, @toDate) VesselAutomative,
   [dbo].[udf_Wego_VesselCount]('CTY3', @portCode, @fromDate, @toDate) VesselBreakBulk,
   [dbo].[udf_Wego_VesselCount]('CTY1',  @portCode, @fromDate, @toDate) VesselContainer,
   [dbo].[udf_Wego_VesselCount]('BULK', @portCode, @fromDate, @toDate) VesselBulk,
   [dbo].[udf_Wego_VesselCount]('CTY4',  @portCode, @fromDate, @toDate) VesselLiquidBulk,
   [dbo].[udf_Wego_VesselCount]('CTNC', @portCode, @fromDate, @toDate) VesselNonOperational,
   [dbo].[udf_Wego_VesselCount]('CTBK',  @portCode, @fromDate, @toDate) VesselBunkers,
   [dbo].[udf_Wego_VesselCount]('DG', @portCode, @fromDate, @toDate) VesselPassengers
   
   
   END
GO


----------------------------------------------------------------------------------


Insert Into Entity
(EntityCode, Moduleid, Entityname, PageUrl, OrderNo, Tokens, HasWorkflow, HasMenuItem, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, PendingTaskColumns, ControllerName) VALUES
('WEGODH', (select ModuleID from Module where ModuleName = 'Port Performance'), 'Wego DashBoard', 'WegoDashBoard', 9, '', 'N', 'Y', 'A', 2, getDate(), 2, getDate(), '', 'WegoDashBoard')
GO

Insert into ENTITYPRIVILEGE
   (ENTITYID, SUBCATCODE, RECORDSTATUS, CREATEDBY, CREATEDDATE, 
    MODIFIEDBY, MODIFIEDDATE)
Values
   ((select entityid from entity where entitycode='WEGODH'), 'VIEW', 'A', 2, getDate(), 
    2,  getDate());

GO

Insert into ROLEPRIVILEGE
   (ROLEID, ENTITYID, SUBCATCODE, RECORDSTATUS, CREATEDBY, 
    CREATEDDATE, MODIFIEDBY, MODIFIEDDATE)
Values
   ((select roleid from role where rolecode='ADMN'), (select entityid from entity where entitycode='WEGODH'), 'VIEW', 'A', 2, 
   getDate(), 2, getDate());

GO
Insert into ROLEPRIVILEGE
   (ROLEID, ENTITYID, SUBCATCODE, RECORDSTATUS, CREATEDBY, 
    CREATEDDATE, MODIFIEDBY, MODIFIEDDATE)
Values
   ((select roleid from role where rolecode='OPM'), (select entityid from entity where entitycode='WEGODH'), 'VIEW', 'A', 2, 
   getDate(), 2, getDate());
GO
Insert into ROLEPRIVILEGE
   (ROLEID, ENTITYID, SUBCATCODE, RECORDSTATUS, CREATEDBY, 
    CREATEDDATE, MODIFIEDBY, MODIFIEDDATE)
Values
   ((select roleid from role where rolecode='OU'), (select entityid from entity where entitycode='WEGODH'), 'VIEW', 'A', 2, 
   getDate(), 2, getDate());

GO
Insert into ROLEPRIVILEGE
   (ROLEID, ENTITYID, SUBCATCODE, RECORDSTATUS, CREATEDBY, 
    CREATEDDATE, MODIFIEDBY, MODIFIEDDATE)
Values
   ((select roleid from role where rolecode='DHM'), (select entityid from entity where entitycode='WEGODH'), 'VIEW', 'A', 2, 
   getDate(), 2, getDate());





