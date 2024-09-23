Update Berth set BerthName ='Dry Dock', ToMeter = 341.44, Lengthm = 341.44
where BerthType = 'BTCO' and PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '1'
GO
Update Bollard set ShortName = 'DD18'
where PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '2' and BollardCode = '12'
GO
Update Bollard set ShortName = 'DD19'
where PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '2' and BollardCode = '13'
GO
Update Bollard set ShortName = 'DD20'
where PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '2' and BollardCode = '14'
GO
Update Bollard set ShortName = 'DD21'
where PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '2' and BollardCode = '15'
GO
Update Bollard set ShortName = 'DD22'
where PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '2' and BollardCode = '16'
GO
Update Bollard set ShortName = 'DD23'
where PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '2' and BollardCode = '17'
GO
INSERT INTO Bollard
(PortCode, QuayCode, BerthCode, BollardCode, BollardName, ShortName, FromMeter, ToMeter, Continuous, Description, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Coordinates, OffsetCoordinates, MidCoordinates) 
SELECT 'DB', 'DD', '1', '12', 'DD12', 'DD12', 195.07, 219.47, 'Y', 'Description', 'A', 1, getDate(), 1, getDate(), '-29.8852095585, 30.9927374655', NULL, NULL 
Where '12'  NOT IN (SELECT BollardCode from Bollard where PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '1' and BollardCode = '12')
GO
INSERT INTO Bollard
(PortCode, QuayCode, BerthCode, BollardCode, BollardName, ShortName, FromMeter, ToMeter, Continuous, Description, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Coordinates, OffsetCoordinates, MidCoordinates) 
SELECT 'DB', 'DD', '1', '13', 'DD13', 'DD13', 219.47, 243.80, 'Y', 'Description', 'A', 1, getDate(), 1, getDate(), '-29.8852420387, 30.9924883356', NULL, NULL
Where '13'  NOT IN (SELECT BollardCode from Bollard where PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '1' and BollardCode = '13')
GO
INSERT INTO Bollard
(PortCode, QuayCode, BerthCode, BollardCode, BollardName, ShortName, FromMeter, ToMeter, Continuous, Description, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Coordinates, OffsetCoordinates, MidCoordinates) 
SELECT 'DB', 'DD', '1', '14', 'DD14', 'DD14', 243.80, 268.29, 'Y', 'Description', 'A', 1, getDate(), 1, getDate(), '-29.8852739411, 30.992237456', NULL, NULL
Where '14'  NOT IN (SELECT BollardCode from Bollard where PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '1' and BollardCode = '14')
GO

INSERT INTO Bollard
(PortCode, QuayCode, BerthCode, BollardCode, BollardName, ShortName, FromMeter, ToMeter, Continuous, Description, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Coordinates, OffsetCoordinates, MidCoordinates) 
SELECT 'DB', 'DD', '1', '15', 'DD15', 'DD15', 268.29, 292.54, 'Y', 'Description', 'A', 1, getDate(), 1, getDate(), '-29.885305374, 30.9919889988', NULL, NULL
Where '15'  NOT IN (SELECT BollardCode from Bollard where PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '1' and BollardCode = '15')
GO
INSERT INTO Bollard
(PortCode, QuayCode, BerthCode, BollardCode, BollardName, ShortName, FromMeter, ToMeter, Continuous, Description, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Coordinates, OffsetCoordinates, MidCoordinates) 
SELECT 'DB', 'DD', '1', '16', 'DD16', 'DD16', 292.54, 317.07, 'Y', 'Description', 'A', 1, getDate(), 1, getDate(), '-29.8853375369, 30.9917377669', NULL, NULL
Where '16'  NOT IN (SELECT BollardCode from Bollard where PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '1' and BollardCode = '16')
GO
INSERT INTO Bollard
(PortCode, QuayCode, BerthCode, BollardCode, BollardName, ShortName, FromMeter, ToMeter, Continuous, Description, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Coordinates, OffsetCoordinates, MidCoordinates) 
SELECT 'DB', 'DD', '1', '17', 'DD17', 'DD17', 317.07, 341.44, 'Y', 'Description', 'A', 1, getDate(), 1, getDate(), '-29.8853697988, 30.9914882326', NULL, NULL
Where '17'  NOT IN (SELECT BollardCode from Bollard where PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '1' and BollardCode = '17')
GO

-- Exsisting Records Updation

Update ArrivalCommodity set BerthCode = '1' where PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '2'
GO
Update DredgingOperation set BerthCode = '1' where PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '2'
GO
Update FuelReceipt set BerthCode = '1' where PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '2'
GO
Update MaterialCodeMaster set BerthCode = '1' where PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '2'
GO
Update OtherServiceRecording set BerthCode = '1' where PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '2'
GO
Update OutTurnVolume set BerthCode = '1'  where PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '2'
GO

Update SuppDryDock set DockBerthCode = '1' where DockPortCode = 'DB' and DockQuayCode = 'DD' and DockBerthCode = '2'
GO
Update SuppServiceRequest set BerthCode = '1' where PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '2'
GO
Update TerminalData set BerthCode = '1' where PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '2'
GO
Update TerminalDelay set BerthCode = '1' where PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '2'
GO
Update TerminalOperatorBerth set BerthCode = '1' where PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '2'  
GO
Update ArrivalNotification set PreferredBerthCode = '1' where PreferredPortCode = 'DB' and PreferredQuayCode = 'DD' and PreferredBerthCode = '2'
GO
Update ArrivalNotification set AlternateBerthCode = '1' where AlternatePortCode = 'DB' and AlternateQuayCode = 'DD' and AlternateBerthCode = '2'
GO
Update ArrivalNotification set DryDockBerthCode = '1' where DryDockBerthPortCode = 'DB' and DryDockBerthQuayCode = 'DD' and DryDockBerthCode = '2'
GO

Update BerthMaintenance set MaintBerthCode = '1'  where MaintPortCode = 'DB' and MaintQuayCode = 'DD' and MaintBerthCode = '2' 
GO
Update BerthMaintenance set FromBerthCode = '1'  where FromPortCode = 'DB' and FromQuayCode = 'DD' and FromBerthCode = '2'
GO
Update BerthMaintenance set ToBerthCode = '1' where ToPortCode = 'DB' and ToQuayCode = 'DD' and ToBerthCode = '2'
GO

Update DivingRequest set FromBerthCode = '1' where FromPortCode = 'DB' and FromQuayCode = 'DD' and FromBerthCode = '2'
GO
Update DivingRequest set ToBerthCode = '1' where  ToPortCode = 'DB' and ToQuayCode = 'DD' and ToBerthCode = '2'
GO
Update ServiceRequestShifting set ToBerthCode = '1'  where ToPortCode = 'DB' and ToQuayCode = 'DD' and ToBerthCode = '2'
GO
Update ServiceRequestShifting set FromPositionBerthCode = '1' where FromPositionPortCode = 'DB' and FromPositionQuayCode = 'DD' and FromPositionBerthCode = '2'
GO
Update ServiceRequestShifting set ToPositionBerthCode = '1' where ToPositionPortCode = 'DB' and ToPositionQuayCode = 'DD' and ToPositionBerthCode = '2'
GO
Update ServiceRequestWarping set FromPositionBerthCode = '1' where FromPositionPortCode = 'DB' and FromPositionQuayCode = 'DD' and FromPositionBerthCode = '2'
GO
Update ServiceRequestWarping set ToPositionBerthCode = '1' where ToPositionPortCode = 'DB' and ToPositionQuayCode = 'DD' and ToPositionBerthCode = '2'
GO
Update ShiftingBerthingTaskExecution set FromBerthCode = '1' where FromBerthPortCode  = 'DB' and FromBerthQuayCode  = 'DD' and FromBerthCode  = '2'
GO
Update ShiftingBerthingTaskExecution set ToBerthCode = '1' where ToBerthPortCode = 'DB' and  ToBerthQuayCode = 'DD' and ToBerthCode  = '2'
GO
Update ShiftingBerthingTaskExecution set FromBollardBerthCode = '1' where FromBollardPortCode  = 'DB' and FromBollardQuayCode  = 'DD' and FromBollardBerthCode  = '2'
GO
Update ShiftingBerthingTaskExecution set ToBollardBerthCode = '1' where ToBollardPortCode  = 'DB' and  ToBollardQuayCode = 'DD' and ToBollardBerthCode  = '2'
GO
Update ShiftingBerthingTaskExecution set MooringBollardBowBerthCode = '1' where MooringBollardBowPortcode  = 'DB' and MooringBollardBowQuayCode = 'DD' and MooringBollardBowBerthCode  = '2'
GO
Update ShiftingBerthingTaskExecution set MooringBollardStemBerthCode = '1' where MooringBollardStemPortcode  = 'DB' and MooringBollardStemQuayCode  = 'DD' and MooringBollardStemBerthCode  = '2'
GO
Update VesselCall set FromPositionBerthCode = '1' where FromPositionPortCode = 'DB' and FromPositionQuayCode = 'DD' and FromPositionBerthCode = '2'

GO
Update VesselCall set ToPositionBerthCode = '1' where ToPositionPortCode = 'DB' and ToPositionQuayCode = 'DD' and ToPositionBerthCode = '2'

GO

Update VesselCallmovement set FromPositionBerthCode = '1' where FromPositionPortCode = 'DB' and FromPositionQuayCode = 'DD' and FromPositionBerthCode = '2'

GO
Update VesselCallmovement set ToPositionBerthCode = '1' where ToPositionPortCode = 'DB' and ToPositionQuayCode = 'DD' and ToPositionBerthCode = '2'
GO
Update VesselCallmovement set MooringBollardBowBerthCode = '1' where MooringBollardBowPortCode = 'DB' and MooringBollardBowQuayCode = 'DD' and MooringBollardBowBerthCode = '2'
GO
Update VesselCallmovement set MooringBollardStemBerthCode = '1' where MooringBollardStemPortCode = 'DB' and MooringBollardStemQuayCode = 'DD' and MooringBollardStemBerthCode = '2'
GO
delete from Bollard where PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '2'
GO
delete from BerthReasonForVisit where PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '2'
GO
delete from BerthVesselType where PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '2'
GO
delete from BerthCargo where PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '2'
GO
delete from Berth where PortCode = 'DB' and QuayCode = 'DD' and BerthCode = '2'
GO





Update ArrivalCommodity set BerthCode = 'DDB1' where PortCode = 'DB' and QuayCode = 'MW' and BerthCode = 'DDB2'
GO
Update DredgingOperation set BerthCode = 'DDB1' where PortCode = 'DB' and QuayCode = 'MW' and BerthCode = 'DDB2'
GO
Update FuelReceipt set  BerthCode = 'DDB1' where PortCode = 'DB' and QuayCode = 'MW' and BerthCode = 'DDB2' 
GO
Update MaterialCodeMaster set BerthCode = 'DDB1' where PortCode = 'DB' and QuayCode = 'MW' and BerthCode = 'DDB2' 
GO
Update OtherServiceRecording set BerthCode = 'DDB1' where PortCode = 'DB' and QuayCode = 'MW' and BerthCode = 'DDB2' 
GO
Update OutTurnVolume set BerthCode = 'DDB1' where PortCode = 'DB' and QuayCode = 'MW' and BerthCode = 'DDB2' 
GO
Update SuppDryDock set DockBerthCode = 'DDB1' where DockPortCode = 'DB' and DockQuayCode = 'MW' and DockBerthCode = 'DDB2'
GO
Update SuppServiceRequest set  BerthCode = 'DDB1' where PortCode = 'DB' and QuayCode = 'MW' and BerthCode = 'DDB2' 
GO
Update TerminalData set BerthCode = 'DDB1' where PortCode = 'DB' and QuayCode = 'MW' and BerthCode = 'DDB2' 
GO
Update TerminalDelay set BerthCode = 'DDB1' where PortCode = 'DB' and QuayCode = 'MW' and BerthCode = 'DDB2' 
GO
Update TerminalOperatorBerth set  BerthCode = 'DDB1' where PortCode = 'DB' and QuayCode = 'MW' and BerthCode = 'DDB2' 
GO
Update ArrivalNotification set PreferredBerthCode = 'DDB1' where PreferredPortCode = 'DB' and PreferredQuayCode = 'MW' and PreferredBerthCode = 'DDB2'
GO
Update ArrivalNotification set AlternateBerthCode = 'DDB1' where AlternatePortCode = 'DB' and AlternateQuayCode = 'MW' and AlternateBerthCode = 'DDB2'
GO
Update ArrivalNotification set DryDockBerthCode = 'DDB1' where DryDockBerthPortCode = 'DB' and DryDockBerthQuayCode = 'MW' and DryDockBerthCode = 'DDB2'
GO
Update BerthMaintenance set  MaintBerthCode = 'DDB1'  where MaintPortCode = 'DB' and MaintQuayCode = 'MW' and MaintBerthCode = 'DDB2' 
GO
Update BerthMaintenance set  FromBerthCode = 'DDB1'  where FromPortCode = 'DB' and FromQuayCode = 'MW' and FromBerthCode = 'DDB2'
GO
Update BerthMaintenance set ToBerthCode = 'DDB1' where ToPortCode = 'DB' and ToQuayCode = 'MW' and ToBerthCode = 'DDB2'
GO
Update DivingRequest set FromBerthCode = 'DDB1' where FromPortCode = 'DB' and FromQuayCode = 'MW' and FromBerthCode = 'DDB2'
GO
Update DivingRequest set ToBerthCode = 'DDB1' where  ToPortCode = 'DB' and ToQuayCode = 'MW' and ToBerthCode = 'DDB2'
GO
Update ServiceRequestShifting set ToBerthCode = 'DDB1'  where ToPortCode = 'DB' and ToQuayCode = 'MW' and ToBerthCode = 'DDB2'
GO
Update ServiceRequestShifting set FromPositionBerthCode = 'DDB1' where FromPositionPortCode = 'DB' and FromPositionQuayCode = 'MW' and FromPositionBerthCode = 'DDB2'
GO
Update ServiceRequestShifting set ToPositionBerthCode = 'DDB1' where ToPositionPortCode = 'DB' and ToPositionQuayCode = 'MW' and ToPositionBerthCode = 'DDB2'
GO
Update ServiceRequestWarping set FromPositionBerthCode = 'DDB1' where FromPositionPortCode = 'DB' and FromPositionQuayCode = 'MW' and FromPositionBerthCode = 'DDB2'
GO
Update ServiceRequestWarping set ToPositionBerthCode = 'DDB1' where ToPositionPortCode = 'DB' and ToPositionQuayCode = 'MW' and ToPositionBerthCode = 'DDB2'
GO
Update ShiftingBerthingTaskExecution set FromBerthCode = 'DDB1' where FromBerthPortCode  = 'DB' and FromBerthQuayCode  = 'MW' and FromBerthCode  = 'DDB2'
GO
Update ShiftingBerthingTaskExecution set  ToBerthCode = 'DDB1' where ToBerthPortCode = 'DB' and  ToBerthQuayCode = 'MW' and ToBerthCode  = 'DDB2'
GO
Update ShiftingBerthingTaskExecution set FromBollardBerthCode = 'DDB1' where FromBollardPortCode  = 'DB' and FromBollardQuayCode  = 'MW' and FromBollardBerthCode  = 'DDB2'
GO
Update ShiftingBerthingTaskExecution set  ToBollardBerthCode = 'DDB1' where ToBollardPortCode  = 'DB' and  ToBollardQuayCode = 'MW' and ToBollardBerthCode  = 'DDB2'
GO
Update ShiftingBerthingTaskExecution set MooringBollardBowBerthCode = 'DDB1' where MooringBollardBowPortcode  = 'DB' and MooringBollardBowQuayCode = 'MW' and MooringBollardBowBerthCode  = 'DDB2'
GO
Update ShiftingBerthingTaskExecution set MooringBollardStemBerthCode = 'DDB1' where MooringBollardStemPortcode  = 'DB' and MooringBollardStemQuayCode  = 'MW' and MooringBollardStemBerthCode  = 'DDB2'
GO
Update VesselCall set FromPositionBerthCode = 'DDB1' where FromPositionPortCode = 'DB' and FromPositionQuayCode = 'MW' and FromPositionBerthCode = 'DDB2'
GO
Update VesselCall set ToPositionBerthCode = 'DDB1' where ToPositionPortCode = 'DB' and ToPositionQuayCode = 'MW' and ToPositionBerthCode = 'DDB2'
GO
Update VesselCallmovement set FromPositionBerthCode = 'DDB1' where FromPositionPortCode = 'DB' and FromPositionQuayCode = 'MW' and FromPositionBerthCode = 'DDB2'
GO
Update VesselCallmovement set ToPositionBerthCode = 'DDB1' where ToPositionPortCode = 'DB' and ToPositionQuayCode = 'MW' and ToPositionBerthCode = 'DDB2'
GO
Update VesselCallmovement set MooringBollardBowBerthCode = 'DDB1' where MooringBollardBowPortCode = 'DB' and MooringBollardBowQuayCode = 'MW' and MooringBollardBowBerthCode = 'DDB2'
GO
Update VesselCallmovement set  MooringBollardStemBerthCode = 'DDB1' where MooringBollardStemPortCode = 'DB' and MooringBollardStemQuayCode = 'MW' and MooringBollardStemBerthCode = 'DDB2'
GO
Update Berth set BerthName = 'Dry Dock Scheduler', ToMeter= 300, Lengthm = 200 where PortCode = 'DB' and QuayCode = 'MW' and BerthCode = 'DDB1'

delete from BerthReasonForVisit where PortCode = 'DB' and QuayCode = 'MW' and BerthCode = 'DDB2'
GO
delete from BerthVesselType where PortCode = 'DB' and  QuayCode = 'MW' and BerthCode = 'DDB2'
GO
delete from BerthCargo where PortCode = 'DB' and  QuayCode = 'MW' and BerthCode = 'DDB2'
GO
delete from Berth where BerthType = 'DRDK' and PortCode = 'DB' and  QuayCode = 'MW' and BerthCode = 'DDB2'


GO
Update WorkflowProcess set ReferenceData = replace(referencedata, 'Inner Dry Dock', 'Dry Dock')
where ReferenceData like '%Inner Dry Dock%' 
GO
Update WorkflowProcess set ReferenceData = replace(referencedata, 'Outer Dry Dock', 'Dry Dock')
where ReferenceData like '%Outer Dry Dock%' 
GO
Update WorkflowProcess set ReferenceData = replace(referencedata, 'Dry Dock Berth1', 'Dry Dock')
where ReferenceData like '%Dry Dock Berth1%' 
GO
Update WorkflowProcess set ReferenceData = replace(referencedata, 'Dry Dockberth2', 'Dry Dock')
where ReferenceData like '%Dry Dockberth2%' 