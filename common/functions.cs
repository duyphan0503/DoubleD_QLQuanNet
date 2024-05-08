using DD_QLQuanNet;
using DD_QLQuanNet.data;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography;     //Thư viện cho các thuật toán mã hóa
using System.Text;      // Thư viện sử lý chuỗi (cho StringBuilder)
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Converters;
using static DD_QLQuanNet.data.QLQuanNetContext;

namespace DD.Functions
{
    public static class Functions
    {
        public static bool isAppExiting = false;

        public static void HandleClosing(CancelEventArgs e)
        {
            if (!isAppExiting)
            {
                var result = MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    isAppExiting = true;
                    Application.Current.Shutdown();
                }
            }
        }
        
        public static void TopUps(string username, decimal txtMoney, string txtNotes)
        {
            using (var db = new QLQuanNetContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username);
                try
                {
                    if(user != null)
                    {
                        var topUp = new TopUp();
                        {
                            topUp.TopUp_Date = DateTime.Now;
                            topUp.Amount = txtMoney; 
                            topUp.Description = txtNotes;
                            topUp.User_ID = user.User_ID;
                        };
                        db.TopUps.Add(topUp);

                        user.Balance += topUp.Amount;

                        db.SaveChanges();
                        MessageBox.Show("Top up successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }    
        }
    }

    public static class PasswordHasher
    {
        // Hàm mã hóa mật khẩu
        public static string HashPassword(string password)
        {
            // Sử dụng thuật toán SHA256 để mã hóa
            using (SHA256 sha256 = SHA256.Create())
            {
                // Mật khẩu được mã hóa thành 1 mảng byte
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Chuyển mảng byte thành chuỗi Hex để lưu trữ
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    // Chuyển mỗi byte thành kí tự Hex và thêm vào chuỗi
                    builder.Append(bytes[i].ToString("x2"));
                }
                // Trả về chuỗi Hex đã mã hóa
                return builder.ToString();
            }
        }

        public static void ChangePassword(string username, string newPassword, string confirmPassword)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(newPassword) ||
                string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please enter username, new password and confirm password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirm password do not match", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var db = new QLQuanNetContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username);
                if (user == null)
                {
                    MessageBox.Show("User not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                user.PasswordHash = HashPassword(newPassword);
                db.SaveChanges();
                MessageBox.Show("Password changed successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public static SecureString ConvertToSecureString(string password)
        {
            SecureString securePassword = new SecureString();
            foreach (char c in password)
            {
                securePassword.AppendChar(c);
            }
            securePassword.MakeReadOnly();
            return securePassword;
        }

        public static string SecureStringToString(SecureString secureString)
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
    }

    public static class UserAccoount
    {
        public static bool AddUser(TextBox txtUsername, PasswordBox txtPassword, ComboBox cbxRole, CheckBox chkActive,
            TextBox txtFullname, DatePicker dpBirthdate, ComboBox cbxGender, TextBox txtEmail, TextBox txtPhone,
            TextBox txtAddress)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Password))
            {
                MessageBox.Show("Please enter username and password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            try
            {
                using (var db = new QLQuanNetContext())
                {
                    if(db.Users.Any(u => u.Username == txtUsername.Text))
                    {
                        MessageBox.Show("Username already exists. Please choose another username.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                    var user = new QLQuanNetContext.User
                    {
                        Username = txtUsername.Text,
                        PasswordHash = PasswordHasher.HashPassword(txtPassword.Password),
                        Role = cbxRole.Text,
                        Status = chkActive.IsChecked == true ? "Allowed" : "Banned"
                    };
                    db.Users.Add(user);
                    db.SaveChanges();
                    var customer = new QLQuanNetContext.Customer
                    {
                        Full_Name = txtFullname.Text,
                        Birthdate = dpBirthdate.SelectedDate ?? DateTime.MinValue,
                        Gender = cbxGender.Text,
                        Email = txtEmail.Text,
                        Address = txtAddress.Text,
                        User_ID = user.User_ID
                    };
                    if(txtPhone.Text != null && txtPhone.Text.Length > 0)
                    {
                        customer.Phone = txtPhone.Text;
                    }
                    db.Customers.Add(customer);

                    db.SaveChanges();
                }
                MessageBox.Show("User added successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        public static void LoadData(DataGrid dgAccounts, TextBox txtUsername, PasswordBox txtPassword, ComboBox cbxRole,
                                        CheckBox chkActive, TextBox txtFullname, DatePicker dpBirthdate, ComboBox cbxGender,
                                        TextBox txtEmail, TextBox txtPhone, TextBox txtAddress)
        {
            if (dgAccounts.SelectedItem != null)
            {
                var selectedUser = (dynamic)dgAccounts.SelectedItem;
                string username = selectedUser.Username;
                try
                {
                    using (var db = new QLQuanNetContext())
                    {
                        var editUser = db.Users.FirstOrDefault(u => u.Username == username);
                        var editCustomer = db.Customers.FirstOrDefault(c => c.User.Username == username);
                        if (editUser != null)
                        {
                            txtUsername.Text = editUser.Username;
                            txtPassword.Password = editUser.PasswordHash;
                            cbxRole.Text = editUser.Role;
                            chkActive.IsChecked = editUser.Status == "Allowed";
                        }
                        if(editCustomer != null) 
                        {
                            txtFullname.Text = editCustomer.Full_Name;
                            dpBirthdate.SelectedDate = editCustomer.Birthdate;
                            cbxGender.Text = editCustomer.Gender;
                            txtEmail.Text = editCustomer.Email;
                            txtPhone.Text = editCustomer.Phone;
                            txtAddress.Text = editCustomer.Address;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }
        public static bool UpdateUser(DataGrid dgAccounts, TextBox txtUsername, PasswordBox txtPassword, ComboBox cbxRole,
                                        CheckBox chkActive, TextBox txtFullname, DatePicker dpBirthdate, ComboBox cbxGender,
                                        TextBox txtEmail, TextBox txtPhone, TextBox txtAddress)
        {
            if (dgAccounts.SelectedItem != null)
            {
                var selectedUser = (dynamic)dgAccounts.SelectedItem;
                string username = selectedUser.Username;

                try
                {
                    using (var db = new QLQuanNetContext())
                    {
                        var editUser = db.Users.FirstOrDefault(u => u.Username == username);
                        var editCustomer = db.Customers.FirstOrDefault(c => c.User.Username == username);

                        if (editUser != null && editCustomer != null)
                        {
                            bool dataChanged = false;
                            

                            if (editUser.Username != txtUsername.Text  || (!string.IsNullOrEmpty(txtPassword.Password) && editUser.PasswordHash != txtPassword.Password) ||
                                editUser.Role != cbxRole.Text || (editUser.Status == "Allowed") != chkActive.IsChecked)
                            {
                                dataChanged = true;
                            }
                            if (editCustomer.Full_Name != txtFullname.Text ||
                             editCustomer.Birthdate != dpBirthdate.SelectedDate ||
                             editCustomer.Gender != cbxGender.Text ||
                             editCustomer.Email != txtEmail.Text ||
                             editCustomer.Phone != txtPhone.Text ||
                             editCustomer.Address != txtAddress.Text)
                            {
                                dataChanged = true;
                            }
                            if (dataChanged)
                            {
                                editUser.Username = txtUsername.Text;

                                editUser.PasswordHash = PasswordHasher.HashPassword(txtPassword.Password);
                     
                                editUser.Role = cbxRole.Text;
                                editUser.Status = chkActive.IsChecked == true ? "Allowed" : "Banned";

                                editCustomer.Full_Name = txtFullname.Text;
                                editCustomer.Birthdate = dpBirthdate.SelectedDate ?? DateTime.MinValue;
                                editCustomer.Gender = cbxGender.Text;
                                editCustomer.Email = txtEmail.Text;
                                if (txtPhone.Text != null && txtPhone.Text.Length > 0)
                                {
                                    editCustomer.Phone = txtPhone.Text;
                                }
                                editCustomer.Address = txtAddress.Text;

                                db.SaveChanges();
                                MessageBox.Show("User updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                                return true;
                            }
                            else
                            {
                                MessageBox.Show("No changes detected.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("User not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            return false;
        }
    }

    public static class Authentication
    {
        // Hàm xác thực người dùng
        public static bool AuthenticateUser(string username, string password, string role)
        {
            using (var db = new QLQuanNetContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username && u.Role == role);
                if (user != null)
                {
                    string passwordHashInDb = user.PasswordHash;
                    if (VerifyPassword(password, passwordHashInDb))
                    {
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

        // Hàm xác thực mật khẩu
        public static bool VerifyPassword(string password, string passwordHashInDb)
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
    }
}