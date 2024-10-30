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
    /// Interaction logic for PhieuMuon.xaml
    /// </summary>
    public partial class PhieuMuon : Window
    {
        QLTV1Context db=new QLTV1Context();
        
        private void HienThiSach()
        {
            var query = from t in db.Saches
                        select t;

            maSach.ItemsSource = query.ToList();
            maSach.DisplayMemberPath = "TenSach";
            maSach.SelectedValuePath = "MaSach";
            maSach.SelectedIndex = 0;
        }

        public PhieuMuon(string maSV)
        {
            InitializeComponent();
            ngayMuon.SelectedDate = DateTime.Now;
            ngayTra.SelectedDate = DateTime.Now;
            var nguoiDung = db.NguoiDungs.FirstOrDefault(tk => tk.MaSv == int.Parse(maSV));
            hoten2.Text = nguoiDung.HoTen.ToString();
            diaChi2.Text =nguoiDung.MaLop.ToString();
            maSV2.Text=nguoiDung.Tk;
            HienThiSach();
        }

        internal static object Where(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            ngayMuon.SelectedDate = DateTime.Now;
            ngayTra.SelectedDate = DateTime.Now;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TaiKhoan = maSV2.Text;
            TrangChu1 trangChu1 = new TrangChu1(TaiKhoan);  
            trangChu1.Show();
            Close();
        }
        public string TaiKhoan { get; set; }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                int? maxId = db.PhieuMuons.Max(m => (int?)m.Id);

                Models.PhieuMuon phieuMuon = new Models.PhieuMuon();
                phieuMuon.Id = (maxId.HasValue ? maxId.Value + 1 : 1);
                phieuMuon.HoTen = hoten2.Text;
                phieuMuon.DiaChi = diaChi2.Text;
                phieuMuon.MaSach = (string)maSach.SelectedValue;
                phieuMuon.MaSv = int.Parse(maSV2.Text);
                phieuMuon.NgayMuon = ngayMuon.SelectedDate;
                phieuMuon.NgayTra = ngayTra.SelectedDate;
                TaiKhoan = phieuMuon.MaSv.ToString();
                Console.WriteLine(TaiKhoan);
                // Kiểm tra điều kiện để xác định TinhTrang
                phieuMuon.TinhTrang ="Đang mượn";

                db.PhieuMuons.Add(phieuMuon);
                db.SaveChanges();

                MessageBox.Show("Đăng ký mượn thành công", "Thông báo", MessageBoxButton.OK);

                TrangChu1 trangChu1 = new TrangChu1(TaiKhoan);
                trangChu1.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đăng ký mượn không thành công", "Thông báo", MessageBoxButton.OK);
            }
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
