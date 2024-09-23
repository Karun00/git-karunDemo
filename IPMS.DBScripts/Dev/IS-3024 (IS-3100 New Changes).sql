
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_rpt_Wego]') AND type in (N'P'))
DROP PROCEDURE [dbo].[usp_rpt_Wego]
GO
CREATE PROCEDURE [dbo].[usp_rpt_Wego]
   @Port VARCHAR (50), @FromDate DATETIME, @ToDate DATETIME
   WITH
   EXECUTE AS CALLER
AS
   BEGIN
    
     SELECT 
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
    CASE WHEN SR.MovementType = 'ARMV' OR SR.MovementType = 'SHMV' THEN ISNULL((FORMAT(DateDiff(s, dbo.udf_Mov_Pilotonboard(sr.ServiceRequestid), SF.EndCargo)/3600,N'00')+':'+ FORMAT(ABS(DateDiff(s,dbo.udf_Mov_Pilotonboard(sr.ServiceRequestid), SF.EndCargo)%3600/60), N'00')), '-') ELSE '-' END  AS BerthWaitingTime1,
    CASE WHEN SR.MovementType = 'SGMV' THEN ISNULL((FORMAT(DateDiff(s,SHF.FirstLineOut, VC.BreakWaterOut)/3600,N'00')+':'+ FORMAT(ABS(DateDiff(s,SHF.FirstLineOut, VC.BreakWaterOut)%3600/60), N'00')), '-') ELSE '-' END  AS ManeuveringTimeOut,
    CASE WHEN SR.MovementType = 'SGMV' THEN ISNULL((FORMAT(DateDiff(s,dbo.udf_Mov_Pilotonboard(sr.ServiceRequestid), VC.BreakWaterOut)/3600,N'00')+':'+ FORMAT(ABS(DateDiff(s, dbo.udf_Mov_Pilotonboard(sr.ServiceRequestid), VC.BreakWaterOut)%3600/60), N'00')), '-') ELSE '-' END  AS PilotageTimeOut,
    CASE WHEN SR.MovementType = 'SGMV' THEN ISNULL((FORMAT(DateDiff(s,VC.BreakWaterIn, VC.BreakWaterOut)/3600,N'00')+':'+ FORMAT(ABS(DateDiff(s, VC.BreakWaterIn, VC.BreakWaterOut)%3600/60), N'00')), '-') ELSE '-' END  AS STAT
    ,  
    coalesce (FORMAT(SF.StartCargo , 'yyyy-MM-dd HH:mm') ,'-') as StartCargo
    , coalesce (FORMAT(SF.EndCargo , 'yyyy-MM-dd HH:mm') ,'-')  as EndCargo,    
    CASE WHEN SR.MovementType = 'WRMV' THEN    
    '-' ELSE
    [dbo].[udf_Wego_Berth_Ocpy](AN.VCN, sr.ServiceRequestID, SHF.LastLineOut,sr.MovementType) END BerthOccupancyTime,
    ISNULL(STUFF ((SELECT ': ' + b.BerthName from StatementFact st
      join StatementCommodity sc on sc.StatementFactID = st.StatementFactID      
      join Berth b ON b.PortCode = sc.PortCode AND b.QuayCode = sc.QuayCode AND b.BerthCode = sc.BerthCode
      where st.VCN = VC.VCN   AND (sc.PortCode = @Port OR @Port IS NULL)
      order by st.StatementFactID desc  FOR XML PATH('')
      ),  1, 1, ''), '-') AS BerthName, 
    ISNULL(STUFF((SELECT ': ' + Convert(varchar, sc.Quantity) from StatementFact st
      join StatementCommodity sc on sc.StatementFactID = st.StatementFactID            
      where st.VCN = VC.VCN AND (sc.PortCode = @Port OR @Port IS NULL) 
      order by st.StatementFactID desc  FOR XML PATH('')
      ),1, 1, ''), '-') AS Volume,
    ISNULL(STUFF ((SELECT  ': ' +  CONVERT(varchar, CAST(ROUND(sc.Quantity / ISNULL((datediff(mi,st.StartCargo, st.EndCargo)/ 60.0), '1'), 2 ) as numeric(10,2)))
      from StatementFact st
      join StatementCommodity sc on sc.StatementFactID = st.StatementFactID            
      where st.VCN = VC.VCN AND (sc.PortCode = @Port OR @Port IS NULL) 
      order by st.StatementFactID desc  FOR XML PATH('')
      ), 1, 1, ''), '-') AS ShipWorkingHour,
    ISNULL(STUFF ((SELECT  ': ' +  CONVERT(varchar, CAST(ROUND(sc.Quantity / ISNULL((datediff(mi,[dbo].[udf_Wego_ResourceAllocTime](VC.VCN, 'ARMV', 'FI'), [dbo].[udf_Wego_ResourceAllocTime](VC.VCN, 'SGMV', 'LO'))/ 60.0), '1'), 2 ) as numeric(10,2)))
      from StatementFact st
      join StatementCommodity sc on sc.StatementFactID = st.StatementFactID            
      where st.VCN = VC.VCN AND (sc.PortCode = @Port OR @Port IS NULL) 
      order by st.StatementFactID desc  FOR XML PATH('')
      ), 1, 1, ''), '-') AS BerthProductivity,    
    ISNULL(STUFF ((  SELECT  ': ' +  CONVERT(varchar, CAST(ROUND(sc.Quantity / ISNULL((datediff(mi,VC.BreakWaterIn, VC.BreakWaterOut)/ 60.0), '1'), 2 ) as numeric(10,2)))
      from StatementFact st
      join StatementCommodity sc on sc.StatementFactID = st.StatementFactID            
      where st.VCN = VC.VCN AND (sc.PortCode = @Port OR @Port IS NULL) 
      order by st.StatementFactID desc  FOR XML PATH('')
      ),1, 1, ''), '-') AS ShipProductivityIndicator    
FROM ArrivalNotification AN      
   INNER JOIN ServiceRequest sr ON SR.VCN = AN.VCN  
   INNER JOIN SubCategory SB ON sb.SubCatCode = sr.MovementType
   INNER JOIN Vessel V ON V.VesselID = AN.VesselID    
   INNER JOIN VesselCall VC ON VC.VCN = AN.VCN
   inner join VesselCallmovement VCM ON VCM.VCN = AN.VCN and VCM.ServiceRequestID = sr.ServiceRequestID
   LEFT JOIN StatementFact SF ON SF.VCN = AN.VCN   
    left join (
    select 
            min(SHF1.LastLineIn) LastLineIn, 
            VM1.ServiceRequestID , SHF1.LastLineOut , SHF1.FirstLineOut
            from ResourceAllocation RA1 
            inner JOIN ShiftingBerthingTaskExecution SHF1 on RA1.ResourceAllocationID = SHF1.ResourceAllocationID  
            inner join VesselCallMovement VM1 on VM1.ServiceRequestID = RA1.ServiceReferenceID
            AND VM1.ATB = SHF1.LastLineIn
            where RA1.ServiceReferenceType = 'VTSR'
           and RA1.RecordStatus = 'A' 
          and VM1.MovementType != 'SGMV'
			GROUP BY SHF1.LastLineIn,    
      VM1.ServiceRequestID ,
      SHF1.LastLineOut , SHF1.FirstLineOut
       union       
        select 
            min(SHF1.LastLineIn) LastLineIn, 
            VM1.ServiceRequestID , SHF1.LastLineOut , SHF1.FirstLineOut
            from ResourceAllocation RA1 
            inner JOIN ShiftingBerthingTaskExecution SHF1 on RA1.ResourceAllocationID = SHF1.ResourceAllocationID  
            inner join VesselCallMovement VM1 on VM1.ServiceRequestID = RA1.ServiceReferenceID
            AND VM1.ATUB =  SHF1.FirstLineOut
            where RA1.ServiceReferenceType = 'VTSR' and RA1.RecordStatus = 'A' 
           and VM1.MovementType = 'SGMV'
			GROUP BY SHF1.LastLineIn,      
      VM1.ServiceRequestID ,
      SHF1.LastLineOut , SHF1.FirstLineOut
    )as SHF on SHF.ServiceRequestID  = sr.ServiceRequestID
      
      WHERE  SR.RecordStatus = 'A' 
   and VC.ATD IS NOT NULL   
       AND  (an.PortCode = @Port OR @Port IS NULL) AND
       cast (vc.ATD AS DATETIME) BETWEEN @FromDate AND @ToDate        
order by AN.VCN , vcm.VesselCallmovementID

  END
GO


