ALTER TABLE VesselCallMovement 
ALTER COLUMN Slot	nvarchar(20) NULL;	


ALTER TABLE ResourceAllocation 
ALTER COLUMN AllocSlot	nvarchar(20) NULL;	


Update AutomatedSlotConfiguration set OperationalPeriod = '0', Duration = '120' where PortCode IN ('DB', 'EL','MB','NG','SB')

Update AutomatedSlotConfiguration set OperationalPeriod = '420', Duration = '120' where PortCode IN ('CT')

Update AutomatedSlotConfiguration set OperationalPeriod = '360', Duration = '90' where PortCode IN ('RB','PE')
