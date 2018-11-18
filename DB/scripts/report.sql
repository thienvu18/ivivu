GO
CREATE FUNCTION func_reportRevenueByMonth (@start DATE, @end DATE)
RETURNS table
AS
RETURN (
SELECT MONTH(ngayThanhToan) AS MONTH, YEAR(ngayThanhToan) AS YEAR, COUNT(*) AS COUNT, SUM(tongTien) AS DOANHTHU 
FROM HoaDon 
WHERE ngayThanhToan <= @end AND ngayThanhToan >= @start
GROUP BY MONTH(ngayThanhToan), YEAR(ngayThanhToan) 
)

-- GO
-- SELECT * FROM dbo.func_reportRevenueByMonth('2000-11-11', '2010-11-11') ORDER BY YEAR DESC, MONTH DESC

GO
CREATE FUNCTION func_reportRevenueByYear (@start DATE, @end DATE)
RETURNS table
AS
RETURN (
SELECT YEAR(ngayThanhToan) AS YEAR, COUNT(*) AS COUNT, SUM(tongTien) AS DOANHTHU 
FROM HoaDon 
WHERE ngayThanhToan <= @end AND ngayThanhToan >= @start
GROUP BY YEAR(ngayThanhToan) 
)

-- GO
-- SELECT * FROM dbo.func_reportRevenueByYear('2000-11-11', '2010-11-11') ORDER BY YEAR DESC

GO
CREATE FUNCTION func_reportRevenueByTypeRoom (@start DATE, @end DATE)
RETURNS table
AS
RETURN (
SELECT dbo.LoaiPhong.maLoaiPhong, dbo.LoaiPhong.tenLoaiPhong, SUM(HoaDon.tongTien) AS DOANHTHU FROM dbo.HoaDon, dbo.DatPhong, dbo.LoaiPhong
WHERE dbo.HoaDon.maDP = dbo.DatPhong.maDP AND dbo.DatPhong.maLoaiPhong = LoaiPhong.maLoaiPhong 
AND dbo.HoaDon.ngayThanhToan <= @end AND dbo.HoaDon.ngayThanhToan >= @start
GROUP BY dbo.LoaiPhong.maLoaiPhong, dbo.LoaiPhong.tenLoaiPhong
)

-- GO
-- SELECT * FROM dbo.func_reportRevenueByTypeRoom('2000-11-11', '2010-11-11') ORDER BY maLoaiPhong ASC

GO
CREATE FUNCTION func_reportEmptyRoom (@start DATE, @end DATE)
RETURNS table
AS
RETURN (
SELECT LoaiPhong.maLoaiPhong, LoaiPhong.tenLoaiPhong, COUNT(*) AS SOLUONG FROM dbo.Phong, dbo.LoaiPhong, dbo.TrangThaiPhong
WHERE Phong.loaiPhong = loaiPhong.maLoaiPhong AND TrangThaiPhong.maPhong = Phong.maPhong AND dbo.TrangThaiPhong.tinhTrang  = N'còn trống'
AND TrangThaiPhong.ngay >= @start AND dbo.TrangThaiPhong.ngay <= @end
GROUP BY LoaiPhong.maLoaiPhong, LoaiPhong.tenLoaiPhong
)

-- GO
-- SELECT * FROM dbo.func_reportEmptyRoom('2000-11-11', '2010-11-11') ORDER BY maLoaiPhong
