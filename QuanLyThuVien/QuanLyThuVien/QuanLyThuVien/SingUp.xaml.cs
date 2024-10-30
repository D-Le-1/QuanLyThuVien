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
    /// Interaction logic for SingUp.xaml
    /// </summary>
    public partial class SingUp : Window
    {
        QLTV1Context db=new QLTV1Context();
        public SingUp()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var query = db.NguoiDungs.FirstOrDefault(t => t.MaSv==int.Parse(MaSV1.Text) || t.Tk.Equals(MaSV1.Text));
            if (query != null)
            {
                MessageBox.Show("Tài khoản đã tồn tại", "Thông báo", MessageBoxButton.OK);
            }
            else
            {
                NguoiDung nd1=new NguoiDung();
                nd1.Mk = matkhau2.Password;
                nd1.Tk = MaSV1.Text;
                nd1.Sdt = SDT1.Text;
                nd1.MaLop=diaChi1.Text;
                nd1.HoTen = hoten1.Text;
                nd1.MaSv =int.Parse(MaSV1.Text);
                TaiKhoan tk1 = new TaiKhoan();
                tk1.Mk = matkhau2.Password;
                tk1.Tk = MaSV1.Text;
                tk1.Loaitk = "nguoidung";
                nd1.SLPhat = 0;
                nd1.TienNo = 0;
                db.TaiKhoans.Add( tk1 );
                db.NguoiDungs.Add(nd1);
                db.SaveChanges();
                MessageBox.Show("Đăng ký thành công","Thông báo",MessageBoxButton.OK);
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
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
    }
}
