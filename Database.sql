USE [master]
GO
/****** Object:  Database [GroceryStore]    Script Date: 19/05/2024 3:40:56 PM ******/
CREATE DATABASE [GroceryStore]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GroceryStore', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.GUNTER\MSSQL\DATA\GroceryStore.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'GroceryStore_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.GUNTER\MSSQL\DATA\GroceryStore_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [GroceryStore] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GroceryStore].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GroceryStore] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GroceryStore] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GroceryStore] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GroceryStore] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GroceryStore] SET ARITHABORT OFF 
GO
ALTER DATABASE [GroceryStore] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GroceryStore] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GroceryStore] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GroceryStore] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GroceryStore] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GroceryStore] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GroceryStore] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GroceryStore] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GroceryStore] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GroceryStore] SET  ENABLE_BROKER 
GO
ALTER DATABASE [GroceryStore] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GroceryStore] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GroceryStore] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GroceryStore] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GroceryStore] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GroceryStore] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GroceryStore] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GroceryStore] SET RECOVERY FULL 
GO
ALTER DATABASE [GroceryStore] SET  MULTI_USER 
GO
ALTER DATABASE [GroceryStore] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GroceryStore] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GroceryStore] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GroceryStore] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [GroceryStore] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [GroceryStore] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'GroceryStore', N'ON'
GO
ALTER DATABASE [GroceryStore] SET QUERY_STORE = ON
GO
ALTER DATABASE [GroceryStore] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [GroceryStore]
GO
/****** Object:  Table [dbo].[ANH]    Script Date: 19/05/2024 3:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ANH](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Url] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BINHLUAN]    Script Date: 19/05/2024 3:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BINHLUAN](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MaSP] [int] NOT NULL,
	[MaTK] [int] NOT NULL,
	[NoiDung] [nvarchar](max) NULL,
	[PhanHoi] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CHITIETDH]    Script Date: 19/05/2024 3:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHITIETDH](
	[MaDH] [int] NOT NULL,
	[MaSP] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[GiaBan] [int] NOT NULL,
	[DanhGia] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaDH] ASC,
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DANHMUC]    Script Date: 19/05/2024 3:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DANHMUC](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenDanhMuc] [nvarchar](30) NULL,
 CONSTRAINT [PK_DMUC] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DONHANG]    Script Date: 19/05/2024 3:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DONHANG](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[KhachHang] [int] NOT NULL,
	[DcGiaoHang] [nvarchar](70) NULL,
	[MaPhuong] [int] NOT NULL,
	[MaQuan] [int] NOT NULL,
	[MaTP] [int] NOT NULL,
	[XuatHD] [bit] NOT NULL,
	[NgayDatHang] [datetime] NOT NULL,
	[NgayGiaoHang] [datetime] NULL,
	[ThanhCong] [bit] NULL,
	[DvVanChuyen] [nvarchar](40) NULL,
	[MaVanChuyen] [nvarchar](20) NULL,
	[PhiShip] [int] NULL,
	[GhiChuKhach] [nvarchar](253) NULL,
	[GhiChuShop] [nvarchar](253) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LOAISP]    Script Date: 19/05/2024 3:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LOAISP](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LoaiSP] [nvarchar](30) NULL,
	[TenDuongDan] [nvarchar](30) NULL,
	[GhiChu] [nvarchar](50) NULL,
	[ID_DanhMuc] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PHUONGXA]    Script Date: 19/05/2024 3:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PHUONGXA](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NULL,
	[QuanHuyen] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QUANHUYEN]    Script Date: 19/05/2024 3:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QUANHUYEN](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NULL,
	[TinhThanh] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SANPHAM]    Script Date: 19/05/2024 3:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SANPHAM](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MaLoai] [int] NOT NULL,
	[TenSP] [nvarchar](40) NOT NULL,
	[TenDuongDan] [nvarchar](40) NOT NULL,
	[TomTat] [nvarchar](100) NULL,
	[NgayDangSP] [datetime] NOT NULL,
	[GiaBan] [int] NULL,
	[GiaKM] [int] NULL,
	[Dvt] [nvarchar](10) NOT NULL,
	[SoLuong] [int] NOT NULL,
	[AnhBia] [int] NOT NULL,
	[NdSP] [nvarchar](max) NULL,
	[LuotXem] [int] NOT NULL,
	[LuotMua] [int] NOT NULL,
	[DangSP] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TAIKHOAN]    Script Date: 19/05/2024 3:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAIKHOAN](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[HoTen] [nvarchar](30) NOT NULL,
	[TenTK] [nvarchar](30) NOT NULL,
	[MatKhau] [nvarchar](50) NOT NULL,
	[NgayCap] [datetime] NOT NULL,
	[GioiTinh] [bit] NOT NULL,
	[SDT] [nvarchar](15) NULL,
	[Email] [nvarchar](30) NULL,
	[DuocSD] [bit] NOT NULL,
	[GhiChu] [nvarchar](max) NULL,
	[Avatar] [int] NULL,
	[VaiTro] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[THUVIENANHSP]    Script Date: 19/05/2024 3:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[THUVIENANHSP](
	[MaANH] [int] NOT NULL,
	[MaSP] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaANH] ASC,
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TINHTHANH]    Script Date: 19/05/2024 3:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TINHTHANH](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ANH] ON 

INSERT [dbo].[ANH] ([ID], [Url]) VALUES (1, N'/Asset/Image/profile/Screenshot 2024-02-07 234248.png')
INSERT [dbo].[ANH] ([ID], [Url]) VALUES (2, N'/Asset/Image/profile/2.jpg')
INSERT [dbo].[ANH] ([ID], [Url]) VALUES (19, N'/Asset/Image/rac/1.jpg')
INSERT [dbo].[ANH] ([ID], [Url]) VALUES (20, N'/Asset/Image/rac/2.jpg')
SET IDENTITY_INSERT [dbo].[ANH] OFF
GO
SET IDENTITY_INSERT [dbo].[DANHMUC] ON 

INSERT [dbo].[DANHMUC] ([ID], [TenDanhMuc]) VALUES (2, N'Thịt - Hải sản tươi')
INSERT [dbo].[DANHMUC] ([ID], [TenDanhMuc]) VALUES (3, N'Rau - Củ - Trái Cây')
INSERT [dbo].[DANHMUC] ([ID], [TenDanhMuc]) VALUES (4, N'Hóa Phẩm - Tẩy rửa')
INSERT [dbo].[DANHMUC] ([ID], [TenDanhMuc]) VALUES (5, N'Chăm Sóc Cá Nhân')
INSERT [dbo].[DANHMUC] ([ID], [TenDanhMuc]) VALUES (6, N'Sữa các loại')
INSERT [dbo].[DANHMUC] ([ID], [TenDanhMuc]) VALUES (7, N'Bánh Kẹo')
INSERT [dbo].[DANHMUC] ([ID], [TenDanhMuc]) VALUES (8, N'Đồ uống')
INSERT [dbo].[DANHMUC] ([ID], [TenDanhMuc]) VALUES (9, N'Mì - Thực Phẩm Ăn Liền')
INSERT [dbo].[DANHMUC] ([ID], [TenDanhMuc]) VALUES (10, N'Thực Phẩm Khô')
INSERT [dbo].[DANHMUC] ([ID], [TenDanhMuc]) VALUES (11, N'Thực Phẩm Chế Biến')
INSERT [dbo].[DANHMUC] ([ID], [TenDanhMuc]) VALUES (12, N'Gia vị')
INSERT [dbo].[DANHMUC] ([ID], [TenDanhMuc]) VALUES (13, N'Thực Phẩm Đông Lạnh')
INSERT [dbo].[DANHMUC] ([ID], [TenDanhMuc]) VALUES (14, N'Trứng - Đậu Hũ')
INSERT [dbo].[DANHMUC] ([ID], [TenDanhMuc]) VALUES (15, N'Chăm Sóc Bé')
INSERT [dbo].[DANHMUC] ([ID], [TenDanhMuc]) VALUES (16, N'Đồ Dùng Gia Đình')
INSERT [dbo].[DANHMUC] ([ID], [TenDanhMuc]) VALUES (17, N'Điện Gia Dụng')
INSERT [dbo].[DANHMUC] ([ID], [TenDanhMuc]) VALUES (18, N'Văn Phòng Phẩm - Đồ Chơi')
SET IDENTITY_INSERT [dbo].[DANHMUC] OFF
GO
SET IDENTITY_INSERT [dbo].[LOAISP] ON 

INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (1, N'Thịt', N'thit', N'Các loại thịt', 2)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (2, N'Hải Sản', N'hai-san', NULL, 2)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (3, N'Rau Lá', N'rau-la', NULL, 3)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (4, N'Củ, Quả', N'cu-qua', NULL, 3)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (5, N'Trái Cây Tươi', N'trai-cay-tuoi', NULL, 3)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (6, N'Bình Xịt Côn Trùng', N'binh-xit-con-trung', NULL, 4)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (7, N'Nước Giặt', N'nuoc-giat', NULL, 4)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (8, N'Nước Lau Sàn - Lau kính', N'nuoc-lau-san', NULL, 4)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (9, N'Nước Rửa Chén', N'nuoc-rua-chen', NULL, 4)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (10, N'Nước Tẩy Rửa', N'nuoc-tay-rua', NULL, 4)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (11, N'Nước Xả', N'nuoc-xa', NULL, 4)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (12, N'Chăm Sóc Tóc', N'cham-soc-toc', NULL, 5)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (13, N'Chăm Sóc Da', N'cham-soc-da', NULL, 5)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (14, N'Chăm Sóc Răng Miệng', N'cham-soc-rang-mieng', NULL, 5)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (15, N'Chăm Sóc Phụ Nữ', N'cham-soc-phu-nu', NULL, 5)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (16, N'Chăm Sóc Cá Nhân Khàc', N'cham-soc-ca-nhan', NULL, 5)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (17, N'Mỹ Phẩm', N'my-pham', NULL, 5)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (18, N'Khăn Giấy', N'khan-giay', NULL, 5)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (19, N'Sữa Tươi', N'sua-tuoi', NULL, 6)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (20, N'Sữa Hạt - Sữa Đậu', N'sua-hat', NULL, 6)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (21, N'Sữa Bột', N'sua-bot', NULL, 6)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (22, N'Bơ Sửa - Phô Mai', N'bo-sua', NULL, 6)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (23, N'Sữa Đặc', N'sua-dac', NULL, 6)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (24, N'Sữa Chua - Váng Sữa', N'sua-chua', NULL, 6)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (25, N'Bánh Xốp - Bánh Quy', N'banh-xop', NULL, 7)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (26, N'Kẹo - Chocolate', N'keo', NULL, 7)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (27, N'Bánh Snack', N'banh-snack', NULL, 7)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (28, N'Hạt-Trái Cây Sấy Khô', N'trai-cay-say-kho', NULL, 7)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (29, N'Bia', N'bia', NULL, 8)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (30, N'Cà Phê', N'ca-phe', NULL, 8)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (31, N'Nước Suối', N'nuoc-suoi', NULL, 8)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (32, N'Nước Ngọt', N'nuoc-ngot', NULL, 8)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (33, N'Trà - Các Loại Khác', N'tra', NULL, 8)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (34, N'Mì', N'mi', NULL, 9)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (35, N'Miến - Hủ Tíu - Bánh Canh', N'mien-hutiu-banhcanh', NULL, 9)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (36, N'Cháo', N'chao', NULL, 9)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (37, N'Phở - Bún', N'pho-bun', NULL, 9)
INSERT [dbo].[LOAISP] ([ID], [LoaiSP], [TenDuongDan], [GhiChu], [ID_DanhMuc]) VALUES (38, N'Gạo - Nông Sản Khô', N'gao---nong-san-kho', NULL, 10)
SET IDENTITY_INSERT [dbo].[LOAISP] OFF
GO
SET IDENTITY_INSERT [dbo].[SANPHAM] ON 

INSERT [dbo].[SANPHAM] ([ID], [MaLoai], [TenSP], [TenDuongDan], [TomTat], [NgayDangSP], [GiaBan], [GiaKM], [Dvt], [SoLuong], [AnhBia], [NdSP], [LuotXem], [LuotMua], [DangSP]) VALUES (1, 1, N'MEATDELI Thịt Nạc vai heo (S)
', N'meat-deli-nac-vai-heo', NULL, CAST(N'2024-04-08T00:00:00.000' AS DateTime), 62434, 49494, N'Hộp', 20, 1, NULL, 10, 0, 1)
INSERT [dbo].[SANPHAM] ([ID], [MaLoai], [TenSP], [TenDuongDan], [TomTat], [NgayDangSP], [GiaBan], [GiaKM], [Dvt], [SoLuong], [AnhBia], [NdSP], [LuotXem], [LuotMua], [DangSP]) VALUES (2, 1, N'12', N'12', NULL, CAST(N'2024-05-13T13:16:26.270' AS DateTime), 111, NULL, N'Cái', 12, 1, N'<p>12</p>', 0, 0, 1)
SET IDENTITY_INSERT [dbo].[SANPHAM] OFF
GO
SET IDENTITY_INSERT [dbo].[TAIKHOAN] ON 

INSERT [dbo].[TAIKHOAN] ([ID], [HoTen], [TenTK], [MatKhau], [NgayCap], [GioiTinh], [SDT], [Email], [DuocSD], [GhiChu], [Avatar], [VaiTro]) VALUES (1, N'Nguyễn Quốc Thái', N'admin', N'202cb962ac59075b964b07152d234b70', CAST(N'2024-05-14T02:27:43.440' AS DateTime), 1, N'0123254789', N'thai@example.com', 1, N'Admin', 1, N'Admin')
INSERT [dbo].[TAIKHOAN] ([ID], [HoTen], [TenTK], [MatKhau], [NgayCap], [GioiTinh], [SDT], [Email], [DuocSD], [GhiChu], [Avatar], [VaiTro]) VALUES (2, N'Khách hàng 2', N'user1', N'b59c67bf196a4758191e42f76670ceba', CAST(N'2024-05-14T02:27:43.440' AS DateTime), 1, N'0987654321', N'user1@example.com', 1, N'Khách hàng tiềm năng', 2, N'User')
INSERT [dbo].[TAIKHOAN] ([ID], [HoTen], [TenTK], [MatKhau], [NgayCap], [GioiTinh], [SDT], [Email], [DuocSD], [GhiChu], [Avatar], [VaiTro]) VALUES (3, N'Khách hàng 2', N'user2', N'202cb962ac59075b964b07152d234b70', CAST(N'2024-05-14T02:27:43.440' AS DateTime), 1, N'0987654322', N'user2@example.com', 1, N'Khách hàng tiềm năng', NULL, N'User')
INSERT [dbo].[TAIKHOAN] ([ID], [HoTen], [TenTK], [MatKhau], [NgayCap], [GioiTinh], [SDT], [Email], [DuocSD], [GhiChu], [Avatar], [VaiTro]) VALUES (4, N'Khách hàng 3', N'user3', N'202cb962ac59075b964b07152d234b70', CAST(N'2024-05-14T02:27:43.440' AS DateTime), 0, N'0987654323', N'user3@example.com', 1, N'Khách hàng tiềm năng', NULL, N'User')
INSERT [dbo].[TAIKHOAN] ([ID], [HoTen], [TenTK], [MatKhau], [NgayCap], [GioiTinh], [SDT], [Email], [DuocSD], [GhiChu], [Avatar], [VaiTro]) VALUES (5, N'Khách hàng 4', N'user4', N'202cb962ac59075b964b07152d234b70', CAST(N'2024-05-14T02:27:43.440' AS DateTime), 0, N'0987654324', N'user4@example.com', 1, N'Khách hàng tiềm năng', NULL, N'User')
INSERT [dbo].[TAIKHOAN] ([ID], [HoTen], [TenTK], [MatKhau], [NgayCap], [GioiTinh], [SDT], [Email], [DuocSD], [GhiChu], [Avatar], [VaiTro]) VALUES (6, N'Khách hàng 2', N'user5', N'202cb962ac59075b964b07152d234b70', CAST(N'2024-05-19T13:46:13.433' AS DateTime), 1, N'0123254788', N'user5@gmail.com', 1, NULL, NULL, N'User')
SET IDENTITY_INSERT [dbo].[TAIKHOAN] OFF
GO
ALTER TABLE [dbo].[BINHLUAN]  WITH CHECK ADD FOREIGN KEY([MaSP])
REFERENCES [dbo].[SANPHAM] ([ID])
GO
ALTER TABLE [dbo].[BINHLUAN]  WITH CHECK ADD FOREIGN KEY([MaTK])
REFERENCES [dbo].[TAIKHOAN] ([ID])
GO
ALTER TABLE [dbo].[BINHLUAN]  WITH CHECK ADD FOREIGN KEY([PhanHoi])
REFERENCES [dbo].[BINHLUAN] ([ID])
GO
ALTER TABLE [dbo].[CHITIETDH]  WITH CHECK ADD FOREIGN KEY([MaDH])
REFERENCES [dbo].[DONHANG] ([ID])
GO
ALTER TABLE [dbo].[CHITIETDH]  WITH CHECK ADD FOREIGN KEY([MaSP])
REFERENCES [dbo].[SANPHAM] ([ID])
GO
ALTER TABLE [dbo].[DONHANG]  WITH CHECK ADD FOREIGN KEY([KhachHang])
REFERENCES [dbo].[TAIKHOAN] ([ID])
GO
ALTER TABLE [dbo].[DONHANG]  WITH CHECK ADD FOREIGN KEY([MaPhuong])
REFERENCES [dbo].[PHUONGXA] ([ID])
GO
ALTER TABLE [dbo].[DONHANG]  WITH CHECK ADD FOREIGN KEY([MaQuan])
REFERENCES [dbo].[QUANHUYEN] ([ID])
GO
ALTER TABLE [dbo].[DONHANG]  WITH CHECK ADD FOREIGN KEY([MaTP])
REFERENCES [dbo].[TINHTHANH] ([ID])
GO
ALTER TABLE [dbo].[LOAISP]  WITH CHECK ADD FOREIGN KEY([ID_DanhMuc])
REFERENCES [dbo].[DANHMUC] ([ID])
GO
ALTER TABLE [dbo].[PHUONGXA]  WITH CHECK ADD FOREIGN KEY([QuanHuyen])
REFERENCES [dbo].[QUANHUYEN] ([ID])
GO
ALTER TABLE [dbo].[QUANHUYEN]  WITH CHECK ADD FOREIGN KEY([TinhThanh])
REFERENCES [dbo].[TINHTHANH] ([ID])
GO
ALTER TABLE [dbo].[SANPHAM]  WITH CHECK ADD FOREIGN KEY([AnhBia])
REFERENCES [dbo].[ANH] ([ID])
GO
ALTER TABLE [dbo].[SANPHAM]  WITH CHECK ADD FOREIGN KEY([MaLoai])
REFERENCES [dbo].[LOAISP] ([ID])
GO
ALTER TABLE [dbo].[TAIKHOAN]  WITH CHECK ADD FOREIGN KEY([Avatar])
REFERENCES [dbo].[ANH] ([ID])
GO
ALTER TABLE [dbo].[THUVIENANHSP]  WITH CHECK ADD FOREIGN KEY([MaANH])
REFERENCES [dbo].[ANH] ([ID])
GO
ALTER TABLE [dbo].[THUVIENANHSP]  WITH CHECK ADD FOREIGN KEY([MaSP])
REFERENCES [dbo].[SANPHAM] ([ID])
GO
/****** Object:  StoredProcedure [dbo].[GetTotalSumIn7days]    Script Date: 19/05/2024 3:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTotalSumIn7days]
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NgayHienTai DATETIME = GETDATE();
    DECLARE @NgayBatDau DATETIME = DATEADD(DAY, -7, @NgayHienTai);

    SELECT 
        CAST(DONHANG.NgayDatHang AS DATE) AS NgayDatHang, 
        SUM(CHITIETDH.GiaBan) AS TongTien
    FROM DONHANG
    INNER JOIN CHITIETDH ON DONHANG.ID = CHITIETDH.MaDH
    WHERE DONHANG.NgayDatHang >= @NgayBatDau AND DONHANG.NgayDatHang <= @NgayHienTai
    GROUP BY CAST(DONHANG.NgayDatHang AS DATE)
    ORDER BY NgayDatHang;
END
GO
/****** Object:  StoredProcedure [dbo].[remov_file_anh]    Script Date: 19/05/2024 3:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[remov_file_anh]
    @file NVARCHAR(253)
AS
BEGIN
    DELETE FROM ANH
    WHERE Url LIKE '%' + @file + '%';
END;
GO
/****** Object:  StoredProcedure [dbo].[Rename_path_anh]    Script Date: 19/05/2024 3:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[Rename_path_anh]
	@folderOldName NVARCHAR(100),
	@folderNewName NVARCHAR(100)
AS
BEGIN
    -- Cập nhật URL
    UPDATE ANH
    SET Url = REPLACE(Url, @folderOldName, @folderNewName)
    WHERE Url LIKE '%' + @folderOldName + '%';
END;
GO
/****** Object:  StoredProcedure [dbo].[Update_url_anh]    Script Date: 19/05/2024 3:40:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_url_anh]
    @ID INT,
    @NewUrl NVARCHAR(100)
AS
BEGIN
    -- Cập nhật URL mới cho bản ghi có ID tương ứng
    UPDATE ANH
    SET Url = @NewUrl
    WHERE ID = @ID;
END;
GO
USE [master]
GO
ALTER DATABASE [GroceryStore] SET  READ_WRITE 
GO
