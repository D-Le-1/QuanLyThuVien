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
    /// Interaction logic for QuanLySach.xaml
    /// </summary>
    public partial class QuanLySach : Page
    {
        QLTV1Context db=new QLTV1Context();

        private void HienThiDL()
        {
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


                datagril.ItemsSource = sachDTOs;
            }
        }
        public void update()
        {
            using (QLTV1Context db = new QLTV1Context())
            {
                var danhSach=db.Saches.ToList();

                foreach (var sach in danhSach)
                {
                    // Tính toán số năm đã xuất bản
                    int namHienTai = DateTime.Now.Year;
                    int soNamDaXuatBan = namHienTai - sach.NamXb.GetValueOrDefault();

                    // Cập nhật tình trạng sách
                    if (soNamDaXuatBan < 3)
                    {
                        sach.TinhTrangSach = "Moi";
                        sach.GiaTriHienTai = 0.9m * sach.GiaTri; // 90% giá trị cho sách mới
                    }
                    else
                    {
                        sach.TinhTrangSach = "Cu";
                        sach.GiaTriHienTai = 0.6m * sach.GiaTri; // 60% giá trị cho sách cũ
                    }
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                db.SaveChanges();
            }

        }
        public QuanLySach()
        {
            update();
            InitializeComponent();
            HienThiDL();
        }
        private void HienThiTG()
        {
            var query =from t in db.TacGia
                       select t;
            TenTacGia.ItemsSource = query.ToList();
            TenTacGia.DisplayMemberPath = "TenTacGia";
            TenTacGia.SelectedIndex = 0;
            TenTacGia.SelectedValuePath = "MaTacGia";
        }
        private void HienThiNXB()
        {
            var query = from t in db.NhaXuatBans
                        select t;
            tenNXB.ItemsSource = query.ToList();
            tenNXB.DisplayMemberPath = "TenNxb";
            tenNXB.SelectedIndex = 0;
            tenNXB.SelectedValuePath = "MaNxb";
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HienThiNXB();
            HienThiTG();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (QLTV1Context db = new QLTV1Context())
            {
                string maSachCanXoa = maSach1.Text; 

                // Tìm sách cần xóa
                var sachCanXoa = db.Saches.FirstOrDefault(sach => sach.MaSach == maSachCanXoa);

                // Kiểm tra xem sách có tồn tại hay không
                if (sachCanXoa != null)
                {
                    MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa cuốn sách có mã " + maSachCanXoa + " không?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        // Xóa sách từ bảng Saches
                        db.Saches.Remove(sachCanXoa);
                        db.SaveChanges();
                        MessageBox.Show("Đã xóa sách trong hệ thống", "Thông báo", MessageBoxButton.OK);
                    }
                }
                else
                {
                    // Xử lý khi sách không tồn tại
                    MessageBox.Show("Sách không có trong hệ thống", "Thông báo", MessageBoxButton.OK);
                }
            }

        }

        private void datagril_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            HienThiDL();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
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
                        GiaTri= sach.GiaTri
                    }
                )
                .OrderBy(sachDTO => sachDTO.SoTrang) // Sắp xếp tăng dần theo số trang
                .ToList();

                // Gán danh sách DTO làm nguồn dữ liệu cho DataGrid
                datagril.ItemsSource = sachDTOs;
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            bool sachTonTai = db.Saches.Any(sach => sach.MaSach == maSach1.Text);

            if (!sachTonTai)
            {
                Sach sach = new Sach();
                sach.MaSach = maSach1.Text;
                sach.TenSach = tenSach1.Text;
                sach.MaNxb = ((NhaXuatBan)tenNXB.SelectedItem).MaNxb;
                sach.NamXb = int.Parse(soluong.Text);
                sach.MaTacGia = ((TacGium)TenTacGia.SelectedItem).MaTacGia;
                sach.SoTrang = int.Parse(soTrang.Text);
                sach.GiaTri = decimal.Parse(GiaTri.Text);

                // Tính toán giá trị mới của sách dựa trên các điều kiện
                int namHienTai = DateTime.Now.Year;
                int namXuatBan = int.Parse(soluong.Text);
                int soNamDaXuatBan = namHienTai - namXuatBan;

                if (soNamDaXuatBan < 3)
                {
                    sach.TinhTrangSach = "Moi";
                    sach.GiaTriHienTai = 0.9m * decimal.Parse(GiaTri.Text);
                }
                else
                {
                    sach.TinhTrangSach = "Cu";
                    sach.GiaTriHienTai = 0.6m * decimal.Parse(GiaTri.Text); 
                }

                db.Saches.Add(sach);
                db.SaveChanges();

                MessageBox.Show("Thêm sách thành công", "Thông báo", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Sách đã có trong hệ thống", "Thông báo", MessageBoxButton.OK);
            }
        }


        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            bool sachTonTai = db.Saches.Any(sach => sach.MaSach == maSach1.Text);
            if (sachTonTai==false)
            {
                MessageBox.Show("Sách chưa có trong hệ thống", "Thông báo", MessageBoxButton.OK);
            }
            else
            {
                var spSua=db.Saches.SingleOrDefault(sach=>sach.MaSach==maSach1.Text);
                spSua.TenSach=tenSach1.Text;
                spSua.MaTacGia=((TacGium)TenTacGia.SelectedItem).MaTacGia;
                spSua.SoTrang=int.Parse(soTrang.Text);
                spSua.NamXb=int.Parse(soluong.Text);
                spSua.MaNxb=((NhaXuatBan)tenNXB.SelectedItem).MaNxb;
                db.SaveChanges();
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            using (QLTV1Context db = new QLTV1Context())
            {
                string maSachCanTimKiem = maSach1.Text; 

                // Tìm kiếm sách theo mã sách
                var sachTimKiem = (
                    from sach in db.Saches
                    join TacGium in db.TacGia on sach.MaTacGia equals TacGium.MaTacGia
                    join NhaXuatBan in db.NhaXuatBans on sach.MaNxb equals NhaXuatBan.MaNxb
                    where sach.MaSach == maSachCanTimKiem
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
                ).FirstOrDefault();

                // Kiểm tra xem sách có tồn tại hay không
                if (sachTimKiem != null)
                {
                    // Hiển thị thông tin sách trong DataGrid
                    datagril.ItemsSource = new List<SachDTO> { sachTimKiem };
                }
                else
                {
                    MessageBox.Show("Sách chưa có trong hệ thống", "Thông báo", MessageBoxButton.OK);
                }
            }

        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            AdministratorScreen1 administratorScreen=new AdministratorScreen1(); 
            administratorScreen.Show();
        }
    }
}
