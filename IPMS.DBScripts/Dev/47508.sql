Alter Table SuppMiscService Add StartMeter bigint null;
GO
Alter Table SuppMiscService Add EndMeter bigint null;
GO
update SuppMiscService set StartMeter=0 , EndMeter=0 
GO
Alter Table SuppMiscService alter column StartMeter bigint not null;
GO
Alter Table SuppMiscService alter column EndMeter bigint not null;
GO