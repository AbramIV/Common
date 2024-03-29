﻿using System.Runtime.InteropServices;

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
}
