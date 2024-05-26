using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using DD_QLQuanNet.common;

namespace DD_QLQuanNet
{
    public partial class ServicesUserControl : UserControl
    {
        //    private ObservableCollection<Service> Services;
        //    private DatabaseManager dbManager;
        public ServicesUserControl()
        {
            InitializeComponent();
        }
        //    public ServicesUserControl(DatabaseManager databaseManager)
        //    {
        //        InitializeComponent();
        //        dbManager = databaseManager;
        //        LoadServices();
        //    }

        //    private void LoadServices()
        //    {
        //        Services = new ObservableCollection<Service>();
        //        string query = "SELECT ServiceId, ServiceName, Price FROM Services";
        //        DataTable dataTable = dbManager.ExecuteQuery(query);

        //        foreach (DataRow row in dataTable.Rows)
        //        {
        //            Services.Add(new Service
        //            {
        //                ServiceId = (int)row["ServiceId"],
        //                ServiceName = (string)row["ServiceName"],
        //                Price = (double)row["Price"]
        //            });
        //        }

        //        ServicesDataGrid.ItemsSource = Services;
        //     }

        private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        //string query = "INSERT INTO Services (ServiceName, Price) VALUES (@ServiceName, @Price)";
        //var parameters = new Dictionary<string, object>
        //        {
        //            { "@ServiceName", "New Service" },
        //            { "@Price", 10.0 }
        //        };
        //dbManager.ExecuteNonQuery(query, parameters);
        //LoadServices();
    }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            //if (ServicesDataGrid.SelectedItem is Service selectedService)
            //{
            //    string query = "UPDATE Services SET ServiceName = @ServiceName, Price = @Price WHERE ServiceId = @ServiceId";
            //    var parameters = new Dictionary<string, object>
            //    {
            //        { "@ServiceId", selectedService.ServiceId },
            //        { "@ServiceName", "Edited Service" },
            //        { "@Price", 20.0 }
            //    };
            //    dbManager.ExecuteNonQuery(query, parameters);
            //    LoadServices();
            //}
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //if (ServicesDataGrid.SelectedItem is Service selectedService)
            //{
            //    string query = "DELETE FROM Services WHERE ServiceId = @ServiceId";
            //    var parameters = new Dictionary<string, object>
            //        {
            //            { "@ServiceId", selectedService.ServiceId }
            //        };
            //    dbManager.ExecuteNonQuery(query, parameters);
            //    LoadServices();
            //}
        }

    //public class Service
    //{
    //    public int ServiceId { get; set; }
    //    public string ServiceName { get; set; }
    //    public double Price { get; set; }
    //}
}
}
