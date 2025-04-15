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

    internal static void Draw(Cell cell, char? symbol = null)
    {
        if (cell is null) return;

        SetCursorPosition(cell.X, cell.Y);
        Write(symbol??cell.View);
    }
    internal static void Erase(Cell cell) => Draw(cell, ' ');

    internal static void SetColor(ConsoleColor color) => ForegroundColor = color;
}
