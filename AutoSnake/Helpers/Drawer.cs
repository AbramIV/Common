using AutoSnake.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace AutoSnake.Helpers;

internal static class Drawer
{
    internal static void Draw(Cell cell)
    {
        SetCursorPosition(cell.X, cell.Y);
        Write(cell.View);
    }

    internal static void Draw(Cell cell, char symbol)
    {
        SetCursorPosition(cell.X, cell.Y);
        Write(symbol);
    }

    internal static void Erase(Cell cell) => Draw(cell, ' ');

    internal static void SetColor(ConsoleColor color) => ForegroundColor = color;
}
