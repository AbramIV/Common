using System;
using System.IO;

namespace CommonLib;

public static class SimpleLogger
{
    private static readonly string path;
    private static ConsoleColor MainConsoleForegroundColor { get; set; } = ConsoleColor.Gray;

    static SimpleLogger() => path = Environment.CurrentDirectory + @"\logs.log";

    public static void SetMainConsoleForegroundColor(ConsoleColor color) => MainConsoleForegroundColor = color;

    public static void Write(string text, LogLevel level = LogLevel.Info)
    {
        File.AppendAllText(path, $"{DateTime.Now} | {level} | {text}\r\n");

        Console.ForegroundColor = level switch
        {
            LogLevel.Warning => ConsoleColor.Yellow,
            LogLevel.Debug => ConsoleColor.Magenta,
            LogLevel.Error => ConsoleColor.Red,
            LogLevel.Fatal => ConsoleColor.DarkRed,
            _ => ConsoleColor.Gray,
        };

        Console.WriteLine($"{DateTime.Now} | {level} | {text}");
        Console.ForegroundColor = MainConsoleForegroundColor;
    }
}

public enum LogLevel
{
    Info,
    Warning,
    Debug,
    Error,
    Fatal
}
