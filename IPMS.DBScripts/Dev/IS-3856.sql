

ALTER PROCEDURE [dbo].[usp_rpt_ServiceDeliverReport]
@port  Varchar(10),
@FromDate DATETIME,
@ToDate DATETIME
WITH 
EXECUTE AS CALLER
AS
BEGIN
SET NOCOUNT ON
SELECT distinct VC.VCN ,SC.SubCatName AS MovementType,VC.MovementDateTime AS Requesttime,VC.SlotDate as scheduledtime,VC.SlotStatus,
ra.OperationType,RA.StartTime as servedtime,
(datediff(minute, RA.StartTime,  VC.MovementDateTime)) AS DELAYTIME,
(datediff(minute, RA.StartTime,  VC.SlotDate)) AS DELAYSCHED,a.DelayReason,
bt.BerthName as FromBerth,bt1.BerthName as ToBerth,
VSL.VesselName as VesselName ,SC1.SubCatName as VType,sc2.SubCatName as servicetype
FROM VesselCallMovement VC 
INNER JOIN SubCategory SC ON VC.MovementType = SC.SubCatCode

INNER JOIN ArrivalNotification AN ON VC.VCN = AN.VCN
INNER JOIN Vessel VSL ON AN.VesselID = VSL.VesselID
INNER JOIN SubCategory SC1 ON VSL.VesselType = SC1.SubCatCode

INNER JOIN ResourceAllocation RA ON RA.ServiceReferenceID = VC.ServiceRequestID 
left join Berth bt on bt.BerthCode = vc.FromPositionBerthCode and bt.QuayCode = vc.FromPositionQuayCode and bt.PortCode = vc.FromPositionPortCode
left join Berth bt1 on bt1.BerthCode = vc.ToPositionBerthCode and bt1.QuayCode = vc.ToPositionQuayCode and bt1.PortCode = vc.ToPositionPortCode
left join 
(
select sf.DelayReason,sf.ResourceAllocationID from ShiftingBerthingTaskExecution sf 
union 
select os.DelayReason,os.ResourceAllocationID from OtherServiceRecording os 
union 
select psr.DelayReason,psr.ResourceAllocationID from PilotageServiceRecording psr 
) a on ra.ResourceAllocationID = a.ResourceAllocationID
inner join Port prt on AN.PortCode = prt.PortCode
inner join SubCategory sc2 on  ra.OperationType = sc2.SubCatCode
WHERE
VC.SlotStatus ='SCHD'AND RA.ServiceReferenceType ='VTSR' and ra.RecordStatus='A' and
(PRT.PortCode = @port or @port IS NULL)AND
cast (RA.StartTime AS DATETIME) BETWEEN @FromDate AND @ToDate 
end

GO


