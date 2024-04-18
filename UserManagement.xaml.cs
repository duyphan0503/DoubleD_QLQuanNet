using DD_QLQuanNet.data;
using Microsoft.VisualBasic.ApplicationServices;
using System.Windows;

namespace DD_QLQuanNet
{
    /// <summary>
    /// Interaction logic for UserAccoounts.xaml
    /// </summary>
    public partial class UserManagement : Window
    {
        public UserManagement()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            //if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Password))
            //{
            //    MessageBox.Show("Please enter username and password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            //using (var db = new QLQuanNetContext())
            //{
            //    var user = new User
            //    {
            //        Username = txtUsername.Text,
            //        Password = txtPassword.Password,
            //        Role = cbxRole.Text,

            //    };
            //    db.Users.Add(user);
            //    db.SaveChanges();

            //    var customer = new Customer
            //    {
            //        Customer_ID = user.User_ID,
            //        Full_Name = txtFullname.Text,
            //        Birthdate = dpBirthdate.SelectedDate,
            //        Gender = cbxGender.Text,
            //        Phone = txtPhone.Text,
            //        Email = txtEmail.Text,
            //        Address = txtAddress.Text,
            //        User_ID = user.User_ID
            //    };
            //    db.Customers.Add(customer);
            //    db.SaveChanges();
            //}
            //MessageBox.Show("User added successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}