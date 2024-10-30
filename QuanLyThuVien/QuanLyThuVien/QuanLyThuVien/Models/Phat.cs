using System;
using System.Collections.Generic;

#nullable disable

namespace QuanLyThuVien.Models
{
    public partial class Phat
    {
        public String Idphat { get; set; }
        public int MaSv { get; set; }
        public string TenSinhVien { get; set; }
        public int? SlTraMuon { get; set; }
        public int? SoLanMatSach { get; set; }

        public virtual NguoiDung MaSvNavigation { get; set; }
    }
}
