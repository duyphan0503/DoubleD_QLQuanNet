using DD_QLQuanNet.data;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using static DD_QLQuanNet.data.QLQuanNetContext;
using DD.Functions;

using System.Collections.Generic;
using System.ComponentModel;

namespace DD_QLQuanNet
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private ObservableCollection<Service> _services;
        private ObservableCollection<Order> _selectedOrders;
        private ObservableCollection<Invoice> _orderWatingPayment;
        private Order _lastSelectedOrder;
        public OrderWindow()
        {
            InitializeComponent();
            _services = new ObservableCollection<Service>();
            _selectedOrders = new ObservableCollection<Order>();
            _orderWatingPayment = new ObservableCollection<Invoice>();

            ServiceListView.ItemsSource = _services;
            SelectedServiceListView.ItemsSource = _selectedOrders;
            OrderWaitingPayment.ItemsSource = _orderWatingPayment;
            LoadServices();
            LoadStationName();
            LoadOrderWaitingPayment();
            txtStaffName.Text = UserAccoount.Username;
            btnSearchStation.Click += btnSearchStation_Click;
        }
        private async Task LoadServices(string searchTerm = "", string category = "")
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
                cbxStationName1.ItemsSource = stationNames;
                cbxStationName2.ItemsSource = stationNames;
                // Đặt giá trị mặc định cho ComboBox nếu cần
                if (stationNames.Count > 0)
                {
                    cbxStationName1.SelectedIndex = 0;
                    cbxStationName2.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading station names" + ex.Message);
            }
        }
        private async void cbxStationName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string searchText = cbxStationName1.Text;

                // Kiểm tra nếu searchText không rỗng và không phải là một số, thì thực hiện lọc
                if (!string.IsNullOrEmpty(searchText) && !int.TryParse(searchText, out _))
                {
                    using var db = new QLQuanNetContext();
                    var matchedStations = await db.Stations
                        .Where(s => s.Station_Name
                        .Contains(searchText))
                        .Select(s => s.Station_Name)
                        .ToListAsync();

                    // Kiểm tra nếu có kết quả trùng khớp
                    if (matchedStations.Count > 0)
                    {
                        // Kiểm tra xem có nhiều hơn một kết quả được trả về không
                        cbxStationName1.ItemsSource = matchedStations;
                        cbxStationName1.IsDropDownOpen = matchedStations.Count > 1;
                        //cbxStationName1.Text = matchedStations.First();
                    }
                    else
                    {
                        // Nếu không có kết quả trùng khớp, không cần mở danh sách thả xuống
                        cbxStationName1.IsDropDownOpen = false;
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
            cbxStationName_TextChanged(sender, e as TextChangedEventArgs);
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
                        User_ID = GetCurrentUserId(),
                        Service_ID = selectedService.Service_ID,
                        Station_ID = GetCurrentStationId(),
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
            decimal totalCost = _selectedOrders.Sum(order => order.TotalCost);
            txtTotalCost.Text = totalCost.ToString("#,0");
        }
        

        private void SelectedServiceListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedServiceListView.SelectedItem is Order selectedOrder)
            {
                _lastSelectedOrder = selectedOrder;
            }
        }

        private async void IncreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (_lastSelectedOrder != null)
            {
                try
                {
                    using var db = new QLQuanNetContext();
                    _lastSelectedOrder.Quantity++; // Tăng số lượng lên 1
                    _lastSelectedOrder.TotalCost = _lastSelectedOrder.Quantity * _lastSelectedOrder.Service.Price; // Cập nhật tổng tiền

                    // Lưu thay đổi vào cơ sở dữ liệu
                    await db.SaveChangesAsync();

                    // Cập nhật lại giao diện người dùng
                    SelectedServiceListView.Items.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error increasing quantity: " + ex.Message);
                }
            }
        }

        private async void DecreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (_lastSelectedOrder != null)
            {
                try
                {
                    using var db = new QLQuanNetContext();
                    // Giảm số lượng đi 1, nhưng không thể nhỏ hơn 1
                    _lastSelectedOrder.Quantity = Math.Max(1, _lastSelectedOrder.Quantity - 1);
                    _lastSelectedOrder.TotalCost = _lastSelectedOrder.Quantity * _lastSelectedOrder.Service.Price; // Cập nhật tổng tiền

                    // Lưu thay đổi vào cơ sở dữ liệu
                    await db.SaveChangesAsync();

                    // Cập nhật lại giao diện người dùng
                    SelectedServiceListView.Items.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error decreasing quantity: " + ex.Message);
                }
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
                        User_ID = GetCurrentUserId(), // Giả sử bạn đã có ID của người dùng hiện tại
                        Service_ID = selectedService.Service_ID,
                        Station_ID = GetCurrentStationId(), // Giả sử bạn đã có ID của trạm hiện tại
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

        private async void btnCompleteOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using var db = new QLQuanNetContext();
                var newInvoice = new Invoice
                {
                    Date = DateTime.Now,
                    Total = _selectedOrders.Sum(o => o.TotalCost),
                    User_ID = GetCurrentUserId(),
                    Orders = new ObservableCollection<Order>()
                };
                db.Invoices.Add(newInvoice);
                await db.SaveChangesAsync();

                foreach (var order in _selectedOrders)
                {
                    order.Invoice_ID = newInvoice.Invoice_ID;
                    order.Station_ID = GetCurrentStationId();
                    order.Service = db.Services.Find(order.Service_ID);
                    order.Status = "Completed";
                    db.Orders.Add(order);
                }
                await db.SaveChangesAsync();
                MessageBox.Show("Order completed successfully.");
                _selectedOrders.Clear();
                SelectedServiceListView.Items.Refresh();
                CalculateTotalCost();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error completing order: " + ex.InnerException?.Message);
            }
        }
        private int GetCurrentUserId()
        {
            using var db = new QLQuanNetContext();
            var user = db.Users.FirstOrDefault(u => u.Username == txtStaffName.Text);
            return user?.User_ID ?? throw new Exception("User not found");
        }
        private int GetCurrentStationId()
        {
            using var db = new QLQuanNetContext();
            var station = db.Stations.FirstOrDefault(s => s.Station_Name == cbxStationName2.Text);
            return station?.Station_ID ?? throw new Exception("Station not found");
        }

        private async void btnSaveOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using var db = new QLQuanNetContext();
                var newInvoice = new Invoice
                {
                    Date = DateTime.Now,
                    Total = _selectedOrders.Sum(o => o.TotalCost),
                    User_ID = GetCurrentUserId(),
                    Orders = new ObservableCollection<Order>()
                };
                db.Invoices.Add(newInvoice);
                await db.SaveChangesAsync();

                foreach (var order in _selectedOrders)
                {
                    order.Invoice_ID = newInvoice.Invoice_ID;
                    order.Status = "Pending";
                    order.Station_ID = GetCurrentStationId();
                    order.Service = db.Services.Find(order.Service_ID);
                    db.Orders.Add(order);
                }
                await db.SaveChangesAsync();
                _selectedOrders.Clear();
                MessageBox.Show("Order saved successfully.");
                SelectedServiceListView.Items.Refresh();
                CalculateTotalCost();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving order: " + ex.InnerException?.Message);
            }
        }
        private async Task LoadOrderWaitingPayment()
        {
            try
            {
                using var db = new QLQuanNetContext();
                // Lấy danh sách các hóa đơn có trạng thái là "Pending" cùng với các đơn hàng liên quan
                var invoices = await db.Invoices
                    .Include(i => i.Orders)
                    .Where(i => i.Orders.Any(o => o.Status == "Pending"))
                    .ToListAsync();

                // Tạo một danh sách mới để lưu trữ thông tin cần thiết
                var orderWaitingPayments = new List<object>();

                // Duyệt qua danh sách hóa đơn
                foreach (var invoice in invoices)
                {
                    // Lấy danh sách các đơn hàng có cùng Invoice_ID
                    var ordersWithPendingStatus = invoice.Orders.Where(o => o.Status == "Pending");

                    // Duyệt qua các đơn hàng và lấy thông tin Station_Name từ Station_ID
                    var stationName = await db.Stations
                        .Where(s => s.Station_ID == ordersWithPendingStatus.FirstOrDefault().Station_ID)
                        .Select(s => s.Station_Name)
                        .FirstOrDefaultAsync();

                    // Thêm thông tin vào danh sách mới
                    orderWaitingPayments.Add(new
                    {
                        Invoice_ID = invoice.Invoice_ID,
                        Station_Name = stationName,
                        Total = invoice.Total
                    });
                }

                // Cập nhật ListView với danh sách mới
                OrderWaitingPayment.ItemsSource = orderWaitingPayments;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading orders waiting payment: " + ex.Message);
            }
        }

        private async void tcOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                TabItem selectedTab = e.AddedItems[0] as TabItem;

                // Kiểm tra xem TabItem được chọn có phải là ListOfOrderTabItem hay không
                if (selectedTab != null && selectedTab == ListOfOrderTabItem)
                {
                    // Nếu có, thực hiện việc tải danh sách đơn hàng chờ thanh toán
                    await LoadOrderWaitingPayment();
                }
                if(selectedTab != null && selectedTab == NewOrderTabItem)
                {
                    await LoadServices();
                }
            }
        }
        private async void CancelOrder_Click(object sender, RoutedEventArgs e)
        {
            // Lấy đơn hàng từ DataContext của nút được nhấn
            if (sender is FrameworkElement element && element.DataContext is Invoice selectedOrder)
            {
                try
                {
                    // Lấy Invoice_ID từ đơn hàng được nhấn
                    int invoiceId = selectedOrder.Invoice_ID;

                    using var db = new QLQuanNetContext();

                    // Lấy danh sách các đơn hàng có Invoice_ID tương ứng và trạng thái là "Pending"
                    var ordersToCancel = await db.Orders
                        .Where(o => o.Invoice_ID == invoiceId && o.Status == "Pending")
                        .ToListAsync();

                    // Duyệt qua danh sách và đổi trạng thái thành "Canceled"
                    foreach (var order in ordersToCancel)
                    {
                        order.Status = "Canceled";
                    }

                    // Lưu thay đổi
                    await db.SaveChangesAsync();

                    // Hiển thị thông báo thành công
                    MessageBox.Show("Selected orders have been canceled successfully.");

                    // Tải lại danh sách đơn hàng chờ thanh toán
                    await LoadOrderWaitingPayment();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error canceling orders: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Error canceling orders: Selected order is null");
            }
        }
        private async void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Lấy Invoice_ID từ dữ liệu được chọn trong ListView
                if (OrderWaitingPayment.SelectedItem is { } selectedItem)
                {
                    int invoiceId = (int)selectedItem.GetType().GetProperty("Invoice_ID").GetValue(selectedItem);

                    // Mở cửa sổ OrderDetailWindow để chỉnh sửa đơn hàng
          

                    // Tải lại danh sách đơn hàng chờ thanh toán
                    await LoadOrderWaitingPayment();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error editing order: " + ex.Message);
            }
        }
        private void OrderWaitingPayment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (OrderWaitingPayment.SelectedItem != null)
            //{
            //    dynamic selectedInvoice = OrderWaitingPayment.SelectedItem;
            //    int invoiceId = selectedInvoice.Invoice_ID;
            //    // Sử dụng invoiceId theo nhu cầu của bạn
            //}
        }
        private void btnAddNotes_Click(object sender, RoutedEventArgs e)
        {
            // Lấy đơn hàng từ DataContext của nút được nhấn
            if (sender is FrameworkElement element && element.DataContext is Order selectedOrder)
            {
                // Hiển thị hộp thoại cho người dùng nhập ghi chú
                string notes = Microsoft.VisualBasic.Interaction.InputBox("Enter notes:", "Add Notes", "");

                // Thêm ghi chú vào đơn hàng được nhấn
                if (!string.IsNullOrWhiteSpace(notes))
                {
                    selectedOrder.Notes = notes;
                }
            }
        }
    }
}
