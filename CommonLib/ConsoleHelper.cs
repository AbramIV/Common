using System.Runtime.InteropServices;

namespace CommonLib
{
    public delegate void SignalHandler(ConsoleSignal consoleSignal);

    public enum ConsoleSignal
    {
        CtrlC = 0,
        CtrlBreak = 1,
        Close = 2,
        LogOff = 5,
        Shutdown = 6
    }

    public static class ConsoleHelper
    {
        static ConsoleHelper() { }

        [DllImport("Kernel32", EntryPoint = "SetConsoleCtrlHandler")]
        public static extern bool SetSignalHandler(SignalHandler handler, bool add);
    }

    public readonly static int CurrentThreadId = Environment.CurrentManagedThreadId;
    public readonly static string CurrentDirectory = Environment.CurrentDirectory;
    public readonly static string CurrentUser = Environment.UserName;

    public static void Initialization(ConsoleColor fontColor = ConsoleColor.Gray, string title = "ConsoleApp")
    {
        SetColor(fontColor);
        SetTitle($"{title} [{CurrentUser}]");
        SimpleLogger.PrintToConsoleOff();
        SimpleLogger.Write("Start.");
        SimpleLogger.Write($"Current directory: \"{CurrentDirectory}\".");
        SimpleLogger.Write($"Current user: \"{CurrentUser}\".");
    }

    public static void SetTitle(string title) => Console.Title = title;

    public static void SetColor(ConsoleColor color) => Console.ForegroundColor = color;

    public static void Print(string message, ConsoleColor color)
    {
        var current = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ForegroundColor = current;
    }

    public static void Print(string message) => Console.Write(message);

    public static void PrintLine(int message, ConsoleColor color)
        => PrintLine(message.ToString(), color);

    public static void PrintLine(double message, ConsoleColor color)
        => PrintLine(message.ToString(), color);

    public static void PrintLine(stringm message, ConsoleColor color)
    {
        var current = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ForegroundColor = current;
    }

    public static void PrintLine(string message) => Console.WriteLine(message);
    public static void PrintLine(int message) => Console.WriteLine(message);
    public static void PrintLine(double message) => Console.WriteLine(message);

    public static void ParseArgs()
    {
        var args = Environment.GetCommandLineArgs();

        if (args is null || args.Length < 1) return;
    }
}