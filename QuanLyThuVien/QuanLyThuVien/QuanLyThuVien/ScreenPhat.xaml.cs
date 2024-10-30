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
    /// Interaction logic for ScreenPhat.xaml
    /// </summary>
    public partial class ScreenPhat : Page
    {
        private void CapNhatBangPhatChoTatCa()
        {
            using (QLTV1Context db = new QLTV1Context())
            {
                var danhSachNguoiDung = db.NguoiDungs.ToList();

                foreach (var nguoiDung in danhSachNguoiDung)
                {
                    int slTraMuon = 0;
                    int soLanMatSach = 0;

                    // Lấy danh sách các phiếu mượn của người dùng
                    var danhSachPhieuMuon = db.PhieuMuons.Where(p => p.MaSv == nguoiDung.MaSv).ToList();

                    foreach (var phieuMuon in danhSachPhieuMuon)
                    {
                        // Kiểm tra điều kiện để xác định SlTraMuon và SoLanMatSach
                        if (phieuMuon.TinhTrangHienTai == "QuaHan")
                        {
                            slTraMuon += 1; // Nếu quá hạn thì đánh dấu là đã trả muộn
                        }

                        if (phieuMuon.TinhTrangHienTai == "Mất sách")
                        {
                            soLanMatSach += 1; // Nếu mất sách thì đánh dấu là đã mất sách
                        }
                    }

                    // Kiểm tra xem người dùng có tồn tại trong bảng Phat hay chưa
                    var phatNguoiDung = db.Phats.FirstOrDefault(p => p.MaSv == nguoiDung.MaSv);

                    if (phatNguoiDung == null)
                    {
                        // Nếu chưa có, thì thêm mới vào bảng Phat
                        Phat phatMoi = new Phat
                        {
                            Idphat = nguoiDung.Tk,
                            MaSv = nguoiDung.MaSv,
                            TenSinhVien = nguoiDung.HoTen,
                            SlTraMuon = slTraMuon,
                            SoLanMatSach = soLanMatSach
                        };

                        db.Phats.Add(phatMoi);
                    }
                    else
                    {
                        // Nếu đã tồn tại, cập nhật giá trị
                        phatNguoiDung.SlTraMuon = slTraMuon;
                        phatNguoiDung.SoLanMatSach = soLanMatSach;
                    }
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                db.SaveChanges();
            }
        }

        private void CapNhatTienNoChoTatCa()
        {
            using (QLTV1Context db = new QLTV1Context())
            {
                var danhSachNguoiDung = db.NguoiDungs.ToList();

                foreach (var nguoiDung in danhSachNguoiDung)
                {
                    decimal tienNo = 0;

                    // Lấy danh sách các phiếu mượn của người dùng
                    var danhSachPhieuMuon = db.PhieuMuons.Where(p => p.MaSv == nguoiDung.MaSv && p.TinhTrang == "Mất sách").ToList();

                    foreach (var phieuMuon in danhSachPhieuMuon)
                    {
                        // Lấy giá trị hiện tại của sách đang mượn
                        var sach = db.Saches.FirstOrDefault(s => s.MaSach == phieuMuon.MaSach);
                        if (sach != null)
                        {
                            // Cập nhật tiền nợ dựa trên giá trị hiện tại của sách
                            tienNo += sach.GiaTriHienTai;
                        }
                    }

                    // Cập nhật tiền nợ cho người dùng
                    nguoiDung.TienNo = tienNo;
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                db.SaveChanges();
            }
        }

        private void HienThiDanhSachPhat()
        {
            using (QLTV1Context db = new QLTV1Context())
            {
                var danhSachPhat = (
                    from phat in db.Phats
                    join nguoiDung in db.NguoiDungs on phat.MaSv equals nguoiDung.MaSv
                    select new PhatDTO
                    {
                        HoTen = nguoiDung.HoTen,
                        Lop = nguoiDung.MaLop,
                        MaSv = nguoiDung.MaSv,
                        SlTraMuon = phat.SlTraMuon,
                        SoLanMatSach = phat.SoLanMatSach,
                        TienNo = nguoiDung.TienNo
                    }
                ).ToList();

                // Gán danh sách vào ItemsSource của DataGrid
                datagril.ItemsSource = danhSachPhat;
            }
        }

        public ScreenPhat()
        {
            InitializeComponent();
            CapNhatBangPhatChoTatCa();
            CapNhatTienNoChoTatCa();
            HienThiDanhSachPhat();
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            using (QLTV1Context db = new QLTV1Context())
            {
                try
                {
                    // Lấy danh sách phạt dựa vào Mã Sinh viên nhập từ TextBox
                    int maSinhVien = int.Parse(MaSV1.Text);
                    var danhSachPhat = (
                        from phat in db.Phats
                        join nguoiDung in db.NguoiDungs on phat.MaSv equals nguoiDung.MaSv
                        where maSinhVien == phat.MaSv
                        select new PhatDTO
                        {
                            HoTen = nguoiDung.HoTen,
                            Lop = nguoiDung.MaLop,
                            MaSv = nguoiDung.MaSv,
                            SlTraMuon = phat.SlTraMuon,
                            SoLanMatSach = phat.SoLanMatSach,
                            TienNo = nguoiDung.TienNo
                        }
                    ).ToList();

                    // Gán danh sách vào ItemsSource của DataGrid
                    datagril.ItemsSource = danhSachPhat;
                }
                catch (Exception ex)
                {
                    // Xử lý ngoại lệ nếu có
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            using(QLTV1Context db=new QLTV1Context())
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
    }
}
