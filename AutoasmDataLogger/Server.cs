using AutoasmDataLogger.Devices;
using System.Net;
using System.Net.Sockets;

namespace AutoasmDataLogger;

internal class Server : IDisposable
{
    private readonly TcpListener? Listener;
    private readonly List<Device> Devices;
    internal bool IsDisposed { get; private set; }

    internal Server(IPAddress ip, int port)
    {
        Listener = new(ip, port);
        Devices = new();
        IsDisposed = false;
    }

    internal void Run()
    {
        try
        {
            Listener?.Start();

            while (true)
            {
                TcpClient? client = Listener?.AcceptTcpClient();
                Device device = new ESP8266(client);
                Thread clientThread = new(new ThreadStart(device.Handle));
                clientThread.Start();
            }
        }
        catch (Exception ex)
        {
            SimpleLogger.Write(ex.Message, LogLevel.Fatal);
        }
        finally
        {
            Dispose();
        }
    }

    private void DeviceDisconnect()
    {

    }

    public void Dispose()
    {
        Listener?.Stop();
        Listener?.Server.Close();
        Listener?.Server.Dispose();
        IsDisposed = true;
    }
}
