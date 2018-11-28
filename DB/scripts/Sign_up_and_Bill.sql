/*
FUNCTION: func_checkUserName
INPUT: UserName VARCHAR(50)
OUTPUT: True | False
*/

CREATE FUNCTION func_checkUserName(@UserName VARCHAR(50))
RETURNS NVARCHAR(300)
AS
BEGIN
	DECLARE @notify_st NVARCHAR(300);
	IF(	LEN(@UserName) > 50 OR 
		@UserName = N'' OR 
		EXISTS(	SELECT KH.tenDangNhap
				FROM KhachHang KH 
				WHERE KH.tenDangNhap = @UserName)
	)
		SET @notify_st = N'False'
	ELSE
		SET @notify_st = N'True'
	RETURN @notify_st;
END
GO
/*
FUNCTION: func_checkPass
INPUT: Pass VARCHAR(20)
OUTPUT: True | False
*/

CREATE FUNCTION func_checkPass(@Pass VARCHAR(20))
RETURNS NVARCHAR(300)
AS
BEGIN
	DECLARE @notify_st NVARCHAR(300);
	IF(LEN(@Pass) > 50 OR @Pass = N'')
		SET @notify_st = N'False'
	ELSE
		SET @notify_st = N'True'
	RETURN @notify_st;
END
GO

/*
PROCUEDURE: proc_signUpUser
INPUT:	FullName	NVARCHAR(50),
		UserName	VARCHAR(50),
		Pass		VARCHAR(20),
		CMND		VARCHAR(15),
		SDT		VARCHAR(15),
		Address	NVARCHAR(50),
		Description NVARCHAR(100),
		Mail		VARCHAR(20),
OUTPUT:	True | False
	+) True: Notify True and insert a new record into KhachHang table
	+) False: Notify Fasle
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
	@Description NVARCHAR(100),
	@Mail		VARCHAR(20),
	@Notify		NVARCHAR(300) OUTPUT
AS
BEGIN
	SET	@Notify = dbo.func_checkUserName(@UserName)
	IF (@Notify = N'True')
		BEGIN
			SET @Notify = dbo.func_checkUserName(@Pass)
			IF(@Notify = N'True')
				BEGIN
					INSERT INTO dbo.KhachHang
					        ( hoTen ,
					          tenDangNhap ,
					          matKhau ,
					          soCMND ,
					          diaChi ,
					          soDienThoai ,
					          moTa ,
					          email
					        )
					VALUES  ( @FullName , -- hoTen - nvarchar(50)
					          @UserName , -- tenDangNhap - varchar(50)
					          @Pass , -- matKhau - varchar(20)
					          @CMND , -- soCMND - varchar(15)
					          @Address , -- diaChi - nvarchar(50)
					          @SDT , -- soDienThoai - varchar(15)
					          @Description , -- moTa - nvarchar(100)
					          @Mail  -- email - varchar(20)
					        )
				END
		END
END
GO

--Testing Example
DECLARE @Notify NVARCHAR(300)
EXEC proc_signUpUser N'Phạm Mai Hương', 'huonghuong', 'huong1', '1234567890', '0123456789', N'1A Lý Thường Kiệt phường 4 quận 10', N'Mô Tả', 'thanh@123.com', @Notify OUT
PRINT @Notify
GO

/*
PROCUEDURE: proc_signUpUser
INPUT:	maDatPhong INT
OUTPUT:	True | False 
		+) True: Notify True and Insert a new record into HoaDon table
		+) False: Notify Fasle
*/

drop procedure proc_setBill
go
CREATE PROCEDURE proc_setBill
	@maDatPhong INT,
	@Notify NVARCHAR(100) OUTPUT
AS
BEGIN
	IF(NOT EXISTS(	SELECT DP.maDP
				FROM DatPhong DP
				WHERE DP.maDP = @maDatPhong)
	)
		BEGIN
			SET	@Notify = N'False'
		END
	ELSE
		BEGIN
			SET @Notify = N'True'
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
		END
	
END
GO

-- Testing Example
DECLARE @Notify1 NVARCHAR(100)
EXEC  proc_setBill 1, @Notify1 out
PRINT @Notify1
GO

select *
from HoaDon hd
where hd.maDP = 1
go

	
