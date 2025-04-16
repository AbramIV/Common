using AutoSnake.Enums;
using AutoSnake.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AutoSnake.Models;

internal class Snake
{
    internal event EventHandler<PositionChangedEventArgs>? PositionChanged;

    internal readonly Body Head;
    internal readonly Queue<Body> Body = new();
    internal Body? Tail { get; private set; }
    internal Cell? Track { get; private set; }

    internal Direction CurrentDirection { get; private set; }
    internal Direction LastDirection { get; private set; }

    internal Snake(int x, int y)
    {
        Head = new(x, y, Views.Head);
        Head.PositionChanged += PositionChanged;
        Body.Enqueue(Head);
        CurrentDirection = Direction.Stop;
    }

    internal void Eat()
    {
        if (Tail is null) Head.PositionChanged -= PositionChanged;
        else Body.Last().PositionChanged -= PositionChanged;

        Tail = new(Track.X, Track.Y, Views.Body, Body.Last());

        Body.Enqueue(Tail);

        Track = null;
    }
    internal void Step()
    {
        int x = Head.X, y = Head.Y;

        if (CurrentDirection == Direction.Stop) return;

        LastDirection = CurrentDirection;

        switch (CurrentDirection)
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
        }

        Track = new(Body.Last().X, Body.Last().Y, Views.Empty);
        Head.SetPosition(this, new() { X = x, Y = y });
        OnPositionChanged(new() { X = Track.X, Y = Track.Y });
    }
    internal void SetDirection(Direction direction) => CurrentDirection = direction;
    protected virtual void OnPositionChanged(PositionChangedEventArgs e) => PositionChanged?.Invoke(this, e);
}
