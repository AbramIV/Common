using CommonLib;
using System.IO.Ports;

namespace AutoAsmLogger.Helpers;

internal class SerialPortsScanner : IDisposable
{
    private readonly System.Timers.Timer limiter;

    private readonly string request;
    private readonly string response;

    private bool isAwaiting = false;
    private bool isTimeout = false;
    private bool isCorrect = false;

    internal SerialPortsScanner(string initRequest = "INIT$", string awaitedResponse = "OK$", int timeout = 1000)
    {
        request = initRequest;
        response = awaitedResponse;

        limiter = new(timeout) { AutoReset = false };
        limiter.Elapsed += Limiter_Elapsed;
    }

    internal IEnumerable<SerialPort?>? Scan(int speed)
    {
        List<SerialPort> ports = new();

        if (SerialPort.GetPortNames().Length.Equals(0)) return null;

        foreach (var name in SerialPort.GetPortNames())
        {
            SerialPort port = new(name, speed, Parity.None, 8, StopBits.One);

            port.DataReceived += SerialPort_DataReceived;

            isAwaiting = true;

            try
            {
                port.Open();
                SimpleLogger.Write($"{name}: opened.", LogLevel.Debug);
                Thread.Sleep(3000);
                port.DiscardInBuffer();
                port.DiscardOutBuffer();
                port.Write(request);
                SimpleLogger.Write($"{name}: init request sent.", LogLevel.Debug);

                limiter.Start();
                SimpleLogger.Write($"{name}: awaiting response.", LogLevel.Debug);

                while (isAwaiting);

                if (isTimeout)
                    throw new TimeoutException("Response timeout.");

                if (isCorrect)
                    SimpleLogger.Write($"{name} connected.");
                else
                    throw new ArgumentException("Response is incorrect.");
            }
            catch (ArgumentException ex)
            {
                port.Close();
                SimpleLogger.Write($"{name}: {ex.Message}", LogLevel.Error);
                continue;
            }
            catch (TimeoutException ex)
            {
                port.Close();
                SimpleLogger.Write($"{name}: {ex.Message}", LogLevel.Warning);
                continue;
            }
            catch (Exception ex)
            {
                port.Close();
                SimpleLogger.Write($"{name}: {ex.Message}", LogLevel.Error);
                continue;
            }
            finally
            {
                if (limiter.Enabled) limiter?.Stop();
                isAwaiting = false;
                isTimeout = false;
                isCorrect = false;
            }

            port.DataReceived -= SerialPort_DataReceived;
            ports.Add(port);
        }

        SimpleLogger.Write($"Scanning finished.");
        return ports.ToArray();
    }

    private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        limiter?.Stop();
        var name = ((SerialPort)sender).PortName;
        var data = ((SerialPort)sender).ReadExisting();
        SimpleLogger.Write($"{name} response: {data}.", LogLevel.Debug);
        isCorrect = data.Trim().Equals(response);
        isAwaiting = false;
    }

    private void Limiter_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        limiter?.Stop();
        isAwaiting = false;
        isTimeout = true;
    }

    public void Dispose()
    {
        limiter?.Close();
        limiter?.Dispose();
    }
}