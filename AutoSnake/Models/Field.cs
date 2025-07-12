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
    internal int Width { get; init; }
    internal int Height { get; init; }
    internal Cell[] Border { get; init; }

    internal Field(int minX, int minY, int width, int height)
    {
        List<Cell> cells = [];

        MinX = minX; 
        MinY = minY;
        Width = width;
        Height = height;

        cells.AddRange(Enumerable.Range(0, Width).Select(i => new Cell(i, 0, CellView.Horizontal)));
        cells.AddRange(Enumerable.Range(0, Width).Select(i => new Cell(i, Height, CellView.Horizontal)));
        cells.AddRange(Enumerable.Range(0, Height).Select(i => new Cell(0, i, CellView.Vertical)));
        cells.AddRange(Enumerable.Range(0, Height).Select(i => new Cell(Width, i, CellView.Vertical)));

        Border = [.. cells];
    }
}
