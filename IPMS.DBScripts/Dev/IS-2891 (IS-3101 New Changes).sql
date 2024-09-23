
ALTER FUNCTION [dbo].[udf_Wego_VesselCount] (@CargoType VARCHAR(4), @fromDate DATETIME, @toDate DATETIME,
@portCode  VARCHAR(4)) 
RETURNS INT
AS

BEGIN
DECLARE @Value INT
     
IF(@CargoType = 'BULK')

     BEGIN
        SELECT @Value = Count(distinct ar.VCN) from 
         ArrivalNotification ar         
         INNER JOIN StatementFact st on st.VCN = ar.VCN
         INNER JOIN StatementCommodity sc ON st.StatementFactID = sc.StatementFactID
         join VesselCall vc on ar.VCN = vc.VCN
         where sc.CargoType IN ('CTY8', 'CTY6', 'CTY5', 'CTY9', 'CTY7') 
         AND ar.RecordStatus = 'A' AND vc.ATD IS NOT NULL
          AND (sc.PortCode = @portCode OR @portCode IS NULL)         
         AND  CAST (vc.BreakWaterOut AS DATETIME) BETWEEN @fromDate AND @toDate          
      END
  ELSE      
        SELECT @Value = Count(distinct ar.VCN) from 
         ArrivalNotification ar         
         INNER JOIN StatementFact st on st.VCN = ar.VCN
         INNER JOIN StatementCommodity sc ON st.StatementFactID = sc.StatementFactID
         join VesselCall vc on ar.VCN = vc.VCN
         where sc.CargoType = @CargoType 
         AND ar.RecordStatus = 'A' AND vc.ATD IS NOT NULL
         AND (sc.PortCode = @portCode OR @portCode IS NULL)   
         AND CAST (vc.BreakWaterOut AS DATETIME) BETWEEN @fromDate AND @toDate
       
RETURN @Value

END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetWegoVesselCargoType]') AND type in (N'P'))
DROP PROCEDURE [dbo].[usp_GetWegoVesselCargoType]
GO
CREATE PROCEDURE [dbo].[usp_GetWegoVesselCargoType]  
    @fromDate DATETIME, @toDate DATETIME, @portCode VARCHAR (50)
   WITH
   EXECUTE AS CALLER
AS
   BEGIN    
   
   SELECT
   [dbo].[udf_Wego_VesselCount]('CTY2', @fromDate, @toDate, @portCode) VesselAutomative,
   [dbo].[udf_Wego_VesselCount]('CTY3', @fromDate, @toDate, @portCode) VesselBreakBulk,
   [dbo].[udf_Wego_VesselCount]('CTY1', @fromDate, @toDate, @portCode) VesselContainer,
   [dbo].[udf_Wego_VesselCount]('BULK', @fromDate, @toDate, @portCode) VesselBulk,
   [dbo].[udf_Wego_VesselCount]('CTY4', @fromDate, @toDate, @portCode) VesselLiquidBulk,
   [dbo].[udf_Wego_VesselCount]('CTNC', @fromDate, @toDate, @portCode) VesselNonOperational,
   [dbo].[udf_Wego_VesselCount]('CTBK', @fromDate, @toDate, @portCode) VesselBunkers,
   [dbo].[udf_Wego_VesselCount]('DG', @fromDate, @toDate, @portCode) VesselPassengers
   
   
   END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetWegoDashBoardDetails]') AND type in (N'P'))
DROP PROCEDURE [dbo].[usp_GetWegoDashBoardDetails]
GO
CREATE PROCEDURE [dbo].[usp_GetWegoDashBoardDetails]
  @FromDate DATETIME, @ToDate DATETIME, @PortCode VARCHAR (50)
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
,SUM(MarineServiceTimeOut) MarineServiceTimeOut
,SUM(Quantity) Volumes
,COALESCE(CASE WHEN SUM(StartEndCargo) = 0 THEN 1  ELSE SUM(StartEndCargo) END, 1) StartEndCargo
,COALESCE(CASE WHEN SUM(LastLineOffFirstLineIn) = 0 THEN 1  ELSE SUM(LastLineOffFirstLineIn) END, 1) LastLineOffFirstLineIn
,SUM(PreCargoWorking) PreCargoWorking
,SUM(WorkingTime) WorkingTime
,SUM(DepartureWaiting) DepartureWaiting
from
(SELECT wego.VCN
,v.GrossRegisteredTonnageInMT GRT
,v.LengthOverallInM LOA
,wego.SubCatName CargoType
,wego.CargoCode CargoCode
,wego.Quantity
,ISNULL(CAST((datediff(mi,VC.BreakWaterIn,VC.BreakWaterOut)/ 60.0)as numeric(7,2)), '0.0') STAT
,ISNULL(CAST((datediff(mi,VC.PortLimitIn,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'PB'))/ 60.0)as numeric(7,2)), '0.0') VesselDelayAnchorage
,ISNULL(CAST((datediff(mi,VC.BreakWaterIn,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'LI'))/ 60.0)as numeric(7,2)), '0.0') NPAManueringTime
,ISNULL(CAST((datediff(mi,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'PB'),VC.BreakWaterIn)/ 60.0)as numeric(7,2)), '0.0') PilotageIn
,ISNULL(CAST((datediff(mi,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'SGMV', 'PB'),VC.BreakWaterOut)/ 60.0)as numeric(7,2)), '0.0') MarineServiceTimeOut
,ISNULL(CAST((datediff(mi,sf.StartCargo, sf.EndCargo)/ 60.0)as numeric(10,2)), '0.0') StartEndCargo
,ISNULL(CAST((datediff(mi,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'FI'),[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'SGMV', 'LO'))/ 60.0)as numeric(10,2)), '0.0') LastLineOffFirstLineIn
,ISNULL(CAST((datediff(mi, [dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'LI'), SF.StartCargo)/ 60.0)as numeric(10,2)), '0.0') PreCargoWorking
,ISNULL(CAST((datediff(mi,sf.StartCargo, sf.EndCargo)/ 60.0)as numeric(10,2)), '0.0') WorkingTime
,ISNULL(CAST((datediff(mi,SF.EndCargo, [dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'SGMV', 'FO'))/ 60.0)as numeric(10,2)), '0.0') DepartureWaiting
,VC.BreakWaterIn
,VC.BreakWaterOut
,VC.PortLimitIn
,VC.PortLimitOut
,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'LI') LastLineIn 
,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'PB') PilotOnBoard 
,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'SGMV', 'PB') SPilotOnBoard   
 from
(SELECT DISTINCT ar.VCN
,sb.SubCatName, sb.SubCatCode CargoCode , sc.Quantity, sc.QuayCode, sc.BerthCode
FROM ArrivalNotification ar
JOIN Vessel v on v.VesselID = ar.VesselID
INNER JOIN StatementFact st on st.VCN = ar.VCN
INNER JOIN StatementCommodity sc ON st.StatementFactID = sc.StatementFactID
and sc.PortCode = @PortCode 
JOIN SubCategory  sb ON sb.SubCatCode = sc.CargoType
JOIN VesselCall VCL on VCL.VCN = ar.VCN AND  CAST(VCL.BreakWaterOut AS DATETIME) BETWEEN @FromDate AND @ToDate
AND VCL.RecordStatus = 'A'
GROUP BY ar.VCN, sb.SubCatName, sb.SubCatCode, sc.Quantity, sc.QuayCode, sc.BerthCode
HAVING  sb.SubCatCode IN ('CTY2','CTY3','CTY1', 'CTY4','CTNC', 'CTBK', 'DG')) wego
INNER JOIN ArrivalNotification an on an.VCN = wego.VCN      
INNER JOIN VesselCall VC on VC.VCN = an.VCN
INNER JOIN Vessel V on V.VesselID = an.VesselID
INNER JOIN StatementFact sf on sf.VCN = an.VCN
WHERE VC.ATD IS NOT NULL AND an.RecordStatus = 'A' 
AND  CAST (VC.BreakWaterOut AS DATETIME) BETWEEN @FromDate AND @ToDate
AND (an.PortCode = @PortCode OR @PortCode IS NULL) 
) s
GROUP BY s.CargoType, s.CargoCode

UNION 
  SELECT 
 'Bulk' CargoType,
'BULK' CargoCode,
ISNULL(SUM(s1.GRT), 0) GRT
,ISNULL(SUM(LOA), 0) LOA
,ISNULL(SUM(STAT), 0) STAT
,ISNULL(SUM(VesselDelayAnchorage), 0) VesselDelayAnchorage
,ISNULL(SUM(NPAManueringTime), 0) NPAManueringTime
,ISNULL(SUM(PilotageIn), 0) PilotageIn
,ISNULL(SUM(NPAManueringTime), 0) + ISNULL(SUM(PilotageIn), 0) MarineServiceTimeIn
,ISNULL(SUM(MarineServiceTimeOut), 0) MarineServiceTimeOut
,ISNULL(SUM(Quantity), 0) Volumes
,COALESCE(CASE WHEN SUM(StartEndCargo) = 0 THEN 1  ELSE SUM(StartEndCargo) END, 1) StartEndCargo
,COALESCE(CASE WHEN SUM(LastLineOffFirstLineIn) = 0 THEN 1  ELSE SUM(LastLineOffFirstLineIn) END, 1) LastLineOffFirstLineIn 
,ISNULL(SUM(PreCargoWorking), 0) PreCargoWorking
,ISNULL(SUM(WorkingTime), 0) WorkingTime
,ISNULL(SUM(DepartureWaiting), 0) DepartureWaiting
from
(SELECT wego.VCN
,v.GrossRegisteredTonnageInMT GRT
,v.LengthOverallInM LOA
,wego.SubCatName CargoType
,wego.CargoCode CargoCode
,wego.Quantity
,ISNULL(CAST((datediff(mi,VC.BreakWaterIn,VC.BreakWaterOut)/ 60.0)as numeric(7,2)), '0.0') STAT
,ISNULL(CAST((datediff(mi,VC.PortLimitIn,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'PB'))/ 60.0)as numeric(7,2)), '0.0') VesselDelayAnchorage
,ISNULL(CAST((datediff(mi,VC.BreakWaterIn,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'LI'))/ 60.0)as numeric(7,2)), '0.0') NPAManueringTime
,ISNULL(CAST((datediff(mi,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'PB'),VC.BreakWaterIn)/ 60.0)as numeric(7,2)), '0.0') PilotageIn
,ISNULL(CAST((datediff(mi,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'SGMV', 'PB'),VC.BreakWaterOut)/ 60.0)as numeric(7,2)), '0.0') MarineServiceTimeOut

,ISNULL(CAST((datediff(mi,sf.StartCargo, sf.EndCargo)/ 60.0)as numeric(10,2)), '0.0') StartEndCargo
,ISNULL(CAST((datediff(mi,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'FI'),[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'SGMV', 'LO'))/ 60.0)as numeric(10,2)), '0.0') LastLineOffFirstLineIn
,ISNULL(CAST((datediff(mi, [dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'LI'), SF.StartCargo)/ 60.0)as numeric(10,2)), '0.0') PreCargoWorking
,ISNULL(CAST((datediff(mi,sf.StartCargo, sf.EndCargo)/ 60.0)as numeric(10,2)), '0.0') WorkingTime
,ISNULL(CAST((datediff(mi,SF.EndCargo, [dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'SGMV', 'FO'))/ 60.0)as numeric(10,2)), '0.0') DepartureWaiting
,VC.BreakWaterIn
,VC.BreakWaterOut
,VC.PortLimitIn
,VC.PortLimitOut
,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'LI') LastLineIn 
,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'PB') PilotOnBoard 
,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'SGMV', 'PB') SPilotOnBoard   
 from
(SELECT DISTINCT ar.VCN
,sb.SubCatName, sb.SubCatCode CargoCode, sc.Quantity, sc.QuayCode, sc.BerthCode
FROM ArrivalNotification ar
JOIN Vessel v on v.VesselID = ar.VesselID
INNER JOIN StatementFact st on st.VCN = ar.VCN
INNER JOIN StatementCommodity sc ON st.StatementFactID = sc.StatementFactID
JOIN SubCategory  sb ON sb.SubCatCode = sc.CargoType
JOIN VesselCall VCL on VCL.VCN = ar.VCN 
where  sb.SubCatCode IN ('CTY8','CTY6','CTY5', 'CTY9','CTY7')
and sc.PortCode = @PortCode 
AND  CAST(VCL.BreakWaterOut AS DATETIME) BETWEEN @FromDate AND @ToDate
AND VCL.RecordStatus = 'A'
) wego
INNER JOIN ArrivalNotification an on an.VCN = wego.VCN      
INNER JOIN VesselCall VC on VC.VCN = an.VCN
INNER JOIN Vessel V on V.VesselID = an.VesselID
INNER JOIN StatementFact sf on sf.VCN = an.VCN
where VC.ATD IS NOT NULL AND an.RecordStatus = 'A' AND VC.RecordStatus = 'A'
AND  CAST (VC.BreakWaterOut AS DATETIME) BETWEEN @FromDate AND @ToDate
AND (an.PortCode = @PortCode OR @PortCode IS NULL)
) s1

  END
GO

