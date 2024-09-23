insert into SuperCategory(SupCatCode,SupCatName,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
select 'SSDC','Supplementary Services Document Category','A',(select UserID from users where UPPER(UserName)='ADMIN') CreatedBy,getdate(),(select UserID from users where UPPER(UserName)='ADMIN') ModifiedBy,getdate() ModifiedDate where 'SSDC' not in (select SupCatCode FROM SuperCategory)
Go
insert into SubCategory(SubCatCode,SupCatCode,SubCatName,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
select 'SGFC','SSDC','Gas Free Certificate','A',(select UserID from users where UPPER(UserName)='ADMIN') CreatedBy,getdate(),(select UserID from users where UPPER(UserName)='ADMIN') ModifiedBy,getdate() ModifiedDate where 'SGFC' not in (select SubCatCode FROM SubCategory)
GO
insert into SubCategory(SubCatCode,SupCatCode,SubCatName,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
Select 'SSOD','SSDC','Others','A',(select UserID from users where UPPER(UserName)='ADMIN') CreatedBy,getdate(),(select UserID from users where UPPER(UserName)='ADMIN') ModifiedBy,getdate() ModifiedDate where 'SSOD' not in (select SubCatCode FROM SubCategory)