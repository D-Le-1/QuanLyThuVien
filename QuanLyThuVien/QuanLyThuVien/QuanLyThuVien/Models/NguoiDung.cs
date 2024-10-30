using System;
using System.Collections.Generic;

#nullable disable

namespace QuanLyThuVien.Models
{
    public partial class NguoiDung
    {
        public NguoiDung()
        {
            Phats = new HashSet<Phat>();
            PhieuMuons = new HashSet<PhieuMuon>();
        }

        public int MaSv { get; set; }
        public string Tk { get; set; }
        public string Mk { get; set; }
        public string HoTen { get; set; }
        public string MaLop { get; set; }
        public string Sdt { get; set; }
        public int SLPhat {  get; set; }
        public Decimal TienNo {  get; set; }

        public virtual TaiKhoan TkNavigation { get; set; }
        public virtual ICollection<Phat> Phats { get; set; }
        public virtual ICollection<PhieuMuon> PhieuMuons { get; set; }
    }
}
