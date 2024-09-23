
ALTER PROCEDURE [dbo].[usp_rpt_SAFREPreport]
@port  Varchar(50),
@FromDate DATETIME,
@ToDate DATETIME
WITH 
EXECUTE AS CALLER
AS
BEGIN
 SET NOCOUNT ON

select 
VC.VCN,
AN.AgentID,
pr.PortName as LastPortOfCall,
pr1.PortName as NextPortOfCall,
VC.BreakWaterIn,
VC.BreakWaterOut,
VC.PortLimitIn,
VC.PortLimitOut,
VSL.VesselName,
SC.SubCatName AS VSLTYP,
pr1.PortName as PortOfRegistry,
VSL.CallSign,
VSL.GrossRegisteredTonnageInMT,
AG.RegisteredName,
an.PortCode,
PRT.PortName,
VC.ATA,
vsl.IMONo,
SC1.SubCatName AS Mtype
from  VesselCall VC  INNER JOIN
ArrivalNotification AN ON VC.VCN = AN.VCN 
INNER JOIN VesselCallMovement VCM ON VCM.VCN = AN.VCN
INNER JOIN Vessel VSL ON AN.VesselID = VSL.VesselID 
INNER JOIN Agent AG ON VC.RecentAgentID = AG.AgentID  
INNER JOIN SubCategory SC ON VSL.VesselType = SC.SubCatCode  
INNER JOIN port prt  on AN.PortCode = prt.PortCode 
inner join users  us on us.CreatedBy = us.UserID
INNER join PortRegistry pr on AN.LastPortOfCall = pr.PortCode 
INNER join PortRegistry pr1 on AN.NextPortOfCall = pr1.PortCode
INNER join PortRegistry pr2 on VSL.PortOfRegistry = pr2.PortCode
INNER JOIN SubCategory SC1 ON VCM.MovementType = SC1.SubCatCode

WHERE
(PRT.PortCode = @port or @port IS NULL) AND
VCM.MovementType IN ('ARMV','SGMV') AND
 (CASE WHEN VCM.MovementType = 'ARMV' THEN cast (VC.BreakWaterIn AS DATETIME) WHEN  VCM.MovementType = 'SGMV' THEN cast (VC.BreakWaterOut AS DATETIME) END)
BETWEEN @FromDate AND @ToDate
--cast (VC.BreakWaterIn AS DATETIME) BETWEEN @FromDate AND @ToDate 
END
GO
