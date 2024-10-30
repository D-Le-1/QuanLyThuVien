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
    /// Interaction logic for AdministratorScreen1.xaml
    /// </summary>
    public partial class AdministratorScreen1 : Window
    {
        private Button lastClickedButton;
        public AdministratorScreen1()
        {
            InitializeComponent();
            SetDefaultButtonColor();
            Main.Content = new QuanLySach();
        }
        private void SetDefaultButtonColor()
        {
            foreach (UIElement element in YourGrid.Children)
            {
                if (element is Button)
                {
                    ((Button)element).Background = defaultButtonColor;
                }
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

        private Brush selectedButtonColor = (Brush)new BrushConverter().ConvertFrom("#28AEED");
        private Brush defaultButtonColor = Brushes.White;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Đặt lại màu mặc định cho nút được chọn gần đây (nếu có)
            if (lastClickedButton != null)
            {
                lastClickedButton.Background = defaultButtonColor;
            }

            // Lấy trang tương ứng với nút được bấm
            Button clickedButton = (Button)sender;
            string pageTag = clickedButton.Tag.ToString();

            // Thực hiện chuyển đến trang tương ứng
            switch (pageTag)
            {
                case "Page1":
                    Main.Content = new QuanLySach();
                    break;

                case "Page2":
                    Main.Content = new QuanLyNguoiMuon();
                    break;

                case "Page3":
                    Main.Content = new ScreenPhat();
                    break;

                case "Page4":
                    Main.Content = new QuanLySV();
                    break;

                case "Page5":
                    Main.Content = new ThongKe();
                    break;
            }

            // Đặt màu cho nút được chọn mới
            clickedButton.Background = selectedButtonColor;

            // Lưu trạng thái của nút được chọn gần đây
            lastClickedButton = clickedButton;
        }


    }
}
