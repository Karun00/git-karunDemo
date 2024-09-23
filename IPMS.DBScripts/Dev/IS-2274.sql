
CREATE PROCEDURE [dbo].[usp_PlannedMovementsForAnonymous]
@portcode nvarchar(2)
WITH 
EXECUTE AS CALLER
AS
BEGIN
declare @UserType varchar(4)
begin
select VCN, VesselName, MovementType, MovementDateTime,Status,
BerthName, RegisteredName,VeselType,LOA,GRT,
  Cast(Draft as varchar)  Draft,
      ReasonforvisitName,ScheduledTime
      from (
select VCN,VesselName, MovementType, MovementDateTime,Status,
BerthName, RegisteredName,VeselType,LOA,GRT,
 case when (DraftAFT > 0 AND DraftFWD > 0 AND DraftAFT > DraftFWD) THEN
            DraftAFT 
      when (DraftAFT > 0 AND DraftFWD > 0 AND DraftFWD > DraftAFT ) THEN      
            DraftFWD 
      when (DraftAFT = 0 AND DraftFWD = 0 AND ArrDraft > DepDraft ) THEN   
            ArrDraft
      else DepDraft 
      end as Draft,
      ReasonforvisitName,ScheduledTime
from (
select AN.VCN,vc.VesselName, ms.SubCatName AS MovementType,
CASE VM.MovementType
       WHEN 'ARMV' THEN 
          VM.ETB
        -- (select ETA from VesselCall CL where cl.VCN = VM.VCN)
       WHEN 'SHMV' THEN 
           VM.ETB
       WHEN 'WRMV' THEN 
           VM.ETB
       WHEN 'SGMV' THEN 
           VM.ETB
          --(select ETUB from VesselCall CL where cl.VCN = VM.VCN)
    END MovementDateTime,
--CASE VM.MovementStatus
--       WHEN 'MPEN' THEN 
--          'In Progress'
--       WHEN 'SCH' THEN 
--          'In Progress'
--       WHEN 'CONF' THEN 
--          'Planned'
--       WHEN 'BERT' THEN
--          'Completed' 
--       WHEN 'SALD' THEN
--          'Completed'      
--    END Status,
case VM.SlotStatus
 when 'PEND' then 
       'Awaitng slot'
 when 'OVRD'then 
        'Overridden'
 when 'PLND' then
       'Planned'
 when 'CNFR' then 
       'Confirmed'
 when 'SCHD'then
       'Scheduled'
 end Status,
    br.BerthName, 
    AG.RegisteredName,
    VT.SubCatName VeselType,
    vc.LengthOverallInM LOA, vc.GrossRegisteredTonnageInMT GRT,
    ISNULL(sr.DraftAFT,0) DraftAFT, ISNULL(sr.DraftFWD,0) DraftFWD, 
    Cast(an.ArrDraft as decimal(10,2)) ArrDraft , 
    Cast(an.DepDraft as decimal(10,2)) DepDraft , 
     dbo.udf_GetArrivalReasonForVisit(an.VCN) ReasonforvisitName,
	 (select slotDate from VesselCallMovement IVCM where IVCM.VCN=VM.VCN and IVCM.VesselCallMovementID=VM.VesselCallMovementID and IVCM.RecordStatus='A')
	 as ScheduledTime

from VesselCallMovement VM
inner join ServiceRequest SR on VM.ServiceRequestID = SR.ServiceRequestID
inner join ArrivalNotification AN on VM.VCN = AN.VCN
inner join Vessel VC on AN.VesselID = VC.VesselID
inner join SubCategory MS on VM.MovementType = MS.SubCatCode
left join Berth BR on VM.FromPositionPortCode = BR.PortCode AND
                      VM.FromPositionQuayCode = BR.QuayCode AND
                      VM.FromPositionBerthCode= BR.BerthCode 
INNER JOIN Agent AG ON AN.AgentID = AG.AgentID
inner join SubCategory vt on VC.VesselType = vt.SubCatCode
where VM.FromPositionPortCode = @portcode  AND VM.MovementStatus != 'SALD' AND VM.MovementStatus != 'MPEN' 
AND AN.RecordStatus = 'A' and SR.RecordStatus = 'A' 
) t
where CONVERT (DATE, MovementDateTime) = CONVERT (DATE, GETDATE())
)k
order by MovementDateTime
end

end
GO


ALTER PROCEDURE [dbo].[usp_PlannedMovements]
@portcode nvarchar(2),
@UserId int
WITH 
EXECUTE AS CALLER
AS
BEGIN
declare @UserType varchar(4)
set @UserType=(select UserType from Users where UserID=@UserId)
if(@UserType='AGNT')
begin
select VCN, VesselName, MovementType, MovementDateTime,Status,
BerthName, RegisteredName,VeselType,LOA,GRT,
  Cast(Draft as varchar)  Draft,
      ReasonforvisitName,ScheduledTime
      from (
select VCN,VesselName, MovementType, MovementDateTime,Status,
BerthName, RegisteredName,VeselType,LOA,GRT,
 case when (DraftAFT > 0 AND DraftFWD > 0 AND DraftAFT > DraftFWD) THEN
            DraftAFT 
      when (DraftAFT > 0 AND DraftFWD > 0 AND DraftFWD > DraftAFT ) THEN      
            DraftFWD 
      when (DraftAFT = 0 AND DraftFWD = 0 AND ArrDraft > DepDraft ) THEN   
            ArrDraft
      else DepDraft 
      end as Draft,
      ReasonforvisitName,ScheduledTime
from (
select AN.VCN,vc.VesselName, ms.SubCatName AS MovementType,
CASE VM.MovementType
       WHEN 'ARMV' THEN 
          VM.ETB
        -- (select ETA from VesselCall CL where cl.VCN = VM.VCN)
       WHEN 'SHMV' THEN 
           VM.ETB
       WHEN 'WRMV' THEN 
           VM.ETB
       WHEN 'SGMV' THEN 
           VM.ETB
          --(select ETUB from VesselCall CL where cl.VCN = VM.VCN)
    END MovementDateTime,
--CASE VM.MovementStatus
--       WHEN 'MPEN' THEN 
--          'In Progress'
--       WHEN 'SCH' THEN 
--          'In Progress'
--       WHEN 'CONF' THEN 
--          'Planned'
--       WHEN 'BERT' THEN
--          'Completed' 
--       WHEN 'SALD' THEN
--          'Completed'      
--    END Status,
case VM.SlotStatus
 when 'PEND' then 
       'Awaitng slot'
 when 'OVRD'then 
        'Overridden'
 when 'PLND' then
       'Planned'
 when 'CNFR' then 
       'Confirmed'
 when 'SCHD'then
       'Scheduled'
 end Status,
    br.BerthName, 
    AG.RegisteredName,
    VT.SubCatName VeselType,
    vc.LengthOverallInM LOA, vc.GrossRegisteredTonnageInMT GRT,
    ISNULL(sr.DraftAFT,0) DraftAFT, ISNULL(sr.DraftFWD,0) DraftFWD, 
    Cast(an.ArrDraft as decimal(10,2)) ArrDraft , 
    Cast(an.DepDraft as decimal(10,2)) DepDraft , 
     dbo.udf_GetArrivalReasonForVisit(an.VCN) ReasonforvisitName,
	 (select slotDate from VesselCallMovement IVCM where IVCM.VCN=VM.VCN and IVCM.VesselCallMovementID=VM.VesselCallMovementID and IVCM.RecordStatus='A')
	 as ScheduledTime

from VesselCallMovement VM
inner join ServiceRequest SR on VM.ServiceRequestID = SR.ServiceRequestID
inner join ArrivalNotification AN on VM.VCN = AN.VCN
inner join Vessel VC on AN.VesselID = VC.VesselID
inner join SubCategory MS on VM.MovementType = MS.SubCatCode
left join Berth BR on VM.FromPositionPortCode = BR.PortCode AND
                      VM.FromPositionQuayCode = BR.QuayCode AND
                      VM.FromPositionBerthCode= BR.BerthCode 
INNER JOIN Agent AG ON AN.AgentID = AG.AgentID
inner join SubCategory vt on VC.VesselType = vt.SubCatCode
where VM.FromPositionPortCode = @portcode  AND VM.MovementStatus != 'SALD' AND VM.MovementStatus != 'MPEN' 
AND AN.RecordStatus = 'A' and SR.RecordStatus = 'A' and SR.CreatedBy=@userid
) t
where CONVERT (DATE, MovementDateTime) = CONVERT (DATE, GETDATE())
)k
order by MovementDateTime
end
else
begin
select VCN, VesselName, MovementType, MovementDateTime,Status,
BerthName, RegisteredName,VeselType,LOA,GRT,
  Cast(Draft as varchar)  Draft,
      ReasonforvisitName,ScheduledTime
      from (
select VCN,VesselName, MovementType, MovementDateTime,Status,
BerthName, RegisteredName,VeselType,LOA,GRT,
 case when (DraftAFT > 0 AND DraftFWD > 0 AND DraftAFT > DraftFWD) THEN
            DraftAFT 
      when (DraftAFT > 0 AND DraftFWD > 0 AND DraftFWD > DraftAFT ) THEN      
            DraftFWD 
      when (DraftAFT = 0 AND DraftFWD = 0 AND ArrDraft > DepDraft ) THEN   
            ArrDraft
      else DepDraft 
      end as Draft,
      ReasonforvisitName,ScheduledTime
from (
select AN.VCN,vc.VesselName, ms.SubCatName AS MovementType,
CASE VM.MovementType
       WHEN 'ARMV' THEN 
          VM.ETB
        -- (select ETA from VesselCall CL where cl.VCN = VM.VCN)
       WHEN 'SHMV' THEN 
           VM.ETB
       WHEN 'WRMV' THEN 
           VM.ETB
       WHEN 'SGMV' THEN 
           VM.ETB
          --(select ETUB from VesselCall CL where cl.VCN = VM.VCN)
    END MovementDateTime,
--CASE VM.MovementStatus
--       WHEN 'MPEN' THEN 
--          'In Progress'
--       WHEN 'SCH' THEN 
--          'In Progress'
--       WHEN 'CONF' THEN 
--          'Planned'
--       WHEN 'BERT' THEN
--          'Completed' 
--       WHEN 'SALD' THEN
--          'Completed'      
--    END Status,
case VM.SlotStatus
 when 'PEND' then 
       'Awaitng slot'
 when 'OVRD'then 
        'Overridden'
 when 'PLND' then
       'Planned'
 when 'CNFR' then 
       'Confirmed'
 when 'SCHD'then
       'Scheduled'
 end Status,
    br.BerthName, 
    AG.RegisteredName,
    VT.SubCatName VeselType,
    vc.LengthOverallInM LOA, vc.GrossRegisteredTonnageInMT GRT,
    ISNULL(sr.DraftAFT,0) DraftAFT, ISNULL(sr.DraftFWD,0) DraftFWD, 
    Cast(an.ArrDraft as decimal(10,2)) ArrDraft , 
    Cast(an.DepDraft as decimal(10,2)) DepDraft , 
     dbo.udf_GetArrivalReasonForVisit(an.VCN) ReasonforvisitName,
	 (select slotDate from VesselCallMovement IVCM where IVCM.VCN=VM.VCN and IVCM.VesselCallMovementID=VM.VesselCallMovementID and IVCM.RecordStatus='A')
	 as ScheduledTime

from VesselCallMovement VM
inner join ServiceRequest SR on VM.ServiceRequestID = SR.ServiceRequestID
inner join ArrivalNotification AN on VM.VCN = AN.VCN
inner join Vessel VC on AN.VesselID = VC.VesselID
inner join SubCategory MS on VM.MovementType = MS.SubCatCode
left join Berth BR on VM.FromPositionPortCode = BR.PortCode AND
                      VM.FromPositionQuayCode = BR.QuayCode AND
                      VM.FromPositionBerthCode= BR.BerthCode 
INNER JOIN Agent AG ON AN.AgentID = AG.AgentID
inner join SubCategory vt on VC.VesselType = vt.SubCatCode
where VM.FromPositionPortCode = @portcode  AND VM.MovementStatus != 'SALD' AND VM.MovementStatus != 'MPEN' 
AND AN.RecordStatus = 'A' and SR.RecordStatus = 'A' 
) t
where CONVERT (DATE, MovementDateTime) = CONVERT (DATE, GETDATE())
)k
order by MovementDateTime
end

End
GO

