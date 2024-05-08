using DD.Functions;
using System.ComponentModel;
using System.Security;
using System.Windows;

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

        public LoginWindow()
        {
            InitializeComponent();

            //Lấy thông tin tài khoản và mật khẩu đã lưu nếu có
            string rememberedUername = Properties.Settings.Default.rememberedUsername;
            rememberedPassword = PasswordHasher.ConvertToSecureString(Properties.Settings.Default.rememberedPassword);

            // Kiểm tra và hiển thị thông tin tài khoản và mật khẩu lên form đăng nhập
            if (!string.IsNullOrEmpty(rememberedUername) && rememberedPassword != null)
            {
                txtUsername.Text = rememberedUername;
                txtPassword.Password = PasswordHasher.SecureStringToString(rememberedPassword);
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
            bool isAuthenticated = Authentication.AuthenticateUser(username, password, "Admin");

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
                this.Hide();
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

        

        private void chkRemember_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.rememberedUsername = string.Empty;
            Properties.Settings.Default.rememberedPassword = string.Empty;
            Properties.Settings.Default.Save();
        }
    }
}