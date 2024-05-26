using DD.Functions;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DD_QLQuanNet
{
    internal class server
    {
        private static Socket listener;

        // Hàm khởi động máy chủ và xử lý kết nối từ máy trạm
        private static void StartServer(string[] args)
        {
            listener = new Socket(SocketType.Stream, ProtocolType.Tcp);

            // Thiết lập điểm cuối cục bộ cho socket
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 3050);

            while (true)
            {
                // Chờ yêu cầu kết nối từ client
                Socket clientSocket = listener.Accept();

                // Xử lý yêu cầu đăng nhập từ client
                string request = ReceiveData(clientSocket);

                string[] credentials = request.Split(',');
                string username = credentials[0];
                string password = credentials[1];
                string role = credentials[2];

                bool isAuthenticated = Authentication.AuthenticateUser(username, password, role);

                // Gửi kết quả đăng nhập về cho client
                string response = isAuthenticated ? "Authenticated" : "NotAuthenticated";
                byte[] responseData = Encoding.ASCII.GetBytes(response);
                clientSocket.Send(responseData);

                // Đóng kết nối với client
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
        }

        private static string ReceiveData(Socket clientSocket)
        {
            // Nhận dữ liệu từ client
            byte[] bytes = new byte[1024];
            int numByte = clientSocket.Receive(bytes);
            return Encoding.ASCII.GetString(bytes, 0, numByte);
        }
    }
}