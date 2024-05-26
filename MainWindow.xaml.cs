<<<<<<< HEAD
﻿using DD.Functions;
using DD_QLQuanNet.data;
using DD_QLQuanNet.resources.models;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DD_QLQuanNet
{
    /// <summary>
    /// Interaction logic for Window.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Tạo và hiển thị form đăng nhập như một modal
            this.Show();
            LoginWindow loginWindow = new LoginWindow();
            bool? dialogResult = loginWindow.ShowDialog();
            CheckUserRoles();
            DataGrid_User("Member");
            LoadComputers();
            LoadTopUpHistory_Click();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Functions.HandleClosing(e);
            base.OnClosing(e);
        }
        public void DataGrid_User(string role)
        {
            using (var db = new QLQuanNetContext())
            {
                var users = db.Users.Select(u => new
                {
                    u.Username,
                    u.Role,
                    u.Status,
                    u.Balance
                }).Where(u => u.Role == role).ToList();

                dgAccounts.ItemsSource = users;
            }
            dgAccounts.Items.Refresh();
        }

        private void btnAddAccount_Click(object sender, RoutedEventArgs e)
        {
            UserManagement userManagement = new UserManagement(this, dgAccounts);
            userManagement.UpdateLabel("Add User Account");
            userManagement.btnAddUser.Visibility = Visibility.Visible;
            userManagement.btnUpdateUser.Visibility = Visibility.Hidden;
            userManagement.txtUsername.IsEnabled = true;
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
                var computers = from station in db.Stations
                                join user in db.Users on station.User_ID equals user.User_ID into joinedUser
                                from subUser in joinedUser.DefaultIfEmpty()
                                select new
                                {
                                    station.Station_ID,
                                    station.Station_Name,
                                    station.Status,
                                    station.Type,
                                    station.Price_Per_Hour,
                                    Balance = subUser != null ? (decimal?)subUser.Balance : null,
                                    Username = subUser != null ? subUser.Username : "" // Nếu subUser không null thì lấy Username, ngược lại để trống
                                };

                dgComputers.ItemsSource = computers.ToList();
            }
        }

        private void ChangeAdminPassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePassword changePassword = new ChangePassword();
            changePassword.ShowDialog();
        }

        private void btnMember_Click(object sender, RoutedEventArgs e)
        {
            DataGrid_User("Member");
        }

        private void btnStaff_Click(object sender, RoutedEventArgs e)
        {
            DataGrid_User("Staff");
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            DataGrid_User("Admin");
        }

        private void btnDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem có dòng được chọn trong DataGrid không
            if (dgAccounts.SelectedItem != null)
            {
                // Lấy dòng được chọn từ DataGrid
                var selectedUser = (dynamic)dgAccounts.SelectedItem;

                // Lấy Username của người dùng được chọn
                string username = selectedUser.Username;

                // Xác nhận xóa bằng hộp thoại xác nhận
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete user " + username + "?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Kết nối đến cơ sở dữ liệu và xóa người dùng
                        using (var db = new QLQuanNetContext())
                        {
                            var customerDelete = db.Customers.FirstOrDefault(c => c.User.Username == username);
                            if (customerDelete != null)
                            {
                                db.Customers.Remove(customerDelete);
                            }
                            var userToDelete = db.Users.FirstOrDefault(u => u.Username == username);
                            if (userToDelete != null)
                            {
                                db.Users.Remove(userToDelete);
                                db.SaveChanges();

                                // Cập nhật lại DataGrid sau khi xóa
                                DataGrid_User("Member"); // Gọi lại phương thức hiển thị dữ liệu
                                MessageBox.Show("User deleted successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show("User not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEditAccount_Click(object sender, RoutedEventArgs e)
        {
            UserManagement userManagement = new UserManagement(this, dgAccounts);
            userManagement.UpdateLabel("Edit User Account");
            userManagement.btnUpdateUser.Visibility = Visibility.Visible;
            userManagement.btnAddUser.Visibility = Visibility.Hidden;
            UserAccoount.LoadData(dgAccounts, userManagement.txtUsername, userManagement.txtPassword, userManagement.cbxRole, userManagement.chkActive, userManagement.txtFullname, userManagement.dpBirthdate, userManagement.cbxGender, userManagement.txtEmail, userManagement.txtPhone, userManagement.txtAddress);
            userManagement.ShowDialog();
        }

        private void dgAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnEditAccount.IsEnabled = true;
        }

        private void TopUp_click(object sender, RoutedEventArgs e)
        {
            var selectedRow = (dynamic)dgAccounts.SelectedItem;
            string username = selectedRow.Username;
            if (selectedRow != null)
            {
                TopUps topUp = new TopUps(username);
                topUp.ShowDialog();
            }
            else
                MessageBox.Show("Please select a user to top up", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void dgAccounts_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.OriginalSource is DependencyObject source)
            {
                DataGridRow row = FindVisualParent<DataGridRow>(source);
                if (row != null)
                {
                    row.IsSelected = true;
                    ContextMenu contextMenu = (ContextMenu)dgAccounts.FindResource("RowContextMenu");
                    contextMenu.IsOpen = true;
                }
            }
        }

        private T FindVisualParent<T>(DependencyObject obj) where T : DependencyObject
        {
            while (obj != null)
            {
                if (obj is T parent)
                {
                    return parent;
                }
                obj = VisualTreeHelper.GetParent(obj);
            }
            return null;
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow orderWindow = new OrderWindow();
            orderWindow.ShowDialog();
        }

        private void OnToggleMaintenance_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnDeleteStation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnEditStation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnAddStation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnStationManagement_Click(object sender, RoutedEventArgs e)
        {
            StationManagement stationManagement = new StationManagement();
            stationManagement.ShowDialog();
        }
        private void CheckUserRoles()
        {
            string username = UserAccoount.Username;
            if (UserAccoount.IsUserAdmin(username))
            { 
                btnAddStation.IsEnabled = true;
                btnEditStation.IsEnabled = true;
                btnDeleteStation.IsEnabled = true;
            }
            else
            {
                btnAddStation.IsEnabled = false;
                btnEditStation.IsEnabled = false;
                btnDeleteStation.IsEnabled = false;
            }
        }
        private void LoadTopUpHistory_Click()
        {
            using var db = new QLQuanNetContext();
            var topUpHistory = db.TopUps.Select(t => new
            {
                t.TopUp_ID,
                t.User.Username,
                t.Amount,
                t.TopUp_Date,
            }).ToList();
            dgHistoryTopUp.ItemsSource = topUpHistory;
        }
    }
=======
﻿using DD.Functions;
using DD_QLQuanNet.data;
using DD_QLQuanNet.resources.models;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DD_QLQuanNet
{
    /// <summary>
    /// Interaction logic for Window.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Tạo và hiển thị form đăng nhập như một modal
            this.Show();
            LoginWindow loginWindow = new LoginWindow();
            bool? dialogResult = loginWindow.ShowDialog();
            DataGrid_User("Member");
            LoadComputers();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Functions.HandleClosing(e);
            base.OnClosing(e);
        }
        public void DataGrid_User(string role)
        {
            using (var db = new QLQuanNetContext())
            {
                var users = db.Users.Select(u => new
                {
                    u.Username,
                    u.Role,
                    u.Status,
                    u.Balance
                }).Where(u => u.Role == role).ToList();

                dgAccounts.ItemsSource = users;
            }
            dgAccounts.Items.Refresh();
        }

        private void btnAddAccount_Click(object sender, RoutedEventArgs e)
        {
            UserManagement userManagement = new UserManagement(this, dgAccounts);
            userManagement.UpdateLabel("Add User Account");
            userManagement.btnAddUser.Visibility = Visibility.Visible;
            userManagement.btnUpdateUser.Visibility = Visibility.Hidden;
            userManagement.txtUsername.IsEnabled = true;
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
            ChangePassword changePassword = new ChangePassword();
            changePassword.ShowDialog();
        }

        private void btnMember_Click(object sender, RoutedEventArgs e)
        {
            DataGrid_User("Member");
        }

        private void btnStaff_Click(object sender, RoutedEventArgs e)
        {
            DataGrid_User("Staff");
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            DataGrid_User("Admin");
        }

        private void btnDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem có dòng được chọn trong DataGrid không
            if (dgAccounts.SelectedItem != null)
            {
                // Lấy dòng được chọn từ DataGrid
                var selectedUser = (dynamic)dgAccounts.SelectedItem;

                // Lấy Username của người dùng được chọn
                string username = selectedUser.Username;

                // Xác nhận xóa bằng hộp thoại xác nhận
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete user " + username + "?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Kết nối đến cơ sở dữ liệu và xóa người dùng
                        using (var db = new QLQuanNetContext())
                        {
                            var customerDelete = db.Customers.FirstOrDefault(c => c.User.Username == username);
                            if (customerDelete != null)
                            {
                                db.Customers.Remove(customerDelete);
                            }
                            var userToDelete = db.Users.FirstOrDefault(u => u.Username == username);
                            if (userToDelete != null)
                            {
                                db.Users.Remove(userToDelete);
                                db.SaveChanges();

                                // Cập nhật lại DataGrid sau khi xóa
                                DataGrid_User("Member"); // Gọi lại phương thức hiển thị dữ liệu
                                MessageBox.Show("User deleted successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show("User not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEditAccount_Click(object sender, RoutedEventArgs e)
        {
            UserManagement userManagement = new UserManagement(this, dgAccounts);
            userManagement.UpdateLabel("Edit User Account");
            userManagement.btnUpdateUser.Visibility = Visibility.Visible;
            userManagement.btnAddUser.Visibility = Visibility.Hidden;
            UserAccoount.LoadData(dgAccounts, userManagement.txtUsername, userManagement.txtPassword, userManagement.cbxRole, userManagement.chkActive, userManagement.txtFullname, userManagement.dpBirthdate, userManagement.cbxGender, userManagement.txtEmail, userManagement.txtPhone, userManagement.txtAddress);
            userManagement.ShowDialog();
        }

        private void dgAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnEditAccount.IsEnabled = true;
        }

        private void TopUp_click(object sender, RoutedEventArgs e)
        {
            var selectedRow = (dynamic)dgAccounts.SelectedItem;
            string username = selectedRow.Username;
            if (selectedRow != null)
            {
                TopUps topUp = new TopUps(username);
                topUp.ShowDialog();
            }
            else
                MessageBox.Show("Please select a user to top up", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void dgAccounts_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.OriginalSource is DependencyObject source)
            {
                DataGridRow row = FindVisualParent<DataGridRow>(source);
                if (row != null)
                {
                    row.IsSelected = true;
                    ContextMenu contextMenu = (ContextMenu)dgAccounts.FindResource("RowContextMenu");
                    contextMenu.IsOpen = true;
                }
            }
        }

        private T FindVisualParent<T>(DependencyObject obj) where T : DependencyObject
        {
            while (obj != null)
            {
                if (obj is T parent)
                {
                    return parent;
                }
                obj = VisualTreeHelper.GetParent(obj);
            }
            return null;
        }
    }
>>>>>>> 2c066f95b899f6c361ceb9c7b666c4dca578392a
}