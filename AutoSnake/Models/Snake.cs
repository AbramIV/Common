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
    internal Direction Direction { get; private set; }
    internal bool IsAlive { get; private set; }

    internal Snake(int x, int y)
    {
        Head = new(x, y, CellView.Head);
        Body = new([Head]);
        Track = new(0, 0, CellView.Empty);
        Direction = Direction.Stop;
        IsAlive = true;
    }

    internal void Step()
    {
        int x, y;

        x = Head.X;
        y = Head.Y;

        switch (Direction)
        {
            case Direction.Up:
                y--;
                break;
            case Direction.Down:
                y++;
                break;
            case Direction.Left:
                x--;
                break;
            case Direction.Right:
                x++;
                break;
            default:
                return;
        }

        if (Body.Where(c => c.X == x && c.Y == y).Any())
        {
            Suicide();
            return;
        }

        Track.SetPosition(Body.Last().X, Body.Last().Y);
        Head.SetPosition(x, y);
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

    internal void SetDirection(Direction direction) => Direction = direction;

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