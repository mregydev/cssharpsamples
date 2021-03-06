
CREATE DATABASE [NationalCriminals]

USE [NationalCriminals]
GO
/****** Object:  Table [dbo].[Criminal]    Script Date: 20/10/2015 08:14:32 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Criminal](
	[Id] [uniqueidentifier] NOT NULL,
	[FilePath] [nvarchar](520) NULL,
	[Name] [nvarchar](50) NULL,
	[Weight] [float] NULL,
	[Height] [float] NULL,
	[IsMale] [bit] NULL,
	[Nationality] [nvarchar](100) NULL,
	[Age] [float] NULL,
 CONSTRAINT [PK_Criminal] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 20/10/2015 08:14:33 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Email] [nchar](20) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
