USE [master]
GO
/****** Object:  Database [EJL73_DB]    Script Date: 17/05/2017 10:22:39 AM ******/
CREATE DATABASE [EJL73_DB] ON  PRIMARY 
( NAME = N'EJL73_DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\EJL73_DB.mdf' , SIZE = 2304KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'EJL73_DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\EJL73_DB_log.LDF' , SIZE = 576KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [EJL73_DB] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EJL73_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EJL73_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EJL73_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EJL73_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EJL73_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EJL73_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [EJL73_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EJL73_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EJL73_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EJL73_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EJL73_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EJL73_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EJL73_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EJL73_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EJL73_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EJL73_DB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [EJL73_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EJL73_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EJL73_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EJL73_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EJL73_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EJL73_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EJL73_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EJL73_DB] SET RECOVERY FULL 
GO
ALTER DATABASE [EJL73_DB] SET  MULTI_USER 
GO
ALTER DATABASE [EJL73_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EJL73_DB] SET DB_CHAINING OFF 
GO
EXEC sys.sp_db_vardecimal_storage_format N'EJL73_DB', N'ON'
GO
USE [EJL73_DB]
GO
/****** Object:  User [EJL73_USR]    Script Date: 17/05/2017 10:22:39 AM ******/
CREATE USER [EJL73_USR] FOR LOGIN [EJL73_USR] WITH DEFAULT_SCHEMA=[db_owner]
GO
ALTER ROLE [db_owner] ADD MEMBER [EJL73_USR]
GO
/****** Object:  Table [db_owner].[Change]    Script Date: 17/05/2017 10:22:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [db_owner].[Change](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Command] [int] NOT NULL,
	[TableName] [nvarchar](max) NOT NULL,
	[PrimaryKey] [nvarchar](max) NOT NULL,
 CONSTRAINT [Change_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [db_owner].[Reservations]    Script Date: 17/05/2017 10:22:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [db_owner].[Reservations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PeopleNr] [int] NOT NULL,
	[DateTo] [datetime2](7) NOT NULL,
	[DateFrom] [datetime2](7) NOT NULL,
	[Building] [char](1) NOT NULL,
	[FloorNr] [int] NOT NULL,
	[Nr] [int] NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
 CONSTRAINT [Reservations_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [db_owner].[Rooms]    Script Date: 17/05/2017 10:22:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [db_owner].[Rooms](
	[Building] [char](1) NOT NULL,
	[FloorNr] [int] NOT NULL,
	[Nr] [int] NOT NULL,
	[MaxPeople] [int] NOT NULL,
	[MinPermissionLevel] [int] NOT NULL,
 CONSTRAINT [Rooms_PK] PRIMARY KEY CLUSTERED 
(
	[Building] ASC,
	[FloorNr] ASC,
	[Nr] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [db_owner].[Users]    Script Date: 17/05/2017 10:22:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [db_owner].[Users](
	[Username] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[PermissionLevel] [int] NOT NULL,
 CONSTRAINT [User_PK] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [db_owner].[Reservations]  WITH CHECK ADD  CONSTRAINT [Reservations_Rooms_FK] FOREIGN KEY([Building], [FloorNr], [Nr])
REFERENCES [db_owner].[Rooms] ([Building], [FloorNr], [Nr])
ON DELETE CASCADE
GO
ALTER TABLE [db_owner].[Reservations] CHECK CONSTRAINT [Reservations_Rooms_FK]
GO
ALTER TABLE [db_owner].[Reservations]  WITH CHECK ADD  CONSTRAINT [Reservations_Users_FK] FOREIGN KEY([Username])
REFERENCES [db_owner].[Users] ([Username])
ON DELETE CASCADE
GO
ALTER TABLE [db_owner].[Reservations] CHECK CONSTRAINT [Reservations_Users_FK]
GO
/****** Object:  StoredProcedure [db_owner].[SP_GetAllReservations]    Script Date: 17/05/2017 10:22:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [db_owner].[SP_GetAllReservations] AS
BEGIN
SELECT ID, PeopleNr, DateTo, DateFrom, Building, FloorNr, Nr, Username
FROM Reservations
END
GO
/****** Object:  StoredProcedure [db_owner].[SP_GetAllRooms]    Script Date: 17/05/2017 10:22:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [db_owner].[SP_GetAllRooms] AS
BEGIN
SELECT Building, FloorNr, Nr, MaxPeople, MinPermissionLevel
FROM Rooms
END
GO
/****** Object:  StoredProcedure [db_owner].[SP_GetAllUsers]    Script Date: 17/05/2017 10:22:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [db_owner].[SP_GetAllUsers] AS
BEGIN
SELECT Username, Email, PermissionLevel
FROM Users
END
GO
/****** Object:  StoredProcedure [db_owner].[SP_InsertReservation]    Script Date: 17/05/2017 10:22:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [db_owner].[SP_InsertReservation] (@PeopleNr Int,
								       @DateTo DateTime2,
								       @DateFrom DateTime2,
									   @Building Char,
									   @FloorNr Int,
									   @Nr Int,
									   @Username NVarChar(100)) AS
BEGIN
INSERT INTO Reservations (PeopleNr, DateTo, DateFrom, Building, FloorNr, Nr, Username) VALUES
				         (@PeopleNr, @DateTo, @DateFrom, @Building, @FloorNr, @Nr, @Username)
END
GO
/****** Object:  StoredProcedure [db_owner].[SP_InsertRoom]    Script Date: 17/05/2017 10:22:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [db_owner].[SP_InsertRoom] (@Building Char(1),
								@FloorNr Int,
								@Nr Int,
								@MaxPeople Int,
								@MinPermissionLevel Int) AS
BEGIN
INSERT INTO Rooms (Building, FloorNr, Nr, MaxPeople, MinPermissionLevel) VALUES
				  (@Building, @FloorNr, @Nr, @MaxPeople, @MinPermissionLevel)
END
GO
/****** Object:  StoredProcedure [db_owner].[SP_InsertUser]    Script Date: 17/05/2017 10:22:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [db_owner].[SP_InsertUser] (@Username NVarChar(100),
								@Email NVarChar(MAX),
								@PermissionLevel Int) AS
BEGIN
INSERT INTO Users (Username, Email, PermissionLevel) VALUES
				 (@Username, @Email, @PermissionLevel)
END
GO
USE [master]
GO
ALTER DATABASE [EJL73_DB] SET  READ_WRITE 
GO
