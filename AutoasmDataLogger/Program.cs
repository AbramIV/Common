using System.Net;

namespace TcpServer;

class Program
{
    const string IP = "192.168.111.63";
    const int PORT = 11000;

    static void Main(string[] args)
    {
        string address = args.Length > 0 && !string.IsNullOrEmpty(args[0]) ? args[0] : IP;

        if (!IPAddress.TryParse(address, out IPAddress? ip))
        {
            SimpleLogger.Write("IP parse error", LogLevel.Error);
            SimpleLogger.Write("Application exit");
            Environment.Exit(1);
        }

        SimpleLogger.Write($"Server start {ip}:{PORT}");
        Server server = new(ip, PORT);
        server.Run();
        SimpleLogger.Write("Application exit");
    }
}