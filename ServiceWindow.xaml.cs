using DD_QLQuanNet.data;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;
using System.IO;
using static DD_QLQuanNet.data.QLQuanNetContext;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Identity.Client;

namespace DD_QLQuanNet
{
    /// <summary>
    /// Interaction logic for ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : Window
    {
        private int currentUserId = 1;
        private int currentStationId = 2;
        private ObservableCollection<Service> _services;
        private ObservableCollection<Order> _selectedOrders;

        public ServiceWindow(string staffName)
        {
            InitializeComponent();
            _services = new ObservableCollection<Service>();
            _selectedOrders = new ObservableCollection<Order>();

            ServiceListView.ItemsSource = _services;
            SelectedServiceListView.ItemsSource = _selectedOrders;
            LoadServices();
            LoadStationName();
            btnSearchStation.Click += btnSearchStation_Click;
        }

        private async void LoadServices(string searchTerm = "", string category = "")
        {
            try
            {
                using var db = new QLQuanNetContext();
                IQueryable<Service> services = db.Services;

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    services = services.Where(s => s.Service_Name.Contains(searchTerm));
                }

                if (!string.IsNullOrEmpty(category))
                {
                    services = services.Where(s => s.Category == category);
                }

                var result = await services.ToListAsync();
                string imagePath = "assets/images/services/";
                _services.Clear();
                foreach (var service in result)
                {
                    service.Image = Path.Combine(imagePath, service.Image);
                    _services.Add(service);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading services: " + ex.Message);
            }
        }
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag != null)
            {
                string category = button.Tag.ToString();
                LoadServices(category: category);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Load services with the search term from 'SearchBox'
            LoadServices(SearchBox.Text);
        }

        private async void LoadStationName()
        {
            try
            {
                using var db = new QLQuanNetContext();
                var stationNames = await db.Stations.Select(s => s.Station_Name).ToListAsync();
                // Gán danh sách tên máy làm nguồn dữ liệu cho ComboBox
                cbxStationName.ItemsSource = stationNames;
                // Đặt giá trị mặc định cho ComboBox nếu cần
                if (stationNames.Count > 0)
                {
                    cbxStationName.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading station names" + ex.Message);
            }
        }
        private async void cbxStationName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchText = cbxStationName.Text;

                // Kiểm tra nếu searchText không rỗng và không phải là một số, thì thực hiện lọc
                if (!string.IsNullOrEmpty(searchText) && !int.TryParse(searchText, out _))
                {
                    using var db = new QLQuanNetContext();
                    var matchedStations = await db.Stations.Where(s => s.Station_Name.Contains(searchText)).Select(s => s.Station_Name).ToListAsync();

                    // Kiểm tra nếu có kết quả trùng khớp
                    if (matchedStations.Count > 0)
                    {
                        // Kiểm tra xem có nhiều hơn một kết quả được trả về không
                        if (matchedStations.Count > 1)
                        {
                            cbxStationName.ItemsSource = matchedStations;
                            cbxStationName.IsDropDownOpen = true;
                        }
                        else
                        {
                            cbxStationName.Text = matchedStations.First();
                        }
                    }
                    else
                    {
                        // Nếu không có kết quả trùng khớp, không cần mở danh sách thả xuống
                        cbxStationName.IsDropDownOpen = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filtering stations" + ex.Message);
            }
        }

        private async void btnSearchStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string searchText = cbxStationName.Text;

                // Kiểm tra nếu searchText không rỗng và không phải là một số, thì thực hiện lọc
                if (!string.IsNullOrEmpty(searchText) && !int.TryParse(searchText, out _))
                {
                    using var db = new QLQuanNetContext();
                    var matchedStations = await db.Stations.Where(s => s.Station_Name.Contains(searchText)).Select(s => s.Station_Name).ToListAsync();

                    // Kiểm tra xem có kết quả trùng khớp
                    if (matchedStations.Count > 0)
                    {
                        // Kiểm tra xem có nhiều hơn một kết quả được trả về không
                        if (matchedStations.Count > 1)
                        {
                            cbxStationName.ItemsSource = matchedStations;
                            cbxStationName.IsDropDownOpen = true;
                        }
                        else
                        {
                            cbxStationName.Text = matchedStations.First();
                        }
                    }
                    else
                    {
                        // Nếu không có kết quả trùng khớp, không cần mở danh sách thả xuống
                        cbxStationName.IsDropDownOpen = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filtering stations" + ex.Message);
            }
        }

        private void ServiceListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ServiceListView.SelectedItem is Service selectedService)
            {
                var existingOrder = _selectedOrders.FirstOrDefault(o => o.Service_ID == selectedService.Service_ID);

                if (existingOrder != null)
                {
                    // If the service already exists in the order list, increase the quantity and update the total cost
                    existingOrder.Quantity++;
                    existingOrder.TotalCost = existingOrder.Quantity * selectedService.Price;
                }
                else
                {
                    // If the service does not exist in the order list, create a new order
                    var newOrder = new Order
                    {
                        Order_Date = DateTime.Now,
                        Quantity = 1,
                        TotalCost = selectedService.Price,
                        User_ID = currentUserId,
                        Service_ID = selectedService.Service_ID,
                        Station_ID = currentStationId,
                        Service = selectedService
                    };

                    _selectedOrders.Add(newOrder);
                }

                SelectedServiceListView.ItemsSource = null;
                SelectedServiceListView.ItemsSource = _selectedOrders;
            }
            CalculateTotalCost();
        }
        private void RemoveService_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Order orderToRemove)
            {
                _selectedOrders.Remove(orderToRemove); // Xóa món khỏi danh sách
                SelectedServiceListView.Items.Refresh(); // Cập nhật lại giao diện người dùng
            }
        }
        private void CalculateTotalCost()
        {
            decimal totalCost = 0;

            foreach (var order in _selectedOrders)
            {
                totalCost += order.TotalCost;
            }

            txtTotalCost.Text = totalCost.ToString("#,0"); // Format as currency
        }
        private Order _lastSelectedOrder;

        private void SelectedServiceListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedServiceListView.SelectedItem is Order selectedOrder)
            {
                _lastSelectedOrder = selectedOrder;
            }
        }

        private void IncreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (_lastSelectedOrder != null)
            {
                _lastSelectedOrder.Quantity++; // Tăng số lượng lên 1
                _lastSelectedOrder.TotalCost = _lastSelectedOrder.Quantity * _lastSelectedOrder.Service.Price; // Cập nhật tổng tiền

                // Cập nhật lại giao diện người dùng
                SelectedServiceListView.Items.Refresh();
            }
        }

        private void DecreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (_lastSelectedOrder != null)
            {
                // Giảm số lượng đi 1, nhưng không thể nhỏ hơn 1
                _lastSelectedOrder.Quantity = Math.Max(1, _lastSelectedOrder.Quantity - 1);
                _lastSelectedOrder.TotalCost = _lastSelectedOrder.Quantity * _lastSelectedOrder.Service.Price; // Cập nhật tổng tiền

                // Cập nhật lại giao diện người dùng
                SelectedServiceListView.Items.Refresh();
            }
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _lastSelectedOrder = ((ListViewItem)sender).Content as Order;
        }
        private void AddServiceButton_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem có món nào được chọn trong ServiceListView không
            if (ServiceListView.SelectedItem is Service selectedService)
            {
                // Tìm xem món đã chọn đã có trong danh sách món đã chọn (_selectedOrders) chưa
                Order existingOrder = _selectedOrders.FirstOrDefault(o => o.Service_ID == selectedService.Service_ID);

                if (existingOrder != null)
                {
                    // Nếu món đã tồn tại, tăng số lượng lên 1 và cập nhật tổng tiền
                    existingOrder.Quantity++;
                    existingOrder.TotalCost = existingOrder.Quantity * selectedService.Price;
                }
                else
                {
                    // Nếu món chưa tồn tại, tạo một đối tượng Order mới với thông tin từ món đã chọn
                    var newOrder = new Order
                    {
                        Order_Date = DateTime.Now,
                        Quantity = 1, // Số lượng mặc định là 1
                        TotalCost = selectedService.Price, // Tổng tiền là giá của món
                        User_ID = currentUserId, // Giả sử bạn đã có ID của người dùng hiện tại
                        Service_ID = selectedService.Service_ID,
                        Station_ID = currentStationId, // Giả sử bạn đã có ID của trạm hiện tại
                        Service = selectedService
                    };

                    // Thêm đối tượng Order mới vào danh sách các món đã chọn
                    _selectedOrders.Add(newOrder);
                }

                // Cập nhật giao diện người dùng
                SelectedServiceListView.Items.Refresh();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to cancel the order?", "Notification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _selectedOrders.Clear();
                this.Close();
            }
            else
                return;
        }

        private void btnCompleteOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using var db = new QLQuanNetContext();
                foreach (var order in _selectedOrders)
                {
                    db.Orders.Add(order);
                }
                db.SaveChangesAsync();
                MessageBox.Show("Order completed successfully.");
                _selectedOrders.Clear();
                SelectedServiceListView.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error completing order: " + ex.Message);
            }
        }
    }
}
