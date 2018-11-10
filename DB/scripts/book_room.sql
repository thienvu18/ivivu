/*
FUNCTION: func_FindRoomTypeByName
INPUT: tenLoaiPhong(NVARCHAR(50))
OUTPUT:
------------------------------------------
|   MaLoaiPhong    |  TenLoaiPhong       |
------------------------------------------
*/
CREATE FUNCTION func_FindRoomTypeByName (@tenLoaiPhong NVARCHAR(50))
RETURNS TABLE
AS
RETURN
(
	SELECT LoaiPhong.maLoaiPhong AS MaLoaiPhong, LoaiPhong.tenLoaiPhong AS TenLoaiPhong
	FROM LoaiPhong
	WHERE LoaiPhong.tenLoaiPhong LIKE N'%' + @tenLoaiPhong + N'%'
);
GO
/*
FUNCTION: func_CheckRoomStatus
INPUT: loaiPhong(INT), ngay(DATE)
OUTPUT:
------------------------------------------
|    SoPhong    |     TrangThaiPhong     |
------------------------------------------
*/
CREATE FUNCTION func_CheckRoomStatus (@maLoaiPhong INT, @ngay DATE)
RETURNS TABLE
AS
RETURN
(
	SELECT Phong.soPhong AS SoPhong, TrangThaiPhong.tinhTrang AS TinhTrangPhong
	FROM TrangThaiPhong JOIN Phong ON TrangThaiPhong.maPhong = Phong.maPhong
	WHERE Phong.loaiPhong = @maLoaiPhong AND TrangThaiPhong.ngay = @ngay
);
GO
/*
FUNCTION: func_BookRoom
INPUT: tenLoaiPhong(NVARCHAR(50))
OUTPUT:
------------------------------------------
|   MaLoaiPhong    |  TenLoaiPhong       |
------------------------------------------
*/