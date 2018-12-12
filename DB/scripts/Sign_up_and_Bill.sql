
/*
PROCUEDURE: proc_signUpUser
INPUT:	FullName	NVARCHAR(50),
		UserName	VARCHAR(50),
		Pass		VARCHAR(20),
		CMND		VARCHAR(15),
		SDT		VARCHAR(15),
		Address	NVARCHAR(50),
		Mail		VARCHAR(20)
OUTPUT:	Thêm 1 record trong table KhachHang
*/

DROP PROCEDURE proc_signUpUser
GO
CREATE PROCEDURE proc_signUpUser
	@FullName	NVARCHAR(50),
	@UserName	VARCHAR(50),
	@Pass		VARCHAR(20),
	@CMND		VARCHAR(15),
	@SDT		VARCHAR(15),
	@Address	NVARCHAR(50),
	@Mail		VARCHAR(20)
AS
BEGIN
	IF(EXISTS(	SELECT KH.tenDangNhap
				FROM KhachHang KH
				WHERE KH.tenDangNhap = @UserName)
	)
		RETURN 0
		
	INSERT INTO dbo.KhachHang
			( hoTen ,
			tenDangNhap ,
			matKhau ,
			soCMND ,
			diaChi ,
			soDienThoai ,
			email
					        )
	VALUES  ( @FullName , -- hoTen - nvarchar(50)
			@UserName , -- tenDangNhap - varchar(50)
			@Pass , -- matKhau - varchar(20)
			@CMND , -- soCMND - varchar(15)
			@Address , -- diaChi - nvarchar(50)
			@SDT , -- soDienThoai - varchar(15)
			@Mail  -- email - varchar(20)
			)
	RETURN 1
END
GO

select	*
from KhachHang kh
where kh.tenDangNhap = 'nguyentu123'
go


/*
PROCEDURE: proc_setBill
INPUT:	maDatPhong INT
OUTPUT:	Thêm 1 record trong table HoaDon
*/

drop procedure proc_setBill
go
CREATE PROCEDURE proc_setBill
	@maDatPhong INT
AS
BEGIN
	IF(NOT EXISTS(	SELECT DP.maDP
				FROM DatPhong DP
				WHERE DP.maDP = @maDatPhong)
	)
		RETURN 0
	ELSE IF(EXISTS (SELECT HD.maDP
					FROM HoaDon HD
					WHERE HD.maDP = @maDatPhong))
		RETURN 1
	ELSE
		BEGIN
			INSERT INTO dbo.HoaDon( ngayThanhToan, maDP )
			VALUES  ( GETDATE(), -- ngayThanhToan - date
					   @maDatPhong  -- maDP - int
	        )
			UPDATE dbo.HoaDon SET tongTien = (	SELECT (DATEDIFF(DAY, DATE_PRICE.NTP, DATE_PRICE.NBD) * DATE_PRICE.GIA) AS N'Tổng Tiền'
											FROM(	SELECT DP.ngayBatDau AS NBD, DP.ngayTraPhong AS NTP, DP.donGia AS GIA
													FROM dbo.DatPhong DP
													WHERE DP.maDP = @maDatPhong
											) AS DATE_PRICE
									   )
			RETURN 2
		END
	
END
GO

DECLARE @TEST INT 
EXEC  @TEST = proc_setBill 0
PRINT @TEST
GO