USE [master]
GO
/****** Object:  Database [TravelAgency]    Script Date: 20/01/2022 15:55:00 ******/
CREATE DATABASE [TravelAgency]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TravelAgency', FILENAME = N'C:\Users\zubey\TravelAgency.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TravelAgency_log', FILENAME = N'C:\Users\zubey\TravelAgency_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [TravelAgency] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TravelAgency].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TravelAgency] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TravelAgency] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TravelAgency] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TravelAgency] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TravelAgency] SET ARITHABORT OFF 
GO
ALTER DATABASE [TravelAgency] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TravelAgency] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TravelAgency] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TravelAgency] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TravelAgency] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TravelAgency] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TravelAgency] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TravelAgency] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TravelAgency] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TravelAgency] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TravelAgency] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TravelAgency] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TravelAgency] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TravelAgency] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TravelAgency] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TravelAgency] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TravelAgency] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TravelAgency] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TravelAgency] SET  MULTI_USER 
GO
ALTER DATABASE [TravelAgency] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TravelAgency] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TravelAgency] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TravelAgency] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TravelAgency] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TravelAgency] SET QUERY_STORE = OFF
GO
USE [TravelAgency]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [TravelAgency]
GO
/****** Object:  User [cs353project]    Script Date: 20/01/2022 15:55:00 ******/
CREATE USER [cs353project] FOR LOGIN [cs353project] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [cs353project]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [cs353project]
GO
/****** Object:  Table [dbo].[Activity]    Script Date: 20/01/2022 15:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activity](
	[activity_id] [int] NOT NULL,
	[activity_name] [varchar](64) NULL,
	[a_description] [varchar](max) NULL,
	[activity_start_time] [datetime] NULL,
	[activity_end_time] [datetime] NULL,
	[ticket_price] [decimal](8, 2) NULL,
	[tour_id] [int] NOT NULL,
	[discount_id] [int] NULL,
	[discount_start_date] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[activity_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Agent]    Script Date: 20/01/2022 15:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Agent](
	[u_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[u_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[assign_guide]    Script Date: 20/01/2022 15:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[assign_guide](
	[tour_id] [int] NOT NULL,
	[guide_u_id] [int] NULL,
	[agent_u_id] [int] NULL,
	[assign_status] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[tour_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Concert]    Script Date: 20/01/2022 15:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Concert](
	[activity_id] [int] NOT NULL,
	[artist_name] [varchar](max) NULL,
	[genre] [varchar](64) NULL,
PRIMARY KEY CLUSTERED 
(
	[activity_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 20/01/2022 15:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[u_id] [int] NOT NULL,
	[c_address] [varchar](max) NULL,
	[wallet] [decimal](8, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[u_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Discount]    Script Date: 20/01/2022 15:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discount](
	[discount_id] [int] NOT NULL,
	[percents] [int] NULL,
	[discount_days] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[discount_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 20/01/2022 15:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[u_id] [int] NOT NULL,
	[salary] [decimal](8, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[u_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 20/01/2022 15:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[feedback_id] [int] NOT NULL,
	[content] [varchar](max) NULL,
	[feedback_time] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[feedback_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Festival]    Script Date: 20/01/2022 15:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Festival](
	[activity_id] [int] NOT NULL,
	[food_catalog] [varchar](max) NULL,
	[age_limit] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[activity_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Guide]    Script Date: 20/01/2022 15:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Guide](
	[u_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[u_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GuideReview]    Script Date: 20/01/2022 15:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GuideReview](
	[review_id] [int] NOT NULL,
	[u_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[review_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[guides_feedback]    Script Date: 20/01/2022 15:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[guides_feedback](
	[feedback_id] [int] NOT NULL,
	[u_id] [int] NULL,
	[tour_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[feedback_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hotel]    Script Date: 20/01/2022 15:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hotel](
	[hotel_id] [int] NOT NULL,
	[hotel_name] [varchar](64) NULL,
	[city] [varchar](32) NULL,
	[num_of_stars] [int] NULL,
	[discount_id] [int] NULL,
	[discount_start_date] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[hotel_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HotelReservation]    Script Date: 20/01/2022 15:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HotelReservation](
	[reserve_id] [int] NOT NULL,
	[room_id] [int] NULL,
	[hotel_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[reserve_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Languages]    Script Date: 20/01/2022 15:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Languages](
	[language_name] [varchar](16) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[language_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[marked_activity]    Script Date: 20/01/2022 15:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[marked_activity](
	[reserve_id] [int] NOT NULL,
	[activity_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[reserve_id] ASC,
	[activity_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservation]    Script Date: 20/01/2022 15:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservation](
	[reserve_id] [int] NOT NULL,
	[reserve_start_date] [date] NULL,
	[num_reserving] [int] NULL,
	[is_booked] [bit] NULL,
	[u_id] [int] NULL,
	[reserve_end_date] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[reserve_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Review]    Script Date: 20/01/2022 15:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Review](
	[review_id] [int] NOT NULL,
	[comment] [varchar](max) NULL,
	[entry_date] [datetime] NULL,
	[rating] [int] NULL,
	[u_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[review_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 20/01/2022 15:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[room_id] [int] NOT NULL,
	[hotel_id] [int] NOT NULL,
	[number] [int] NULL,
	[size] [int] NULL,
	[price] [decimal](8, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[room_id] ASC,
	[hotel_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SightseeingPlace]    Script Date: 20/01/2022 15:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SightseeingPlace](
	[place_id] [int] NOT NULL,
	[place_name] [varchar](64) NULL,
	[start_time] [datetime] NULL,
	[end_time] [datetime] NULL,
	[s_location] [varchar](max) NULL,
	[s_description] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[place_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[speak]    Script Date: 20/01/2022 15:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[speak](
	[u_id] [int] NOT NULL,
	[language_name] [varchar](16) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[u_id] ASC,
	[language_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tour]    Script Date: 20/01/2022 15:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tour](
	[tour_id] [int] NOT NULL,
	[city] [varchar](32) NULL,
	[tour_name] [varchar](64) NULL,
	[tour_start_date] [date] NULL,
	[tour_description] [varchar](max) NULL,
	[price] [decimal](8, 2) NULL,
	[discount_id] [int] NULL,
	[tour_end_date] [date] NULL,
	[discount_start_date] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[tour_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TourReservation]    Script Date: 20/01/2022 15:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TourReservation](
	[reserve_id] [int] NOT NULL,
	[tour_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[reserve_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TourReview]    Script Date: 20/01/2022 15:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TourReview](
	[review_id] [int] NOT NULL,
	[tour_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[review_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 20/01/2022 15:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[u_id] [int] NOT NULL,
	[first_name] [varchar](32) NULL,
	[last_name] [varchar](32) NULL,
	[email] [varchar](255) NULL,
	[phone_number] [varchar](15) NULL,
	[username] [varchar](32) NULL,
	[pass] [varchar](64) NULL,
	[birth_date] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[u_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[visits]    Script Date: 20/01/2022 15:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[visits](
	[place_id] [int] NOT NULL,
	[tour_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[place_id] ASC,
	[tour_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [tourName]    Script Date: 20/01/2022 15:55:00 ******/
CREATE NONCLUSTERED INDEX [tourName] ON [dbo].[Tour]
(
	[tour_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [userLastName]    Script Date: 20/01/2022 15:55:00 ******/
CREATE NONCLUSTERED INDEX [userLastName] ON [dbo].[Users]
(
	[last_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Activity]  WITH CHECK ADD FOREIGN KEY([discount_id])
REFERENCES [dbo].[Discount] ([discount_id])
GO
ALTER TABLE [dbo].[Activity]  WITH CHECK ADD FOREIGN KEY([tour_id])
REFERENCES [dbo].[Tour] ([tour_id])
GO
ALTER TABLE [dbo].[Agent]  WITH CHECK ADD FOREIGN KEY([u_id])
REFERENCES [dbo].[Employee] ([u_id])
GO
ALTER TABLE [dbo].[assign_guide]  WITH CHECK ADD FOREIGN KEY([agent_u_id])
REFERENCES [dbo].[Agent] ([u_id])
GO
ALTER TABLE [dbo].[assign_guide]  WITH CHECK ADD FOREIGN KEY([guide_u_id])
REFERENCES [dbo].[Guide] ([u_id])
GO
ALTER TABLE [dbo].[assign_guide]  WITH CHECK ADD FOREIGN KEY([tour_id])
REFERENCES [dbo].[Tour] ([tour_id])
GO
ALTER TABLE [dbo].[Concert]  WITH CHECK ADD FOREIGN KEY([activity_id])
REFERENCES [dbo].[Activity] ([activity_id])
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD FOREIGN KEY([u_id])
REFERENCES [dbo].[Users] ([u_id])
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD FOREIGN KEY([u_id])
REFERENCES [dbo].[Users] ([u_id])
GO
ALTER TABLE [dbo].[Festival]  WITH CHECK ADD FOREIGN KEY([activity_id])
REFERENCES [dbo].[Activity] ([activity_id])
GO
ALTER TABLE [dbo].[Guide]  WITH CHECK ADD FOREIGN KEY([u_id])
REFERENCES [dbo].[Employee] ([u_id])
GO
ALTER TABLE [dbo].[GuideReview]  WITH CHECK ADD FOREIGN KEY([review_id])
REFERENCES [dbo].[Review] ([review_id])
GO
ALTER TABLE [dbo].[GuideReview]  WITH CHECK ADD FOREIGN KEY([u_id])
REFERENCES [dbo].[Guide] ([u_id])
GO
ALTER TABLE [dbo].[guides_feedback]  WITH CHECK ADD FOREIGN KEY([feedback_id])
REFERENCES [dbo].[Feedback] ([feedback_id])
GO
ALTER TABLE [dbo].[guides_feedback]  WITH CHECK ADD FOREIGN KEY([tour_id])
REFERENCES [dbo].[Tour] ([tour_id])
GO
ALTER TABLE [dbo].[guides_feedback]  WITH CHECK ADD FOREIGN KEY([u_id])
REFERENCES [dbo].[Guide] ([u_id])
GO
ALTER TABLE [dbo].[Hotel]  WITH CHECK ADD FOREIGN KEY([discount_id])
REFERENCES [dbo].[Discount] ([discount_id])
GO
ALTER TABLE [dbo].[HotelReservation]  WITH CHECK ADD FOREIGN KEY([reserve_id])
REFERENCES [dbo].[Reservation] ([reserve_id])
GO
ALTER TABLE [dbo].[HotelReservation]  WITH CHECK ADD FOREIGN KEY([room_id], [hotel_id])
REFERENCES [dbo].[Room] ([room_id], [hotel_id])
GO
ALTER TABLE [dbo].[marked_activity]  WITH CHECK ADD FOREIGN KEY([activity_id])
REFERENCES [dbo].[Activity] ([activity_id])
GO
ALTER TABLE [dbo].[marked_activity]  WITH CHECK ADD FOREIGN KEY([reserve_id])
REFERENCES [dbo].[TourReservation] ([reserve_id])
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD FOREIGN KEY([u_id])
REFERENCES [dbo].[Customer] ([u_id])
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD FOREIGN KEY([u_id])
REFERENCES [dbo].[Customer] ([u_id])
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD FOREIGN KEY([hotel_id])
REFERENCES [dbo].[Hotel] ([hotel_id])
GO
ALTER TABLE [dbo].[speak]  WITH CHECK ADD FOREIGN KEY([language_name])
REFERENCES [dbo].[Languages] ([language_name])
GO
ALTER TABLE [dbo].[speak]  WITH CHECK ADD FOREIGN KEY([u_id])
REFERENCES [dbo].[Guide] ([u_id])
GO
ALTER TABLE [dbo].[Tour]  WITH CHECK ADD FOREIGN KEY([discount_id])
REFERENCES [dbo].[Discount] ([discount_id])
GO
ALTER TABLE [dbo].[TourReservation]  WITH CHECK ADD FOREIGN KEY([reserve_id])
REFERENCES [dbo].[Reservation] ([reserve_id])
GO
ALTER TABLE [dbo].[TourReservation]  WITH CHECK ADD FOREIGN KEY([tour_id])
REFERENCES [dbo].[Tour] ([tour_id])
GO
ALTER TABLE [dbo].[TourReview]  WITH CHECK ADD FOREIGN KEY([review_id])
REFERENCES [dbo].[Review] ([review_id])
GO
ALTER TABLE [dbo].[TourReview]  WITH CHECK ADD FOREIGN KEY([tour_id])
REFERENCES [dbo].[Tour] ([tour_id])
GO
ALTER TABLE [dbo].[visits]  WITH CHECK ADD FOREIGN KEY([place_id])
REFERENCES [dbo].[SightseeingPlace] ([place_id])
GO
ALTER TABLE [dbo].[visits]  WITH CHECK ADD FOREIGN KEY([tour_id])
REFERENCES [dbo].[Tour] ([tour_id])
GO
ALTER TABLE [dbo].[Tour]  WITH CHECK ADD  CONSTRAINT [discountCheck] CHECK  (([discount_id] IS NULL AND [discount_start_date] IS NULL OR [discount_id] IS NOT NULL AND [discount_start_date] IS NOT NULL))
GO
ALTER TABLE [dbo].[Tour] CHECK CONSTRAINT [discountCheck]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'discountId IFF discount_start_date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tour', @level2type=N'CONSTRAINT',@level2name=N'discountCheck'
GO
USE [master]
GO
ALTER DATABASE [TravelAgency] SET  READ_WRITE 
GO
