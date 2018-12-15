
-- STORE TÌM KIẾM THÔNG TIN KHÁCH SẠN THEO GIÁ CẢ VÀ TP-----------
--Input: Tên tp cần tra, giá phòng lớn nhất, giá phòng nhỏ nhất
--Output: hiện thị thông tin các khách sạn có giá phòng nằm trong mức giá cần tra và tp cần tìm
--vidu: usp_timKiemThongTinhKS_giaCa_Tp 'Hồ Chí Minh',200,500

CREATE PROCEDURE usp_timKiemThongTinhKS_giaCa_Tp
    @thanhphotimkiem nvarchar(20),
    @giaCaoNhat int,
    @giaThapNhat int
 
AS
BEGIN
	IF  (NOT EXISTS (SELECT * FROM dbo.KhachSan KH WHERE thanhPho like @thanhphotimkiem)	)--// kiểm tra thành phố đã nhập có tồn tại không
		BEGIN
			RETURN 0;
		END
	ELSE
	IF (NOT EXISTS (SELECT * FROM dbo.KhachSan KH WHERE giaTB > @giaCaoNhat OR giaTB < @giaThapNhat)	)--// kiểm tra giá cần tra có thông tin không
		BEGIN
			RETURN 0;
		END
     ELSE
		 BEGIN
			 SELECT *
			 FROM dbo.KhachSan ks1
			 WHERE ks1.thanhPho like @thanhphotimkiem and ks1.maKS in 
			 (SELECT maKS
			 FROM dbo.KhachSan
			 WHERE giaTB <= @giaCaoNhat and giaTB >= @giaThapNhat )
		 END
END
GO


-- STORE TÌM KIẾM THÔNG TIN KHÁCH SẠN THEO SỐ SAO VÀ TP-----------
--Input: Tên tp cần tra, số sao của khách sạn cần tra
--Output: hiện thị thông tin các khách sạn có số sao và tp cần tìm
--vidu: usp_timKiemThongTinhKS_sao_Tp 'Vũng Tàu', 3

CREATE PROCEDURE usp_timKiemThongTinhKS_sao_Tp
	@thanhPhoTimKiem nvarchar(20),
    @soSaoTimKiem int
  
AS
BEGIN
     IF  (NOT EXISTS (SELECT * FROM dbo.KhachSan WHERE thanhPho LIKE @thanhphotimkiem)	)--// kiểm tra thành phố đã nhập có tồn tại không
		BEGIN
			RETURN 0;
		END
	ELSE
	IF (NOT EXISTS (SELECT * FROM dbo.KhachSan WHERE soSao = @soSaoTimKiem ))--// kiểm tra số sao cần tra có thông tin không
		BEGIN
			RETURN 0;
		END
     ELSE
		 BEGIN
			 SELECT *
			 FROM dbo.KhachSan
			 WHERE thanhPho like @thanhphotimkiem and soSao = @soSaoTimKiem
		 END
END
GO
-- STORE TÌM KIẾM THÔNG TIN HÓA ĐƠN THEO MÃ KHÁCH HÀNG -----------
--Input: mã khách hàng cần tìm kiếm
--Output:Hệ thống hiển thị danh sách các hóa đơn gồm mã hóa đơn, ngày lập và tổng tiền theo các tiêu chí tìm kiếm
--vidu: usp_timKiemTTHD_MaKH 10 

CREATE PROC usp_timKiemTTHD_MaKH
@maKH int
AS
	BEGIN
		IF  (NOT EXISTS (SELECT * FROM dbo.DatPhong WHERE maKH = @maKH))--// kiểm tra mã khách hàng nhập có tồn tại không
		BEGIN
			RETURN 0;
		END
		ELSE
			IF  (NOT EXISTS (SELECT * FROM dbo.DatPhong DP, dbo.HoaDon HD WHERE DP.maDP = HD.maDP))--// kiểm tra mã khách hàng đã nhập có xuất hóa đơn không
				BEGIN
					RETURN 0;
				END
			ELSE
			begin
				SELECT *
				FROM dbo.DatPhong DP, dbo.HoaDon HD 
				WHERE DP.maDP = HD.maDP AND HD.maDP IN (SELECT maDP FROM dbo.DatPhong WHERE maKH = @maKH)
			end
	END
	GO
-- STORE TÌM KIẾM THÔNG TIN HÓA ĐƠN THEO NGÀY LẬP HÓA ĐƠN -----------
--Input: Ngày lập hóa đơn cần tìm kiếm
--Output:Hệ thống hiển thị danh sách các hóa đơn gồm mã hóa đơn, ngày lập và tổng tiền theo các tiêu chí tìm kiếm
--vidu: usp_timKiemTTHD_MaKH '23/11/2018'

CREATE PROC usp_timKiemTTHD_NgayLap
@ngayThanhToan date --hóa đơn được xuất khi đã thanh toán tiền xong
AS
	BEGIN
		IF  (NOT EXISTS (SELECT * FROM dbo.HoaDon WHERE ngayThanhToan = @ngayThanhToan))--// kiểm tra ngày lập hóa đơn có tồn tại không
		BEGIN
			RETURN 0;
		END
		ELSE
		BEGIN
			SELECT *
			FROM dbo.HoaDon
			WHERE ngayThanhToan = @ngayThanhToan
		END
	END
	GO
-- STORE TÌM KIẾM THÔNG TIN HÓA ĐƠN THEO THÀNH TIỀN -----------
--Input: Tổng tiền cần tìm kiếm
--Output:Hệ thống hiển thị danh sách các hóa đơn gồm mã hóa đơn, ngày lập và tổng tiền theo các tiêu chí tìm kiếm
--vidu: usp_timKiemTTHD_ThanhTien 500000

CREATE PROC usp_timKiemTTHD_ThanhTien
@thanhtien bigint 
AS
	BEGIN
		IF  (NOT EXISTS (SELECT * FROM dbo.HoaDon WHERE tongTien = @thanhtien))--// kiểm tra ngày lập hóa đơn có tồn tại không
		BEGIN
			RETURN 0;
		END
		ELSE
		BEGIN
			SELECT *
			FROM dbo.HoaDon
			WHERE tongTien = @thanhtien
		END
	END
	GO

-- STORE TÌM KIẾM THÔNG TIN HÓA ĐƠN THEO MÃ KHÁCH HÀNG, NGÀY LẬP, THÀNH TIỀN -----------
--Input: mã khách hàng, ngày lập, thành tiền cần tìm kiếm
--Output:  Hệ thống hiển thị danh sách các hóa đơn gồm mã hóa đơn, ngày lập và tổng tiền theo các tiêu chí tìm kiếm
--vidu: usp_timKiemTTHD_MaKH 1,'23/11/2018',500000
	
CREATE PROC usp_timKiemTTHD_MaKH_NgayLap_ThanhTien
	@makh INT,
	@ngayThanhToan DATE,
	@thanhTien bigint
	AS
	BEGIN
		IF  (NOT EXISTS (SELECT * FROM dbo.DatPhong WHERE maKH = @maKH))--// kiểm tra mã khách hàng nhập có tồn tại không
		BEGIN
			RETURN 0;
		END
		ELSE
		IF  (NOT EXISTS (SELECT * FROM dbo.HoaDon WHERE ngayThanhToan = @ngayThanhToan))--// kiểm tra ngày lập hóa đơn có tồn tại không
		BEGIN
			RETURN 0;
		END
		ELSE
		IF  (NOT EXISTS (SELECT * FROM dbo.HoaDon WHERE tongTien = @thanhtien))--// kiểm tra ngày lập hóa đơn có tồn tại không
		BEGIN
			RETURN 0;
		END
		ELSE
			BEGIN
				SELECT *
				FROM dbo.DatPhong DP, dbo.HoaDon HD 
				WHERE DP.maDP = HD.maDP AND HD.maDP IN (
				SELECT maDP FROM dbo.DatPhong WHERE maKH = @maKH)
				 AND HD.maHD IN 
				 (SELECT maHD FROM dbo.HoaDon WHERE ngayThanhToan = @ngayThanhToan and tongTien = @thanhtien)
			END
	END
	GO