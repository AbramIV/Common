using AutoSnake.Enums;
using AutoSnake.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSnake.Models;

internal class Snake
{
    internal event PositionChanged_EventHandler StepFinished;

    internal readonly Head Head;
    internal readonly Queue<Cell> Body = new();
    internal Body Tail { get; private set; }
    internal  Cell Track { get; private set; }

    internal Snake(int x, int y)
    {
        Head = new Head(x, y, Views.Head);
        Head.PositionChanged += PositionChanged;
        Body.Enqueue(Head);
    }

    internal bool Eat()
    {
        if (Tail is null)
        {
            Tail = new(Track.X, Track.Y, Views.Body, Head);
            Tail.SetView(Views.Body);
            Head.PositionChanged -= PositionChanged;
            Tail.PositionChanged += PositionChanged;
            Body.Enqueue(Tail);
        }

        Tail = new Body(Track.X, Track.Y, Views.Body, Body.Last());
        Tail.PositionChanged += PositionChanged;
        Body?.Enqueue(Tail);

        return true;
    }

    internal void StepRight() => Step(Head.X + 1, Head.Y);
    internal void StepLeft() => Step(Head.X - 1, Head.Y);
    internal void StepUp() => Step(Head.X, Head.Y - 1);
    internal void StepDown() => Step(Head.X, Head.Y + 1);
    private void Step(int x, int y)
    {
        Track = new(Body.Last().X, Body.Last().Y, Views.Empty);
        Head.SetPosition(x, y);
    }

    private void PositionChanged(int x, int y)
    {
        StepFinished?.Invoke(x, y);
    }
}
