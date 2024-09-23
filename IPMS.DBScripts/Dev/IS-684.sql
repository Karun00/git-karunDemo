create PROCEDURE [dbo].[usp_rpt_AuditTrailMisReport]
         @AuditFromDate DATETIME ,
         @AuditToDate DATETIME 
         
AS
BEGIN

    -- Insert statements for procedure here
	   SELECT top 200   A.AuditTrailID AS AuditTrailID,
                A.AuditTrailConfigID AS AuditTrailConfigID,
                A.EntryORExit AS EntryORExit,
                A.UserID AS UserID,
                A.UserIPAddress AS UserIPAddress,
                A.UserName AS UserName,
                E.ControlerName AS ControllerName,
                E.ActionName AS ActionName,
                --E.UserFriendlyDescription AS UserFriendlyDescription,  
				case  
					when a.Parameters is null then E.UserFriendlyDescription
					else
						case  
							when a.Parameters <> '' then 
							--replace(replace(E.UserFriendlyDescription, '%Name%', substring(a.Parameters, 1, charindex('@', a.Parameters)-1)), '%Roles%', substring(a.Parameters, charindex('@', a.Parameters)+1, len(a.Parameters))) 
							REPLACE (
										  REPLACE(
												REPLACE(
												E.UserFriendlyDescription , '%Name%', SUBSTRING(a.Parameters,1, charindex('@', a.Parameters)-1)
												),'%Roles1%', substring(a.Parameters, charindex('@', a.Parameters)+1, (CHARINDEX('#',A.Parameters)-CHARINDEX('@',A.Parameters))-1)) 
											,'%User1%',substring(a.Parameters, charindex('#', a.Parameters)+1, LEN(a.Parameters))
								   )
						else ''
					end 
				end UserFriendlyDescription ,

                           IsSecurityAuditTrail,              
                CONVERT(VARCHAR(10), A.AuditDateTime, 111)+' '+ SUBSTRING(CONVERT(VARCHAR(16), A.AuditDateTime, 114), 1, 5)  AS AuditDateTime, A.UserComputerName
           FROM dbo.AuditTrail A
                INNER JOIN dbo.AuditTrailConfig E
                ON E.AuditTrailConfigID = A.AuditTrailConfigID
          WHERE E.IsAuditTrailRequired = 'Y' AND EntryORExit='ENTRY'
                  AND Convert(datetime,AuditDateTime)  between  Convert(datetime,@AuditFromDate)  AND  Convert(datetime,@AuditToDate) 
				 
          ORDER BY AuditDateTime DESC    
                  
END





--Entity table
GO
INSERT INTO [dbo].[Entity](EntityCode,Moduleid,Entityname,PageUrl,OrderNo,Tokens,HasWorkflow,HasMenuItem,RecordStatus,CreatedBy,
  CreatedDate,ModifiedBy,ModifiedDate,PendingTaskColumns,ControllerName) 
  VALUES('AUDTRAIL',(select ModuleID from [dbo].[Module] where ModuleName='Authorization Reports'),'Audit Trail Report','Report/AuditTrailMisReport',5,null,'N','Y','A',2,getdate(),2,getdate(),null,null)
--INSERT INTO [dbo].[Entity](EntityCode,Moduleid,Entityname,PageUrl,OrderNo,Tokens,HasWorkflow,HasMenuItem,RecordStatus,CreatedBy,
--  CreatedDate,ModifiedBy,ModifiedDate,PendingTaskColumns,ControllerName) 
--  VALUES('AUDTRAIL',44,'Audit Trail Report','Report/AuditTrailMisReport',5,null,'N','Y','A',2,getdate(),2,getdate(),null,null)

 
 --[EntityPrivilege] table
 GO
  insert into [dbo].[EntityPrivilege](EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
  values((select EntityID from [dbo].[Entity] where EntityCode='AUDTRAIL'),'ADD','A',1,getdate(),1,getdate())

  insert into [dbo].[EntityPrivilege](EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
  values((select EntityID from [dbo].[Entity] where EntityCode='AUDTRAIL'),'DEL','A',1,getdate(),1,getdate())

  insert into [dbo].[EntityPrivilege](EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
  values((select EntityID from [dbo].[Entity] where EntityCode='AUDTRAIL'),'EDIT','A',1,getdate(),1,getdate())

  insert into [dbo].[EntityPrivilege](EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
  values((select EntityID from [dbo].[Entity] where EntityCode='AUDTRAIL'),'VERF','A',1,getdate(),1,getdate())

  insert into [dbo].[EntityPrivilege](EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
  values((select EntityID from [dbo].[Entity] where EntityCode='AUDTRAIL'),'VIEW','A',1,getdate(),1,getdate())


  --[RolePrivilege] table
  GO
  insert into [dbo].[RolePrivilege](RoleID,EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
  values((select RoleId from [dbo].[Role] where RoleName='Admin'),(select EntityID from [dbo].[Entity] where EntityCode='AUDTRAIL'),'ADD','A',2,getdate(),2,getdate())
  insert into [dbo].[RolePrivilege](RoleID,EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
  values((select RoleId from [dbo].[Role] where RoleName='Admin'),(select EntityID from [dbo].[Entity] where EntityCode='AUDTRAIL'),'EDIT','A',2,getdate(),2,getdate())
  insert into [dbo].[RolePrivilege](RoleID,EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
  values((select RoleId from [dbo].[Role] where RoleName='Admin'),(select EntityID from [dbo].[Entity] where EntityCode='AUDTRAIL'),'VIEW','A',2,getdate(),2,getdate())

 