using QuanLyThuVien.Models;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string TaiKhoan { get; set; }
        QLTV1Context db = new QLTV1Context();
        string loaitk = "";
        public MainWindow()
        {
            InitializeComponent();
        }
        public string MaSV { get; private set; }
        private void SetMaSV(string maSV)
        {
            MaSV = maSV;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SingUp singUp = new SingUp();
            singUp.ShowDialog();
            Close();
        }
        public bool kiemTraAdmin()
        {
            string taiKhoan = taiKhoan1.Text;
            string matKhau = matKhau1.Password;
            Console.WriteLine(matKhau);

            // Kết nối đến cơ sở dữ liệu và truy vấn bảng Người Dùng
            using (QLTV1Context db = new QLTV1Context())
            {
                // Kiểm tra xem có bản ghi nào có tài khoản và mật khẩu như trên không
                bool taiKhoanTonTai = db.TaiKhoans.Any(nd => nd.Tk == taiKhoan && nd.Mk == matKhau);

                if (taiKhoanTonTai)
                {
                    var taiKhoan12 = db.TaiKhoans.FirstOrDefault(tk => tk.Tk == taiKhoan && tk.Mk == matKhau);
                    loaitk = taiKhoan12.Loaitk;
                    Console.WriteLine("Tài khoản và mật khẩu tồn tại trong bảng Người Dùng.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Tài khoản và mật khẩu không tồn tại trong bảng Người Dùng.");
                    return false;
                }
            }
        }
        public static class GlobalVariables
        {
            public static string MaSV { get; set; }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (kiemTraAdmin() == true && loaitk.Equals("admin"))
            {
                AdministratorScreen1 adminScreen = new AdministratorScreen1();
                adminScreen.Show();
                Close();
            }
            else if (kiemTraAdmin() == true && loaitk.Equals("nguoidung"))
            {
                TrangChu1 trangChu1 = new TrangChu1(TaiKhoan);
                trangChu1.TaiKhoan = taiKhoan1.Text;
                trangChu1.Show();
                Close();
            }
            else
            {
                Console.WriteLine(matKhau1.ToString().Trim());
                MessageBox.Show("Tài khoản hoặc mật khẩu bị sai", "Thông báo");
            }

        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton==MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Border_MouseLeft(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount==2)
            {
                this.WindowState=WindowState.Normal;
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