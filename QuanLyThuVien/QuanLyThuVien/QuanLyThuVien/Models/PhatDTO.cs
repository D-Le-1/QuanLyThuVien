using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Models
{
    internal class PhatDTO
    {
        public string HoTen { get; set; }
        public string Lop { get; set; }
        public int MaSv { get; set; }
        public int? SlTraMuon { get; set; }
        public int? SoLanMatSach { get; set; }
        public decimal TienNo { get; set; }
    }
}
