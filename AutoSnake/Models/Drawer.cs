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
    internal static void ConsoleInit()
    {
        Title = "Snake";
        CursorVisible = false;
        WriteLine("Choose window size and press any button to continue...");
        AwaitUserInput();
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
        if (cell == null) return;

        DrawCell(new(cell.X, cell.Y, CellView.Empty));
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
    internal static void WriteOnCellPosition(Cell cell, string message)
    {
        SetCursorPosition(cell.X, cell.Y);
        WriteLine(message);
    }

    internal static void AwaitUserInput() => ReadKey();

    internal static int Width => WindowWidth;
    internal static int Height => WindowHeight;
}
