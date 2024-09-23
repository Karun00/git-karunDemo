


Insert Into WorkflowStep
(StepId, StepDescription) VALUES
(45, 'Cancel'),
(50, 'Confirmation Cancel')


Insert Into SubCategory
(SubCatCode, SupCatCode, SubCatName, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) VALUES
('WFCC', 'WFST', 'Confirmation Cancel', 'A', 2, getDate(), 2, getDate()),
('WSSA', 'WFST', 'Cancel Request Approve', 'A', 2, getDate(), 2, getDate()),
('WSRE', 'WFST', 'Cancel Request Reject', 'A', 2, getDate(), 2, getDate())

INSERT INTO WorkflowTask(EntityID, WorkflowTaskCode, Step, NextStep, ValidityPeriod, HasNotification, APIUrl, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, PortCode, HasRemarks) 
SELECT (select EntityId from Entity where EntityCode='SERVREQ') EntityId, 'WFCC' as WorkflowTaskCode, 45 Step,50 NextStep,0 ValidityPeriod, 'Y' HasNotification, '' APIUrl,'A' as RecordStatus,1 as CreatedBy,getdate() as CreatedDate,1 as ModifiedBy, getdate() as ModifiedDate , PortCode, 'N' HasRemarks from Port order by portcode



INSERT INTO WorkflowTask(EntityID, WorkflowTaskCode, Step, NextStep, ValidityPeriod, HasNotification, APIUrl, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, PortCode, HasRemarks) 
SELECT (select EntityId from Entity where EntityCode='SERVREQ') EntityId, 'WSSA' as WorkflowTaskCode, 50 Step,9999 NextStep,0 ValidityPeriod, 'Y' HasNotification, 'api/ServiceRequests/CancelApprove' APIUrl,'A' as RecordStatus,1 as CreatedBy,getdate() as CreatedDate,1 as ModifiedBy, getdate() as ModifiedDate , PortCode, 'N' HasRemarks from Port order by portcode

INSERT INTO WorkflowTask(EntityID, WorkflowTaskCode, Step, NextStep, ValidityPeriod, HasNotification, APIUrl, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, PortCode, HasRemarks) 
SELECT (select EntityId from Entity where EntityCode='SERVREQ') EntityId, 'WSRE' as WorkflowTaskCode, 50 Step, 30 NextStep,0 ValidityPeriod, 'Y' HasNotification, 'api/ServiceRequests/CancelReject' APIUrl,'A' as RecordStatus,1 as CreatedBy,getdate() as CreatedDate,1 as ModifiedBy, getdate() as ModifiedDate , PortCode, 'N' HasRemarks from Port order by portcode

INSERT INTO dbo.WorkflowTaskRole ( EntityID ,RoleID ,Step ,PortCode ,RecordStatus ,CreatedBy ,CreatedDate ,ModifiedBy ,ModifiedDate) SELECT (select EntityId from Entity where EntityCode='SERVREQ') EntityId, RoleId, 45 StepId, PortCode, 'A' RecordStatus, 1 CreatedBy, getdate() CreatedDate, 1 ModifiedBy, getdate() ModifiedDate from Port, ((select roleid from Role where Rolecode in (select value from udf_SplitString('AGNT',',')))) A Order By PortCode
INSERT INTO dbo.WorkflowTaskRole ( EntityID ,RoleID ,Step ,PortCode ,RecordStatus ,CreatedBy ,CreatedDate ,ModifiedBy ,ModifiedDate) SELECT (select EntityId from Entity where EntityCode='SERVREQ') EntityId, RoleId, 50 StepId, PortCode, 'A' RecordStatus, 1 CreatedBy, getdate() CreatedDate, 1 ModifiedBy, getdate() ModifiedDate from Port, ((select roleid from Role where Rolecode in (select value from udf_SplitString('VTC',',')))) A Order By PortCode
