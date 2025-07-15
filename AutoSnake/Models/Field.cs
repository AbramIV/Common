using AutoSnake.Enums;
using AutoSnake.Models.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSnake.Models;

internal class Field
{
    internal int MinX {  get; init; }
    internal int MinY { get; init; }
    internal int MaxX { get; init; }
    internal int MaxY { get; init; }
    internal Cell Center { get; init; }
    internal Cell[] Border { get; init; }

    internal Field(int minX, int minY, int width, int height)
    {
        List<Cell> cells = [];

        MinX = minX; 
        MinY = minY;
        MaxX = width;
        MaxY = height;
        Center = new(MaxX/2, MaxY/2);

        cells.AddRange(Enumerable.Range(0, MaxX).Select(i => new Cell(i, 0, CellView.Horizontal)));
        cells.AddRange(Enumerable.Range(0, MaxX).Select(i => new Cell(i, MaxY, CellView.Horizontal)));
        cells.AddRange(Enumerable.Range(0, MaxY+1).Select(i => new Cell(0, i, CellView.Vertical)));
        cells.AddRange(Enumerable.Range(0, MaxY+1).Select(i => new Cell(MaxX, i, CellView.Vertical)));

        Border = [.. cells];
    }

    internal bool BorderContains(Cell cell) => Border.Where(c => c.X == cell.X && c.Y == cell.Y).Any();
}
