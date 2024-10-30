using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Models
{
    class SachDTO
    {
        public string MaSach { get; set; }
        public string TenSach { get; set; }
        public string TacGia { get; set; }
        public string NhaXuatBan { get; set; }
        public int? NamXb { get; set; }
        public int? SoTrang { get; set; }
        public decimal GiaTri { get; set; }
        public decimal GiaTriHienTai { get; set; }
        public string TinhTrangSach { get; set; }
    }
}
