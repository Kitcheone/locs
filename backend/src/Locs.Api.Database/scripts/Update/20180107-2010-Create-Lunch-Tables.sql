CREATE TABLE [Locs].[Lunch](
	[Id] [uniqueidentifier] PRIMARY KEY,
	[NavigationUrl] [nvarchar](6) NOT NULL,
	[Location] [nvarchar](255) NOT NULL,
	[Date] [datetime] NULL,
	[MenuUrl] [nvarchar](255) NULL
);

GO

CREATE TABLE [Locs].[Attendee](
	[Id] [uniqueidentifier] PRIMARY KEY,
	[LunchId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Choice] [nvarchar](255) NOT NULL
	FOREIGN KEY (LunchId) REFERENCES [Locs].[Lunch](Id)
);

GO