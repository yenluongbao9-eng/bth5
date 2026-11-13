using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace bth5
{
    public partial class MainWindow : Window
    {
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5050/")
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private async Task GoiApiBaoCao(string endpoint, string tieuDe)
        {
            try
            {
                txtKetQua.Text = "Đang tải dữ liệu từ dịch vụ BTHSOA...";
                var response = await _client.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    txtKetQua.Text = $"=== {tieuDe} ===\n\n{json}";
                }
                else
                {
                    txtKetQua.Text = $"Lỗi từ dịch vụ: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không kết nối được tới BTHSOA! Hãy chắc chắn dịch vụ BTHSOA đang chạy.\nChi tiết: " + ex.Message);
            }
        }

        private async void BtnTongQuan_Click(object sender, RoutedEventArgs e)
        {
            await GoiApiBaoCao("api/Reports/overview", "THỐNG KÊ TỔNG QUAN (TỒN KHO, DOANH THU, LỢI NHUẬN)");
        }

        private async void BtnSanPham_Click(object sender, RoutedEventArgs e)
        {
            await GoiApiBaoCao("api/Reports/products", "BÁO CÁO THEO SẢN PHẨM");
        }

        private async void BtnDonHang_Click(object sender, RoutedEventArgs e)
        {
            await GoiApiBaoCao("api/Reports/orders", "BÁO CÁO THEO ĐƠN HÀNG");
        }
    }
}