using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Models
{
    class PhieuMuonDTO
    {
        public int Id { get; set; }
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public int? MaSv { get; set; }
        public string MaSach { get; set; }
        public string TenSach { get; set; }
        public DateTime? NgayMuon { get; set; }
        public DateTime? NgayTra { get; set; }
        public string TinhTrang { get; set; }
        public string TinhTrangHienTai { get; set; }
    }
}
