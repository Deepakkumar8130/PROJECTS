USE [master]
GO
/****** Object:  Database [ClaimDB]    Script Date: 28-Dec-23 1:13:33 PM ******/
CREATE DATABASE [ClaimDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ClaimDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ClaimDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ClaimDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ClaimDB_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ClaimDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ClaimDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ClaimDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ClaimDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ClaimDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ClaimDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ClaimDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ClaimDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ClaimDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ClaimDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ClaimDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ClaimDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ClaimDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ClaimDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ClaimDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ClaimDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ClaimDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ClaimDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ClaimDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ClaimDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ClaimDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ClaimDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ClaimDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ClaimDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ClaimDB] SET RECOVERY FULL 
GO
ALTER DATABASE [ClaimDB] SET  MULTI_USER 
GO
ALTER DATABASE [ClaimDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ClaimDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ClaimDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ClaimDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ClaimDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ClaimDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ClaimDB', N'ON'
GO
ALTER DATABASE [ClaimDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [ClaimDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ClaimDB]
GO
/****** Object:  UserDefinedFunction [dbo].[HashPassword]    Script Date: 28-Dec-23 1:13:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[HashPassword](@pass VARCHAR(100))
RETURNS NVARCHAR(500)
AS
BEGIN
	DECLARE @afterHash VARBINARY(500) = HASHBYTES('SHA2_256', @pass)
	RETURN CONVERT(NVARCHAR(1000), @afterHash, 2)
END
GO
/****** Object:  Table [dbo].[Claim_Master]    Script Date: 28-Dec-23 1:13:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Claim_Master](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Claim_Title] [varchar](100) NULL,
	[Claim_Reason] [varchar](100) NULL,
	[Amount] [decimal](18, 0) NULL,
	[ClaimDt] [datetime] NULL,
	[Evidence] [varchar](500) NULL,
	[ExpenseDt] [datetime] NULL,
	[Claim_Description] [varchar](500) NULL,
	[CurrentStatus] [varchar](50) NULL,
	[Status] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee_Claim_Action]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee_Claim_Action](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimId] [int] NULL,
	[Action] [varchar](100) NULL,
	[ActionBy] [int] NULL,
	[ActionDt] [datetime] NULL,
	[Remarks] [varchar](100) NULL,
	[Status] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee_Claim_Master_Mapping]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee_Claim_Master_Mapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CurrentAction] [varchar](100) NULL,
	[NextAction] [varchar](100) NULL,
	[Status] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee_Claim_Role_Master]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee_Claim_Role_Master](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Role] [varchar](100) NULL,
	[Action] [varchar](100) NULL,
	[Status] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee_Claim_Transaction]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee_Claim_Transaction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Transaction_No] [varchar](100) NULL,
	[Employee_Id] [int] NULL,
	[Amount] [decimal](18, 0) NULL,
	[TransactionDt] [datetime] NULL,
	[ClaimId] [int] NULL,
	[Status] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Program_Master]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Program_Master](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[P_Title] [varchar](100) NULL,
	[Path] [varchar](500) NULL,
	[Descr] [varchar](500) NULL,
	[Display_Sequence] [int] NULL,
	[Status] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role_Employee_Mapping]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role_Employee_Mapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NULL,
	[EmpId] [int] NULL,
	[Status] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role_Master]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role_Master](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Role] [varchar](100) NULL,
	[Status] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Rights]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Rights](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Programe_id] [int] NULL,
	[UserId] [int] NULL,
	[RoleId] [int] NULL,
	[Status] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_Master]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Master](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nm] [varchar](50) NULL,
	[Email] [varchar](100) NULL,
	[Mobile] [varchar](50) NULL,
	[Password] [varchar](500) NULL,
	[Manager_Id] [int] NULL,
	[Status] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Employee_Claim_Master_Mapping] ON 

INSERT [dbo].[Employee_Claim_Master_Mapping] ([Id], [CurrentAction], [NextAction], [Status]) VALUES (1, N'Initiated', N'Pending At Manager', 1)
INSERT [dbo].[Employee_Claim_Master_Mapping] ([Id], [CurrentAction], [NextAction], [Status]) VALUES (2, N'Pending At Manager', N'Pending At HR', 1)
INSERT [dbo].[Employee_Claim_Master_Mapping] ([Id], [CurrentAction], [NextAction], [Status]) VALUES (3, N'Pending At Manager', N'Rejected By Manager', 0)
INSERT [dbo].[Employee_Claim_Master_Mapping] ([Id], [CurrentAction], [NextAction], [Status]) VALUES (4, N'Pending At HR', N'Pending At Account', 1)
INSERT [dbo].[Employee_Claim_Master_Mapping] ([Id], [CurrentAction], [NextAction], [Status]) VALUES (5, N'Pending At HR', N'Rejected At HR', 0)
INSERT [dbo].[Employee_Claim_Master_Mapping] ([Id], [CurrentAction], [NextAction], [Status]) VALUES (6, N'Pending At Account', N'Completed', 1)
SET IDENTITY_INSERT [dbo].[Employee_Claim_Master_Mapping] OFF
GO
SET IDENTITY_INSERT [dbo].[Employee_Claim_Role_Master] ON 

INSERT [dbo].[Employee_Claim_Role_Master] ([Id], [Role], [Action], [Status]) VALUES (1, N'Employee', N'Initiated', 1)
INSERT [dbo].[Employee_Claim_Role_Master] ([Id], [Role], [Action], [Status]) VALUES (2, N'Manager', N'Pending At Manager', 1)
INSERT [dbo].[Employee_Claim_Role_Master] ([Id], [Role], [Action], [Status]) VALUES (3, N'HR', N'Pending At HR', 1)
INSERT [dbo].[Employee_Claim_Role_Master] ([Id], [Role], [Action], [Status]) VALUES (4, N'Account', N'Pending At Account', 1)
SET IDENTITY_INSERT [dbo].[Employee_Claim_Role_Master] OFF
GO
SET IDENTITY_INSERT [dbo].[Program_Master] ON 

INSERT [dbo].[Program_Master] ([Id], [P_Title], [Path], [Descr], [Display_Sequence], [Status]) VALUES (1, N'Add Claim', N'Claim/AddClaim', N'Add new claim', 1, 1)
INSERT [dbo].[Program_Master] ([Id], [P_Title], [Path], [Descr], [Display_Sequence], [Status]) VALUES (2, N'Employee Claims', N'Claim/ShowClaim', N'show claim request', 2, 1)
INSERT [dbo].[Program_Master] ([Id], [P_Title], [Path], [Descr], [Display_Sequence], [Status]) VALUES (3, N'Dashboard', N'Home/Dashboard', N'dashboard', 3, 1)
INSERT [dbo].[Program_Master] ([Id], [P_Title], [Path], [Descr], [Display_Sequence], [Status]) VALUES (4, N'Show Claim Status', N'Claim/ClaimStatus', N'Show Claim Status', 4, 1)
INSERT [dbo].[Program_Master] ([Id], [P_Title], [Path], [Descr], [Display_Sequence], [Status]) VALUES (5, N'Claim Transactions', N'Claim/ShowClaimTransaction', N'Show Claims Transaction', 5, 1)
INSERT [dbo].[Program_Master] ([Id], [P_Title], [Path], [Descr], [Display_Sequence], [Status]) VALUES (6, N'Managed User', N'User/ManageUser', N'Users Managed', 6, 1)
INSERT [dbo].[Program_Master] ([Id], [P_Title], [Path], [Descr], [Display_Sequence], [Status]) VALUES (7, N'Managed Roles', N'Role/ManageRole', N'Roles Managed', 7, 1)
INSERT [dbo].[Program_Master] ([Id], [P_Title], [Path], [Descr], [Display_Sequence], [Status]) VALUES (8, N'Assign Role', N'Role/AssignRole', N'Assigned Role To User', 8, 1)
INSERT [dbo].[Program_Master] ([Id], [P_Title], [Path], [Descr], [Display_Sequence], [Status]) VALUES (9, N'Assign Individual Rights', N'ProgramRights/AssignRights', N'Assigned Program Rights', 9, 1)
INSERT [dbo].[Program_Master] ([Id], [P_Title], [Path], [Descr], [Display_Sequence], [Status]) VALUES (10, N'Assign Group Rights', N'ProgramRights/AssignGroupRights', N'Assigned Program Rights For Group', 10, 1)
INSERT [dbo].[Program_Master] ([Id], [P_Title], [Path], [Descr], [Display_Sequence], [Status]) VALUES (11, N'Managed Program', N'Program/ManageProgram', N'Programs Managed', 11, 1)
INSERT [dbo].[Program_Master] ([Id], [P_Title], [Path], [Descr], [Display_Sequence], [Status]) VALUES (12, N'Test A', N'Program /Test A', N'for Testin Purpose', 12, 1)
INSERT [dbo].[Program_Master] ([Id], [P_Title], [Path], [Descr], [Display_Sequence], [Status]) VALUES (13, N'Test B', N'Program /Test B', N'for testin purpose b', 13, 1)
INSERT [dbo].[Program_Master] ([Id], [P_Title], [Path], [Descr], [Display_Sequence], [Status]) VALUES (14, N'Test C', N'Program /Test C', N'for tesing purpose', 14, 1)
INSERT [dbo].[Program_Master] ([Id], [P_Title], [Path], [Descr], [Display_Sequence], [Status]) VALUES (15, N'Test e', N'Program /Test e', N'for tesing peeee', 15, 1)
SET IDENTITY_INSERT [dbo].[Program_Master] OFF
GO
SET IDENTITY_INSERT [dbo].[Role_Employee_Mapping] ON 

INSERT [dbo].[Role_Employee_Mapping] ([Id], [RoleId], [EmpId], [Status]) VALUES (1, 1, 2, 1)
INSERT [dbo].[Role_Employee_Mapping] ([Id], [RoleId], [EmpId], [Status]) VALUES (2, 2, 3, 1)
INSERT [dbo].[Role_Employee_Mapping] ([Id], [RoleId], [EmpId], [Status]) VALUES (3, 2, 1, 1)
INSERT [dbo].[Role_Employee_Mapping] ([Id], [RoleId], [EmpId], [Status]) VALUES (4, 3, 4, 1)
INSERT [dbo].[Role_Employee_Mapping] ([Id], [RoleId], [EmpId], [Status]) VALUES (5, 4, 5, 1)
INSERT [dbo].[Role_Employee_Mapping] ([Id], [RoleId], [EmpId], [Status]) VALUES (6, 6, 6, 1)
SET IDENTITY_INSERT [dbo].[Role_Employee_Mapping] OFF
GO
SET IDENTITY_INSERT [dbo].[Role_Master] ON 

INSERT [dbo].[Role_Master] ([Id], [Role], [Status]) VALUES (1, N'Employee', 1)
INSERT [dbo].[Role_Master] ([Id], [Role], [Status]) VALUES (2, N'Manager', 1)
INSERT [dbo].[Role_Master] ([Id], [Role], [Status]) VALUES (3, N'HR', 1)
INSERT [dbo].[Role_Master] ([Id], [Role], [Status]) VALUES (4, N'Account', 1)
INSERT [dbo].[Role_Master] ([Id], [Role], [Status]) VALUES (5, N'Admin', 1)
INSERT [dbo].[Role_Master] ([Id], [Role], [Status]) VALUES (6, N'Super Admin', 1)
INSERT [dbo].[Role_Master] ([Id], [Role], [Status]) VALUES (7, N'System Admin', 1)
SET IDENTITY_INSERT [dbo].[Role_Master] OFF
GO
SET IDENTITY_INSERT [dbo].[Tbl_Rights] ON 

INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (1, 1, NULL, 1, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (2, 3, NULL, 1, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (3, 4, NULL, 1, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (4, 5, NULL, 1, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (5, 1, NULL, 2, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (6, 2, NULL, 2, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (7, 3, NULL, 2, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (8, 4, NULL, 2, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (9, 5, NULL, 2, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (10, 1, NULL, 3, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (11, 2, NULL, 3, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (12, 3, NULL, 3, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (13, 4, NULL, 3, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (14, 5, NULL, 3, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (15, 1, NULL, 4, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (16, 2, NULL, 4, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (17, 3, NULL, 4, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (18, 4, NULL, 4, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (19, 5, NULL, 4, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (20, 1, NULL, 5, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (21, 2, NULL, 5, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (22, 3, NULL, 5, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (23, 4, NULL, 5, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (24, 5, NULL, 5, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (25, 6, NULL, 5, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (26, 7, NULL, 5, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (27, 1, NULL, 6, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (28, 2, NULL, 6, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (29, 3, NULL, 6, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (30, 4, NULL, 6, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (31, 5, NULL, 6, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (32, 6, NULL, 6, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (33, 7, NULL, 6, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (34, 8, NULL, 6, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (35, 9, NULL, 6, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (36, 10, NULL, 6, 1)
INSERT [dbo].[Tbl_Rights] ([Id], [Programe_id], [UserId], [RoleId], [Status]) VALUES (37, 11, NULL, 6, 1)
SET IDENTITY_INSERT [dbo].[Tbl_Rights] OFF
GO
SET IDENTITY_INSERT [dbo].[User_Master] ON 

INSERT [dbo].[User_Master] ([Id], [Nm], [Email], [Mobile], [Password], [Manager_Id], [Status]) VALUES (1, N'Akash Kumar', N'akash123@gmail.com', N'93483943', N'5BD4D4E7AB3495A126035CC0FCF08557192B0B027CC54F4FEDE03449833A13B8', NULL, 1)
INSERT [dbo].[User_Master] ([Id], [Nm], [Email], [Mobile], [Password], [Manager_Id], [Status]) VALUES (2, N'Rahul Kumar', N'rahul123@gmail.com', N'73483943', N'6110E881C3B1912B7C4D0E63D645AC95F529FBEAE7BD80DA7DCC56438F757CD8', 1, 1)
INSERT [dbo].[User_Master] ([Id], [Nm], [Email], [Mobile], [Password], [Manager_Id], [Status]) VALUES (3, N'Mayank Kumar', N'mayank0023@gmail.com', N'88483943', N'535B3311338EAE13AEB2CB13263F6B5948158A7DEA55090D8313A57552D9D4D1', 1, 1)
INSERT [dbo].[User_Master] ([Id], [Nm], [Email], [Mobile], [Password], [Manager_Id], [Status]) VALUES (4, N'Sumit Kumar', N'smt03@gmail.com', N'78483943', N'F0769D93661E28CDD2625A6AAC07AFEF373A81D2CDC5292794D058AB71BA7C2C', 3, 1)
INSERT [dbo].[User_Master] ([Id], [Nm], [Email], [Mobile], [Password], [Manager_Id], [Status]) VALUES (5, N'Pawan Kumar', N'pawan123@gmail.com', N'87483943', N'476D0D465F370CBAB7C04A8B03BBFAAB92DA7F5DA2413AD6C05D4E3326885395', 3, 1)
INSERT [dbo].[User_Master] ([Id], [Nm], [Email], [Mobile], [Password], [Manager_Id], [Status]) VALUES (6, N'Sunil Sir', N'sunilSir123@gmail.com', N'1234567890', N'53C47A3E491A27082028E9D03C33A6B27E1F5535AAC026FE8F98D321023964B0', 1, 1)
INSERT [dbo].[User_Master] ([Id], [Nm], [Email], [Mobile], [Password], [Manager_Id], [Status]) VALUES (11, N'testing', N'test@gmail.com', N'1741852963', N'6460662E217C7A9F899208DD70A2C28ABDEA42F128666A9B78E6C0C064846493', 1, 1)
INSERT [dbo].[User_Master] ([Id], [Nm], [Email], [Mobile], [Password], [Manager_Id], [Status]) VALUES (12, N'testing2', N'testing2@gmail.com', N'7856', N'55BB35C6E8D3A3F87618F5276D62F179F5991CCAE7450B433F6EFF3904AA110F', 1, 1)
SET IDENTITY_INSERT [dbo].[User_Master] OFF
GO
/****** Object:  StoredProcedure [dbo].[USP_ASSIGN_ROLE]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_ASSIGN_ROLE]
@UserId INT,
@RoleId INT,
@Status INT
AS
BEGIN
	/*--- IF ROLE ALREADY ASSIGNED TO USER ---*/
	IF EXISTS(SELECT 1 FROM Role_Employee_Mapping(NOLOCK)
				WHERE EmpId = @UserId
				AND RoleId = @RoleId 
				AND Status = @Status)
		BEGIN
			THROW 50000, 'This Role Already Assigned', 1;
			RETURN;
		END
	BEGIN TRY
		BEGIN TRAN trn_assign_role
		/*--- DE-ACTIVE THE ASSIGNED ROLE ---*/
			UPDATE Role_Employee_Mapping SET Status = 0 WHERE EmpId = @UserId;
			/*--- IF ROLE ALREADY ASSIGNED TO USER BUT NOT ACTIVE ---*/
			IF EXISTS(SELECT 1 FROM Role_Employee_Mapping(NOLOCK)
				WHERE EmpId = @UserId
				AND RoleId = @RoleId 
				AND Status = 0)
				BEGIN
					/*--- ACTIVE THE ROLE ---*/
					UPDATE Role_Employee_Mapping SET Status = 1 WHERE EmpId = @UserId AND RoleId = @RoleId;
					COMMIT TRAN trn_assign_role;
					RETURN;
				END
				/*--- INSERT THE NEW ROLE ASSIGN ENTRY ---*/
			INSERT INTO Role_Employee_Mapping (RoleId, EmpId, Status)
						VALUES(@RoleId, @UserId, @Status);
		COMMIT TRAN trn_assign_role;
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN trn_assign_role;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[USP_CHECK_PROGRAM_RIGHTS]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_CHECK_PROGRAM_RIGHTS]
@UserId INT,
@path VARCHAR(200)
AS
BEGIN
	DECLARE @programId INT;
	DECLARE @roleId INT;

	SELECT @programId = Id FROM Program_Master WHERE path = @path and Status = 1;
	SELECT @roleId = RoleId FROM Role_Employee_Mapping WHERE EmpId = @userid and Status =1;
	IF(@programId = (SELECT Id FROM Program_Master WHERE P_Title = 'Dashboard' and Status = 1))
		BEGIN 
			SELECT 1 as result
		END

	ELSE IF EXISTS ( SELECT 1 FROM Tbl_Rights(NOLOCK) WHERE Programe_id=@programId AND (UserId = @userid OR RoleId = @roleId) AND status = 1)
		BEGIN
			SELECT 1 AS result
		END
	ELSE
		BEGIN
			SELECT 0 AS result
		END
END
GO
/****** Object:  StoredProcedure [dbo].[USP_CLAIM_TRANSACTION_ENTRY]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_CLAIM_TRANSACTION_ENTRY]
@UserId INT,
@ClaimId INT
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN claim_tran
			DECLARE @amount DECIMAL;
			SELECT @amount = Amount FROM Claim_Master(NOLOCK) WHERE Id = @ClaimId;
			INSERT INTO Employee_Claim_Transaction(
									Transaction_No,
									Employee_Id,
									Amount,
									TransactionDt,
									CliamId,
									Status
									) VALUES(
									4548,
									@UserId,
									@amount,
									GETDATE(),
									@ClaimId,
									1
									);
		COMMIT TRAN claim_tran;
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN claim_tran;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_ACTIVE_ROLES]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_ACTIVE_ROLES]
AS
BEGIN
	SELECT
		Id, Role
		FROM  Role_Master(NOLOCK)
		WHERE Status = 1;
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_AUTHENTICATE]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_AUTHENTICATE]
@email VARCHAR(100),
@pass VARCHAR(100)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM User_Master(NOLOCK) WHERE Email = @email AND Status = 1)
		BEGIN
			IF EXISTS (SELECT 1 FROM User_Master(NOLOCK) WHERE Email = @email AND Password = DBO.HashPassword(@pass))
				BEGIN
					SELECT 1 AS RESULT;
				END
			ELSE
				BEGIN
					SELECT 2 AS RESULT;
				END
		END
	ELSE
		BEGIN
			SELECT 3 AS RESULT;
		END
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_CLAIM_ACTION_HISTORY]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_CLAIM_ACTION_HISTORY]
@claimId INT
AS
BEGIN
	SELECT
		CASE
			WHEN eca.Action = 'Initiated' THEN 'Raise A Claim Request'
			WHEN eca.Action = 'Pending At HR' THEN 'Approved By Manager'
			WHEN eca.Action = 'Pending At Account' THEN 'Approved By HR'
			WHEN eca.Action = 'Completed' THEN 'Claim Raised Successfully !!!'
			ELSE eca.Action
			END 'Action',
		um.Nm 'Name',
		eca.Remarks 'Remark',
		eca.ActionDt 'Action Date'
		FROM Employee_Claim_Action(NOLOCK) eca
		INNER JOIN User_Master(NOLOCK) um
		ON eca.ActionBy = um.Id
		WHERE eca.ClaimId = @claimId;
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_CLAIM_STATUS]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_CLAIM_STATUS]
@UserId INT
AS
BEGIN
	SELECT
	cm.Id, cm.Claim_Title, cm.Claim_Reason, cm.Amount, cm.ClaimDt,
	  CASE  
		   WHEN eca.Action = 'Initiated' THEN 'Raise A Claim Request'  
		   WHEN eca.Action = 'Pending At HR' THEN 'Approved By Manager'  
		   WHEN eca.Action = 'Pending At Account' THEN 'Approved By HR' 
		   ELSE eca.Action  
		   END 'Action',
	um.Nm AS ActionBy,cm.CurrentStatus
	FROM Claim_Master(NOLOCK) cm
	INNER JOIN Employee_Claim_Action(NOLOCK) eca
	ON cm.Id = eca.ClaimId
	INNER JOIN User_Master(NOLOCK) um
	ON um.Id = eca.ActionBy
	WHERE cm.CurrentStatus LIKE'%Pending%' 
	AND cm.Status = 1 AND cm.UserId = @UserId;
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_CLAIMS_TRANSACTION_DATA]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_CLAIMS_TRANSACTION_DATA]
@UserId INT
AS
BEGIN
	SELECT
		emt.Transaction_No,
		cm.Claim_Title,
		cm.Claim_Reason,
		cm.Amount,
		cm.Claim_Description,
		cm.ClaimDt,
		emt.TransactionDt,
		um.Nm ApprovedBy
		FROM Employee_Claim_Transaction(NOLOCK) emt
		INNER JOIN Claim_Master(NOLOCK) cm
		ON emt.ClaimId = cm.Id
		INNER JOIN User_Master(NOLOCK) um
		on emt.Employee_Id = um.Id
		WHERE cm.UserId = @UserId; 
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_PENDING_REQUEST]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_PENDING_REQUEST]
@role VARCHAR(20),
@userId INT
AS
BEGIN
	SELECT  cm.Id ClaimId,
			um.Nm EmployeeName,
			cm.Claim_Title ClaimTitle,
			cm.Claim_Reason ClaimReason,
			cm.Amount ClaimAmount,
			cm.ClaimDt ClaimDt,
			cm.Evidence ClaimEvidence,
			cm.ExpenseDt ClaimExpenseDt,
			cm.Claim_Description ClaimDescription,
			cm.CurrentStatus CurrentStatus
			FROM Claim_Master(NOLOCK) cm
			INNER JOIN User_Master(NOLOCK) um
			ON cm.UserId = um.Id
			WHERE cm.CurrentStatus = (SELECT rm.Action FROM Employee_Claim_Role_Master(NOLOCK) rm
												WHERE rm.Role = @role)
			AND um.Manager_Id = CASE WHEN @role = 'Manager'
										THEN @userId
										ELSE um.Manager_Id
								END;
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_PROGRAMS]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_PROGRAMS]
@userId INT
AS
BEGIN
	SELECT DISTINCT prog.Id, prog.P_title Title, prog.Path Path, prog.Descr Description
	FROM Tbl_Rights(NOLOCK) tbl
	INNER JOIN Program_Master(NOLOCK) prog
	ON prog.Id = tbl.Programe_id
	where tbl.UserId = @userId
	AND tbl.Status = 1 
	AND prog.Status = 1
	OR tbL.RoleId IN (SELECT RoleId FROM Role_Employee_Mapping(NOLOCK) WHERE EmpId = @userId)
	ORDER BY prog.Id;
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_PROGRAMS_RIGHTS_BY_ROLEID]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_PROGRAMS_RIGHTS_BY_ROLEID]
@RoleId INT
AS
BEGIN
	/*--- GENERATE A TEMPORARY TABLE THAT STORE TBL_RIGHTS COPY DATA ---*/
	SELECT Id, P_Title, Descr INTO #Program_Temp FROM Program_Master(NOLOCK)
					WHERE Status = 1;

	/*--- ADD A COLUMN "IsChecked" IN TEMPORARY TABLE 
			FOR CHECK WHICH RIGHTS ASSIGN TO USER OR NOT 
			FOR ASSIGN THEN IsChecked = 1 OTHERWISE = 0 ---*/
	ALTER TABLE #Program_Temp ADD IsChecked TINYINT DEFAULT 0 WITH VALUES;;

	/*--- UPDATE TEMPORARY TABLE COLUMN "IsChecked" FOR STORE A 1/0 
			ACCORDING TO USER ID ---*/
	UPDATE temp
			SET temp.IsChecked = 1 FROM #Program_Temp(NOLOCK) temp
			INNER JOIN Tbl_Rights(NOLOCK) r
			ON  temp.Id = r.Programe_id
			WHERE r.RoleId = @roleId
			AND r.Status = 1;

	SELECT*FROM #Program_Temp;
	/*--- DROP A TEMPORARY TABLE ---*/
	DROP TABLE #Program_Temp;
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_PROGRAMS_RIGHTS_BY_USERID]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_PROGRAMS_RIGHTS_BY_USERID]
@UserId INT
AS
BEGIN
	/*--- DECLARE VARIABLE FOR SAVE ROLE ID ---*/
	DECLARE @roleId INT;
	SELECT @roleId = RoleId FROM Role_Employee_Mapping(NOLOCK)
					WHERE EmpId = @UserId and Status = 1;

	/*--- GENERATE A TEMPORARY TABLE THAT STORE TBL_RIGHTS COPY DATA ---*/
	SELECT Id, P_Title, Descr INTO #Program_Temp FROM Program_Master(NOLOCK)
					WHERE Status = 1;

	/*--- ADD A COLUMN "IsChecked" IN TEMPORARY TABLE 
			FOR CHECK WHICH RIGHTS ASSIGN TO USER OR NOT 
			FOR ASSIGN THEN IsChecked = 1 OTHERWISE = 0 ---*/
	ALTER TABLE #Program_Temp ADD IsChecked TINYINT DEFAULT 0 WITH VALUES;;

	/*--- UPDATE TEMPORARY TABLE COLUMN "IsChecked" FOR STORE A 1/0 
			ACCORDING TO USER ID ---*/
	UPDATE temp
			SET temp.IsChecked = 1 FROM #Program_Temp(NOLOCK) temp
			INNER JOIN Tbl_Rights(NOLOCK) r
			ON  temp.Id = r.Programe_id
			WHERE (r.UserId = @UserId OR r.RoleId = @roleId)
			AND r.Status = 1;

	SELECT*FROM #Program_Temp;
	/*--- DROP A TEMPORARY TABLE ---*/
	DROP TABLE #Program_Temp;
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_USER_BY_EMAIL]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_USER_BY_EMAIL]
@email VARCHAR(100)
AS
BEGIN
	SELECT um.Id, um.Nm, um.Email, um.Mobile, um.Manager_Id, um.Status, rm.Role
		FROM User_Master(NOLOCK) um
		INNER JOIN Role_Employee_Mapping(NOLOCK) rem
		ON um.Id = rem.EmpId
		INNER JOIN Role_Master(NOLOCK) rm
		ON rem.RoleId = rm.Id
		WHERE um.Email = @email;
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_USER_BY_ROLE]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_USER_BY_ROLE]
@Role VARCHAR(50),
@Id INT
AS
BEGIN
	SELECT
		um.Id, um.Nm Name
		FROM User_Master(NOLOCK) um
		INNER JOIN Role_Employee_Mapping(NOLOCK) rem
		ON um.Id = rem.EmpId
		INNER JOIN Role_Master(NOLOCK) rm
		ON rem.RoleId = rm.Id
		WHERE rm.Role = @Role
		AND um.Id<>@Id AND um.Status = 1;
END

GO
/****** Object:  StoredProcedure [dbo].[USP_GET_USERS_WITH_ROLE]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_USERS_WITH_ROLE]
AS
BEGIN
	SELECT
		um.Id, um.Nm Name, ISNULL(rm.Role, 'Role Not Assigned') Role
		FROM User_Master(NOLOCK) um
		LEFT JOIN Role_Employee_Mapping(NOLOCK) rem
		ON um.Id = rem.EmpId
		LEFT JOIN Role_Master(NOLOCK) rm
		ON rem.RoleId = rm.Id
		WHERE um.Status = 1;
END
GO
/****** Object:  StoredProcedure [dbo].[USP_MANAGED_PROGRAM]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_MANAGED_PROGRAM]
@Action VARCHAR(20), --- This action like Create/Update/Get ---
@Id INT = NULL,
@Title VARCHAR(100) = NULL,
@Path VARCHAR(100) = NULL,
@Descr VARCHAR(100) = NULL,
@Updated_Sequence VARCHAR(100) = NULL,
@Status TINYINT = NULL
AS
BEGIN
	IF @Action = 'create' AND EXISTS (SELECT 1 FROM Program_Master WHERE Path  = @Path)
		BEGIN
			THROW 50000, 'Progarm Already Exist', 1;
			RETURN;
		END
	IF @Action = 'update' AND EXISTS (SELECT 1 FROM Program_Master WHERE Path  = @Path AND Id <> @Id)
		BEGIN
			THROW 50000, 'Progarm Already Exist For Another Someone', 1;
			RETURN;
		END
	BEGIN TRY
		BEGIN TRAN trn_progManage
			IF @Action = 'create'
				BEGIN
				DECLARE @Display_Sequence INT;
				SELECT @Display_Sequence = MAX(Display_Sequence)+1 FROM Program_Master;
					INSERT INTO Program_Master(
											P_Title,
											Path,
											Descr,
											Display_Sequence,
											Status
											)VALUES(
											@Title,
											@Path,
											@Descr,
											@Display_Sequence,
											@Status);
					SELECT 1 AS RESULT;
				END
			ELSE IF @Action = 'update'
				BEGIN
				DECLARE @Current_Sequence INT;
				SELECT @Current_Sequence = Display_Sequence FROM Program_Master WHERE Id = @Id;

				IF @Current_Sequence <> @Updated_Sequence
					BEGIN
						UPDATE Program_Master SET Display_Sequence = @Current_Sequence
										WHERE Display_Sequence = @Updated_Sequence;
					END

					UPDATE Program_Master SET
										P_Title = @Title,
										Path = @Path,
										Descr = @Descr,
										Display_Sequence = @Updated_Sequence,
										Status = @Status
										WHERE Id = @Id;
					SELECT 1 AS RESULT;
				END
			ELSE IF @Action = 'get'
				BEGIN
					SELECT
						Id, P_Title Title, Path, Descr, Display_Sequence Sequence, Status
						FROM Program_Master(NOLOCK)
						WHERE Id = @Id;
				END
			ELSE IF @Action = 'getall'
				BEGIN
					SELECT
						Id, P_Title Title, Path, Descr, Display_Sequence Sequence, Status
						FROM Program_Master(NOLOCK) ORDER BY Display_Sequence;
				END
		COMMIT TRAN trn_progManage;
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN trn_progManage;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[USP_MANAGED_ROLE]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_MANAGED_ROLE]
@Action VARCHAR(50),
@Id INT = NULL,
@RoleName VARCHAR(50) =  NULL,
@Status TINYINT = NULL
AS
BEGIN
	IF @Action = 'create' AND EXISTS (SELECT 1 FROM Role_Master WHERE Role = @RoleName)
		BEGIN
			THROW 50000, 'This Role Already Exist', 1;
			RETURN;
		END
	IF @Action = 'update' AND EXISTS (SELECT 1 FROM Role_Master WHERE Role = @RoleName AND Id <> @Id)
		BEGIN
			THROW 50000, 'This Role Already Exist', 1;
			RETURN;
		END
	BEGIN TRY
		BEGIN TRAN trn_role
			IF @Action = 'create'
				BEGIN
					INSERT INTO Role_Master(Role, Status)
									VALUES(@RoleName, @Status);
					SELECT 1 AS RESULT;
				END
			ELSE IF @Action = 'update'
				BEGIN
					UPDATE Role_Master SET
								Role = @RoleName,
								Status = @Status
								WHERE Id = @Id;
			SELECT 1 AS RESULT;
				END
			ELSE IF @Action ='getall'
				BEGIN
					SELECT Id, Role,
					CASE WHEN Status = 1 THEN 'Active'
							ELSE 'Inactive' 
					END Status
					FROM Role_Master(NOLOCK);
				END
			ELSE IF @Action ='get'
				BEGIN
					SELECT Id, Role, Status FROM Role_Master(NOLOCK) WHERE Id = @Id;
				END
		COMMIT TRAN trn_role;
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN trn_role;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[USP_MANAGED_USER]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_MANAGED_USER]
@Action VARCHAR(20), --- This action like Create/Update/Get ---
@Id INT = NULL,
@Name VARCHAR(100) = NULL,
@Email VARCHAR(100) = NULL,
@Mobile VARCHAR(100) = NULL,
@Password VARCHAR(100) = NULL,
@Manager INT = NULL,
@Status TINYINT = NULL
AS
BEGIN
	IF @Action = 'create' AND EXISTS (SELECT 1 FROM User_Master WHERE Email = @Email)
		BEGIN
			THROW 50000, 'User Email Already Exist', 1;
			RETURN;
		END
	IF @Action = 'update' AND EXISTS (SELECT 1 FROM User_Master WHERE Email = @Email AND Id <> @Id)
		BEGIN
			THROW 50000, 'User Email Already Exist For Another Someone', 1;
			RETURN;
		END
	BEGIN TRY
		BEGIN TRAN trn_userManage
			IF @Action = 'create'
				BEGIN
					INSERT INTO User_Master (
											Nm,
											Email,
											Mobile,
											Password,
											Manager_Id,
											Status
											)VALUES(
											@Name,
											@Email,
											@Mobile,
											DBO.HashPassword(@Password),
											@Manager,
											@Status);
					SELECT 1 AS RESULT;
				END
			ELSE IF @Action = 'update'
				BEGIN
					UPDATE User_Master SET
										Nm = @Name,
										Email = @Email,
										Mobile = @Mobile,
										Manager_Id = @Manager,
										Status = @Status
										WHERE Id = @Id;
					SELECT 1 AS RESULT;
				END
			ELSE IF @Action = 'get'
				BEGIN
					SELECT
						Id, Nm Name, Email, Mobile, Manager_Id, Status
						FROM User_Master(NOLOCK)
						WHERE Id = @Id;
				END
			ELSE IF @Action = 'getall'
				BEGIN
					SELECT
						U1.Id,
						U1.Nm Name,
						U1.Email,
						U1.Mobile,
						ISNull(U2.Nm,'Not Assigned') Manager,
						CASE WHEN U1.Status = 1 THEN 'Active'
							ELSE 'Inactive'
						END Status
						FROM User_Master(NOLOCK) U1
						LEFT JOIN User_Master(NOLOCK) U2
						ON U1.Manager_Id = U2.Id
						WHERE U1.Id<>@Id;
				END
		COMMIT TRAN trn_userManage;
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN trn_userManage;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[USP_RAISE_CLAIM_REQUEST]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_RAISE_CLAIM_REQUEST]
@UserId INT,
@Claim_Title VARCHAR(100),
@Claim_Reason VARCHAR(100),
@Amount DECIMAL,
@Evidence VARCHAR(500) = NULL,
@ExpenseDt VARCHAR(100),
@Claim_Description VARCHAR(500)
AS
BEGIN
	DECLARE @Current_Status VARCHAR(100)
	DECLARE @ClaimId INT
	SELECT @Current_Status =NextAction FROM Employee_Claim_Master_Mapping (NOLOCK)
							 WHERE CurrentAction = 'Initiated'
	
		
		/*CHECK CLAIM TABLE USER ALREADY RAISED CLAIM OR NOT*/
		IF EXISTS (SELECT 1 FROM Claim_Master(NOLOCK) WHERE UserId = @UserId AND CurrentStatus LIKE '%Pending%')
			BEGIN
			  THROW 50000, 'Claim already in pending', 1
			--RAISERROR('Claim Already In Pending', 1, 1);
			  RETURN;
			END
		BEGIN TRY
			BEGIN TRAN Trn_Claim;
		/*INSERT INTO CLAIM RECORD TABLE*/
			INSERT INTO Claim_Master (
										UserId ,
										Claim_Title ,
										Claim_Reason,
										Amount,
										ClaimDt ,
										Evidence,
										ExpenseDt,
										Claim_Description,
										CurrentStatus,
										Status
										)
									VALUES
									( @UserId,
									  @Claim_Title,
									  @Claim_Reason,
									  @Amount,
									  GETDATE(),
									  @Evidence,
									  @ExpenseDt,
									  @Claim_Description,
									  @Current_Status,
									  1
									)
		-----------
		/*INSERT INTO CLAIM ACTION TABLE*/
		SET @ClaimId = SCOPE_IDENTITY();

		INSERT INTO Employee_Claim_Action(
											ClaimId,
											Action,
											ActionBy,
											ActionDt,
											Remarks,
											Status
											)
										VALUES
										( @ClaimId,
										  'Initiated',
										  @UserId,
										  GETDATE(),
										  @Claim_Description,
										  1
										)
		COMMIT TRAN Trn_Claim;
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN Trn_Claim;
		END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[USP_SAVE_GROUP_RIGHTS]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_SAVE_GROUP_RIGHTS]
@RoleId INT,
@Rights XML
AS
BEGIN
	SELECT
		n.value('Id[1]','VARCHAR(20)') AS Program_Id,
		n.value('IsChecked[1]','VARCHAR(20)') AS IsChecked
		INTO #Rights
		FROM @Rights.nodes('/ArrayOfAssignProgramRightModel/AssignProgramRightModel') AS p(n)

		/*DELETE FROM #Rights
			WHERE Program_Id 
					NOT IN (SELECT Program_Id FROM Tbl_Rights WHERE RoleId = @RoleId) AND IsChecked = 0;
*/
		UPDATE T
			SET T.Status = R.IsChecked
			FROM Tbl_Rights T
			INNER JOIN #Rights R
			ON T.Programe_id = R.Program_Id
			WHERE T.RoleId = @RoleId AND T.Status<>R.IsChecked;

		INSERT INTO Tbl_Rights(Programe_id, RoleId, Status)
			SELECT Program_Id, @RoleId, IsChecked
				FROM #Rights
				WHERE Program_Id NOT IN  (SELECT Programe_id FROM Tbl_Rights WHERE RoleId = @RoleId)
				AND IsChecked = 1;

		DROP TABLE #Rights
END
GO
/****** Object:  StoredProcedure [dbo].[USP_SAVE_INDIVIDUAL_RIGHTS]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_SAVE_INDIVIDUAL_RIGHTS]
@UserId INT,
@Rights XML
AS
BEGIN
	SELECT
		n.value('Id[1]','VARCHAR(20)') AS Program_Id,
		n.value('IsChecked[1]','VARCHAR(20)') AS IsChecked
		INTO #Rights
		FROM @Rights.nodes('/ArrayOfAssignProgramRightModel/AssignProgramRightModel') AS p(n)

		/*DELETE FROM #Rights
			WHERE Program_Id 
					NOT IN (SELECT Program_Id FROM Tbl_Rights WHERE UserId = @UserId) AND IsChecked = 0;
*/
		UPDATE T
			SET T.Status = R.IsChecked
			FROM Tbl_Rights T
			INNER JOIN #Rights R
			ON T.Programe_id = R.Program_Id
			WHERE T.UserId = @UserId AND T.Status<>R.IsChecked;

		INSERT INTO Tbl_Rights(Programe_id, UserId, Status)
			SELECT Program_Id, @UserId, IsChecked
				FROM #Rights
				WHERE Program_Id NOT IN  (SELECT Programe_id FROM Tbl_Rights WHERE UserId = @UserId)
				AND IsChecked = 1

		DROP TABLE #Rights
END
GO
/****** Object:  StoredProcedure [dbo].[USP_UPDATE_CLAIM]    Script Date: 28-Dec-23 1:13:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_UPDATE_CLAIM]
@claimId INT,
@action TINYINT, --1 FOR APPROVE & 0 FOR REJECT--
@userId INT,
@role VARCHAR(20),
@remark VARCHAR(200)
AS
BEGIN
	DECLARE @current_status VARCHAR(100);
	DECLARE @next_action VARCHAR(100);

	SELECT @current_status = CurrentStatus FROM Claim_Master(NOLOCK)
							 WHERE Id = @claimId;
	
	SELECT @next_action = NextAction FROM Employee_Claim_Master_Mapping (NOLOCK)
							 WHERE CurrentAction = @current_status AND Status = @action;
	BEGIN TRAN Trn_Update_Claim;
		BEGIN TRY
		UPDATE Claim_Master SET CurrentStatus = @next_action
							WHERE Id = @claimId
		INSERT INTO Employee_Claim_Action(
											ClaimId,
											Action,
											ActionBy,
											ActionDt,
											Remarks,
											Status
											)
										VALUES(
											@claimId,
											@next_action,
											@userId,
											GETDATE(),
											@remark,
											1
										);
		IF @role = 'Account'
			BEGIN
				EXEC USP_CLAIM_TRANSACTION_ENTRY @userId, @claimId
			END
		COMMIT TRAN Trn_Update_Claim;
		END TRY
		BEGIN CATCH
		ROLLBACK TRAN Trn_Update_Claim;
		END CATCH
END
GO
USE [master]
GO
ALTER DATABASE [ClaimDB] SET  READ_WRITE 
GO
