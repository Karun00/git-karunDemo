IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetVoyageMonitoringDtls]') AND type in (N'P'))
DROP PROCEDURE [dbo].[usp_GetVoyageMonitoringDtls]
GO
CREATE PROCEDURE [dbo].[usp_GetVoyageMonitoringDtls]   
 @p_SearchText NVARCHAR(200),   
 @p_PortCode NVARCHAR(4),  
 @p_agentId Varchar(30)  
AS   
BEGIN   
SELECT DISTINCT
       vs.VesselID AS VesselID,
       an.VCN AS VCN,
       vs.VesselName AS VesselName,
       CONCAT (vs.VesselName, ' - ', an.VCN) VCNVesselName,
       an.VoyageIn AS VoyageIn,
       an.VoyageOut AS VoyageOut,
       (SELECT dbo.udf_GetArrivalReasonForVisit (an.VCN)) AS ReasonForVisit,
       sub2.SubCatName AS VesselType,
       vs.CallSign AS CallSign,
       an.ETA AS ETA,
       an.ETD AS ETD,
       vs.IMONo AS IMONo,
       vs.LengthOverallInM AS LengthOverallInM,
       vs.BeamInM AS BeamInM,
       sub1.SubCatName AS VesselNationality,
       vs.GrossRegisteredTonnageInMT AS GrossRegisteredTonnageInMT,
       vs.DeadWeightTonnageInMT AS DeadWeightTonnageInMT,
       (SELECT portname
          FROM PortRegistry WITH (NOLOCK)
         WHERE portcode = an.LastPortOfCall)
          AS LastPortOfCall,
       (SELECT portname
          FROM PortRegistry WITH (NOLOCK)
         WHERE portcode = an.NextPortOfCall)
          AS NextPortOfCall,
       CASE WHEN an.Tidal = 'Y' THEN 'Yes' ELSE 'No' END AS Tidal,
       CASE WHEN an.DaylightRestriction = 'Y' THEN 'Yes' ELSE 'No' END
          AS DaylightRestriction,
       an.NominationDate AS NominationDate,
       CASE WHEN an.ExceedPortLimitations = 'Y' THEN 'Yes' ELSE 'No' END
          AS PortRestriction,
       an.CargoDescription AS CargoType,
       vc.ATA AS ATA,
       vc.ATD AS ATD,
       t.RegisteredName AS Terminal,
       an.ArrDraft AS ArrDraft,
       an.DepDraft AS DepDraft,
       an.PortCode AS portcode
  FROM vessel vs
       INNER JOIN dbo.ArrivalNotification an
          ON an.VesselID = vs.VesselID AND an.PortCode = @p_PortCode
       INNER JOIN dbo.VesselCall vc ON vc.VCN = an.VCN
       LEFT JOIN dbo.terminaloperator t
          ON an.TerminalOperatorID = t.TerminalOperatorID
       LEFT JOIN dbo.SubCategory sub1
          ON vs.VesselNationality = sub1.SubCatCode
       LEFT JOIN dbo.SubCategory sub2 ON vs.VesselType = sub2.SubCatCode
 WHERE        an.PortCode = @p_PortCode
          AND (   UPPER (an.VCN) LIKE '%' + UPPER (@p_SearchText) + '%'
               OR UPPER (vs.VesselName) LIKE
                     '%' + UPPER (@p_SearchText) + '%')
          AND an.RecordStatus = 'A'
          AND (@p_agentId = 0 AND vc.RecentAgentID > 0)
       OR (@p_agentId <> 0 AND vc.RecentAgentID = @p_agentId)
ORDER BY an.VCN  
end