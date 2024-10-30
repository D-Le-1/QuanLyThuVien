using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyThuVien
{
    /// <summary>
    /// Interaction logic for ThongKe.xaml
    /// </summary>
    public partial class ThongKe : Page
    {
        QLTV1Context db=new QLTV1Context();
        public void LoadDataGrid()
        {
            using (QLTV1Context db = new QLTV1Context())
            {
                var query = from phieuMuon in db.PhieuMuons
                            join sach in db.Saches on phieuMuon.MaSach equals sach.MaSach
                            join nxb in db.NhaXuatBans on sach.MaNxb equals nxb.MaNxb
                            join tacGia in db.TacGia on sach.MaTacGia equals tacGia.MaTacGia
                            group phieuMuon by new
                            {
                                sach.MaSach,
                                sach.TenSach,
                                TenNxb = nxb.TenNxb,
                                sach.NamXb,
                                TenTacGia = tacGia.TenTacGia,
                                sach.SoTrang,
                                sach.GiaTri
                            } into grouped
                            select new
                            {
                                grouped.Key.MaSach,
                                grouped.Key.TenSach,
                                grouped.Key.TenNxb,
                                grouped.Key.NamXb,
                                grouped.Key.TenTacGia,
                                grouped.Key.SoTrang,
                                grouped.Key.GiaTri,
                                SoLanDuocMuonTrongThang = GetSoLanDuocMuonTrongThang(db, grouped.Key.MaSach)
                                // Add other properties if needed
                            };

                datagril.ItemsSource = query.ToList();
            }
        }


        public static int GetSoLanDuocMuonTrongThang(QLTV1Context db, string maSach)
        {
            using (QLTV1Context newDb = new QLTV1Context())
            {
                DateTime today = DateTime.Now;
                DateTime firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                int soLanDuocMuon = newDb.PhieuMuons
                    .Count(pm => pm.MaSach == maSach && pm.NgayMuon >= firstDayOfMonth && pm.NgayMuon <= lastDayOfMonth);

                return soLanDuocMuon;
            }
        }



        public ThongKe()
        {
            InitializeComponent();
            LoadDataGrid();
        }
    }
}
