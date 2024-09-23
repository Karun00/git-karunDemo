ALTER PROCEDURE [dbo].[usp_rpt_Wego]
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
    CASE WHEN SR.MovementType = 'SGMV' THEN ISNULL((FORMAT(DateDiff(s,dbo.udf_Mov_Pilotonboard(sr.ServiceRequestid), VC.BreakWaterOut)/3600,N'00')+':'+ FORMAT(ABS(DateDiff(s, dbo.udf_Mov_Pilotonboard(sr.ServiceRequestid), VC.BreakWaterOut)%3600/60), N'00')), '-') ELSE '-' END  AS PilotageTimeOut,
    CASE WHEN SR.MovementType = 'SGMV' THEN ISNULL((FORMAT(DateDiff(s,VC.BreakWaterIn, VC.BreakWaterOut)/3600,N'00')+':'+ FORMAT(ABS(DateDiff(s, VC.BreakWaterIn, VC.BreakWaterOut)%3600/60), N'00')), '-') ELSE '-' END  AS STAT    
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
   and VC.ATD IS NOT NULL --AND (SF.StartCargo IS NOT NULL OR SF.EndCargo IS NOT NULL)    
       AND  (an.PortCode = @Port OR @Port IS NULL) AND
       cast (sr.MovementDateTime AS DATETIME) BETWEEN @FromDate AND @ToDate
 order by AN.VCN , vcm.VesselCallmovementID

  END
GO
