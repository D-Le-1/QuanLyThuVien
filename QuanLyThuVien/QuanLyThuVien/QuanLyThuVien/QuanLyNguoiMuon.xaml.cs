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
using QuanLyThuVien.Models;

namespace QuanLyThuVien
{

    /// <summary>
    /// Interaction logic for QuanLyNguoiMuon.xaml
    /// </summary>
    public partial class QuanLyNguoiMuon : Page
    {
        private void HienThi()
        {
            using (QLTV1Context db = new QLTV1Context())
            {
                var query = (
                        from phieuMuon in db.PhieuMuons
                        join sach in db.Saches on phieuMuon.MaSach equals sach.MaSach
                        join nguoiDung in db.NguoiDungs on phieuMuon.MaSv equals nguoiDung.MaSv
                        select new Models.PhieuMuonDTO
                        {
                            Id = phieuMuon.Id,
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
                datagril.ItemsSource = query;
            }
        }
        public void UPdate()
        {
            using (QLTV1Context db = new QLTV1Context())
            {
                // Lấy tất cả các mục từ bảng PhieuMuon
                var danhSachPhieuMuon = db.PhieuMuons.ToList();

                foreach (var phieuMuon in danhSachPhieuMuon)
                {
                    switch (phieuMuon.TinhTrang)
                    {
                        case "Đang mượn" when (phieuMuon.NgayTra != null && (DateTime.Now - phieuMuon.NgayTra.Value).Days >= 1):
                            phieuMuon.TinhTrangHienTai = "QuaHan";
                            break;

                        case "Mất sách":
                            phieuMuon.TinhTrangHienTai = "Mất sách";
                            break;

                        case "Đã trả" when phieuMuon.TinhTrangHienTai == "Mất sách":
                            // Không thay đổi TinhTrangHienTai
                            break;
                        case "Đã trả" when phieuMuon.TinhTrangHienTai == "QuaHan":
                            break;
                        default:
                            phieuMuon.TinhTrangHienTai = "DangMuon";
                            break;
                    }
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                db.SaveChanges();

                // Hiển thị danh sách hoặc thực hiện các bước khác ở đây
            }
        }


        public QuanLyNguoiMuon()
        {
            InitializeComponent();
            UPdate();
            HienThi();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (QLTV1Context db = new QLTV1Context())
            {
                var query = (
                        from phieuMuon in db.PhieuMuons
                        join sach in db.Saches on phieuMuon.MaSach equals sach.MaSach
                        join nguoiDung in db.NguoiDungs on phieuMuon.MaSv equals nguoiDung.MaSv
                        select new Models.PhieuMuonDTO
                        {
                            Id = phieuMuon.Id,
                            HoTen = phieuMuon.HoTen,
                            DiaChi = phieuMuon.DiaChi,
                            MaSv = phieuMuon.MaSv,
                            TenSach = sach.TenSach,
                            MaSach = phieuMuon.MaSach,
                            NgayMuon = phieuMuon.NgayMuon,
                            NgayTra = phieuMuon.NgayTra,
                            TinhTrang=phieuMuon.TinhTrang
                        }
                    ).OrderBy(PhieuMuonDTO => PhieuMuonDTO.MaSv)
                    .ToList();
                datagril.ItemsSource = query;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (QLTV1Context db = new QLTV1Context())
            {
                if (int.TryParse(MaSV1.Text, out int maSVCanTim))
                {
                    var SinhvienCanTim = (
                        from phieuMuon in db.PhieuMuons
                        join sach in db.Saches on phieuMuon.MaSach equals sach.MaSach
                        join nguoiDung in db.NguoiDungs on phieuMuon.MaSv equals nguoiDung.MaSv
                        where phieuMuon.MaSv == maSVCanTim
                        select new Models.PhieuMuonDTO
                        {
                            Id = phieuMuon.Id+1,
                            HoTen = phieuMuon.HoTen,
                            DiaChi = phieuMuon.DiaChi,
                            MaSv = phieuMuon.MaSv,
                            TenSach = sach.TenSach,
                            MaSach = phieuMuon.MaSach,
                            NgayMuon = phieuMuon.NgayMuon,
                            NgayTra = phieuMuon.NgayTra,
                            TinhTrang=phieuMuon.TinhTrang
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            HienThi();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            TinhTrangMuon tinhTrangMuon = new TinhTrangMuon();
            tinhTrangMuon.Show();
        }
    }
}
