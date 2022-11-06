using PostgresLib;
using PostgresLib.Types;
using System;
using System.IO;

namespace CommonLib
{
    public static class SimpleLogger
    {
        private static readonly string Path;
        private static bool PrintToConsole = false;
        private static bool WriteToDatabase = false;

        static SimpleLogger() => Path = Environment.CurrentDirectory + @"\logs.log";

        public static void PrintToConsoleOn() => PrintToConsole = true;
        public static void PrintToConsoleOff() => PrintToConsole = false;
        public static void WriteToDatabaseOn() => WriteToDatabase = true;
        public static void WriteToDatabaseOff() => WriteToDatabase = false;
        public static async void Write(string text, LogLevel level = LogLevel.Info)
        {
            File.AppendAllText(Path, $"{DateTime.Now} | {level} | {text}\r\n");

            if (PrintToConsole)
            {
                Console.ForegroundColor = level switch
                {
                    LogLevel.Warning => ConsoleColor.Yellow,
                    LogLevel.Error => ConsoleColor.Red,
                    LogLevel.Fatal => ConsoleColor.DarkRed,
                    _ => ConsoleColor.Gray,
                };

                Console.WriteLine($"{DateTime.Now} | {level} | {text}");
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            if (WriteToDatabase)
            {
                try
                {
                    var result = await DAL.InsertLog(text, level);
                    if (result.Equals(0)) throw new Exception("Write log to database is failed. Method has returned 0.");
                }
                catch (Exception ex)
                {
                    WriteToDatabaseOff();
                    Write(ex.Message, LogLevel.Error);
                    WriteToDatabaseOn();
                }
            }
        }
    }
}
