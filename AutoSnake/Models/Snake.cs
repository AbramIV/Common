using AutoSnake.Enums;
using AutoSnake.Models.Cells;
using System.Reflection.Metadata.Ecma335;

namespace AutoSnake.Models;

internal class Snake
{
    internal event Action PositionChanged;

    internal readonly BodyCell Head;
    internal BodyCell Neck { get; private set; }
    internal BodyCell Tail { get; private set; }

    internal readonly List<BodyCell> Body;

    internal bool IsAlive { get; private set; }

    internal Snake(Cell cell)
    {
        Head = new(cell.X, cell.Y, CellView.Head);
        Body = new([Head]);
        IsAlive = true;
    }

    internal void Move(Cell cell)
    {
        Head.SetPosition(cell.X, cell.Y);

        PositionChanged?.Invoke();
    }

    internal void Grow(Cell track)
    {
        if (Tail is null)
        {
            Tail = new(Head.X, Head.Y, CellView.Tail, Head);
            Body.Add(Tail);
            Head.SetPosition(track.X, track.Y);

            return;
        }

        int lastTailX = Tail.X;
        int lastTailY = Tail.Y;

        Head.SetPosition(track.X, track.Y);
        Tail.ChangeView(CellView.Body);
        Neck ??= Tail;
        Tail = new(lastTailX, lastTailY, CellView.Tail, Tail);
        Body.Add(Tail);
    }

    internal void Die() => IsAlive = false;

    internal bool Contains(Cell cell) => Body.Where(c => c.X == cell.X &&  c.Y == cell.Y).Any();
}