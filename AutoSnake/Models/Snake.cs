using AutoSnake.Enums;
using AutoSnake.Models.Cells;
using System.Reflection.Metadata.Ecma335;

namespace AutoSnake.Models;

internal class Snake
{
    internal event Action PositionChanged;

    internal readonly List<BodyCell> body;

    internal Snake(Cell cell)
    {
        body = new([new(cell.X, cell.Y, CellView.Head)]);
    }

    internal void Move()
    {
        //Head.SetPosition(cell.X, cell.Y);
        PositionChanged?.Invoke();
    }

    internal void Grow()
    {

    }
}