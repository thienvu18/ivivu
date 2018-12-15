/*
PROCEDURE: proc_BookRoom
INPUT: MaPhong(INT), MaKhachHang(INT), NgayBatDau(DATE), NgayTraPhong(DATE), MoTa(NVARCHAR(100))
*/
CREATE PROCEDURE [dbo].[proc_BookRoom] (@MaPhong INT, @MaKhachHang INT, @NgayBatDau DATE, @NgayTraPhong DATE, @MoTa NVARCHAR(100))
AS
BEGIN
	DECLARE @err_message nvarchar(255);
	DECLARE @MaLoaiPhong INT, @DonGia BIGINT;

	IF EXISTS (SELECT 1
				FROM TrangThaiPhong
				WHERE TrangThaiPhong.maPhong = @MaPhong
						AND TrangThaiPhong.ngay = @NgayBatDau
						AND (TrangThaiPhong.tinhTrang LIKE N'Đang sử dụng' OR TrangThaiPhong.tinhTrang LIKE N'Đang bảo trì')
				)
		BEGIN
			SET @err_message = 'Room id ' + CAST(@MaPhong as varchar(10)) + ' is not available.';
			RAISERROR (@err_message, 11,1);
			RETURN -1;
		END
	ELSE
		BEGIN
			SELECT @MaLoaiPhong = LoaiPhong.maLoaiPhong, @DonGia = LoaiPhong.donGia
			FROM LoaiPhong JOIN Phong ON loaiPhong.maLoaiPhong = Phong.loaiPhong
			WHERE Phong.maPhong = @MaPhong

			INSERT INTO DatPhong(maLoaiPhong, maKH, ngayBatDau, ngayTraPhong, ngayDat, donGia, moTa, tinhTrang) VALUES (@MaLoaiPhong, @MaKhachHang, @NgayBatDau, @NgayTraPhong, GETDATE(), @DonGia, @MoTa, N'Chưa xác nhận');
			
			BEGIN TRANSACTION
				UPDATE TrangThaiPhong SET tinhTrang = N'Đang sử dụng' WHERE maPhong = @MaPhong AND ngay = @NgayBatDau;
				IF @@ROWCOUNT = 0
				BEGIN
					INSERT INTO TrangThaiPhong (maPhong, ngay, tinhTrang) VALUES(@MaPhong, @NgayBatDau, N'Đang sử dụng');
				END
			COMMIT TRANSACTION
		END
	RETURN 0;
END
GO