ALTER PROCEDURE [dbo].[usp_rpt_IMDGReport]

@Port VARCHAR(50),

@VCN  VARCHAR(50),

@FromDate DATETIME,

@ToDate DATETIME,

@Classification varchar(50)= NULL,

@Commodity VARCHAR(50)

WITH 

EXECUTE AS CALLER

AS

BEGIN

SET NOCOUNT ON

SELECT  
AN.VCN,(select VM.VesselName from vessel VM where VM.VesselID=AN.VesselID or VM.VesselID is null and AN.VesselID is null) as VesselName,
(select VM.CallSign from vessel VM where VM.VesselID=AN.VesselID or VM.VesselID is null and AN.VesselID is null) as CallSign, 
AN.ISPSReferenceNo,
(select pr.PortName from PortRegistry pr where AN.LastPortOfCall = pr.PortCode or AN.LastPortOfCall is null and  pr.PortCode is null)as LastPortOfCall,
(select pr.PortName from PortRegistry pr where AN.NextPortOfCall = pr.PortCode or AN.NextPortOfCall is null and  pr.PortCode is null)as NextPortOfCall,
AN.ETA, AN.ETD,AN.ReasonForVisit,
(select s.SubCatName from SubCategory s,Vessel v where s.SubCatCode=v.VesselType and v.VesselID=An.VesselID) AS VesselType,
AN.PortCode, 
(select PM.PortName from port pm where AN.PortCode = PM.PortCode or AN.PortCode is null and PM.PortCode is null)as PortName,
AN.RecordStatus, 
(select UM.UserName from Users UM where UM.UserID = AN.CreatedBy or  UM.UserID is null and  AN.CreatedBy is null)as UserName,
(select A.AgentID from Agent AS A where A.AgentID = AN.AgentID or A.AgentID is null and  AN.AgentID is null)as AgentID,
(select A.RegisteredName from Agent AS A where A.AgentID = AN.AgentID or A.AgentID is null and  AN.AgentID is null)as RegisteredName,
CASE WHEN (an.AnyDangerousGoodsonBoard = 'I') THEN 'N' ELSE 'Y' END AS IMDG,
(select tpr.TradingName from  TerminalOperator AS TPR where  TPR.TerminalOperatorID = AN.TerminalOperatorID or TPR.TerminalOperatorID is null and  AN.TerminalOperatorID is null)as TradingName,
(select VM.IMONo from vessel VM where VM.VesselID=AN.VesselID or VM.VesselID is null and AN.VesselID is null) as IMONo,
(select VM.VesselID from vessel VM where VM.VesselID=AN.VesselID or VM.VesselID is null and AN.VesselID is null) as VesselID,
(select sub.SubCatName from SubCategory sub where AN.ReasonForVisit = sub.SubCatCode or AN.ReasonForVisit is null and sub.SubCatCode is null)AS REASONVisit,
(select sub.SubCatName from SubCategory sub where IMG.purpose=Sub.SubCatCode or IMG.purpose is null and Sub.SubCatCode is null)AS Purpose,
IMG.UNNo,
(select sub.SubCatName from SubCategory sub where IMG.CargoCode = sub.SubCatCode or IMG.CargoCode is null and sub.SubCatCode is null) as CARGO,
(select sub.SubCatName from SubCategory sub where IMG.ClassCode = Sub.SubCatCode or IMG.ClassCode is null and Sub.SubCatCode is null ) as ClassCode,
IMG.Quantity,
IMG.NoofContainer,
IMG.Others,
AN.CargoDescription,
BT.BerthName,
an.DischargeDate,
----AC.Quantity,
cast(datename(week,CONVERT (DATE,an.eta)) as int)- cast( datename(week,dateadd(dd,1-day(CONVERT (DATE,an.eta)),CONVERT (DATE,an.eta))) as int)+1 AS MonthWeeks,
 CONVERT (DATE,an.ETA) as months


FROM ArrivalNotification AS AN 
inner join IMDGInformation IMG ON IMG.VCN = AN.VCN
inner join SubCategory sub on IMG.CargoCode=sub.SubCatCode
--left JOIN Vessel AS VT on VT.VesselType=sub.SubCatCode 
INNER JOIN Berth AS BT ON AN.PortCode = BT.PortCode AND AN.PreferredQuayCode = BT.QuayCode AND AN.PreferredBerthCode = BT.BerthCode

WHERE      
(An.PortCode = @Port OR @Port IS NULL) AND
(AN.VCN = @VCN OR @VCN IS NULL) AND
cast (AN.ETA AS DATE) BETWEEN @FromDate AND @ToDate AND
(Sub.SubCatCode = @Classification or @Classification IS NULL)AND
(sub.SubCatCode = @Commodity OR @Commodity IS NULL)
AND an.IsIMDGANFinal ='Y'

 END