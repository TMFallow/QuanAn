/*
	Phát Triển Phần Mềm Và Ứng Dụng Thông Minh
Đồ Án: Quản Lý Cửa Hàng Bán Đồ Ăn Nhanh
Thành Viên: Phan Thanh Bình - 2001190031
*/

CREATE DATABASE BAN_QUAN_AN_NHANH

GO 

USE BAN_QUAN_AN_NHANH

GO

-------------------------------------------------------------Tạo Các Bảng-----------------------------------------------------------

CREATE TABLE NHANVIEN   -- Bảng Nhân Viên Bao Gồm Mã nhân viên, họ tên, số điện thoại và chức vụ (Thu ngân - 1, Quản lý - 2, Nhân viên bộ phận kho - 3)
( 
	MANV INT NOT NULL PRIMARY KEY,
	TENNV NVARCHAR(50),
	SDT NVARCHAR(15),
	CHUCVU NVARCHAR(50)
)

GO

CREATE TABLE TAIKHOAN
(
	TENDN NVARCHAR(50) NOT NULL PRIMARY KEY,
	MATKHAU NVARCHAR(50),
	MANV INT NOT NULL,
	CONSTRAINT FK_TAIKHOAN FOREIGN KEY (MANV) REFERENCES NHANVIEN(MANV),
)

GO

CREATE TABLE KHACHHANG
(
	MAKH INT NOT NULL PRIMARY KEY,
	TENKH NVARCHAR(50),
	GIOITINH INT,
	DIACHI NVARCHAR(100),
	SDT NVARCHAR(15)
)

GO

CREATE TABLE MONAN --Bảng món ăn bao gồm các cột mã các món ăn, tên các món ăn, đơn vị tính từng loại, số lượng còn, giá của các món.
(
	MAMONAN INT NOT NULL PRIMARY KEY,
	TENMONAN NVARCHAR(50),
	LOAI NVARCHAR(50),
	DVT NVARCHAR(50),
	SLHIENCO INT,
	GIA INT
)

update MONAN
set SLHIENCO = SLHIENCO - 1 where MAMONAN = 1

select * from MonAn where MAMONAN = 1
GO

CREATE TABLE NGUYENLIEU
(
	MANGUYENLIEU INT NOT NULL PRIMARY KEY,
	TENNGUYENLIEU NVARCHAR(50),
	SOLUONG INT, 
	DONVITINH NVARCHAR(50),
	GIA INT
)

select DONVITINH from NGUYENLIEU group by DONVITINH

update NGUYENLIEU
set SOLUONG = 5
where TENNGUYENLIEU = N''

select * from NGUYENLIEU 

delete NGUYENLIEU Where MANGUYENLIEU = 81 
GO


CREATE TABLE CONGTHUCCHEBIENMONAN
(
	MAMONAN INT NOT NULL,
	MANGUYENLIEU INT NOT NULL,
	SOLUONGCHEBIEN INT,
	SLNGUYENLIEU INT
	CONSTRAINT PK_MONAN_NGUYENLIEU PRIMARY KEY (MAMONAN, MANGUYENLIEU)
	CONSTRAINT FK_MONAN FOREIGN KEY (MAMONAN) REFERENCES MONAN(MAMONAN),
	CONSTRAINT FK_NGUYENLIEU FOREIGN KEY (MANGUYENLIEU) REFERENCES NGUYENLIEU(MANGUYENLIEU) 
)
select * from CONGTHUCCHEBIENMONAN
select * from NGUYENLIEU
select COUNT(*) from NGUYENLIEU 


GO 
 
CREATE TABLE HOADON -- Bảng hóa đơn cho biết thông tin đơn hàng đã bán gồm mã hóa đơn, mã nhân viên, ngày thanh toán và thành tiền.
(
	MAHOADON INT NOT NULL PRIMARY KEY,
	MANV INT NOT NULL,
	MAKH INT NOT NULL,
	NGAYTHANHTOAN DATE,
	THANHTIEN INT
	CONSTRAINT FK_HOADON FOREIGN KEY (MANV) REFERENCES NHANVIEN(MANV),
	CONSTRAINT FK_KH FOREIGN KEY (MAKH) REFERENCES KHACHHANG(MAKH)
)

select * from HOADON

select * from KHACHHANG
select * from CHITIETHOADON


GO

CREATE TABLE CHITIETHOADON -- Chi tiết hóa đơn chứa các danh sách các món ăn đã được đặt thông qua mã các món ăn
(
	MAHOADON INT,
	MAMONAN INT,
	SOLUONG INT
	CONSTRAINT PK_CTHD PRIMARY KEY (MAHOADON, MAMONAN)
	CONSTRAINT FK_CTHD FOREIGN KEY (MAHOADON) REFERENCES HOADON(MAHOADON),
	CONSTRAINT FK_CTMONAN FOREIGN KEY (MAMONAN) REFERENCES MONAN(MAMONAN)
)

GO

CREATE TABLE NHAPMONAN -- Bảng nhập món ăn bao gồm các cột mã đơn nhập, nhân viên, ngày nhập và thành tiền
(
	MADONNHAP INT NOT NULL PRIMARY KEY,
	MANV INT NOT NULL,
	NGAYNHAP DATE,
	THANHTIEN INT
	CONSTRAINT FK_NHAPMONAN FOREIGN KEY (MANV) REFERENCES NHANVIEN(MANV)
)

GO
select * from NHAPMONAN

select GIA from NguyenLieu where TENNGUYENLIEU = 'Gà'

CREATE TABLE CHITIETNHAPMONAN -- Bảng chi tiết đơn nhập gồm mã đơn nhập và danh sách các đơn nhập các món ăn theo mã
(
	MADONNHAP INT NOT NULL,
	MANGUYENLIEU INT NOT NULL,
	SOLUONG INT
	CONSTRAINT PK_CTNHAPMONAN PRIMARY KEY (MADONNHAP, MANGUYENLIEU)
	CONSTRAINT FK_CTNHAPMADON FOREIGN KEY (MADONNHAP) REFERENCES NHAPMONAN(MADONNHAP),
	CONSTRAINT FK_CTNHAPMONAN FOREIGN KEY (MANGUYENLIEU) REFERENCES NGUYENLIEU(MANGUYENLIEU)
)

select * from MONAN where MAMONAN = 1

---------------------------------------------Thêm Dữ Liệu Vào Các Bảng-----------------------------------------------------
GO

INSERT INTO NHANVIEN
VALUES(1, N'Phan Thanh Bình', N'0978523655', N'Admin'),
	  (2, N'Nguyễn Thụy Xuân Hạnh', N'03652647892', N'Admin'),
	  (3, N'Phạm Quốc Thái', N'09632568952', N'Nhân Viên')

GO

select ChucVu from NHANVIEN where TENNV = N'Phan Thanh Bình'

select TENNV from NHANVIEN AS NV, TAIKHOAN AS TK WHERE NV.MANV = TK.MANV AND TENDN = N'ThanhBinh'


INSERT INTO TAIKHOAN
VALUES('ThanhBinh', '12345', 1),
	  ('XuanHanh', '11111', 2),
	  ('ThaiQuoc', '22222', 3)

GO

INSERT INTO KHACHHANG
VALUES(1, N'Ngô Quốc Việt', 1, N'TP.Hồ Chí Minh', '0985658521'),
	  (2, N'Phạm Thái Anh', 1, N'Bến Tre', '0698512546'),
      (3, N'Nguyễn Ngọc Ánh', 0, N'TP.Hồ Chí Minh', '0698523665'),
	  (4, N'Lâm Nguyên Ngọc', 0, N'Cần Thơ', '0236563985'),
	  (5, N'Phan Thái Bình', 1, N'Long An', '0639856663'),
	  (0, null, null, null, null)

delete KHACHHANG
where MAKH = 0

GO

INSERT INTO MONAN
VALUES(1, N'Chân Gà Sốt Cay', N'Đồ Ăn', N'Cái', 50, 20000),
	  (2, N'Gà Lắc Phô Mai', N'Đồ Ăn', N'Cái', 20, 25000),
	  (3, N'Đùi Gà Chiên Giòn', N'Đồ Ăn', N'Cái', 33, 15000),
	  (4, N'Hamburger Bò', N'Đồ Ăn', N'Cái', 10, 45000),
	  (5, N'Hamburger Phô Mai', N'Đồ Ăn', N'Cái', 15, 45000),
	  (6, N'Pizza Hải Sản', N'Đồ Ăn', N'Cái', 11, 70000),
	  (7, N'Pizza Thập Cẩm', N'Đồ Ăn', N'Cái', 22, 80000),
	  (8, N'Pizza Phô Mai', N'Đồ Ăn', N'Cái', 21, 70000),
	  (9, N'Trà Tắc', N'Thức Uống', N'Ly', 30, 10000),
	  (10, N'Trà Đào', N'Thức Uống', N'Ly', 30, 10000)

GO

INSERT INTO NGUYENLIEU
VALUES (1, N'Gà', 17, N'Cái', 5000),
	   (2, N'Phô Mai', 10, N'Cái', 5000),
	   (3, N'Bột', 10, N'Cái', 5000),
	   (4, N'Thịt Bò', 10, N'Cái', 5000),
	   (5, N'Tôm', 10, N'Cái', 5000),
	   (6, N'Mực', 10, N'Cái', 5000),
	   (7, N'Tắc', 10, N'Cái', 5000),
	   (8, N'Mức Đào', 20, N'Cái', 5000)

select count(*) from NGUYENLIEU WHERE TENNGUYENLIEU = N'bột'

GO

INSERT INTO CONGTHUCCHEBIENMONAN
VALUES (1, 1, 1, 1),
	   (2, 1, 1, 1),
	   (2, 2, 1, 1),
	   (3, 1, 1, 1),
	   (4, 3, 1, 1),
	   (4, 4, 1, 1),
	   (5, 2, 1, 1),
	   (6, 3, 1, 1),
	   (6, 5, 1, 1),
	   (6, 6, 1, 1),
	   (7, 2, 1, 1),
	   (7, 3, 1, 1),
	   (7, 4, 1, 1),
	   (8, 2, 1, 1),
	   (9, 7, 1 ,1),
	   (10, 8, 1, 1)


INSERT INTO HOADON
VALUES(1, 1, 2, N'05-02-2022', 60000),
	  (2, 2, 0, N'05-01-2022', 90000),
	  (3, 1, 0, N'04-30-2022', 50000),
	  (4, 1, 1, N'05-05-2022', 205000),
	  (5, 3, 3, N'06-02-2022', 70000)

GO

select SUM(THANHTIEN) from HOADON WHERE NGAYTHANHTOAN between '2022-06-02' and '2022-08-02'

select MAHOADON, NGAYTHANHTOAN, THANHTIEN from HOADON WHERE NGAYTHANHTOAN between '2022-06-02' and '2022-08-02'

select * from CHITIETHOADON where MAHOADON = 2

INSERT INTO CHITIETHOADON
VALUES(1, 1, 2),
	  (1, 9, 2),
	  (2, 4, 1),
	  (2, 5, 1),
	  (3, 2, 5),
	  (4, 5, 1),
	  (4, 6, 1),
	  (4, 7, 1),
	  (5, 10, 7)

GO

SELECT * FROM MONAN WHERE SLHIENCO < 10


INSERT INTO NHAPMONAN
VALUES(1, 1, N'04-30-2022', 120000),
	  (2, 1, N'05-04-2022', 200000),
	  (3, 1, N'06-05-2022', 120000)


GO

select * MONAN where MaMonAn = 1

select * from NHANVIEN WHERE MANV = 1

select * from NHAPMONAN

select SUM(THANHTIEN) from NHAPMONAN WHERE NGAYNHAP between '2022-06-02' and '2022-08-02'

delete NHAPMONAN
where MADONNHAP = 13

select MADONNHAP, TENNGUYENLIEU, CT.SOLUONG from CHITIETNHAPMONAN AS CT, NGUYENLIEU AS NL WHERE CT.MANGUYENLIEU = NL.MANGUYENLIEU AND MADONNHAP = 1

select * from NGUYENLIEU

select MANGUYENLIEU from NGUYENLIEU where TENNGUYENLIEU = N'Hộp Trà Sữa'

select MANGUYENLIEU from NGUYENLIEU where TENNGUYENLIEU = N'Mực'


UPDATE NHAPMONAN
SET THANHTIEN = 75000
FROM CHITIETNHAPMONAN AS CT, NHAPMONAN AS MN
WHERE MN.MADONNHAP = 5

INSERT INTO CHITIETNHAPMONAN
VALUES(1, 1, 10),
	  (1, 2, 15),
	  (2, 3, 10),
	  (2, 4, 10),
	  (3, 5, 15)

SELECT * FROM NHANVIEN

SELECT MAHOADON, TENMONAN, SOLUONG FROM CHITIETHOADON AS CT, MONAN AS MA WHERE CT.MAMONAN = MA.MAMONAN

SELECT * FROM NGUYENLIEU WHERE SOLUONG < 10

select * from CONGTHUCCHEBIENMONAN

select * from NHAPMONAN

SELECT MADONNHAP, NGAYNHAP, THANHTIEN FROM NHAPMONAN

select MAMONAN from MONAN where TENMONAN = N'Chân Gà Sốt Cay'

SELECT SUM(THANHTIEN) FROM HOADON

SELECT SUM(THANHTIEN) FROM NHAPMONAN




