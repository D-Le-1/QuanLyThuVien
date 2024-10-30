using System;
using System.Collections.Generic;

#nullable disable

namespace QuanLyThuVien.Models
{
    public partial class PhieuMuon
    {
        public int Id { get; set; }
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public int? MaSv { get; set; }
        public string MaSach { get; set; }
        public DateTime? NgayMuon { get; set; }
        public DateTime? NgayTra { get; set; }
        public string TinhTrang { get; set; }
        public string TinhTrangHienTai { get; set; }

        public virtual Sach MaSachNavigation { get; set; }
        public virtual NguoiDung MaSvNavigation { get; set; }
    }
}
