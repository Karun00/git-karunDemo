INSERT INTO ENTITY(EntityCode, Moduleid, Entityname, PageUrl, OrderNo, Tokens, HasWorkflow, HasMenuItem, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, PendingTaskColumns, ControllerName) VALUES
('SLOTOVR', (select ModuleID from Module where ModuleName = 'Port Operations' and ParentModuleID IS NOT NULL), 'Slots Overridden Report', 'Report/SlotsOveriddenReport', 20, null, 'N', 'Y', 'A', 2, getDate(), 2,  getDate(), NULL, NULL)
GO

Insert into ENTITYPRIVILEGE(ENTITYID, SUBCATCODE, RECORDSTATUS, CREATEDBY, CREATEDDATE, MODIFIEDBY, MODIFIEDDATE)
Values
((select entityid from entity where entitycode='SLOTOVR'), 'EDIT', 'A', 2, getDate(), 2, getDate()),
((select entityid from entity where entitycode='SLOTOVR'), 'VIEW', 'A', 2, getDate(), 2, getDate()),
((select entityid from entity where entitycode='SLOTOVR'), 'ADD', 'A',2, getDate(), 2, getDate())
GO


Insert into ROLEPRIVILEGE(ROLEID, ENTITYID, SUBCATCODE, RECORDSTATUS, CREATEDBY, CREATEDDATE, MODIFIEDBY, MODIFIEDDATE)
Values
((select roleid from role where rolecode='ADMN'), (select entityid from entity where entitycode='SLOTOVR'), 'VIEW', 'A',  2, getDate(), 2, getDate()),
((select roleid from role where rolecode='ADMN'), (select entityid from entity where entitycode='SLOTOVR'), 'EDIT', 'A',  2, getDate(), 2, getDate()),
((select roleid from role where rolecode='ADMN'), (select entityid from entity where entitycode='SLOTOVR'), 'ADD', 'A',  2, getDate(), 2, getDate())
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_rpt_GetSlotsOveridden]') AND type in (N'P'))
DROP PROCEDURE [dbo].[usp_rpt_GetSlotsOveridden]
GO
CREATE PROCEDURE [dbo].[usp_rpt_GetSlotsOveridden]
  @FromDate DATETIME, @ToDate DATETIME
   WITH
   EXECUTE AS CALLER
AS
   BEGIN  
   
   select VCM.[VCN],V.[VesselName],SOR.PreviousSlot,SOR.OverriddenSlot
     ,sc.SubCatName as 'Reason_for_Over_ride',SOR.EnteredDateAndTime as 'DateTime',U.[UserName] as 'User'
  from [dbo].[SlotOverRidingReasons] SOR 
       join [dbo].[VesselCallMovement] VCM on SOR.[VesselCallMovementID]=VCM.[VesselCallMovementID] 
       join [dbo].[ArrivalNotification] AN on AN.VCN=VCM.VCN
       join [dbo].[Vessel] V on AN.[VesselID]=V.[VesselID] join SubCategory SC on SOR.ReasonCode=SC.SubCatCode 
       join [dbo].[Users] U on [UserID]=SOR.ModifiedBy
       where CAST (SOR.EnteredDateAndTime AS DATETIME) BETWEEN @FromDate AND @ToDate

  END
GO
