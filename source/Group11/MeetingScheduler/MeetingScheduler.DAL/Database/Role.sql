USE [MeetingScheduler]
GO

/****** Object:  Table [dbo].[Role]    Script Date: 11-10-2023 11:52:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Role](
	[Role_Id] [int] NOT NULL,
	[Role_Name] [nchar](10) NULL,
	[Last_Updt_User_id] [varchar](50) NULL,
	[Last_Updt_Ts] [datetime] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Role_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


