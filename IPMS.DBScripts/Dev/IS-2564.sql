
ALTER FUNCTION [dbo].[udf_Wego_ResourceAllocTime] (@VCN VARCHAR(50),@MovementType  VARCHAR(4), @Flag  VARCHAR(2)) 
RETURNS varchar(50)
AS

 BEGIN

DECLARE @Value DATETIME


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
  BEGIN
    IF(@Flag = 'FO')    
          SELECT  @Value =   
           max(SHF1.FirstLineOut)
        FROM ArrivalNotification AN      
      INNER JOIN ServiceRequest sr ON SR.VCN = AN.VCN  
      INNER JOIN VesselCallmovement VCM ON VCM.VCN = AN.VCN and VCM.ServiceRequestID = sr.ServiceRequestID
      INNER JOIN ResourceAllocation R on SR.ServiceRequestID = R.ServiceReferenceID and R.ServiceReferenceType = 'VTSR' and R.OperationType = 'BRTH'
      INNER JOIN ShiftingBerthingTaskExecution SHF1 on R.ResourceAllocationID = SHF1.ResourceAllocationID  
      WHERE AN.VCN = @VCN AND sr.RecordStatus = 'A' AND R.RecordStatus = 'A'
       AND VCM.MovementType = 'SGMV'
    ELSE  
         SELECT  @Value =   
           min(P1.PilotOnBoard)
        FROM ArrivalNotification AN      
      INNER JOIN ServiceRequest sr ON SR.VCN = AN.VCN  
      INNER JOIN VesselCallmovement VCM ON VCM.VCN = AN.VCN and VCM.ServiceRequestID = sr.ServiceRequestID
      INNER JOIN ResourceAllocation R on SR.ServiceRequestID = R.ServiceReferenceID and R.ServiceReferenceType = 'VTSR' and R.OperationType = 'PILT'
      INNER JOIN PilotageServiceRecording P1 on R.ResourceAllocationID = P1.ResourceAllocationID  
      WHERE AN.VCN = @VCN AND sr.RecordStatus = 'A' AND R.RecordStatus = 'A'
        AND VCM.MovementType = 'SGMV'

  END
  
RETURN @Value

END
GO


CREATE PROCEDURE [dbo].[usp_GetWegoBerthUtilization]
  @FromDate DATE, @ToDate DATE,  @PortCode VARCHAR (50)
   WITH
   EXECUTE AS CALLER
AS
   BEGIN
SELECT
 S.BerthName 
 ,COUNT(S.VCN) NoofVessels
,CONVERT(VARCHAR, CAST(SUM(AnchorageWaitingTime)/COUNT(S.VCN) as numeric(12,2)))   AnchorageWaitingTime
,CONVERT(VARCHAR, CAST(SUM(STAT)/COUNT(S.VCN) as numeric(12,2)))  STAT
,CONVERT(VARCHAR, CAST((SUM(NPAManueringTime) + SUM(PilotageIn))/COUNT(S.VCN) as numeric(12,2)))  PilotageTimeIn
,CONVERT(VARCHAR, CAST(SUM(PreCargoWorking)/COUNT(S.VCN) as numeric(12,2)))  PreCargoWorking
,CONVERT(VARCHAR, CAST(SUM(VesselWorkingTime)/COUNT(S.VCN) as numeric(12,2)))  VesselWorkingTime
,CONVERT(VARCHAR, CAST(SUM(PostCargoWorkingTime)/COUNT(S.VCN) as numeric(12,2)))  PostCargoWorkingTime
,CONVERT(VARCHAR, CAST(SUM(PilotageTimeOut)/COUNT(S.VCN) as numeric(12,2)))  PilotageTimeOut
 FROM
(SELECT 
 AN.VCN
,BR.BerthName
,ISNULL((datediff(mi,VC.PortLimitIn,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'PB'))/ 60.0), '0.0') AnchorageWaitingTime
,ISNULL((datediff(mi,VC.BreakWaterIn,VC.BreakWaterOut)/ 60.0), '0.0') STAT
,ISNULL((datediff(mi,VC.BreakWaterIn,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'LI'))/ 60.0), '0.0') NPAManueringTime
,ISNULL((datediff(mi,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'PB'),VC.BreakWaterIn)/ 60.0), '0.0') PilotageIn
,ISNULL((datediff(mi, [dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'LI') ,SF.StartCargo)/ 60.0), '0.0') PreCargoWorking
,ISNULL((datediff(mi,SF.StartCargo, SF.EndCargo)/ 60.0), '0.0') VesselWorkingTime
,ISNULL((datediff(mi,SF.EndCargo, [dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'SGMV', 'FO'))/ 60.0), '0.0') PostCargoWorkingTime
,ISNULL((datediff(mi,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'SGMV', ''),VC.BreakWaterOut)/ 60.0), '0.0') PilotageTimeOut
,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'PB') PilotOnBoard
,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'ARMV', 'LI') LastLineIn
,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'SGMV', 'FO') FirstLineOut
,[dbo].[udf_Wego_ResourceAllocTime](AN.VCN, 'SGMV', '') PilotOnBoardSailing
FROM
ArrivalNotification AN   
INNER JOIN VesselCall VC ON VC.VCN = AN.VCN
INNER JOIN Vessel V ON V.VesselID = AN.VesselID
INNER JOIN Berth BR ON BR.PortCode = VC.FromPositionPortCode AND BR.QuayCode = VC.FromPositionQuayCode
AND BR.BerthCode = VC.FromPositionBerthCode
LEFT JOIN StatementFact SF ON SF.VCN = AN.VCN
WHERE VC.ATD IS NOT NULL 
AND AN.RecordStatus = 'A' AND VC.RecordStatus = 'A' 
  AND  CAST (VC.BreakWaterOut AS DATE) BETWEEN @FromDate AND @ToDate
  AND (an.PortCode = @PortCode OR @PortCode IS NULL) 
) S GROUP BY S.BerthName
ORDER BY S.BerthName

END
GO


Insert Into Entity
(EntityCode, Moduleid, Entityname, PageUrl, OrderNo, Tokens, HasWorkflow, HasMenuItem, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, PendingTaskColumns, ControllerName) VALUES
('WEGOBRTH', (select ModuleID from Module where ModuleName = 'Port Performance'), 'Wego Berth Utilization', 'WegoBerthDashBoard', 10, '', 'N', 'Y', 'A', 2, getDate(), 2, getDate(), '', 'WegoBerthDashBoard')
GO

Insert into ENTITYPRIVILEGE
   (ENTITYID, SUBCATCODE, RECORDSTATUS, CREATEDBY, CREATEDDATE, 
    MODIFIEDBY, MODIFIEDDATE)
Values
   ((select entityid from entity where entitycode='WEGOBRTH'), 'VIEW', 'A', 2, getDate(), 
    2,  getDate());

GO

Insert into ROLEPRIVILEGE
   (ROLEID, ENTITYID, SUBCATCODE, RECORDSTATUS, CREATEDBY, 
    CREATEDDATE, MODIFIEDBY, MODIFIEDDATE)
Values
   ((select roleid from role where rolecode='ADMN'), (select entityid from entity where entitycode='WEGOBRTH'), 'VIEW', 'A', 2, 
   getDate(), 2, getDate());

GO
Insert into ROLEPRIVILEGE
   (ROLEID, ENTITYID, SUBCATCODE, RECORDSTATUS, CREATEDBY, 
    CREATEDDATE, MODIFIEDBY, MODIFIEDDATE)
Values
   ((select roleid from role where rolecode='OPM'), (select entityid from entity where entitycode='WEGOBRTH'), 'VIEW', 'A', 2, 
   getDate(), 2, getDate());
GO
Insert into ROLEPRIVILEGE
   (ROLEID, ENTITYID, SUBCATCODE, RECORDSTATUS, CREATEDBY, 
    CREATEDDATE, MODIFIEDBY, MODIFIEDDATE)
Values
   ((select roleid from role where rolecode='OU'), (select entityid from entity where entitycode='WEGOBRTH'), 'VIEW', 'A', 2, 
   getDate(), 2, getDate());

GO
Insert into ROLEPRIVILEGE
   (ROLEID, ENTITYID, SUBCATCODE, RECORDSTATUS, CREATEDBY, 
    CREATEDDATE, MODIFIEDBY, MODIFIEDDATE)
Values
   ((select roleid from role where rolecode='DHM'), (select entityid from entity where entitycode='WEGOBRTH'), 'VIEW', 'A', 2, 
   getDate(), 2, getDate());

