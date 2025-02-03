using CommonLib;
using System.Diagnostics;
using System.IO.Ports;
using Counter = System.Timers.Timer;

namespace AutoAsmLogger.Helpers;

internal class SerialPortHandler : IDisposable
{
    private readonly string file;

    private readonly SerialPort port;
    private readonly Counter counter;
    private readonly Stopwatch watch;

    private bool awaiting = true;

    public SerialPortHandler(SerialPort serialPort, int pollingInterval, string path) 
    {
        file = path;

        port = serialPort;

        counter = new(pollingInterval) { AutoReset = true };
        counter.Elapsed += Counter_Elapsed;

        watch = new Stopwatch();
    }

    public void Handle()
    {
        try
        {
            port.DataReceived += Serial_DataReceived;
            if (!port.IsOpen) port.Open();
            port.DiscardInBuffer();
            port.DiscardOutBuffer();
            counter.Start();
            watch.Start();

            while (awaiting);

            SimpleLogger.Write($"{port.PortName} closed or response timeout. ", LogLevel.Warning);
        }
        catch (Exception ex)
        {
            SimpleLogger.Write($"{port.PortName}: {ex.Message}", LogLevel.Error);
        }
        finally
        {
            Dispose();
        }
    }

    private void Serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        watch.Reset();
        File.AppendAllText(file, ((SerialPort)sender)?.ReadExisting());
    }

    private void Counter_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        awaiting = watch.ElapsedMilliseconds <= counter.Interval*5;
        if (port.IsOpen) port?.Write("$"); else awaiting = false;
    }

    public void Dispose()
    {
        SimpleLogger.Write($"{port?.PortName} disposing.");
        
        watch.Stop();

        counter.Elapsed -= Counter_Elapsed;
        counter.Stop();
        counter.Close();
        counter.Dispose();

        port.DataReceived -= Serial_DataReceived;
        port?.Close();
        port?.Dispose();
    }
}