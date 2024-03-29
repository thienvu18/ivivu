USE [master]
GO
/****** Object:  Database [QLKhachSan]    Script Date: 11/06/2018 23:39:14 ******/
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'QLKhachSan')
CREATE DATABASE [QLKhachSan]

GO
ALTER DATABASE [QLKhachSan] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLKhachSan].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLKhachSan] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [QLKhachSan] SET ANSI_NULLS OFF
GO
ALTER DATABASE [QLKhachSan] SET ANSI_PADDING OFF
GO
ALTER DATABASE [QLKhachSan] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [QLKhachSan] SET ARITHABORT OFF
GO
ALTER DATABASE [QLKhachSan] SET AUTO_CLOSE ON
GO
ALTER DATABASE [QLKhachSan] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [QLKhachSan] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [QLKhachSan] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [QLKhachSan] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [QLKhachSan] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [QLKhachSan] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [QLKhachSan] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [QLKhachSan] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [QLKhachSan] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [QLKhachSan] SET  DISABLE_BROKER
GO
ALTER DATABASE [QLKhachSan] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [QLKhachSan] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [QLKhachSan] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [QLKhachSan] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [QLKhachSan] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [QLKhachSan] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [QLKhachSan] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [QLKhachSan] SET  READ_WRITE
GO
ALTER DATABASE [QLKhachSan] SET RECOVERY SIMPLE
GO
ALTER DATABASE [QLKhachSan] SET  MULTI_USER
GO
ALTER DATABASE [QLKhachSan] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [QLKhachSan] SET DB_CHAINING OFF
GO
USE [QLKhachSan]
GO
/****** Object:  ForeignKey [FK_NhanVien_KhachSan]    Script Date: 11/06/2018 23:39:15 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NhanVien_KhachSan]') AND parent_object_id = OBJECT_ID(N'[dbo].[NhanVien]'))
ALTER TABLE [dbo].[NhanVien] DROP CONSTRAINT [FK_NhanVien_KhachSan]
GO
/****** Object:  ForeignKey [FK_LoaiPhong_KhachSan]    Script Date: 11/06/2018 23:39:15 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LoaiPhong_KhachSan]') AND parent_object_id = OBJECT_ID(N'[dbo].[LoaiPhong]'))
ALTER TABLE [dbo].[LoaiPhong] DROP CONSTRAINT [FK_LoaiPhong_KhachSan]
GO
/****** Object:  ForeignKey [FK_Phong_LoaiPhong]    Script Date: 11/06/2018 23:39:15 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Phong_LoaiPhong]') AND parent_object_id = OBJECT_ID(N'[dbo].[Phong]'))
ALTER TABLE [dbo].[Phong] DROP CONSTRAINT [FK_Phong_LoaiPhong]
GO
/****** Object:  ForeignKey [FK_DatPhong_KhachHang]    Script Date: 11/06/2018 23:39:15 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DatPhong_KhachHang]') AND parent_object_id = OBJECT_ID(N'[dbo].[DatPhong]'))
ALTER TABLE [dbo].[DatPhong] DROP CONSTRAINT [FK_DatPhong_KhachHang]
GO
/****** Object:  ForeignKey [FK_DatPhong_LoaiPhong]    Script Date: 11/06/2018 23:39:15 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DatPhong_LoaiPhong]') AND parent_object_id = OBJECT_ID(N'[dbo].[DatPhong]'))
ALTER TABLE [dbo].[DatPhong] DROP CONSTRAINT [FK_DatPhong_LoaiPhong]
GO
/****** Object:  ForeignKey [FK_TrangThaiPhong_Phong]    Script Date: 11/06/2018 23:39:16 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TrangThaiPhong_Phong]') AND parent_object_id = OBJECT_ID(N'[dbo].[TrangThaiPhong]'))
ALTER TABLE [dbo].[TrangThaiPhong] DROP CONSTRAINT [FK_TrangThaiPhong_Phong]
GO
/****** Object:  ForeignKey [FK_HoaDon_DatPhong]    Script Date: 11/06/2018 23:39:16 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_HoaDon_DatPhong]') AND parent_object_id = OBJECT_ID(N'[dbo].[HoaDon]'))
ALTER TABLE [dbo].[HoaDon] DROP CONSTRAINT [FK_HoaDon_DatPhong]
GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 11/06/2018 23:39:16 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_HoaDon_DatPhong]') AND parent_object_id = OBJECT_ID(N'[dbo].[HoaDon]'))
ALTER TABLE [dbo].[HoaDon] DROP CONSTRAINT [FK_HoaDon_DatPhong]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HoaDon]') AND type in (N'U'))
DROP TABLE [dbo].[HoaDon]
GO
/****** Object:  Table [dbo].[TrangThaiPhong]    Script Date: 11/06/2018 23:39:16 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TrangThaiPhong_Phong]') AND parent_object_id = OBJECT_ID(N'[dbo].[TrangThaiPhong]'))
ALTER TABLE [dbo].[TrangThaiPhong] DROP CONSTRAINT [FK_TrangThaiPhong_Phong]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TrangThaiPhong]') AND type in (N'U'))
DROP TABLE [dbo].[TrangThaiPhong]
GO
/****** Object:  Table [dbo].[DatPhong]    Script Date: 11/06/2018 23:39:15 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DatPhong_KhachHang]') AND parent_object_id = OBJECT_ID(N'[dbo].[DatPhong]'))
ALTER TABLE [dbo].[DatPhong] DROP CONSTRAINT [FK_DatPhong_KhachHang]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DatPhong_LoaiPhong]') AND parent_object_id = OBJECT_ID(N'[dbo].[DatPhong]'))
ALTER TABLE [dbo].[DatPhong] DROP CONSTRAINT [FK_DatPhong_LoaiPhong]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DatPhong]') AND type in (N'U'))
DROP TABLE [dbo].[DatPhong]
GO
/****** Object:  Table [dbo].[Phong]    Script Date: 11/06/2018 23:39:15 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Phong_LoaiPhong]') AND parent_object_id = OBJECT_ID(N'[dbo].[Phong]'))
ALTER TABLE [dbo].[Phong] DROP CONSTRAINT [FK_Phong_LoaiPhong]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Phong]') AND type in (N'U'))
DROP TABLE [dbo].[Phong]
GO
/****** Object:  Table [dbo].[LoaiPhong]    Script Date: 11/06/2018 23:39:15 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LoaiPhong_KhachSan]') AND parent_object_id = OBJECT_ID(N'[dbo].[LoaiPhong]'))
ALTER TABLE [dbo].[LoaiPhong] DROP CONSTRAINT [FK_LoaiPhong_KhachSan]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoaiPhong]') AND type in (N'U'))
DROP TABLE [dbo].[LoaiPhong]
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 11/06/2018 23:39:15 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NhanVien_KhachSan]') AND parent_object_id = OBJECT_ID(N'[dbo].[NhanVien]'))
ALTER TABLE [dbo].[NhanVien] DROP CONSTRAINT [FK_NhanVien_KhachSan]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NhanVien]') AND type in (N'U'))
DROP TABLE [dbo].[NhanVien]
GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 11/06/2018 23:39:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KhachHang]') AND type in (N'U'))
DROP TABLE [dbo].[KhachHang]
GO
/****** Object:  Table [dbo].[KhachSan]    Script Date: 11/06/2018 23:39:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KhachSan]') AND type in (N'U'))
DROP TABLE [dbo].[KhachSan]
GO
/****** Object:  Table [dbo].[KhachSan]    Script Date: 11/06/2018 23:39:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KhachSan]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[KhachSan](
	[maKS] [int] IDENTITY(1,1) NOT NULL,
	[tenKS] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[soSao] [int] NULL,
	[soNha] [nvarchar](10) COLLATE Latin1_General_CI_AS NULL,
	[duong] [nvarchar](20) COLLATE Latin1_General_CI_AS NULL,
	[quan] [nvarchar](20) COLLATE Latin1_General_CI_AS NULL,
	[thanhPho] [nvarchar](20) COLLATE Latin1_General_CI_AS NULL,
	[giaTB] [bigint] NULL,
	[moTa] [nvarchar](100) COLLATE Latin1_General_CI_AS NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[maKS] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 11/06/2018 23:39:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KhachHang]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[KhachHang](
	[maKH] [int] IDENTITY(1,1) NOT NULL,
	[hoTen] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[tenDangNhap] [varchar](50) COLLATE Latin1_General_CI_AS NULL,
	[matKhau] [varchar](20) COLLATE Latin1_General_CI_AS NULL,
	[soCMND] [varchar](15) COLLATE Latin1_General_CI_AS NULL,
	[diaChi] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[soDienThoai] [varchar](15) COLLATE Latin1_General_CI_AS NULL,
	[moTa] [nvarchar](100) COLLATE Latin1_General_CI_AS NULL,
	[email] [varchar](20) COLLATE Latin1_General_CI_AS NULL,
 CONSTRAINT [PK_KhachHang] PRIMARY KEY CLUSTERED 
(
	[maKH] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 11/06/2018 23:39:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NhanVien]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[NhanVien](
	[maNV] [int] IDENTITY(1,1) NOT NULL,
	[hoTen] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[tenDangNhap] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[matKhau] [varchar](20) COLLATE Latin1_General_CI_AS NULL,
	[maKS] [int] NULL,
 CONSTRAINT [PK_NhanVien] PRIMARY KEY CLUSTERED 
(
	[maNV] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LoaiPhong]    Script Date: 11/06/2018 23:39:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoaiPhong]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LoaiPhong](
	[maLoaiPhong] [int] IDENTITY(1,1) NOT NULL,
	[tenLoaiPhong] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[maKS] [int] NULL,
	[donGia] [bigint] NULL,
	[moTa] [nvarchar](50) COLLATE Latin1_General_CI_AS NULL,
	[slTrong] [int] NULL,
 CONSTRAINT [PK_LoaiPhong] PRIMARY KEY CLUSTERED 
(
	[maLoaiPhong] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Phong]    Script Date: 11/06/2018 23:39:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Phong]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Phong](
	[maPhong] [int] IDENTITY(1,1) NOT NULL,
	[loaiPhong] [int] NULL,
	[soPhong] [varchar](10) COLLATE Latin1_General_CI_AS NULL,
 CONSTRAINT [PK_Phong] PRIMARY KEY CLUSTERED 
(
	[maPhong] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DatPhong]    Script Date: 11/06/2018 23:39:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DatPhong]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DatPhong](
	[maDP] [int] IDENTITY(1,1) NOT NULL,
	[maLoaiPhong] [int] NULL,
	[maKH] [int] NULL,
	[ngayBatDau] [date] NULL,
	[ngayTraPhong] [date] NULL,
	[ngayDat] [date] NULL,
	[donGia] [bigint] NULL,
	[moTa] [nvarchar](100) COLLATE Latin1_General_CI_AS NULL,
	[tinhTrang] [nvarchar](30) COLLATE Latin1_General_CI_AS NULL,
 CONSTRAINT [PK_DatPhong] PRIMARY KEY CLUSTERED 
(
	[maDP] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[TrangThaiPhong]    Script Date: 11/06/2018 23:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TrangThaiPhong]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TrangThaiPhong](
	[maPhong] [int] NOT NULL,
	[ngay] [date] NOT NULL,
	[tinhTrang] [nvarchar](30) COLLATE Latin1_General_CI_AS NULL,
 CONSTRAINT [PK_TrangThaiPhong] PRIMARY KEY CLUSTERED 
(
	[maPhong] ASC,
	[ngay] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 11/06/2018 23:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HoaDon]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[HoaDon](
	[maHD] [int] IDENTITY(1,1) NOT NULL,
	[ngayThanhToan] [date] NULL,
	[tongTien] [bigint] NULL,
	[maDP] [int] NULL,
 CONSTRAINT [PK_HoaDon] PRIMARY KEY CLUSTERED 
(
	[maHD] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  ForeignKey [FK_NhanVien_KhachSan]    Script Date: 11/06/2018 23:39:15 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NhanVien_KhachSan]') AND parent_object_id = OBJECT_ID(N'[dbo].[NhanVien]'))
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD  CONSTRAINT [FK_NhanVien_KhachSan] FOREIGN KEY([maKS])
REFERENCES [dbo].[KhachSan] ([maKS])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NhanVien_KhachSan]') AND parent_object_id = OBJECT_ID(N'[dbo].[NhanVien]'))
ALTER TABLE [dbo].[NhanVien] CHECK CONSTRAINT [FK_NhanVien_KhachSan]
GO
/****** Object:  ForeignKey [FK_LoaiPhong_KhachSan]    Script Date: 11/06/2018 23:39:15 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LoaiPhong_KhachSan]') AND parent_object_id = OBJECT_ID(N'[dbo].[LoaiPhong]'))
ALTER TABLE [dbo].[LoaiPhong]  WITH CHECK ADD  CONSTRAINT [FK_LoaiPhong_KhachSan] FOREIGN KEY([maKS])
REFERENCES [dbo].[KhachSan] ([maKS])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LoaiPhong_KhachSan]') AND parent_object_id = OBJECT_ID(N'[dbo].[LoaiPhong]'))
ALTER TABLE [dbo].[LoaiPhong] CHECK CONSTRAINT [FK_LoaiPhong_KhachSan]
GO
/****** Object:  ForeignKey [FK_Phong_LoaiPhong]    Script Date: 11/06/2018 23:39:15 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Phong_LoaiPhong]') AND parent_object_id = OBJECT_ID(N'[dbo].[Phong]'))
ALTER TABLE [dbo].[Phong]  WITH CHECK ADD  CONSTRAINT [FK_Phong_LoaiPhong] FOREIGN KEY([loaiPhong])
REFERENCES [dbo].[LoaiPhong] ([maLoaiPhong])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Phong_LoaiPhong]') AND parent_object_id = OBJECT_ID(N'[dbo].[Phong]'))
ALTER TABLE [dbo].[Phong] CHECK CONSTRAINT [FK_Phong_LoaiPhong]
GO
/****** Object:  ForeignKey [FK_DatPhong_KhachHang]    Script Date: 11/06/2018 23:39:15 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DatPhong_KhachHang]') AND parent_object_id = OBJECT_ID(N'[dbo].[DatPhong]'))
ALTER TABLE [dbo].[DatPhong]  WITH CHECK ADD  CONSTRAINT [FK_DatPhong_KhachHang] FOREIGN KEY([maKH])
REFERENCES [dbo].[KhachHang] ([maKH])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DatPhong_KhachHang]') AND parent_object_id = OBJECT_ID(N'[dbo].[DatPhong]'))
ALTER TABLE [dbo].[DatPhong] CHECK CONSTRAINT [FK_DatPhong_KhachHang]
GO
/****** Object:  ForeignKey [FK_DatPhong_LoaiPhong]    Script Date: 11/06/2018 23:39:15 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DatPhong_LoaiPhong]') AND parent_object_id = OBJECT_ID(N'[dbo].[DatPhong]'))
ALTER TABLE [dbo].[DatPhong]  WITH CHECK ADD  CONSTRAINT [FK_DatPhong_LoaiPhong] FOREIGN KEY([maLoaiPhong])
REFERENCES [dbo].[LoaiPhong] ([maLoaiPhong])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DatPhong_LoaiPhong]') AND parent_object_id = OBJECT_ID(N'[dbo].[DatPhong]'))
ALTER TABLE [dbo].[DatPhong] CHECK CONSTRAINT [FK_DatPhong_LoaiPhong]
GO
/****** Object:  ForeignKey [FK_TrangThaiPhong_Phong]    Script Date: 11/06/2018 23:39:16 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TrangThaiPhong_Phong]') AND parent_object_id = OBJECT_ID(N'[dbo].[TrangThaiPhong]'))
ALTER TABLE [dbo].[TrangThaiPhong]  WITH CHECK ADD  CONSTRAINT [FK_TrangThaiPhong_Phong] FOREIGN KEY([maPhong])
REFERENCES [dbo].[Phong] ([maPhong])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TrangThaiPhong_Phong]') AND parent_object_id = OBJECT_ID(N'[dbo].[TrangThaiPhong]'))
ALTER TABLE [dbo].[TrangThaiPhong] CHECK CONSTRAINT [FK_TrangThaiPhong_Phong]
GO
/****** Object:  ForeignKey [FK_HoaDon_DatPhong]    Script Date: 11/06/2018 23:39:16 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_HoaDon_DatPhong]') AND parent_object_id = OBJECT_ID(N'[dbo].[HoaDon]'))
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_DatPhong] FOREIGN KEY([maDP])
REFERENCES [dbo].[DatPhong] ([maDP])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_HoaDon_DatPhong]') AND parent_object_id = OBJECT_ID(N'[dbo].[HoaDon]'))
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_DatPhong]
GO
