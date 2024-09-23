Insert Into SubCategory
(SubCatCode, SupCatCode, SubCatName, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) VALUES
('WST', 'LIC', 'Waste Disposal', 'A', 2, getDate(), 2, getDate())

GO
Insert Into SuperCategory
(SupCatCode, SupCatName, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) VALUES
('MRPL', 'Marpol', 'A', 2, getDate(), 2, getDate())
GO
Insert Into SubCategory
(SubCatCode, SupCatCode, SubCatName, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) VALUES
('MRL1', 'MRPL', 'MARPOL Annex I –Oil', 'A', 2, getDate(), 2, getDate()),
('MRL2', 'MRPL', 'MARPOL Annex II – NLS (Noxious Liquid Substance)', 'A', 2, getDate(), 2, getDate()),
('MRL4', 'MRPL', 'MARPOL Annex IV – Sewage ', 'A', 2, getDate(), 2, getDate()),
('MRL5', 'MRPL', 'MARPOL Annex V – Garbage', 'A', 2, getDate(), 2, getDate()),
('MRL6', 'MRPL', 'MARPOL Annex VI – Air pollution Quantity (m3)', 'A', 2, getDate(), 2, getDate())
GO



CREATE TABLE dbo.Marpol(
ClassCode nvarchar(4) NOT NULL,
ClassName nvarchar(50) NOT NULL,
MarpolCode nvarchar(4) NOT NULL,
RecordStatus	nchar(1) NULL,
CreatedBy	int NOT NULL,
CreatedDate	datetime NOT NULL,
ModifiedBy	int NULL, 
ModifiedDate	datetime NOT NULL)
GO
ALTER TABLE Marpol
ADD CONSTRAINT PK_Marpol_ClassCode PRIMARY KEY (ClassCode);
GO
ALTER TABLE Marpol
ADD CONSTRAINT FK_Marpol_MarpolCode
FOREIGN KEY (MarpolCode) REFERENCES SubCategory(SubCatCode);
GO
ALTER TABLE Marpol
ADD CONSTRAINT FK_Marpol_CreatedBy
FOREIGN KEY (CreatedBy) REFERENCES Users(UserID);
GO
ALTER TABLE Marpol
ADD CONSTRAINT FK_Marpol_ModifiedBy
FOREIGN KEY (ModifiedBy) REFERENCES Users(UserID);
GO

Insert Into Marpol
(ClassCode, ClassName, MarpolCode,RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)  VALUES
('OIL1','Oily bilge water', 'MRL1', 'A', 2, getDate(), 2, getDate()),
('OIL2','Oily residues (sludge)', 'MRL1', 'A', 2, getDate(), 2, getDate()),
('OIL3','Oily tank washings', 'MRL1', 'A', 2, getDate(), 2, getDate()),
('OIL4','Dirty ballast water', 'MRL1', 'A', 2, getDate(), 2, getDate()),
('OIL5','Scale and sludge from tank cleaning', 'MRL1', 'A', 2, getDate(), 2, getDate()),
('OIL6','Other', 'MRL1', 'A', 2, getDate(), 2, getDate())
GO
Insert Into Marpol
(ClassCode, ClassName, MarpolCode,RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)  VALUES
('NLS1','Category X substance', 'MRL2', 'A', 2, getDate(), 2, getDate()),
('NLS2','Category Y substance', 'MRL2', 'A', 2, getDate(), 2, getDate()),
('NLS3','Category Z substance', 'MRL2', 'A', 2, getDate(), 2, getDate()),
('NLS4','OS – other substances', 'MRL2', 'A', 2, getDate(), 2, getDate())
GO
Insert Into Marpol
(ClassCode, ClassName, MarpolCode,RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)  VALUES
('SWGE','Sewage', 'MRL4', 'A', 2, getDate(), 2, getDate())
GO
Insert Into Marpol
(ClassCode, ClassName, MarpolCode,RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)  VALUES
('GRB1','Plastics', 'MRL5', 'A', 2, getDate(), 2, getDate()),
('GRB2','Food wastes','MRL5','A', 2, getDate(), 2, getDate()),
('GRB3','Domestic wastes', 'MRL5', 'A', 2, getDate(), 2, getDate()),
('GRB4','Cooking oil', 'MRL5', 'A', 2, getDate(), 2, getDate()),
('GRB5','Incinerator ashes', 'MRL5', 'A', 2, getDate(), 2, getDate()),
('GRB6','Operational wastes', 'MRL5', 'A', 2, getDate(), 2, getDate()),
('GRB7','Cargo residues', 'MRL5', 'A', 2, getDate(), 2, getDate()),
('GRB8','Animal carcass(es)', 'MRL5', 'A', 2, getDate(), 2, getDate()),
('GRB9','Fishing gear',  'MRL5', 'A', 2, getDate(), 2, getDate())
GO
Insert Into Marpol
(ClassCode, ClassName, MarpolCode,RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)  VALUES
('AIR1','Exhaust gas-cleaning residues', 'MRL6', 'A', 2, getDate(), 2, getDate()),
('AIR2','Ozone', 'MRL6', 'A', 2, getDate(), 2, getDate())





GO
CREATE TABLE [dbo].[WasteDeclaration] (
WasteDeclarationID int IDENTITY(1, 1) NOT NULL,
VCN nvarchar(12) NOT NULL,
MarpolCode nvarchar(4) NOT NULL,
ClassCode nvarchar(4) NOT NULL,
LicenseRequestID int NOT NULL,
Quantity numeric(14, 3) NULL,
DeclarationName nvarchar(50) NULL,
Others nvarchar(200) NULL,
RecordStatus	nchar(1) NULL,
CreatedBy	int NOT NULL,
CreatedDate	datetime NOT NULL,
ModifiedBy	int NULL, 
ModifiedDate	datetime NOT NULL)
GO
ALTER TABLE WasteDeclaration
ADD CONSTRAINT FK_WasteDeclaration_VCN
FOREIGN KEY (VCN) REFERENCES ArrivalNotification(VCN);
GO
ALTER TABLE WasteDeclaration
ADD CONSTRAINT FK_WasteDeclaration_LicenseRequestID
FOREIGN KEY (LicenseRequestID) REFERENCES LicenseRequest(LicenseRequestID);
GO
ALTER TABLE WasteDeclaration
ADD CONSTRAINT FK_WasteDeclaration_MarpolCode
FOREIGN KEY (MarpolCode) REFERENCES SubCategory(SubCatCode);
GO
ALTER TABLE WasteDeclaration
ADD CONSTRAINT FK_WasteDeclaration_ClassCode
FOREIGN KEY (ClassCode) REFERENCES Marpol(ClassCode);
GO
ALTER TABLE WasteDeclaration
ADD CONSTRAINT FK_WasteDeclaration_CreatedBy
FOREIGN KEY (CreatedBy) REFERENCES Users(UserID);
GO
ALTER TABLE WasteDeclaration
ADD CONSTRAINT FK_WasteDeclaration_ModifiedBy
FOREIGN KEY (ModifiedBy) REFERENCES Users(UserID);
GO


ALTER TABLE ArrivalNotification
ADD LastPortWasteDelivered datetime NULL
GO
ALTER TABLE ArrivalNotification
ADD NextPortWasteDelivery datetime NULL
GO
ALTER TABLE ArrivalNotification
ADD DateLastWasteDelivered datetime NULL
GO


INSERT INTO ENTITY
(EntityCode, Moduleid, Entityname, PageUrl, OrderNo, Tokens, HasWorkflow, HasMenuItem, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, PendingTaskColumns, ControllerName) VALUES
('MARPOL', (select ModuleID from Module where ModuleName = 'Masters'), 'Marpol', 'Marpol', 23, null, 'N', 'Y', 'A', 2, getDate(), 2,  getDate(), NULL, 'Marpol')
GO

Insert into ENTITYPRIVILEGE(ENTITYID, SUBCATCODE, RECORDSTATUS, CREATEDBY, CREATEDDATE, MODIFIEDBY, MODIFIEDDATE)
Values
((select entityid from entity where entitycode='MARPOL'), 'EDIT', 'A', 2, getDate(), 2, getDate()),
((select entityid from entity where entitycode='MARPOL'), 'VIEW', 'A', 2, getDate(), 2, getDate()),
((select entityid from entity where entitycode='MARPOL'), 'ADD', 'A',2, getDate(), 2, getDate())
GO


Insert into ROLEPRIVILEGE(ROLEID, ENTITYID, SUBCATCODE, RECORDSTATUS, CREATEDBY, CREATEDDATE, MODIFIEDBY, MODIFIEDDATE)
Values
((select roleid from role where rolecode='ADMN'), (select entityid from entity where entitycode='MARPOL'), 'VIEW', 'A',  2, getDate(), 2, getDate()),
((select roleid from role where rolecode='ADMN'), (select entityid from entity where entitycode='MARPOL'), 'EDIT', 'A',  2, getDate(), 2, getDate()),
((select roleid from role where rolecode='ADMN'), (select entityid from entity where entitycode='MARPOL'), 'ADD', 'A',  2, getDate(), 2, getDate())
GO


ALTER TABLE ArrivalNotification
DROP column LastPortWasteDelivered
GO
ALTER TABLE ArrivalNotification
DROP column NextPortWasteDelivery
GO

ALTER TABLE ArrivalNotification
ADD LastPortWasteDelivered nvarchar(5) NULL
GO
ALTER TABLE ArrivalNotification
ADD NextPortWasteDelivery nvarchar(5) NULL
GO

ALTER TABLE ArrivalNotification
ADD CONSTRAINT FK_ArrivalNotification_LastPortWasteDelivered
FOREIGN KEY (LastPortWasteDelivered) REFERENCES PortRegistry(PortCode);
GO
ALTER TABLE ArrivalNotification
ADD CONSTRAINT FK_ArrivalNotification_NextPortWasteDelivery
FOREIGN KEY (NextPortWasteDelivery) REFERENCES PortRegistry(PortCode);



