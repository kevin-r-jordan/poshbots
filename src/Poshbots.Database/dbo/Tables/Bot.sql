﻿CREATE TABLE [dbo].[Bot]
(
	[BotId] INT NOT NULL PRIMARY KEY IDENTITY,
	[UserId] NVARCHAR(128) NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	[Code] NVARCHAR(MAX) NULL,
	[Wins] INT NULL,
	[Losses] INT NULL,
	[Draws] INT NULL
)