CREATE TABLE [dbo].[StatementCommodity](
	[StatementCommodityID] [int] IDENTITY(1,1) NOT NULL,	
  [StatementFactID] [int] NOT NULL,
  [TerminalOperatorID] int NOT NULL,
  [PortCode] [nvarchar](2) NOT NULL,
	[QuayCode] [nvarchar](4) NOT NULL,
	[BerthCode] [nvarchar](4) NOT NULL,
  [Commodity] [nvarchar](4) NOT NULL,
	[CargoType] [nvarchar](4) NOT NULL,
	[Package] [nvarchar](4) NOT NULL,
	[UOM] [nvarchar](4) NOT NULL,
	[Quantity] [numeric](14, 3) NULL,
	[RecordStatus] [nchar](1) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NOT NULL,
CONSTRAINT [PK_StatementCommodity_StatementCommodityID] PRIMARY KEY CLUSTERED 
(
	[StatementCommodityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[StatementCommodity] ADD  DEFAULT ('A') FOR [RecordStatus]
GO

ALTER TABLE [dbo].[StatementCommodity] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[StatementCommodity] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO

ALTER TABLE [dbo].[StatementCommodity]  WITH CHECK ADD  CONSTRAINT [FK_StatementCommodity_TerminalOperatorID] FOREIGN KEY([TerminalOperatorID])
REFERENCES [dbo].[TerminalOperator] ([TerminalOperatorID])
GO

ALTER TABLE [dbo].[StatementCommodity] CHECK CONSTRAINT [FK_StatementCommodity_TerminalOperatorID]
GO

ALTER TABLE [dbo].[StatementCommodity]  WITH CHECK ADD  CONSTRAINT [FK_StatementCommodity_CargoType] FOREIGN KEY([CargoType])
REFERENCES [dbo].[SubCategory] ([SubCatCode])
GO

ALTER TABLE [dbo].[StatementCommodity] CHECK CONSTRAINT [FK_StatementCommodity_CargoType]
GO

ALTER TABLE [dbo].[StatementCommodity]  WITH CHECK ADD  CONSTRAINT [FK_StatementCommodity_Commodity] FOREIGN KEY([Commodity])
REFERENCES [dbo].[SubCategory] ([SubCatCode])
GO

ALTER TABLE [dbo].[StatementCommodity]  WITH CHECK ADD  CONSTRAINT [FK_StatementCommodity_PortCodeQuayCodeBerthCode] FOREIGN KEY([PortCode], [QuayCode], [BerthCode])
REFERENCES [dbo].[Berth] ([PortCode], [QuayCode], [BerthCode])
GO

ALTER TABLE [dbo].[StatementCommodity] CHECK CONSTRAINT [FK_StatementCommodity_PortCodeQuayCodeBerthCode]
GO

ALTER TABLE [dbo].[StatementCommodity] CHECK CONSTRAINT [FK_StatementCommodity_Commodity]
GO

ALTER TABLE [dbo].[StatementCommodity]  WITH CHECK ADD  CONSTRAINT [FK_StatementCommodity_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserID])
GO

ALTER TABLE [dbo].[StatementCommodity] CHECK CONSTRAINT [FK_StatementCommodity_CreatedBy]
GO

ALTER TABLE [dbo].[StatementCommodity]  WITH CHECK ADD  CONSTRAINT [FK_StatementCommodity_ModifiedBy] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[Users] ([UserID])
GO

ALTER TABLE [dbo].[StatementCommodity] CHECK CONSTRAINT [FK_StatementCommodity_ModifiedBy]
GO

ALTER TABLE [dbo].[StatementCommodity]  WITH CHECK ADD  CONSTRAINT [FK_StatementCommodity_Package] FOREIGN KEY([Package])
REFERENCES [dbo].[SubCategory] ([SubCatCode])
GO

ALTER TABLE [dbo].[StatementCommodity] CHECK CONSTRAINT [FK_StatementCommodity_Package]
GO

ALTER TABLE [dbo].[StatementCommodity]  WITH CHECK ADD  CONSTRAINT [FK_StatementCommodity_UOM] FOREIGN KEY([UOM])
REFERENCES [dbo].[SubCategory] ([SubCatCode])
GO

ALTER TABLE [dbo].[StatementCommodity] CHECK CONSTRAINT [FK_StatementCommodity_UOM]
GO

ALTER TABLE [dbo].[StatementCommodity]  WITH CHECK ADD  CONSTRAINT [FK_StatementCommodity_StatementFactID] FOREIGN KEY([StatementFactID])
REFERENCES [dbo].[StatementFact] ([StatementFactID])
GO

ALTER TABLE [dbo].[StatementCommodity]  WITH CHECK ADD  CONSTRAINT [CHK_StatementCommodity_RecordStatus] CHECK  (([RecordStatus]='A' OR [RecordStatus]='I'))
GO

ALTER TABLE [dbo].[StatementCommodity] CHECK CONSTRAINT [CHK_StatementCommodity_RecordStatus]
GO



