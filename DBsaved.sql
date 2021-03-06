CREATE DATABASE [cookbook]
GO
USE [cookbook]
GO
/****** Object:  Table [dbo].[categories]    Script Date: 3/1/2017 5:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categories](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[style] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cookbook]    Script Date: 3/1/2017 5:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cookbook](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[recipe_id] [int] NULL,
	[category_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[recipes]    Script Date: 3/1/2017 5:13:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[recipes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[ingredients] [varchar](255) NULL,
	[instructions] [text] NULL,
	[rating] [varchar](255) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[categories] ON 

INSERT [dbo].[categories] ([id], [style]) VALUES (1, N'curry')
INSERT [dbo].[categories] ([id], [style]) VALUES (2, N'dog')
SET IDENTITY_INSERT [dbo].[categories] OFF
SET IDENTITY_INSERT [dbo].[recipes] ON 

INSERT [dbo].[recipes] ([id], [name], [ingredients], [instructions], [rating]) VALUES (1, N'fish', N'salmon', N'boil', N'4')
INSERT [dbo].[recipes] ([id], [name], [ingredients], [instructions], [rating]) VALUES (2, N'fish', N'salmon', N'boil', N'4')
INSERT [dbo].[recipes] ([id], [name], [ingredients], [instructions], [rating]) VALUES (3, N'taco', N'taco', N'taco', N'4')
INSERT [dbo].[recipes] ([id], [name], [ingredients], [instructions], [rating]) VALUES (4, N'asd', N'asd', N'asd', N'3')
SET IDENTITY_INSERT [dbo].[recipes] OFF
