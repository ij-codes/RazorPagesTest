# User registration implemented in .NET6 with Razor Pages

Environment
---
- Visual Studio 2022
- .NET 6
- SQL Server
- Database creation script (Creates `user_registration` database & `Users` table within it)

```
USE [master]
GO

/****** Object:  Database [user_registration]    Script Date: 3/23/2022 12:17:51 PM ******/
CREATE DATABASE [user_registration]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'user_registration', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\user_registration.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'user_registration_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\user_registration_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [user_registration].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [user_registration] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [user_registration] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [user_registration] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [user_registration] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [user_registration] SET ARITHABORT OFF 
GO

ALTER DATABASE [user_registration] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [user_registration] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [user_registration] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [user_registration] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [user_registration] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [user_registration] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [user_registration] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [user_registration] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [user_registration] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [user_registration] SET  DISABLE_BROKER 
GO

ALTER DATABASE [user_registration] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [user_registration] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [user_registration] SET TRUSTWORTHY OFF 

USE [user_registration]
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
- Clean up & parameterize the db creation script (It was just exported from SSMS as part of this scope)
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



