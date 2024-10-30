using System;
using System.Collections.Generic;

#nullable disable

namespace QuanLyThuVien.Models
{
    public partial class NhaXuatBan
    {
        public NhaXuatBan()
        {
            Saches = new HashSet<Sach>();
        }

        public string MaNxb { get; set; }
        public string TenNxb { get; set; }

        public virtual ICollection<Sach> Saches { get; set; }
    }
}
