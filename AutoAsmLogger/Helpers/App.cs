using SerialPortServer.Helpers;
using CommonLib;
using System.IO.Ports;
using System.Reflection;

namespace SerialPortServer.Helpers;

internal class App : IDisposable
{
    private IEnumerable<SerialPort?>? ports;
    private IEnumerable<Task> tasks;

    internal App()
    {
        SimpleLogger.SetMainConsoleForegroundColor(ConsoleColor.Green);
    }

    internal void Run(int portSpeed)
    {
        while (true)
        {
            using var scanner = new SerialPortsScanner();
            ports = scanner.Scan(portSpeed);

            if (ports is null || !ports.Any())
            {
                if (int.TryParse(Console.ReadLine(), out int exit) && exit == 0) Environment.Exit(0);
                
                continue;
            }

            break;
        }

        try
        {
            string path = $@"{Environment.CurrentDirectory}\{DateTime.Today:d}\";

            tasks = ports.Select(port => Task.Factory.StartNew(() =>
                new SerialPortHandler(port, 500, FileHelper.GetNextPath($@"{path}record_({port?.PortName}).txt")).Handle()));

            Task.WaitAll(tasks.ToArray());
        }
        catch (Exception ex)
        {
            SimpleLogger.Write(ex.ToString(), LogLevel.Error);
        }
    }

    public void Dispose()
    {
        if (ports is not null)
        {
            foreach (var port in ports)
            {
                port?.Close();
                port?.Dispose();
            }
        }

        SimpleLogger.Write("App close.");
    }
}