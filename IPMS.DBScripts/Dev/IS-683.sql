INSERT INTO [dbo].[Entity](EntityCode,Moduleid,Entityname,PageUrl,OrderNo,Tokens,HasWorkflow,HasMenuItem,RecordStatus,CreatedBy,
  CreatedDate,ModifiedBy,ModifiedDate,PendingTaskColumns,ControllerName) 
 VALUES('WEGO',(select ModuleID from [dbo].[Module] where ModuleName='Port Performance'),'Wego Detailed Report','Report/WegoDetailedReport',8,null,'N','Y','A',2,getdate(),2,getdate(),null,null) 
  GO
  insert into [dbo].[EntityPrivilege](EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
  values((select EntityID from [dbo].[Entity] where EntityCode='WEGO'),'ADD','A',1,getdate(),1,getdate())

  insert into [dbo].[EntityPrivilege](EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
  values((select EntityID from [dbo].[Entity] where EntityCode='WEGO'),'DEL','A',1,getdate(),1,getdate())

  insert into [dbo].[EntityPrivilege](EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
  values((select EntityID from [dbo].[Entity] where EntityCode='WEGO'),'EDIT','A',1,getdate(),1,getdate())

  insert into [dbo].[EntityPrivilege](EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
  values((select EntityID from [dbo].[Entity] where EntityCode='WEGO'),'VERF','A',1,getdate(),1,getdate())

  insert into [dbo].[EntityPrivilege](EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
  values((select EntityID from [dbo].[Entity] where EntityCode='WEGO'),'VIEW','A',1,getdate(),1,getdate())
  GO
  insert into [dbo].[RolePrivilege](RoleID,EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
  values((select RoleId from [dbo].[Role] where RoleName='Admin'),(select EntityID from [dbo].[Entity] where EntityCode='WEGO'),'ADD','A',2,getdate(),2,getdate())
  insert into [dbo].[RolePrivilege](RoleID,EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
  values((select RoleId from [dbo].[Role] where RoleName='Admin'),(select EntityID from [dbo].[Entity] where EntityCode='WEGO'),'EDIT','A',2,getdate(),2,getdate())
  insert into [dbo].[RolePrivilege](RoleID,EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
  values((select RoleId from [dbo].[Role] where RoleName='Admin'),(select EntityID from [dbo].[Entity] where EntityCode='WEGO'),'VIEW','A',2,getdate(),2,getdate())

GO


create FUNCTION [dbo].[udf_Mov_Pilotonboard] ( @ServiceReferenceID int ) 
RETURNS datetime
AS
BEGIN

DECLARE @OutputString   datetime

select  @OutputString =min(PB.PilotOnBoard)  from ResourceAllocation RA2 
            inner join PilotageServiceRecording PB on PB.ResourceAllocationID = RA2.ResourceAllocationID  
              where 
            RA2.ServiceReferenceType = 'VTSR' 
            and RA2.ServiceReferenceID = (@ServiceReferenceID)

RETURN @OutputString

END

GO

CREATE PROCEDURE [dbo].[usp_rpt_Wego]
   @Port VARCHAR (50), @FromDate DATETIME, @ToDate DATETIME
   WITH
   EXECUTE AS CALLER
AS
   BEGIN
    
     SELECT --RA.ServiceReferenceID,ra.OperationType,
     V.VesselName,
     SB.SubCatName MovementType,
   dbo.udf_GetArrivalReasonForVisit (AN.VCN) AS Reason, 
    VC.PortLimitIn,
    VC.PortLimitOut,
    VC.BreakWaterIn,
    VC.BreakWaterOut,
    V.GrossRegisteredTonnageInMT GRT,
    V.LengthOverallInM LOA,   
    CASE WHEN SR.MovementType = 'ARMV' THEN 
        ISNULL((FORMAT(DateDiff(s,VC.PortLimitIn, dbo.udf_Mov_Pilotonboard(sr.ServiceRequestid))/3600,N'00')+':'+ FORMAT(ABS(DateDiff(s,VC.PortLimitIn, dbo.udf_Mov_Pilotonboard(sr.ServiceRequestid))%3600/60), N'00')), '-') ELSE '-' END AnchorageWaiting,
    CASE WHEN SR.MovementType = 'ARMV' THEN 
        ISNULL((FORMAT(DateDiff(s,VC.BreakWaterIn, dbo.udf_Mov_Pilotonboard(sr.ServiceRequestid))/3600,N'00')+':'+ FORMAT(ABS(DateDiff(s, VC.BreakWaterIn, dbo.udf_Mov_Pilotonboard(sr.ServiceRequestid))%3600/60), N'00')), '-') ELSE '-' END  AS PilotageTimeIn,
    CASE WHEN SR.MovementType = 'ARMV' THEN 
        ISNULL((FORMAT(DateDiff(s,VC.BreakWaterIn, SHF.LastLineIn)/3600,N'00')+':'+ FORMAT(ABS(DateDiff(s,VC.BreakWaterIn, SHF.LastLineIn)%3600/60), N'00')), '-') ELSE
    CASE WHEN SR.MovementType = 'SHMV' THEN 
        ISNULL((FORMAT(DateDiff(s,SHF.LastLineIn, SHF.LastLineOut)/3600,N'00')+':'+ FORMAT(ABS(DateDiff(s,SHF.LastLineIn, SHF.LastLineOut)%3600/60), N'00')), '-')   
    ELSE '-' END END AS ManeuveringTimeIn,
    CASE WHEN SR.MovementType = 'ARMV' OR SR.MovementType = 'SHMV' THEN 
        ISNULL((FORMAT(DateDiff(s,SHF.LastLineIn, SF.StartCargo)/3600,N'00')+':'+ FORMAT(ABS(DateDiff(s,SHF.LastLineIn, SF.StartCargo)%3600/60), N'00')), '-') ELSE '-' END  AS BerthWaitingTime,
    CASE WHEN SR.MovementType = 'ARMV' OR SR.MovementType = 'SHMV' OR  SR.MovementType = 'WRMV' THEN ISNULL((FORMAT(DateDiff(s,SF.StartCargo, SF.EndCargo)/3600,N'00')+':'+ FORMAT(ABS(DateDiff(s, SF.StartCargo, SF.EndCargo)%3600/60), N'00')), '-') ELSE '-' END  AS WorkingTime,
    --CASE WHEN SR.MovementType = 'ARMV' OR SR.MovementType = 'SHMV' THEN ISNULL((FORMAT(DateDiff(s,SF.EndCargo, dbo.udf_Mov_Pilotonboard(sr.ServiceRequestid))/3600,N'00')+':'+ FORMAT(ABS(DateDiff(s,SF.EndCargo, dbo.udf_Mov_Pilotonboard(sr.ServiceRequestid))%3600/60), N'00')), '-') ELSE '-' END  AS BerthWaitingTime1,
    CASE WHEN SR.MovementType = 'ARMV' OR SR.MovementType = 'SHMV' THEN ISNULL((FORMAT(DateDiff(s, dbo.udf_Mov_Pilotonboard(sr.ServiceRequestid), SF.EndCargo)/3600,N'00')+':'+ FORMAT(ABS(DateDiff(s,dbo.udf_Mov_Pilotonboard(sr.ServiceRequestid), SF.EndCargo)%3600/60), N'00')), '-') ELSE '-' END  AS BerthWaitingTime1,
    CASE WHEN SR.MovementType = 'SGMV' THEN ISNULL((FORMAT(DateDiff(s,SHF.FirstLineOut, VC.BreakWaterOut)/3600,N'00')+':'+ FORMAT(ABS(DateDiff(s,SHF.FirstLineOut, VC.BreakWaterOut)%3600/60), N'00')), '-') ELSE '-' END  AS ManeuveringTimeOut,
    CASE WHEN SR.MovementType = 'SGMV' THEN ISNULL((FORMAT(DateDiff(s,dbo.udf_Mov_Pilotonboard(sr.ServiceRequestid), VC.BreakWaterOut)/3600,N'00')+':'+ FORMAT(ABS(DateDiff(s, dbo.udf_Mov_Pilotonboard(sr.ServiceRequestid), VC.BreakWaterOut)%3600/60), N'00')), '-') ELSE '-' END  AS PilotageTimeOut    
FROM ArrivalNotification AN      
   INNER JOIN ServiceRequest sr ON SR.VCN = AN.VCN  
   INNER JOIN SubCategory SB ON sb.SubCatCode = sr.MovementType
   INNER JOIN Vessel V ON V.VesselID = AN.VesselID    
   INNER JOIN VesselCall VC ON VC.VCN = AN.VCN
   inner join VesselCallmovement VCM ON VCM.VCN = AN.VCN and VCM.ServiceRequestID = sr.ServiceRequestID
   LEFT JOIN StatementFact SF ON SF.VCN = AN.VCN
    left join (
    select 
            min(SHF1.LastLineIn) LastLineIn, --RA1.ResourceAllocationID ,
            VM1.ServiceRequestID , SHF1.LastLineOut , SHF1.FirstLineOut
            from ResourceAllocation RA1 
            inner JOIN ShiftingBerthingTaskExecution SHF1 on RA1.ResourceAllocationID = SHF1.ResourceAllocationID  
            inner join VesselCallMovement VM1 on VM1.ServiceRequestID = RA1.ServiceReferenceID
            AND VM1.ATB = SHF1.LastLineIn
            where cast (VM1.MovementDateTime AS DATETIME) BETWEEN @FromDate AND @ToDate and
           RA1.ServiceReferenceType = 'VTSR'
           and RA1.RecordStatus = 'A' 
          and VM1.MovementType != 'SGMV'
			GROUP BY SHF1.LastLineIn, 
      --RA1.ResourceAllocationID ,
      VM1.ServiceRequestID ,
      SHF1.LastLineOut , SHF1.FirstLineOut
       union       
        select 
            min(SHF1.LastLineIn) LastLineIn, --RA1.ResourceAllocationID ,
            VM1.ServiceRequestID , SHF1.LastLineOut , SHF1.FirstLineOut
            from ResourceAllocation RA1 
            inner JOIN ShiftingBerthingTaskExecution SHF1 on RA1.ResourceAllocationID = SHF1.ResourceAllocationID  
            inner join VesselCallMovement VM1 on VM1.ServiceRequestID = RA1.ServiceReferenceID
            AND VM1.ATUB =  SHF1.FirstLineOut
            where  cast (VM1.MovementDateTime AS DATETIME) BETWEEN @FromDate AND @ToDate and
           RA1.ServiceReferenceType = 'VTSR' and RA1.RecordStatus = 'A' 
           and VM1.MovementType = 'SGMV'
			GROUP BY SHF1.LastLineIn, 
      --RA1.ResourceAllocationID ,
      VM1.ServiceRequestID ,
      SHF1.LastLineOut , SHF1.FirstLineOut
    )as SHF on -- RA.ResourceAllocationID = SHF.ResourceAllocationID  and
    SHF.ServiceRequestID  = sr.ServiceRequestID
      
      WHERE  SR.RecordStatus = 'A' 
   and VC.ATD IS NOT NULL AND (SF.StartCargo IS NOT NULL OR SF.EndCargo IS NOT NULL) 
   --and VC.VCN = 'VCNDB1701546'
       AND  (an.PortCode = @Port OR @Port IS NULL) AND
       cast (sr.MovementDateTime AS DATETIME) BETWEEN @FromDate AND @ToDate
 order by AN.VCN , vcm.VesselCallmovementID

  END
GO



