USE IPMSSEED
GO

insert into Port
(PortCode, PortName, InternationalCharacter, GeographicLocation, ContactNo, Email, Fax, Website, Description, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, IsTNPA, IsSA) VALUES
(N'RB', N'Richards Bay', N'NA', N'NULL', N'1521696582', N'richardsbay@gmail.com', N'7', N'www.richardsbay.com', N'NULL', N'I', 2, '12-Dec-2014 10:12:30 PM', 2, '12-Dec-2014 10:12:30 PM', NULL, NULL)

if not exists (select * from Employee where EmployeeID = 1) 
begin 
   set identity_insert Employee on
   insert into Employee
   (EmployeeID, PortCode, SAPNumber, FirstName, LastName, Initials, BirthDate, Age, JoiningDate, YearsofService, OfficialMobileNo, PersonalMobileNo, EmailID, Gender, Department, Designation, BusinessUnit, CostCenter, PayrollArea, IDNo, PSGroup, PersonalSubArea, OrganizationalUnit, IsContractEmployee, Position, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, GrossWeightTonnage, DeadWeightTonnage) VALUES
   (1, N'RB', N'anuuser', N'Anonymous', N'User', N' ', '12/31/1994 6:30:00 PM', 19, '12/31/2013 6:30:00 PM', 0, N'9848621091', N'9848621091', N'ipmsuat@navayugainfotech.com', N'GENM', N'DEP1', N'DEAO', N'BUCB', N'CC1', N'PA1', N'0001', N'PSG1', N'PSA2', N'OU1', NULL, NULL, N'A', 2, '12/12/2014 10:16:12 PM', 2, '12/12/2014 10:16:12 PM', NULL, NULL) 
   set identity_insert Employee off
end
GO

if not exists (select * from Employee where EmployeeID = 2) 
begin 
   set identity_insert Employee on
   insert into Employee
   (EmployeeID, PortCode, SAPNumber, FirstName, LastName, Initials, BirthDate, Age, JoiningDate, YearsofService, OfficialMobileNo, PersonalMobileNo, EmailID, Gender, Department, Designation, BusinessUnit, CostCenter, PayrollArea, IDNo, PSGroup, PersonalSubArea, OrganizationalUnit, IsContractEmployee, Position, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, GrossWeightTonnage, DeadWeightTonnage) VALUES
   (2, N'RB', N'admin', N'Administrator', N'IPMS', N'', '12/31/1994 6:30:00 PM', 19, '12/11/2014 6:30:00 PM', 0, N'9848621091', N'9848621091', N'ipmsuat@navayugainfotech.com', N'GENM', N'DEP1', N'DEAO', N'BUCB', N'CC1', N'PA1', N'002', N'PSG1', N'PSA2', N'OU1', NULL, NULL, N'A', 2, '12/12/2014 10:18:54 PM', 2, '12/12/2014 10:18:54 PM', NULL, NULL)
   set identity_insert Employee off
end
GO

set identity_insert Employee on
insert into Employee
(EmployeeID, PortCode, SAPNumber, FirstName, LastName, Initials, BirthDate, JoiningDate, OfficialMobileNo, PersonalMobileNo, EmailID, Gender, Department, Designation, BusinessUnit, CostCenter, PayrollArea, IDNo, PersonalSubArea, PSGroup, OrganizationalUnit, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) VALUES
(218, 'RB', N'00043687', N'Alan', N'Logan', N'A', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B118', 'BUCB', 'CC1', 'PA1', 43687, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(219, 'RB', N'00043690', N'Glen', N'Rapson', N'G', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG26', 'BUCB', 'CC1', 'PA1', 43690, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(220, 'RB', N'00043692', N'Bogdan', N'Dutkowski', N'B', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG12', 'BUCB', 'CC1', 'PA1', 43692, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(221, 'RB', N'00043712', N'Anton', N'Mlondo', N'A', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG25', 'BUCB', 'CC1', 'PA1', 43712, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(222, 'RB', N'00043729', N'Bongumusa', N'Mthiyane', N'B', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG19', 'BUCB', 'CC1', 'PA1', 43729, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(223, 'RB', N'00043732', N'Simon', N'Madida', N'S', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B096', 'BUCB', 'CC1', 'PA1', 43732, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(224, 'RB', N'00043737', N'Henk', N'Coetzee', N'H', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG19', 'BUCB', 'CC1', 'PA1', 43737, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(225, 'RB', N'00043743', N'John', N'Drewry', N'J', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG19', 'BUCB', 'CC1', 'PA1', 43743, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(226, 'RB', N'00043765', N'Quinton', N'Gower', N'Q', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG15', 'BUCB', 'CC1', 'PA1', 43765, N'PSA8', N'PG03', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(227, 'RB', N'00043766', N'James', N'Field', N'J', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B118', 'BUCB', 'CC1', 'PA1', 43766, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(228, 'RB', N'00043768', N'Rodger', N'Thackwray', N'R', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B118', 'BUCB', 'CC1', 'PA1', 43768, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(229, 'RB', N'00043949', N'Neelan', N'Moodley', N'N', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG07', 'BUCB', 'CC1', 'PA1', 43949, N'PSA8', N'PG07', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(230, 'RB', N'00044699', N'Jerome', N'Mhlongo', N'J', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG03', 'BUCB', 'CC1', 'PA1', 44699, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(231, 'RB', N'00044731', N'Aime', N'Moorghen', N'A', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG26', 'BUCB', 'CC1', 'PA1', 44731, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(232, 'RB', N'00044746', N'Sabelo', N'Xulu', N'S', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG26', 'BUCB', 'CC1', 'PA1', 44746, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(233, 'RB', N'00044776', N'Sizwe', N'Dubazane', N'S', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B118', 'BUCB', 'CC1', 'PA1', 44776, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(234, 'RB', N'00044827', N'Simphiwe', N'Sithole', N'S', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG17', 'BUCB', 'CC1', 'PA1', 44827, N'PSA8', N'PG07', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(235, 'RB', N'00045003', N'Kwanele', N'Masakazi', N'K', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B096', 'BUCB', 'CC1', 'PA1', 45003, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(236, 'RB', N'00045049', N'Thamsanqa', N'Mdlalose', N'T', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG26', 'BUCB', 'CC1', 'PA1', 45049, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(237, 'RB', N'00045050', N'Sabelo', N'Mdlalose', N'S', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG18', 'BUCB', 'CC1', 'PA1', 45050, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(238, 'RB', N'00045056', N'Bhekamafunze', N'Ngcobo', N'B', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG27', 'BUCB', 'CC1', 'PA1', 45056, N'PSA8', N'PG03', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(239, 'RB', N'00045118', N'Mbuyiselwa', N'Mbanjwa', N'M', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG23', 'BUCB', 'CC1', 'PA1', 45118, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(240, 'RB', N'00045127', N'Siphesihle', N'Nyewula', N'S', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG23', 'BUCB', 'CC1', 'PA1', 45127, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(241, 'RB', N'00045133', N'Julia', N'Ramitshana', N'JD', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG33', 'BUCB', 'CC1', 'PA1', 45133, N'PSA8', N'PG04', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(242, 'RB', N'00045162', N'Lungile', N'Manqele', N'LA', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B121', 'BUCB', 'CC1', 'PA1', 45162, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(243, 'RB', N'00045369', N'Nondumiso', N'Mthiyane', N'N', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B121', 'BUCB', 'CC1', 'PA1', 45369, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(244, 'RB', N'00045433', N'Aretha', N'Mkhize', N'A', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG26', 'BUCB', 'CC1', 'PA1', 45433, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(245, 'RB', N'00045440', N'Edmara', N'Masuku', N'E', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG26', 'BUCB', 'CC1', 'PA1', 45440, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(246, 'RB', N'00045441', N'Freddah', N'Legoabe', N'F', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG26', 'BUCB', 'CC1', 'PA1', 45441, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(247, 'RB', N'00045442', N'Thandeka', N'Madondo', N'T', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG26', 'BUCB', 'CC1', 'PA1', 45442, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(248, 'RB', N'00045461', N'Hlengiwe', N'Mthembu', N'H', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B118', 'BUCB', 'CC1', 'PA1', 45461, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(249, 'RB', N'00045462', N'Grace', N'Reynolds', N'G', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG26', 'BUCB', 'CC1', 'PA1', 45462, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(250, 'RB', N'00045466', N'Londiwe', N'Phoswa', N'LM', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B118', 'BUCB', 'CC1', 'PA1', 45466, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(251, 'RB', N'00045468', N'Siphamandla', N'Mpanza', N'SM', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG26', 'BUCB', 'CC1', 'PA1', 45468, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(252, 'RB', N'00045472', N'Vusumusi', N'Msomi', N'V', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG26', 'BUCB', 'CC1', 'PA1', 45472, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(253, 'RB', N'00045474', N'Sboniso', N'Ncube', N'S', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG19', 'BUCB', 'CC1', 'PA1', 45474, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(254, 'RB', N'00045476', N'Ngenzeni', N'Ntimbane', N'NP', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B118', 'BUCB', 'CC1', 'PA1', 45476, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(255, 'RB', N'00045481', N'Philile', N'Magubane', N'P', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG26', 'BUCB', 'CC1', 'PA1', 45481, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(256, 'RB', N'00045484', N'Sithembiso', N'Sithole', N'S', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B096', 'BUCB', 'CC1', 'PA1', 45484, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(257, 'RB', N'00045527', N'Ntombizodwa', N'Mdluli', N'N', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B096', 'BUCB', 'CC1', 'PA1', 45527, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(258, 'RB', N'00045542', N'Ntokozo', N'Khumalo', N'N', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B120', 'BUCB', 'CC1', 'PA1', 45542, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(259, 'RB', N'00045543', N'Cebile', N'Dlamini', N'C', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B118', 'BUCB', 'CC1', 'PA1', 45543, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(260, 'RB', N'00045545', N'Lydia', N'Ngema', N'L', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B120', 'BUCB', 'CC1', 'PA1', 45545, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(261, 'RB', N'00045546', N'Dumisani', N'Ngobese', N'D', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG23', 'BUCB', 'CC1', 'PA1', 45546, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(262, 'RB', N'00045547', N'Phiwayinkosi', N'Dube', N'P', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG23', 'BUCB', 'CC1', 'PA1', 45547, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(263, 'RB', N'00045549', N'Winnie', N'Mpanza', N'W', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B118', 'BUCB', 'CC1', 'PA1', 45549, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(264, 'RB', N'00045628', N'Bongile', N'Tikwayo', N'B', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG19', 'BUCB', 'CC1', 'PA1', 45628, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(265, 'RB', N'00045632', N'Sibusiso', N'Sithomo', N'SZI', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG19', 'BUCB', 'CC1', 'PA1', 45632, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(266, 'RB', N'00045660', N'Robert', N'Brindle', N'RW', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG12', 'BUCB', 'CC1', 'PA1', 45660, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(267, 'RB', N'00045774', N'Nalini', N'Naidoo', N'N', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG19', 'BUCB', 'CC1', 'PA1', 45774, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(268, 'RB', N'00045777', N'Siboniso', N'Zikalala', N'SA', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG19', 'BUCB', 'CC1', 'PA1', 45777, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(269, 'RB', N'00045876', N'Bongumusa', N'Jele', N'B', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG19', 'BUCB', 'CC1', 'PA1', 45876, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(270, 'RB', N'00045879', N'Juanitha', N'Baboolal', N'J', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B121', 'BUCB', 'CC1', 'PA1', 45879, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(271, 'RB', N'00045894', N'Patrick', N'Mngomezulu', N'P', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B121', 'BUCB', 'CC1', 'PA1', 45894, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(272, 'RB', N'00045897', N'John', N'Haupt', N'J', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG26', 'BUCB', 'CC1', 'PA1', 45897, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(273, 'RB', N'00045929', N'Thandeka', N'Khanyile', N'T', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG05', 'BUCB', 'CC1', 'PA1', 45929, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(274, 'RB', N'00045945', N'Andisa', N'Moyake', N'A', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG19', 'BUCB', 'CC1', 'PA1', 45945, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(275, 'RB', N'00046011', N'Jabulile', N'Ntombela', N'JP', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B118', 'BUCB', 'CC1', 'PA1', 46011, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(276, 'RB', N'00046100', N'Andries', N'Nkosi', N'AM', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B118', 'BUCB', 'CC1', 'PA1', 46100, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(277, 'RB', N'00046135', N'Thobile', N'Nkwanyana', N'TPS', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B121', 'BUCB', 'CC1', 'PA1', 46135, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(278, 'RB', N'00046181', N'S''thembiso', N'Zulu', N'S', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG23', 'BUCB', 'CC1', 'PA1', 46181, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(279, 'RB', N'00046354', N'Archiebald', N'Ngwane', N'AS', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG25', 'BUCB', 'CC1', 'PA1', 46354, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(280, 'RB', N'00046417', N'Siyethemba', N'Gida', N'SF', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B118', 'BUCB', 'CC1', 'PA1', 46417, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(281, 'RB', N'00046430', N'Zandile', N'Mdletshe', N'Z', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B118', 'BUCB', 'CC1', 'PA1', 46430, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(282, 'RB', N'00046432', N'Sanele', N'Dlamini', N'S', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B118', 'BUCB', 'CC1', 'PA1', 46432, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(283, 'RB', N'00046433', N'Thandeka', N'Cele', N'TF', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B118', 'BUCB', 'CC1', 'PA1', 46433, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(284, 'RB', N'00046434', N'Nompumelelo', N'Sithole', N'NA', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG19', 'BUCB', 'CC1', 'PA1', 46434, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(285, 'RB', N'00046435', N'Muzi', N'Mfekayi', N'MR', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG19', 'BUCB', 'CC1', 'PA1', 46435, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(286, 'RB', N'00046436', N'Thabani', N'Mbatha', N'TG', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG19', 'BUCB', 'CC1', 'PA1', 46436, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(287, 'RB', N'00046437', N'Bongani', N'Gumede', N'BW', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG19', 'BUCB', 'CC1', 'PA1', 46437, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(288, 'RB', N'00046547', N'Subramoney', N'Maistry', N'S', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG17', 'BUCB', 'CC1', 'PA1', 46547, N'PSA8', N'PG07', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(289, 'RB', N'00046558', N'David', N'Cottril', N'DE', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B118', 'BUCB', 'CC1', 'PA1', 46558, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(290, 'RB', N'00054552', N'Zaulien', N'Kroukamp', N'Z', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG24', 'BUCB', 'CC1', 'PA1', 54552, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(291, 'RB', N'00108340', N'Zezethu', N'Nduna', N'Z', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B120', 'BUCB', 'CC1', 'PA1', 108340, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(292, 'RB', N'00120012', N'Phakamani Cypriah', N'Shazi', N'PC', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG25', 'BUCB', 'CC1', 'PA1', 120012, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(293, 'RB', N'00120017', N'Bhekani Mazwi', N'Ntombela', N'BM', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG25', 'BUCB', 'CC1', 'PA1', 120017, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(294, 'RB', N'00120018', N'Siyabonga Edicious', N'Vilane', N'SE', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG25', 'BUCB', 'CC1', 'PA1', 120018, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(295, 'RB', N'00120020', N'Thulani Hemilton', N'Njilo', N'TH', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG25', 'BUCB', 'CC1', 'PA1', 120020, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(296, 'RB', N'00120021', N'Bhekifa Sipho', N'Mthethwa', N'BS', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG25', 'BUCB', 'CC1', 'PA1', 120021, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(297, 'RB', N'00120022', N'Sibusiso Moses', N'Mabuza', N'SM', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'DG25', 'BUCB', 'CC1', 'PA1', 120022, N'PSA8', N'PG05', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM'), 
(298, 'RB', N'00131802', N'Divan', N'Horn', N'D', '1900-01-01', '1900-01-01', '0270313613767', '0270313613767', 'ipmsuat@navayugainfotech.com', 'GENM', 'DEP1', N'B120', 'BUCB', 'CC1', 'PA1', 131802, N'PSA8', N'PSG1', 'OU1', 'A', 2, '16-Apr-2015 6:23:59 PM', 2, '16-Apr-2015 6:23:59 PM')
set identity_insert Employee off

set identity_insert Users on
insert into Users
(UserID, UserName, UserType, UserTypeID, FirstName, LastName, ContactNo, EmailID, RecordStatus, AnonymousUserYn, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, PWD, IsFirstTimeLogin, PwdExpirtyDate, IncorrectLogins, LoginTime, DormantStatus) VALUES
(318, N'00043687', 'EMP', 218, N'Alan', N'Logan', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(319, N'00043690', 'EMP', 219, N'Glen', N'Rapson', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(320, N'00043692', 'EMP', 220, N'Bogdan', N'Dutkowski', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(321, N'00043712', 'EMP', 221, N'Anton', N'Mlondo', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(322, N'00043729', 'EMP', 222, N'Bongumusa', N'Mthiyane', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(323, N'00043732', 'EMP', 223, N'Simon', N'Madida', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(324, N'00043737', 'EMP', 224, N'Henk', N'Coetzee', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(325, N'00043743', 'EMP', 225, N'John', N'Drewry', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(326, N'00043765', 'EMP', 226, N'Quinton', N'Gower', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(327, N'00043766', 'EMP', 227, N'James', N'Field', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(328, N'00043768', 'EMP', 228, N'Rodger', N'Thackwray', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(329, N'00043949', 'EMP', 229, N'Neelan', N'Moodley', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(330, N'00044699', 'EMP', 230, N'Jerome', N'Mhlongo', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(331, N'00044731', 'EMP', 231, N'Aime', N'Moorghen', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(332, N'00044746', 'EMP', 232, N'Sabelo', N'Xulu', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(333, N'00044776', 'EMP', 233, N'Sizwe', N'Dubazane', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(334, N'00044827', 'EMP', 234, N'Simphiwe', N'Sithole', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(335, N'00045003', 'EMP', 235, N'Kwanele', N'Masakazi', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(336, N'00045049', 'EMP', 236, N'Thamsanqa', N'Mdlalose', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(337, N'00045050', 'EMP', 237, N'Sabelo', N'Mdlalose', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(338, N'00045056', 'EMP', 238, N'Bhekamafunze', N'Ngcobo', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(339, N'00045118', 'EMP', 239, N'Mbuyiselwa', N'Mbanjwa', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(340, N'00045127', 'EMP', 240, N'Siphesihle', N'Nyewula', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(341, N'00045133', 'EMP', 241, N'Julia', N'Ramitshana', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(342, N'00045162', 'EMP', 242, N'Lungile', N'Manqele', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(343, N'00045369', 'EMP', 243, N'Nondumiso', N'Mthiyane', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(344, N'00045433', 'EMP', 244, N'Aretha', N'Mkhize', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(345, N'00045440', 'EMP', 245, N'Edmara', N'Masuku', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(346, N'00045441', 'EMP', 246, N'Freddah', N'Legoabe', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(347, N'00045442', 'EMP', 247, N'Thandeka', N'Madondo', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(348, N'00045461', 'EMP', 248, N'Hlengiwe', N'Mthembu', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(349, N'00045462', 'EMP', 249, N'Grace', N'Reynolds', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(350, N'00045466', 'EMP', 250, N'Londiwe', N'Phoswa', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(351, N'00045468', 'EMP', 251, N'Siphamandla', N'Mpanza', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(352, N'00045472', 'EMP', 252, N'Vusumusi', N'Msomi', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(353, N'00045474', 'EMP', 253, N'Sboniso', N'Ncube', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(354, N'00045476', 'EMP', 254, N'Ngenzeni', N'Ntimbane', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(355, N'00045481', 'EMP', 255, N'Philile', N'Magubane', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(356, N'00045484', 'EMP', 256, N'Sithembiso', N'Sithole', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(357, N'00045527', 'EMP', 257, N'Ntombizodwa', N'Mdluli', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(358, N'00045542', 'EMP', 258, N'Ntokozo', N'Khumalo', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(359, N'00045543', 'EMP', 259, N'Cebile', N'Dlamini', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(360, N'00045545', 'EMP', 260, N'Lydia', N'Ngema', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(361, N'00045546', 'EMP', 261, N'Dumisani', N'Ngobese', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(362, N'00045547', 'EMP', 262, N'Phiwayinkosi', N'Dube', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(363, N'00045549', 'EMP', 263, N'Winnie', N'Mpanza', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(364, N'00045628', 'EMP', 264, N'Bongile', N'Tikwayo', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(365, N'00045632', 'EMP', 265, N'Sibusiso', N'Sithomo', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(366, N'00045660', 'EMP', 266, N'Robert', N'Brindle', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(367, N'00045774', 'EMP', 267, N'Nalini', N'Naidoo', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(368, N'00045777', 'EMP', 268, N'Siboniso', N'Zikalala', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(369, N'00045876', 'EMP', 269, N'Bongumusa', N'Jele', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(370, N'00045879', 'EMP', 270, N'Juanitha', N'Baboolal', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(371, N'00045894', 'EMP', 271, N'Patrick', N'Mngomezulu', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(372, N'00045897', 'EMP', 272, N'John', N'Haupt', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(373, N'00045929', 'EMP', 273, N'Thandeka', N'Khanyile', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(374, N'00045945', 'EMP', 274, N'Andisa', N'Moyake', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(375, N'00046011', 'EMP', 275, N'Jabulile', N'Ntombela', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(376, N'00046100', 'EMP', 276, N'Andries', N'Nkosi', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(377, N'00046135', 'EMP', 277, N'Thobile', N'Nkwanyana', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(378, N'00046181', 'EMP', 278, N'S''thembiso', N'Zulu', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(379, N'00046354', 'EMP', 279, N'Archiebald', N'Ngwane', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(380, N'00046417', 'EMP', 280, N'Siyethemba', N'Gida', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(381, N'00046430', 'EMP', 281, N'Zandile', N'Mdletshe', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(382, N'00046432', 'EMP', 282, N'Sanele', N'Dlamini', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(383, N'00046433', 'EMP', 283, N'Thandeka', N'Cele', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(384, N'00046434', 'EMP', 284, N'Nompumelelo', N'Sithole', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(385, N'00046435', 'EMP', 285, N'Muzi', N'Mfekayi', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(386, N'00046436', 'EMP', 286, N'Thabani', N'Mbatha', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(387, N'00046437', 'EMP', 287, N'Bongani', N'Gumede', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(388, N'00046547', 'EMP', 288, N'Subramoney', N'Maistry', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(389, N'00046558', 'EMP', 289, N'David', N'Cottril', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(390, N'00054552', 'EMP', 290, N'Zaulien', N'Kroukamp', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(391, N'00108340', 'EMP', 291, N'Zezethu', N'Nduna', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(392, N'00120012', 'EMP', 292, N'Phakamani Cypriah', N'Shazi', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(393, N'00120017', 'EMP', 293, N'Bhekani Mazwi', N'Ntombela', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(394, N'00120018', 'EMP', 294, N'Siyabonga Edicious', N'Vilane', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(395, N'00120020', 'EMP', 295, N'Thulani Hemilton', N'Njilo', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(396, N'00120021', 'EMP', 296, N'Bhekifa Sipho', N'Mthethwa', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(397, N'00120022', 'EMP', 297, N'Sibusiso Moses', N'Mabuza', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N'), 
(398, N'00131802', 'EMP', 298, N'Divan', N'Horn', '0270313613767', 'ipms@transnet.net', 'A', 'N', 2, '16-Apr-2015 6:26:06 PM', 2, '16-Apr-2015 6:26:06 PM', 'k28/J2ItBj3YKQu7+BmzOQ==', 'N', '15-Jul-2015 6:26:06 PM', 0, '16-Apr-2015 6:26:06 PM', 'N')
set identity_insert Users off

delete from UserPort where PortCode = 'RB'
GO

insert into UserPort (UserID, PortCode, WFStatus, VerifiedBy, VerifiedDate, ApprovedBy, ApprovedDate, RecordStatus, CreatedBy, 
CreatedDate, ModifiedBy, ModifiedDate)
select UserID, b.PortCode, 'WFSA' WFStatus, 2 VerifiedBy, getdate() VerifiedDate, 2 ApprovedBy, getdate() ApprovedDate, 
a.RecordStatus, a.CreatedBy, a.CreatedDate, a.ModifiedBy, a.ModifiedDate
from Users a inner join Employee b on a.UserTypeID = b.EmployeeID where a.UserType = 'EMP' and b.PortCode = 'RB'

-- Pending Quay
-- Pending Berth
-- Pending Bollard
-- Pending BerthCargo 
-- Pending BerthReasonForVisit 
-- Pending BerthVesselType 

set identity_insert Code on
insert into Code
(CodeID, PortCode, CodeName, Description, StartValue, CurValue, IsMonth, CodeYear, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) VALUES
(71, 'RB', N'FUEL', N'Fuel Request', 1, 6, N'Y', 2014, N'A', 2, '13-Dec-2014 6:51:36 PM', 2, '13-Dec-2014 6:51:36 PM'), 
(72, 'RB', N'RECEPT', N'Fuel Recept', 1, 5, N'Y', 2014, N'A', 2, '13-Dec-2014 6:51:36 PM', 2, '13-Dec-2014 6:51:36 PM'), 
(73, 'RB', N'SERVREQ', N'Service Request', 1, 1, N'Y', 2014, N'A', 2, '13-Dec-2014 6:51:36 PM', 2, '13-Dec-2014 6:51:36 PM'), 
(74, 'RB', N'VCN', N'Arrival Notification', 1, 21, N'Y', 2014, N'I', 2, '13-Dec-2014 6:51:36 PM', 2, '13-Dec-2014 6:51:36 PM'), 
(75, 'RB', N'DRFT', N'Arrival Notification Draft', 1, 16, N'Y', 2014, N'I', 2, '13-Dec-2014 6:51:36 PM', 2, '13-Dec-2014 6:51:36 PM'), 
(76, 'RB', N'LIR', N'License Request', 1, 1, N'Y', 2014, N'A', 2, '15-Dec-2014 3:08:07 PM', 2, '15-Dec-2014 3:08:07 PM'), 
(77, 'RB', N'FUEL', N'Fuel Request', 1, 6, N'Y', 2015, N'A', 2, '01-Jan-2015 4:41:47 PM', 2, '01-Jan-2015 4:41:47 PM'), 
(78, 'RB', N'RECEPT', N'Fuel Recept', 1, 5, N'Y', 2015, N'A', 2, '01-Jan-2015 4:41:47 PM', 2, '01-Jan-2015 4:41:47 PM'), 
(79, 'RB', N'SERVREQ', N'Service Request', 1, 1, N'Y', 2015, N'A', 2, '01-Jan-2015 4:41:47 PM', 2, '01-Jan-2015 4:41:47 PM'), 
(80, 'RB', N'VCN', N'Arrival Notification', 1, 21, N'Y', 2015, N'A', 2, '01-Jan-2015 4:41:47 PM', 2, '01-Jan-2015 4:41:47 PM'), 
(81, 'RB', N'DRFT', N'Arrival Notification Draft', 1, 9, N'Y', 2015, N'A', 2, '01-Jan-2015 4:41:47 PM', 2, '01-Jan-2015 4:41:47 PM'), 
(82, 'RB', N'LIR', N'License Request', 1, 1, N'Y', 2015, N'A', 2, '01-Jan-2015 4:41:47 PM', 2, '01-Jan-2015 4:41:47 PM'), 
(83, 'RB', N'DOCKPL', N'Docking Plan', 1, 16, N'Y', 2014, N'A', 1, '07-Jan-2015 7:05:19 PM', 1, '07-Jan-2015 7:05:19 PM'), 
(84, 'RB', N'DOCKPL', N'Docking Plan', 1, 16, N'Y', 2015, N'A', 1, '01-Jan-2015 4:12:35 PM', 1, '01-Jan-2015 4:24:28 PM'), 
(85, 'RB', N'IEP', N'InternalEmployeePermit Request', 1, 5, N'Y', 2015, N'A', 1, '01-Jul-2015 4:12:35 PM', 1, '06-Aug-2014 2:03:37 PM'), 
(86, 'RB', N'PEP', N'Permit Request', 1, 1, N'Y', 2014, N'A', 1, '06-Aug-2014 2:03:37 PM', 1, '06-Aug-2014 2:03:37 PM'), 
(87, 'RB', N'PEP', N'Permit Request', 1, 14, N'Y', 2015, N'A', 1, '14-Jan-2015 4:58:44 PM', 1, '14-Jan-2015 4:58:44 PM'), 
(88, 'RB', N'BERT', N'Berth Maintenance', 1, 5, N'Y', 2015, N'A', 1, '06-Aug-2014 2:03:37 PM', 1, '06-Aug-2014 2:03:37 PM')
set identity_insert Code off

insert into SubCategory (SubCatCode, SupCatCode, SubCatName, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
values ('CT19', 'CRFY', 'TWIN-UNIT CYC. TRACTOR TUG', 'A', 2, getdate(), 2, getdate())

insert into SubCategory (SubCatCode, SupCatCode, SubCatName, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
values ('CT20', 'CRFY', 'TWIN-SCREW PILOT BOAT', 'A', 2, getdate(), 2, getdate())

insert into SubCategory (SubCatCode, SupCatCode, SubCatName, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
values ('ET19', 'ENG', '2 X MIR-CATERPILLAR', 'A', 2, getdate(), 2, getdate())

set identity_insert Craft on
insert into Craft
(CraftID, CraftCode, CraftName, PortCode, IMONo, CallSign, ExCallSign, CraftType, CraftBuildDate, DateOfDelivery, CraftNationality, ClassificationSociety, CommissionDate, AFCInMetricTon, FuelType, PortOfRegistry, EnginePower, EngineType, PropulsionType, NoOfPropellers, MaxManeuveringSpeed, BeamM, RegisteredLengthM, ForwardDraftM, AftDraftM, GrossRegisteredTonnageMT, NetRegisteredTonnageMT, DeadWeightTonnageMT, BollardPullMT, OwnersName, Address, PhoneNumber, EmailID, InitialFuelQuantityMT, LOROBMT, FreshwaterROBMT, CraftCommissionStatus, HYDROBMT, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, DredgerColorCode) VALUES
(20, N'NA-20', N'Ballito (Ex John Cox)', N'RB', N'NA', N'NA', N'NA', N'CT20', '01-Jan-1972 12:00:00 AM', NULL, N'KENA', NULL, NULL, 100.000, N'DSL', N'POR1', 1044, N'ET13', N'PRP7', 2, NULL, 100, 100, 100, 100, 104, 100, NULL, 0, N'NA', N'NA', NULL, NULL, NULL, NULL, NULL, N'OC', NULL, N'A', 1, '14-Apr-2015 6:06:53 PM', 1, '14-Apr-2015 6:06:53 PM', NULL), 
(21, N'NA-21', N'Indlazi', N'RB', N'NA', N'NA', N'NA', N'CT06', '01-Jan-2001 12:00:00 AM', NULL, N'KENA', NULL, NULL, 100.000, N'DSL', N'POR1', 1990, N'ENG4', N'PRP4', 2, NULL, 100, 100, 100, 100, 378, 100, NULL, 55, N'NA', N'NA', NULL, NULL, NULL, NULL, NULL, N'OC', NULL, N'A', 1, '14-Apr-2015 6:06:53 PM', 1, '14-Apr-2015 6:06:53 PM', NULL), 
(22, N'NA-22', N'Iphothwe', N'RB', N'NA', N'NA', N'NA', N'CT06', '01-Jan-2011 12:00:00 AM', NULL, N'KENA', NULL, NULL, 100.000, N'DSL', N'POR1', 5440, N'ET13', N'PRP4', 2, NULL, 100, 100, 100, 100, 461, 100, NULL, 72, N'NA', N'NA', NULL, NULL, NULL, NULL, NULL, N'OC', NULL, N'A', 1, '14-Apr-2015 6:06:53 PM', 1, '14-Apr-2015 6:06:53 PM', NULL), 
(23, N'NA-23', N'Lilani', N'RB', N'NA', N'NA', N'NA', N'CT06', '01-Jan-2011 12:00:00 AM', NULL, N'KENA', NULL, NULL, 100.000, N'DSL', N'POR1', 5440, N'ET13', N'PRP4', 2, NULL, 100, 100, 100, 100, 462, 100, NULL, 74, N'NA', N'NA', NULL, NULL, NULL, NULL, NULL, N'OC', NULL, N'A', 1, '14-Apr-2015 6:06:53 PM', 1, '14-Apr-2015 6:06:53 PM', NULL), 
(24, N'NA-24', N'Swift Tern', N'RB', N'NA', N'NA', N'NA', N'CT04', '01-Jan-1998 12:00:00 AM', NULL, N'KENA', NULL, NULL, 100.000, N'DSL', N'POR1', 1044, N'ENG2', N'PRP7', 2, NULL, 100, 100, 100, 100, 95, 100, NULL, 19, N'NA', N'NA', NULL, NULL, NULL, NULL, NULL, N'OC', NULL, N'A', 1, '14-Apr-2015 6:06:53 PM', 1, '14-Apr-2015 6:06:53 PM', NULL), 
(25, N'NA-25', N'Uhuva (Ex W. Marshall Clarke)', N'RB', N'NA', N'NA', N'NA', N'CT19', '01-Jan-1974 12:00:00 AM', NULL, N'KENA', NULL, NULL, 100.000, N'DSL', N'POR1', 2947, N'ET19', N'PRP4', 2, NULL, 100, 100, 100, 100, 374, 100, NULL, 43, N'NA', N'NA', NULL, NULL, NULL, NULL, NULL, N'OC', NULL, N'A', 1, '14-Apr-2015 6:06:53 PM', 1, '14-Apr-2015 6:06:53 PM', NULL), 
(26, N'NA-26', N'Uzavolo (Ex R.H. Tarpey)', N'RB', N'NA', N'NA', N'NA', N'CT19', '01-Jan-1974 12:00:00 AM', NULL, N'KENA', NULL, NULL, 100.000, N'DSL', N'POR1', 2947, N'ET14', N'PRP4', 2, NULL, 100, 100, 100, 100, 374, 100, NULL, 43, N'NA', N'NA', NULL, NULL, NULL, NULL, NULL, N'OC', NULL, N'A', 1, '14-Apr-2015 6:06:53 PM', 1, '14-Apr-2015 6:06:53 PM', NULL)
set identity_insert Craft off

-- Pending MaterialCodeMaster 
-- Pending MaterialCodePort 

set identity_insert PortGeneralConfig on
insert into PortGeneralConfig
(PortGeneralConfigID, PortCode, ConfigName, ConfigValue, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, ConfigLabelName, GroupName) VALUES
(299, 'RB', N'AgentValidity', N'1', 'A', 2, '12-Dec-2014 11:16:40 PM', 2, '12-Dec-2014 11:16:40 PM', N'Agent Validity (Years)', N'Agent'), 
(300, 'RB', N'ApproveCode', N'WFSA', 'A', 2, '12-Dec-2014 11:16:40 PM', 2, '12-Dec-2014 11:16:40 PM', N'ApproveCode', N'Workflow'), 
(301, 'RB', N'ARRESTED', N'#8B0000', 'A', 2, '12-Dec-2014 11:16:40 PM', 2, '02-Apr-2015 11:25:29 AM', N'Arrested Colour', N'Berth Planning'), 
(302, 'RB', N'ARRIVALGRACETIME', N'72', 'A', 2, '12-Dec-2014 11:16:40 PM', 2, '26-Dec-2014 6:24:24 PM', N'ARRIVALGRACETIME', N'Arrival Notification'), 
(303, 'RB', N'ARRIVALRISTRICTTIME', N'0', 'A', 2, '12-Dec-2014 11:16:40 PM', 2, '26-Dec-2014 6:24:24 PM', N'ARRIVALRISTRICTTIME', N'Arrival Notification'), 
(304, 'RB', N'BERTHED', N'#CD6090', 'A', 2, '12-Dec-2014 11:16:40 PM', 2, '02-Apr-2015 11:25:29 AM', N'Berthed Colour', N'Berth Planning'), 
(305, 'RB', N'CancelCode', N'WFCA', 'A', 2, '12-Dec-2014 11:16:40 PM', 2, '12-Dec-2014 11:16:40 PM', N'CancelCode', N'Workflow'), 
(306, 'RB', N'CONFIRMED', N'#006400', 'A', 2, '12-Dec-2014 11:16:40 PM', 2, '02-Apr-2015 11:25:29 AM', N'Confirmend Colour', N'Berth Planning'), 
(307, 'RB', N'DateFormat', N'yyyyMMdd', 'A', 2, '12-Dec-2014 11:16:40 PM', 2, '03-Mar-2015 5:21:25 PM', N'Application Date fromat', N'General Configuration'), 
(308, 'RB', N'DAYS', N'2', 'A', 2, '12-Dec-2014 11:16:40 PM', 2, '02-Apr-2015 11:25:29 AM', N'Days', N'Berth Planning'), 
(309, 'RB', N'FileUploadMaxSize', N'10', 'A', 2, '12-Dec-2014 11:16:40 PM', 2, '03-Mar-2015 5:21:25 PM', N'FileUpload MaxSize (MB)', N'General Configuration'), 
(310, 'RB', N'IncorrectPWDCount', N'2', 'A', 2, '12-Dec-2014 11:16:40 PM', 2, '03-Mar-2015 5:21:25 PM', N'IncorrectPWDCount', N'General Configuration'), 
(311, 'RB', N'MAINTAINENCE', N'#CFCFCF    ', 'A', 2, '12-Dec-2014 11:16:40 PM', 2, '02-Apr-2015 11:25:29 AM', N'Maintainence Colour', N'Berth Planning'), 
(312, 'RB', N'NotificationLifeSpan', N'48', 'A', 2, '12-Dec-2014 11:16:40 PM', 2, '26-Jan-2015 8:53:22 AM', N'Notifications Span (Hours)', N'Notification'), 
(313, 'RB', N'PENDING', N'#FFFFFF', 'A', 2, '12-Dec-2014 11:16:40 PM', 2, '02-Apr-2015 11:25:29 AM', N'Pending Colour', N'Berth Planning'), 
(314, 'RB', N'RejectCode', N'WFRE', 'A', 2, '12-Dec-2014 11:16:40 PM', 2, '12-Dec-2014 11:16:40 PM', N'RejectCode', N'Workflow'), 
(315, 'RB', N'ReportPeriod', N'7', 'A', 2, '12-Dec-2014 11:16:40 PM', 2, '12-Dec-2014 11:16:40 PM', N'Report View Duration (Days)', N'Reports'), 
(316, 'RB', N'SAFEDISTANCE', N'20', 'A', 2, '12-Dec-2014 11:16:40 PM', 2, '02-Apr-2015 11:25:29 AM', N'Vehicle Safe Distance', N'Berth Planning'), 
(317, 'RB', N'SCHEDULED', N'#FFBF00', 'A', 2, '12-Dec-2014 11:16:40 PM', 2, '02-Apr-2015 11:25:29 AM', N'Scheduled Colour', N'Berth Planning'), 
(318, 'RB', N'ServiceRequestCancelTaskReminder', N'149', 'A', 2, '12-Dec-2014 11:16:40 PM', 2, '12-Dec-2014 11:16:40 PM', N'ServiceRequestCancelTaskReminder', N'Service Request Auto Reminders'), 
(319, 'RB', N'ServiceRequestConfirmTaskReminder-1', N'180', 'A', 2, '12-Dec-2014 11:16:40 PM', 2, '12-Dec-2014 11:16:40 PM', N'ServiceRequestConfirmTaskReminder-1', N'Service Request Auto Reminders'), 
(320, 'RB', N'ServiceRequestConfirmTaskReminder-2', N'150', 'A', 2, '12-Dec-2014 11:16:40 PM', 2, '12-Dec-2014 11:16:40 PM', N'ServiceRequestConfirmTaskReminder-2', N'Service Request Auto Reminders'), 
(321, 'RB', N'SERVREQPRECOND1', N'240', 'A', 2, '12-Dec-2014 11:16:40 PM', 2, '28-Jan-2015 10:42:29 AM', N'Service request filing prior minimum duration (Minutes)', N'Service Request'), 
(322, 'RB', N'SLOT', N'1', 'A', 2, '12-Dec-2014 11:16:40 PM', 2, '02-Apr-2015 11:25:29 AM', N'Slot', N'Berth Planning'), 
(323, 'RB', N'UNDER KEEL CLEARANCE', N'0.6', 'A', 2, '12-Dec-2014 11:16:40 PM', 2, '02-Apr-2015 11:25:29 AM', N'Under Keel Clearance', N'Berth Planning'), 
(324, 'RB', N'WorkFlowInitialStatus', N'NEW', 'A', 2, '12-Dec-2014 11:16:40 PM', 2, '12-Dec-2014 11:16:40 PM', N'WorkFlowInitialStatus', N'Workflow'), 
(325, 'RB', N'Show Vessel Top Records', N'200', 'A', 1, '08-Jan-2015 2:46:31 PM', 1, '08-Jan-2015 2:46:31 PM', N'Show Vessels Top Records', N'VesselRegistration'), 
(326, 'RB', N'ATA/ATD Configuration', N'BreakWater', 'A', 1, '28-Jan-2015 6:34:50 PM', 2, '02-Apr-2015 11:25:29 AM', N'ATA/ATD Configuration', N'Berth Planning'), 
(327, 'RB', N'SAILED', N'#ffff00', 'A', 1, '31-Mar-2015 4:08:52 PM', 2, '02-Apr-2015 11:25:29 AM', N'Sailed Colour', N'Berth Planning'), 
(328, 'RB', N'SESSIONTIMEOUT', N'15', 'A', 1, '31-Mar-2015 4:26:34 PM', 1, '31-Mar-2015 4:26:34 PM', N'Session Timeout', N'General Configuration')
set identity_insert PortGeneralConfig off

set identity_insert ResourceAllocationConfigRule on
insert into ResourceAllocationConfigRule
(ResourceAllocationConfigRuleID, PortCode, PilotCapacity, TotalTugs, EffectedFrom, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) VALUES
(2, N'RB', N'DEDW', 1, '29-Mar-2014 12:00:00 AM', 'A', 1, '28-Mar-2015 3:38:36 PM', 2, '02-Apr-2015 1:12:57 PM')
set identity_insert ResourceAllocationConfigRule off

set identity_insert ResourceAllocationMovementTypeRule on 
insert into ResourceAllocationMovementTypeRule
(ResourceAllocationMovementTypeRuleID, ResourceAllocationConfigRuleID, PortCode, MovementType, ServiceTypeID) VALUES
(21, 2, N'RB', N'ARMV', 3), 
(22, 2, N'RB', N'ARMV', 2), 
(23, 2, N'RB', N'ARMV', 6), 
(24, 2, N'RB', N'SHMV', 3), 
(25, 2, N'RB', N'SHMV', 2), 
(26, 2, N'RB', N'SHMV', 1), 
(27, 2, N'RB', N'SGMV', 3), 
(28, 2, N'RB', N'SGMV', 2), 
(29, 2, N'RB', N'SGMV', 1), 
(30, 2, N'RB', N'WRMV', 3), 
(31, 2, N'RB', N'WRMV', 2), 
(32, 2, N'RB', N'WRMV', 1)
set identity_insert ResourceAllocationMovementTypeRule off

set identity_insert ResourceGangConfig on
insert into ResourceGangConfig
(ResourceGangConfigID, ResourceAllocationConfigRuleID, FromMeter, ToMeter, NoOfGangs) VALUES
(7, 2, 0.00, 99.00, 1), 
(8, 2, 100.00, 199.00, 1), 
(9, 2, 200.00, 1000.00, 1)
set identity_insert ResourceGangConfig off

set identity_insert ServiceTypeDesignation on
insert into ServiceTypeDesignation
(ServiceTypeDesignationID, ServiceTypeID, PortCode, DesignationCode, CraftType, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) VALUES
(22, 1, N'RB', N'DG26', NULL, 'A', 2, '13-Dec-2014 6:42:56 PM', 2, '13-Dec-2014 6:42:56 PM'), 
(23, 1, N'RB', N'PTTR', NULL, 'A', 2, '13-Dec-2014 6:42:56 PM', 2, '13-Dec-2014 6:42:56 PM'), 
(24, 1, N'RB', N'DG12', NULL, 'A', 2, '13-Dec-2014 6:42:56 PM', 2, '13-Dec-2014 6:42:56 PM'), 
(25, 4, N'RB', N'WMUR', NULL, 'A', 2, '13-Dec-2014 6:42:56 PM', 2, '13-Dec-2014 6:42:56 PM'), 
(26, 4, N'RB', N'WMSP', NULL, 'A', 2, '13-Dec-2014 6:42:56 PM', 2, '13-Dec-2014 6:42:56 PM'), 
(27, 5, N'RB', N'FCMR', N'CRTF', 'A', 2, '13-Dec-2014 6:42:56 PM', 2, '13-Dec-2014 6:42:56 PM'), 
(28, 6, N'RB', N'PTMR', N'CRTP', 'A', 2, '13-Dec-2014 6:42:56 PM', 2, '13-Dec-2014 6:42:56 PM'), 
(29, 3, N'RB', N'BRMR', NULL, 'A', 2, '26-Mar-2015 10:32:04 AM', 2, '26-Mar-2015 10:32:04 AM'), 
(30, 3, N'RB', N'DG02', NULL, 'A', 2, '26-Mar-2015 10:32:05 AM', 2, '26-Mar-2015 10:32:05 AM'), 
(31, 2, N'RB', N'WBMR', N'CT17', 'A', 2, '01-Apr-2015 4:59:47 PM', 2, '01-Apr-2015 4:59:47 PM'), 
(32, 2, N'RB', N'DG37', N'CT17', 'A', 2, '01-Apr-2015 4:59:47 PM', 2, '01-Apr-2015 4:59:47 PM')
set identity_insert ServiceTypeDesignation off

set identity_insert AutomatedSlotConfiguration on
insert into AutomatedSlotConfiguration
(SlotCofiguratinid, EffectiveFrm, Duration, NoofSlots, ExtendableSlots, PortCode, CreatedBy, CreatedDate, ModfiedBy, ModifiedDate, RecordStatus, OperationalPeriod) VALUES
(2, '29-Mar-2014 12:00:00 AM', 2, 4, 2, N'RB', 2, '28-Mar-2015 3:36:36 PM', 2, '28-Mar-2015 3:36:36 PM', N'A', N'0-24')
set identity_insert AutomatedSlotConfiguration off

insert into NotificationPort (NotificationTemplateCode, PortCode, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
select NotificationTemplateCode, 'RB' PortCode, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
from NotificationTemplate

-- Pending TerminalOperator
-- Pending TerminalOperatorBerth
-- Pending TerminalOperatorPort 

insert into UserRole
(UserID, RoleID, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) VALUES
(330, 3, 'A', 2, '16-Apr-2015 8:26:27 PM', 2, '16-Apr-2015 8:26:27 PM'),
(323, 17, 'A', 2, '16-Apr-2015 8:28:31 PM', 2, '16-Apr-2015 8:28:31 PM'), 
(335, 17, 'A', 2, '16-Apr-2015 8:28:31 PM', 2, '16-Apr-2015 8:28:31 PM'), 
(356, 17, 'A', 2, '16-Apr-2015 8:28:31 PM', 2, '16-Apr-2015 8:28:31 PM'), 
(357, 17, 'A', 2, '16-Apr-2015 8:28:31 PM', 2, '16-Apr-2015 8:28:31 PM'),
(342, 5, 'A', 2, '16-Apr-2015 8:31:15 PM', 2, '16-Apr-2015 8:31:15 PM'), 
(343, 5, 'A', 2, '16-Apr-2015 8:31:15 PM', 2, '16-Apr-2015 8:31:15 PM'), 
(358, 5, 'A', 2, '16-Apr-2015 8:31:15 PM', 2, '16-Apr-2015 8:31:15 PM'), 
(360, 5, 'A', 2, '16-Apr-2015 8:31:15 PM', 2, '16-Apr-2015 8:31:15 PM'), 
(370, 5, 'A', 2, '16-Apr-2015 8:31:15 PM', 2, '16-Apr-2015 8:31:15 PM'), 
(371, 5, 'A', 2, '16-Apr-2015 8:31:15 PM', 2, '16-Apr-2015 8:31:15 PM'), 
(377, 5, 'A', 2, '16-Apr-2015 8:31:15 PM', 2, '16-Apr-2015 8:31:15 PM'), 
(391, 5, 'A', 2, '16-Apr-2015 8:31:15 PM', 2, '16-Apr-2015 8:31:15 PM'), 
(398, 5, 'A', 2, '16-Apr-2015 8:31:15 PM', 2, '16-Apr-2015 8:31:15 PM'),
(320, 47, 'A', 2, '16-Apr-2015 8:32:34 PM', 2, '16-Apr-2015 8:32:34 PM'), 
(366, 47, 'A', 2, '16-Apr-2015 8:32:34 PM', 2, '16-Apr-2015 8:32:34 PM'),
(319, 45, 'A', 2, '16-Apr-2015 8:33:36 PM', 2, '16-Apr-2015 8:33:36 PM'), 
(331, 45, 'A', 2, '16-Apr-2015 8:33:36 PM', 2, '16-Apr-2015 8:33:36 PM'), 
(332, 45, 'A', 2, '16-Apr-2015 8:33:36 PM', 2, '16-Apr-2015 8:33:36 PM'), 
(336, 45, 'A', 2, '16-Apr-2015 8:33:36 PM', 2, '16-Apr-2015 8:33:36 PM'), 
(344, 45, 'A', 2, '16-Apr-2015 8:33:36 PM', 2, '16-Apr-2015 8:33:36 PM'), 
(345, 45, 'A', 2, '16-Apr-2015 8:33:36 PM', 2, '16-Apr-2015 8:33:36 PM'), 
(346, 45, 'A', 2, '16-Apr-2015 8:33:36 PM', 2, '16-Apr-2015 8:33:36 PM'), 
(347, 45, 'A', 2, '16-Apr-2015 8:33:36 PM', 2, '16-Apr-2015 8:33:36 PM'), 
(349, 45, 'A', 2, '16-Apr-2015 8:33:36 PM', 2, '16-Apr-2015 8:33:36 PM'), 
(351, 45, 'A', 2, '16-Apr-2015 8:33:36 PM', 2, '16-Apr-2015 8:33:36 PM'), 
(352, 45, 'A', 2, '16-Apr-2015 8:33:36 PM', 2, '16-Apr-2015 8:33:36 PM'), 
(355, 45, 'A', 2, '16-Apr-2015 8:33:36 PM', 2, '16-Apr-2015 8:33:36 PM'), 
(372, 45, 'A', 2, '16-Apr-2015 8:33:36 PM', 2, '16-Apr-2015 8:33:36 PM'),
(339, 54, 'A', 2, '16-Apr-2015 8:35:07 PM', 2, '16-Apr-2015 8:35:07 PM'), 
(340, 54, 'A', 2, '16-Apr-2015 8:35:07 PM', 2, '16-Apr-2015 8:35:07 PM'), 
(361, 54, 'A', 2, '16-Apr-2015 8:35:07 PM', 2, '16-Apr-2015 8:35:07 PM'), 
(362, 54, 'A', 2, '16-Apr-2015 8:35:07 PM', 2, '16-Apr-2015 8:35:07 PM'), 
(378, 54, 'A', 2, '16-Apr-2015 8:35:07 PM', 2, '16-Apr-2015 8:35:07 PM'),
(318, 43, 'A', 2, '16-Apr-2015 8:36:11 PM', 2, '16-Apr-2015 8:36:11 PM'), 
(327, 43, 'A', 2, '16-Apr-2015 8:36:11 PM', 2, '16-Apr-2015 8:36:11 PM'), 
(328, 43, 'A', 2, '16-Apr-2015 8:36:11 PM', 2, '16-Apr-2015 8:36:11 PM'), 
(333, 43, 'A', 2, '16-Apr-2015 8:36:11 PM', 2, '16-Apr-2015 8:36:11 PM'), 
(348, 43, 'A', 2, '16-Apr-2015 8:36:11 PM', 2, '16-Apr-2015 8:36:11 PM'), 
(350, 43, 'A', 2, '16-Apr-2015 8:36:11 PM', 2, '16-Apr-2015 8:36:11 PM'), 
(354, 43, 'A', 2, '16-Apr-2015 8:36:11 PM', 2, '16-Apr-2015 8:36:11 PM'), 
(359, 43, 'A', 2, '16-Apr-2015 8:36:11 PM', 2, '16-Apr-2015 8:36:11 PM'), 
(363, 43, 'A', 2, '16-Apr-2015 8:36:11 PM', 2, '16-Apr-2015 8:36:11 PM'), 
(375, 43, 'A', 2, '16-Apr-2015 8:36:11 PM', 2, '16-Apr-2015 8:36:11 PM'), 
(376, 43, 'A', 2, '16-Apr-2015 8:36:11 PM', 2, '16-Apr-2015 8:36:11 PM'), 
(380, 43, 'A', 2, '16-Apr-2015 8:36:11 PM', 2, '16-Apr-2015 8:36:11 PM'), 
(381, 43, 'A', 2, '16-Apr-2015 8:36:11 PM', 2, '16-Apr-2015 8:36:11 PM'), 
(382, 43, 'A', 2, '16-Apr-2015 8:36:11 PM', 2, '16-Apr-2015 8:36:11 PM'), 
(383, 43, 'A', 2, '16-Apr-2015 8:36:11 PM', 2, '16-Apr-2015 8:36:11 PM'), 
(389, 43, 'A', 2, '16-Apr-2015 8:36:11 PM', 2, '16-Apr-2015 8:36:11 PM'),
(321, 26, 'A', 2, '16-Apr-2015 8:37:39 PM', 2, '16-Apr-2015 8:37:39 PM'), 
(379, 26, 'A', 2, '16-Apr-2015 8:37:39 PM', 2, '16-Apr-2015 8:37:39 PM'), 
(392, 26, 'A', 2, '16-Apr-2015 8:37:39 PM', 2, '16-Apr-2015 8:37:39 PM'), 
(393, 26, 'A', 2, '16-Apr-2015 8:37:39 PM', 2, '16-Apr-2015 8:37:39 PM'), 
(394, 26, 'A', 2, '16-Apr-2015 8:37:39 PM', 2, '16-Apr-2015 8:37:39 PM'), 
(395, 26, 'A', 2, '16-Apr-2015 8:37:39 PM', 2, '16-Apr-2015 8:37:39 PM'), 
(396, 26, 'A', 2, '16-Apr-2015 8:37:39 PM', 2, '16-Apr-2015 8:37:39 PM'), 
(397, 26, 'A', 2, '16-Apr-2015 8:37:39 PM', 2, '16-Apr-2015 8:37:39 PM'),
(373, 44, 'A', 2, '16-Apr-2015 8:38:22 PM', 2, '16-Apr-2015 8:38:22 PM'),
(322, 24, 'A', 2, '16-Apr-2015 8:39:29 PM', 2, '16-Apr-2015 8:39:29 PM'), 
(324, 24, 'A', 2, '16-Apr-2015 8:39:29 PM', 2, '16-Apr-2015 8:39:29 PM'), 
(325, 24, 'A', 2, '16-Apr-2015 8:39:29 PM', 2, '16-Apr-2015 8:39:29 PM'), 
(353, 24, 'A', 2, '16-Apr-2015 8:39:29 PM', 2, '16-Apr-2015 8:39:29 PM'), 
(364, 24, 'A', 2, '16-Apr-2015 8:39:29 PM', 2, '16-Apr-2015 8:39:29 PM'), 
(365, 24, 'A', 2, '16-Apr-2015 8:39:29 PM', 2, '16-Apr-2015 8:39:29 PM'), 
(367, 24, 'A', 2, '16-Apr-2015 8:39:29 PM', 2, '16-Apr-2015 8:39:29 PM'), 
(368, 24, 'A', 2, '16-Apr-2015 8:39:29 PM', 2, '16-Apr-2015 8:39:29 PM'), 
(369, 24, 'A', 2, '16-Apr-2015 8:39:29 PM', 2, '16-Apr-2015 8:39:29 PM'), 
(374, 24, 'A', 2, '16-Apr-2015 8:39:29 PM', 2, '16-Apr-2015 8:39:29 PM'), 
(384, 24, 'A', 2, '16-Apr-2015 8:39:29 PM', 2, '16-Apr-2015 8:39:29 PM'), 
(385, 24, 'A', 2, '16-Apr-2015 8:39:29 PM', 2, '16-Apr-2015 8:39:29 PM'), 
(386, 24, 'A', 2, '16-Apr-2015 8:39:29 PM', 2, '16-Apr-2015 8:39:29 PM'), 
(387, 24, 'A', 2, '16-Apr-2015 8:39:29 PM', 2, '16-Apr-2015 8:39:29 PM'),
(337, 9, 'A', 2, '16-Apr-2015 8:40:08 PM', 2, '16-Apr-2015 8:40:08 PM'),
(338, 15, 'A', 2, '16-Apr-2015 8:40:36 PM', 2, '16-Apr-2015 8:40:36 PM'),
(341, 62, 'A', 2, '16-Apr-2015 8:41:15 PM', 2, '16-Apr-2015 8:41:15 PM'),
(329, 56, 'A', 2, '16-Apr-2015 8:41:46 PM', 2, '16-Apr-2015 8:41:46 PM'),
(334, 30, 'A', 2, '16-Apr-2015 8:42:20 PM', 2, '16-Apr-2015 8:42:20 PM'), 
(388, 30, 'A', 2, '16-Apr-2015 8:42:20 PM', 2, '16-Apr-2015 8:42:20 PM'),
(326, 14, 'A', 2, '16-Apr-2015 8:42:56 PM', 2, '16-Apr-2015 8:42:56 PM'),
(390, 48, 'A', 2, '16-Apr-2015 8:43:38 PM', 2, '16-Apr-2015 8:43:38 PM')

insert into WorkflowTask
(EntityID, WorkflowTaskCode, Step, NextStep, ValidityPeriod, HasNotification, APIUrl, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, PortCode, HasRemarks) VALUES
(15, N'CLOS', 9999, NULL, 0, 'Y', NULL, N'A', 1, '31-Mar-2015 4:04:08 PM', 1, '31-Mar-2015 4:04:08 PM', 'RB', 'N'), 
(15, N'NEW', 0, 40, 0, 'Y', NULL, N'A', 1, '31-Mar-2015 4:04:08 PM', 1, '31-Mar-2015 4:04:08 PM', 'RB', 'N'), 
(15, N'UPDT', 0, 40, 0, 'Y', NULL, N'A', 1, '31-Mar-2015 4:04:08 PM', 1, '31-Mar-2015 4:04:08 PM', 'RB', 'N'), 
(15, N'WFAK', 40, 9999, 0, 'Y', N'api/CraftsReminderConfig/Acknowledge', N'A', 1, '31-Mar-2015 4:04:08 PM', 1, '31-Mar-2015 4:04:08 PM', 'RB', 'N'), 
(33, N'CLOS', 9999, NULL, 0, 'Y', NULL, N'A', 1, '04-Feb-2015 5:20:02 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(33, N'NEW', 0, 20, 0, 'Y', NULL, N'A', 1, '04-Feb-2015 5:20:02 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(33, N'UPDT', 0, 20, 0, 'Y', NULL, N'A', 1, '04-Feb-2015 5:20:02 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(33, N'WFRE', 20, 9999, 0, 'Y', N'api/VesselRegistration/Reject', N'A', 1, '04-Feb-2015 5:20:02 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'Y'), 
(33, N'WFSA', 20, 9999, 0, 'Y', N'api/VesselRegistration/Approve', N'A', 1, '04-Feb-2015 5:20:02 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(34, N'CLOS', 9999, NULL, 0, 'Y', NULL, N'A', 1, '12-Feb-2015 3:10:34 PM', 1, '12-Feb-2015 3:10:34 PM', 'RB', 'N'), 
(34, N'NEW', 0, 10, 0, 'Y', NULL, N'A', 1, '12-Feb-2015 3:10:34 PM', 1, '12-Feb-2015 3:10:34 PM', 'RB', 'N'), 
(34, N'REJ', 20, 9999, 0, 'Y', N'api/Agent/RejectAgentRegistration', N'A', 1, '12-Feb-2015 3:10:34 PM', 1, '12-Feb-2015 3:10:34 PM', 'RB', 'Y'), 
(34, N'UPDT', 0, 10, 0, 'Y', NULL, N'A', 1, '12-Feb-2015 3:10:34 PM', 1, '12-Feb-2015 3:10:34 PM', 'RB', 'N'), 
(34, N'WFRE', 10, 9999, 0, 'Y', N'api/Agent/RejectAgentRegistration', N'A', 1, '12-Feb-2015 3:10:34 PM', 1, '12-Feb-2015 3:10:34 PM', 'RB', 'Y'), 
(34, N'WFSA', 20, 9999, 0, 'Y', N'api/Agent/ApproveAgentRegistration', N'A', 1, '12-Feb-2015 3:10:34 PM', 1, '12-Feb-2015 3:10:34 PM', 'RB', 'N'), 
(34, N'WFVE', 10, 20, 0, 'Y', N'api/Agent/VerifyAgentRegistration', N'A', 1, '12-Feb-2015 3:10:34 PM', 1, '12-Feb-2015 3:10:34 PM', 'RB', 'N'), 
(35, N'CLOS', 9999, NULL, 0, 'Y', NULL, N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(35, N'NEW', 0, 10, 0, 'Y', NULL, N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(35, N'REJ', 20, 9999, 0, 'Y', N'api/UserRegistration/RejectUserRegistration', N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'Y'), 
(35, N'UPDT', 0, 10, 0, 'Y', NULL, N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(35, N'WFRE', 10, 9999, 0, 'Y', N'api/UserRegistration/RejectUserRegistration', N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'Y'), 
(35, N'WFSA', 20, 9999, 0, 'Y', N'api/UserRegistration/ApproveUserRegistration', N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(35, N'WFVE', 10, 20, 0, 'Y', N'api/UserRegistration/VerifyUserRegistration', N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(36, N'CLOS', 9999, NULL, 0, 'Y', NULL, N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(36, N'NEW', 0, 20, 0, 'Y', N'api/ArrivalNotifications', N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(36, N'UPDT', 0, 20, 0, 'Y', NULL, N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(36, N'VRES', 20, 25, 0, 'Y', N'api/ArrivalNotifications/RequestToResubmit', N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(36, N'VUPD', 25, 20, 0, 'Y', NULL, N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(36, N'WFCA', 0, 9999, 0, 'Y', NULL, N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'Y'), 
(36, N'WFRE', 20, 9999, 0, 'Y', N'api/ArrivalNotifications/Reject', N'A', 1, '25-Feb-2015 8:11:38 PM', 1, '25-Feb-2015 8:11:38 PM', 'RB', 'Y'), 
(36, N'WFSA', 20, 9999, 0, 'Y', N'api/ArrivalNotifications/Approve', N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(37, N'CLOS', 9999, NULL, 0, 'Y', NULL, N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(37, N'NEW', 0, 20, 0, 'Y', N'api/ArrivalNotifications', N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(37, N'UPDT', 0, 20, 0, 'Y', NULL, N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(37, N'VRES', 20, 25, 0, 'Y', N'api/ArrivalNotifications/Isps/RequestToResubmit', N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(37, N'VUPD', 25, 20, 0, 'Y', NULL, N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(37, N'WFRE', 20, 9999, 0, 'Y', N'api/ArrivalNotifications/Isps/Reject', N'A', 1, '25-Feb-2015 8:11:42 PM', 1, '25-Feb-2015 8:11:42 PM', 'RB', 'Y'), 
(37, N'WFSA', 20, 9999, 0, 'Y', N'api/ArrivalNotifications/Isps/Approve', N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(38, N'CLOS', 9999, NULL, 0, 'Y', NULL, N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(38, N'NEW', 0, 20, 0, 'Y', N'api/ArrivalNotifications', N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(38, N'UPDT', 0, 20, 0, 'Y', NULL, N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(38, N'VRES', 20, 25, 0, 'Y', N'api/ArrivalNotifications/Ph/RequestToResubmit', N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(38, N'VUPD', 25, 20, 0, 'Y', NULL, N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(38, N'WFCA', 0, 9999, 0, 'Y', NULL, N'A', 1, '03-Dec-2014 10:16:40 AM', 1, '03-Dec-2014 10:16:40 AM', 'RB', 'Y'), 
(38, N'WFRE', 20, 9999, 0, 'Y', N'api/ArrivalNotifications/Ph/Reject', N'A', 1, '25-Feb-2015 8:11:42 PM', 1, '25-Feb-2015 8:11:42 PM', 'RB', 'Y'), 
(38, N'WFSA', 20, 9999, 0, 'Y', N'api/ArrivalNotifications/Ph/Approve', N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(39, N'CLOS', 9999, NULL, 0, 'Y', NULL, N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(39, N'NEW', 0, 20, 0, 'Y', N'api/ArrivalNotifications', N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(39, N'UPDT', 0, 20, 0, 'Y', NULL, N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(39, N'VRES', 20, 25, 0, 'Y', N'api/ArrivalNotifications/Imdg/RequestToResubmit', N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(39, N'VUPD', 25, 20, 0, 'Y', NULL, N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(39, N'WFRE', 20, 9999, 0, 'Y', N'api/ArrivalNotifications/Imdg/Reject', N'A', 1, '25-Feb-2015 8:11:42 PM', 1, '25-Feb-2015 8:11:42 PM', 'RB', 'Y'), 
(39, N'WFSA', 20, 9999, 0, 'Y', N'api/ArrivalNotifications/Imdg/Approve', N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(41, N'CLOS', 9999, NULL, 0, 'Y', NULL, N'A', 1, '10-Feb-2015 2:55:12 PM', 1, '10-Feb-2015 2:55:12 PM', 'RB', 'N'), 
(41, N'NEW', 0, 10, 0, 'Y', NULL, N'A', 1, '10-Feb-2015 2:54:02 PM', 1, '10-Feb-2015 2:54:02 PM', 'RB', 'N'), 
(41, N'REJ', 10, 9999, 0, 'Y', N'api/VesselAgentChange/Reject', N'A', 1, '10-Feb-2015 2:54:52 PM', 1, '10-Feb-2015 2:54:52 PM', 'RB', 'Y'), 
(41, N'WFRE', 20, 9999, 0, 'Y', N'api/VesselAgentChange/Reject', N'A', 1, '10-Feb-2015 2:55:03 PM', 1, '10-Feb-2015 2:55:03 PM', 'RB', 'Y'), 
(41, N'WFSA', 20, 9999, 0, 'Y', N'api/VesselAgentChange/Approve', N'A', 1, '10-Feb-2015 2:55:01 PM', 1, '10-Feb-2015 2:55:01 PM', 'RB', 'N'), 
(41, N'WFVE', 10, 20, 0, 'Y', N'api/VesselAgentChange/Verify', N'A', 1, '10-Feb-2015 2:54:58 PM', 1, '10-Feb-2015 2:54:58 PM', 'RB', 'N'), 
(42, N'CLOS', 9999, NULL, 0, 'Y', NULL, N'A', 2, '28-Mar-2015 4:06:57 PM', 2, '28-Mar-2015 4:06:57 PM', 'RB', 'N'), 
(42, N'NEW', 0, 20, 0, 'Y', NULL, N'A', 2, '28-Mar-2015 4:06:57 PM', 2, '28-Mar-2015 4:06:57 PM', 'RB', 'N'), 
(42, N'UPDT', 0, 20, 0, 'Y', NULL, N'A', 2, '28-Mar-2015 4:06:57 PM', 2, '28-Mar-2015 4:06:57 PM', 'RB', 'N'), 
(42, N'WFCA', 30, 9999, 0, 'Y', N'api/ServiceRequests/Cancel', N'A', 2, '28-Mar-2015 4:06:57 PM', 2, '28-Mar-2015 4:06:57 PM', 'RB', 'Y'), 
(42, N'WFCO', 30, 9999, 0, 'Y', N'api/ServiceRequests/Confirm', N'A', 2, '28-Mar-2015 4:06:57 PM', 2, '28-Mar-2015 4:06:57 PM', 'RB', 'N'), 
(42, N'WFRE', 20, 9999, 0, 'Y', N'api/ServiceRequests/Reject', N'A', 2, '28-Mar-2015 4:06:57 PM', 2, '28-Mar-2015 4:06:57 PM', 'RB', 'Y'), 
(42, N'WFSA', 20, 30, 0, 'Y', N'api/ServiceRequests/Approve', N'A', 2, '28-Mar-2015 4:06:57 PM', 2, '28-Mar-2015 4:06:57 PM', 'RB', 'N'), 
(43, N'CLOS', 9999, NULL, 0, 'Y', NULL, N'A', 1, '26-Dec-2014 5:31:56 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(43, N'NEW', 0, 20, 0, 'Y', NULL, N'A', 1, '26-Dec-2014 5:31:56 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(43, N'WFRE', 20, 9999, 0, 'Y', N'api/LicensingRequest/RejectLicenseRegistration', N'A', 1, '26-Dec-2014 5:31:56 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'Y'), 
(43, N'WFSA', 20, 9999, 0, 'Y', N'api/LicensingRequest/ApproveLicenseRegistration', N'A', 1, '26-Dec-2014 5:31:56 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(44, N'NEW', 0, 20, 0, 'Y', NULL, N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(44, N'WFRE', 20, 9999, 0, 'Y', N'api/PilotExemptionRequest/ApprovePilotExemptionRegistration', N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'Y'), 
(44, N'WFSA', 20, 9999, 0, 'Y', N'api/PilotExemptionRequest/ApprovePilotExemptionRegistration', N'A', 2, '12-Dec-2014 11:20:46 PM', 2, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(50, N'NEW', 0, 20, 0, 'Y', NULL, N'A', 1, '07-Jan-2015 7:10:14 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(50, N'UPDT', 0, 20, 0, 'Y', NULL, N'A', 1, '07-Jan-2015 7:10:14 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(50, N'WFRE', 20, 9999, 0, 'Y', N'api/DivingRequest/RejectDivingRequestOccupation', N'A', 1, '07-Jan-2015 7:10:14 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'Y'), 
(50, N'WFSA', 20, 9999, 0, 'Y', N'api/DivingRequest/ApproveDivingRequestOccupation', N'A', 1, '07-Jan-2015 7:10:14 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(62, N'CLOS', 9999, NULL, 0, 'Y', NULL, N'A', 1, '26-Dec-2014 4:52:51 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(62, N'NEW', 0, 20, 0, 'Y', NULL, N'A', 1, '26-Dec-2014 4:52:51 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(62, N'UPDT', 0, 20, 0, 'Y', NULL, N'A', 1, '26-Dec-2014 4:52:51 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(62, N'WFRE', 20, 9999, 0, 'Y', N'api/BerthMaintenances/Reject', N'A', 1, '26-Dec-2014 4:52:51 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'Y'), 
(62, N'WFSA', 20, 9999, 0, 'Y', N'api/BerthMaintenances/Approve', N'A', 1, '26-Dec-2014 4:52:51 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(63, N'CLOS', 9999, NULL, 0, 'Y', NULL, N'A', 1, '31-Dec-2014 4:50:39 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(63, N'NEW', 0, 20, 0, 'Y', NULL, N'A', 1, '31-Dec-2014 4:50:33 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(63, N'UPDT', 0, 20, 0, 'Y', NULL, N'A', 1, '31-Dec-2014 4:50:36 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(63, N'WFRE', 20, 9999, 0, 'Y', N'api/BerthMaintenanceCompletions/Reject', N'A', 1, '31-Dec-2014 4:50:39 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'Y'), 
(63, N'WFSA', 20, 9999, 0, 'Y', N'api/BerthMaintenanceCompletions/Approve', N'A', 1, '31-Dec-2014 4:50:39 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(67, N'CLOS', 9999, NULL, 0, 'Y', NULL, N'A', 1, '07-Jan-2015 7:09:46 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(67, N'NEW', 0, 20, 0, 'Y', NULL, N'A', 1, '07-Jan-2015 7:09:46 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(67, N'UPDT', 0, 20, 0, 'Y', NULL, N'A', 1, '07-Jan-2015 7:09:46 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(67, N'WFRE', 20, 9999, 0, 'Y', N'api/SupplymentaryServiceRequest/RejectSupplymentaryServiceRequest', N'A', 1, '07-Jan-2015 7:09:46 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'Y'), 
(67, N'WFSA', 20, 9999, 0, 'Y', N'api/SupplymentaryServiceRequest/ApproveSupplymentaryServiceRequest', N'A', 1, '07-Jan-2015 7:09:46 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(70, N'CLOS', 9999, NULL, 0, 'Y', NULL, N'A', 1, '26-Dec-2014 5:02:09 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(70, N'NEW', 0, 20, 0, 'Y', NULL, N'A', 1, '26-Dec-2014 5:02:09 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(70, N'UPDT', 0, 20, 0, 'Y', NULL, N'A', 1, '21-Jan-2015 8:49:55 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(70, N'WFRE', 20, 9999, 0, 'Y', N'api/FuelRequisition/Reject', N'A', 1, '26-Dec-2014 5:02:09 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'Y'), 
(70, N'WFSA', 20, 9999, 0, 'Y', N'api/FuelRequisition/Approve', N'A', 1, '26-Dec-2014 5:02:09 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(71, N'CLOS', 9999, NULL, 0, 'Y', NULL, N'A', 1, '26-Dec-2014 4:59:21 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(71, N'NEW', 0, 40, 0, 'Y', NULL, N'A', 1, '26-Dec-2014 4:59:21 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(71, N'UPDT', 0, 40, 0, 'Y', NULL, N'A', 1, '21-Jan-2015 8:50:04 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(71, N'WFAK', 40, 9999, 0, 'Y', N'api/FuelReceipt/Acknowledge', N'A', 1, '26-Dec-2014 4:59:21 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(114, N'CLOS', 9999, NULL, 0, 'Y', NULL, N'A', 1, '26-Dec-2014 4:44:10 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(114, N'NEW', 0, 20, 0, 'Y', NULL, N'A', 1, '26-Dec-2014 4:44:10 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(114, N'UPDT', 0, 20, 0, 'Y', NULL, N'A', 1, '26-Dec-2014 4:44:10 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(114, N'WFCA', 30, 9999, 0, 'Y', N'api/SuppDryDock/Cancel', N'A', 1, '26-Dec-2014 4:44:10 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'Y'), 
(114, N'WFCO', 30, 9999, 0, 'Y', N'api/SuppDryDock/Confirm', N'A', 1, '26-Dec-2014 4:44:10 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(114, N'WFRE', 20, 9999, 0, 'Y', N'api/SuppDryDock/Reject', N'A', 1, '26-Dec-2014 4:44:10 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'Y'), 
(114, N'WFSA', 20, 30, 0, 'Y', N'api/SuppDryDock/Approve', N'A', 1, '26-Dec-2014 4:44:10 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(125, N'CLOS', 9999, NULL, 0, 'Y', NULL, N'A', 1, '11-Feb-2015 6:18:07 PM', 1, '11-Feb-2015 6:18:07 PM', 'RB', 'N'), 
(125, N'NEW', 0, 20, 0, 'Y', NULL, N'A', 1, '11-Feb-2015 6:18:07 PM', 1, '11-Feb-2015 6:18:07 PM', 'RB', 'N'), 
(125, N'UPDT', 0, 20, 0, 'Y', NULL, N'A', 1, '11-Feb-2015 6:19:36 PM', 1, '11-Feb-2015 6:19:36 PM', 'RB', 'N'), 
(125, N'WFRE', 20, 9999, 0, 'Y', N'api/DockingPlan/Reject', N'A', 1, '11-Feb-2015 6:18:07 PM', 1, '11-Feb-2015 6:18:07 PM', 'RB', 'Y'), 
(125, N'WFSA', 20, 9999, 0, 'Y', N'api/DockingPlan/Approve', N'A', 1, '11-Feb-2015 6:18:07 PM', 1, '11-Feb-2015 6:18:07 PM', 'RB', 'N'), 
(128, N'CLOS', 9999, NULL, 0, 'Y', NULL, N'A', 1, '26-Dec-2014 5:20:45 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(128, N'NEW', 0, 40, 0, 'Y', NULL, N'A', 1, '26-Dec-2014 5:20:45 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(128, N'UPDT', 0, 40, 0, 'Y', NULL, N'A', 1, '26-Dec-2014 5:20:45 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(128, N'WFAK', 40, 9999, 0, 'Y', N'api/DepartureNotice/Acknowledge', N'A', 1, '26-Dec-2014 5:20:45 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(131, N'CLOS', 9999, NULL, 0, 'Y', NULL, N'A', 1, '31-Dec-2014 4:56:28 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(131, N'NEW', 0, 20, 0, 'Y', NULL, N'A', 1, '31-Dec-2014 4:56:28 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(131, N'UPDT', 0, 20, 0, 'Y', NULL, N'A', 1, '31-Dec-2014 4:56:28 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(131, N'WFRE', 20, 9999, 0, 'Y', N'api/SuppDryDockExtension/RejectSuppDryDockExtension', N'A', 1, '31-Dec-2014 4:56:28 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'Y'), 
(131, N'WFSA', 20, 9999, 0, 'Y', N'api/SuppDryDockExtension/ApproveSuppDryDockExtension', N'A', 1, '31-Dec-2014 4:56:28 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(134, N'CLOS', 9999, NULL, 0, 'Y', NULL, N'A', 1, '07-Jan-2015 7:35:03 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(134, N'NEW', 0, 20, 0, 'Y', NULL, N'A', 1, '07-Jan-2015 7:35:03 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(134, N'WFRE', 20, 9999, 0, 'Y', N'api/DredgingPriority/Reject', N'A', 1, '07-Jan-2015 7:35:03 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'Y'), 
(134, N'WFSA', 20, 9999, 0, 'Y', N'api/DredgingPriority/Approve', N'A', 1, '07-Jan-2015 7:35:03 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(135, N'CLOS', 9999, NULL, 0, 'Y', NULL, N'A', 1, '07-Jan-2015 7:36:23 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(135, N'NEW', 0, 20, 0, 'Y', NULL, N'A', 1, '07-Jan-2015 7:36:23 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(135, N'WFRE', 20, 9999, 0, 'Y', N'api/BerthOccupation/Reject', N'A', 1, '07-Jan-2015 7:36:23 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'Y'), 
(135, N'WFSA', 20, 9999, 0, 'Y', N'api/BerthOccupation/Approve', N'A', 1, '07-Jan-2015 7:36:23 PM', 1, '04-Feb-2015 5:29:27 PM', 'RB', 'N'), 
(136, N'CLOS', 9999, NULL, 0, 'Y', NULL, N'A', 1, '12-Feb-2015 2:58:38 PM', 1, '12-Feb-2015 2:58:38 PM', 'RB', 'N'), 
(136, N'NEW', 0, 20, 0, 'Y', NULL, N'A', 1, '12-Feb-2015 2:58:38 PM', 1, '12-Feb-2015 2:58:38 PM', 'RB', 'N'), 
(136, N'WFRE', 20, 9999, 0, 'Y', N'api/DredgingVolume/Reject', N'A', 1, '12-Feb-2015 2:58:38 PM', 1, '12-Feb-2015 2:58:38 PM', 'RB', 'Y'), 
(136, N'WFSA', 20, 9999, 0, 'Y', N'api/DredgingVolume/Approve', N'A', 1, '12-Feb-2015 2:58:38 PM', 1, '12-Feb-2015 2:58:38 PM', 'RB', 'N')

insert into WorkflowTaskRole
(EntityID, RoleID, Step, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, PortCode) VALUES
(15, 1, 0, N'A', 1, '31-Mar-2015 4:04:11 PM', 1, '31-Mar-2015 4:04:11 PM', 'RB'), 
(15, 23, 40, N'A', 1, '31-Mar-2015 4:04:11 PM', 1, '31-Mar-2015 4:04:11 PM', 'RB'), 
(33, 2, 0, N'A', 1, '04-Feb-2015 5:20:02 PM', 1, '04-Feb-2015 5:20:02 PM', 'RB'), 
(33, 3, 0, N'A', 1, '04-Feb-2015 5:20:02 PM', 1, '04-Feb-2015 5:20:02 PM', 'RB'), 
(33, 32, 20, N'A', 1, '04-Feb-2015 5:20:02 PM', 1, '04-Feb-2015 5:20:02 PM', 'RB'), 
(34, 1, 0, N'A', 1, '12-Feb-2015 3:10:34 PM', 1, '12-Feb-2015 3:10:34 PM', 'RB'), 
(34, 12, 10, N'A', 1, '12-Feb-2015 3:10:34 PM', 1, '12-Feb-2015 3:10:34 PM', 'RB'), 
(34, 13, 20, N'A', 1, '12-Feb-2015 3:10:34 PM', 1, '12-Feb-2015 3:10:34 PM', 'RB'), 
(35, 1, 0, N'A', 2, '12-Dec-2014 11:22:54 PM', 2, '12-Dec-2014 11:22:54 PM', 'RB'), 
(35, 1, 10, N'A', 2, '12-Dec-2014 11:22:54 PM', 2, '12-Dec-2014 11:22:54 PM', 'RB'), 
(35, 1, 20, N'A', 2, '12-Dec-2014 11:22:54 PM', 2, '12-Dec-2014 11:22:54 PM', 'RB'), 
(36, 2, 0, N'A', 2, '12-Dec-2014 11:22:54 PM', 2, '12-Dec-2014 11:22:54 PM', 'RB'), 
(36, 2, 25, N'A', 2, '12-Dec-2014 11:22:54 PM', 2, '12-Dec-2014 11:22:54 PM', 'RB'), 
(36, 3, 20, N'A', 2, '12-Dec-2014 11:22:54 PM', 2, '12-Dec-2014 11:22:54 PM', 'RB'), 
(37, 2, 0, N'A', 2, '12-Dec-2014 11:22:54 PM', 2, '12-Dec-2014 11:22:54 PM', 'RB'), 
(37, 2, 25, N'A', 2, '12-Dec-2014 11:22:54 PM', 2, '12-Dec-2014 11:22:54 PM', 'RB'), 
(37, 6, 20, N'A', 2, '12-Dec-2014 11:22:54 PM', 2, '12-Dec-2014 11:22:54 PM', 'RB'), 
(38, 2, 0, N'A', 2, '12-Dec-2014 11:22:54 PM', 2, '12-Dec-2014 11:22:54 PM', 'RB'), 
(38, 2, 25, N'A', 2, '12-Dec-2014 11:22:54 PM', 2, '12-Dec-2014 11:22:54 PM', 'RB'), 
(38, 7, 20, N'A', 2, '12-Dec-2014 11:22:54 PM', 2, '12-Dec-2014 11:22:54 PM', 'RB'), 
(39, 2, 0, N'A', 2, '12-Dec-2014 11:22:54 PM', 2, '12-Dec-2014 11:22:54 PM', 'RB'), 
(39, 2, 25, N'A', 2, '12-Dec-2014 11:22:54 PM', 2, '12-Dec-2014 11:22:54 PM', 'RB'), 
(39, 8, 20, N'A', 2, '12-Dec-2014 11:22:54 PM', 2, '12-Dec-2014 11:22:54 PM', 'RB'), 
(41, 2, 0, N'A', 1, '10-Feb-2015 2:45:00 PM', 1, '10-Feb-2015 2:45:00 PM', 'RB'), 
(41, 2, 10, N'A', 1, '10-Feb-2015 2:45:05 PM', 1, '10-Feb-2015 2:45:05 PM', 'RB'), 
(41, 10, 20, N'A', 1, '10-Feb-2015 2:45:08 PM', 1, '10-Feb-2015 2:45:08 PM', 'RB'), 
(42, 2, 0, N'A', 2, '28-Mar-2015 4:06:57 PM', 2, '28-Mar-2015 4:06:57 PM', 'RB'), 
(42, 2, 30, N'A', 2, '28-Mar-2015 4:06:57 PM', 2, '28-Mar-2015 4:06:57 PM', 'RB'), 
(42, 4, 0, N'A', 2, '28-Mar-2015 4:06:57 PM', 2, '28-Mar-2015 4:06:57 PM', 'RB'), 
(42, 4, 30, N'A', 2, '28-Mar-2015 4:06:57 PM', 2, '28-Mar-2015 4:06:57 PM', 'RB'), 
(42, 5, 20, N'A', 2, '28-Mar-2015 4:06:57 PM', 2, '28-Mar-2015 4:06:57 PM', 'RB'), 
(43, 1, 0, N'A', 1, '26-Dec-2014 5:35:08 PM', 1, '26-Dec-2014 5:35:08 PM', 'RB'), 
(43, 20, 20, N'A', 1, '26-Dec-2014 5:35:08 PM', 1, '26-Dec-2014 5:35:08 PM', 'RB'), 
(44, 1, 0, N'A', 2, '12-Dec-2014 11:22:54 PM', 2, '12-Dec-2014 11:22:54 PM', 'RB'), 
(44, 9, 20, N'A', 2, '12-Dec-2014 11:22:54 PM', 2, '12-Dec-2014 11:22:54 PM', 'RB'), 
(50, 5, 20, N'A', 1, '07-Jan-2015 7:10:16 PM', 1, '07-Jan-2015 7:10:16 PM', 'RB'), 
(50, 14, 0, N'A', 1, '07-Jan-2015 7:10:16 PM', 1, '07-Jan-2015 7:10:16 PM', 'RB'), 
(50, 17, 20, N'A', 1, '07-Jan-2015 7:10:16 PM', 1, '07-Jan-2015 7:10:16 PM', 'RB'), 
(62, 3, 20, N'A', 1, '26-Dec-2014 4:53:04 PM', 1, '26-Dec-2014 4:53:04 PM', 'RB'), 
(62, 9, 20, N'A', 1, '26-Dec-2014 4:53:04 PM', 1, '26-Dec-2014 4:53:04 PM', 'RB'), 
(62, 10, 20, N'A', 1, '26-Dec-2014 4:53:04 PM', 1, '26-Dec-2014 4:53:04 PM', 'RB'), 
(62, 15, 0, N'A', 1, '26-Dec-2014 4:53:04 PM', 1, '26-Dec-2014 4:53:04 PM', 'RB'), 
(63, 3, 20, N'A', 1, '31-Dec-2014 4:50:53 PM', 1, '31-Dec-2014 4:50:53 PM', 'RB'), 
(63, 9, 20, N'A', 1, '31-Dec-2014 4:50:53 PM', 1, '31-Dec-2014 4:50:53 PM', 'RB'), 
(63, 10, 20, N'A', 1, '31-Dec-2014 4:50:53 PM', 1, '31-Dec-2014 4:50:53 PM', 'RB'), 
(63, 15, 0, N'A', 1, '31-Dec-2014 4:50:53 PM', 1, '31-Dec-2014 4:50:53 PM', 'RB'), 
(67, 2, 0, N'A', 1, '07-Jan-2015 7:09:48 PM', 1, '07-Jan-2015 7:09:48 PM', 'RB'), 
(67, 16, 20, N'A', 1, '07-Jan-2015 7:09:48 PM', 1, '07-Jan-2015 7:09:48 PM', 'RB'), 
(70, 22, 0, N'A', 1, '26-Dec-2014 5:02:11 PM', 1, '26-Dec-2014 5:02:11 PM', 'RB'), 
(70, 23, 20, N'A', 1, '26-Dec-2014 5:02:11 PM', 1, '26-Dec-2014 5:02:11 PM', 'RB'), 
(70, 24, 0, N'A', 1, '26-Dec-2014 5:02:11 PM', 1, '26-Dec-2014 5:02:11 PM', 'RB'), 
(70, 26, 0, N'A', 1, '26-Dec-2014 5:02:11 PM', 1, '26-Dec-2014 5:02:11 PM', 'RB'), 
(71, 22, 0, N'A', 1, '26-Dec-2014 4:59:17 PM', 1, '26-Dec-2014 4:59:17 PM', 'RB'), 
(71, 23, 40, N'A', 1, '26-Dec-2014 4:59:17 PM', 1, '26-Dec-2014 4:59:17 PM', 'RB'), 
(71, 25, 40, N'A', 1, '26-Dec-2014 4:59:17 PM', 1, '26-Dec-2014 4:59:17 PM', 'RB'), 
(71, 33, 0, N'A', 1, '26-Dec-2014 4:59:17 PM', 1, '26-Dec-2014 4:59:17 PM', 'RB'), 
(114, 2, 0, N'A', 1, '26-Dec-2014 4:44:44 PM', 1, '26-Dec-2014 4:44:44 PM', 'RB'), 
(114, 2, 30, N'A', 1, '26-Dec-2014 4:44:44 PM', 1, '26-Dec-2014 4:44:44 PM', 'RB'), 
(114, 27, 20, N'A', 1, '26-Dec-2014 4:44:44 PM', 1, '26-Dec-2014 4:44:44 PM', 'RB'), 
(125, 2, 0, N'A', 1, '11-Feb-2015 6:18:20 PM', 1, '11-Feb-2015 6:18:20 PM', 'RB'), 
(125, 27, 20, N'A', 1, '11-Feb-2015 6:18:20 PM', 1, '11-Feb-2015 6:18:20 PM', 'RB'), 
(128, 2, 0, N'A', 1, '26-Dec-2014 5:21:42 PM', 1, '26-Dec-2014 5:21:42 PM', 'RB'), 
(128, 4, 0, N'A', 1, '26-Dec-2014 5:21:42 PM', 1, '26-Dec-2014 5:21:42 PM', 'RB'), 
(128, 5, 40, N'A', 1, '26-Dec-2014 5:21:42 PM', 1, '26-Dec-2014 5:21:42 PM', 'RB'), 
(128, 17, 40, N'A', 1, '26-Dec-2014 5:21:42 PM', 1, '26-Dec-2014 5:21:42 PM', 'RB'), 
(131, 2, 0, N'A', 1, '31-Dec-2014 4:56:32 PM', 1, '31-Dec-2014 4:56:32 PM', 'RB'), 
(131, 27, 20, N'A', 1, '31-Dec-2014 4:56:32 PM', 1, '31-Dec-2014 4:56:32 PM', 'RB'), 
(134, 9, 0, N'A', 1, '07-Jan-2015 7:35:06 PM', 1, '07-Jan-2015 7:35:06 PM', 'RB'), 
(134, 19, 20, N'A', 1, '07-Jan-2015 7:35:06 PM', 1, '07-Jan-2015 7:35:06 PM', 'RB'), 
(135, 9, 20, N'A', 1, '08-Feb-2015 12:03:58 PM', 1, '08-Feb-2015 12:03:58 PM', 'RB'), 
(135, 10, 20, N'A', 1, '08-Feb-2015 12:03:58 PM', 1, '08-Feb-2015 12:03:58 PM', 'RB'), 
(135, 19, 0, N'A', 1, '08-Feb-2015 12:03:55 PM', 1, '08-Feb-2015 12:03:55 PM', 'RB'), 
(136, 19, 20, N'A', 1, '12-Feb-2015 2:58:45 PM', 1, '12-Feb-2015 2:58:45 PM', 'RB'), 
(136, 35, 0, N'A', 1, '12-Feb-2015 2:58:45 PM', 1, '12-Feb-2015 2:58:45 PM', 'RB')

set identity_insert AutomatedSlotExtend on
insert into AutomatedSlotExtend
(ID, PortCode, SlotDate, ExtendYn) VALUES
(3, N'RB', N'2015-04-02 12:00:00 AM', 'N'), 
(4, N'RB', N'4/3/2015 12:00:00 AM', 'Y')
set identity_insert AutomatedSlotExtend off