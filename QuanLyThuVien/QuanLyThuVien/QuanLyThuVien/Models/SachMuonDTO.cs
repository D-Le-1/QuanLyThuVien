using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Models
{
    public class SachMuonDTO
    {
        public string MaSach { get; set; }
        public string TenSach { get; set; }
        public string TenNxb { get; set; }
        public int NamXb { get; set; }
        public string TenTacGia { get; set; }
        public int SoTrang { get; set; }
        public decimal GiaTri { get; set; }
        public int SoLanMuonTrongThang { get; set; }
    }

}
