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
    private static void Draw(int x, int y, char symbol = ' ')
    {
        SetCursorPosition(x, y);
        Write(symbol);
    }

    internal static void DrawCell(Cell cell) => Draw(cell.X, cell.Y, cell.View);
    internal static void DrawSymbol(Cell cell, char symbol) => Draw(cell.X, cell.Y, symbol);
    internal static void Erase(Cell cell) => Draw(cell.X, cell.Y);
    internal static void SetColor(ConsoleColor color) => ForegroundColor = color;
}
