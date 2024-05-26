<<<<<<< HEAD
﻿using DD.Functions;
using DD_QLQuanNet.resources.models;
using System.Windows;
using System.Windows.Controls;
using Window = System.Windows.Window;

namespace DD_QLQuanNet
{
    /// <summary>
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : Window
    {
        private MainWindow mainWindow;
        private DataGrid dgAccounts;
        public UserManagement(MainWindow mainWindow, DataGrid dgAccounts)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.dgAccounts = dgAccounts;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            bool isSuccess = UserAccoount.AddUser(txtUsername, txtPassword, cbxRole, chkActive, txtFullname, dpBirthdate, cbxGender, txtEmail, txtPhone, txtAddress);
            if(isSuccess)
            {
                this.Close();
                mainWindow.DataGrid_User("Member");

            }
        }

        public void UpdateLabel(string text)
        {
            lbUserManager.Content = text;
        }

        private void btnEnterMount_Click(object sender, RoutedEventArgs e)
        {
            TopUps topUp = new TopUps(txtUsername.Text);
            topUp.ShowDialog();
        }

        private void txtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPassword.Password = string.Empty;
        }

        private void txtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Password))
            {
            }
        }

        private void btnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            bool isSuccess = UserAccoount.UpdateUser(dgAccounts, txtUsername, txtPassword, cbxRole, chkActive, txtFullname, dpBirthdate, cbxGender, txtEmail, txtPhone, txtAddress);
            if (isSuccess)
            {
                this.Close();
                mainWindow.DataGrid_User("Member");
            }
        }
    }
=======
﻿using DD.Functions;
using DD_QLQuanNet.resources.models;
using System.Windows;
using System.Windows.Controls;
using Window = System.Windows.Window;

namespace DD_QLQuanNet
{
    /// <summary>
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : Window
    {
        private MainWindow mainWindow;
        private DataGrid dgAccounts;
        public UserManagement(MainWindow mainWindow, DataGrid dgAccounts)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.dgAccounts = dgAccounts;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            bool isSuccess = UserAccoount.AddUser(txtUsername, txtPassword, cbxRole, chkActive, txtFullname, dpBirthdate, cbxGender, txtEmail, txtPhone, txtAddress);
            if(isSuccess)
            {
                this.Close();
                mainWindow.DataGrid_User("Member");

            }
        }

        public void UpdateLabel(string text)
        {
            lbUserManager.Content = text;
        }

        private void btnEnterMount_Click(object sender, RoutedEventArgs e)
        {
            TopUps topUp = new TopUps(txtUsername.Text);
            topUp.ShowDialog();
        }

        private void txtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPassword.Password = string.Empty;
        }

        private void txtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Password))
            {
            }
        }

        private void btnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            bool isSuccess = UserAccoount.UpdateUser(dgAccounts, txtUsername, txtPassword, cbxRole, chkActive, txtFullname, dpBirthdate, cbxGender, txtEmail, txtPhone, txtAddress);
            if (isSuccess)
            {
                this.Close();
                mainWindow.DataGrid_User("Member");
            }
        }
    }
>>>>>>> 2c066f95b899f6c361ceb9c7b666c4dca578392a
}