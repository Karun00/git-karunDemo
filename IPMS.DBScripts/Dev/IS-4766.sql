ALTER PROCEDURE [dbo].[usp_RevenueVTSRDues]
   @portcode NVARCHAR (2), @VCN NVARCHAR (15)
   WITH
   EXECUTE AS CALLER
AS
   BEGIN
      DECLARE @postedon AS DATETIME
      DECLARE @postedBerthdueson AS DATETIME
      DECLARE @postedRefuseRemovalon AS DATETIME
      DECLARE @SAPACCOUNT AS NVARCHAR (16)
      DECLARE @SAPACCOUNTBerth AS NVARCHAR (16)
      DECLARE @SAPACCOUNTRefuse AS NVARCHAR (16)
      DECLARE @Dry12postedon AS DATETIME
      DECLARE @Dry12SAPACCOUNT AS NVARCHAR (16)
      DECLARE @cntAll AS INT
      DECLARE @cntBnk AS INT
      DECLARE @BERTDUEVCN AS NVARCHAR (15)
      DECLARE @cntCrewflag AS INT


      SET @cntAll =
             (SELECT count (ANN.VCN)
                FROM ArrivalNotification ANN
                     INNER JOIN dbo.ArrivalReason ANR ON ANR.VCN = ANN.VCN
                     INNER JOIN SubCategory ASB
                        ON ANR.Reason = ASB.SubCatCode
               WHERE     ANN.VCN NOT IN (SELECT An.VCN
                                           FROM ArrivalNotification An
                                                INNER JOIN
                                                dbo.ArrivalReason AR
                                                   ON AR.VCN = An.VCN
                                                INNER JOIN SubCategory SB
                                                   ON AR.Reason =
                                                         SB.SubCatCode
                                          WHERE     An.VCN = @VCN
                                                AND SB.SubCatCode IN (SELECT SubCatCode
                                                                        FROM SubCategory
                                                                       WHERE     SupCatCode =
                                                                                    'RSV'
                                                                             AND SubCatCode NOT IN ('LABY',
                                                                                                    'BUNK',
                                                                                                    'REPA')))
                     AND ANN.VCN = @VCN
              GROUP BY ANN.VCN)

      SET @cntCrewflag =
             (SELECT count (VM.ServiceRequestID)
                FROM VesselCallMovement VM
                     LEFT JOIN MaterialCodeMaster BR
                        ON     vm.FromPositionPortCode = br.PortCode
                           AND vm.FromPositionQuayCode = br.QuayCode
                           AND vm.FromPositionBerthCode = br.BerthCode
                           AND BR.ChargedFor = 'ILND'
               WHERE     vcn = @VCN
                     AND FromPositionBerthCode IS NOT NULL
                     AND BR.ChargedFor IS NULL)


      SET @cntBnk =
             (SELECT count (ANN.VCN)
                FROM ArrivalNotification ANN
                     INNER JOIN dbo.ArrivalReason ANR ON ANR.VCN = ANN.VCN
                     INNER JOIN SubCategory ASB
                        ON ANR.Reason = ASB.SubCatCode
               WHERE     ANN.VCN NOT IN (SELECT An.VCN
                                           FROM ArrivalNotification An
                                                INNER JOIN
                                                dbo.ArrivalReason AR
                                                   ON AR.VCN = An.VCN
                                                INNER JOIN SubCategory SB
                                                   ON AR.Reason =
                                                         SB.SubCatCode
                                          WHERE     An.VCN = @VCN
                                                AND SB.SubCatCode IN (SELECT SubCatCode
                                                                        FROM SubCategory
                                                                       WHERE     SupCatCode =
                                                                                    'RSV'
                                                                             AND SubCatCode NOT IN ('BUNK')))
                     AND ANN.VCN = @VCN
              GROUP BY ANN.VCN)

      IF @cntAll > 0
         BEGIN
            SET @BERTDUEVCN = @VCN

            IF @cntAll = 1 AND @cntBnk = 1
               BEGIN
                  SET @cntBnk = 2
               END
            ELSE
               SET @cntBnk = 0
         END
      ELSE
         BEGIN
            SET @cntBnk = 0
            SET @BERTDUEVCN = ''
         END

      SET @postedon =
             (SELECT TOP (1) dateadd (mi, +1, RD.PostedOn)
                FROM RevenuePostingDtl RD
                     INNER JOIN RevenuePosting RH
                        ON RH.RevenuePostingID = RD.RevenuePostingID
                     INNER JOIN ArrivalNotification an ON an.VCN = RH.vcn
                     INNER JOIN MaterialCodePort mp
                        ON mp.PortCode = an.PortCode
                     INNER JOIN MaterialCodeMaster mc
                        ON     mc.MaterialCodeMasterid =
                                  mp.MaterialCodeMasterid
                           AND RD.MaterialCode = mc.MaterialCode
                           AND RD.GroupCode = mc.GroupCode
               WHERE mc.ChargedFor = 'PODU' AND RH.vcn = @VCN
              ORDER BY RH.RevenuePostingID DESC);

      SET @postedBerthdueson =
             (SELECT TOP (1) dateadd (mi, +1, RD.PostedOn)
                FROM RevenuePostingDtl RD
                     INNER JOIN RevenuePosting RH
                        ON RH.RevenuePostingID = RD.RevenuePostingID
                     INNER JOIN ArrivalNotification an ON an.VCN = RH.vcn
                     INNER JOIN MaterialCodePort mp
                        ON mp.PortCode = an.PortCode
                     INNER JOIN MaterialCodeMaster mc
                        ON     mc.MaterialCodeMasterid =
                                  mp.MaterialCodeMasterid
                           AND RD.MaterialCode = mc.MaterialCode
                           AND RD.GroupCode = mc.GroupCode
               WHERE mc.ChargedFor = 'BRTH' AND RH.vcn = @VCN
              ORDER BY RH.RevenuePostingID DESC);

      SET @postedRefuseRemovalon =
             (SELECT TOP (1) dateadd (mi, +1, RD.PostedOn)
                FROM RevenuePostingDtl RD
                     INNER JOIN RevenuePosting RH
                        ON RH.RevenuePostingID = RD.RevenuePostingID
                     INNER JOIN ArrivalNotification an ON an.VCN = RH.vcn
                     INNER JOIN MaterialCodePort mp
                        ON mp.PortCode = an.PortCode
                     INNER JOIN MaterialCodeMaster mc
                        ON     mc.MaterialCodeMasterid =
                                  mp.MaterialCodeMasterid
                           AND RD.MaterialCode = mc.MaterialCode
                           AND RD.GroupCode = mc.GroupCode
               WHERE mc.ChargedFor = 'REFU' AND RH.vcn = @VCN
              ORDER BY RH.RevenuePostingID DESC);

      SET @SAPACCOUNT =
             (SELECT TOP (1) RH.SAPAccNo
                FROM RevenuePostingDtl RD
                     INNER JOIN RevenuePosting RH
                        ON RH.RevenuePostingID = RD.RevenuePostingID
                     INNER JOIN ArrivalNotification an ON an.VCN = RH.vcn
                     INNER JOIN MaterialCodePort mp
                        ON mp.PortCode = an.PortCode
                     INNER JOIN MaterialCodeMaster mc
                        ON     mc.MaterialCodeMasterid =
                                  mp.MaterialCodeMasterid
                           AND RD.MaterialCode = mc.MaterialCode
                           AND RD.GroupCode = mc.GroupCode
               WHERE mc.ChargedFor = 'PODU' AND RH.vcn = @VCN
              ORDER BY RH.RevenuePostingID DESC);
      SET @SAPACCOUNTBerth =
             (SELECT TOP (1) RH.SAPAccNo
                FROM RevenuePostingDtl RD
                     INNER JOIN RevenuePosting RH
                        ON RH.RevenuePostingID = RD.RevenuePostingID
                     INNER JOIN ArrivalNotification an ON an.VCN = RH.vcn
                     INNER JOIN MaterialCodePort mp
                        ON mp.PortCode = an.PortCode
                     INNER JOIN MaterialCodeMaster mc
                        ON     mc.MaterialCodeMasterid =
                                  mp.MaterialCodeMasterid
                           AND RD.MaterialCode = mc.MaterialCode
                           AND RD.GroupCode = mc.GroupCode
               WHERE mc.ChargedFor = 'BRTH' AND RH.vcn = @VCN
              ORDER BY RH.RevenuePostingID DESC);
      SET @SAPACCOUNTRefuse =
             (SELECT TOP (1) RH.SAPAccNo
                FROM RevenuePostingDtl RD
                     INNER JOIN RevenuePosting RH
                        ON RH.RevenuePostingID = RD.RevenuePostingID
                     INNER JOIN ArrivalNotification an ON an.VCN = RH.vcn
                     INNER JOIN MaterialCodePort mp
                        ON mp.PortCode = an.PortCode
                     INNER JOIN MaterialCodeMaster mc
                        ON     mc.MaterialCodeMasterid =
                                  mp.MaterialCodeMasterid
                           AND RD.MaterialCode = mc.MaterialCode
                           AND RD.GroupCode = mc.GroupCode
               WHERE mc.ChargedFor = 'REFU' AND RH.vcn = @VCN
              ORDER BY RH.RevenuePostingID DESC);



      DECLARE @postednewdate AS DATETIME
      SET @postednewdate = (SELECT getdate ());
      --set @postednewdate = (select CAST(CAST(getdate() AS Date) As datetime));


      SET @Dry12postedon =
             (SELECT TOP (1) RPD.PostedOn
                FROM SuppDryDock SD
                     INNER JOIN MaterialCodeMaster M
                        ON     SD.DockPortCode = M.PortCode
                           AND SD.DockQuayCode = M.QuayCode
                           AND SD.DockBerthCode = M.BerthCode
                           AND M.ChargedFor = 'DR12'
                     INNER JOIN MaterialCodePort MP
                        ON MP.MaterialCodeMasterid = M.MaterialCodeMasterid
                     INNER JOIN RevenuePostingDtl RPD
                        ON     RPD.vcn = SD.VCN
                           AND rpd.GroupCode = M.GroupCode
                           AND rpd.MaterialCode = M.MaterialCode
                     INNER JOIN RevenuePosting RPH
                        ON     RPD.RevenuePostingID = RPH.RevenuePostingID
                           AND RPD.VCN = SD.VCN
               WHERE SD.VCN = @VCN AND MP.PortCode = @portcode
              ORDER BY RPH.RevenuePostingID DESC)

      SET @Dry12SAPACCOUNT =
             (SELECT TOP (1) RPH.SAPAccNo
                FROM SuppDryDock SD
                     INNER JOIN MaterialCodeMaster M
                        ON     SD.DockPortCode = M.PortCode
                           AND SD.DockQuayCode = M.QuayCode
                           AND SD.DockBerthCode = M.BerthCode
                           AND M.ChargedFor = 'DR12'
                     INNER JOIN MaterialCodePort MP
                        ON MP.MaterialCodeMasterid = M.MaterialCodeMasterid
                     INNER JOIN RevenuePostingDtl RPD
                        ON     RPD.vcn = SD.VCN
                           AND rpd.GroupCode = M.GroupCode
                           AND rpd.MaterialCode = M.MaterialCode
                     INNER JOIN RevenuePosting RPH
                        ON     RPD.RevenuePostingID = RPH.RevenuePostingID
                           AND RPD.VCN = SD.VCN
               WHERE SD.VCN = @VCN AND MP.PortCode = @portcode
              ORDER BY RPH.RevenuePostingID DESC)
      -- FOR PORT DUES VIEWS ONLYY

      SELECT '0' ISPOSTED,
             RevenuePostingDtlID ResourceAllocationID,
             V.VCN,
             'PORTDUESVIEW' AS MovementName,
             'PORT DUES' AS ServiceName,
             M.GroupCode GroupCode,
             M.MaterialCode MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             RH.SAPAccNo AS AccountNo,
             RH.CreatedDate AS StartTime,
             V.BreakWaterOut AS Endtime,
             'N' AS IsCalculated,
             M.Chargedas AS Chargedas,
             M.UOM,
             '' AS MovementType,
             '' AS ServiceType,
             '' AS ServiceReferenceType,
             '' AS OperationType,
             '' AS TaskStatus,
             rd.PostingFrom RecentlyPostedDate,
             RD.PostedOn PostingDateTime,
             REPLACE (RD.Units, ',', '.') AS 'DueHours',
             REPLACE (RD.Units, ',', '.') AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '' MeterSerialNo,
             '' BerthName
        FROM VesselCall V
             INNER JOIN ArrivalNotification an ON an.VCN = V.VCN
             INNER JOIN MaterialCodePort MP ON an.PortCode = MP.PortCode
             INNER JOIN MaterialCodeMaster M
                ON     M.MaterialCodeMasterid = MP.MaterialCodeMasterid
                   AND M.ChargedFor = 'PODU'
             INNER JOIN RevenuePostingDtl RD
                ON     RD.GroupCode = M.GroupCode
                   AND RD.MaterialCode = M.MaterialCode
                   AND an.VCN = RD.VCN
             INNER JOIN RevenuePosting RH
                ON     RH.RevenuePostingID = RD.RevenuePostingID
                   AND RH.vcn = an.VCN
       WHERE V.VCN = @VCN
      ----
      UNION
      -- FOR BERTH DUES VIEWS ONLYY
      SELECT '0' ISPOSTED,
             RevenuePostingDtlID ResourceAllocationID,
             V.VCN,
             'PORTDUESVIEW' AS MovementName,
             'BERTH DUES' AS ServiceName,
             M.GroupCode GroupCode,
             M.MaterialCode MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             RH.SAPAccNo AS AccountNo,
             RH.CreatedDate AS StartTime,
             V.BreakWaterOut AS Endtime,
             'N' AS IsCalculated,
             M.Chargedas AS Chargedas,
             M.UOM,
             '' AS MovementType,
             '' AS ServiceType,
             '' AS ServiceReferenceType,
             '' AS OperationType,
             '' AS TaskStatus,
             rd.PostingFrom RecentlyPostedDate,
             RD.PostedOn PostingDateTime,
             REPLACE (RD.Units, ',', '.') AS 'DueHours',
             REPLACE (RD.Units, ',', '.') AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '' MeterSerialNo,
             '' BerthName
        FROM VesselCall V
             INNER JOIN ArrivalNotification an ON an.VCN = V.VCN
             INNER JOIN MaterialCodePort MP ON an.PortCode = MP.PortCode
             INNER JOIN MaterialCodeMaster M
                ON     M.MaterialCodeMasterid = MP.MaterialCodeMasterid
                   AND M.ChargedFor = 'BRTH'
             INNER JOIN RevenuePostingDtl RD
                ON     RD.GroupCode = M.GroupCode
                   AND RD.MaterialCode = M.MaterialCode
                   AND an.VCN = RD.VCN
             INNER JOIN RevenuePosting RH
                ON     RH.RevenuePostingID = RD.RevenuePostingID
                   AND RH.vcn = an.VCN
       WHERE V.VCN = @VCN
      --For Refuse Removal View only
      UNION
      SELECT '0' ISPOSTED,
             RevenuePostingDtlID ResourceAllocationID,
             V.VCN,
             'PORTDUESVIEW' AS MovementName,
             'REFUSE REMOVE' AS ServiceName,
             M.GroupCode GroupCode,
             M.MaterialCode MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             RH.SAPAccNo AS AccountNo,
             RH.CreatedDate AS StartTime,
             V.BreakWaterOut AS Endtime,
             'N' AS IsCalculated,
             M.Chargedas AS Chargedas,
             M.UOM,
             '' AS MovementType,
             '' AS ServiceType,
             '' AS ServiceReferenceType,
             '' AS OperationType,
             '' AS TaskStatus,
             rd.PostingFrom RecentlyPostedDate,
             RD.PostedOn PostingDateTime,
             REPLACE (RD.Units, ',', '.') AS 'DueHours',
             REPLACE (RD.Units, ',', '.') AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '' MeterSerialNo,
             '' BerthName
        FROM VesselCall V
             INNER JOIN ArrivalNotification an ON an.VCN = V.VCN
             INNER JOIN MaterialCodePort MP ON an.PortCode = MP.PortCode
             INNER JOIN MaterialCodeMaster M
                ON     M.MaterialCodeMasterid = MP.MaterialCodeMasterid
                   AND M.ChargedFor = 'REFU'
             INNER JOIN RevenuePostingDtl RD
                ON     RD.GroupCode = M.GroupCode
                   AND RD.MaterialCode = M.MaterialCode
                   AND an.VCN = RD.VCN
             INNER JOIN RevenuePosting RH
                ON     RH.RevenuePostingID = RD.RevenuePostingID
                   AND RH.vcn = an.VCN
       WHERE V.VCN = @VCN
      -----



      UNION
      -- FOR PORT DUES
      SELECT CASE DueHours WHEN '0' THEN '1' ELSE '0' END ISPOSTED, *
        FROM (SELECT VesselCallID ResourceAllocationID,
                     V.VCN,
                     'PORT DUES' AS MovementName,
                     'PORT DUES' AS ServiceName,
                     M.GroupCode GroupCode,
                     M.MaterialCode MaterialCode,
                     upper (M.MaterialDescription) MaterialDescription,
                     @SAPACCOUNT AS AccountNo,
                     V.BreakWaterIn AS StartTime,
                     V.BreakWaterOut AS Endtime,
                     'N' AS IsCalculated,
                     M.Chargedas AS Chargedas,
                     M.UOM,
                     '' AS MovementType,
                     '' AS ServiceType,
                     '' AS ServiceReferenceType,
                     '' AS OperationType,
                     '' AS TaskStatus,
                     isnull (@postedon, V.BreakWaterIn) RecentlyPostedDate,
                     isnull (V.BreakWaterOut, @postednewdate) PostingDateTime,
                     CAST (
                        ROUND (
                             DATEDIFF (
                                mi,
                                isnull (@postedon, V.BreakWaterIn),
                                isnull (V.BreakWaterOut, @postednewdate))
                           / 1440.0,
                           3,
                           3) AS DECIMAL (18, 3))
                        AS 'DueHours',
                     '0' AS 'TotalHours',
                     '0' CloseMterReding,
                     '0' AS startmtrreding,
                     '' MeterSerialNo,
                     '' BerthName
                FROM VesselCall V
                     INNER JOIN ArrivalNotification an ON an.VCN = V.VCN
                     INNER JOIN MaterialCodePort MP
                        ON an.PortCode = MP.PortCode
                     INNER JOIN MaterialCodeMaster M
                        ON     M.MaterialCodeMasterid =
                                  MP.MaterialCodeMasterid
                           AND M.ChargedFor = 'PODU'
               WHERE V.BreakWaterIn IS NOT NULL --and V.BreakWaterOut  >= ISNULL(@postedon, V.BreakWaterOut)
                                                --and V.BreakWaterIn  < @postednewdate
                     AND V.VCN = @VCN         -- 'VCNDB2014190' 'VCNDB2014289'
                                     ) a
       WHERE DueHours > 0
      UNION
      -- BERTH DUES
      SELECT CASE DueHours WHEN '0' THEN '1' ELSE '0' END ISPOSTED, *
        FROM (SELECT VesselCallID ResourceAllocationID,
                     V.VCN,
                     'BERTH DUES' AS MovementName,
                     'BERTH DUES' AS ServiceName,
                     M.GroupCode GroupCode,
                     M.MaterialCode MaterialCode,
                     upper (M.MaterialDescription) MaterialDescription,
                     @SAPACCOUNTBerth AS AccountNo,
                     V.BreakWaterIn AS StartTime,
                     V.BreakWaterOut AS Endtime,
                     'N' AS IsCalculated,
                     M.Chargedas AS Chargedas,
                     M.UOM,
                     '' AS MovementType,
                     '' AS ServiceType,
                     '' AS ServiceReferenceType,
                     '' AS OperationType,
                     '' AS TaskStatus,
                     isnull (@postedBerthdueson, V.BreakWaterIn)
                        RecentlyPostedDate,
                     isnull (V.BreakWaterOut, @postednewdate) PostingDateTime,
                     CAST (
                        ROUND (
                             DATEDIFF (
                                mi,
                                isnull (@postedBerthdueson, V.BreakWaterIn),
                                isnull (V.BreakWaterOut, @postednewdate))
                           / 1440.0,
                           3,
                           3) AS DECIMAL (18, 3))
                        AS 'DueHours',
                     '0' AS 'TotalHours',
                     '0' CloseMterReding,
                     '0' AS startmtrreding,
                     '' MeterSerialNo,
                     '' BerthName
                FROM VesselCall V
                     INNER JOIN ArrivalNotification an ON an.VCN = V.VCN
                     INNER JOIN MaterialCodePort MP
                        ON an.PortCode = MP.PortCode
                     INNER JOIN MaterialCodeMaster M
                        ON     M.MaterialCodeMasterid =
                                  MP.MaterialCodeMasterid
                           AND M.ChargedFor = 'BRTH'
               WHERE     V.BreakWaterIn IS NOT NULL
                     AND V.ATB IS NOT NULL
                     AND V.VCN = @BERTDUEVCN
                     -- and convert(varchar, CEILING (ROUND(cast((datediff(hour,BreakWaterIn,isnull(BreakWaterOut, @postednewdate))  / 24.0) as FLOAT),2)))
                     AND DATEDIFF (hour, BreakWaterIn, BreakWaterOut) >
                            CASE WHEN @cntBnk > 0 THEN 48 ELSE 0 END) a
       WHERE DueHours > 0
      UNION
      -- FOR VTS CHARGES ONLY FOR DURBON PORT
      SELECT isnull (RPD.RevenuePostingDtlID, '0') ISPOSTED,
             VesselCallID ResourceAllocationID,
             V.VCN,
             'Arrival' AS MovementName,
             upper (M.MaterialDescription) AS ServiceName,
             M.GroupCode GroupCode,
             M.MaterialCode MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             CASE
                WHEN (RPD.RevenuePostingDtlID IS NULL) THEN ''
                WHEN (RPD.RevenuePostingDtlID IS NOT NULL) THEN RP.SAPAccNo
             END
                AS AccountNo,
             v.ATA AS StartTime,
             v.ATD AS Endtime,
             'N' AS IsCalculated,
             M.Chargedas,
             M.UOM,
             '' AS MovementType,
             '' AS ServiceType,
             '' AS ServiceReferenceType,
             '' AS OperationType,
             '' AS TaskStatus,
             RPD.PostedOn RecentlyPostedDate,
             getdate () PostingDateTime,
             M.Chargedas AS 'DueHours',
             M.Chargedas AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '' MeterSerialNo,
             '' BerthName
        FROM VesselCall V
             INNER JOIN ArrivalNotification an ON an.VCN = V.VCN
             INNER JOIN MaterialCodePort MP ON an.PortCode = MP.PortCode
             INNER JOIN MaterialCodeMaster M
                ON     M.MaterialCodeMasterid = MP.MaterialCodeMasterid
                   AND M.ChargedFor = 'VTCH'
             LEFT JOIN RevenuePostingDtl RPD
                ON     RPD.vcn = V.VCN
                   AND rpd.GroupCode = M.GroupCode
                   AND rpd.MaterialCode = M.MaterialCode
             LEFT JOIN RevenuePosting RP
                ON RP.RevenuePostingID = RPD.RevenuePostingID
       WHERE V.VCN = @VCN                     -- 'VCNDB2014190' 'VCNDB2014289'
      UNION
      --  REFUSE REMOVAL

      SELECT CASE DueHours WHEN '0' THEN '1' ELSE '0' END ISPOSTED, *
        FROM (SELECT VC.VesselCallid ResourceAllocationID,
                     VC.VCN,
                     'REFUSE REMOVAL' MovementName,
                     upper (M.MaterialDescription) ServiceName,
                     M.GroupCode,
                     M.MaterialCode,
                     upper (M.MaterialDescription) MaterialDescription,
                     @SAPACCOUNTRefuse AS AccountNo,
                     VC.BreakWaterIn StartTime,
                     VC.BreakWaterOut Endtime,
                     M.IsCalculated,
                     CONVERT (
                        VARCHAR,
                        CEILING (
                           ROUND (
                              cast (
                                 (  datediff (hour,
                                              VC.BreakWaterIn,
                                              VC.BreakWaterOut)
                                  / 24.0) AS FLOAT),
                              2)))
                        Chargedas,
                     M.UOM,
                     '' MovementType,
                     '' ServiceType,
                     '' ServiceReferenceType,
                     '' OperationType,
                     '' TaskStatus,
                     --PostedOn RecentlyPostedDate, getdate() PostingDateTime,--commented by divya on 7Nov
                     isnull (@postedRefuseRemovalon, VC.BreakWaterIn)
                        RecentlyPostedDate,
                     isnull (VC.BreakWaterOut, @postednewdate)
                        PostingDateTime,
                     CAST (
                        ROUND (
                             DATEDIFF (
                                mi,
                                isnull (@postedRefuseRemovalon,
                                        VC.BreakWaterIn),
                                isnull (VC.BreakWaterOut, @postednewdate))
                           / 1440.0,
                           3,
                           3) AS DECIMAL (18, 3))
                        AS 'DueHours',
                     '0' AS 'TotalHours',
                     '0' CloseMterReding,
                     '0' AS startmtrreding,
                     '' MeterSerialNo,
                     '' BerthName
                FROM VesselCall VC
                     INNER JOIN ArrivalNotification AN ON AN.VCN = VC.VCN
                     INNER JOIN MaterialCodePort MP
                        ON an.PortCode = MP.PortCode
                     INNER JOIN MaterialCodeMaster M
                        ON     M.MaterialCodeMasterid =
                                  MP.MaterialCodeMasterid
                           AND M.ChargedFor = 'REFU'
                     LEFT JOIN RevenuePostingDtl RPD
                        ON     RPD.vcn = VC.VCN
                           AND rpd.ReferenceID = VC.VesselCallid
                           AND rpd.GroupCode = M.GroupCode
                           AND rpd.MaterialCode = M.MaterialCode
                     LEFT JOIN RevenuePosting RP
                        ON RP.RevenuePostingID = RPD.RevenuePostingID
               WHERE VC.BreakWaterIn IS NOT NULL AND VC.VCN = @VCN) a
       WHERE DueHours > 0
      UNION
      -- Fire Protection plus Security
      SELECT isnull (RPD.RevenuePostingDtlID, '0') ISPOSTED,
             VC.VesselCallMovementID ResourceAllocationID,
             VC.VCN,
             S.SubCatName AS MovementName,
             --SB1.SubCatName as ServiceName,
             upper (M.MaterialDescription) AS ServiceName,
             M.GroupCode,
             M.MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             CASE
                WHEN (RPD.RevenuePostingDtlID IS NULL) THEN ''
                WHEN (RPD.RevenuePostingDtlID IS NOT NULL) THEN RP.SAPAccNo
             END
                AS AccountNo,
             VC.ATB StartTime,
             VC.ATUB Endtime,
             M.IsCalculated,
             CONVERT (VARCHAR,
                      ceiling (DATEDIFF (mi, VC.ATB, VC.ATUB) / 60.0))
                Chargedas,
             M.UOM,
             VC.MovementType,
             M.ServiceType,
             'VTSR' ServiceReferenceType,
             '' OperationType,
             '' TaskStatus,
             PostedOn RecentlyPostedDate,
             getdate () PostingDateTime,
             CONVERT (VARCHAR,
                      ceiling (DATEDIFF (mi, VC.ATB, VC.ATUB) / 60.0))
                AS 'DueHours',
             1 AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '' MeterSerialNo,
             '' BerthName
        FROM VesselCallMovement VC
             INNER JOIN ArrivalNotification AN
                ON AN.VCN = VC.VCN AND AN.IsIMDGANFinal = 'Y'
             INNER JOIN MaterialCodeMaster M
                ON     m.PortCode = VC.FromPositionPortCode
                   AND M.QuayCode = VC.FromPositionQuayCode
                   AND M.BerthCode = VC.FromPositionBerthCode
                   AND M.ChargedFor = 'FPPS'
             INNER JOIN MaterialCodePort MP
                ON     MP.MaterialCodeMasterid = M.MaterialCodeMasterid
                   AND MP.PortCode = @portcode
             INNER JOIN SubCategory S ON S.SubCatCode = VC.MovementType
             INNER JOIN SubCategory SB1 ON SB1.SubCatCode = M.ServiceType
             LEFT JOIN RevenuePostingDtl RPD
                ON     RPD.vcn = VC.VCN
                   AND rpd.ReferenceID = VC.VesselCallMovementID
                   AND rpd.GroupCode = M.GroupCode
                   AND rpd.MaterialCode = M.MaterialCode
             LEFT JOIN RevenuePosting RP
                ON RP.RevenuePostingID = RPD.RevenuePostingID
       WHERE     VC.ATUB IS NOT NULL
             AND VC.VCN = @VCN
             AND (   VC.MovementType = 'ARMV'
                  OR VC.MovementType = 'SHMV'
                  OR VC.MovementType = 'WRMV')
      UNION
      -- CREW TRANS PORTATION
      SELECT DISTINCT
             isnull (RPD.RevenuePostingDtlID, '0') ISPOSTED,
             RA.ServiceReferenceID,
             VC.VCN,
             CASE
                WHEN (@cntCrewflag > 0) THEN S.SubCatName
                ELSE 'Arrival'
             END
                AS MovementName,
             --   S.SubCatName AS MovementName ,
             upper (M.MaterialDescription) AS ServiceName,
             M.GroupCode,
             M.MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             CASE
                WHEN (RPD.RevenuePostingDtlID IS NULL) THEN ''
                WHEN (RPD.RevenuePostingDtlID IS NOT NULL) THEN RP.SAPAccNo
             END
                AS AccountNo,
             CASE WHEN (@cntCrewflag > 0) THEN VC.ATB ELSE V.BreakWaterIn END
                AS StartTime,
             CASE
                WHEN (@cntCrewflag > 0) THEN VC.ATUB
                ELSE V.BreakWaterOut
             END
                AS Endtime,
             --   VC.ATB StartTime,
             -- VC.ATUB Endtime,
             M.IsCalculated,
             CASE
                WHEN (@cntCrewflag > 0)
                THEN
                   CONVERT (VARCHAR, (datediff (day, VC.ATB, VC.ATUB) + 1))
                ELSE
                   CONVERT (
                      VARCHAR,
                      (datediff (day, V.BreakWaterIn, V.BreakWaterOut) + 1))
             END
                AS Chargedas,
             --   convert(varchar, (datediff(day,VC.ATB,VC.ATUB)+1)) Chargedas,
             M.UOM,
             VC.MovementType,
             M.ServiceType,
             RA.ServiceReferenceType,
             RA.OperationType,
             RA.TaskStatus,
             PostedOn RecentlyPostedDate,
             getdate () PostingDateTime,
             CASE
                WHEN (@cntCrewflag > 0)
                THEN
                   CONVERT (VARCHAR, (datediff (day, VC.ATB, VC.ATUB) + 1))
                ELSE
                   CONVERT (
                      VARCHAR,
                      (datediff (day, V.BreakWaterIn, V.BreakWaterOut) + 1))
             END
                AS DueHours,
             --  convert(varchar, (datediff(day,VC.ATB,VC.ATUB)+1)) AS 'DueHours',

             1 AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '' MeterSerialNo,
             '' BerthName
        FROM ShiftingBerthingTaskExecution SB
             INNER JOIN ResourceAllocation RA
                ON RA.ResourceAllocationID = SB.ResourceAllocationID
             INNER JOIN VesselCallMovement VC
                ON VC.ServiceRequestID = RA.ServiceReferenceID
             INNER JOIN VesselCall V ON v.vcn = VC.VCN
             INNER JOIN MaterialCodeMaster M
                ON     m.PortCode = SB.FromBerthPortCode
                   AND M.QuayCode = SB.FromBerthQuayCode
                   AND M.BerthCode = SB.FromBerthCode
                   AND M.ChargedFor = 'ILND'
             INNER JOIN MaterialCodePort MP
                ON     MP.MaterialCodeMasterid = M.MaterialCodeMasterid
                   AND MP.PortCode = @portcode
             INNER JOIN SubCategory S ON S.SubCatCode = VC.MovementType
             INNER JOIN SubCategory SB1 ON SB1.SubCatCode = M.ServiceType
             LEFT JOIN RevenuePostingDtl RPD
                ON     RPD.vcn = VC.VCN
                   AND rpd.ReferenceID = RA.ResourceAllocationID
                   AND rpd.GroupCode = M.GroupCode
                   AND rpd.MaterialCode = M.MaterialCode
             LEFT JOIN RevenuePosting RP
                ON RP.RevenuePostingID = RPD.RevenuePostingID
       WHERE     (RA.TaskStatus = 'COMP' OR RA.TaskStatus = 'VERF')
             AND RA.OperationType = 'BRTH'
             AND VC.ATUB IS NOT NULL
             AND VC.VCN = @VCN
             AND (   VC.MovementType = 'ARMV'
                  OR VC.MovementType =
                        CASE
                           WHEN @cntCrewflag > 0 THEN 'SHMV'
                           ELSE 'ARMV'
                        END)
      UNION
      -- Running of Lines
      SELECT isnull (RPD.RevenuePostingDtlID, '0') ISPOSTED,
             RA.ResourceAllocationID,
             VC.VCN,
             S.SubCatName AS MovementName,
             upper (M.MaterialDescription) AS ServiceName,
             M.GroupCode,
             M.MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             CASE
                WHEN (RPD.RevenuePostingDtlID IS NULL) THEN ''
                WHEN (RPD.RevenuePostingDtlID IS NOT NULL) THEN RP.SAPAccNo
             END
                AS AccountNo,
             VC.ATB StartTime,
             VC.ATUB Endtime,
             M.IsCalculated,
             m.Chargedas,
             M.UOM,
             VC.MovementType,
             M.ServiceType,
             RA.ServiceReferenceType,
             RA.OperationType,
             RA.TaskStatus,
             PostedOn RecentlyPostedDate,
             getdate () PostingDateTime,
             m.Chargedas AS 'DueHours',
             1 AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '' MeterSerialNo,
             '' BerthName
        FROM ShiftingBerthingTaskExecution SB
             INNER JOIN ResourceAllocation RA
                ON RA.ResourceAllocationID = SB.ResourceAllocationID
             INNER JOIN VesselCallMovement VC
                ON VC.ServiceRequestID = RA.ServiceReferenceID
             INNER JOIN MaterialCodeMaster M
                ON     m.PortCode = SB.FromBerthPortCode
                   AND M.QuayCode = SB.FromBerthQuayCode
                   AND M.BerthCode = SB.FromBerthCode
                   AND M.ChargedFor = 'RNOF'
             INNER JOIN MaterialCodePort MP
                ON     MP.MaterialCodeMasterid = M.MaterialCodeMasterid
                   AND MP.PortCode = @portcode
             INNER JOIN SubCategory S ON S.SubCatCode = VC.MovementType
             INNER JOIN SubCategory SB1 ON SB1.SubCatCode = M.ServiceType
             LEFT JOIN RevenuePostingDtl RPD
                ON     RPD.vcn = VC.VCN
                   AND rpd.ReferenceID = RA.ResourceAllocationID
                   AND rpd.GroupCode = M.GroupCode
                   AND rpd.MaterialCode = M.MaterialCode
             LEFT JOIN RevenuePosting RP
                ON RP.RevenuePostingID = RPD.RevenuePostingID
       WHERE     (RA.TaskStatus = 'COMP' OR RA.TaskStatus = 'VERF')
             AND RA.OperationType = 'BRTH'
             AND VC.VCN = @VCN
      UNION
      -- FOR SAMSA LEVY CHARGE
      SELECT isnull (RPD.RevenuePostingDtlID, '0') ISPOSTED,
             VesselCallID ResourceAllocationID,
             V.VCN,
             'Arrival' AS MovementName,
             upper (M.MaterialDescription) AS ServiceName,
             M.GroupCode GroupCode,
             M.MaterialCode MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             CASE
                WHEN (RPD.RevenuePostingDtlID IS NULL) THEN ''
                WHEN (RPD.RevenuePostingDtlID IS NOT NULL) THEN RP.SAPAccNo
             END
                AS AccountNo,
             v.ATA AS StartTime,
             v.ATD AS Endtime,
             'N' AS IsCalculated,
             m.Chargedas,
             M.UOM AS UOM,
             '' AS MovementType,
             '' AS ServiceType,
             '' AS ServiceReferenceType,
             '' AS OperationType,
             '' AS TaskStatus,
             RPD.PostedOn RecentlyPostedDate,
             getdate () PostingDateTime,
             m.Chargedas AS 'DueHours',
             '1' AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '' MeterSerialNo,
             '' BerthName
        FROM VesselCall V
             INNER JOIN ArrivalNotification an ON an.VCN = V.VCN
             INNER JOIN MaterialCodePort MP ON an.PortCode = MP.PortCode
             INNER JOIN MaterialCodeMaster M
                ON     M.MaterialCodeMasterid = MP.MaterialCodeMasterid
                   AND M.ChargedFor = 'SMSA'
                   AND M.RecordStatus = 'A'
             INNER JOIN PortRegistry po ON po.PortCode = an.LastPortOfCall
             LEFT JOIN RevenuePostingDtl RPD
                ON     RPD.vcn = V.VCN
                   AND rpd.GroupCode = M.GroupCode
                   AND rpd.MaterialCode = M.MaterialCode
             LEFT JOIN RevenuePosting RP
                ON RP.RevenuePostingID = RPD.RevenuePostingID
       WHERE V.VCN = @VCN                  --  'VCNDB1500300'-- 'VCNDB2014289'
                         AND isnull (po.IsSA, 'N') = 'N'
      UNION
      -- FOR LIGHT DUES CHARGE
      SELECT isnull (RPD.RevenuePostingDtlID, '0') ISPOSTED,
             VesselCallID ResourceAllocationID,
             V.VCN,
             'Arrival' AS MovementName,
             upper (M.MaterialDescription) AS ServiceName,
             M.GroupCode GroupCode,
             M.MaterialCode MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             CASE
                WHEN (RPD.RevenuePostingDtlID IS NULL) THEN ''
                WHEN (RPD.RevenuePostingDtlID IS NOT NULL) THEN RP.SAPAccNo
             END
                AS AccountNo,
             v.ATA AS StartTime,
             v.ATD AS Endtime,
             'N' AS IsCalculated,
             M.Chargedas,
             M.UOM,
             '' AS MovementType,
             '' AS ServiceType,
             '' AS ServiceReferenceType,
             '' AS OperationType,
             '' AS TaskStatus,
             RPD.PostedOn RecentlyPostedDate,
             getdate () PostingDateTime,
             m.Chargedas AS 'DueHours',
             '1' AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '' MeterSerialNo,
             '' BerthName
        FROM VesselCall V
             INNER JOIN ArrivalNotification an ON an.VCN = V.VCN
             INNER JOIN MaterialCodePort MP ON an.PortCode = MP.PortCode
             INNER JOIN MaterialCodeMaster M
                ON     M.MaterialCodeMasterid = MP.MaterialCodeMasterid
                   AND M.ChargedFor = 'LIDC'
             INNER JOIN PortRegistry po ON po.PortCode = an.LastPortOfCall
             LEFT JOIN RevenuePostingDtl RPD
                ON     RPD.vcn = V.VCN
                   AND rpd.GroupCode = M.GroupCode
                   AND rpd.MaterialCode = M.MaterialCode
             LEFT JOIN RevenuePosting RP
                ON RP.RevenuePostingID = RPD.RevenuePostingID
       WHERE V.VCN = @VCN                  --  'VCNDB1500300'-- 'VCNDB2014289'
                         AND isnull (po.IsSA, 'N') = 'N'
      UNION
      -- IS SPECIAL NATURE

      SELECT isnull (RPD.RevenuePostingDtlID, '0') ISPOSTED,
             VesselCallID ResourceAllocationID,
             V.VCN,
             'Arrival' AS MovementName,
             upper (M.MaterialDescription) AS ServiceName,
             M.GroupCode GroupCode,
             M.MaterialCode MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             CASE
                WHEN (RPD.RevenuePostingDtlID IS NULL) THEN ''
                WHEN (RPD.RevenuePostingDtlID IS NOT NULL) THEN RP.SAPAccNo
             END
                AS AccountNo,
             v.ATA AS StartTime,
             v.ATD AS Endtime,
             'N' AS IsCalculated,
             M.Chargedas,
             M.UOM,
             '' AS MovementType,
             '' AS ServiceType,
             '' AS ServiceReferenceType,
             '' AS OperationType,
             '' AS TaskStatus,
             RPD.PostedOn RecentlyPostedDate,
             getdate () PostingDateTime,
             m.Chargedas AS 'DueHours',
             m.Chargedas AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '' MeterSerialNo,
             '' BerthName
        FROM VesselCall V
             INNER JOIN ArrivalNotification an ON an.VCN = V.VCN
             INNER JOIN MaterialCodePort MP ON an.PortCode = MP.PortCode
             INNER JOIN MaterialCodeMaster M
                ON     M.MaterialCodeMasterid = MP.MaterialCodeMasterid
                   AND M.ChargedFor = 'SPCL'
             LEFT JOIN RevenuePostingDtl RPD
                ON     RPD.vcn = V.VCN
                   AND rpd.GroupCode = M.GroupCode
                   AND rpd.MaterialCode = M.MaterialCode
             LEFT JOIN RevenuePosting RP
                ON RP.RevenuePostingID = RPD.RevenuePostingID
       WHERE an.IsSpecialNature = 'A' AND V.VCN = @VCN
      UNION
      -- FOR VTS SERVICES ARRV,SHIFT,WARP,SAIL
      SELECT isnull (RPD.RevenuePostingDtlID, '0') ISPOSTED,
             T.ResourceAllocationID,
             T.VCN,
             SB.SubCatName AS MovementName,
             upper (SB1.SubCatName) AS ServiceName,
             M.GroupCode,
             M.MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             CASE
                WHEN (RPD.RevenuePostingDtlID IS NULL) THEN ''
                WHEN (RPD.RevenuePostingDtlID IS NOT NULL) THEN RP.SAPAccNo
             END
                AS AccountNo,
             T.StartTime,
             T.Endtime,
             M.IsCalculated,
             M.Chargedas,
             M.UOM,
             M.MovementType,
             M.ServiceType,
             T.ServiceReferenceType,
             T.OperationType,
             T.TaskStatus,
             PostedOn RecentlyPostedDate,
             getdate () PostingDateTime,
             m.Chargedas AS 'DueHours',
             m.Chargedas AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             --   '' MeterSerialNo, '' BerthName
             TB.BerthName MeterSerialNo,
             FB.BerthName BerthName
        FROM (SELECT sbr.FromBerthCode,
                     sbr.FromBerthQuayCode,
                     sbr.FromBerthPortCode,
                     sbr.ToBerthCode,
                     sbr.ToBerthQuayCode,
                     sbr.ToBerthPortCode,
                     RA.*,
                     CASE RA.ServiceReferenceType
                        WHEN 'SUPP'
                        THEN
                           (SELECT VCN
                              FROM dbo.SuppServiceRequest
                             WHERE SuppServiceRequestID =
                                      RA.ServiceReferenceID)
                        ELSE
                           (SELECT VCN
                              FROM dbo.ServiceRequest
                             WHERE ServiceRequestID = RA.ServiceReferenceID)
                     END
                        VCN,
                     CASE RA.ServiceReferenceType
                        WHEN 'SUPP'
                        THEN
                           (SELECT ServiceType
                              FROM dbo.SuppServiceRequest
                             WHERE SuppServiceRequestID =
                                      RA.ServiceReferenceID)
                        ELSE
                           (SELECT MovementType
                              FROM dbo.ServiceRequest
                             WHERE ServiceRequestID = RA.ServiceReferenceID)
                     END
                        MovementType
                FROM dbo.ResourceAllocation RA
                     LEFT JOIN ShiftingBerthingTaskExecution sbr
                        ON ra.ResourceAllocationID = sbr.ResourceAllocationID)
             T
             INNER JOIN MaterialCodeMaster M
                ON     M.MovementType = T.MovementType
                   AND M.ServiceType = T.OperationType
             INNER JOIN MaterialCodePort MP
                ON MP.MaterialCodeMasterid = M.MaterialCodeMasterid
             INNER JOIN SubCategory SB ON SB.SubCatCode = M.MovementType
             INNER JOIN SubCategory SB1 ON SB1.SubCatCode = M.ServiceType
             LEFT JOIN RevenuePostingDtl RPD
                ON     RPD.vcn = t.VCN
                   AND rpd.ReferenceID = T.ResourceAllocationID
                   AND rpd.GroupCode = M.GroupCode
                   AND rpd.MaterialCode = M.MaterialCode
                   AND rpd.MomentType = M.MovementType
                   AND rpd.ServiceType = M.ServiceType
             LEFT JOIN RevenuePosting RP
                ON RP.RevenuePostingID = RPD.RevenuePostingID
             LEFT JOIN Berth FB
                ON     FB.BerthCode = T.FromBerthCode
                   AND FB.QuayCode = T.FromBerthQuayCode
                   AND FB.PortCode = T.FromBerthPortCode
             LEFT JOIN Berth TB
                ON     TB.BerthCode = T.ToBerthCode
                   AND TB.QuayCode = T.ToBerthQuayCode
                   AND TB.PortCode = T.ToBerthPortCode
       WHERE     T.VCN = @VCN                 -- 'VCNDB2014190' 'VCNDB2014289'
             AND MP.PortCode = @portcode
             AND (taskstatus = 'COMP' OR taskstatus = 'VERF')
      UNION
      -- DRY DOCK BOOKING FEES
      SELECT isnull (RPD.RevenuePostingDtlID, '0') ISPOSTED,
             SuppDryDockID ResourceAllocationID,
             SD.VCN,
             'DRYDOCKSERVICES' AS MovementName,
             upper (M.MaterialDescription) AS ServiceName,
             M.GroupCode,
             M.MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             RPH.SAPAccNo AS AccountNo,
             SD.FromDate AS StartTime,
             SD.ToDate AS Endtime,
             M.IsCalculated AS IsCalculated,
             M.Chargedas,
             M.UOM,
             '' AS MovementType,
             '' AS ServiceType,
             '' AS ServiceReferenceType,
             '' AS OperationType,
             '' AS TaskStatus,
             RPD.PostedOn RecentlyPostedDate,
             getdate () PostingDateTime,
             m.Chargedas 'DueHours',
             '1' AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '1' MeterSerialNo,
             '' BerthName
        FROM SuppDryDock SD
             INNER JOIN MaterialCodeMaster M
                ON     SD.DockPortCode = M.PortCode
                   AND SD.DockQuayCode = M.QuayCode
                   AND SD.DockBerthCode = M.BerthCode
                   AND M.ChargedFor = 'DKBK'
             INNER JOIN MaterialCodePort MP
                ON MP.MaterialCodeMasterid = M.MaterialCodeMasterid
             LEFT JOIN RevenuePostingDtl RPD
                ON     RPD.vcn = SD.VCN
                   AND rpd.GroupCode = M.GroupCode
                   AND rpd.MaterialCode = M.MaterialCode
             LEFT JOIN RevenuePosting RPH
                ON     RPD.RevenuePostingID = RPH.RevenuePostingID
                   AND RPD.VCN = SD.VCN
       WHERE SD.VCN = @VCN                    -- 'VCNDB2014190' 'VCNDB2014289'
                          AND MP.PortCode = @portcode
      UNION
      -- PREPARATION COST OF DRY DOCK
      SELECT isnull (RPD.RevenuePostingDtlID, '0') ISPOSTED,
             SuppDryDockID ResourceAllocationID,
             SD.VCN,
             'DRYDOCKSERVICES' AS MovementName,
             upper (M.MaterialDescription) AS ServiceName,
             M.GroupCode,
             M.MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             RPH.SAPAccNo AS AccountNo,
             SD.FromDate AS StartTime,
             SD.ToDate AS Endtime,
             M.IsCalculated AS IsCalculated,
             M.Chargedas,
             M.UOM,
             '' AS MovementType,
             '' AS ServiceType,
             '' AS ServiceReferenceType,
             '' AS OperationType,
             '' AS TaskStatus,
             RPD.PostedOn RecentlyPostedDate,
             getdate () PostingDateTime,
             m.Chargedas 'DueHours',
             '1' AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '2' MeterSerialNo,
             '' BerthName
        FROM SuppDryDock SD
             INNER JOIN MaterialCodeMaster M
                ON     SD.DockPortCode = M.PortCode
                   AND SD.DockQuayCode = M.QuayCode
                   AND SD.DockBerthCode = M.BerthCode
                   AND M.ChargedFor = 'PREP'
             INNER JOIN MaterialCodePort MP
                ON MP.MaterialCodeMasterid = M.MaterialCodeMasterid
             LEFT JOIN RevenuePostingDtl RPD
                ON     RPD.vcn = SD.VCN
                   AND rpd.GroupCode = M.GroupCode
                   AND rpd.MaterialCode = M.MaterialCode
             LEFT JOIN RevenuePosting RPH
                ON     RPD.RevenuePostingID = RPH.RevenuePostingID
                   AND RPD.VCN = SD.VCN
       WHERE SD.VCN = @VCN                  --'VCNDB2014229' -- 'VCNDB2014190'
                          AND MP.PortCode = @portcode
      UNION
      -- DOCKING FEES
      SELECT isnull (RPD.RevenuePostingDtlID, '0') ISPOSTED,
             SuppDryDockID ResourceAllocationID,
             SD.VCN,
             'DRYDOCKSERVICES' AS MovementName,
             upper (M.MaterialDescription) AS ServiceName,
             M.GroupCode,
             M.MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             RPH.SAPAccNo AS AccountNo,
             SD.EnteredDockDateTime AS StartTime,
             SD.LeftDockDateTime AS Endtime,
             M.IsCalculated AS IsCalculated,
             M.Chargedas,
             M.UOM AS UOM,
             '' AS MovementType,
             '' AS ServiceType,
             '' AS ServiceReferenceType,
             '' AS OperationType,
             '' AS TaskStatus,
             RPD.PostedOn RecentlyPostedDate,
             getdate () PostingDateTime,
             m.Chargedas 'DueHours',
             '1' AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '3' MeterSerialNo,
             '' BerthName
        FROM SuppDryDock SD
             INNER JOIN MaterialCodeMaster M
                ON     SD.DockPortCode = M.PortCode
                   AND SD.DockQuayCode = M.QuayCode
                   AND SD.DockBerthCode = M.BerthCode
                   AND M.ChargedFor = 'DOCK'
             INNER JOIN MaterialCodePort MP
                ON MP.MaterialCodeMasterid = M.MaterialCodeMasterid
             LEFT JOIN RevenuePostingDtl RPD
                ON     RPD.vcn = SD.VCN
                   AND rpd.GroupCode = M.GroupCode
                   AND rpd.MaterialCode = M.MaterialCode
             LEFT JOIN RevenuePosting RPH
                ON     RPD.RevenuePostingID = RPH.RevenuePostingID
                   AND RPD.VCN = SD.VCN
       WHERE     SD.VCN = @VCN                -- 'VCNDB2014190' 'VCNDB2014289'
             AND MP.PortCode = @portcode
             AND SD.EnteredDockDateTime IS NOT NULL
      UNION
      -- DRY DOCK 24 HOURS
      SELECT isnull (RPD.RevenuePostingDtlID, '0') ISPOSTED,
             SuppDryDockID ResourceAllocationID,
             SD.VCN,
             'DRYDOCKSERVICES' AS MovementName,
             'FIRST 24 HR PERIOD' AS ServiceName,
             M.GroupCode,
             M.MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             RPH.SAPAccNo AS AccountNo,
             SD.EnteredDockDateTime AS StartTime,
             SD.LeftDockDateTime AS Endtime,
             M.IsCalculated AS IsCalculated,
             M.Chargedas,
             M.UOM,
             '' AS MovementType,
             '' AS ServiceType,
             '' AS ServiceReferenceType,
             '' AS OperationType,
             '' AS TaskStatus,
             RPD.PostedOn RecentlyPostedDate,
             getdate () PostingDateTime,
             m.Chargedas AS 'DueHours',
             '1' AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '4' MeterSerialNo,
             '' BerthName
        FROM SuppDryDock SD
             INNER JOIN MaterialCodeMaster M
                ON     SD.DockPortCode = M.PortCode
                   AND SD.DockQuayCode = M.QuayCode
                   AND SD.DockBerthCode = M.BerthCode
                   AND M.ChargedFor = 'DR24'
             INNER JOIN MaterialCodePort MP
                ON MP.MaterialCodeMasterid = M.MaterialCodeMasterid
             LEFT JOIN RevenuePostingDtl RPD
                ON     RPD.vcn = SD.VCN
                   AND rpd.GroupCode = M.GroupCode
                   AND rpd.MaterialCode = M.MaterialCode
             LEFT JOIN RevenuePosting RPH
                ON     RPD.RevenuePostingID = RPH.RevenuePostingID
                   AND RPD.VCN = SD.VCN
       WHERE     SD.VCN = @VCN                -- 'VCNDB2014190' 'VCNDB2014289'
             AND MP.PortCode = @portcode
             AND SD.EnteredDockDateTime IS NOT NULL
      UNION
      -- DRY DOCK 12 Hours
      -- isnull    @Dry12postedon  @Dry12SAPACCOUNT



      SELECT CASE DueHours WHEN '0' THEN '1' ELSE '0' END ISPOSTED, *
        FROM (SELECT ResourceAllocationID,
                     VCN,
                     MovementName,
                     ServiceName,
                     GroupCode,
                     MaterialCode,
                     MaterialDescription,
                     AccountNo,
                     StartTime,
                     Endtime,
                     IsCalculated,
                     Chargedas,
                     UOM,
                     MovementType,
                     ServiceType,
                     ServiceReferenceType,
                     OperationType,
                     TaskStatus,
                     RecentlyPostedDate,
                     (  isnull (RecentlyPostedDate, StartTime)
                      +   ceiling (
                               DATEDIFF (
                                  mi,
                                  isnull (RecentlyPostedDate, StartTime),
                                  isnull (Endtime, getdate ()))
                             / 720.0)
                        / 2.0)
                        PostingDateTime,
                     ceiling (
                          DATEDIFF (mi,
                                    isnull (RecentlyPostedDate, StartTime),
                                    isnull (Endtime, getdate ()))
                        / 720.0)
                        AS 'DueHours',
                     ceiling (
                          DATEDIFF (mi,
                                    StartTime,
                                    isnull (Endtime, getdate ()))
                        / 720.0)
                        AS 'TotalHours',
                     '0' CloseMterReding,
                     '0' AS startmtrreding,
                     '5' MeterSerialNo,
                     '' BerthName
                FROM (SELECT SuppDryDockID ResourceAllocationID,
                             SD.VCN,
                             'DRYDOCK12HRSSERVICES' AS MovementName,
                             upper (M.MaterialDescription) AS ServiceName,
                             M.GroupCode,
                             M.MaterialCode,
                             upper (M.MaterialDescription)
                                MaterialDescription,
                             @Dry12SAPACCOUNT AS AccountNo,
                             SD.EnteredDockDateTime + 1 AS StartTime,
                             SD.LeftDockDateTime AS Endtime,
                             M.IsCalculated AS IsCalculated,
                             M.Chargedas AS Chargedas,
                             M.UOM AS UOM,
                             '' AS MovementType,
                             '' AS ServiceType,
                             '' AS ServiceReferenceType,
                             '' AS OperationType,
                             '' AS TaskStatus,
                             @Dry12postedon RecentlyPostedDate,
                             CASE
                                WHEN sd.Todate >= getdate () THEN getdate ()
                                ELSE sd.Todate
                             END
                                AS calculateddate
                        FROM SuppDryDock SD
                             INNER JOIN MaterialCodeMaster M
                                ON     SD.DockPortCode = M.PortCode
                                   AND SD.DockQuayCode = M.QuayCode
                                   AND SD.DockBerthCode = M.BerthCode
                                   AND M.ChargedFor = 'DR12'
                                   AND SD.EnteredDockDateTime + 1 <=
                                          getdate ()
                             INNER JOIN MaterialCodePort MP
                                ON MP.MaterialCodeMasterid =
                                      M.MaterialCodeMasterid
                             LEFT JOIN RevenuePostingDtl RPD
                                ON     RPD.vcn = SD.VCN
                                   AND rpd.GroupCode = M.GroupCode
                                   AND rpd.MaterialCode = M.MaterialCode
                             LEFT JOIN RevenuePosting RPH
                                ON     RPD.RevenuePostingID =
                                          RPH.RevenuePostingID
                                   AND RPD.VCN = SD.VCN
                       WHERE     SD.VCN = @VCN -- 'VCNDB2014190' 'VCNDB2014289'
                             AND MP.PortCode = @portcode
                             AND ceiling (
                                      DATEDIFF (mi,
                                                SD.EnteredDockDateTime,
                                                SD.ToDate)
                                    / 60.0) > 24
                             AND SD.EnteredDockDateTime IS NOT NULL) n) k
      UNION
      -- UNDOCKING FEES
      SELECT isnull (RPD.RevenuePostingDtlID, '0') ISPOSTED,
             SuppDryDockID ResourceAllocationID,
             SD.VCN,
             'DRYDOCKSERVICES' AS MovementName,
             upper (M.MaterialDescription) AS ServiceName,
             M.GroupCode,
             M.MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             RPH.SAPAccNo AS AccountNo,
             SD.EnteredDockDateTime AS StartTime,
             SD.LeftDockDateTime AS Endtime,
             M.IsCalculated AS IsCalculated,
             M.Chargedas,
             M.UOM,
             '' AS MovementType,
             '' AS ServiceType,
             '' AS ServiceReferenceType,
             '' AS OperationType,
             '' AS TaskStatus,
             RPD.PostedOn RecentlyPostedDate,
             getdate () PostingDateTime,
             M.Chargedas 'DueHours',
             '1' AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '6' MeterSerialNo,
             '' BerthName
        FROM SuppDryDock SD
             INNER JOIN MaterialCodeMaster M
                ON     SD.DockPortCode = M.PortCode
                   AND SD.DockQuayCode = M.QuayCode
                   AND SD.DockBerthCode = M.BerthCode
                   AND M.ChargedFor = 'UNDK'
             INNER JOIN MaterialCodePort MP
                ON MP.MaterialCodeMasterid = M.MaterialCodeMasterid
             LEFT JOIN RevenuePostingDtl RPD
                ON     RPD.vcn = SD.VCN
                   AND rpd.GroupCode = M.GroupCode
                   AND rpd.MaterialCode = M.MaterialCode
             LEFT JOIN RevenuePosting RPH
                ON     RPD.RevenuePostingID = RPH.RevenuePostingID
                   AND RPD.VCN = SD.VCN
       WHERE     SD.VCN = @VCN                -- 'VCNDB2014190' 'VCNDB2014289'
             AND MP.PortCode = @portcode
             AND SD.LeftDockDateTime IS NOT NULL
      UNION
      -- SUPPLIMANTORY SERVICE FOR WATER AND FLOOTING CRANES
      SELECT isnull (RPD.RevenuePostingDtlID, '0') ISPOSTED,
             a.ResourceAllocationID,
             sup.VCN,
             b.ServiceTypeName AS MovementName,
             'SUPPLIMANT' AS ServiceName,
             M.GroupCode,
             M.MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             RPH.SAPAccNo AccountNo,
             oth.StartTime AS StartTime,
             oth.EndTime AS Endtime,
             M.IsCalculated,
             M.Chargedas,
             M.UOM,
             '' AS MovementType,
             a.OperationType AS ServiceType,
             '' AS ServiceReferenceType,
             a.OperationType AS OperationType,
             '' AS TaskStatus,
             RPD.PostedOn RecentlyPostedDate,
             getdate () PostingDateTime,
             CASE
                WHEN (a.OperationType = 'WTST')
                THEN
				coalesce(oth.TotalDispensed,0)
                   --Issue : IS-4766 - The incorrect Units are being reflected on Marine Posting Screen&Please check and resolve 
				   ---Repeating sum of TotalDispensed to multiple recording, hence commented
				   
				   -- (SELECT isnull (sum (oth.TotalDispensed), 0)
                      -- FROM OtherServiceRecording oth,
                           -- dbo.SuppServiceRequest sup
                     -- WHERE     oth.ResourceAllocationID =
                                  -- a.ResourceAllocationID
                           -- AND sup.SuppServiceRequestID =
                                  -- a.ServiceReferenceID
                    -- GROUP BY oth.ResourceAllocationID)
					
                --isnull(oth.ClosingMeterReading,0)-isnull(oth.OpeningMeterReading,0)
                WHEN (a.OperationType = 'FCST')
                THEN
                   ceiling (DATEDIFF (mi, oth.StartTime, oth.EndTime) / 60.0)
             END
                AS 'DueHours',
             '0' AS 'TotalHours',
             isnull (oth.ClosingMeterReading, 0) AS CloseMterReding,
             isnull (oth.OpeningMeterReading, 0) AS startmtrreding,
             isnull (oth.MeterSerialNo, ' ') AS MeterSerialNo,
             BT.BerthName
        FROM dbo.ResourceAllocation a
             INNER JOIN ServiceType b ON b.ServiceTypeCode = a.OperationType
             INNER JOIN dbo.SuppServiceRequest sup
                ON sup.SuppServiceRequestID = a.ServiceReferenceID
             INNER JOIN OtherServiceRecording oth
                ON oth.ResourceAllocationID = a.ResourceAllocationID
             INNER JOIN MaterialCodeMaster M
                ON M.ServiceType = a.OperationType
             INNER JOIN MaterialCodePort MP
                ON MP.MaterialCodeMasterid = M.MaterialCodeMasterid
             LEFT JOIN dbo.Berth BT
                ON     BT.PortCode = sup.PortCode
                   AND BT.QuayCode = sup.QuayCode
                   AND BT.BerthCode = sup.BerthCode
             LEFT JOIN RevenuePostingDtl RPD
                ON     RPD.vcn = sup.VCN
                   AND rpd.GroupCode = M.GroupCode
                   AND rpd.MaterialCode = M.MaterialCode
                   AND RPD.ReferenceID = a.ResourceAllocationID
             LEFT JOIN RevenuePosting RPH
                ON     RPD.RevenuePostingID = RPH.RevenuePostingID
                   AND RPD.VCN = sup.VCN
       WHERE     a.ServiceReferenceType = 'SUPP'
             AND (a.TaskStatus = 'COMP' OR a.TaskStatus = 'VERF')
             AND sup.VCN = @VCN               -- 'VCNDB2014226' 'VCNDB2014289'
             AND MP.PortCode = @portcode
      UNION
      
      SELECT isnull (RPD.RevenuePostingDtlID, '0') ISPOSTED,
             a.ResourceAllocationID,
             sup.VCN,
             b.ServiceTypeName AS MovementName,
             'SUPPLIMANT' AS ServiceName,
             M.GroupCode,
             M.MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             RPH.SAPAccNo AccountNo,
             oth.StartTime AS StartTime,
             oth.EndTime AS Endtime,
             M.IsCalculated,
             M.Chargedas,
             M.UOM,
             '' AS MovementType,
             a.OperationType AS ServiceType,
             '' AS ServiceReferenceType,
             a.OperationType AS OperationType,
             '' AS TaskStatus,
             RPD.PostedOn RecentlyPostedDate,
             getdate () PostingDateTime,
             M.Chargedas AS 'DueHours',
             '1' AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '' MeterSerialNo,
             '' BerthName
        FROM dbo.SuppServiceRequest sup
             INNER JOIN dbo.ResourceAllocation a
                ON     sup.SuppServiceRequestID = a.ServiceReferenceID
                   AND a.ResourceAllocationID =
                          (SELECT TOP (1) RAA.ResourceAllocationID
                             FROM ResourceAllocation RAA
                            WHERE     RAA.ServiceReferenceID =
                                         sup.SuppServiceRequestID
                                  AND RAA.ServiceReferenceType = 'SUPP'
                           ORDER BY ResourceAllocationID ASC)
             INNER JOIN ServiceType b ON b.ServiceTypeCode = a.OperationType
             INNER JOIN OtherServiceRecording oth
                ON oth.ResourceAllocationID = a.ResourceAllocationID
             INNER JOIN MaterialCodeMaster M
                ON M.ChargedFor = a.OperationType
             INNER JOIN MaterialCodePort MP
                ON MP.MaterialCodeMasterid = M.MaterialCodeMasterid
             LEFT JOIN RevenuePostingDtl RPD
                ON     RPD.vcn = sup.VCN
                   AND rpd.GroupCode = M.GroupCode
                   AND rpd.MaterialCode = M.MaterialCode
                   AND RPD.ReferenceID = a.ResourceAllocationID
             LEFT JOIN RevenuePosting RPH
                ON     RPD.RevenuePostingID = RPH.RevenuePostingID
                   AND RPD.VCN = sup.VCN
       WHERE     a.ServiceReferenceType = 'SUPP'
             AND (a.TaskStatus = 'COMP' OR a.TaskStatus = 'VERF')
             AND sup.VCN = @VCN               -- 'VCNDB2014226' 'VCNDB2014289'
             AND MP.PortCode = @portcode
      UNION
      -- HOT WORK PERMITS
      SELECT isnull (RPD.RevenuePostingDtlID, '0') ISPOSTED,
             s.SuppServiceRequestID ResourceAllocationID,
             S.VCN,
             SB.SubCatName AS MovementName,
             'SUPPLIMANT' AS ServiceName,
             M.GroupCode,
             M.MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             RPH.SAPAccNo AccountNo,
             S.FromDate AS StartTime,
             S.ToDate AS Endtime,
             M.IsCalculated,
             M.Chargedas,
             M.UOM,
             '' AS MovementType,
             M.ServiceType AS ServiceType,
             '' AS ServiceReferenceType,
             M.ChargedFor AS OperationType,
             '' AS TaskStatus,
             RPD.PostedOn RecentlyPostedDate,
             getdate () PostingDateTime,
             M.Chargedas AS 'DueHours',
             '1' AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '' MeterSerialNo,
             '' BerthName
        FROM dbo.SuppServiceRequest S
             INNER JOIN SuppHotWorkInspection H
                ON S.SuppServiceRequestID = h.SuppServiceRequestID
             INNER JOIN SubCategory SB ON SB.SubCatCode = S.ServiceType
             INNER JOIN MaterialCodeMaster M ON M.ServiceType = S.ServiceType
             INNER JOIN MaterialCodePort MP
                ON MP.MaterialCodeMasterid = M.MaterialCodeMasterid
             LEFT JOIN RevenuePostingDtl RPD
                ON     RPD.vcn = S.VCN
                   AND rpd.GroupCode = M.GroupCode
                   AND rpd.MaterialCode = M.MaterialCode
                   AND RPD.ReferenceID = s.SuppServiceRequestID
             LEFT JOIN RevenuePosting RPH
                ON RPD.RevenuePostingID = RPH.RevenuePostingID
       --                             and RPD.VCN = S.VCN
       WHERE     S.ServiceType = 'HWST'
             AND M.ChargedFor = 'HWRK'
             AND s.VCN = @VCN
             AND MP.PortCode = @portcode
      ------
      UNION
      -- DRY DOCK ELECTRICITY CONNECTION FEES
      SELECT isnull (RPD.RevenuePostingDtlID, '0') ISPOSTED,
             MS.SuppMiscServiceID ResourceAllocationID,
             SD.VCN,
             'DRYDOCKMISCSERVICE' AS MovementName,
             upper (M.MaterialDescription) AS ServiceName,
             M.GroupCode,
             M.MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             RPH.SAPAccNo AS AccountNo,
             MS.FromDateTime AS StartTime,
             MS.ToDateTime AS Endtime,
             M.IsCalculated AS IsCalculated,
             M.Chargedas,
             M.UOM,
             '' AS MovementType,
             '' AS ServiceType,
             '' AS ServiceReferenceType,
             '' AS OperationType,
             '' AS TaskStatus,
             RPD.PostedOn RecentlyPostedDate,
             getdate () PostingDateTime,
             M.Chargedas 'DueHours',
             M.Chargedas AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '' MeterSerialNo,
             '' BerthName
        FROM SuppDryDock SD
             INNER JOIN SuppMiscService MS
                ON SD.SuppDryDockid = MS.SuppDryDockID
             INNER JOIN MaterialCodeMaster M
                ON     SD.DockPortCode = M.PortCode
                   AND SD.DockQuayCode = M.QuayCode
                   AND SD.DockBerthCode = M.BerthCode
                   AND M.ChargedFor = 'ELCT'
                   AND MS.ServiceTypeCode = M.ServiceType
             INNER JOIN MaterialCodePort MP
                ON MP.MaterialCodeMasterid = M.MaterialCodeMasterid
             LEFT JOIN RevenuePostingDtl RPD
                ON     RPD.vcn = SD.VCN
                   AND rpd.GroupCode = M.GroupCode
                   AND rpd.MaterialCode = M.MaterialCode
                   AND RPD.ReferenceID = MS.SuppMiscServiceID
             LEFT JOIN RevenuePosting RPH
                ON     RPD.RevenuePostingID = RPH.RevenuePostingID
                   AND RPD.VCN = SD.VCN
       WHERE SD.VCN = @VCN                    -- 'VCNDB2014226' 'VCNDB2014289'
                          AND MP.PortCode = @portcode
      UNION
      -- DRY DOCK ELECTRICITY DAY HAIR
      SELECT isnull (RPD.RevenuePostingDtlID, '0') ISPOSTED,
             MS.SuppMiscServiceID ResourceAllocationID,
             SD.VCN,
             'DRYDOCKMISCSERVICE' AS MovementName,
             upper (M.MaterialDescription) AS ServiceName,
             M.GroupCode,
             M.MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             RPH.SAPAccNo AS AccountNo,
             MS.FromDateTime AS StartTime,
             MS.ToDateTime AS Endtime,
             M.IsCalculated AS IsCalculated,
             M.Chargedas,
             M.UOM,
             '' AS MovementType,
             '' AS ServiceType,
             '' AS ServiceReferenceType,
             '' AS OperationType,
             '' AS TaskStatus,
             RPD.PostedOn RecentlyPostedDate,
             getdate () PostingDateTime,
             ceiling (DATEDIFF (mi, MS.FromDateTime, MS.ToDateTime) / 1440.0)
                AS 'DueHours',
             ceiling (DATEDIFF (mi, MS.FromDateTime, MS.ToDateTime) / 1440.0)
                AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '' MeterSerialNo,
             '' BerthName
        FROM SuppDryDock SD
             INNER JOIN SuppMiscService MS
                ON SD.SuppDryDockid = MS.SuppDryDockID
             INNER JOIN MaterialCodeMaster M
                ON     SD.DockPortCode = M.PortCode
                   AND SD.DockQuayCode = M.QuayCode
                   AND SD.DockBerthCode = M.BerthCode
                   AND M.ChargedFor = 'ELPD'
                   AND MS.ServiceTypeCode = M.ServiceType
             INNER JOIN MaterialCodePort MP
                ON MP.MaterialCodeMasterid = M.MaterialCodeMasterid
             LEFT JOIN RevenuePostingDtl RPD
                ON     RPD.vcn = SD.VCN
                   AND rpd.GroupCode = M.GroupCode
                   AND rpd.MaterialCode = M.MaterialCode
                   AND RPD.ReferenceID = MS.SuppMiscServiceID
             LEFT JOIN RevenuePosting RPH
                ON     RPD.RevenuePostingID = RPH.RevenuePostingID
                   AND RPD.VCN = SD.VCN
       WHERE SD.VCN = @VCN                    -- 'VCNDB2014226' 'VCNDB2014289'
                          AND MP.PortCode = @portcode
      UNION
      -- DRY DOCK ELECTRICITY USAGE CHARGES
      SELECT isnull (RPD.RevenuePostingDtlID, '0') ISPOSTED,
             MS.SuppMiscServiceID ResourceAllocationID,
             SD.VCN,
             'DRYDOCKMISCSERVICE' AS MovementName,
             upper (M.MaterialDescription) AS ServiceName,
             M.GroupCode,
             M.MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             RPH.SAPAccNo AS AccountNo,
             MS.FromDateTime AS StartTime,
             MS.ToDateTime AS Endtime,
             M.IsCalculated AS IsCalculated,
             M.Chargedas AS Chargedas,
             M.UOM AS UOM,
             '' AS MovementType,
             '' AS ServiceType,
             '' AS ServiceReferenceType,
             '' AS OperationType,
             '' AS TaskStatus,
             RPD.PostedOn RecentlyPostedDate,
             getdate () PostingDateTime,
             MS.Quantity AS 'DueHours',
             MS.Quantity AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '' MeterSerialNo,
             '' BerthName
        FROM SuppDryDock SD
             INNER JOIN SuppMiscService MS
                ON SD.SuppDryDockid = MS.SuppDryDockID
             INNER JOIN MaterialCodeMaster M
                ON     SD.DockPortCode = M.PortCode
                   AND SD.DockQuayCode = M.QuayCode
                   AND SD.DockBerthCode = M.BerthCode
                   AND M.ChargedFor = 'ELMT'
                   AND MS.ServiceTypeCode = M.ServiceType
             INNER JOIN MaterialCodePort MP
                ON MP.MaterialCodeMasterid = M.MaterialCodeMasterid
             LEFT JOIN RevenuePostingDtl RPD
                ON     RPD.vcn = SD.VCN
                   AND rpd.GroupCode = M.GroupCode
                   AND rpd.MaterialCode = M.MaterialCode
                   AND RPD.ReferenceID = MS.SuppMiscServiceID
             LEFT JOIN RevenuePosting RPH
                ON     RPD.RevenuePostingID = RPH.RevenuePostingID
                   AND RPD.VCN = SD.VCN
       WHERE SD.VCN = @VCN                    -- 'VCNDB2014226' 'VCNDB2014289'
                          AND MP.PortCode = @portcode
      UNION
      -- DRY DOCK FRESH WATER
      SELECT isnull (RPD.RevenuePostingDtlID, '0') ISPOSTED,
             MS.SuppMiscServiceID ResourceAllocationID,
             SD.VCN,
             'DRYDOCKMISCSERVICE' AS MovementName,
             upper (M.MaterialDescription) AS ServiceName,
             M.GroupCode,
             M.MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             RPH.SAPAccNo AS AccountNo,
             MS.FromDateTime AS StartTime,
             MS.ToDateTime AS Endtime,
             M.IsCalculated AS IsCalculated,
             M.Chargedas AS Chargedas,
             M.UOM AS UOM,
             '' AS MovementType,
             '' AS ServiceType,
             '' AS ServiceReferenceType,
             '' AS OperationType,
             '' AS TaskStatus,
             RPD.PostedOn RecentlyPostedDate,
             getdate () PostingDateTime,
             MS.Quantity AS 'DueHours',
             MS.Quantity AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '' MeterSerialNo,
             '' BerthName
        FROM SuppDryDock SD
             INNER JOIN SuppMiscService MS
                ON SD.SuppDryDockid = MS.SuppDryDockID
             INNER JOIN MaterialCodeMaster M
                ON     SD.DockPortCode = M.PortCode
                   AND SD.DockQuayCode = M.QuayCode
                   AND SD.DockBerthCode = M.BerthCode
                   AND M.ChargedFor = 'DOWT'
                   AND MS.ServiceTypeCode = M.ServiceType
             INNER JOIN MaterialCodePort MP
                ON MP.MaterialCodeMasterid = M.MaterialCodeMasterid
             LEFT JOIN RevenuePostingDtl RPD
                ON     RPD.vcn = SD.VCN
                   AND rpd.GroupCode = M.GroupCode
                   AND rpd.MaterialCode = M.MaterialCode
                   AND RPD.ReferenceID = MS.SuppMiscServiceID
             LEFT JOIN RevenuePosting RPH
                ON     RPD.RevenuePostingID = RPH.RevenuePostingID
                   AND RPD.VCN = SD.VCN
       WHERE SD.VCN = @VCN                    -- 'VCNDB2014226' 'VCNDB2014289'
                          AND MP.PortCode = @portcode
      UNION
      -- CHARGED FOR WHARF CRANE USAGE AT DRY DOCK/SHIP REPAIR
      SELECT isnull (RPD.RevenuePostingDtlID, '0') ISPOSTED,
             MS.SuppMiscServiceID ResourceAllocationID,
             SD.VCN,
             'DRYDOCKMISCSERVICE' AS MovementName,
             upper (M.MaterialDescription) AS ServiceName,
             M.GroupCode,
             M.MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             RPH.SAPAccNo AS AccountNo,
             MS.FromDateTime AS StartTime,
             MS.ToDateTime AS Endtime,
             M.IsCalculated AS IsCalculated,
             M.Chargedas,
             M.UOM,
             '' AS MovementType,
             '' AS ServiceType,
             '' AS ServiceReferenceType,
             '' AS OperationType,
             '' AS TaskStatus,
             RPD.PostedOn RecentlyPostedDate,
             getdate () PostingDateTime,
             ceiling (DATEDIFF (mi, MS.FromDateTime, MS.ToDateTime) / 60.0)
                AS 'DueHours',
             ceiling (DATEDIFF (mi, MS.FromDateTime, MS.ToDateTime) / 60.0)
                AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '' MeterSerialNo,
             '' BerthName
        FROM SuppDryDock SD
             INNER JOIN SuppMiscService MS
                ON SD.SuppDryDockid = MS.SuppDryDockID
             INNER JOIN MaterialCodeMaster M
                ON     SD.DockPortCode = M.PortCode
                   AND SD.DockQuayCode = M.QuayCode
                   AND SD.DockBerthCode = M.BerthCode
                   AND M.ChargedFor = 'WHAR'
                   AND MS.ServiceTypeCode = M.ServiceType
             INNER JOIN MaterialCodePort MP
                ON MP.MaterialCodeMasterid = M.MaterialCodeMasterid
             LEFT JOIN RevenuePostingDtl RPD
                ON     RPD.vcn = SD.VCN
                   AND rpd.GroupCode = M.GroupCode
                   AND rpd.MaterialCode = M.MaterialCode
                   AND RPD.ReferenceID = MS.SuppMiscServiceID
             LEFT JOIN RevenuePosting RPH
                ON     RPD.RevenuePostingID = RPH.RevenuePostingID
                   AND RPD.VCN = SD.VCN
       WHERE SD.VCN = @VCN                    -- 'VCNDB2014226' 'VCNDB2014289'
                          AND MP.PortCode = @portcode
      UNION
      -- CHARGED FOR COMPRESSED AIR
      SELECT isnull (RPD.RevenuePostingDtlID, '0') ISPOSTED,
             MS.SuppMiscServiceID ResourceAllocationID,
             SD.VCN,
             'DRYDOCKMISCSERVICE' AS MovementName,
             upper (M.MaterialDescription) AS ServiceName,
             M.GroupCode,
             M.MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             RPH.SAPAccNo AS AccountNo,
             MS.FromDateTime AS StartTime,
             MS.ToDateTime AS Endtime,
             M.IsCalculated AS IsCalculated,
             M.Chargedas,
             M.UOM,
             '' AS MovementType,
             '' AS ServiceType,
             '' AS ServiceReferenceType,
             '' AS OperationType,
             '' AS TaskStatus,
             RPD.PostedOn RecentlyPostedDate,
             getdate () PostingDateTime,
             ceiling (DATEDIFF (mi, MS.FromDateTime, MS.ToDateTime) / 60.0)
                AS 'DueHours',
             ceiling (DATEDIFF (mi, MS.FromDateTime, MS.ToDateTime) / 60.0)
                AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '' MeterSerialNo,
             '' BerthName
        FROM SuppDryDock SD
             INNER JOIN SuppMiscService MS
                ON SD.SuppDryDockid = MS.SuppDryDockID
             INNER JOIN MaterialCodeMaster M
                ON     SD.DockPortCode = M.PortCode
                   AND SD.DockQuayCode = M.QuayCode
                   AND SD.DockBerthCode = M.BerthCode
                   AND M.ChargedFor = 'CPAR'
                   AND MS.ServiceTypeCode = M.ServiceType
             INNER JOIN MaterialCodePort MP
                ON MP.MaterialCodeMasterid = M.MaterialCodeMasterid
             LEFT JOIN RevenuePostingDtl RPD
                ON     RPD.vcn = SD.VCN
                   AND rpd.GroupCode = M.GroupCode
                   AND rpd.MaterialCode = M.MaterialCode
                   AND RPD.ReferenceID = MS.SuppMiscServiceID
             LEFT JOIN RevenuePosting RPH
                ON     RPD.RevenuePostingID = RPH.RevenuePostingID
                   AND RPD.VCN = SD.VCN
       WHERE SD.VCN = @VCN                    -- 'VCNDB2014226' 'VCNDB2014289'
                          AND MP.PortCode = @portcode
      UNION
      -- NO EXTRA TUGS USED FOR DISPLAY
      SELECT '1' ISPOSTED,
             1 ResourceAllocationID,
             VCN,
             'DISPLAYONLY' AS MovementName,
             CONVERT (VARCHAR, concat ('No of Extra Tugs Used: ', extratugs))
                AS ServiceName,
             '11' GroupCode,
             '11' MaterialCode,
             upper (SubCatName) MaterialDescription,
             '11' AS AccountNo,
             AllocationDate AS StartTime,
             AllocationDate AS Endtime,
             'N' AS IsCalculated,
             '1' AS Chargedas,
             'Units' AS UOM,
             '' AS MovementType,
             '' AS ServiceType,
             '' AS ServiceReferenceType,
             '' AS OperationType,
             '' AS TaskStatus,
             getdate () RecentlyPostedDate,
             AllocationDate PostingDateTime,
             '0' AS 'DueHours',
             '0' AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '' MeterSerialNo,
             '' BerthName
        FROM (SELECT (  count (1)
                      - (SELECT TOP 1 TotalTugs
                           FROM ResourceAllocationConfigRule cr
                          WHERE     EffectedFrom <= RA.AllocationDate
                                AND portcode = @portcode
                         ORDER BY EffectedFrom DESC))
                        AS extratugs,
                     MT.SubCatName,
                     RA.ServiceReferenceID,
                     SR.VCN,
                     RA.AllocationDate
                FROM ResourceAllocation RA
                     INNER JOIN ServiceRequest SR
                        ON SR.ServiceRequestID = RA.ServiceReferenceID
                     INNER JOIN SubCategory MT
                        ON SR.MovementType = MT.SubCatCode
               WHERE     RA.ServiceReferenceType = 'VTSR'
                     AND RA.OperationType = 'TGWR'
                     AND SR.VCN = @VCN
              GROUP BY MT.SubCatName,
                       RA.ServiceReferenceID,
                       SR.VCN,
                       RA.AllocationDate) t
       WHERE extratugs > 0
      UNION
      -- MAIN ENGINE DISPLAY
      SELECT '1' ISPOSTED,
             1 ResourceAllocationID,
             VCN,
             'DISPLAYONLY' AS MovementName,
             'No Main Engine' AS ServiceName,
             '11' GroupCode,
             '11' MaterialCode,
             upper (SubCatName) MaterialDescription,
             '11' AS AccountNo,
             MovementDateTime AS StartTime,
             MovementDateTime AS Endtime,
             'N' AS IsCalculated,
             '1' AS Chargedas,
             'Units' AS UOM,
             '' AS MovementType,
             '' AS ServiceType,
             '' AS ServiceReferenceType,
             '' AS OperationType,
             '' AS TaskStatus,
             getdate () RecentlyPostedDate,
             MovementDateTime PostingDateTime,
             '0' AS 'DueHours',
             '0' AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '' MeterSerialNo,
             '' BerthName
        FROM (SELECT SR.VCN, MT.SubCatName, SR.MovementDateTime
                FROM ServiceRequest SR
                     INNER JOIN SubCategory MT
                        ON SR.MovementType = MT.SubCatCode
               WHERE SR.NoMainEngine = 'Y' AND SR.VCN = @VCN) t
      UNION
      -- WAITING BERTH MASTER DISPLAY
      SELECT '1' ISPOSTED,
             1 ResourceAllocationID,
             VCN,
             'DISPLAYONLY' AS MovementName,
             Reason AS ServiceName,
             '11' GroupCode,
             '11' MaterialCode,
             upper (srvname) MaterialDescription,
             '11' AS AccountNo,
             AllocationDate AS StartTime,
             AllocationDate AS Endtime,
             'N' AS IsCalculated,
             '1' AS Chargedas,
             'Units' AS UOM,
             '' AS MovementType,
             '' AS ServiceType,
             '' AS ServiceReferenceType,
             '' AS OperationType,
             '' AS TaskStatus,
             getdate () RecentlyPostedDate,
             AllocationDate PostingDateTime,
             '0' AS 'DueHours',
             '0' AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '' MeterSerialNo,
             '' BerthName
        FROM (SELECT sr.VCN,
                     concat (sb.SubCatName, ' - ', OT.SubCatName) srvname,
                     RA.AllocationDate,
                     --concat('Delay From: ', BT.WaitingStartTime ,' To: ' , BT.WaitingEndTime, '   ', BT.DelayReason) AS Reason
                     concat (
                        'Delay From: ',
                        dbo.udf_FormatDateTime (BT.WaitingStartTime,
                                                'YYYY-MM-DD'),
                        ' ',
                        dbo.udf_FormatDateTime (BT.WaitingStartTime,
                                                'HH:MM 24'),
                        ' To: ',
                        dbo.udf_FormatDateTime (BT.WaitingEndTime,
                                                'YYYY-MM-DD'),
                        ' ',
                        dbo.udf_FormatDateTime (BT.WaitingEndTime,
                                                'HH:MM 24'),
                        '   ',
                        BT.DelayReason)
                        AS Reason
                --BT.WaitingStartTime, BT.WaitingEndTime, BT.DelayReason
                FROM ResourceAllocation RA
                     INNER JOIN ServiceRequest SR
                        ON SR.ServiceRequestID = RA.ServiceReferenceID
                     INNER JOIN ShiftingBerthingTaskExecution BT
                        ON BT.ResourceAllocationID = RA.ResourceAllocationID
                     INNER JOIN SubCategory sb
                        ON sb.SubCatCode = SR.MovementType
                     INNER JOIN SubCategory OT
                        ON OT.SubCatCode = RA.OperationType
               WHERE     RA.ServiceReferenceType = 'VTSR'
                     AND SR.VCN = @VCN
                     AND BT.WaitingStartTime IS NOT NULL
                     AND BT.WaitingEndTime IS NOT NULL
                     AND (ra.taskstatus = 'COMP' OR RA.taskstatus = 'VERF'))
             t
      UNION
      -- WAITING PILOT DISPLAY

      SELECT '1' ISPOSTED,
             1 ResourceAllocationID,
             VCN,
             'DISPLAYONLY' AS MovementName,
             Reason AS ServiceName,
             '11' GroupCode,
             '11' MaterialCode,
             upper (srvname) MaterialDescription,
             '11' AS AccountNo,
             AllocationDate AS StartTime,
             AllocationDate AS Endtime,
             'N' AS IsCalculated,
             '1' AS Chargedas,
             'Units' AS UOM,
             '' AS MovementType,
             '' AS ServiceType,
             '' AS ServiceReferenceType,
             '' AS OperationType,
             '' AS TaskStatus,
             getdate () RecentlyPostedDate,
             AllocationDate PostingDateTime,
             '0' AS 'DueHours',
             '0' AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '' MeterSerialNo,
             '' BerthName
        FROM (SELECT sr.VCN,
                     concat (sb.SubCatName, ' - ', OT.SubCatName) srvname,
                     RA.AllocationDate,
                     -- concat('Delay From: ', BT.WaitingStartTime ,' To: ' , BT.WaitingEndTime, '   ', BT.DelayReason) AS Reason
                     concat (
                        'Delay From: ',
                        dbo.udf_FormatDateTime (BT.WaitingStartTime,
                                                'YYYY-MM-DD'),
                        ' ',
                        dbo.udf_FormatDateTime (BT.WaitingStartTime,
                                                'HH:MM 24'),
                        ' To: ',
                        dbo.udf_FormatDateTime (BT.WaitingEndTime,
                                                'YYYY-MM-DD'),
                        ' ',
                        dbo.udf_FormatDateTime (BT.WaitingEndTime,
                                                'HH:MM 24'),
                        '   ',
                        BT.DelayReason)
                        AS Reason
                --BT.WaitingStartTime, BT.WaitingEndTime, BT.DelayReason
                FROM ResourceAllocation RA
                     INNER JOIN ServiceRequest SR
                        ON SR.ServiceRequestID = RA.ServiceReferenceID
                     INNER JOIN PilotageServiceRecording BT
                        ON BT.ResourceAllocationID = RA.ResourceAllocationID
                     INNER JOIN SubCategory sb
                        ON sb.SubCatCode = SR.MovementType
                     INNER JOIN SubCategory OT
                        ON OT.SubCatCode = RA.OperationType
               WHERE     RA.ServiceReferenceType = 'VTSR'
                     AND SR.VCN = @VCN
                     AND BT.WaitingStartTime IS NOT NULL
                     AND BT.WaitingEndTime IS NOT NULL
                     AND (ra.taskstatus = 'COMP' OR RA.taskstatus = 'VERF'))
             t
      UNION
      -- WAITING TUG DISPLAY
      SELECT '1' ISPOSTED,
             1 ResourceAllocationID,
             VCN,
             'DISPLAYONLY' AS MovementName,
             Reason AS ServiceName,
             '11' GroupCode,
             '11' MaterialCode,
             upper (srvname) MaterialDescription,
             '11' AS AccountNo,
             AllocationDate AS StartTime,
             AllocationDate AS Endtime,
             'N' AS IsCalculated,
             '1' AS Chargedas,
             'Units' AS UOM,
             '' AS MovementType,
             '' AS ServiceType,
             '' AS ServiceReferenceType,
             '' AS OperationType,
             '' AS TaskStatus,
             getdate () RecentlyPostedDate,
             AllocationDate PostingDateTime,
             '0' AS 'DueHours',
             '0' AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '' MeterSerialNo,
             '' BerthName
        FROM (SELECT sr.VCN,
                     concat (sb.SubCatName, ' - ', OT.SubCatName) srvname,
                     RA.AllocationDate,
                     -- concat('Delay From: ', BT.WaitingStartTime ,' To: ' , BT.WaitingEndTime, '   ', BT.DelayReason) AS Reason
                     concat (
                        'Delay From: ',
                        dbo.udf_FormatDateTime (BT.WaitingStartTime,
                                                'YYYY-MM-DD'),
                        ' ',
                        dbo.udf_FormatDateTime (BT.WaitingStartTime,
                                                'HH:MM 24'),
                        ' To: ',
                        dbo.udf_FormatDateTime (BT.WaitingEndTime,
                                                'YYYY-MM-DD'),
                        ' ',
                        dbo.udf_FormatDateTime (BT.WaitingEndTime,
                                                'HH:MM 24'),
                        '   ',
                        BT.DelayReason)
                        AS Reason
                --BT.WaitingStartTime, BT.WaitingEndTime, BT.DelayReason
                FROM ResourceAllocation RA
                     INNER JOIN ServiceRequest SR
                        ON SR.ServiceRequestID = RA.ServiceReferenceID
                     INNER JOIN OtherServiceRecording BT
                        ON BT.ResourceAllocationID = RA.ResourceAllocationID
                     INNER JOIN SubCategory sb
                        ON sb.SubCatCode = SR.MovementType
                     INNER JOIN SubCategory OT
                        ON OT.SubCatCode = RA.OperationType
               WHERE     RA.ServiceReferenceType = 'VTSR'
                     AND SR.VCN = @VCN
                     AND BT.WaitingStartTime IS NOT NULL
                     AND BT.WaitingEndTime IS NOT NULL
                     AND (ra.taskstatus = 'COMP' OR RA.taskstatus = 'VERF'))
             t
      UNION
      -- PASSENGER BAGGAGE
      SELECT isnull (RPD.RevenuePostingDtlID, '0') ISPOSTED,
             SR.ServiceRequestid ResourceAllocationID,
             AN.VCN,
             SB.SubCatName AS MovementName,
             upper (M.MaterialDescription) AS ServiceName,
             M.GroupCode GroupCode,
             M.MaterialCode MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             CASE
                WHEN (RPD.RevenuePostingDtlID IS NULL) THEN ''
                WHEN (RPD.RevenuePostingDtlID IS NOT NULL) THEN RP.SAPAccNo
             END
                AS AccountNo,
             SR.MovementDateTime AS StartTime,
             SR.MovementDateTime AS Endtime,
             'N' AS IsCalculated,
             CONVERT (
                VARCHAR,
                  ISNULL (SR.PassengersEmbarking, 0)
                + ISNULL (SR.PassengersDisembarking, 0))
                AS Chargedas,
             M.UOM AS UOM,
             '' AS MovementType,
             '' AS ServiceType,
             '' AS ServiceReferenceType,
             '' AS OperationType,
             '' AS TaskStatus,
             RPD.PostedOn RecentlyPostedDate,
             getdate () PostingDateTime,
             CONVERT (
                VARCHAR,
                  ISNULL (SR.PassengersEmbarking, 0)
                + ISNULL (SR.PassengersDisembarking, 0))
                AS 'DueHours',
             '1' AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '' MeterSerialNo,
             '' BerthName
        FROM ArrivalNotification an
             INNER JOIN ServiceRequest SR ON an.vcn = sr.vcn
             INNER JOIN SubCategory SB ON SB.SubCatCode = SR.MovementType
             INNER JOIN MaterialCodePort MP ON an.PortCode = MP.PortCode
             INNER JOIN MaterialCodeMaster M
                ON     M.MaterialCodeMasterid = MP.MaterialCodeMasterid
                   AND M.ChargedFor = 'BAGG'
             LEFT JOIN RevenuePostingDtl RPD
                ON     RPD.vcn = AN.VCN
                   AND rpd.ReferenceID = SR.ServiceRequestid
                   AND rpd.GroupCode = M.GroupCode
                   AND rpd.MaterialCode = M.MaterialCode
             LEFT JOIN RevenuePosting RP
                ON RP.RevenuePostingID = RPD.RevenuePostingID
       WHERE     SR.MovementType = 'SGMV'
             AND (   ISNULL (SR.PassengersEmbarking, 0) > 0
                  OR ISNULL (SR.PassengersDisembarking, 0) > 0)
             AND AN.VCN = @VCN
      UNION
      -- PASSENGER LEVY
      SELECT isnull (RPD.RevenuePostingDtlID, '0') ISPOSTED,
             SR.ServiceRequestid ResourceAllocationID,
             AN.VCN,
             SB.SubCatName AS MovementName,
             upper (M.MaterialDescription) AS ServiceName,
             M.GroupCode GroupCode,
             M.MaterialCode MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             CASE
                WHEN (RPD.RevenuePostingDtlID IS NULL) THEN ''
                WHEN (RPD.RevenuePostingDtlID IS NOT NULL) THEN RP.SAPAccNo
             END
                AS AccountNo,
             SR.MovementDateTime AS StartTime,
             SR.MovementDateTime AS Endtime,
             'N' AS IsCalculated,
             CONVERT (
                VARCHAR,
                  ISNULL (SR.PassengersEmbarking, 0)
                + ISNULL (SR.PassengersDisembarking, 0))
                AS Chargedas,
             M.UOM AS UOM,
             '' AS MovementType,
             '' AS ServiceType,
             '' AS ServiceReferenceType,
             '' AS OperationType,
             '' AS TaskStatus,
             RPD.PostedOn RecentlyPostedDate,
             getdate () PostingDateTime,
             CONVERT (
                VARCHAR,
                  ISNULL (SR.PassengersEmbarking, 0)
                + ISNULL (SR.PassengersDisembarking, 0))
                AS 'DueHours',
             '1' AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '' MeterSerialNo,
             '' BerthName
        FROM ArrivalNotification an
             INNER JOIN ServiceRequest SR ON an.vcn = sr.vcn
             INNER JOIN SubCategory SB ON SB.SubCatCode = SR.MovementType
             INNER JOIN MaterialCodePort MP ON an.PortCode = MP.PortCode
             INNER JOIN MaterialCodeMaster M
                ON     M.MaterialCodeMasterid = MP.MaterialCodeMasterid
                   AND M.ChargedFor = 'LEVY'
             LEFT JOIN RevenuePostingDtl RPD
                ON     RPD.vcn = AN.VCN
                   AND rpd.ReferenceID = SR.ServiceRequestid
                   AND rpd.GroupCode = M.GroupCode
                   AND rpd.MaterialCode = M.MaterialCode
             LEFT JOIN RevenuePosting RP
                ON RP.RevenuePostingID = RPD.RevenuePostingID
       WHERE     SR.MovementType = 'SGMV'
             AND (   ISNULL (SR.PassengersEmbarking, 0) > 0
                  OR ISNULL (SR.PassengersDisembarking, 0) > 0)
             AND AN.VCN = @VCN
      UNION
      SELECT isnull (RPD.RevenuePostingDtlID, '0') ISPOSTED,
             VC.VesselCallMovementID ResourceAllocationID,
             VC.VCN,
             S.SubCatName AS MovementName,
             --SB1.SubCatName as ServiceName,
             upper (M.MaterialDescription) AS ServiceName,
             M.GroupCode,
             M.MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             CASE
                WHEN (RPD.RevenuePostingDtlID IS NULL) THEN ''
                WHEN (RPD.RevenuePostingDtlID IS NOT NULL) THEN RP.SAPAccNo
             END
                AS AccountNo,
             VC.ATB StartTime,
             VC.ATUB Endtime,
             M.IsCalculated,
             m.Chargedas,
             M.UOM,
             VC.MovementType,
             M.ServiceType,
             'VTSR' ServiceReferenceType,
             '' OperationType,
             '' TaskStatus,
             PostedOn RecentlyPostedDate,
             getdate () PostingDateTime,
             m.Chargedas AS 'DueHours',
             1 AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '' MeterSerialNo,
             '' BerthName
        FROM VesselCallMovement VC
             INNER JOIN ArrivalNotification AN ON AN.VCN = VC.VCN
             INNER JOIN MaterialCodeMaster M
                ON     m.PortCode = VC.FromPositionPortCode
                   AND M.QuayCode = VC.FromPositionQuayCode
                   AND M.BerthCode = VC.FromPositionBerthCode
                   AND M.MovementType = VC.MovementType
                   AND M.ChargedFor = 'WBCA'
             INNER JOIN MaterialCodePort MP
                ON     MP.MaterialCodeMasterid = M.MaterialCodeMasterid
                   AND MP.PortCode = @portcode
             INNER JOIN SubCategory S ON S.SubCatCode = VC.MovementType
             LEFT JOIN RevenuePostingDtl RPD
                ON     RPD.vcn = VC.VCN
                   AND rpd.ReferenceID = VC.VesselCallMovementID
                   AND rpd.GroupCode = M.GroupCode
                   AND rpd.MaterialCode = M.MaterialCode
             LEFT JOIN RevenuePosting RP
                ON RP.RevenuePostingID = RPD.RevenuePostingID
       WHERE VC.VCN = @VCN AND VC.MovementType = 'ARMV'
      UNION
      SELECT isnull (RPD.RevenuePostingDtlID, '0') ISPOSTED,
             (SELECT VesselCallMovementID
                FROM VesselCallMovement VC
               WHERE VC.VCN = @VCN AND VC.MovementType = 'SGMV')
                AS ResourceAllocationID,
             VC.VCN,
             S.SubCatName AS MovementName,
             --SB1.SubCatName as ServiceName,
             upper (M.MaterialDescription) AS ServiceName,
             M.GroupCode,
             M.MaterialCode,
             upper (M.MaterialDescription) MaterialDescription,
             CASE
                WHEN (RPD.RevenuePostingDtlID IS NULL) THEN ''
                WHEN (RPD.RevenuePostingDtlID IS NOT NULL) THEN RP.SAPAccNo
             END
                AS AccountNo,
             VC.ATB StartTime,
             VC.ATUB Endtime,
             M.IsCalculated,
             m.Chargedas,
             M.UOM,
             'SGMV' AS MovementType,
             M.ServiceType,
             'VTSR' ServiceReferenceType,
             '' OperationType,
             '' TaskStatus,
             PostedOn RecentlyPostedDate,
             getdate () PostingDateTime,
             m.Chargedas AS 'DueHours',
             m.Chargedas AS 'TotalHours',
             '0' CloseMterReding,
             '0' AS startmtrreding,
             '' MeterSerialNo,
             '' BerthName
        FROM VesselCallMovement VC
             INNER JOIN
             (SELECT TOP (1) VesselCallMovementID
                FROM VesselCallMovement nvc
               WHERE     NVC.VCN = @VCN
                     AND VesselCallMovementID <
                            (SELECT VesselCallMovementID
                               FROM VesselCallMovement VC
                              WHERE     VC.VCN = @VCN
                                    AND VC.MovementType = 'SGMV')
              ORDER BY 1 DESC) AS NWVCN
                ON NWVCN.VesselCallMovementID = VC.VesselCallMovementID
             INNER JOIN ArrivalNotification AN ON AN.VCN = VC.VCN
             INNER JOIN MaterialCodeMaster M
                ON     m.PortCode = VC.FromPositionPortCode
                   AND M.QuayCode = VC.FromPositionQuayCode
                   AND M.BerthCode = VC.FromPositionBerthCode
                   AND M.MovementType = VC.MovementType
                   AND M.ChargedFor = 'WBCA'
             INNER JOIN MaterialCodePort MP
                ON     MP.MaterialCodeMasterid = M.MaterialCodeMasterid
                   AND MP.PortCode = @portcode
             INNER JOIN SubCategory S ON S.SubCatCode = 'SGMV'
             LEFT JOIN RevenuePostingDtl RPD
                ON     RPD.vcn = VC.VCN
                   AND rpd.ReferenceID = VC.VesselCallMovementID
                   AND rpd.GroupCode = M.GroupCode
                   AND rpd.MaterialCode = M.MaterialCode
             LEFT JOIN RevenuePosting RP
                ON RP.RevenuePostingID = RPD.RevenuePostingID
       WHERE VC.VCN = @VCN
   END