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
    internal event EventHandler<PositionChangedEventArgs>? PositionChanged;

    internal readonly Body Head;
    internal readonly Queue<Body> Body = new();
    internal Body? Tail { get; private set; }
    internal  Cell? Track { get; private set; }

    internal Snake(int x, int y)
    {
        Head = new(x, y, Views.Head);
        Head.PositionChanged += PositionChanged;
        Body.Enqueue(Head);
    }

    internal void Eat()
    {
        if (Tail is null) Head.PositionChanged -= PositionChanged;
        else Body.Last().PositionChanged -= PositionChanged;

        Tail = new(Track.X, Track.Y, Views.Body, Body.Last());

        Body.Enqueue(Tail);

        Track = null;
    }

    internal void StepRight() => Step(Head.X + 1, Head.Y);
    internal void StepLeft() => Step(Head.X - 1, Head.Y);
    internal void StepUp() => Step(Head.X, Head.Y - 1);
    internal void StepDown() => Step(Head.X, Head.Y + 1);
    private void Step(int x, int y)
    {
        Track = new(Body.Last().X, Body.Last().Y, Views.Empty);
        Head.SetPosition(this, new() { X = x, Y = y });
        OnPositionChanged(new() { X = Track.X, Y = Track.Y });
    }

    protected virtual void OnPositionChanged(PositionChangedEventArgs e) => PositionChanged?.Invoke(this, e);
}
