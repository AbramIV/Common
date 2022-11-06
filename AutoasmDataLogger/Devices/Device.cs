using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Timers;

namespace AutoasmDataLogger.Devices;

public class Device
{
    public string? Name;
    public byte[]? Buffer;
    public bool IntervalElapsed;
    public readonly TcpClient Client;
    public readonly NetworkStream? ClientStream;
    public readonly Stopwatch Watcher = new();
    public readonly System.Timers.Timer Limiter;

    public Device() { }

    public Device(TcpClient client)
    {
        Client = client;
        Limiter = new(5000);
        Limiter.Elapsed += Limiter_Elapsed;
        ClientStream = Client.GetStream();
        ClientStream.ReadTimeout = 5000;
        Buffer = new byte[1024];
        SimpleLogger.Write($"Device {Client.Client.RemoteEndPoint} connected");
    }

    public virtual void Handle()
    {

    }

    public void SendRequest(string command)
    {
        IntervalElapsed = false;
        Buffer = Encoding.ASCII.GetBytes(command);
        ClientStream?.Write(Buffer, 0, Buffer.Length);
        SimpleLogger.Write($"\"{command}\" sent");
    }

    public string GetResponse()
    {
        StringBuilder stringBuilder = new();
        int bytes;

        try
        {
            do
            {
                bytes = ClientStream.Read(Buffer, 0, Buffer.Length);
                stringBuilder.Append(Encoding.ASCII.GetString(Buffer, 0, bytes));
            }
            while (ClientStream.DataAvailable);
        }
        catch (Exception ex)
        {
            SimpleLogger.Write(ex.Message, LogLevel.Warning);
            return stringBuilder.ToString();
        }

        SimpleLogger.Write($"Received: {stringBuilder}");

        return stringBuilder.ToString();
    }

    public void Close()
    {
        SimpleLogger.Write($"Client {Client?.Client.RemoteEndPoint} disconnected.");
        ClientStream?.Close();
        Client?.Close();
        ClientStream?.Dispose();
        Client?.Dispose();
    }

    private void Limiter_Elapsed(object? sender, ElapsedEventArgs e)
    {
        IntervalElapsed = true;
        Limiter.Stop();
    }
}
