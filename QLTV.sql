create database QLTV1
go
use QLTV1
go

CREATE TABLE TaiKhoan (
  tk NVARCHAR(50) primary key,
  mk NVARCHAR(50),
  loaitk NVARCHAR(10)
);

CREATE TABLE NguoiDung (
  MaSV INT PRIMARY KEY,
  tk NVARCHAR(50),
  mk NVARCHAR(MAX),
  hoTen NVARCHAR(MAX),
  MaLop NVARCHAR(MAX),
  SDT NVARCHAR(10)
  FOREIGN KEY (tk) REFERENCES TaiKhoan(tk)
);

CREATE TABLE TacGia (
  MaTacGia char(50) PRIMARY KEY,
  TenTacGia NVARCHAR(MAX)
);

CREATE TABLE NhaXuatBan (
  MaNXB char(50) PRIMARY KEY,
  TenNXB NVARCHAR(MAX)
);

CREATE TABLE Sach (
  MaSach char(10) PRIMARY KEY,
  TenSach NVARCHAR(MAX),
  MaNXB char(50),
  NamXB INT,
  MaTacGia char(50),
  SoTrang INT,
  FOREIGN KEY (MaNXB) REFERENCES NhaXuatBan(MaNXB),
  FOREIGN KEY (MaTacGia) REFERENCES TacGia(MaTacGia)
);

CREATE TABLE PhieuMuon (
  ID INT PRIMARY KEY,
  HoTen NVARCHAR(MAX),
  DiaChi NVARCHAR(MAX),
  MaSV INT,
  FOREIGN KEY (MaSV) REFERENCES NguoiDung(MaSV),
  MaSach char(10),
  FOREIGN KEY (MaSach) REFERENCES Sach(MaSach),
  NgayMuon DATE,
  NgayTra DATE,
  TinhTrang NVARCHAR(MAX)
);

CREATE TABLE Phat (
  IDPhat INT PRIMARY KEY,
  MaSV INT,
  TenSinhVien NVARCHAR(MAX),
  FOREIGN KEY (MaSV) REFERENCES NguoiDung(MaSV),
  SlTraMuon INT,
  SoLanMatSach INT
);