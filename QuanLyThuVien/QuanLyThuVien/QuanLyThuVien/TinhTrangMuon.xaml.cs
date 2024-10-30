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
    /// Interaction logic for TinhTrangMuon.xaml
    /// </summary>
    public partial class TinhTrangMuon : Window
    {
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

        private void LoadPhieuMuonIds()
        {
            using (QLTV1Context db = new QLTV1Context())
            {
                var phieuMuonIds = db.PhieuMuons.Select(pm => pm.Id).ToList();

                // Đổ dữ liệu vào ComboBox
                maID.ItemsSource = phieuMuonIds;
            }
        }
        private void SetupTinhTrangComboBox()
        {
            List<string> tinhTrangList = new List<string>
            {
                "Đang mượn",
                "Đã trả",
                "Mất sách"
            };
            tinhTrang.ItemsSource = tinhTrangList;
        }
        public TinhTrangMuon()
        {
            InitializeComponent();
            LoadPhieuMuonIds();
            SetupTinhTrangComboBox();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {
                // Lấy ID từ ComboBox
                int selectedId = Convert.ToInt32(maID.SelectedItem);

                // Lấy giá trị TinhTrang từ ComboBox
                string selectedTinhTrang = tinhTrang.SelectedItem as string;

                // Kiểm tra xem có ID và TinhTrang được chọn không
                if (selectedId > 0 && !string.IsNullOrEmpty(selectedTinhTrang))
                {
                    using (QLTV1Context db = new QLTV1Context())
                    {
                        // Lấy đối tượng PhieuMuon cần cập nhật từ ID
                        var phieuMuonToUpdate = db.PhieuMuons.FirstOrDefault(pm => pm.Id == selectedId);

                        if (phieuMuonToUpdate != null)
                        {
                            // Thực hiện cập nhật trạng thái tại đây
                            phieuMuonToUpdate.TinhTrang = selectedTinhTrang;

                            // Lưu các thay đổi vào cơ sở dữ liệu
                            db.SaveChanges();

                            MessageBox.Show("Cập nhật trạng thái thành công", "Thông báo", MessageBoxButton.OK);
                            UPdate();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy phiếu mượn với ID đã chọn", "Thông báo", MessageBoxButton.OK);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn ID và Tình trạng trước khi cập nhật", "Thông báo", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cập nhật trạng thái không thành công. Lỗi: " + ex.Message, "Thông báo", MessageBoxButton.OK);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
