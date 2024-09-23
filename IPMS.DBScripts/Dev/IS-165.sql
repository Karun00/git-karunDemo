IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetVCNDetailsForServiceRequest]') AND type in (N'P'))
DROP PROCEDURE [dbo].[usp_GetVCNDetailsForServiceRequest]
GO
CREATE PROCEDURE [dbo].[usp_GetVCNDetailsForServiceRequest] 
@p_PortCode varchar(4), @p_AgentUserID int, @p_ToUserID int, @p_searchValue varchar(12) 
WITH EXEC AS CALLER 
AS 
BEGIN SET  NOCOUNT ON; 
 
SELECT an.VCN,
       vs.VesselID,
       vc.RecentAgentID,
       vs.VesselName,
       isnull (vesseleta.VoyageIn, an.VoyageIn) VoyageIn,
       an.VoyageOut,
       an.IsPHANFinal,
       an.IsISPSANFinal,
       an.IsIMDGANFinal,
       dbo.udf_GetArrivalReasonForVisit (an.VCN) AS ReasonForVisit,
       svt.SubCatName AS VesselType,
       vs.VesselType AS VesselTypeCode,
       vs.CallSign,
       vc.ETA,
       vc.ETD,
       vs.IMONo,
       CASE
          WHEN (vs.LengthOverallInM IS NULL) THEN 0
          ELSE vs.LengthOverallInM
       END
          AS LengthOverallInM,
       CASE WHEN (vs.BeamInM IS NULL) THEN 0 ELSE vs.BeamInM END AS BeamInM,
       an.ArrDraft,
       sc.SubCatName AS VesselNationality,
       vs.GrossRegisteredTonnageInMT,
       vs.DeadWeightTonnageInMT,
       por1.PortName AS LastPortOfCall,
       por2.PortName AS NextPortOfCall,
       CASE WHEN (an.Tidal = 'I') THEN 'No' ELSE 'Yes' END AS Tidal,
       CASE WHEN (an.IsSpecialNature = 'I') THEN 'No' ELSE 'Yes' END
          AS IsSpecialNature,
       CASE WHEN (an.DaylightRestriction = 'I') THEN 'No' ELSE 'Yes' END
          AS DaylightRestriction,
       CASE WHEN (an.PilotExemption = 'I') THEN 'No' ELSE 'Yes' END
          AS PilotExemption,
       agnt.RegisteredName,
       agnt.TelephoneNo1,
       agnt.FaxNo,
       cont.FirstName,
       cont.SurName,
       cont.CellularNo,
       cont.EmailID,
       CASE
          WHEN (an.AnyDangerousGoodsonBoard = 'I') THEN 'Not Binded'
          ELSE 'Yes'
       END
          AS AnyDangerousGoodsonBoard,
       CASE
          WHEN (an.DangerousGoodsClass IS NULL) THEN 'Not Binded'
          ELSE an.DangerousGoodsClass
       END
          AS DangerousGoodsClass,
       CASE WHEN (an.UNNo IS NULL) THEN 'Not Binded' ELSE an.UNNo END AS UNNo,
       ETUB = vc.ETUB,
       ETB = vc.ETB,
       ISNULL (curberth.BerthName, 'NA') CurrentBerth,
       ISNULL (curbollard.BollardName, 'NA') CurrentFromBollardName,
       ISNULL (tobollard.BollardName, 'NA') CurrentToBollardName,
       VC.ToPositionBollardCode,
       VC.FromPositionBollardCode,
       CASE
          WHEN ( (SELECT WorkflowTaskCode
                    FROM WorkflowInstance
                   WHERE     ReferenceID = an.VCN
                         AND EntityID = (SELECT EntityID
                                           FROM Entity
                                          WHERE EntityCode = 'DHMAN')) =
                   'WFSA')
          THEN
             'Yes'
          ELSE
             'No'
       END
          AS TidalStatus
  FROM dbo.ArrivalNotification an
       INNER JOIN dbo.Vessel vs
          ON     vs.VesselID = an.VesselID
             AND an.RecordStatus = 'A'
             AND an.PortCode = @p_PortCode
       JOIN SubCategory svt ON svt.SubCatCode = vs.VesselType
       JOIN dbo.VesselCall vc
          ON     vc.VCN = an.VCN
             AND (vc.ATUB IS NULL)
             AND (vc.ATD IS NULL)
             AND (UPPER (vc.VCN) LIKE '%' + UPPER (@p_searchValue) + '%')
       JOIN dbo.Agent agnt ON an.AgentID = agnt.AgentID
       JOIN dbo.AuthorizedContactPerson cont
          ON agnt.AuthorizedContactPersonID = cont.AuthorizedContactPersonID
       JOIN dbo.SubCategory sc ON vs.VesselNationality = sc.SubCatCode
       JOIN dbo.SubCategory subc ON an.ReasonForVisit = subc.SubCatCode
       JOIN dbo.PortRegistry por1 ON an.LastPortOfCall = por1.PortCode
       JOIN dbo.PortRegistry por2 ON an.NextPortOfCall = por2.PortCode
       LEFT JOIN
       (SELECT TOP (1)
               VoyageIn, VCN
          FROM VesselETAChange
         WHERE     VoyageIn != ''
               AND (UPPER (VCN) LIKE '%' + UPPER (@p_searchValue) + '%')
        ORDER BY VesselETAChangeID DESC) vesseleta
          ON vesseleta.VCN = an.VCN
       LEFT JOIN Berth curberth
          ON     curberth.BerthCode = vc.FromPositionBerthCode
             AND curberth.QuayCode = vc.FromPositionQuayCode
             AND curberth.PortCode = @p_PortCode
       LEFT JOIN Berth toberth
          ON     toberth.BerthCode = VC.ToPositionBerthCode
             AND toberth.QuayCode = VC.ToPositionQuayCode
             AND toberth.PortCode = @p_PortCode
       LEFT JOIN Bollard curbollard
          ON     curbollard.BollardCode = VC.FromPositionBollardCode
             AND curbollard.BerthCode = VC.FromPositionBerthCode
             AND curbollard.QuayCode = VC.FromPositionQuayCode
             AND curbollard.PortCode = @p_PortCode
       LEFT JOIN Bollard tobollard
          ON     tobollard.BollardCode = VC.ToPositionBollardCode
             AND tobollard.BerthCode = VC.ToPositionBerthCode
             AND tobollard.QuayCode = VC.ToPositionQuayCode
             AND tobollard.PortCode = @p_PortCode
 WHERE        an.PortCode = @p_PortCode
          AND an.IsANFinal = 'Y'
          AND an.RecordStatus = 'A'
          AND (@p_AgentUserID = 0 AND an.TerminalOperatorID = @p_ToUserID)
       OR (@p_AgentUserID <> 0 AND vc.RecentAgentID = @p_AgentUserID)
          
END