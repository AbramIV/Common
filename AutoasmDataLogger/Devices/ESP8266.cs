using System.Net.Sockets;

namespace AutoasmDataLogger.Devices;

public class ESP8266 : Device
{
    public ESP8266() { }

    public ESP8266(TcpClient client) : base(client) { }

    public override void Handle()
    {
        try
        {
            SendRequest("Get name");
            Name = GetResponse();

            if (string.IsNullOrEmpty(Name.Replace("\r\n", "").Trim()))
            {
                SimpleLogger.Write($"Name is empty, assigned to {Environment.CurrentManagedThreadId}", LogLevel.Warning);
                Name = Environment.CurrentManagedThreadId.ToString();
            }

            SimpleLogger.Write($"Device {Name} connected.");

            while (true)
            {
                SendRequest("Get data");
                var data = GetResponse();
                if (string.IsNullOrEmpty(data)) SimpleLogger.Write($"Device {Name} response is empty.", LogLevel.Warning);
                else SimpleLogger.Write($"Device data: {data}");
                data = string.Empty;
            }
        }
        catch (Exception ex)
        {
            SimpleLogger.Write(ex.Message, LogLevel.Error);
        }
        finally
        {
            Close();
        }
    }
}

