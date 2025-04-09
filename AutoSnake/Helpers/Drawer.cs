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

    internal static void DrawField()
    {
        CursorVisible = false;
        ForegroundColor = ConsoleColor.Red;

        for (int i = 0; i < WindowWidth - 1; i++)
        {
            SetCursorPosition(i, 0);
            Write('-');
            SetCursorPosition(i, WindowHeight - 1);
            Write('-');
        }

        for (int i = 1; i < WindowHeight - 1; i++)
        {
            SetCursorPosition(0, i);
            Write('|');
            SetCursorPosition(WindowWidth - 1, i);
            Write('|');
        }
    }

    internal static void DrawCell(Cell cell) => Draw(cell.X, cell.Y, cell.View);
    internal static void DrawSymbol(Cell cell, char symbol) => Draw(cell.X, cell.Y, symbol);
    internal static void Erase(Cell cell) => Draw(cell.X, cell.Y);
    internal static void SetColor(ConsoleColor color) => ForegroundColor = color;

    internal static void DrawSnake(Snake snake)
    {

    }
}
