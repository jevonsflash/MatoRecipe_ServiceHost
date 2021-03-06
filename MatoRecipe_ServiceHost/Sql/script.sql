USE [bds296555359_db]
GO
/****** Object:  Table [dbo].[cook_classify]    Script Date: 2017/7/1 下午 10:25:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cook_classify](
	[Id] [int] NOT NULL,
	[cook_class] [int] NULL,
	[description] [nvarchar](50) NULL,
	[name] [nvarchar](50) NULL,
	[seq] [int] NULL,
	[title] [nvarchar](50) NULL,
	[create_time] [datetime] NOT NULL,
 CONSTRAINT [PK_cook_classify] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cook_show_item]    Script Date: 2017/7/1 下午 10:25:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cook_show_item](
	[Id] [int] NOT NULL,
	[count] [int] NULL,
	[fcount] [int] NULL,
	[food] [nvarchar](255) NULL,
	[img] [nchar](255) NULL,
	[name] [nvarchar](50) NULL,
	[rcount] [int] NULL,
	[create_time] [datetime] NOT NULL,
 CONSTRAINT [PK_cook_show_item] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[show_item_classify]    Script Date: 2017/7/1 下午 10:25:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[show_item_classify](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[classify_id] [int] NOT NULL,
	[item_id] [int] NOT NULL,
	[create_time] [datetime] NOT NULL,
 CONSTRAINT [PK_show_item_classify] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cook_classify] ADD  CONSTRAINT [DF_cook_classify_create_time]  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[cook_show_item] ADD  CONSTRAINT [DF_cook_show_item_update_time]  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[show_item_classify] ADD  CONSTRAINT [DF_show_item_classify_create_time]  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[show_item_classify]  WITH CHECK ADD  CONSTRAINT [FK_show_item_classify_classify_id] FOREIGN KEY([classify_id])
REFERENCES [dbo].[cook_classify] ([Id])
GO
ALTER TABLE [dbo].[show_item_classify] CHECK CONSTRAINT [FK_show_item_classify_classify_id]
GO
ALTER TABLE [dbo].[show_item_classify]  WITH CHECK ADD  CONSTRAINT [FK_show_item_classify_show_item_id] FOREIGN KEY([item_id])
REFERENCES [dbo].[cook_show_item] ([Id])
GO
ALTER TABLE [dbo].[show_item_classify] CHECK CONSTRAINT [FK_show_item_classify_show_item_id]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cook_classify'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cook_show_item'
GO
