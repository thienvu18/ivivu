GO
CREATE PROCEDURE proc_LoginCustomer @tenDangNhap CHAR(50), @matKhau CHAR(20)
AS
BEGIN
	IF (EXISTS(SELECT * FROM KhachHang WHERE tenDangNhap = @tenDangNhap AND matKhau = @matKhau))
		RETURN 1
		ELSE RETURN 0
END

-- Test
-- DECLARE @test int
-- EXEC @test = proc_LoginEmployee @tenDangNhap = 'liem123', -- char(50)
--                                     @matKhau = '123456'      -- char(20)
-- PRINT @test

GO
CREATE PROCEDURE proc_LoginEmployee @tenDangNhap CHAR(50), @matKhau CHAR(20)
AS
BEGIN
	IF (EXISTS(SELECT * FROM NhanVien WHERE tenDangNhap = @tenDangNhap AND matKhau = @matKhau))
		RETURN 1
		ELSE RETURN 0
END

-- Test
-- DECLARE @test int
-- EXEC @test = proc_LoginEmployee @tenDangNhap = 'liem123', -- char(50)
--                                     @matKhau = '123456'      -- char(20)
-- PRINT @test