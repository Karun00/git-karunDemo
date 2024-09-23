IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID (N'usp_IPMS_FeedLocationService') AND type IN (N'P'))
   BEGIN
      DROP PROCEDURE [dbo].[usp_IPMS_FeedLocationService]
   END
GO
CREATE PROCEDURE [dbo].[usp_IPMS_FeedLocationService]
   @PortCode               VARCHAR (4),
   @VCN                    NVARCHAR (12),
   @IMONO                  NVARCHAR (15),
   @VesselName             NVARCHAR (200),
   @portLimitInFromDate    VARCHAR (25),
   @portLimitInToDate      VARCHAR (25)
AS
   DECLARE
      @fromdate   varchar(25),
      @todate     varchar(25)
   BEGIN

      SET @fromdate = @portLimitInFromDate
      SET @todate = @portLimitInToDate
	
	SELECT AN.VCN,
		  VSL.VESSELNAME,
		  VSL.IMONO,
		  CONVERT (VARCHAR (50), VC.ATA, 121) AS ATA,
		  CONVERT (VARCHAR (50), VC.ATD, 121) AS ATD,
		  CONVERT (VARCHAR (50), VC.BREAKWATERIN, 121)
			 AS BREAKWATERIN,
		  CONVERT (VARCHAR (50), VC.BREAKWATEROUT, 121)
			 AS BREAKWATEROUT,
		  CONVERT (VARCHAR (50), VC.PORTLIMITIN, 121) AS PORTLIMITIN,
		  CONVERT (VARCHAR (50), VC.PORTLIMITOUT, 121)
			 AS PORTLIMITOUT,
		  CONVERT (VARCHAR (50), VCa.AnchorDropTime, 121)
			 AS AnchorDropTime,
		  CONVERT (VARCHAR (50), VCa.AnchorAweighTime, 121)
			 AS AnchorAweighTime,
		  VCa.AnchorPosition,
		  sc.SubCatName AS Reason
	 FROM VESSELCALL VC
		  INNER JOIN ARRIVALNOTIFICATION AN ON VC.VCN = AN.VCN
		  INNER JOIN VESSEL VSL ON VSL.VESSELID = AN.VESSELID
		  LEFT JOIN VesselCallAnchorage vca ON vca.VCN = VC.VCN
		  LEFT JOIN SubCategory sc ON sc.SubCatCode = vca.Reason
WHERE (AN.PortCode = @PortCode OR @PortCode IS NULL)
                           AND (AN.VCN = @VCN OR @VCN IS NULL)
                           AND (VSL.IMONo = @IMONO OR @IMONO IS NULL)
                           AND (VSL.VesselName LIKE '%' + @VesselName + '%' OR @VesselName IS NULL)
                           AND ((dbo.udf_FormatDateTime (VC.PORTLIMITIN, 'yyyy-mm-dd') BETWEEN @fromdate AND @todate) OR (@fromdate is null OR @todate is null))
END
GO