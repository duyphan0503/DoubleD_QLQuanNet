<<<<<<< HEAD
﻿using DD.Functions;
using System.Windows;

namespace DD_QLQuanNet.resources.models
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void btnChangePassword_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnChangePassword_Ok_Click(object sender, RoutedEventArgs e)
        {
            PasswordHasher.ChangePassword(txtUsername.Text, txtNewPassword.Password, txtConfirmPassword.Password);
            this.Close();
        }
    }
=======
﻿using DD.Functions;
using System.Windows;

namespace DD_QLQuanNet.resources.models
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void btnChangePassword_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnChangePassword_Ok_Click(object sender, RoutedEventArgs e)
        {
            PasswordHasher.ChangePassword(txtUsername.Text, txtNewPassword.Password, txtConfirmPassword.Password);
            this.Close();
        }
    }
>>>>>>> master
}