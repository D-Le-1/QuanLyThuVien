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
using System.Windows.Shapes;

namespace QuanLyThuVien
{
    /// <summary>
    /// Interaction logic for SachDaMuon.xaml
    /// </summary>
    public partial class SachDaMuon : Window
    {
        public SachDaMuon(string maSV)
        {
            InitializeComponent();
            
            using (QLTV1Context db = new QLTV1Context())
            {
                var tienNoDecimal = (from nguoiDung in db.NguoiDungs
                                     where maSV == nguoiDung.Tk
                                     select nguoiDung.TienNo).FirstOrDefault();

                double tienNoDouble = Convert.ToDouble(tienNoDecimal);

                tienNo1.Text = "Số tiền nợ: " + tienNoDouble.ToString().Trim() + " VNĐ";

                if (int.TryParse(maSV, out int maSVCanTim))
                {
                    var SinhvienCanTim = (
                        from phieuMuon in db.PhieuMuons
                        join sach in db.Saches on phieuMuon.MaSach equals sach.MaSach
                        join nguoiDung in db.NguoiDungs on phieuMuon.MaSv equals nguoiDung.MaSv
                        where phieuMuon.MaSv == maSVCanTim
                        select new Models.PhieuMuonDTO
                            {
                                Id = phieuMuon.Id + 1,
                                HoTen = phieuMuon.HoTen,
                                DiaChi = phieuMuon.DiaChi,
                                MaSv = phieuMuon.MaSv,
                                MaSach = phieuMuon.MaSach,
                                TenSach = sach.TenSach,  
                                NgayMuon = phieuMuon.NgayMuon,
                                NgayTra = phieuMuon.NgayTra,
                                TinhTrang = phieuMuon.TinhTrang
                            }
                        ).ToList();

                    // Kiểm tra xem sinh viên có tồn tại hay không
                    if (SinhvienCanTim != null)
                    {
                        // Hiển thị thông tin sinh viên trong DataGrid
                        datagril.ItemsSource = SinhvienCanTim;
                    }
                    else
                    {
                        MessageBox.Show("Sinh viên không tồn tại trong hệ thống", "Thông báo", MessageBoxButton.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Mã sinh viên không hợp lệ", "Thông báo", MessageBoxButton.OK);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Border_MouseLeft(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
