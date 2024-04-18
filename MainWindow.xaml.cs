using DD.Functions;
using DD_QLQuanNet.data;
using System.ComponentModel;
using System.Data;
using System.Windows;

namespace DD_QLQuanNet
{
    /// <summary>
    /// Interaction logic for Window.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly QLQuanNetContext db;

        public MainWindow()
        {
            InitializeComponent();
            //Tạo và hiển thị form đăng nhập như một modal
            this.Show();
            LoginWindow loginWindow = new LoginWindow();
            bool? dialogResult = loginWindow.ShowDialog();
            DataGrid_User();
            LoadComputers();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Functions.HandleClosing(e);
            base.OnClosing(e);
        }

        public void DataGrid_User()
        {
            using (var db = new QLQuanNetContext())
            {
                var users = db.Users.Select(u => new
                {
                    u.Username,
                    u.Role
                }).Where(u => u.Role == "staff").ToList();

                dgAccounts.ItemsSource = users;
            }
        }

        private void btnAddAccount_Click(object sender, RoutedEventArgs e)
        {
            UserManagement userManagement = new UserManagement();
            userManagement.ShowDialog();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }

        private void LoadComputers()
        {
            using (var db = new QLQuanNetContext())
            {
                var stations = db.Stations.Select(s => new
                {
                    s.Station_Name,
                    s.Status,
                    s.Type
                }).ToList();

                dgComputers.ItemsSource = stations;
            }
        }

        private void ChangeAdminPassword_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}