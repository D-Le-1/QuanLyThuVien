using System;
using System.Collections.Generic;

#nullable disable

namespace QuanLyThuVien.Models
{
    public partial class TacGium
    {
        public TacGium()
        {
            Saches = new HashSet<Sach>();
        }

        public string MaTacGia { get; set; }
        public string TenTacGia { get; set; }

        public virtual ICollection<Sach> Saches { get; set; }
    }
}
