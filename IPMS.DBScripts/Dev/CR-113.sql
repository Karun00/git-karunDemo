CREATE PROCEDURE [dbo].[usp_IPMS_FEED_SERVICE]
   @PortCode      VARCHAR (4),
   @VCN           NVARCHAR (12),
   @IMONO         NVARCHAR (15),
   @VesselName    NVARCHAR (200),
   @MovementFrmDt      VARCHAR (25),
   @MovementToDt       VARCHAR (25)
AS
   DECLARE
      @fromdate   varchar(25),
      @todate     varchar(25),
	@days		int;
   BEGIN
      SET @fromdate = @MovementFrmDt
      SET @todate = @MovementToDt

      IF (@MovementFrmDt IS NOT NULL AND @MovementToDt IS NULL)
         SET @todate =
                dbo.udf_FormatDateTime (
                   DATEADD (month, 1, CONVERT (DATE, @MovementFrmDt)),
                   'yyyy-mm-dd')

      IF (@MovementToDt IS NOT NULL AND @MovementFrmDt IS NULL)
         SET @fromdate =
                dbo.udf_FormatDateTime (
                   DATEADD (month, -1, CONVERT (DATE, @MovementToDt)),
                   'yyyy-mm-dd')

		if (@VesselName is NOT NULL or @IMONO is NOT NULL)
			set @PortCode=null;
			
		
		set @days= DATEDIFF(day, @fromdate,@todate)

	
		SELECT [dbo].[udf_IPMS_SERVICES] (ServiceRequestID) MovementDateTime,
             VCN,
             CONVERT (VARCHAR (100), ServiceRequestID) ServiceRequestID,
             MOVEMENTTYP,
             VesselName,
             VesselType vssltyp,
             CONVERT (VARCHAR (50), BreakWaterIn, 121) BreakWaterIn,
             CONVERT (VARCHAR (50), PortLimitOut, 121) PortLimitOut,
             BerthName,
             PortCode,
             ReasonForVisit,
             PortName,
             LastPortOfCall,
             NextPortOfCall,
             CallSign,
             CONVERT (VARCHAR (100), GrossRegisteredTonnageInMT)
                GrossRegisteredTonnageInMT,
             CONVERT (VARCHAR (100), DeadWeightTonnageInMT)
                DeadWeightTonnageInMT,
             CONVERT (VARCHAR (100), LengthOverallInM) LengthOverallInM,
             CONVERT (VARCHAR (100), forwarddraft) forwarddraft,
             CONVERT (VARCHAR (100), afterdraft) afterdraft,
             CONVERT (VARCHAR (50), BreakWaterOut, 121) BreakWaterOut,
             CONVERT (VARCHAR (50), PortLimitIn, 121) PortLimitIn,
             RegisteredName,
             [BerthName] BERTH1,
             Pilot_1 Pilot,
             [VesselNationality] CraftNationality,
             [Tug Boats/Work Boat_1] Tug,
             IMONo,
             [Berth Master_1] Berthmaster1,
             CONVERT (VARCHAR (50), [FirstLineIn_1], 121) FirstLineInTime1,
             CONVERT (VARCHAR (50), [LastLineIn_1], 121) LastLineInTime1,
             [Berth Master_2] Berthmaster2,
             CONVERT (VARCHAR (50), [FirstLineIn_2], 121) FirstLineInTime2,
             CONVERT (VARCHAR (50), [LastLineIn_2], 121) LastLineInTime2,
             [Pilot_1] pilt1,
             CONVERT (VARCHAR (50), [PilotOnBoard_1], 121) PilotOnBoardTime1,
             CONVERT (VARCHAR (50), [PilotOff_1], 121) PilotOfftime1,
             Pilot_2 pilt2,
             CONVERT (VARCHAR (50), [PilotOnBoard_2], 121) PilotOnBoardTime2,
             CONVERT (VARCHAR (50), [PilotOff_2], 121) PilotOfftime2,
             [Tug Boats/Work Boat_1] Tug1,
             CONVERT (VARCHAR (50), [Tug_StartTime_1], 121) TugStartTime1,
             CONVERT (VARCHAR (50), [Tug_EndTime_1], 121) TugEndTime1,
             [Tug Boats/Work Boat_2] Tug2,
             CONVERT (VARCHAR (50), [Tug_StartTime_2], 121) TugStartTime2,
             CONVERT (VARCHAR (50), [Tug_EndTime_2], 121) TugEndTime2,
             CONVERT (VARCHAR (50), [Tug Boats/Work Boat_3], 121) Tug3,
             CONVERT (VARCHAR (50), [Tug_StartTime_3], 121) TugStartTime3,
             CONVERT (VARCHAR (50), [Tug_EndTime_3], 121) TugEndTime3,
             CONVERT (VARCHAR (100), Sno) Sno
        FROM RPT_CompleteMovement
		Where (PortCode = @PortCode OR @PortCode IS NULL)
                           AND (VCN = @VCN OR @VCN IS NULL)
                           AND (IMONo = @IMONO OR @IMONO IS NULL)
                           AND (   VesselName LIKE
                                      '%' + @VesselName + '%'
                                OR @VesselName IS NULL)
						   AND ((dbo.udf_FormatDateTime (MovementDateTime, 'yyyy-mm-dd') BETWEEN @fromdate AND @todate) OR (@fromdate is null OR @todate is null))
        ORDER BY VCN, ServiceRequestID, Sno
   END
GO
CREATE PROCEDURE [dbo].[usp_IPMS_ANFEEDService]
   @PortCode      VARCHAR (4),
   @VCN           NVARCHAR (12),
   @IMONO         NVARCHAR (15),
   @VesselName    NVARCHAR (200),
   @ETAFrmDt      VARCHAR (25),
   @ETAToDt       VARCHAR (25)
AS
   DECLARE
      @fromdate   varchar(25),
      @todate     varchar(25),
	  @days		  int;
   BEGIN

      SET @fromdate = @ETAFrmDt
      SET @todate = @ETAToDt

      IF (@ETAFrmDt IS NOT NULL AND @ETAToDt IS NULL)
         SET @todate =
                dbo.udf_FormatDateTime (
                   DATEADD (month, 1, CONVERT (DATE, @ETAFrmDt)),
                   'yyyy-mm-dd')

      IF (@etatodt IS NOT NULL AND @etafrmdt IS NULL)
         SET @fromdate =
                dbo.udf_FormatDateTime (
                   DATEADD (month, -1, CONVERT (DATE, @ETAToDt)),
                   'yyyy-mm-dd')

		if (@VesselName is NOT NULL or @IMONO is NOT NULL)
			set @PortCode=null;
		
            SELECT VCN,
                   VESSELNAME,
                   IMONO,
                   CALLSIGN,
                   REGISTEREDNAME,
                   ETA,
                   ETD,
                   ALTERNATEBERTHNAME,
                   PREFERREDBERTHNAME,
                   ETB,
                   ETUB
              FROM (SELECT DISTINCT
                           AN.vcn,
                           VSL.VesselName,
                           VSL.IMONo,
                           VSL.CallSign,
                           AGT.RegisteredName,
                           CONVERT (VARCHAR (50), an.ETA, 121) ETA,
                           CONVERT (VARCHAR (50), an.ETD, 121) ETD,
                           brt1.BerthName AlternateBerthname,
                           brt2.BerthName PreferredBerthname,
                           CONVERT (VARCHAR (50), VC1.ETB, 121) ETB,
                           CONVERT (VARCHAR (50), VC1.ETUB, 121) ETUB
                      FROM VESSELCALL VC1
                           INNER JOIN ARRIVALNOTIFICATION AN
                              ON VC1.VCN = AN.VCN
                           INNER JOIN VESSEL VSL
                              ON VSL.VESSELID = AN.VESSELID
                           INNER JOIN AGENT AGT ON an.AgentID = AGT.AgentID
                           INNER JOIN Berth brt1
                              ON     brt1.PortCode = an.AlternatePortCode
                                 AND brt1.QuayCode = an.AlternateQuayCode
                                 AND brt1.BerthCode = an.AlternateBerthCode
                           INNER JOIN Berth brt2
                              ON     brt2.PortCode = an.PreferredPortCode
                                 AND brt2.QuayCode = an.PreferredQuayCode
                                 AND brt2.BerthCode = an.PreferredBerthCode
                     WHERE     (AN.PortCode = @PortCode OR @PortCode IS NULL)
                           AND (AN.VCN = @VCN OR @VCN IS NULL)
                           AND (VSL.IMONo = @IMONO OR @IMONO IS NULL)
                           AND (   VSL.VesselName LIKE
                                      '%' + @VesselName + '%'
                                OR @VesselName IS NULL)
                                AND ((dbo.udf_FormatDateTime (AN.ETA, 'yyyy-mm-dd') BETWEEN @fromdate AND @todate) OR (@fromdate is null OR @todate is null))
								) AS A
   END
GO