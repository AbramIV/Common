using AutoSnake.Enums;
using AutoSnake.Models.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace AutoSnake.Models;

internal static class Drawer
{
    static Drawer()
    {
        Title = "Snake";
        CursorVisible = false;
    }

    internal static void EraseCells(IEnumerable<Cell> cells)
    {
        if (!cells.Any()) return;

        _ = cells.All(c => { EraseCell(c); return true; });
    }
    internal static void DrawCells(IEnumerable<Cell> cells, ConsoleColor color = ConsoleColor.Gray)
    {
        if (!cells.Any()) return;

        _ = cells.All(c => { DrawCell(c, color); return true; });
    }
    internal static void EraseCell(Cell cell)
    {
        cell.ChangeView(CellView.Empty);
        DrawCell(cell);
    }
    internal static void DrawCell(Cell cell, ConsoleColor color = ConsoleColor.Gray)
    {
        if (cell is null || cell.View == CellView.None) return;

        ConsoleColor last = ForegroundColor;

        ForegroundColor = color;

        SetCursorPosition(cell.X, cell.Y);
        Write((char)cell.View);

        ForegroundColor = last;
    }
    internal static void PrintLine(string message) => WriteLine(message);
    internal static void Print(string message) => Write(message);

    internal static int Width => WindowWidth;
    internal static int Height => WindowHeight;
}
