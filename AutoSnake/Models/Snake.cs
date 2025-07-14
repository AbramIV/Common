using AutoSnake.Enums;
using AutoSnake.Models.Cells;
using System.Reflection.Metadata.Ecma335;

namespace AutoSnake.Models;

internal class Snake
{
    internal event Action PositionChanged;

    internal readonly List<BodyCell> Body;

    internal bool IsAlive { get; private set; }

    internal Snake(Cell cell)
    {
        Body = new([new(cell.X, cell.Y, CellView.Head)]);
        IsAlive = true;
    }

    internal void Move(Cell cell)
    {
        Body.First().SetPosition(cell.X, cell.Y);

        PositionChanged?.Invoke();
    }

    internal void Grow()
    {

    }

    internal void Die() => IsAlive = false;

    internal bool Contains(Cell cell) => Body.Where(c => c.X == cell.X &&  c.Y == cell.Y).Any();
}