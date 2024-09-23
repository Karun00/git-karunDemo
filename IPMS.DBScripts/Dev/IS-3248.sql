CREATE PROCEDURE [dbo].[usp_rpt_SAPPosting]
   @Port VARCHAR (50), @FromDate DATETIME, @ToDate DATETIME
   WITH
   EXECUTE AS CALLER
AS
   BEGIN
    
     SELECT 
     AN.VCN,
     V.VesselName,
     SP.CreatedDate	DateSubmitted,
      SP.Remarks ErrorReason 
  
FROM ArrivalNotification AN  
   INNER JOIN Vessel V ON V.VesselID = AN.VesselID
   INNER JOIN SAPPosting SP ON SP.ReferenceNo = AN.VCN    
      WHERE  SP.PostingStatus = 'SERR'    
       AND  (AN.PortCode = @Port OR @Port IS NULL) AND
     CAST (AN.ETA AS DATETIME) BETWEEN @FromDate AND @ToDate        
   ORDER BY AN.VCN 
END
GO

INSERT INTO [dbo].[Entity](EntityCode,Moduleid,Entityname,PageUrl,OrderNo,Tokens,HasWorkflow,HasMenuItem,RecordStatus,CreatedBy,
  CreatedDate,ModifiedBy,ModifiedDate,PendingTaskColumns,ControllerName) 
 VALUES('SAPPT',(select ModuleID from [dbo].[Module] where ModuleName='Management Reports'),'SAP Posting Report','Report/SAPPostingReport',11,null,'N','Y','A',2,getdate(),2,getdate(),null,null) 
  GO
  insert into [dbo].[EntityPrivilege](EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
  values((select EntityID from [dbo].[Entity] where EntityCode='SAPPT'),'ADD','A',1,getdate(),1,getdate())

  insert into [dbo].[EntityPrivilege](EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
  values((select EntityID from [dbo].[Entity] where EntityCode='SAPPT'),'DEL','A',1,getdate(),1,getdate())

  insert into [dbo].[EntityPrivilege](EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
  values((select EntityID from [dbo].[Entity] where EntityCode='SAPPT'),'EDIT','A',1,getdate(),1,getdate())

  insert into [dbo].[EntityPrivilege](EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
  values((select EntityID from [dbo].[Entity] where EntityCode='SAPPT'),'VERF','A',1,getdate(),1,getdate())

  insert into [dbo].[EntityPrivilege](EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
  values((select EntityID from [dbo].[Entity] where EntityCode='SAPPT'),'VIEW','A',1,getdate(),1,getdate())
  GO
  insert into [dbo].[RolePrivilege](RoleID,EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
  values((select RoleId from [dbo].[Role] where RoleName='Admin'),(select EntityID from [dbo].[Entity] where EntityCode='SAPPT'),'ADD','A',2,getdate(),2,getdate())
  insert into [dbo].[RolePrivilege](RoleID,EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
  values((select RoleId from [dbo].[Role] where RoleName='Admin'),(select EntityID from [dbo].[Entity] where EntityCode='SAPPT'),'EDIT','A',2,getdate(),2,getdate())
  insert into [dbo].[RolePrivilege](RoleID,EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
  values((select RoleId from [dbo].[Role] where RoleName='Admin'),(select EntityID from [dbo].[Entity] where EntityCode='SAPPT'),'VIEW','A',2,getdate(),2,getdate())

GO