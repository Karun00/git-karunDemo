INSERT INTO Entity
(EntityCode, Moduleid, Entityname, PageUrl, OrderNo, Tokens, HasWorkflow, HasMenuItem, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, PendingTaskColumns, ControllerName)
SELECT 'MOPSDELAY', 29, 'Mops Delay Report', 'Report/MOPSDelayReport', 1, NULL, 'N', 'Y', 'A', 2, getDate(), 2, getDate(), NULL, NULL
where 'MOPSDELAY' NOT IN (select EntityCode from Entity where EntityCode = 'MOPSDELAY')
GO
Insert into EntityPrivilege
(EntityID, SubCatCode, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) 
select (SELECT EntityID from Entity where EntityCode = 'MOPSDELAY') EntityID, 'VIEW' SubCatCode, 'A' RecordStatus, 1 CreatedBy,  getDate() CreatedDate, 1 ModifiedBy, getDate() ModifiedDate
where CONVERT(varchar,(SELECT EntityID from Entity where EntityCode = 'MOPSDELAY'))+'VIEW' NOT IN (select CONVERT(varchar, EntityID)+SubCatCode from EntityPrivilege)
GO
Insert into RolePrivilege
(RoleID, EntityID, SubCatCode, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) 
SELECT (SELECT RoleID from ROLE where ROLECODE = 'ADMN'),(SELECT EntityID from Entity where EntityCode = 'MOPSDELAY') ,'VIEW', 'A', 1 ,  getDate(), 1, getDate()
where CONVERT(varchar,(SELECT EntityID from Entity where EntityCode = 'MOPSDELAY'))+'VIEW' NOT IN (select CONVERT(varchar, EntityID)+SubCatCode from RolePrivilege)
GO
IF EXISTS
      (SELECT *
         FROM sys.objects
        WHERE object_id = OBJECT_ID (N'[dbo].[usp_rpt_MOPSDelay]')
              AND type IN (N'P'))
DROP PROCEDURE [dbo].[usp_rpt_MOPSDelay]
GO
CREATE PROCEDURE [dbo].[usp_rpt_MOPSDelay]
   @Port VARCHAR (50), @FromDate DATETIME, @ToDate DATETIME
   WITH
   EXECUTE AS CALLER
AS
   BEGIN
      SET  NOCOUNT ON
    SELECT VesselName, an.VCN, ISNULL(dbo.udf_CargoTypeBasedOnVCN(an.VCN), 'NA') AS Commodity,
     ISNULL(dbo.udf_GetArrivalReasonForVisit(an.VCN), 'NA') AS ReasonForVisit
      ,sb.SubCatName AS MovementType, sr.MovementDateTime RequestedDateTime
      ,CASE WHEN sr.MovementType = 'SGMV' THEN (ISNULL(toberth.BerthName, 'NA')) ELSE ISNULL(curberth.BerthName, 'NA') END Berth
      ,ps.StartTime ServedDateTime
      ,FORMAT(DateDiff(s,sr.MovementDateTime, ps.StartTime)/3600,N'00')+':'+ FORMAT(ABS(DateDiff(s,sr.MovementDateTime,ps.StartTime)%3600/60), N'00')  AS Deviation
      ,ISNULL(NULLIF(ps.DelayReason,''),'NA') DelayReason
      ,ISNULL(psu.SubCatName,'NA') MOPSDelay
     FROM ResourceAllocation ra
      INNER JOIN ServiceRequest sr ON sr.ServiceRequestID = ra.ServiceReferenceID
      INNER JOIN ArrivalNotification an ON an.VCN= sr.VCN
      INNER JOIN Vessel v ON v.VesselID = an.VesselID
      INNER JOIN VesselCallMovement vcm ON vcm.ServiceRequestID = sr.ServiceRequestID
      INNER JOIN SubCategory sb ON sb.SubCatCode= sr.MovementType
      INNER JOIN VesselCall vc ON vc.VCN = an.VCN
      INNER JOIN PilotageServiceRecording ps ON ra.ResourceAllocationID = ps.ResourceAllocationID
      LEFT JOIN Berth curberth ON curberth.BerthCode=vcm.FromPositionBerthCode AND curberth.QuayCode = vcm.FromPositionQuayCode AND curberth.PortCode = an.PortCode
      LEFT JOIN Berth toberth ON toberth.BerthCode=vc.FromPositionBerthCode AND toberth.QuayCode = vc.FromPositionQuayCode AND toberth.PortCode =  an.PortCode
      LEFT JOIN SubCategory psu ON ps.MOPSDelay = psu.SubCatCode
      WHERE ra.RecordStatus = 'A' AND ra.ServiceReferenceType = 'VTSR' AND ra.OperationType =  'PILT' AND (ra.TaskStatus = 'VERF' OR ra.TaskStatus = 'COMP')
           AND  (an.PortCode = @Port OR @Port IS NULL) AND
           cast (sr.MovementDateTime AS DATETIME) BETWEEN @FromDate AND @ToDate
   END
GO