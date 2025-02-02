using CommonLib;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using static CommonLib.ConsoleHelper;

namespace AutoAsmLogger.Helpers;

internal class App : IDisposable
{
    private IEnumerable<SerialPort?>? ports;
    private IEnumerable<Task> tasks;

    internal App()
    {
        Initialization(ConsoleColor.Green, Assembly.GetCallingAssembly().GetName().Name);
        SetSignalHandler(HandleConsoleSignal, true);
        SimpleLogger.SetMainConsoleForegroundColor(ConsoleColor.Green);
    }

    internal void Run(int portSpeed)
    {
        while (true)
        {
            PrintLine("Serial ports scanning...");
            using var scanner = new SerialPortsScanner();
            ports = scanner.Scan(portSpeed);

            if (ports is null || !ports.Any())
            {
                PrintLine("No available ports\nPress any key to refresh or 0 to exit...\n", ConsoleColor.Yellow);

                if (int.TryParse(Console.ReadLine(), out int exit) && exit == 0) Environment.Exit(0);

                continue;
            }

            break;
        }

        try
        {
            foreach (var port in ports) PrintLine($"{port.PortName} is ready.");

            string path = $@"{Environment.CurrentDirectory}\{DateTime.Today:d}\";

            PrintLine($"Data folder: {path}");
            PrintLine("Data receiving...");

            tasks = ports.Select(port => Task.Factory.StartNew(() =>
                new SerialPortHandler(port, 500, FileHelper.GetNextPath($@"{path}record_({port?.PortName}).txt")).Handle()));

            Task.WaitAll(tasks.ToArray());
        }
        catch (Exception ex)
        {
            PrintLine(ex.Message, ConsoleColor.Red);
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

    private void HandleConsoleSignal(ConsoleSignal consoleSignal)
    {
        SimpleLogger.Write("Win32 event has been called.", LogLevel.Warning);
        Dispose();
    }
}