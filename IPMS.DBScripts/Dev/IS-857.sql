alter table [dbo].[News] add  PortCode nvarchar(2) REFERENCES Port(PortCode);



update [dbo].[News] set PortCode='DB' 

alter table [dbo].[News] alter column PortCode nvarchar(2) NOT NULL


