using CommonLib;
using System.Diagnostics;
using System.IO.Ports;
using System.Text;
using System.Timers;
using static System.Console;

string[] Ports = SerialPort.GetPortNames();
string Path = Environment.CurrentDirectory + $"\\{DateTime.Today:d}";
string Folder = string.Empty;
string FileName = "record.txt";
string FolderPath = string.Empty;
string FilePath = string.Empty;
string Port = "COM";
string Text = string.Empty;
string Value = string.Empty;
System.Timers.Timer Counter = new(200);
Counter.AutoReset = true;
Counter.Elapsed += CounterElapsed;
Stopwatch sw = new();
SerialPort uart = new(Port, 250000, Parity.None, 8, StopBits.One) { Handshake = Handshake.None, Encoding = Encoding.ASCII };
uart.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
SignalHandler signalHandler = HandleConsoleSignal;
ConsoleHelper.SetSignalHandler(signalHandler, true);
SimpleLogger.Write("The application has been started");
sw.Start();

if (!Directory.Exists(Path))
{
    Directory.CreateDirectory(Path);
    SimpleLogger.Write($"Directory created: {Path}");
}

while (Ports.Length < 1)
{
    WriteLine("No available ports\nPress any key to refresh or 0 to exit...\n");
    if (int.TryParse(ReadLine(), out int zero) && zero == 0)
    {
        SimpleLogger.Write("Win32 event has been called");
        sw.Stop();
        SimpleLogger.Write($"Execution time: {sw.ElapsedMilliseconds} ms");
        SimpleLogger.Write("The application has been terminated");
        Environment.Exit(0);
    }

    Ports = SerialPort.GetPortNames();
}

Ports.ToList().ForEach(p => WriteLine(p));

while (true)
{
    WriteLine("Select ordered number of COM port: ");
    if (int.TryParse(ReadLine(), out int port))
    {
        if ((port < 1) || (port > Ports.Length))
        {
            WriteLine("Incorrect number", ConsoleColor.Red);
            SimpleLogger.Write($"Incorrect number: {port}");
            continue;
        }

        Port = Ports[port - 1];
        uart.PortName = Port;
        SimpleLogger.Write($"Choosen {Ports[port - 1]}");
        break;
    }

    WriteLine("Incorrect symbol", ConsoleColor.Red);
}

while (true)
{
    try
    {
        WriteLine("Folder: ");
        Folder = ReadLine()?.Trim();
        FolderPath = $"{Path}\\{Folder}";
        if (Directory.Exists(FolderPath)) throw new Exception("Folder already exist!");
        Directory.CreateDirectory(FolderPath);
        FilePath = $"{FolderPath}\\{FileName}";
        SimpleLogger.Write($"Set data path: {FilePath}");
        break;
    }
    catch (Exception ex)
    {
        WriteLine(ex.Message, ConsoleColor.Red);
    }
}

try
{
    string message = string.Empty;
    uart.Open();
    uart.DiscardInBuffer();
    ReadLine();
    WriteLine("Record...", ConsoleColor.Red);
    Text = File.ReadAllText(FilePath);
    //SplitValue(',', '.', ',');
    SplitValues('$');
}
catch (Exception ex)
{
    SimpleLogger.Write(ex.Message, CommonLib.LogLevel.Error);
    WriteLine(ex.Message, ConsoleColor.Red);
    ReadKey();
}
finally
{
    SimpleLogger.Write("Win32 event has been called");
    uart.Close();
    uart.Dispose();
    SimpleLogger.Write($"{(string.IsNullOrEmpty(uart.PortName) ? "UART" : uart.PortName)} disposed");
    sw.Stop();
    SimpleLogger.Write($"Execution time: {sw.ElapsedMilliseconds} ms");
    SimpleLogger.Write("The application has been terminated");
}

void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
{
    string receive = string.Empty;
    SerialPort com = (SerialPort)sender;
    receive = com.ReadExisting();

    try
    {
        File.AppendAllText(FilePath, receive);
        WriteLine(receive);
    }
    catch (Exception ex)
    {
        WriteLine(ex.Message, ConsoleColor.Red);
    }
}

void HandleConsoleSignal(ConsoleSignal consoleSignal)
{
    SimpleLogger.Write("Win32 event has been called");
    uart.Close();
    uart.Dispose();
    SimpleLogger.Write($"{(uart.PortName.Equals("COM") ? "UART" : uart.PortName)} disposed");
    sw.Stop();
    SimpleLogger.Write($"Execution time: {sw.ElapsedMilliseconds} ms");
    SimpleLogger.Write("The application has been terminated");
}

void CounterElapsed(object sender, ElapsedEventArgs e)
{
    if (Value.Length >= 3) WriteLine(Value);
}

void SplitValue(char splitter, char oldChar, char newChar)
{
    File.Delete(FilePath);

    foreach (string value in Text.Split(splitter))
        File.AppendAllText(FilePath, value.Trim().Replace(oldChar, newChar) + "\r\n");
}

void SplitValues(char splitter)
{
    string temp = string.Empty;

    foreach (string value in Text.Split(splitter))
    {
        temp = value.Trim().Replace('.', ',');
        if (temp.StartsWith('A')) File.AppendAllText($"{FolderPath}\\Aramid.txt", temp.Replace("A", "") + "\r\n");
        else if (temp.StartsWith('P')) File.AppendAllText($"{FolderPath}\\Polyamide.txt", temp.Replace("P", "") + "\r\n");
        else if (temp.StartsWith('T')) File.AppendAllText($"{FolderPath}\\Temperature.txt", temp.Replace("T", "") + "\r\n");
        else if (temp.StartsWith('H')) File.AppendAllText($"{FolderPath}\\Humidity.txt", temp.Replace("H", "") + "\r\n");
        else if (temp.StartsWith('D')) File.AppendAllText($"{FolderPath}\\Direction.txt", temp.Replace("D", "") + "\r\n");
        else File.AppendAllText($"{FolderPath}\\Unrecognized.txt", temp + "\r\n");
    }
}