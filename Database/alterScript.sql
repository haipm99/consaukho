CREATE TABLE [dbo].[Users](
	[Id] [varchar](100) NOT NULL primary key,
	[Username] [varchar](50) NULL,
	[Password] [varchar](100) NULL,
	[Fullname] [varchar](50) NULL,
	[Salt] [varchar](100) NULL,
)