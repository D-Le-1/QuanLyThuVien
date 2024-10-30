using System;
using System.Collections.Generic;

#nullable disable

namespace QuanLyThuVien.Models
{
    public partial class TaiKhoan
    {
        public TaiKhoan()
        {
            NguoiDungs = new HashSet<NguoiDung>();
        }

        public string Tk { get; set; }
        public string Mk { get; set; }
        public string Loaitk { get; set; }

        public virtual ICollection<NguoiDung> NguoiDungs { get; set; }
    }
}
