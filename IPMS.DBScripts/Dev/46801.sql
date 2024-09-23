INSERT INTO SuperCategory(SupCatCode, SupCatName, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) SELECT 'MOPS', 'Mops Delay', 'A', 2, getDate(), 2, getDate() where 'MOPS' NOT IN (select SupCatCode from SuperCategory where SupCatCode = 'MOPS')
GO
Insert Into SubCategory(SubCatCode, SupCatCode, SubCatName, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
SELECT 'WEHR', 'MOPS', 'Weather', 'A', 2, getDate(), 1, getDate() where 'MOPS' NOT IN (select SupCatCode from SubCategory where SupCatCode = 'MOPS')
SELECT 'SHLI', 'MOPS', 'Shipping Line', 'A', 2, getDate(), 2, getDate() where 'MOPS' NOT IN (select SupCatCode from SubCategory where SupCatCode = 'MOPS')
SELECT 'PORT', 'MOPS', 'Port', 'A', 1, getDate(), 2, getDate() where 'MOPS' NOT IN (select SupCatCode from SubCategory where SupCatCode = 'MOPS')
SELECT 'MRSR', 'MOPS', 'Marine Service', 'A', 2, getDate(), 2, getDate()  where 'MOPS' NOT IN (select SupCatCode from SubCategory where SupCatCode = 'MOPS')
GO
IF NOT EXISTS (select column_name from INFORMATION_SCHEMA.columns where table_name = 'PilotageServiceRecording' and column_name = 'MOPSDelay')
begin
Alter table PilotageServiceRecording ADD MOPSDelay nvarchar(4) NULL;
end
GO
IF not EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'FK_PilotageServiceRecording_MOPSDelay')
   AND parent_object_id = OBJECT_ID(N'dbo.PilotageServiceRecording')
)
begin
  ALTER TABLE PilotageServiceRecording ADD CONSTRAINT FK_PilotageServiceRecording_MOPSDelay FOREIGN KEY (MOPSDelay) REFERENCES SUBCATEGORY (SUBCATCODE)
end
GO

