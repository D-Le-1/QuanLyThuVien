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
    /// Interaction logic for QuanLySV.xaml
    /// </summary>
    public partial class QuanLySV : Page
    {
        private void HienThi()
        {
            using (QLTV1Context db = new QLTV1Context())
            {
                var danhSachSinhVien = db.NguoiDungs.ToList();
                datagril.ItemsSource = danhSachSinhVien;
            }
        }
        public QuanLySV()
        {
            InitializeComponent();
            HienThi();
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            using (QLTV1Context db = new QLTV1Context())
            {
                var taiKhoan = db.TaiKhoans.FirstOrDefault(tk => tk.Tk == MaSV1.Text);

                if (taiKhoan != null)
                {
                    // Cập nhật trạng thái loaitk thành "BiCam"
                    taiKhoan.Loaitk = "BiCam";

                    // Lưu thay đổi vào cơ sở dữ liệu
                    db.SaveChanges();

                    MessageBox.Show("Đã cấm thành công", "Thông báo", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy tài khoản cho mã sinh viên này", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            QLTV1Context db=new QLTV1Context();
            var filteredStudents = db.NguoiDungs.Where(student => student.Tk == MaSV1.Text).ToList();

            datagril.ItemsSource = filteredStudents;
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            HienThi();
        }
        private void ResetPassword(string username)
        {
            using (QLTV1Context db = new QLTV1Context())
            {
                // Assuming TaiKhoan is the entity representing user accounts
                var user = db.TaiKhoans.FirstOrDefault(u => u.Tk == username);

                if (user != null)
                {
                    // Set the new password
                    user.Mk = "123456";

                    // Save changes to the database
                    db.SaveChanges();

                    MessageBox.Show("Password reset successfully.", "Success", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("User not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            ResetPassword(MaSV1.Text);
        }
    }
}
