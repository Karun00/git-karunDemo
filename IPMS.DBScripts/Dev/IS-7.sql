INSERT INTO PortGeneralConfig(PortCode, ConfigName, ConfigValue, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, ConfigLabelName, GroupName)
SELECT Portcode,'SERVREQPRECOND2','0','A' as RecordStatus,2 as CreatedBy, getDate() as CreatedDate,2 as ModifiedBy,getDate() as ModifiedDate, 'Service request bunkers filing prior minimum duration (Minutes)', 'Service Request Bunkers' from Port;