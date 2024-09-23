
-- Old Procedure usp_rpt_anchoragereport
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_rpt_anchoragereport]') AND type in (N'P'))
DROP PROCEDURE [dbo].[usp_rpt_anchoragereport]
GO
CREATE PROCEDURE [dbo].[usp_rpt_anchoragereport]
@VCN Varchar(50),
@Port Varchar(50),
@FromDate DATETIME,
@ToDate DATETIME
WITH 
EXECUTE AS CALLER
AS
BEGIN
SET NOCOUNT ON

select distinct AnchorDropTime,AnchorAweighTime,BreakWaterIn,Reason,VesselName,ATA,CargoType,VesselType,VCN,ReasonForVisit,PortCode,PortName,
ISNULL((FORMAT(DateDiff(s,AnchorDropTime, getDate())/3600,N'00')+':'+ FORMAT(ABS(DateDiff(s, AnchorDropTime, getDate())%3600/60), N'00')), '-') AS times
from 
(
SELECT distinct VCA.AnchorDropTime,
       VCA.AnchorAweighTime,
       VC.BreakWaterIn,
       sc4.SubCatName AS Reason,
       VL.VesselName,
       VC.ATA,
       sc3.SubCatName AS cargotype,
       sc2.SubCatName AS vesseltype,
       AN.VCN,
       dbo.udf_GetArrivalReasonForVisit (AN.VCN) AS ReasonForVisit,
       AN.PortCode,
       PM.PortName      
  FROM ArrivalNotification AN
       INNER JOIN VesselCall VC ON VC.VCN = AN.VCN       
       INNER JOIN
        (
            SELECT  VCN,
                    MAX(VesselCallAnchorageID) VesselCallAnchorageID
            FROM    VesselCallAnchorage where RecordStatus = 'A'
            GROUP BY VCN
        ) vesselCallAnchorages ON vc.VCN = vesselCallAnchorages.VCN 
        INNER JOIN  VesselCallAnchorage VCA ON vesselCallAnchorages.VCN = VCA.VCN AND vesselCallAnchorages.VesselCallAnchorageID = VCA.VesselCallAnchorageID        
       INNER JOIN SubCategory sc4 ON VCA.Reason = sc4.SubCatCode
       INNER JOIN Vessel VL ON VL.VesselID = AN.VesselID
       INNER JOIN SubCategory AS SC2 ON VL.VesselType = SC2.SubCatCode
       INNER JOIN PORT PM ON AN.PortCode = PM.PortCode
       LEFT JOIN ArrivalCommodity AC ON AC.VCN = AN.VCN
       LEFT JOIN SubCategory AS SC3 ON SC3.SubCatCode = AC.CargoType
WHERE    (AN.VCN = @VCN OR @VCN IS NULL) and VCA.RecordStatus != 'I'
       AND (PM.PortCode = @Port OR @Port IS NULL)
       AND VC.ATA IS NOT NULL AND VC.ATD IS NULL AND VCA.AnchorAweighTime IS NULL
       AND cast (VCA.AnchorDropTime AS DATETIME) <= @FromDate 
      
      ) as b

END

GO



-- New Procedure usp_rpt_anchoragereport
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_rpt_anchoragereport]') AND type in (N'P'))
DROP PROCEDURE [dbo].[usp_rpt_anchoragereport]
GO
CREATE PROCEDURE [dbo].[usp_rpt_anchoragereport]
@VCN varchar(50), @Port varchar(50), @FromDate datetime, @ToDate datetime
WITH EXEC AS CALLER
AS
BEGIN
SET NOCOUNT ON


select distinct AnchorDropTime,AnchorAweighTime,BreakWaterIn,Reason,VesselName,ATA,cargotype,vesseltype,VCN,ReasonForVisit,PortCode,PortName,
(datediff (Hour, AnchorDropTime, AnchorAweighTime)) AS times  from 
(

SELECT distinct VCA.AnchorDropTime,
       VCA.AnchorAweighTime,
       VC.BreakWaterIn,
       sc4.SubCatName AS Reason,
       VL.VesselName,
       VC.ATA,
       sc3.SubCatName AS cargotype,
       sc2.SubCatName AS vesseltype,
       AN.VCN,
       dbo.udf_GetArrivalReasonForVisit (AN.VCN) AS ReasonForVisit,
       AN.PortCode,
       PM.PortName      
  FROM ArrivalNotification AN
       INNER JOIN VesselCall VC ON VC.VCN = AN.VCN
       INNER JOIN VesselCallAnchorage VCA ON VCA.VCN = AN.VCN
       INNER JOIN SubCategory sc4 ON VCA.Reason = sc4.SubCatCode
       INNER JOIN Vessel VL ON VL.VesselID = AN.VesselID
       INNER JOIN SubCategory AS SC2 ON VL.VesselType = SC2.SubCatCode
       INNER JOIN PORT PM ON AN.PortCode = PM.PortCode
       LEFT JOIN ArrivalCommodity AC ON AC.VCN = AN.VCN
       LEFT JOIN SubCategory AS SC3 ON SC3.SubCatCode = AC.CargoType
WHERE     (AN.VCN = @VCN OR @VCN IS NULL)
       AND (PM.PortCode = @Port OR @Port IS NULL)
       AND VC.ATA IS NOT NULL
       AND 
       VCA.AnchorAweighTime BETWEEN @FromDate AND @ToDate  
	   union all
     
	   SELECT distinct VCA.AnchorDropTime,
       VCA.AnchorAweighTime,
       VC.BreakWaterIn,
       sc4.SubCatName AS Reason,
       VL.VesselName,
       VC.ATA,
       sc3.SubCatName AS cargotype,
       sc2.SubCatName AS vesseltype,
       AN.VCN,
       dbo.udf_GetArrivalReasonForVisit (AN.VCN) AS ReasonForVisit,
       AN.PortCode,
       PM.PortName      
  FROM ArrivalNotification AN
       INNER JOIN VesselCall VC ON VC.VCN = AN.VCN
       INNER JOIN VesselCallAnchorage VCA ON VCA.VCN = AN.VCN
       INNER JOIN SubCategory sc4 ON VCA.Reason = sc4.SubCatCode
       INNER JOIN Vessel VL ON VL.VesselID = AN.VesselID
       INNER JOIN SubCategory AS SC2 ON VL.VesselType = SC2.SubCatCode
       INNER JOIN PORT PM ON AN.PortCode = PM.PortCode
       LEFT JOIN ArrivalCommodity AC ON AC.VCN = AN.VCN
       LEFT JOIN SubCategory AS SC3 ON SC3.SubCatCode = AC.CargoType
WHERE    (AN.VCN = @VCN OR @VCN IS NULL)
       AND (PM.PortCode = @Port OR @Port IS NULL)
      AND VC.ATA IS NOT NULL
       AND 
       VCA.AnchorDropTime BETWEEN @FromDate AND @ToDate  
	   ) as b
END
GO





--- Procedure for Anchorage Waiting Report
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_rpt_anchoredreport]') AND type in (N'P'))
DROP PROCEDURE [dbo].[usp_rpt_anchoredreport]
GO
CREATE PROCEDURE [dbo].[usp_rpt_anchoredreport]
@VCN Varchar(50),
@Port Varchar(50),
@FromDate DATETIME,
@ToDate DATETIME
WITH 
EXECUTE AS CALLER
AS
BEGIN
SET NOCOUNT ON

select distinct AnchorDropTime,AnchorAweighTime,BreakWaterIn,Reason,VesselName,ATA,CargoType,VesselType,VCN,ReasonForVisit,PortCode,PortName,
ISNULL((FORMAT(DateDiff(s,AnchorDropTime, getDate())/3600,N'00')+':'+ FORMAT(ABS(DateDiff(s, AnchorDropTime, getDate())%3600/60), N'00')), '-') AS times
from 
(
SELECT distinct VCA.AnchorDropTime,
       VCA.AnchorAweighTime,
       VC.BreakWaterIn,
       sc4.SubCatName AS Reason,
       VL.VesselName,
       VC.ATA,
       sc3.SubCatName AS cargotype,
       sc2.SubCatName AS vesseltype,
       AN.VCN,
       dbo.udf_GetArrivalReasonForVisit (AN.VCN) AS ReasonForVisit,
       AN.PortCode,
       PM.PortName      
  FROM ArrivalNotification AN
       INNER JOIN VesselCall VC ON VC.VCN = AN.VCN       
       INNER JOIN
        (
            SELECT  VCN,
                    MAX(VesselCallAnchorageID) VesselCallAnchorageID
            FROM    VesselCallAnchorage where RecordStatus = 'A'
            GROUP BY VCN
        ) vesselCallAnchorages ON vc.VCN = vesselCallAnchorages.VCN 
        INNER JOIN  VesselCallAnchorage VCA ON vesselCallAnchorages.VCN = VCA.VCN AND vesselCallAnchorages.VesselCallAnchorageID = VCA.VesselCallAnchorageID        
       INNER JOIN SubCategory sc4 ON VCA.Reason = sc4.SubCatCode
       INNER JOIN Vessel VL ON VL.VesselID = AN.VesselID
       INNER JOIN SubCategory AS SC2 ON VL.VesselType = SC2.SubCatCode
       INNER JOIN PORT PM ON AN.PortCode = PM.PortCode
       LEFT JOIN ArrivalCommodity AC ON AC.VCN = AN.VCN
       LEFT JOIN SubCategory AS SC3 ON SC3.SubCatCode = AC.CargoType
WHERE    (AN.VCN = @VCN OR @VCN IS NULL) and VCA.RecordStatus != 'I'
       AND (PM.PortCode = @Port OR @Port IS NULL)
       AND VC.ATA IS NOT NULL AND VC.ATD IS NULL AND VCA.AnchorAweighTime IS NULL
       AND cast (VCA.AnchorDropTime AS DATETIME) <= @FromDate 
      
      ) as b

END

GO
