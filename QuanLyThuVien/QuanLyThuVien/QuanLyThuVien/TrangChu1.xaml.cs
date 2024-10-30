using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    
    public partial class TrangChu1 : Window
    {
        public string TaiKhoan;
        public ObservableCollection<Sach> SachList { get; set; }
        public TrangChu1(String TaiKhoan1)
        {
            TaiKhoan = TaiKhoan1;
            InitializeComponent();
            using (QLTV1Context db = new QLTV1Context())
            {
                var sachDTOs = (
                from sach in db.Saches
                join TacGium in db.TacGia on sach.MaTacGia equals TacGium.MaTacGia
                join NhaXuatBan in db.NhaXuatBans on sach.MaNxb equals NhaXuatBan.MaNxb
                select new SachDTO
                {
                    MaSach = sach.MaSach,
                    TenSach = sach.TenSach,
                    TacGia = TacGium.TenTacGia,
                    NhaXuatBan = NhaXuatBan.TenNxb,
                    NamXb = sach.NamXb,
                    SoTrang = sach.SoTrang,
                    GiaTri = sach.GiaTri
                }
                ).ToList();

                // Gán danh sách DTO làm nguồn dữ liệu cho DataGrid
                datagril.ItemsSource = sachDTOs;
            }
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void datagril_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PhieuMuon phieuMuon=new PhieuMuon(TaiKhoan);
            phieuMuon.ShowDialog();
            Close();
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            SachDaMuon sachDaMuon = new SachDaMuon(TaiKhoan);
            sachDaMuon.ShowDialog();
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
