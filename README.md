# User registration implemented in .NET6 with Razor Pages

Environment
---
- Visual Studio 2022
- .NET 6
- SQL Server
- Create users table in a SQL database

```
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[password] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
```

Configuration
---
- Update the `SqlServerConfig.ConnectionString` value in `appsettings.Development.json`
- Build the project
- Run the tests
- Run the project


Improvement points
---
- Split the solution into different projects
- Add resource files for (error)messages which would help reusability such as in tests and also supports translations
- Maybe think about vertical slices (project structure)
- Refactor the unit tests to improve quality and remove duplicate code (using SetUp action per scope)
- Add integration tests
- Add fluent validation (removes duplicate validation on VM (RazorPage) and Service layer)
- Extract the hardcoded queries into SQL stored procedures
- Add DTOs for retrieving data and exposing it to the presentation layer
- Setup CQRS architecture or a modified version (something like the VerticalToDo CRUD example I have here: https://github.com/ice-j/VerticalToDo)
- [Feature request] Add email verification for a new user registration
- ?
