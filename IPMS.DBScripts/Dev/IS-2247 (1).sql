
Insert Into Address
(AddressType, NumberStreet, Suburb, TownCity, PostalCode, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, CountryCode) VALUES
('ADPE', 'NA', 'NA', 'NA', '9999', 'A', 1, getDate(), 1, getDate(), 'HUN'),
('ADTE', 'NA', 'NA', 'NA', '9999', 'A', 1, getDate(), 1, getDate(), 'HUN')

GO
Insert Into AuthorizedContactPerson
(AuthorizedContactPersonType, FirstName, SurName, IdentityNo, Designation, CellularNo, EmailID, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) VALUES
('EMP', 'NA', 'NA', 'NA', 'NA', '9999999999999', 'NA@transnet.net', 'A', 2, getDate(), 2, getDate())
GO
Insert Into LicenseRequest
(LicenseRequestType, ReferenceNo, RegisteredName, TradingName, RegistrationNumber, VATNumber, IncomeTaxNumber, SkillsDevLevyNumber, BusinessAddressID, PostalAddressID, TelephoneNo1, TelephoneNo2, FaxNo, AuthorizedContactPersonID, ValidTaxClearanceCertificate, BBBEEStatus, VerifiedBBBEEStatus, BBBEEExemptedMicroEnterprise, PublicLiabilityInsurance, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) VALUES
('DIV', 'LIR201800002', 'Dormac', 'Dormac', 'NA', 'NA1', 'NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('DIV', 'LIR201800003', 'Subtech SA (Pty) Ltd', 'Subtech SA (Pty) Ltd', 'NA', 'NA2', 'NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('DIV', 'LIR201800004', 'TAG Diving Services (Pty) Ltd', 'TAG Diving Services (Pty) Ltd', 'NA', 'NA3', 'NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('DIV', 'LIR201800005', 'African Marine Solutions Group (Pty) Ltd trading as AMSOL', 'African Marine Solutions Group (Pty) Ltd trading as AMSOL', 'NA', 'NA', 'NA4', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('DIV', 'LIR201800006', 'Aqua-tech Diving Services CC', 'Aqua-tech Diving Services CC', 'NA', 'NA5', 'NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('DIV', 'LIR201800007', 'Katlantic (Pty) Ltd', 'Katlantic (Pty) Ltd', 'NA', 'NA6', 'NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('DIV', 'LIR201800008', 'Subtech Core Innovation (Pty) Ltd', 'Subtech Core Innovation (Pty) Ltd', 'NA', 'NA7', 'NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('DIV', 'LIR201800009', 'Guerrini Marine Construction CC', 'Guerrini Marine Construction CC', 'NA', 'NA8', 'NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('DIV', 'LIR201800010', 'GBAP Marine Services t/a Commercial Diving Contractors', 'GBAP Marine Services t/a Commercial Diving Contractors', 'NA', 'NA9', 'NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('DIV', 'LIR201800011', 'Cape Diving (Pty) Ltd', 'Cape Diving (Pty) Ltd', 'NA', 'NA10', 'NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('DIV', 'LIR201800012', 'RS Africa Diving (Pty) Ltd', 'RS Africa Diving (Pty) Ltd', 'NA', 'NA11', 'NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('DIV', 'LIR201800013', 'Atlatech Divers & Salvors CC', 'Atlatech Divers & Salvors CC', 'NA', 'NA12', 'NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('DIV', 'LIR201800014', 'DM Diving', 'DM Diving', 'NA', 'NA13', 'NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('DIV', 'LIR201800015', 'Aim Diving (Pty) Ltd', 'Aim Diving (Pty) Ltd', 'NA', 'NA14', 'NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('DIV', 'LIR201800016', 'Academy of Diving and Offshore Medicine CC t/a Sea Dog', 'Academy of Diving and Offshore Medicine CC t/a Sea Dog', 'NA', 'NA15', 'NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('DIV', 'LIR201800017', 'Saldanha Diving and Blasting Services CC', 'Saldanha Diving and Blasting Services CC', 'NA', 'NA16', 'NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate())

GO
Insert Into LicenseRequestPort
(LicenseRequestID, PortCode, WFStatus, VerifiedBy, VerifiedDate, ApprovedBy, ApprovedDate, RejectComments, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, WorkflowInstanceID)
SELECT LicenseRequestID, 'CT', 'WFSA', 2, getDate(), 2,  getDate(), NULL, 'A', 2,  getDate(), 2,  getDate(), 4
from (select LicenseRequestID FROM LicenseRequest where ReferenceNo IN ('LIR201800003','LIR201800004','LIR201800005','LIR201800009','LIR201800011','LIR201800012','LIR201800013','LIR201800014')) A;
GO

Insert Into LicenseRequestPort
(LicenseRequestID, PortCode, WFStatus, VerifiedBy, VerifiedDate, ApprovedBy, ApprovedDate, RejectComments, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, WorkflowInstanceID)
SELECT LicenseRequestID, 'DB', 'WFSA', 2, getDate(), 2,  getDate(), NULL, 'A', 2,  getDate(), 2,  getDate(), 4
from (select LicenseRequestID FROM LicenseRequest where ReferenceNo IN ('LIR201800002','LIR201800003','LIR201800004','LIR201800005','LIR201800006','LIR201800007','LIR201800008')) A;
GO

Insert Into LicenseRequestPort
(LicenseRequestID, PortCode, WFStatus, VerifiedBy, VerifiedDate, ApprovedBy, ApprovedDate, RejectComments, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, WorkflowInstanceID)
SELECT LicenseRequestID, 'EL', 'WFSA', 2, getDate(), 2,  getDate(), NULL, 'A', 2,  getDate(), 2,  getDate(), 4
from (select LicenseRequestID FROM LicenseRequest where ReferenceNo IN ('LIR201800003','LIR201800004','LIR201800009','LIR201800010')) A;
GO

Insert Into LicenseRequestPort
(LicenseRequestID, PortCode, WFStatus, VerifiedBy, VerifiedDate, ApprovedBy, ApprovedDate, RejectComments, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, WorkflowInstanceID)
SELECT LicenseRequestID, 'MB', 'WFSA', 2, getDate(), 2,  getDate(), NULL, 'A', 2,  getDate(), 2,  getDate(), 4
from (select LicenseRequestID FROM LicenseRequest where ReferenceNo IN ('LIR201800003','LIR201800010','LIR201800012')) A;
GO

Insert Into LicenseRequestPort
(LicenseRequestID, PortCode, WFStatus, VerifiedBy, VerifiedDate, ApprovedBy, ApprovedDate, RejectComments, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, WorkflowInstanceID)
SELECT LicenseRequestID, 'NG', 'WFSA', 2, getDate(), 2,  getDate(), NULL, 'A', 2,  getDate(), 2,  getDate(), 4
from (select LicenseRequestID FROM LicenseRequest where ReferenceNo IN ('LIR201800002','LIR201800003','LIR201800004','LIR201800010','LIR201800011','LIR201800012')) A;
GO

Insert Into LicenseRequestPort
(LicenseRequestID, PortCode, WFStatus, VerifiedBy, VerifiedDate, ApprovedBy, ApprovedDate, RejectComments, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, WorkflowInstanceID)
SELECT LicenseRequestID, 'PE', 'WFSA', 2, getDate(), 2,  getDate(), NULL, 'A', 2,  getDate(), 2,  getDate(), 4
from (select LicenseRequestID FROM LicenseRequest where ReferenceNo IN ('LIR201800002','LIR201800003','LIR201800010')) A;
GO

Insert Into LicenseRequestPort
(LicenseRequestID, PortCode, WFStatus, VerifiedBy, VerifiedDate, ApprovedBy, ApprovedDate, RejectComments, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, WorkflowInstanceID)
SELECT LicenseRequestID, 'RB', 'WFSA', 2, getDate(), 2,  getDate(), NULL, 'A', 2,  getDate(), 2,  getDate(), 4
from (select LicenseRequestID FROM LicenseRequest where ReferenceNo IN ('LIR201800002','LIR201800003','LIR201800004')) A;
GO

Insert Into LicenseRequestPort
(LicenseRequestID, PortCode, WFStatus, VerifiedBy, VerifiedDate, ApprovedBy, ApprovedDate, RejectComments, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, WorkflowInstanceID)
SELECT LicenseRequestID, 'SB', 'WFSA', 2, getDate(), 2,  getDate(), NULL, 'A', 2,  getDate(), 2,  getDate(), 4
from (select LicenseRequestID FROM LicenseRequest where ReferenceNo IN ('LIR201800003','LIR201800004','LIR201800005','LIR201800009','LIR201800012','LIR201800015','LIR201800016','LIR201800017')) A;
GO




Insert Into LicenseRequest
(LicenseRequestType, ReferenceNo, RegisteredName, TradingName, RegistrationNumber, VATNumber, IncomeTaxNumber, SkillsDevLevyNumber, BusinessAddressID, PostalAddressID, TelephoneNo1, TelephoneNo2, FaxNo, AuthorizedContactPersonID, ValidTaxClearanceCertificate, BBBEEStatus, VerifiedBBBEEStatus, BBBEEExemptedMicroEnterprise, PublicLiabilityInsurance, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) VALUES
('WST','LIR201800021', 'Africa Bunkering and Shipping CC','Africa Bunkering and Shipping CC', 'NA', 'NA21','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800022', 'Abaphumeleli Trading 651 CC, t/a Pollution Control Services','Abaphumeleli Trading 651 CC, t/a Pollution Control Services', 'NA', 'NA22','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800023', 'Compass Medical Waste Services (Pty) Ltd','Compass Medical Waste Services (Pty) Ltd', 'NA', 'NA23','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800024', 'Dolphin Coast Landfill Management (Pty) Ltd','Dolphin Coast Landfill Management (Pty) Ltd', 'NA', 'NA24','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800025', 'Endlovini General Services and Maintenance CC','Endlovini General Services and Maintenance CC', 'NA', 'NA25','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800026', 'Enviroserv Waste Management (Pty) Ltd','Enviroserv Waste Management (Pty) Ltd', 'NA', 'NA26','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800027', 'FFS Refiners (Pty) Ltd','FFS Refiners (Pty) Ltd', 'NA', 'NA27','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800028', 'MIB Waste Services CC','MIB Waste Services CC', 'NA', 'NA28','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800029', 'Spill Tech (Pty) Ltd','Spill Tech (Pty) Ltd', 'NA', 'NA29','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800030', 'Waco Africa (Pty) Ltd, t/a Sanitech','Waco Africa (Pty) Ltd, t/a Sanitech', 'NA', 'NA30','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800031', 'Averda South Africa (Pty) Ltd','Averda South Africa (Pty) Ltd', 'NA', 'NA31','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800032', 'Coalition Trading 1225 CC','Coalition Trading 1225 CC', 'NA', 'NA32','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800033', 'Commercial Waste Services','Commercial Waste Services', 'NA', 'NA33','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800034', 'Drizit Environmental CC','Drizit Environmental CC', 'NA', 'NA34','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800035', 'DRUMNET CC','DRUMNET CC', 'NA', 'NA35','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800036', 'Dynasty Ports International','Dynasty Ports International', 'NA', 'NA36','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800037', 'Ekapa Drum Reconditioners (Pty) ltd','Ekapa Drum Reconditioners (Pty) ltd', 'NA', 'NA37','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800038', 'Envirocare Marine Waste ','Envirocare Marine Waste ', 'NA', 'NA38','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800039', 'Enviroshore','Enviroshore', 'NA', 'NA39','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800040', 'Honeysucker Haulage CC','Honeysucker Haulage CC', 'NA', 'NA40','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800041', 'Northern Ocean Marine (Pty) Ltd','Northern Ocean Marine (Pty) Ltd', 'NA', 'NA41','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800042', 'Oil Separation Services (Pty) Ltd ','Oil Separation Services (Pty) Ltd ', 'NA', 'NA42','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800043', 'Oricol Environmental Services (Pty) Ltd','Oricol Environmental Services (Pty) Ltd', 'NA', 'NA43','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800044', 'Separating Waste Solutions CC','Separating Waste Solutions CC', 'NA', 'NA44','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800045', 'Siyaphambili Waste Services','Siyaphambili Waste Services', 'NA', 'NA45','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800046', 'Tiasat (Pty) Ltd t/a Supply Five Marine','Tiasat (Pty) Ltd t/a Supply Five Marine', 'NA', 'NA46','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800047', 'The Waste Group (Pty) Ltd','The Waste Group (Pty) Ltd', 'NA', 'NA47','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800048', 'Thekweni Marine Waste','Thekweni Marine Waste', 'NA', 'NA48','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800049', 'Pedal Trading 164 (Pty) Ltd t/a Wallace Bulk','Pedal Trading 164 (Pty) Ltd t/a Wallace Bulk', 'NA', 'NA49','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800050', 'Wastetrans CC','Wastetrans CC', 'NA', 'NA50','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800051', 'XP Ibhayi Environmental Specialist (Pty) Ltd t/a Xtreme Projects','XP Ibhayi Environmental Specialist (Pty) Ltd t/a Xtreme Projects', 'NA', 'NA51','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800052', 'Interwaste (Pty) Ltd ','Interwaste (Pty) Ltd ', 'NA', 'NA52','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800053', 'Marine Slops (Pty) Ltd','Marine Slops (Pty) Ltd', 'NA', 'NA53','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800054', 'Southey Contracting (a division of Southey Holdings (Pty) Ltd','Southey Contracting (a division of Southey Holdings (Pty) Ltd', 'NA', 'NA54','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800055', 'Waste Busters CC ','Waste Busters CC ', 'NA', 'NA55','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800056', 'Wasteman Holdings (Pty) Ltd ','Wasteman Holdings (Pty) Ltd ', 'NA', 'NA56','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800057', 'Brainwave Projects 2005 t/a Green Bin CC','Brainwave Projects 2005 t/a Green Bin CC', 'NA', 'NA57','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate()),
('WST','LIR201800058', 'World Focus 2025 CC','World Focus 2025 CC', 'NA', 'NA58','NA', 'NA', (select MAX(AddressID) from Address where AddressID < (select MAX(AddressID) from Address)), (select MAX(AddressID) from Address), '9999999999999', NULL, '9999999999999', (select MAX(AuthorizedContactPersonID) from AuthorizedContactPerson), 'Y', ' ', 'Y', 'Y', 'Y', 'A', 2, getDate(), 2, getDate())


GO

Insert Into LicenseRequestPort
(LicenseRequestID, PortCode, WFStatus, VerifiedBy, VerifiedDate, ApprovedBy, ApprovedDate, RejectComments, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, WorkflowInstanceID)
SELECT LicenseRequestID, 'CT', 'WFSA', 2, getDate(), 2,  getDate(), NULL, 'A', 2,  getDate(), 2,  getDate(), 4
from (select LicenseRequestID FROM LicenseRequest where ReferenceNo IN ('LIR201800021','LIR201800026','LIR201800027','LIR201800029','LIR201800049','LIR201800051','LIR201800052','LIR201800053','LIR201800054','LIR201800055','LIR201800056')) A;
GO

Insert Into LicenseRequestPort
(LicenseRequestID, PortCode, WFStatus, VerifiedBy, VerifiedDate, ApprovedBy, ApprovedDate, RejectComments, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, WorkflowInstanceID)
SELECT LicenseRequestID, 'DB', 'WFSA', 2, getDate(), 2,  getDate(), NULL, 'A', 2,  getDate(), 2,  getDate(), 4
from (select LicenseRequestID FROM LicenseRequest where ReferenceNo IN ('LIR201800021','LIR201800023','LIR201800024','LIR201800026','LIR201800027','LIR201800028','LIR201800029','LIR201800031','LIR201800032','LIR201800033','LIR201800034','LIR201800035','LIR201800036','LIR201800037','LIR201800038','LIR201800039','LIR201800040','LIR201800041','LIR201800042','LIR201800043','LIR201800044','LIR201800045','LIR201800046','LIR201800047','LIR201800048','LIR201800049','LIR201800050')) A;
GO

Insert Into LicenseRequestPort
(LicenseRequestID, PortCode, WFStatus, VerifiedBy, VerifiedDate, ApprovedBy, ApprovedDate, RejectComments, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, WorkflowInstanceID)
SELECT LicenseRequestID, 'EL', 'WFSA', 2, getDate(), 2,  getDate(), NULL, 'A', 2,  getDate(), 2,  getDate(), 4
from (select LicenseRequestID FROM LicenseRequest where ReferenceNo IN ('LIR201800029')) A;
GO

Insert Into LicenseRequestPort
(LicenseRequestID, PortCode, WFStatus, VerifiedBy, VerifiedDate, ApprovedBy, ApprovedDate, RejectComments, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, WorkflowInstanceID)
SELECT LicenseRequestID, 'MB', 'WFSA', 2, getDate(), 2,  getDate(), NULL, 'A', 2,  getDate(), 2,  getDate(), 4
from (select LicenseRequestID FROM LicenseRequest where ReferenceNo IN ('LIR201800027','LIR201800029','LIR201800052','LIR201800053')) A;
GO

Insert Into LicenseRequestPort
(LicenseRequestID, PortCode, WFStatus, VerifiedBy, VerifiedDate, ApprovedBy, ApprovedDate, RejectComments, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, WorkflowInstanceID)
SELECT LicenseRequestID, 'NG', 'WFSA', 2, getDate(), 2,  getDate(), NULL, 'A', 2,  getDate(), 2,  getDate(), 4
from (select LicenseRequestID FROM LicenseRequest where ReferenceNo IN ('LIR201800026','LIR201800027','LIR201800029','LIR201800051')) A;
GO

Insert Into LicenseRequestPort
(LicenseRequestID, PortCode, WFStatus, VerifiedBy, VerifiedDate, ApprovedBy, ApprovedDate, RejectComments, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, WorkflowInstanceID)
SELECT LicenseRequestID, 'PE', 'WFSA', 2, getDate(), 2,  getDate(), NULL, 'A', 2,  getDate(), 2,  getDate(), 4
from (select LicenseRequestID FROM LicenseRequest where ReferenceNo IN ('LIR201800026','LIR201800027','LIR201800029','LIR201800051')) A;
GO

Insert Into LicenseRequestPort
(LicenseRequestID, PortCode, WFStatus, VerifiedBy, VerifiedDate, ApprovedBy, ApprovedDate, RejectComments, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, WorkflowInstanceID)
SELECT LicenseRequestID, 'RB', 'WFSA', 2, getDate(), 2,  getDate(), NULL, 'A', 2,  getDate(), 2,  getDate(), 4
from (select LicenseRequestID FROM LicenseRequest where ReferenceNo IN ('LIR201800021','LIR201800022','LIR201800023','LIR201800024','LIR201800025','LIR201800026','LIR201800027','LIR201800028','LIR201800029','LIR201800030')) A;
GO

Insert Into LicenseRequestPort
(LicenseRequestID, PortCode, WFStatus, VerifiedBy, VerifiedDate, ApprovedBy, ApprovedDate, RejectComments, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, WorkflowInstanceID)
SELECT LicenseRequestID, 'SB', 'WFSA', 2, getDate(), 2,  getDate(), NULL, 'A', 2,  getDate(), 2,  getDate(), 4
from (select LicenseRequestID FROM LicenseRequest where ReferenceNo IN ('LIR201800026','LIR201800027','LIR201800029','LIR201800051','LIR201800053','LIR201800054','LIR201800057','LIR201800058')) A;
GO




