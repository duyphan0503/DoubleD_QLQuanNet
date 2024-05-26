<<<<<<< HEAD
﻿using System.Windows;
using System.Windows.Input;
using DD.Functions;
using DD_QLQuanNet.data;
namespace DD_QLQuanNet.resources.models
{
    /// <summary>
    /// Interaction logic for TopUps.xaml
    /// </summary>
    public partial class TopUps : Window
    {
        private const decimal price_per_hour = 10000;
        private bool isUpdating = false;
        private bool isMoneyChanging = false;
        public TopUps(string username)
        {
            InitializeComponent();
            txtUsername.Text = username;
            RecalculateMoneyAndTime();
        }

        private void btnTopUp_OK_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(txtMoney.Text))
            {
                Functions.TopUps(txtUsername.Text,decimal.Parse(txtMoney.Text), txtNotes.Text);
                this.Close();
            }
        }
        private void RecalculateMoneyAndTime()
        {
            if (isUpdating) return;
            isMoneyChanging = true;
            UpdateMoneyAndTime();
            isMoneyChanging = false;

        }
        private void iudHour_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
                RecalculateMoneyAndTime();
        }


        private void iudMinute_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if(iudMinute.Value == 60)
            {
                iudMinute.Value = 0;
                iudHour.Value++;
            }
                RecalculateMoneyAndTime();
        }

        private void txtMoney_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (isUpdating) return;
            decimal money;
            if (decimal.TryParse(txtMoney.Text, out money))
            {
                isUpdating = true;
                UpdateHourAndMinute(money);
                isUpdating = false;
            }

        }
        private void UpdateHourAndMinute(decimal money)
        {
            if(isMoneyChanging) return;
            decimal totalMinutes = money * 60 / price_per_hour;
            decimal hours = Math.Floor(totalMinutes / 60); 
            decimal minutes = totalMinutes % 60;

            iudHour.Value = (int)hours;
            iudMinute.Value = (int)Math.Round(minutes);
            using (var db = new QLQuanNetContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == txtUsername.Text);
                if (user != null)
                {
                    decimal newMoney = user.Balance + money;
                    decimal newHours = Math.Floor(newMoney / price_per_hour);
                    decimal newMinutes = Math.Round((newMoney * 60 / price_per_hour) % 60);

                    txtNewMoney.Text = Math.Round(newMoney).ToString();
                    txtNewTime.Text = $"{(int)newHours}h : {(int)newMinutes}m";
                }
            }
        }
        private void UpdateMoneyAndTime()
        {
            if (iudHour == null || iudMinute == null || txtMoney == null) return;
            if(iudHour.Value != null && iudMinute.Value != null)
            {
                decimal totalMinutes = (decimal)iudHour.Value * 60 + (decimal)iudMinute.Value;
                decimal totalMoney = totalMinutes * price_per_hour / 60;
                txtMoney.Text = Math.Round(totalMoney).ToString();
                using (var db = new QLQuanNetContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.Username == txtUsername.Text);
                    if (user != null)
                    {
                        
                        decimal newMoney = user.Balance + totalMoney;
                        decimal newHours = Math.Floor(newMoney / price_per_hour);
                        decimal newMinutes = Math.Round((newMoney * 60 / price_per_hour) % 60);

                        txtNewMoney.Text = Math.Round(newMoney).ToString();
                        txtNewTime.Text = $"{(int)newHours}h : {(int)newMinutes}m";
                    }
                }
            }
            else
            {
                iudHour.Value = 0;
                iudMinute.Value = 0;
            }
            
        }

        private void btnTopUp_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtMoney_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtMoney.Text == "0")
            {
                txtMoney.Text = "";
            }
        }


        private void txtMoney_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (txtMoney.Text.Length >= 10)
            {
                // Chặn việc nhập thêm nếu phím được nhấn không phải là phím điều hướng hoặc xóa
                if (!(e.Key == Key.Delete || e.Key == Key.Back || e.Key == Key.Left || e.Key == Key.Right))
                {
                    e.Handled = true;
                }
            }
        }
        private void txtMoney_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

    }
}
=======
﻿using System.Windows;
using System.Windows.Input;
using DD.Functions;
using DD_QLQuanNet.data;
namespace DD_QLQuanNet.resources.models
{
    /// <summary>
    /// Interaction logic for TopUps.xaml
    /// </summary>
    public partial class TopUps : Window
    {
        private const decimal price_per_hour = 10000;
        private bool isUpdating = false;
        private bool isMoneyChanging = false;
        public TopUps(string username)
        {
            InitializeComponent();
            txtUsername.Text = username;
            RecalculateMoneyAndTime();
        }

        private void btnTopUp_OK_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(txtMoney.Text))
            {
                Functions.TopUps(txtUsername.Text,decimal.Parse(txtMoney.Text), txtNotes.Text);
                this.Close();
            }
        }
        private void RecalculateMoneyAndTime()
        {
            if (isUpdating) return;
            isMoneyChanging = true;
            UpdateMoneyAndTime();
            isMoneyChanging = false;

        }
        private void iudHour_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
                RecalculateMoneyAndTime();
        }


        private void iudMinute_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if(iudMinute.Value == 60)
            {
                iudMinute.Value = 0;
                iudHour.Value++;
            }
                RecalculateMoneyAndTime();
        }

        private void txtMoney_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (isUpdating) return;
            decimal money;
            if (decimal.TryParse(txtMoney.Text, out money))
            {
                isUpdating = true;
                UpdateHourAndMinute(money);
                isUpdating = false;
            }

        }
        private void UpdateHourAndMinute(decimal money)
        {
            if(isMoneyChanging) return;
            decimal totalMinutes = money * 60 / price_per_hour;
            decimal hours = Math.Floor(totalMinutes / 60); 
            decimal minutes = totalMinutes % 60;

            iudHour.Value = (int)hours;
            iudMinute.Value = (int)Math.Round(minutes);
            using (var db = new QLQuanNetContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == txtUsername.Text);
                if (user != null)
                {
                    decimal newMoney = user.Balance + money;
                    decimal newHours = Math.Floor(newMoney / price_per_hour);
                    decimal newMinutes = Math.Round((newMoney * 60 / price_per_hour) % 60);

                    txtNewMoney.Text = Math.Round(newMoney).ToString();
                    txtNewTime.Text = $"{(int)newHours}h : {(int)newMinutes}m";
                }
            }
        }
        private void UpdateMoneyAndTime()
        {
            if (iudHour == null || iudMinute == null || txtMoney == null) return;
            if(iudHour.Value != null && iudMinute.Value != null)
            {
                decimal totalMinutes = (decimal)iudHour.Value * 60 + (decimal)iudMinute.Value;
                decimal totalMoney = totalMinutes * price_per_hour / 60;
                txtMoney.Text = Math.Round(totalMoney).ToString();
                using (var db = new QLQuanNetContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.Username == txtUsername.Text);
                    if (user != null)
                    {
                        
                        decimal newMoney = user.Balance + totalMoney;
                        decimal newHours = Math.Floor(newMoney / price_per_hour);
                        decimal newMinutes = Math.Round((newMoney * 60 / price_per_hour) % 60);

                        txtNewMoney.Text = Math.Round(newMoney).ToString();
                        txtNewTime.Text = $"{(int)newHours}h : {(int)newMinutes}m";
                    }
                }
            }
            else
            {
                iudHour.Value = 0;
                iudMinute.Value = 0;
            }
            
        }

        private void btnTopUp_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtMoney_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtMoney.Text == "0")
            {
                txtMoney.Text = "";
            }
        }


        private void txtMoney_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (txtMoney.Text.Length >= 10)
            {
                // Chặn việc nhập thêm nếu phím được nhấn không phải là phím điều hướng hoặc xóa
                if (!(e.Key == Key.Delete || e.Key == Key.Back || e.Key == Key.Left || e.Key == Key.Right))
                {
                    e.Handled = true;
                }
            }
        }
        private void txtMoney_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

    }
}
>>>>>>> 2c066f95b899f6c361ceb9c7b666c4dca578392a
