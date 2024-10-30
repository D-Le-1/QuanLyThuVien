using System;
using System.Collections.Generic;

#nullable disable

namespace QuanLyThuVien.Models
{
    public partial class Sach
    {
        public Sach()
        {
            PhieuMuons = new HashSet<PhieuMuon>();
        }

        public string MaSach { get; set; }
        public string TenSach { get; set; }
        public string MaNxb { get; set; }
        public int? NamXb { get; set; }
        public string MaTacGia { get; set; }
        public int? SoTrang { get; set; }
        public decimal GiaTri {  get; set; }
        public decimal GiaTriHienTai { get; set; }
        public string  TinhTrangSach { get; set; }
        public virtual NhaXuatBan MaNxbNavigation { get; set; }
        public virtual TacGium MaTacGiaNavigation { get; set; }
        public virtual ICollection<PhieuMuon> PhieuMuons { get; set; }
    }
}
