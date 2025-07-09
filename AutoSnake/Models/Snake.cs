using AutoSnake.Enums;
using System.Reflection.Metadata.Ecma335;

namespace AutoSnake.Models;

internal class Snake
{
   internal event Action PositionChanged;

    internal readonly BodyCell Head;
    internal readonly Queue<BodyCell> Body;

    internal BodyCell? Neck { get; private set; }
    internal BodyCell? Tail { get; private set; }
    internal Cell? Track { get; private set; }
    internal bool IsAlive { get; private set; }

    internal Snake(int x, int y)
    {
        Head = new(x, y, CellView.Head);
        Body = new([Head]);
        Track = new(0, 0, CellView.Empty);
        IsAlive = true;
    }

    internal void Step(Cell cell)
    {
        if (Body.Where(c => c.X == cell.X && c.Y == cell.Y).Any())
        {
            Suicide();
            return;
        }

        Track.SetPosition(Body.Last().X, Body.Last().Y);
        Head.SetPosition(cell.X, cell.Y);
        PositionChanged?.Invoke();
    }

    internal void Eat()
    {
        if (Tail is null)
        {
            Tail = new(Track.X, Track.Y, CellView.Tail, Head);
            Body.Enqueue(Tail);

            return;
        }

        if (Neck is null)
        {
            Neck = Tail;
            Neck.ChangeView(CellView.Body);
        }

        Tail.ChangeView(CellView.Body);
        Tail = new(Track.X, Track.Y, CellView.Tail, Tail);
        Body.Enqueue(Tail);
    }

    internal void Suicide()
    {
        IsAlive = false;
        Head.ChangeView(CellView.Break);
    }
}

internal class PositionChangedEventArgs : EventArgs
{
    //internal int X { get; set; }
    //internal int Y { get; set; }
}