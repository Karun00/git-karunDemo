IF EXISTS
      (SELECT *
         FROM sys.objects
        WHERE     object_id =
                     OBJECT_ID (N'[dbo].[USP_CHANGE_OF_AGENT_UPDATION]')
              AND type IN (N'P'))
   DROP PROCEDURE [dbo].[USP_CHANGE_OF_AGENT_UPDATION]
GO
CREATE PROCEDURE [dbo].[USP_CHANGE_OF_AGENT_UPDATION]
AS
   BEGIN
      UPDATE VesselCall
         SET RecentAgentID = VCU.ProposedAgent
        FROM VesselCall VC
             INNER JOIN
             (SELECT VAC.VCN, VAC.ProposedAgent
                FROM VesselCall VC
                     INNER JOIN VesselAgentChange VAC ON VC.VCN = VAC.VCN
               WHERE     VAC.ProposedAgent <> VC.RecentAgentID
                     AND getdate () >= VAC.EffectiveDateTime
                     AND VAC.RecordStatus = 'A'
                     AND VAC.IsFinal = 'Y') VCU
                ON VC.VCN = VCU.VCN
--UPDATE Users set DormantStatus='N',LoginTime=getdate(),IsFirstTimeLogin='N' where DormantStatus='Y'
--UPDATE Users set ModifiedDate=getdate(),DormantStatus='Y' where coalesce(dateadd (dd, 30, coalesce(LoginTime,PwdExpirtyDate)),getdate()) <= getdate ()  

END