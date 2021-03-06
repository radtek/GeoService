USE [master]
GO
/****** Object:  Database [ZipCodeData]    Script Date: 6/19/2018 5:26:12 PM ******/
CREATE DATABASE [ZipCodeData]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ZipCodeData', FILENAME = N'D:\Repo\ZipCodeData.mdf' , SIZE = 21504KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ZipCodeData_log', FILENAME = N'D:\Repo\ZipCodeData_log.ldf' , SIZE = 22144KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ZipCodeData] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ZipCodeData].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ZipCodeData] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ZipCodeData] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ZipCodeData] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ZipCodeData] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ZipCodeData] SET ARITHABORT OFF 
GO
ALTER DATABASE [ZipCodeData] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ZipCodeData] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ZipCodeData] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ZipCodeData] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ZipCodeData] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ZipCodeData] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ZipCodeData] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ZipCodeData] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ZipCodeData] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ZipCodeData] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ZipCodeData] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ZipCodeData] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ZipCodeData] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ZipCodeData] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ZipCodeData] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ZipCodeData] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ZipCodeData] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ZipCodeData] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ZipCodeData] SET  MULTI_USER 
GO
ALTER DATABASE [ZipCodeData] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ZipCodeData] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ZipCodeData] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ZipCodeData] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [ZipCodeData] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ZipCodeData] SET QUERY_STORE = OFF
GO
USE [ZipCodeData]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [ZipCodeData]
GO
/****** Object:  Table [dbo].[State]    Script Date: 6/19/2018 5:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[StateId] [int] IDENTITY(1,1) NOT NULL,
	[Abbreviation] [varchar](2) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[IsPrimaryState] [bit] NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ZipCode]    Script Date: 6/19/2018 5:26:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ZipCode](
	[ZipCodeId] [int] IDENTITY(1,1) NOT NULL,
	[City] [nvarchar](255) NULL,
	[StateId] [int] NULL,
	[Zip] [varchar](5) NULL,
	[County] [nvarchar](255) NULL,
	[AreaCode] [int] NULL,
	[Fips] [int] NULL,
	[TimeZone] [nvarchar](255) NULL,
	[ObservesDST] [bit] NULL,
	[Latitude] [float] NULL,
	[Longitude] [float] NULL,
 CONSTRAINT [PK_USZipCodes] PRIMARY KEY CLUSTERED 
(
	[ZipCodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[ZipCodeGetWithinRange]    Script Date: 6/19/2018 5:26:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[ZipCodeGetWithinRange]
	@p_Zip varchar(5),
	@p_Miles float
As
	Begin
	
		Declare @Degrees float,
				@Id int,
				@Latitude float,
				@Longitude float
		
		Select @Degrees = @p_Miles / 69.047

		Select
			@Id = a.Id,
			@Latitude = a.Latitude,
			@Longitude = a.Longitude
		From
			ZipCode a
		Where
			a.Zip = @p_Zip

		Select
			IsNull([Id], 0) 'Id',
			IsNull([City], '') 'City',
			IsNull([County], '') 'County',
			IsNull([State], '') 'State',
			IsNull([AreaCode], 0) 'AreaCode',
			IsNull([Fips], 0) 'Fips',
			IsNull([TimeZone], '') 'TimeZone',
			IsNull([Dst], '') 'Dst',
			IsNull([Latitude], 0) 'Latitude',
			IsNull([Longitude], 0) 'Longitude',
			IsNull([Zip], '') 'Zip'
		From
			[ZipCode]
		Where
			([Latitude] <= @Latitude + @Degrees And [Latitude] >= @Latitude - @Degrees) And
			([Longitude] <= @Longitude + @Degrees And [Longitude] >= @Longitude - @Degrees)
			
	End
GO
USE [master]
GO
ALTER DATABASE [ZipCodeData] SET  READ_WRITE 
GO
