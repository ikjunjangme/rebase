using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace PublicUtility
{
    public class Localhost
    {
        public static string GetAddress()
        {
            return Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(x => x.AddressFamily == AddressFamily.InterNetwork).Last().ToString();
        }

        public static int GetUsePort(int port)
        {
            TcpListener l = new TcpListener(IPAddress.Loopback, port);
            l.Start();
            int next_port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            return next_port;
        }

        public static int FindFreePort()
        {
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            return port;
        }
    }
}
