
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

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
        
        
    }
    
}

