IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_rpt_Completemovementreport]') AND type in (N'P'))
DROP PROCEDURE [dbo].[usp_rpt_Completemovementreport]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_rpt_Completemovementreport]') AND type in (N'P'))
DROP PROCEDURE [dbo].[usp_rpt_Completemovementreport]
GO
CREATE PROCEDURE [dbo].[usp_rpt_Completemovementreport]
   @FromDate      DATETIME,
   @ToDate        DATETIME,
   @Port          VARCHAR (50),
   @VesselType    VARCHAR (50),
   @VCN           VARCHAR (20)
   WITH
   EXECUTE AS CALLER
AS
   BEGIN
      SET  NOCOUNT ON

      SET  ANSI_WARNINGS OFF

     SELECT VCN,
       ServiceRequestID,
       MOVEMENTTYP,
       VesselName,
       VesselType as vssltyp,
       BreakWaterIn,
       PortLimitOut,
       BerthName,
       PortCode,
       ReasonForVisit,
       PortName,
       LastPortOfCall,
       NextPortOfCall,
       CallSign,
       GrossRegisteredTonnageInMT,
       DeadWeightTonnageInMT,
       LengthOverallInM,
       ForwardDraft,
       AfterDraft,
       BreakWaterOut,
       PortLimitIn,
       RegisteredName,
	   SNO,
       BerthName AS Berth1,
       Pilot_1 AS Pilot,
       VesselNationality AS CraftNationality,
       [Tug Boats/Work Boat_1] AS Tug,
       IMONo,
       [Berth Master_1] AS Berthmaster1,
	   case MovementTyp when 'Sailing' then FirstLineOut_1 else case when SNO=0 then FirstLineOut_1 else FirstLineIn_1 END END as FirstLineInTime1,
	   case MovementTyp when 'Sailing' then LastLineOut_1 else case when SNO=0 then LastLineOut_1 else LastLineIn_1 END END AS LastLineInTime1,

       FirstLineOut_1 AS FirstLineOutTime1,
       LastLineOut_1 AS LastLineOutTime1,
       [Berth Master_2] AS Berthmaster2,
	   
	   case MovementTyp when 'Sailing' then FirstLineOut_2 else case when SNO=0 then FirstLineOut_2 else FirstLineIn_2 END END as FirstLineInTime2,
	   case MovementTyp when 'Sailing' then LastLineOut_2 else case when SNO=0 then LastLineOut_2 else LastLineIn_2 END END AS LastLineInTime2,
       
	   FirstLineOut_2 as FirstLineOutTime2,
       LastLineOut_2 as LastLineOutTime2,
       Pilot_1 as pilt1,
       PilotOnBoard_1 as PilotOnBoardTime1,
       PilotOff_1 as PilotOfftime1,
       Pilot_2 as pilt2,
       PilotOnBoard_2 as PilotOnBoardTime2,
       PilotOff_2 as PilotOfftime2,
       [Tug Boats/Work Boat_1] as Tug1,
       Tug_StartTime_1 as TugStartTime1,
       Tug_EndTime_1 as TugEndTime1,
       [Tug Boats/Work Boat_2] as Tug2,
       Tug_StartTime_2 as TugStartTime2,
       Tug_EndTime_2 as TugEndTime2,
       [Tug Boats/Work Boat_3] as Tug3,
       Tug_StartTime_3 as TugStartTime3,
       Tug_EndTime_3 as TugEndTime3
from dbo.[RPT_CompleteMovement]
Where (VCN = @VCN OR @VCN IS NULL)  AND (VesselType = @VesselType OR @VesselType is NULL) AND (PortCode = @Port OR @Port IS NULL)
AND case MovementTyp when 'Sailing' then ATUB else ATB END between @FromDate AND @ToDate  order by VCN, ServiceRequestID, Sno 

   END
GO

CREATE TABLE [dbo].[RPT_CompleteMovement]
(
   [VCN]                          VARCHAR (50) NULL,
   [VesselName]                   NVARCHAR (200) NULL,
   [GrossRegisteredTonnageInMT]   NUMERIC (8, 2) NULL,
   [DeadWeightTonnageInMT]        NUMERIC (8, 2) NULL,
   [LengthOverallInM]             NUMERIC (8, 2) NULL,
   [SNO]                          BIGINT NULL,
   [BerthName]                    NVARCHAR (25) NULL,
   [ReasonForVisit]               NVARCHAR (MAX) NULL,
   [PortCode]                     NVARCHAR (2) NULL,
   [PortName]                     NVARCHAR (20) NULL,
   [IMONo]                        NVARCHAR (15) NULL,
   [RegisteredName]               NVARCHAR (100) NULL,
   [VesselType]                   NVARCHAR (500) NULL,
   [VesselNationality]            NVARCHAR (200) NULL,
   [PortLimitIn]                  DATETIME NULL,
   [PortLimitOut]                 DATETIME NULL,
   [BreakWaterIn]                 DATETIME NULL,
   [BreakWaterOut]                DATETIME NULL,
   [LastPortOfCall]               NVARCHAR (200) NULL,
   [NextPortOfCall]               NVARCHAR (200) NULL,
   [ForwardDraft]                 NVARCHAR (15) NULL,
   [AfterDraft]                   NVARCHAR (15) NULL,
   [ServiceRequestID]             INT NULL,
   [MovementDateTime]             DATETIME NULL,
   [ATB]                          DATETIME NULL,
   [ATUB]                         DATETIME NULL,
   [MovementTyp]                  VARCHAR (50) NULL,
   [Tug Boats/Work Boat_1]        VARCHAR (50) NULL,
   [Tug Boats/Work Boat_2]        VARCHAR (50) NULL,
   [Pilot_1]                      VARCHAR (50) NULL,
   [Berth Master_3]               VARCHAR (50) NULL,
   [Pilot_2]                      VARCHAR (50) NULL,
   [Tug Boats/Work Boat_4]        VARCHAR (50) NULL,
   [Tug Boats/Work Boat_3]        VARCHAR (50) NULL,
   [Berth Master_2]               VARCHAR (50) NULL,
   [Berth Master_1]               VARCHAR (50) NULL,
   [Berth_StartTime_3]            DATETIME NULL,
   [Berth_StartTime_2]            DATETIME NULL,
   [Berth_StartTime_1]            DATETIME NULL,
   [Berth_EndTime_1]              DATETIME NULL,
   [Berth_EndTime_2]              DATETIME NULL,
   [Berth_EndTime_3]              DATETIME NULL,
   [FirstLineIn_2]                DATETIME NULL,
   [FirstLineIn_1]                DATETIME NULL,
   [FirstLineIn_3]                DATETIME NULL,
   [FirstLineOut_2]               DATETIME NULL,
   [FirstLineOut_1]               DATETIME NULL,
   [FirstLineOut_3]               DATETIME NULL,
   [LastLineIn_1]                 DATETIME NULL,
   [LastLineIn_2]                 DATETIME NULL,
   [LastLineIn_3]                 DATETIME NULL,
   [LastLineOut_2]                DATETIME NULL,
   [LastLineOut_1]                DATETIME NULL,
   [LastLineOut_3]                DATETIME NULL,
   [Pilot_StartTime_2]            DATETIME NULL,
   [Pilot_StartTime_1]            DATETIME NULL,
   [Pilot_EndTime_2]              DATETIME NULL,
   [Pilot_EndTime_1]              DATETIME NULL,
   [PilotOnBoard_1]               DATETIME NULL,
   [PilotOnBoard_2]               DATETIME NULL,
   [PilotOff_2]                   DATETIME NULL,
   [PilotOff_1]                   DATETIME NULL,
   [Tug_StartTime_2]              DATETIME NULL,
   [Tug_StartTime_1]              DATETIME NULL,
   [Tug_StartTime_4]              DATETIME NULL,
   [Tug_StartTime_3]              DATETIME NULL,
   [Tug_EndTime_3]                DATETIME NULL,
   [Tug_EndTime_2]                DATETIME NULL,
   [Tug_EndTime_4]                DATETIME NULL,
   [Tug_EndTime_1]                DATETIME NULL,
   [CallSign]                     NVARCHAR (200) NULL,
   [PilotBoat_1]                  NVARCHAR (50) NULL,
   [PilotBoat_2]                  NVARCHAR (50) NULL
)
GO
CREATE INDEX idx_temp ON RPT_CompleteMovement (VCN,ServiceRequestID,SNO);

CREATE FUNCTION [dbo].[udf_get_PreviousBerthDetails] (
   @ServRequestID    INT)
   RETURNS TABLE
AS 
RETURN 	select * From (
SELECT dense_rank() over (order by a.BerthingTaskExecutionID) as SNo,vcm.VesselCallMovementID,
       VCM.MovementDateTime,
       vcm.VCN,
       vcm.MovementType,
       vcm.ServiceRequestID,
       vcm.ToPositionPortCode,
       vcm.ToPositionQuayCode,
       vcm.ToPositionBerthCode,
       vcm.ToPositionBollardCode,
       A.ShiftedFromBerth
  FROM VesselCallMovement vcm
       INNER JOIN
       (SELECT dense_rank() over (order by SBTE.BerthingTaskExecutionID) as SNo, RA.OperationType,
               RA.ResourceAllocationID,
               RA.ServiceReferenceID,
               RA.StartTime,
               RA.EndTime,
               RA.ResourceType,
               RA.ResourceID,
               SBTE.BerthingTaskExecutionID,
               SBTE.FirstLineOut,
               SBTE.LastLineOut,
               B.BerthName as ShiftedFromBerth,
               SBTE.ToBerthPortCode, SBTE.ToBerthQuayCode, SBTE.ToBerthCode
          FROM ResourceAllocation RA
               INNER JOIN ShiftingBerthingTaskExecution SBTE
                  ON SBTE.ResourceAllocationID = RA.ResourceAllocationID
               INNER JOIN Berth B on B.PortCode = SBTE.ToBerthPortCode and B.QuayCode=SBTE.ToBerthQuayCode and B.BerthCode = SBTE.ToBerthCode
         WHERE RA.RecordStatus='A' and RA.OperationType='BRTH'  and ra.ServiceReferenceType = 'VTSR' --and (SBTE.FirstLineOut is not null or SBTE.LastLineOut is not null ) 
         ) A
          ON A.ServiceReferenceID = vcm.ServiceRequestID --and A.SNO=1
 WHERE  ServiceRequestID<>@ServRequestID and  vcm.VCN = (select SR.VCN  FROM ServiceRequest SR where ServiceRequestID=@ServRequestID)
       AND vcm.MovementDateTime =
              (SELECT max (L_VCM.MovementDateTime)
                 FROM VesselCallMovement L_VCM
                WHERE     L_VCM.VCN = (select SR.VCN  FROM ServiceRequest SR where ServiceRequestID=@ServRequestID)
                      AND L_VCM.RecordStatus = 'A'
                      AND L_VCM.MovementDateTime <
                             (SELECT MovementDateTime
                                FROM VesselCallMovement
                               WHERE ServiceRequestID = @ServRequestID))) SFB where SNO=1;                               

GO
------------------Schedule every hour below Procedure
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USP_RPT_CMR]') AND type in (N'P'))
DROP PROCEDURE [dbo].[USP_RPT_CMR]
GO
CREATE PROCEDURE [dbo].[USP_RPT_CMR]
AS
BEGIN
DECLARE
  @vcn          VARCHAR (50),
  @Port         VARCHAR (50),
  @FromDate     DATETIME,
  @ToDate       DATETIME,
  @VesselType	nvarchar(500),
  @Columns      NVARCHAR(MAX), 
  @AllColumns   NVARCHAR(MAX), 
  @query        NVARCHAR(MAX),
  @DynaQry      NVARCHAR(MAX),
  @errmsg       NVARCHAR (MAX),
  @currdate     DATETIME,
  @RowsInserted INT,
  @Row_count 	INT,
  @AllTempTables NVARCHAR(MAX),
  @TempTable    NVARCHAR(50),
  @SearchCharPos	INT,
  @TargetTable   NVARCHAR (50),
  @StartPos		INT;
BEGIN
set @Row_count=-1
set @StartPos=1

SELECT @Row_count=count(PortCode) FROM [dbo].[Port]
          
select @AllTempTables = STUFF (
            (SELECT DISTINCT ',' + (DynamicTab)
             FROM (SELECT 'RPT_CompleteMovement_'+portcode as DynamicTab
                   FROM Port) As SourceTable
           FOR XML PATH ( '' ) , TYPE).value ('.', 'nvarchar(max)'),1,1,'')
           
select @SearchCharPos=CHARINDEX(',',@AllTempTables)
select @TempTable = substring(@AllTempTables,@StartPos,CHARINDEX(',',@AllTempTables)-1)
set @StartPos = @StartPos+len(@TempTable)+1

WHILE @Row_count != 0
BEGIN
SET @vcn = null;--'VCNDB1801963'
SET @Port = substring(@TempTable,len(@TempTable)-1,2);
SET @FromDate = CONVERT(DATETIME, '2015-01-01', 121);--CONVERT(DATETIME, '2018-08-01', 121);
SET @ToDate = CONVERT(DATETIME, '2018-09-30', 121);--CONVERT(DATETIME, '2018-08-31', 121);
set @VesselType = null;

IF OBJECT_ID('tempdb..#Recordings') IS NOT NULL DROP TABLE #Recordings

CREATE TABLE  #Recordings 
(
VCN						    VARCHAR (50),
MovementDateTime  			datetime,
MovementTyp		  	        VARCHAR (50),
ServiceRequestID			INT,
ServiceReferenceID			INT,
LastPortOfCall  		  	NVARCHAR(200),
NextPortOfCall 			  	NVARCHAR(200),
ForwardDraft			    nvarchar(15),
AfterDraft				    nvarchar(15),
VesselName  			    nvarchar(200),
VesselType          		nvarchar(500),
VesselNationality  			NVARCHAR(200),
IMONo					    nvarchar(15),
BerthName     			  	NVARCHAR(25),
PortCode    			    NVARCHAR(2),
PortName    			    nvarchar(20),
CallSign				    nvarchar(200),
GrossRegisteredTonnageInMT	numeric(8, 2),
DeadWeightTonnageInMT		numeric(8, 2),
LengthOverallInM		  	numeric(8, 2),
ReasonForVisit        		NVARCHAR (MAX),
RegisteredName			  	nvarchar(100),
PortLimitIn				    DATETIME,
PortLimitOut          		DATETIME,
BreakWaterIn			    DATETIME,
BreakWaterOut			    DATETIME,
ResourceSno					INT,
ResourceAllocationID 		INT,
ResourceID				    INT,
ResourceName			    VARCHAR (50),
ServiceTypeName		  		VARCHAR (50),
Berth_StartTime			  	DATETIME,
Berth_EndTime				DATETIME,
FirstLineIn					DATETIME,
FirstLineOut				DATETIME,
LastLineIn					DATETIME,
LastLineOut					DATETIME,
Pilot_StartTime			  	DATETIME,
Pilot_EndTime				DATETIME,
PilotOnBoard				DATETIME,
PilotOff 					DATETIME,
TugName						NVARCHAR(30),
Tug_StartTime			    DATETIME,
Tug_EndTime					DATETIME,
ATB					        DATETIME,
ATUB				        DATETIME
)

CREATE INDEX idx_temp ON #Recordings (VCN,ServiceRequestID,ResourceSno);

----Collecting Service Recording Data and saving in Temporary table
BEGIN
insert into #Recordings (VCN,LastPortOfCall,NextPortOfCall,MovementDateTime,ATB,ATUB,MovementTyp,ServiceRequestID,ServiceReferenceID,ResourceSno,ResourceAllocationID,ResourceID,ResourceName,ServiceTypeName,Berth_StartTime,Berth_EndTime,FirstLineIn,FirstLineOut,LastLineIn,LastLineOut,Pilot_StartTime,Pilot_EndTime,PilotOnBoard,PilotOff ,TugName,Tug_StartTime,Tug_EndTime,PortLimitIn,PortLimitOut,BreakWaterIn,BreakWaterOut,ForwardDraft,AfterDraft,VesselName,CallSign,IMONo,RegisteredName,VesselType,VesselNationality,BerthName,PortCode,PortName,GrossRegisteredTonnageInMT,DeadWeightTonnageInMT,LengthOverallInM,ReasonForVisit)
SELECT Rec.VCN,REC.LastPortOfCall,REC.NextPortOfCall,Rec.MovementDateTime,REC.ATB,REC.ATUB,Rec.MovementTyp,REC.ServiceRequestID, Rec.ServiceReferenceID,Rec.ResourceSno,
       Rec.ResourceAllocationID,Rec.ResourceID,
       Rec.ResourceName,Rec.ServiceTypeName,
       MAX(Rec.Berth_StartTime) AS 'BerthStartTime',
       MAX(Rec.Berth_EndTime) AS 'BerthEndTime',
       MAX(Rec.FirstLineIn) FirstLineIn,
       MAX(Rec.FirstLineOut) FirstLineOut,
       MAX(Rec.LastLineIn) LastLineIn,
       MAX(Rec.LastLineOut) LastLineOut,       
       MAX(Rec.Pilot_StartTime) AS 'PilotStartTime',
       MAx(Rec.Pilot_EndTime) AS 'PilotEndTime',
       MAX(Rec.PilotOnBoard) AS PilotOnBoard,
       MAX(Rec.PilotOff) as PilotOff,       
       Rec.CraftName as TugName,
       MAX(Rec.Tug_StartTime) AS 'TugStartTime',
       MAX(Rec.Tug_EndTime) AS 'TugEndTime',
       Rec.PortLimitIn,REC.PortLimitOut,Rec.BreakWaterIn,Rec.BreakWaterOut,ForwardDraft,AfterDraft,VesselName,CallSign,IMONo,RegisteredName,VesselType,VesselNationality,BerthName,PortCode,PortName,
       REC.GrossRegisteredTonnageInMT,REC.DeadWeightTonnageInMT,REC.LengthOverallInM,REC.ReasonForVisit
  FROM (SELECT VCN,LastPortOfCall,NextPortOfCall,ForwardDraft,AfterDraft,MovementDateTime,ATB,ATUB,MovementTyp,ServiceRequestID,ServiceReferenceID,ResourceSno,ResourceAllocationID,ResourceID,CraftName,ResourceName,ServiceTypeName,PortLimitIn,PortLimitOut,BreakWaterIn,BreakWaterOut,MAX([BRTH]) as Berth_StartTime,null as Berth_EndTime, MAX([PILT]) as Pilot_StartTime, null as Pilot_EndTime,MAX([TGWR]) as Tug_StartTime, null as Tug_EndTime,
				MAX(PilotOnBoard) as PilotOnBoard,MAX(PilotOff) as PilotOff,MAX(FirstLineIn) as FirstLineIn,MAX(FirstLineOut) as FirstLineOut,MAX(LastLineIn) as LastLineIn,MAX(LastLineOut) as LastLineOut,VesselName,CallSign,IMONo,RegisteredName,VesselType,VesselNationality,BerthName,PortCode,PortName,GrossRegisteredTonnageInMT,DeadWeightTonnageInMT,LengthOverallInM,ReasonForVisit
          FROM (SELECT AN.VCN,LPC.PortName AS LastPortOfCall,NPC.PortName AS NextPortOfCall,AN.ArrDraft as ForwardDraft, AN.DepDraft as AfterDraft,SR.MovementDateTime,VCM.ATB,VCM.ATUB,MT.SubCatName as MovementTyp,SR.ServiceRequestID, RA.ServiceReferenceID,VC.PortLimitIn,VC.PortLimitOut,VC.BreakWaterIn,VC.BreakWaterOut,
                       Dense_rank ()
                       OVER (PARTITION BY ServiceReferenceID, ServiceTypeName ORDER BY RA.ResourceAllocationID) ResourceSno,
                       RA.ResourceAllocationID,
                       RA.ResourceID,
                       CM.CraftName,
					   CASE ra.OperationType
                                  WHEN 'TGWR'
                                  THEN
                                     CM.CraftName
                                  ELSE
                                     CONCAT (u.FirstName, ' ', u.LastName)
                               END
                                AS ResourceName,
                       RA.OperationType,
                       OT.ServiceTypeName,
                       RA.StartTime,
                       case when ra.OperationType = 'PILT' then psr.PilotOnBoard else NULL end PilotOnBoard,
                       case when ra.OperationType = 'PILT' then psr.PilotOff else NULL end PilotOff,
					   
                       case when ra.OperationType = 'BRTH' then SBTE.FirstLineIn else NULL end FirstLineIn,
                       case when ra.OperationType = 'BRTH' then SBTE.FirstLineOut else NULL end FirstLineOut,
                       case when ra.OperationType = 'BRTH' then SBTE.LastLineIn else NULL end LastLineIn,
                       case when ra.OperationType = 'BRTH' then SBTE.LastLineOut else NULL end LastLineOut,
                       
                       'StartTime' [Timings],
                       dbo.udf_GetArrivalReasonForVisit (AN.VCN) AS ReasonForVisit,
                       V.VesselName,V.IMONo,AGT.RegisteredName,VT.SubCatName as VesselType, VN.SubCatName as VesselNationality,B.BerthName,AN.PortCode,pm.PortName,
                       V.CallSign,V.GrossRegisteredTonnageInMT,V.DeadWeightTonnageInMT,V.LengthOverallInM
                  FROM ArrivalNotification AN
                       INNER JOIN Vessel V ON V.VesselID = AN.VesselID
                       INNER JOIN SubCategory VT ON VT.SubCatCode = V.VesselType
                       INNER JOIN SubCategory VN ON VN.SubCatCode = V.VesselNationality
                       INNER JOIN PortRegistry LPC ON LPC.PortCode = AN.LastPortOfCall
					             INNER JOIN PortRegistry NPC ON NPC.PortCode = AN.NextPortOfCall
                       INNER JOIN VesselCall VC ON VC.VCN = AN.VCN
					             INNER JOIN AGENT AGT ON AGT.AgentID =VC.RecentAgentID
                       INNER JOIN ServiceRequest SR ON (SR.VCN = AN.VCN AND SR.RecordStatus='A')
                       INNER JOIN SubCategory MT ON MT.SubCatCode = SR.MovementType
                       INNER JOIN VesselCallMovement VCM ON (VCM.ServiceRequestID = SR.ServiceRequestID AND VCM.RecordStatus='A')
                       INNER JOIN port pm ON pm.PortCode = AN.PortCode                       
                       INNER JOIN dbo.ResourceAllocation RA ON (RA.ServiceReferenceType = 'VTSR' AND RA.ServiceReferenceID = SR.SERVICEREQUESTID AND RA.RecordStatus = 'A' AND (   RA.TaskStatus = 'COMP' OR RA.TaskStatus = 'VERF' OR RA.TaskStatus = 'STRD'))
                       INNER JOIN dbo.ServiceType OT ON OT.ServiceTypeCode = RA.OperationType
                       INNER JOIN dbo.SubCategory c ON c.SubCatCode = RA.ServiceReferenceType
                       INNER JOIN dbo.SubCategory e ON e.SubCatCode = RA.TaskStatus
					   LEFT JOIN Berth b ON b.PortCode = VCM.ToPositionPortCode AND b.QuayCode = VCM.ToPositionQuayCode AND b.BerthCode = VCM.ToPositionBerthCode
                       LEFT JOIN dbo.Users u ON RA.ResourceID = u.UserID
                       LEFT JOIN PilotageServiceRecording PSR ON PSR.ResourceAllocationID = RA.ResourceAllocationID
                       LEFT JOIN ShiftingBerthingTaskExecution SBTE ON SBTE.ResourceAllocationID = RA.ResourceAllocationID
                       LEFT JOIN dbo.Craft CM ON CM.CraftID = RA.CraftID
                       Where (AN.VCN = @VCN OR @VCN IS NULL)  AND (VT.SubCatName = @VesselType OR @VesselType is NULL) AND AN.PortCode=@Port AND AN.RecordStatus='A' and RA.RecordStatus = 'A' 
                             AND case VCM.MovementType when 'SGMV' then VCM.ATUB else VCM.ATB END between @FromDate AND @ToDate
                       )
               AS SourceTable PIVOT (MAX ([StartTime])
                              FOR [OperationType]
                              IN ([BRTH], [PILT], [TGWR])) AS OTStart
			   Group by VCN,BerthName,PortCode,PortName,VesselName,CallSign,IMONo,RegisteredName,VesselType,VesselNationality,GrossRegisteredTonnageInMT,DeadWeightTonnageInMT,LengthOverallInM,LastPortOfCall,NextPortOfCall,ForwardDraft,AfterDraft,MovementDateTime,ATB,ATUB,MovementTyp,ServiceRequestID,ServiceReferenceID,ResourceSno,ResourceAllocationID,ResourceID,CraftName,ResourceName,ServiceTypeName,PortLimitIn,PortLimitOut,BreakWaterIn,BreakWaterOut,ReasonForVisit
        UNION ALL
        SELECT VCN,LastPortOfCall,NextPortOfCall,ForwardDraft,AfterDraft,MovementDateTime,ATB,ATUB,MovementTyp,ServiceRequestID, ServiceReferenceID,ResourceSno,ResourceAllocationID,ResourceID,CraftName,ResourceName,ServiceTypeName,PortLimitIn,PortLimitOut,BreakWaterIn,BreakWaterOut,null as Berth_StartTime, MAX([BRTH]) as Berth_EndTime,  null as Pilot_StartTime,MAX([PILT]) as Pilot_EndTime,null as Tug_StartTime, MAX([TGWR]) as Tug_EndTime, 
			   MAX(PilotOnBoard) as PilotOnBoard,MAX(PilotOff) as PilotOff,MAX(FirstLineIn) as FirstLineIn,MAX(FirstLineOut) as FirstLineOut,MAX(LastLineIn) as LastLineIn,MAX(LastLineOut) as LastLineOut,VesselName,CallSign,IMONo,RegisteredName,VesselType,VesselNationality,BerthName,PortCode,PortName,GrossRegisteredTonnageInMT,DeadWeightTonnageInMT,LengthOverallInM,ReasonForVisit
          FROM (SELECT AN.VCN,LPC.PortName AS LastPortOfCall,NPC.PortName AS NextPortOfCall,AN.ArrDraft as ForwardDraft, AN.DepDraft as AfterDraft,SR.MovementDateTime,VCM.ATB,VCM.ATUB, MT.SubCatName as MovementTyp,SR.ServiceRequestID,RA.ServiceReferenceID,VC.PortLimitIn,VC.PortLimitOut,VC.BreakWaterIn,VC.BreakWaterOut,
                       Dense_rank ()
                       OVER (PARTITION BY ServiceReferenceID, ServiceTypeName ORDER BY RA.ResourceAllocationID) ResourceSno,
                       RA.ResourceAllocationID,
                       RA.ResourceID,
                       CM.CraftName,
					   CASE ra.OperationType
                                  WHEN 'TGWR'
                                  THEN
                                     CM.CraftName
                                  ELSE
                                     CONCAT (u.FirstName, ' ', u.LastName)
                               END
                                AS ResourceName,
                       RA.OperationType,
                       OT.ServiceTypeName,
                       RA.EndTime,
                       case when ra.OperationType = 'PILT' then psr.PilotOnBoard else NULL end PilotOnBoard,
					             case when ra.OperationType = 'PILT' then psr.PilotOff else NULL end PilotOff,
                       
                       case when ra.OperationType = 'BRTH' then SBTE.FirstLineIn else NULL end FirstLineIn,
                       case when ra.OperationType = 'BRTH' then SBTE.FirstLineOut else NULL end FirstLineOut,
                       case when ra.OperationType = 'BRTH' then SBTE.LastLineIn else NULL end LastLineIn,
                       case when ra.OperationType = 'BRTH' then SBTE.LastLineOut else NULL end LastLineOut,
                       
                       'EndTime' [Timings],
                       dbo.udf_GetArrivalReasonForVisit (AN.VCN) AS ReasonForVisit,
                       V.VesselName,V.IMONo,AGT.RegisteredName,VT.SubCatName as VesselType, VN.SubCatName as VesselNationality,B.BerthName,AN.PortCode,pm.PortName,
                       V.CallSign,V.GrossRegisteredTonnageInMT,V.DeadWeightTonnageInMT,V.LengthOverallInM
                  FROM ArrivalNotification AN
                       INNER JOIN Vessel V ON V.VesselID = AN.VesselID
                       INNER JOIN SubCategory VT ON VT.SubCatCode = V.VesselType
                       INNER JOIN SubCategory VN ON VN.SubCatCode = V.VesselNationality
                       INNER JOIN PortRegistry LPC ON LPC.PortCode = AN.LastPortOfCall
					             INNER JOIN PortRegistry NPC ON NPC.PortCode = AN.NextPortOfCall
                       INNER JOIN VesselCall VC ON VC.VCN = AN.VCN
                       INNER JOIN AGENT AGT ON AGT.AgentID =VC.RecentAgentID
                       INNER JOIN ServiceRequest SR ON SR.VCN = AN.VCN
                       INNER JOIN SubCategory MT ON MT.SubCatCode = SR.MovementType
                       INNER JOIN VesselCallMovement VCM ON (VCM.ServiceRequestID = SR.ServiceRequestID AND VCM.RecordStatus='A')
                       INNER JOIN port pm ON pm.PortCode = AN.PortCode                       
                       INNER JOIN dbo.ResourceAllocation RA ON (RA.ServiceReferenceType = 'VTSR' AND RA.ServiceReferenceID = SR.SERVICEREQUESTID AND RA.RecordStatus = 'A' AND (   RA.TaskStatus = 'COMP' OR RA.TaskStatus = 'VERF' OR RA.TaskStatus = 'STRD'))
                       INNER JOIN dbo.ServiceType OT ON OT.ServiceTypeCode = RA.OperationType
                       INNER JOIN dbo.SubCategory c ON c.SubCatCode = RA.ServiceReferenceType
                       INNER JOIN dbo.SubCategory e ON e.SubCatCode = RA.TaskStatus
					   LEFT JOIN Berth b ON b.PortCode = VCM.ToPositionPortCode AND b.QuayCode = VCM.ToPositionQuayCode AND b.BerthCode = VCM.ToPositionBerthCode
                       LEFT JOIN dbo.Users u ON RA.ResourceID = u.UserID
                       LEFT JOIN PilotageServiceRecording PSR ON PSR.ResourceAllocationID = RA.ResourceAllocationID
                       LEFT JOIN ShiftingBerthingTaskExecution SBTE ON SBTE.ResourceAllocationID = RA.ResourceAllocationID
                       LEFT JOIN dbo.Craft CM ON CM.CraftID = RA.CraftID
                       Where (AN.VCN = @VCN OR @VCN IS NULL)  AND (VT.SubCatName = @VesselType OR @VesselType is NULL) AND AN.PortCode=@Port AND AN.RecordStatus='A' and RA.RecordStatus = 'A'
                             AND case VCM.MovementType when 'SGMV' then VCM.ATUB else VCM.ATB END between @FromDate AND @ToDate
                       )
               AS SourceTable PIVOT (MAX ([EndTime])
                              FOR [OperationType]
                              IN ([BRTH], [PILT], [TGWR])) AS P2
				Group by VCN,BerthName,PortCode,PortName,VesselName,CallSign,IMONo,RegisteredName,VesselType,VesselNationality,GrossRegisteredTonnageInMT,DeadWeightTonnageInMT,LengthOverallInM,LastPortOfCall,NextPortOfCall,ForwardDraft,AfterDraft,MovementDateTime,ATB,ATUB,MovementTyp,ServiceRequestID,ServiceReferenceID,ResourceSno,ResourceAllocationID,ResourceID,CraftName,ResourceName,ServiceTypeName,PortLimitIn,PortLimitOut,BreakWaterIn,BreakWaterOut,ReasonForVisit
         ) Rec
  Group by Rec.VCN,Rec.LastPortOfCall,REC.NextPortOfCall,rec.BerthName,rec.PortCode,rec.PortName,rec.VesselName,rec.CallSign,rec.IMONo,rec.RegisteredName,rec.VesselType,rec.VesselNationality,rec.ReasonForVisit,rec.GrossRegisteredTonnageInMT,rec.DeadWeightTonnageInMT,rec.LengthOverallInM,REC.LastPortOfCall,Rec.NextPortOfCall,Rec.ForwardDraft,Rec.AfterDraft,Rec.MovementDateTime,Rec.ATB,REC.ATUB,Rec.MovementTyp,rec.ServiceRequestID,Rec.ServiceReferenceID,PortLimitIn,PortLimitOut,BreakWaterIn,BreakWaterOut,Rec.ResourceSno,
       Rec.ResourceAllocationID,Rec.ResourceID,
       Rec.ResourceName,Rec.ServiceTypeName,Rec.CraftName,ReasonForVisit
ORDER BY VCN,LastPortOfCall,NextPortOfCall,ServiceRequestID,ServiceReferenceID, ServiceTypeName, ResourceSno
END

SET @AllColumns =N''
------Resource Names Table
begin
SET @Columns =N''
select @Columns = STUFF (
            (SELECT DISTINCT ',' + QUOTENAME (ServiceTypeName + '_'+ CONVERT (VARCHAR (MAX), ResourceSno))
             FROM (SELECT ServiceRequestID,
                          ResourceSno,
                          ResourceName,
                          MovementTyp,
                          ServiceTypeName
                   FROM #Recordings REC 
                   GROUP BY VCN,VesselName,CallSign,ReasonForVisit,GrossRegisteredTonnageInMT,DeadWeightTonnageInMT,LengthOverallInM,BerthName,PortCode,PortName,IMONo,RegisteredName,VesselType,VesselNationality,ServiceRequestID, ServiceTypeName, ResourceSno, MovementTyp, ResourceName, TugName
                   ) As SourceTable
           FOR XML PATH ( '' ) , TYPE).value ('.', 'nvarchar(max)'),1,1,'')

SET @AllColumns='RN.' + replace(@Columns,',',',RN.');
--print @AllColumns

SET @query = N'SELECT VCN,CallSign,VesselName,ReasonForVisit,GrossRegisteredTonnageInMT,DeadWeightTonnageInMT,LengthOverallInM,BerthName,PortCode,PortName,IMONo,RegisteredName,VesselType,VesselNationality,PortLimitIn,PortLimitOut,BreakWaterIn,BreakWaterOut,LastPortOfCall,NextPortOfCall,ForwardDraft,AfterDraft,ServiceRequestID,MovementDateTime,ATB,ATUB,MovementTyp,' + @Columns +' from 
         (SELECT VCN,VesselName,CallSign,ReasonForVisit,GrossRegisteredTonnageInMT,DeadWeightTonnageInMT,LengthOverallInM,BerthName,PortCode,PortName,IMONo,RegisteredName,VesselType,VesselNationality,PortLimitIn,PortLimitOut,BreakWaterIn,BreakWaterOut,LastPortOfCall,NextPortOfCall,ForwardDraft,AfterDraft,ServiceRequestID,MovementDateTime,ATB,ATUB,[MovementTyp],([ServiceTypeName]+''_''+CONVERT(varchar(max), ResourceSno)) as [OperationType],
                 [ResourceName]
                 FROM (select ResourceSno,VCN,VesselName,CallSign,ReasonForVisit,GrossRegisteredTonnageInMT,DeadWeightTonnageInMT,LengthOverallInM,BerthName,PortCode,PortName,IMONo,RegisteredName,VesselType,VesselNationality,PortLimitIn,PortLimitOut,BreakWaterIn,BreakWaterOut,LastPortOfCall,NextPortOfCall,ForwardDraft,AfterDraft,ServiceRequestID,MovementDateTime,ATB,ATUB,case when ServiceTypeName =''Tug Boats/Work Boat'' then TugName else ResourceName end as ResourceName,MovementTyp, ServiceTypeName
                       from #Recordings 
                       group by VCN,VesselName,CallSign,ReasonForVisit,GrossRegisteredTonnageInMT,DeadWeightTonnageInMT,LengthOverallInM,BerthName,PortCode,PortName,IMONo,RegisteredName,VesselType,VesselNationality,PortLimitIn,PortLimitOut,BreakWaterIn,BreakWaterOut,LastPortOfCall,NextPortOfCall,ForwardDraft,AfterDraft,ServiceRequestID,MovementDateTime,ATB,ATUB,ServiceTypeName,MovementTyp,ResourceSno,ResourceName,TugName
                       ) A
         ) x pivot (max([ResourceName]) for [OperationType] in ('+@Columns+')) RES' 


--select * From #recordings
--print @query
--EXECUTE (@query);
Set @DynaQry=N''
set @DynaQry = '(' + Replace(@query,'  ',' ') + ') RN '
END

------BerthMaster Start Time
Begin
SET @Columns =N''
select @Columns = STUFF (
            (SELECT DISTINCT ',' + QUOTENAME ('Berth_StartTime' + '_'+ CONVERT (VARCHAR (MAX), ResourceSno))
             FROM (SELECT ServiceRequestID,
                          ResourceSno,
                          ResourceName,
                          MovementTyp
                   FROM #Recordings Where ServiceTypeName='Berth Master' 
                   GROUP BY VCN,VesselName, ServiceRequestID, ServiceTypeName, ResourceSno, MovementTyp, ResourceName, TugName
                   ) As SourceTable
           FOR XML PATH ( '' ) , TYPE).value ('.', 'nvarchar(max)'),1,1,'')

SET @AllColumns=@AllColumns +',BMST.'+ replace(@Columns,',',',BMST.');

SET @query = N'SELECT VCN,VesselName, ServiceRequestID,MovementTyp, '+ @Columns + ' from 
         (SELECT VCN,VesselName, ServiceRequestID, [MovementTyp],(''Berth_StartTime''+''_''+CONVERT(varchar(max), ResourceSno)) as [BMStime],
                 Berth_StartTime
                 FROM (select VCN,VesselName, ServiceRequestID,ResourceSno,ResourceName,MovementTyp, ServiceTypeName,
                        Berth_StartTime
                       from #Recordings
                       ) B
         ) x pivot (max([Berth_StartTime]) for [BMStime] in ('+@Columns+')) BMS'
set @DynaQry = @DynaQry + ' INNER JOIN ('+ Replace(@query,'  ',' ') + ') BMST on RN.VCN=BMST.VCN and RN.ServiceRequestID=BMST.ServiceRequestID and RN.MovementTyp=BMST.MovementTyp '
END

------BerthMaster End Time
Begin
SET @Columns =N''
select @Columns = STUFF (
            (SELECT DISTINCT ',' + QUOTENAME ('Berth_EndTime' + '_'+ CONVERT (VARCHAR (MAX), ResourceSno))
             FROM (SELECT ServiceRequestID,
                          ResourceSno,
                          ResourceName,
                          MovementTyp
                   FROM #Recordings Where ServiceTypeName='Berth Master' 
                   GROUP BY VCN,VesselName, ServiceRequestID, ServiceTypeName, ResourceSno, MovementTyp, ResourceName, TugName
                   ) As SourceTable
           FOR XML PATH ( '' ) , TYPE).value ('.', 'nvarchar(max)'),1,1,'')

SET @AllColumns=@AllColumns +',BME.'+ replace(@Columns,',',',BME.');

SET @query = N'SELECT VCN,VesselName, ServiceRequestID,MovementTyp, '+ @Columns+ ' from 
         (SELECT VCN,VesselName, ServiceRequestID,[MovementTyp],(''Berth_EndTime''+''_''+CONVERT(varchar(max), ResourceSno)) as [BMEtime],
                 Berth_EndTime
                 FROM (select VCN,VesselName, ServiceRequestID,ResourceSno,ResourceName,MovementTyp, ServiceTypeName,Berth_EndTime 
                       from #Recordings 
                       ) C
          ) Source pivot (max([Berth_EndTime]) for [BMEtime] in ('+@Columns+')) BM'
set @DynaQry = @DynaQry + ' INNER JOIN ('+ Replace(@query,'  ',' ') + ') BME on RN.VCN=BME.VCN and RN.ServiceRequestID=BME.ServiceRequestID and RN.MovementTyp=BME.MovementTyp '

END

------FirstLineIn
Begin
SET @Columns =N''
select @Columns = STUFF (
            (SELECT DISTINCT ',' + QUOTENAME ('FirstLineIn' + '_'+ CONVERT (VARCHAR (MAX), ResourceSno))
             FROM (SELECT ServiceRequestID,
                          ResourceSno,
                          ResourceName,
                          MovementTyp
                   FROM #Recordings Where ServiceTypeName='Berth Master' GROUP BY VCN,VesselName, ServiceRequestID, ServiceTypeName, ResourceSno, MovementTyp, ResourceName, TugName
                   ) As SourceTable
           FOR XML PATH ( '' ) , TYPE).value ('.', 'nvarchar(max)'),1,1,'')

SET @AllColumns=@AllColumns +',FI.'+ replace(@Columns,',',',FI.');

SET @query = N'SELECT VCN,VesselName, ServiceRequestID,MovementTyp, '+ @Columns+ ' from 
         (SELECT VCN,VesselName, ServiceRequestID,[MovementTyp],(''FirstLineIn''+''_''+CONVERT(varchar(max), ResourceSno)) as [FLI],
                 FirstLineIn
                 FROM (select VCN,VesselName, ServiceRequestID,ResourceSno,ResourceName,MovementTyp, ServiceTypeName,FirstLineIn 
                       from #Recordings 
                       ) D
          ) Source pivot (max([FirstLineIn]) for [FLI] in ('+@Columns+')) FLI'
set @DynaQry = @DynaQry + ' INNER JOIN ('+ Replace(@query,'  ',' ') + ') FI on RN.VCN=FI.VCN and RN.ServiceRequestID=FI.ServiceRequestID and RN.MovementTyp=FI.MovementTyp '
END

------FirstLineOut
Begin
SET @Columns =N''
select @Columns = STUFF (
            (SELECT DISTINCT ',' + QUOTENAME ('FirstLineOut' + '_'+ CONVERT (VARCHAR (MAX), ResourceSno))
             FROM (SELECT ServiceRequestID,
                          ResourceSno,
                          ResourceName,
                          MovementTyp
                   FROM #Recordings Where ServiceTypeName='Berth Master' 
                   GROUP BY VCN,VesselName, ServiceRequestID, ServiceTypeName, ResourceSno, MovementTyp, ResourceName, TugName
                   ) As SourceTable
           FOR XML PATH ( '' ) , TYPE).value ('.', 'nvarchar(max)'),1,1,'')

SET @AllColumns=@AllColumns +',FO.'+ replace(@Columns,',',',FO.');
SET @query = N'SELECT VCN,VesselName, ServiceRequestID,MovementTyp, '+ @Columns+ ' from 
         (SELECT VCN,VesselName, ServiceRequestID,[MovementTyp],(''FirstLineOut''+''_''+CONVERT(varchar(max), ResourceSno)) as [FLO],
                 FirstLineOut
                 FROM (select VCN,VesselName, ServiceRequestID,ResourceSno,ResourceName,MovementTyp, ServiceTypeName,FirstLineOut 
                       from #Recordings 
                       ) E
          ) Source pivot (max([FirstLineOut]) for [FLO] in ('+@Columns+')) FLO'
set @DynaQry = @DynaQry + ' INNER JOIN ('+ Replace(@query,'  ',' ') + ') FO on RN.VCN=FO.VCN and RN.ServiceRequestID=FO.ServiceRequestID and RN.MovementTyp=FO.MovementTyp '

END

------LastLineIn
Begin
SET @Columns =N''
select @Columns = STUFF (
            (SELECT DISTINCT ',' + QUOTENAME ('LastLineIn' + '_'+ CONVERT (VARCHAR (MAX), ResourceSno))
             FROM (SELECT ServiceRequestID,
                          ResourceSno,
                          ResourceName,
                          MovementTyp
                   FROM #Recordings Where ServiceTypeName='Berth Master' 
                   GROUP BY VCN,VesselName, ServiceRequestID, ServiceTypeName, ResourceSno, MovementTyp, ResourceName, TugName
                   ) As SourceTable
           FOR XML PATH ( '' ) , TYPE).value ('.', 'nvarchar(max)'),1,1,'')

SET @AllColumns=@AllColumns +',LI.'+ replace(@Columns,',',',LI.');

SET @query = N'SELECT VCN,VesselName, ServiceRequestID,MovementTyp, '+ @Columns+ ' from 
         (SELECT VCN,VesselName, ServiceRequestID,[MovementTyp],(''LastLineIn''+''_''+CONVERT(varchar(max), ResourceSno)) as [LLI],
                 LastLineIn
                 FROM (select ResourceSno,VCN,VesselName, ServiceRequestID,ResourceName,MovementTyp, ServiceTypeName,LastLineIn 
                       from #Recordings 
                       ) F
          ) Source pivot (max([LastLineIn]) for [LLI] in ('+@Columns+')) LLI'
set @DynaQry = @DynaQry + ' INNER JOIN ('+ Replace(@query,'  ',' ') + ') LI on RN.VCN=LI.VCN and RN.ServiceRequestID=LI.ServiceRequestID and RN.MovementTyp=LI.MovementTyp '

END

------LastLineOut
Begin
SET @Columns =N''
select @Columns = STUFF (
            (SELECT DISTINCT ',' + QUOTENAME ('LastLineOut' + '_'+ CONVERT (VARCHAR (MAX), ResourceSno))
             FROM (SELECT ServiceRequestID,
                          ResourceSno,
                          ResourceName,
                          MovementTyp
                   FROM #Recordings Where ServiceTypeName='Berth Master' 
                   GROUP BY VCN,VesselName, ServiceRequestID, ServiceTypeName, ResourceSno, MovementTyp, ResourceName, TugName
                   ) As SourceTable
           FOR XML PATH ( '' ) , TYPE).value ('.', 'nvarchar(max)'),1,1,'')

SET @AllColumns=@AllColumns +',LO.'+ replace(@Columns,',',',LO.');

SET @query = N'SELECT VCN,VesselName, ServiceRequestID,MovementTyp, '+ @Columns+ ' from 
         (SELECT VCN,VesselName, ServiceRequestID,[MovementTyp],(''LastLineOut''+''_''+CONVERT(varchar(max), ResourceSno)) as [LLO],
                 LastLineOut
                 FROM (select VCN,VesselName, ServiceRequestID,ResourceSno,ResourceName,MovementTyp, ServiceTypeName,LastLineOut 
                       from #Recordings 
                       ) G
          ) Source pivot (max([LastLineOut]) for [LLO] in ('+@Columns+')) LLO'
set @DynaQry = @DynaQry + ' INNER JOIN ('+ Replace(@query,'  ',' ') + ') LO on RN.VCN=LO.VCN and RN.ServiceRequestID=LO.ServiceRequestID and RN.MovementTyp=LO.MovementTyp  '

END

------Pilot Start Time
Begin
SET @Columns =N''
select @Columns = STUFF (
            (SELECT DISTINCT ',' + QUOTENAME ('Pilot_StartTime' + '_'+ CONVERT (VARCHAR (MAX), ResourceSno))
             FROM (SELECT ServiceRequestID,
                          ResourceSno,
                          ResourceName,
                          MovementTyp
                   FROM #Recordings Where ServiceTypeName='Pilot' 
                   GROUP BY VCN,VesselName, ServiceRequestID, ServiceTypeName, ResourceSno, MovementTyp, ResourceName, TugName
                   ) As SourceTable
           FOR XML PATH ( '' ) , TYPE).value ('.', 'nvarchar(max)'),1,1,'')

SET @AllColumns=@AllColumns +',PST.'+ replace(@Columns,',',',PST.');

SET @query = N'SELECT VCN,VesselName, ServiceRequestID,MovementTyp, '+ @Columns + ' from 
         (SELECT VCN,VesselName, ServiceRequestID, [MovementTyp],(''Pilot_StartTime''+''_''+CONVERT(varchar(max), ResourceSno)) as [PStime],
                 Pilot_StartTime
                 FROM (select VCN,VesselName, ServiceRequestID,ResourceSno,ResourceName,MovementTyp, ServiceTypeName,
                        Pilot_StartTime
                       from #Recordings
                       ) H
         ) x pivot (max([Pilot_StartTime]) for [PStime] in ('+@Columns+')) PS'
set @DynaQry = @DynaQry + ' INNER JOIN ('+ Replace(@query,'  ',' ') + ') PST on RN.VCN=PST.VCN and RN.ServiceRequestID=PST.ServiceRequestID and RN.MovementTyp=PST.MovementTyp '

END

------Pilot End Time
Begin
SET @Columns =N''
select @Columns = STUFF (
            (SELECT DISTINCT ',' + QUOTENAME ('Pilot_EndTime' + '_'+ CONVERT (VARCHAR (MAX), ResourceSno))
             FROM (SELECT ServiceRequestID,
                          ResourceSno,
                          ResourceName,
                          MovementTyp
                   FROM #Recordings Where ServiceTypeName='Pilot' 
                   GROUP BY VCN,VesselName, ServiceRequestID, ServiceTypeName, ResourceSno, MovementTyp, ResourceName, TugName
                   ) As SourceTable
           FOR XML PATH ( '' ) , TYPE).value ('.', 'nvarchar(max)'),1,1,'')

SET @AllColumns=@AllColumns +',PET.'+ replace(@Columns,',',',PET.');

SET @query = N'SELECT VCN,VesselName, ServiceRequestID,MovementTyp, '+ @Columns+ ' from 
         (SELECT VCN,VesselName, ServiceRequestID,[MovementTyp],(''Pilot_EndTime''+''_''+CONVERT(varchar(max), ResourceSno)) as [PEtime],
                 Pilot_EndTime
                 FROM (select VCN,VesselName, ServiceRequestID,ResourceSno,ResourceName,MovementTyp, ServiceTypeName,Pilot_EndTime 
                       from #Recordings 
                       ) I
          ) Source pivot (max([Pilot_EndTime]) for [PEtime] in ('+@Columns+')) PE'
set @DynaQry = @DynaQry + ' INNER JOIN ('+ @query + ') PET on RN.VCN=PET.VCN and RN.ServiceRequestID=PET.ServiceRequestID and RN.MovementTyp=PET.MovementTyp '

END

------PilotOnBoard
Begin
SET @Columns =N''
select @Columns = STUFF (
            (SELECT DISTINCT ',' + QUOTENAME ('PilotOnBoard' + '_'+ CONVERT (VARCHAR (MAX), ResourceSno))
             FROM (SELECT ServiceRequestID,
                          ResourceSno,
                          ResourceName,
                          MovementTyp
                   FROM #Recordings Where ServiceTypeName='Pilot' 
                   GROUP BY VCN,VesselName, ServiceRequestID, ServiceTypeName, ResourceSno, MovementTyp, ResourceName, TugName
                   ) As SourceTable
           FOR XML PATH ( '' ) , TYPE).value ('.', 'nvarchar(max)'),1,1,'')

SET @AllColumns=@AllColumns +',PON.'+ replace(@Columns,',',',PON.');

SET @query = N'SELECT VCN,VesselName, ServiceRequestID,MovementTyp, '+ @Columns+ ' from 
         (SELECT VCN,VesselName, ServiceRequestID,[MovementTyp],(''PilotOnBoard''+''_''+CONVERT(varchar(max), ResourceSno)) as [POBtime],
                 PilotOnBoard
                 FROM (select VCN,VesselName, ServiceRequestID,ResourceSno,ResourceName,MovementTyp, ServiceTypeName,PilotOnBoard 
                       from #Recordings 
                       ) J
          ) Source pivot (max([PilotOnBoard]) for [POBtime] in ('+@Columns+')) POB'
set @DynaQry = @DynaQry + ' INNER JOIN ('+ Replace(@query,'  ',' ') + ') PON on RN.VCN=PON.VCN and RN.ServiceRequestID=PON.ServiceRequestID and RN.MovementTyp=PON.MovementTyp '
END

------PilotOff
Begin
SET @Columns =N''
select @Columns = STUFF (
            (SELECT DISTINCT ',' + QUOTENAME ('PilotOff' + '_'+ CONVERT (VARCHAR (MAX), ResourceSno))
             FROM (SELECT ServiceRequestID,
                          ResourceSno,
                          ResourceName,
                          MovementTyp
                   FROM #Recordings Where ServiceTypeName='Pilot' 
                   GROUP BY VCN,VesselName, ServiceRequestID, ServiceTypeName, ResourceSno, MovementTyp, ResourceName, TugName
                   ) As SourceTable
           FOR XML PATH ( '' ) , TYPE).value ('.', 'nvarchar(max)'),1,1,'')

SET @AllColumns=@AllColumns +',POF.'+ replace(@Columns,',',',POF.');

SET @query = N'SELECT VCN,VesselName, ServiceRequestID,MovementTyp, '+ @Columns+ ' from 
         (SELECT VCN,VesselName, ServiceRequestID,[MovementTyp],(''PilotOff''+''_''+CONVERT(varchar(max), ResourceSno)) as [POfftime],
                 PilotOff
                 FROM (select VCN,VesselName, ServiceRequestID,ResourceSno,ResourceName,MovementTyp, ServiceTypeName,PilotOff 
                       from #Recordings 
                       ) K
          ) Source pivot (max([PilotOff]) for [POfftime] in ('+@Columns+')) POB'
set @DynaQry = @DynaQry + ' INNER JOIN ('+ Replace(@query,'  ',' ') + ') POF on RN.VCN=POF.VCN and RN.ServiceRequestID=POF.ServiceRequestID and RN.MovementTyp=POF.MovementTyp '
END

------Tug Names Table
begin
SET @Columns =N''
select @Columns = STUFF (
            (SELECT DISTINCT ',' + QUOTENAME (ServiceTypeName + '_'+ CONVERT (VARCHAR (MAX), ResourceSno))
             FROM (SELECT ServiceRequestID,
                          ResourceSno,
                          ResourceName,
                          MovementTyp,
                          ServiceTypeName
                   FROM #Recordings REC where ServiceTypeName='Tug Boats/Work Boat' 
                   GROUP BY VCN,VesselName, ServiceRequestID, ServiceTypeName, ResourceSno, MovementTyp, ResourceName, TugName
                   ) As SourceTable
           FOR XML PATH ( '' ) , TYPE).value ('.', 'nvarchar(max)'),1,1,'')

SET @query = N'SELECT VCN,VesselName, ServiceRequestID,MovementTyp, '+ @Columns +' from 
         (SELECT VCN,VesselName, ServiceRequestID, [MovementTyp],([ServiceTypeName]+''_''+CONVERT(varchar(max), ResourceSno)) as [TugRes],
                 [TugName]
                 FROM (select VCN,VesselName, ServiceRequestID,ResourceSno,ResourceName,MovementTyp, ServiceTypeName,TugName
                       from #Recordings 
                       group by VCN,VesselName, ServiceRequestID,ServiceTypeName,ResourceSno,MovementTyp,ResourceName,TugName
                       ) L
         ) x pivot (max([TugName]) for [TugRes] in ('+@Columns+')) TUG' 


set @DynaQry = @DynaQry + ' INNER JOIN ('+ Replace(@query,'  ',' ') + ') TUG on RN.VCN=TUG.VCN and RN.ServiceRequestID=TUG.ServiceRequestID and RN.MovementTyp=TUG.MovementTyp '

END

------Tug Start Time
Begin
SET @Columns =N''
select @Columns = STUFF (
            (SELECT DISTINCT ',' + QUOTENAME ('Tug_StartTime' + '_'+ CONVERT (VARCHAR (MAX), ResourceSno))
             FROM (SELECT ServiceRequestID,
                          ResourceSno,
                          ResourceName,
                          MovementTyp
                   FROM #Recordings Where ServiceTypeName='Tug Boats/Work Boat' 
                   GROUP BY VCN,VesselName, ServiceRequestID, ServiceTypeName, ResourceSno, MovementTyp, ResourceName, TugName
                   ) As SourceTable
           FOR XML PATH ( '' ) , TYPE).value ('.', 'nvarchar(max)'),1,1,'')

SET @AllColumns=@AllColumns +',TST.'+ replace(@Columns,',',',TST.');

SET @query = N'SELECT VCN,VesselName, ServiceRequestID,MovementTyp, '+ @Columns + ' from 
         (SELECT VCN,VesselName, ServiceRequestID, [MovementTyp],(''Tug_StartTime''+''_''+CONVERT(varchar(max), ResourceSno)) as [TStime],
                 Tug_StartTime
                 FROM (select VCN,VesselName, ServiceRequestID,ResourceSno,ResourceName,MovementTyp, ServiceTypeName,
                        Tug_StartTime
                       from #Recordings
                       ) M
         ) x pivot (max([Tug_StartTime]) for [TStime] in ('+@Columns+')) TS'
set @DynaQry = @DynaQry + ' INNER JOIN ('+ Replace(@query,'  ',' ') + ') TST on RN.VCN=TST.VCN and RN.ServiceRequestID=TST.ServiceRequestID and RN.MovementTyp=TST.MovementTyp '

END

------Tug End Time
Begin
SET @Columns =N''
select @Columns = STUFF (
            (SELECT DISTINCT ',' + QUOTENAME ('Tug_EndTime' + '_'+ CONVERT (VARCHAR (MAX), ResourceSno))
             FROM (SELECT ServiceRequestID,
                          ResourceSno,
                          ResourceName,
                          MovementTyp
                   FROM #Recordings Where ServiceTypeName='Tug Boats/Work Boat' 
                   GROUP BY VCN,VesselName, ServiceRequestID, ServiceTypeName, ResourceSno, MovementTyp, ResourceName, TugName
                   ) As SourceTable
           FOR XML PATH ( '' ) , TYPE).value ('.', 'nvarchar(max)'),1,1,'')

SET @AllColumns=@AllColumns +',TET.'+ replace(@Columns,',',',TET.');

SET @query = N'SELECT VCN,VesselName, ServiceRequestID,MovementTyp, '+ @Columns+ ' from 
         (SELECT VCN,VesselName, ServiceRequestID,[MovementTyp],(''Tug_EndTime''+''_''+CONVERT(varchar(max), ResourceSno)) as [TEtime],
                 Tug_EndTime
                 FROM (select VCN,VesselName, ServiceRequestID,ResourceSno,ResourceName,MovementTyp, ServiceTypeName,Tug_EndTime 
                       from #Recordings 
                       ) N
          ) Source pivot (max([Tug_EndTime]) for [TEtime] in ('+@Columns+')) TE'
set @DynaQry = @DynaQry + ' INNER JOIN ('+ Replace(@query,'  ',' ') + ') TET on RN.VCN=TET.VCN and RN.ServiceRequestID=TET.ServiceRequestID and RN.MovementTyp=TET.MovementTyp '
END

--##Removing Duplicate columns in providing string
begin
DECLARE @word varchar(MAX) = @AllColumns;

WITH Splitter AS
(
    SELECT 1 N, LEFT(@word,CHARINDEX(',',@word,1)-1) Word, SUBSTRING(@word, CHARINDEX(',', @word, 0)+1, LEN(@word)) Rest
    UNION ALL 
    SELECT N+1 N,
           CASE WHEN CHARINDEX(',', Rest, 0)>0 THEN LEFT(Rest, CHARINDEX(',', Rest, 0)-1) ELSE Rest END,
           CASE WHEN CHARINDEX(',', Rest, 0)>0 THEN SUBSTRING(Rest, CHARINDEX(',', Rest, 0)+1, LEN(Rest)) ELSE NULL END
    FROM Splitter
    WHERE LEN(Rest)>0
), Numbered AS
(
    SELECT N, Word, ROW_NUMBER() OVER (PARTITION BY Word ORDER BY N) RowNum
    FROM Splitter
)
select @AllColumns = STUFF((SELECT ','+Word
              FROM Numbered
              WHERE RowNum=1
              ORDER BY N
              FOR XML PATH('')), 1, 1, '')
end
----------#####

set @DynaQry= 'SELECT Dense_rank () OVER (PARTITION BY RN.VCN ORDER BY RN.ServiceRequestID) as SNO,RN.VCN,RN.VesselName,RN.CallSign,RN.GrossRegisteredTonnageInMT,RN.DeadWeightTonnageInMT,RN.LengthOverallInM,RN.BerthName,RN.ReasonForVisit,RN.PortCode,RN.PortName,RN.IMONo,RN.RegisteredName,RN.VesselType,RN.VesselNationality,RN.PortLimitIn,RN.PortLimitOut,RN.BreakWaterIn,RN.BreakWaterOut,RN.LastPortOfCall,RN.NextPortOfCall,RN.ForwardDraft,RN.AfterDraft,RN.ServiceRequestID,RN.MovementDateTime,RN.ATB,RN.ATUB,RN.MovementTyp,'+ @AllColumns+' from ('+ Replace(@DynaQry,'  ',' ') +')'

BEGIN TRY

    set @currdate = getdate();

	  IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(@TempTable) AND type in (N'U')) 
      BEGIN
      EXECUTE ('DROP TABLE ['+@TempTable+']')
      END
      
    ------NOTE: Replacing "from ((" with INTO "from ((" to create Temporary Physical table for CMR
    set @DynaQry = replace(@DynaQry,' from ((',' INTO '+@TempTable+' from ((') 
    EXECUTE (@DynaQry)

    ---------Inserting Shifted from Berth details
    BEGIN
     set @query ='insert into '+@TempTable+' ('+ (select STUFF((select distinct ',[' + COLUMN_NAME + '] ' from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME =''+@TempTable+'' and COLUMN_NAME not in ('SNO','BerthName') FOR XML PATH('')),1,1,'')) + ',SNO,BerthName) 
           (select '+(select STUFF((select distinct ',[' + COLUMN_NAME + '] ' from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME =''+@TempTable+''
          and COLUMN_NAME not in ('SNO','BerthName') FOR XML PATH('')),1,1,'')) +',0 SNO,COALESCE ((SELECT ShiftedFromBerth FROM udf_get_PreviousBerthDetails (ServiceRequestID)),'''') AS ShiftedFromBerth from '+ @TempTable+' where [Berth Master_1] is NOT NULL and MovementTyp=''Shifting'')'
          execute (@query);
    END

	
END TRY
BEGIN CATCH
   select @errmsg =N'ErrorMessage = '+ ERROR_MESSAGE ()+ ' Query : '+@DynaQry
END CATCH
------------------

---------Sync to @TargetTable table from all temporary tables
BEGIN
SET @TargetTable = 'RPT_CompleteMovement';
SET @DynaQry =
                  (SELECT STUFF (
                             (SELECT DISTINCT
                                       ',C1.['+ column_name + '] =  C2.'+ '['+ column_name + ']'
                                FROM INFORMATION_SCHEMA.COLUMNS
                               WHERE     TABLE_NAME = '' + @TempTable + ''
                                     AND column_name IN (SELECT column_name
                                                           FROM INFORMATION_SCHEMA.COLUMNS
                                                          WHERE TABLE_NAME = ''
                                                                   + @TargetTable
                                                                   + '')
                              FOR XML PATH ( '' )),1,1,''))
                + ','
                + (SELECT STUFF (
                             (SELECT DISTINCT
                                     ',C1.[' + column_name + '] =  null'
                                FROM INFORMATION_SCHEMA.COLUMNS
                               WHERE     TABLE_NAME = '' + @TargetTable + ''
                                     AND column_name NOT IN (SELECT column_name
                                                               FROM INFORMATION_SCHEMA.COLUMNS
                                                              WHERE TABLE_NAME =
                                                                         ''+ @TempTable
                                                                       + '')
                              FOR XML PATH ( '' )),1,1,''))
         SET @DynaQry =
                  'MERGE INTO '
                + @TargetTable
                + ' AS C1 USING '
                + @TempTable
                + ' AS C2 ON C1.VCN COLLATE DATABASE_DEFAULT=C2.VCN COLLATE DATABASE_DEFAULT and C1.[ServiceRequestID]=C2.[ServiceRequestID] and C1.[SNO]=C2.[SNO]
WHEN MATCHED THEN  UPDATE SET '+ @DynaQry + ' 
WHEN NOT MATCHED THEN INSERT ( '

         SET @DynaQry =
                  @DynaQry
                + (SELECT STUFF (
                             (SELECT DISTINCT ',[' + column_name + ']'
                                FROM INFORMATION_SCHEMA.COLUMNS
                               WHERE     TABLE_NAME = '' + @TempTable + ''
                                     AND column_name IN (SELECT column_name
                                                           FROM INFORMATION_SCHEMA.COLUMNS
                                                          WHERE TABLE_NAME =
                                                                     ''
                                                                   + @TargetTable
                                                                   + '')
                              FOR XML PATH ( '' )),
                             1,
                             1,
                             ''))
                + ') VALUES ('
                + (SELECT STUFF (
                             (SELECT DISTINCT ',C2.[' + column_name + ']'
                                FROM INFORMATION_SCHEMA.COLUMNS
                               WHERE     TABLE_NAME = '' + @TempTable + ''
                                     AND column_name IN (SELECT column_name
                                                           FROM INFORMATION_SCHEMA.COLUMNS
                                                          WHERE TABLE_NAME =
                                                                     ''
                                                                   + @TargetTable
                                                                   + '')
                              FOR XML PATH ( '' )),
                             1,
                             1,
                             ''))
                + ') ;'
         PRINT (@TempTable);
         EXECUTE (@DynaQry);
END
--------

select @TempTable = substring(@AllTempTables,@StartPos,CHARINDEX(',',@AllTempTables)-1)
set @StartPos = @StartPos+len(@TempTable)+1
SET @Row_count = @Row_count - 1
END

IF OBJECT_ID('tempdb..#Recordings') IS NOT NULL DROP TABLE #Recordings

END
END
