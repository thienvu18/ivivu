USE QLKhachSan

-- Báo cáo doanh thu theo tháng
GO
CREATE PROC proc_reportRevenueByMonth
@start DATE,
@end DATE
AS
BEGIN
SELECT MONTH(ngayThanhToan) AS MONTH, YEAR(ngayThanhToan) AS YEAR, COUNT(*) AS COUNT, SUM(tongTien) AS DOANHTHU 
FROM HoaDon 
WHERE ngayThanhToan <= @end AND ngayThanhToan >= @start
GROUP BY MONTH(ngayThanhToan), YEAR(ngayThanhToan) 
ORDER BY MONTH(ngayThanhToan) DESC, YEAR(ngayThanhToan) DESC
END

--GO
--EXEC proc_reportRevenueByMonth '2000-11-11', '2010-11-11'

-- Báo cáo doanh thu theo năm
GO
CREATE PROC proc_reportRevenueByYear
@start DATE,
@end DATE
AS
BEGIN
	SELECT YEAR(ngayThanhToan) AS YEAR, COUNT(*) AS COUNT, SUM(tongTien) AS DOANHTHU 
FROM HoaDon 
WHERE ngayThanhToan <= @end AND ngayThanhToan >= @start
GROUP BY YEAR(ngayThanhToan) 
ORDER BY YEAR DESC
END

--GO
--EXEC proc_reportRevenueByYear '2000-11-11', '2010-11-11'

-- Báo cáo doanh thu theo loại phòng
GO
CREATE PROC proc_reportRevenueByTypeRoom
@start DATE,
@end DATE
AS
BEGIN
	SELECT dbo.LoaiPhong.maLoaiPhong, dbo.LoaiPhong.tenLoaiPhong, SUM(HoaDon.tongTien) AS DOANHTHU FROM dbo.HoaDon, dbo.DatPhong, dbo.LoaiPhong
	WHERE dbo.HoaDon.maDP = dbo.DatPhong.maDP AND dbo.DatPhong.maLoaiPhong = LoaiPhong.maLoaiPhong 
	AND dbo.HoaDon.ngayThanhToan <= @end AND dbo.HoaDon.ngayThanhToan >= @start
	GROUP BY dbo.LoaiPhong.maLoaiPhong, dbo.LoaiPhong.tenLoaiPhong
	ORDER BY maLoaiPhong ASC
END

--GO
--EXEC proc_reportRevenueByTypeRoom '2000-11-11', '2010-11-11'

-- Báo cáo các trang thái phòng 'đang bảo trì'
GO
CREATE PROC proc_reportStatusRoom
@start DATE,
@end DATE,
@min INT
AS
BEGIN
	SELECT COUNT(*) AS SOLUONG,  maPhong FROM TrangThaiPhong
	WHERE ngay >=@start AND ngay <= @end AND tinhTrang = N'đang bảo trì'
	GROUP BY maPhong
	HAVING COUNT(*) >= @min
END

--GO
--EXEC proc_reportStatusRoom '2000-11-11', '9000-11-11', 2

-- Báo cáo số lượng phòng trống
GO 
CREATE PROC proc_reportEmptyRoom
@start DATE,
@end DATE
AS
BEGIN
	SELECT LoaiPhong.maLoaiPhong, LoaiPhong.tenLoaiPhong, COUNT(*) AS SOLUONG FROM dbo.Phong, dbo.LoaiPhong, dbo.TrangThaiPhong
	WHERE Phong.loaiPhong = loaiPhong.maLoaiPhong AND TrangThaiPhong.maPhong = Phong.maPhong AND dbo.TrangThaiPhong.tinhTrang  = N'còn trống'
	AND TrangThaiPhong.ngay >= @start AND dbo.TrangThaiPhong.ngay <= @end
	GROUP BY LoaiPhong.maLoaiPhong, LoaiPhong.tenLoaiPhong
	ORDER BY maLoaiPhong
END

--GO
--EXEC proc_reportEmptyRoom '2000-11-11', '8010-11-11'