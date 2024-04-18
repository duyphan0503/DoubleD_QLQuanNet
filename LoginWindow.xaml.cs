using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Data;
using DD.Functions;
using System.Security;

namespace DD_QLQuanNet
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        // Khai báo hàng số lưu trữ thông tin đăng nhập
        private const string rememberedUsername = "RememberedUsername";
        
        private SecureString rememberedPassword;
        // chuỗi kết nối đến cơ sở dữ liệu
        private const string connectionString = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=DD_QLQuanNet;Integrated Security=True;Encrypt=False";
        public LoginWindow()
        {
            InitializeComponent();

            //Lấy thông tin tài khoản và mật khẩu đã lưu nếu có
            string rememberedUername = Properties.Settings.Default.rememberedUsername;
            rememberedPassword = ConvertToSecureString(Properties.Settings.Default.rememberedPassword);

            // Kiểm tra và hiển thị thông tin tài khoản và mật khẩu lên form đăng nhập
            if (!string.IsNullOrEmpty(rememberedUername) && rememberedPassword != null)
            {
                txtUsername.Text = rememberedUername;
                txtPassword.Password = SecureStringToString(rememberedPassword);
                chkRemember.IsChecked = true;
            }
        }
        // Xử lý sự kiện đăng nhập
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            bool rememberPassword = chkRemember.IsChecked ?? false;

            // Xác thực người dùng
            bool isAuthenticated = AuthenticateUser(username, password);

            // Nếu xác thực thành công
            if (isAuthenticated)
            {
                // Nếu người dùng chọn ghi nhớ đăng nhập
                if (rememberPassword)
                {
                    // Lưu thông tin tài khoản và mật khẩu
                    Properties.Settings.Default.rememberedUsername = username;
                    Properties.Settings.Default.rememberedPassword = password;
                    Properties.Settings.Default.Save();
                }
                MessageBox.Show("Login successful!");
                this.Hide();
            }
        }
        // Hàm xác thực người dùng
        private bool AuthenticateUser(string username, string password)
        {
            // Mở kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Thực hiện truy vấn để lấy mật khẩu đã mã hóa từ cơ sở dữ liệu dự trên tên người dùng
                using (SqlCommand command = new SqlCommand("SELECT PasswordHash FROM Users WHERE Username = @username AND Role IN ('admin', 'staff')", connection))
                {
                    // Thêm tham số @username vào câu truy vấn
                    command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;

                    // Thực thi câu truy vấn và đọc dữ liệu
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Nếu có kết quả truy vấn
                        if (reader.Read())
                        {
                            // Lấy mật khẩu đã mã hóa từ dữ liệu đọc được
                            string passwordHashInDb = reader.GetString(0);

                            // Xác thực mật khẩu
                            if (VerifyPassword(password, passwordHashInDb))
                            {
                                // Xác thực thành công trả về true
                                return true;
                            }
                            else
                            {
                                MessageBox.Show("Incorrect password. Please try again.");
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Username does not exist. Please try again.");
                            return false;
                        }
                    }
                }
            }
        }
        // Hàm xác thực mật khẩu
        private bool VerifyPassword(string password, string passwordHashInDb)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Mã hóa mật khẩu
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                string passwordHash = BitConverter.ToString(bytes).Replace("-", "").ToLower();

                // So sánh mật khẩu đã mã hóa với mật khẩu đã mã hóa trong cơ sở dữ liệu
                return passwordHash == passwordHashInDb;
            }
        }
        // Xử lý sự kiện đóng form
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            Functions.HandleClosing(e);
            base.OnClosing(e);
        }
        private SecureString ConvertToSecureString(string password)
        {
            SecureString securePassword = new SecureString();
            foreach (char c in password)
            {
                securePassword.AppendChar(c);
            }
            securePassword.MakeReadOnly();
            return securePassword;
        }

        private string SecureStringToString(SecureString secureString)
        {
            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(secureString);
            try
            {
                return System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }
        }

        private void chkRemember_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.rememberedUsername = string.Empty;
            Properties.Settings.Default.rememberedPassword = string.Empty;
            Properties.Settings.Default.Save();
        }
    }
}
