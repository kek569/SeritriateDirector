USE [master]
GO
/****** Object:  Database [user512]    Script Date: 05.06.2024 12:45:51 ******/
CREATE DATABASE [user512]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'user512', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\user512.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'user512_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\user512_log.ldf' , SIZE = 1280KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [user512] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [user512].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [user512] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [user512] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [user512] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [user512] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [user512] SET ARITHABORT OFF 
GO
ALTER DATABASE [user512] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [user512] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [user512] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [user512] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [user512] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [user512] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [user512] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [user512] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [user512] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [user512] SET  ENABLE_BROKER 
GO
ALTER DATABASE [user512] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [user512] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [user512] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [user512] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [user512] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [user512] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [user512] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [user512] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [user512] SET  MULTI_USER 
GO
ALTER DATABASE [user512] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [user512] SET DB_CHAINING OFF 
GO
ALTER DATABASE [user512] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [user512] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [user512] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [user512] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [user512] SET QUERY_STORE = OFF
GO
USE [user512]
GO
/****** Object:  Table [dbo].[Director]    Script Date: 05.06.2024 12:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Director](
	[IdDirector] [int] IDENTITY(1,1) NOT NULL,
	[IdStaff] [int] NOT NULL,
	[IdSecretary] [int] NULL,
 CONSTRAINT [PK_Director] PRIMARY KEY CLUSTERED 
(
	[IdDirector] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gender]    Script Date: 05.06.2024 12:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gender](
	[IdGender] [int] IDENTITY(1,1) NOT NULL,
	[NameGender] [nvarchar](80) NOT NULL,
 CONSTRAINT [PK_Gender] PRIMARY KEY CLUSTERED 
(
	[IdGender] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Graphics]    Script Date: 05.06.2024 12:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Graphics](
	[IdGraphics] [int] IDENTITY(1,1) NOT NULL,
	[NameEvents] [nvarchar](80) NOT NULL,
	[PlaceEvents] [nvarchar](150) NOT NULL,
	[DateEvents] [date] NOT NULL,
	[TimeEvents] [time](7) NOT NULL,
	[TargetEvents] [nvarchar](150) NOT NULL,
	[PhotoEvents] [varbinary](max) NULL,
	[IdStaff] [int] NOT NULL,
 CONSTRAINT [PK_Graphics] PRIMARY KEY CLUSTERED 
(
	[IdGraphics] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobTitle]    Script Date: 05.06.2024 12:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobTitle](
	[IdJobTitle] [int] IDENTITY(1,1) NOT NULL,
	[NameJobTitle] [nvarchar](80) NOT NULL,
 CONSTRAINT [PK_JobTitle] PRIMARY KEY CLUSTERED 
(
	[IdJobTitle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Letters]    Script Date: 05.06.2024 12:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Letters](
	[IdLetters] [int] IDENTITY(1,1) NOT NULL,
	[IdTypeLetters] [int] NOT NULL,
	[WhereLetters] [nvarchar](150) NULL,
	[IdStaff] [int] NOT NULL,
	[SubjectLetters] [nvarchar](80) NOT NULL,
	[DateReceivingOrDeparturesLetters] [date] NOT NULL,
	[TextLetters] [nvarchar](350) NOT NULL,
	[FromWhomLetters] [nvarchar](150) NULL,
	[ToWhomLetters] [nvarchar](150) NULL,
	[IdWhoAnswersToWhomLetters] [int] NULL,
 CONSTRAINT [PK_Letters] PRIMARY KEY CLUSTERED 
(
	[IdLetters] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 05.06.2024 12:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[IdOrders] [int] IDENTITY(1,1) NOT NULL,
	[IdTypeOrders] [int] NOT NULL,
	[WhereOrders] [nvarchar](150) NULL,
	[IdStaff] [int] NOT NULL,
	[SubjectOrders] [nvarchar](80) NOT NULL,
	[DateReceivingOrDeparturesOrders] [date] NOT NULL,
	[TextOrders] [nvarchar](350) NOT NULL,
	[FromWhomOrders] [nvarchar](150) NULL,
	[ToWhomOrders] [nvarchar](150) NULL,
	[IdWhoAnswersToWhomOrders] [int] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[IdOrders] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResponseToTheLetters]    Script Date: 05.06.2024 12:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResponseToTheLetters](
	[IdResponseToTheLetters] [int] IDENTITY(1,1) NOT NULL,
	[IdLetters] [int] NOT NULL,
 CONSTRAINT [PK_ResponseToTheLetters] PRIMARY KEY CLUSTERED 
(
	[IdResponseToTheLetters] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResponseToTheOrders]    Script Date: 05.06.2024 12:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResponseToTheOrders](
	[IdResponseToTheOrders] [int] IDENTITY(1,1) NOT NULL,
	[IdOrders] [int] NOT NULL,
 CONSTRAINT [PK_ResponseToTheOrders] PRIMARY KEY CLUSTERED 
(
	[IdResponseToTheOrders] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 05.06.2024 12:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[IdRole] [int] IDENTITY(1,1) NOT NULL,
	[NameRole] [nvarchar](80) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[IdRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Secretary]    Script Date: 05.06.2024 12:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Secretary](
	[IdSecretary] [int] IDENTITY(1,1) NOT NULL,
	[IdStaff] [int] NOT NULL,
 CONSTRAINT [PK_Secretary] PRIMARY KEY CLUSTERED 
(
	[IdSecretary] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 05.06.2024 12:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[IdStaff] [int] IDENTITY(1,1) NOT NULL,
	[FirstNameStaff] [nvarchar](80) NOT NULL,
	[SurNameStaff] [nvarchar](80) NOT NULL,
	[MiddleNameStaff] [nvarchar](80) NULL,
	[NumberPhoneStaff] [nvarchar](30) NOT NULL,
	[DateOfBirthStaff] [date] NOT NULL,
	[SeriesPassport] [int] NOT NULL,
	[IdGender] [int] NOT NULL,
	[IdJobTitle] [int] NOT NULL,
	[IdUser] [int] NOT NULL,
	[AdressStaff] [nvarchar](150) NOT NULL,
	[FullName] [nvarchar](200) NOT NULL,
	[PhotoStaff] [varbinary](max) NULL,
	[NumberPassport] [int] NOT NULL,
	[IdDirector] [int] NULL,
 CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED 
(
	[IdStaff] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Test]    Script Date: 05.06.2024 12:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Test](
	[IdTest] [int] IDENTITY(1,1) NOT NULL,
	[IdStaff] [int] NOT NULL,
	[IdTestTwo] [int] NOT NULL,
 CONSTRAINT [PK_Test] PRIMARY KEY CLUSTERED 
(
	[IdTest] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestTwo]    Script Date: 05.06.2024 12:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestTwo](
	[IdTestTwo] [int] IDENTITY(1,1) NOT NULL,
	[IdStaff] [int] NOT NULL,
 CONSTRAINT [PK_TestTwo] PRIMARY KEY CLUSTERED 
(
	[IdTestTwo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeLetters]    Script Date: 05.06.2024 12:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeLetters](
	[IdTypeLetters] [int] IDENTITY(1,1) NOT NULL,
	[NameTypeLetters] [nvarchar](80) NOT NULL,
 CONSTRAINT [PK_TypeLetters] PRIMARY KEY CLUSTERED 
(
	[IdTypeLetters] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeOrders]    Script Date: 05.06.2024 12:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeOrders](
	[IdTypeOrders] [int] IDENTITY(1,1) NOT NULL,
	[NameTypeOrders] [nvarchar](80) NOT NULL,
 CONSTRAINT [PK_TypeOrders] PRIMARY KEY CLUSTERED 
(
	[IdTypeOrders] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 05.06.2024 12:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[IdUser] [int] IDENTITY(1,1) NOT NULL,
	[LoginUser] [nvarchar](80) NOT NULL,
	[PasswordUser] [nvarchar](80) NOT NULL,
	[IdRole] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WhoAnswersToWhomLetters]    Script Date: 05.06.2024 12:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WhoAnswersToWhomLetters](
	[IdWhoAnswersToWhomLetters] [int] IDENTITY(1,1) NOT NULL,
	[IdLetters] [int] NOT NULL,
	[IdResponseToTheLetters] [int] NOT NULL,
 CONSTRAINT [PK_WhoAnswersToWhomLetters] PRIMARY KEY CLUSTERED 
(
	[IdWhoAnswersToWhomLetters] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WhoAnswersToWhomOrders]    Script Date: 05.06.2024 12:45:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WhoAnswersToWhomOrders](
	[IdWhoAnswersToWhomOrders] [int] IDENTITY(1,1) NOT NULL,
	[IdOrders] [int] NOT NULL,
	[IdResponseToTheOrders] [int] NOT NULL,
 CONSTRAINT [PK_WhoAnswersToWhomOrders] PRIMARY KEY CLUSTERED 
(
	[IdWhoAnswersToWhomOrders] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Director] ON 

INSERT [dbo].[Director] ([IdDirector], [IdStaff], [IdSecretary]) VALUES (1, 8, 1)
SET IDENTITY_INSERT [dbo].[Director] OFF
GO
SET IDENTITY_INSERT [dbo].[Gender] ON 

INSERT [dbo].[Gender] ([IdGender], [NameGender]) VALUES (1, N'Муж')
INSERT [dbo].[Gender] ([IdGender], [NameGender]) VALUES (2, N'Жен')
SET IDENTITY_INSERT [dbo].[Gender] OFF
GO
SET IDENTITY_INSERT [dbo].[Graphics] ON 

INSERT [dbo].[Graphics] ([IdGraphics], [NameEvents], [PlaceEvents], [DateEvents], [TimeEvents], [TargetEvents], [PhotoEvents], [IdStaff]) VALUES (1, N'День генерального директора', N'644116, г. Омск, ул. 24-я Северная, 196/1', CAST(N'2023-09-30' AS Date), CAST(N'09:00:00' AS Time), N'к чему готовить бизнес в 2023-2024 годах. Чем обернется трансформация экономики', NULL, 7)
SET IDENTITY_INSERT [dbo].[Graphics] OFF
GO
SET IDENTITY_INSERT [dbo].[JobTitle] ON 

INSERT [dbo].[JobTitle] ([IdJobTitle], [NameJobTitle]) VALUES (1, N'Администратор системы')
INSERT [dbo].[JobTitle] ([IdJobTitle], [NameJobTitle]) VALUES (2, N'Секретар')
INSERT [dbo].[JobTitle] ([IdJobTitle], [NameJobTitle]) VALUES (3, N'Директор')
SET IDENTITY_INSERT [dbo].[JobTitle] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([IdRole], [NameRole]) VALUES (1, N'Администратор системы')
INSERT [dbo].[Role] ([IdRole], [NameRole]) VALUES (2, N'Секретарь')
INSERT [dbo].[Role] ([IdRole], [NameRole]) VALUES (3, N'Директор')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Secretary] ON 

INSERT [dbo].[Secretary] ([IdSecretary], [IdStaff]) VALUES (1, 7)
SET IDENTITY_INSERT [dbo].[Secretary] OFF
GO
SET IDENTITY_INSERT [dbo].[Staff] ON 

INSERT [dbo].[Staff] ([IdStaff], [FirstNameStaff], [SurNameStaff], [MiddleNameStaff], [NumberPhoneStaff], [DateOfBirthStaff], [SeriesPassport], [IdGender], [IdJobTitle], [IdUser], [AdressStaff], [FullName], [PhotoStaff], [NumberPassport], [IdDirector]) VALUES (6, N'Борисов', N'Павел', N'Климентович', N'+7 (954) 163 98-18', CAST(N'1975-04-11' AS Date), 4821, 1, 1, 1, N'Россия, г. Волгоград, Коммунистическая ул,. д. 17 кв.54', N'Борисов Павел Климентович', NULL, 644903, NULL)
INSERT [dbo].[Staff] ([IdStaff], [FirstNameStaff], [SurNameStaff], [MiddleNameStaff], [NumberPhoneStaff], [DateOfBirthStaff], [SeriesPassport], [IdGender], [IdJobTitle], [IdUser], [AdressStaff], [FullName], [PhotoStaff], [NumberPassport], [IdDirector]) VALUES (7, N'Сиянская', N'Вера', N'Ефимовна', N'+7 (921) 927 88-19', CAST(N'1961-05-13' AS Date), 4466, 2, 2, 2, N'Россия, г. Йошкар-Ола, Дзержинского ул., д. 23 кв.117', N'Сиянская Вера Ефимовна', NULL, 191196, NULL)
INSERT [dbo].[Staff] ([IdStaff], [FirstNameStaff], [SurNameStaff], [MiddleNameStaff], [NumberPhoneStaff], [DateOfBirthStaff], [SeriesPassport], [IdGender], [IdJobTitle], [IdUser], [AdressStaff], [FullName], [PhotoStaff], [NumberPassport], [IdDirector]) VALUES (8, N'Каирова', N'Лидия', N'Ивановна', N'+7 (918) 577 12-67', CAST(N'1987-04-28' AS Date), 4978, 2, 3, 3, N'Россия, г. Северодвинск, Зеленый пер., д. 9 кв.69', N'Каирова Лидия Ивановна', NULL, 851680, 1)
SET IDENTITY_INSERT [dbo].[Staff] OFF
GO
SET IDENTITY_INSERT [dbo].[Test] ON 

INSERT [dbo].[Test] ([IdTest], [IdStaff], [IdTestTwo]) VALUES (3, 6, 1)
SET IDENTITY_INSERT [dbo].[Test] OFF
GO
SET IDENTITY_INSERT [dbo].[TestTwo] ON 

INSERT [dbo].[TestTwo] ([IdTestTwo], [IdStaff]) VALUES (1, 6)
SET IDENTITY_INSERT [dbo].[TestTwo] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([IdUser], [LoginUser], [PasswordUser], [IdRole]) VALUES (1, N'admin', N'123', 1)
INSERT [dbo].[User] ([IdUser], [LoginUser], [PasswordUser], [IdRole]) VALUES (2, N'secr', N'123', 2)
INSERT [dbo].[User] ([IdUser], [LoginUser], [PasswordUser], [IdRole]) VALUES (3, N'dir', N'123', 3)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Director]  WITH CHECK ADD  CONSTRAINT [FK_Director_Secretary2] FOREIGN KEY([IdSecretary])
REFERENCES [dbo].[Secretary] ([IdSecretary])
GO
ALTER TABLE [dbo].[Director] CHECK CONSTRAINT [FK_Director_Secretary2]
GO
ALTER TABLE [dbo].[Director]  WITH CHECK ADD  CONSTRAINT [FK_Director_Staff1] FOREIGN KEY([IdStaff])
REFERENCES [dbo].[Staff] ([IdStaff])
GO
ALTER TABLE [dbo].[Director] CHECK CONSTRAINT [FK_Director_Staff1]
GO
ALTER TABLE [dbo].[Graphics]  WITH CHECK ADD  CONSTRAINT [FK_Graphics_Staff] FOREIGN KEY([IdStaff])
REFERENCES [dbo].[Staff] ([IdStaff])
GO
ALTER TABLE [dbo].[Graphics] CHECK CONSTRAINT [FK_Graphics_Staff]
GO
ALTER TABLE [dbo].[Letters]  WITH CHECK ADD  CONSTRAINT [FK_Letters_Staff] FOREIGN KEY([IdStaff])
REFERENCES [dbo].[Staff] ([IdStaff])
GO
ALTER TABLE [dbo].[Letters] CHECK CONSTRAINT [FK_Letters_Staff]
GO
ALTER TABLE [dbo].[Letters]  WITH CHECK ADD  CONSTRAINT [FK_Letters_TypeLetters] FOREIGN KEY([IdTypeLetters])
REFERENCES [dbo].[TypeLetters] ([IdTypeLetters])
GO
ALTER TABLE [dbo].[Letters] CHECK CONSTRAINT [FK_Letters_TypeLetters]
GO
ALTER TABLE [dbo].[Letters]  WITH CHECK ADD  CONSTRAINT [FK_Letters_WhoAnswersToWhomLetters] FOREIGN KEY([IdWhoAnswersToWhomLetters])
REFERENCES [dbo].[WhoAnswersToWhomLetters] ([IdWhoAnswersToWhomLetters])
GO
ALTER TABLE [dbo].[Letters] CHECK CONSTRAINT [FK_Letters_WhoAnswersToWhomLetters]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Staff] FOREIGN KEY([IdStaff])
REFERENCES [dbo].[Staff] ([IdStaff])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Staff]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_TypeOrders] FOREIGN KEY([IdOrders])
REFERENCES [dbo].[TypeOrders] ([IdTypeOrders])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_TypeOrders]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_WhoAnswersToWhomOrders1] FOREIGN KEY([IdWhoAnswersToWhomOrders])
REFERENCES [dbo].[WhoAnswersToWhomOrders] ([IdWhoAnswersToWhomOrders])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_WhoAnswersToWhomOrders1]
GO
ALTER TABLE [dbo].[ResponseToTheLetters]  WITH CHECK ADD  CONSTRAINT [FK_ResponseToTheLetters_Letters] FOREIGN KEY([IdLetters])
REFERENCES [dbo].[Letters] ([IdLetters])
GO
ALTER TABLE [dbo].[ResponseToTheLetters] CHECK CONSTRAINT [FK_ResponseToTheLetters_Letters]
GO
ALTER TABLE [dbo].[ResponseToTheOrders]  WITH CHECK ADD  CONSTRAINT [FK_ResponseToTheOrders_Orders] FOREIGN KEY([IdOrders])
REFERENCES [dbo].[Orders] ([IdOrders])
GO
ALTER TABLE [dbo].[ResponseToTheOrders] CHECK CONSTRAINT [FK_ResponseToTheOrders_Orders]
GO
ALTER TABLE [dbo].[Secretary]  WITH CHECK ADD  CONSTRAINT [FK_Secretary_Staff1] FOREIGN KEY([IdStaff])
REFERENCES [dbo].[Staff] ([IdStaff])
GO
ALTER TABLE [dbo].[Secretary] CHECK CONSTRAINT [FK_Secretary_Staff1]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_Director1] FOREIGN KEY([IdDirector])
REFERENCES [dbo].[Director] ([IdDirector])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_Director1]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_Gender] FOREIGN KEY([IdGender])
REFERENCES [dbo].[Gender] ([IdGender])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_Gender]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_JobTitle] FOREIGN KEY([IdJobTitle])
REFERENCES [dbo].[JobTitle] ([IdJobTitle])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_JobTitle]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([IdUser])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_User]
GO
ALTER TABLE [dbo].[Test]  WITH CHECK ADD  CONSTRAINT [FK_Test_Staff] FOREIGN KEY([IdStaff])
REFERENCES [dbo].[Staff] ([IdStaff])
GO
ALTER TABLE [dbo].[Test] CHECK CONSTRAINT [FK_Test_Staff]
GO
ALTER TABLE [dbo].[Test]  WITH CHECK ADD  CONSTRAINT [FK_Test_TestTwo] FOREIGN KEY([IdTestTwo])
REFERENCES [dbo].[TestTwo] ([IdTestTwo])
GO
ALTER TABLE [dbo].[Test] CHECK CONSTRAINT [FK_Test_TestTwo]
GO
ALTER TABLE [dbo].[TestTwo]  WITH CHECK ADD  CONSTRAINT [FK_TestTwo_Staff] FOREIGN KEY([IdStaff])
REFERENCES [dbo].[Staff] ([IdStaff])
GO
ALTER TABLE [dbo].[TestTwo] CHECK CONSTRAINT [FK_TestTwo_Staff]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([IdRole])
REFERENCES [dbo].[Role] ([IdRole])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
ALTER TABLE [dbo].[WhoAnswersToWhomLetters]  WITH CHECK ADD  CONSTRAINT [FK_WhoAnswersToWhomLetters_Letters] FOREIGN KEY([IdLetters])
REFERENCES [dbo].[Letters] ([IdLetters])
GO
ALTER TABLE [dbo].[WhoAnswersToWhomLetters] CHECK CONSTRAINT [FK_WhoAnswersToWhomLetters_Letters]
GO
ALTER TABLE [dbo].[WhoAnswersToWhomLetters]  WITH CHECK ADD  CONSTRAINT [FK_WhoAnswersToWhomLetters_ResponseToTheLetters1] FOREIGN KEY([IdResponseToTheLetters])
REFERENCES [dbo].[ResponseToTheLetters] ([IdResponseToTheLetters])
GO
ALTER TABLE [dbo].[WhoAnswersToWhomLetters] CHECK CONSTRAINT [FK_WhoAnswersToWhomLetters_ResponseToTheLetters1]
GO
ALTER TABLE [dbo].[WhoAnswersToWhomOrders]  WITH CHECK ADD  CONSTRAINT [FK_WhoAnswersToWhomOrders_Orders1] FOREIGN KEY([IdOrders])
REFERENCES [dbo].[Orders] ([IdOrders])
GO
ALTER TABLE [dbo].[WhoAnswersToWhomOrders] CHECK CONSTRAINT [FK_WhoAnswersToWhomOrders_Orders1]
GO
ALTER TABLE [dbo].[WhoAnswersToWhomOrders]  WITH CHECK ADD  CONSTRAINT [FK_WhoAnswersToWhomOrders_ResponseToTheOrders] FOREIGN KEY([IdResponseToTheOrders])
REFERENCES [dbo].[ResponseToTheOrders] ([IdResponseToTheOrders])
GO
ALTER TABLE [dbo].[WhoAnswersToWhomOrders] CHECK CONSTRAINT [FK_WhoAnswersToWhomOrders_ResponseToTheOrders]
GO
USE [master]
GO
ALTER DATABASE [user512] SET  READ_WRITE 
GO
