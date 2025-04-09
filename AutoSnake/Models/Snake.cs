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
    internal readonly Tail Tail;
    internal  Cell Track { get; private set; }

    internal Snake(int x, int y)
    {
        Head = new Head(x, y, (char)Symbols.Head);
        Body.Enqueue(Head);
        Head.PositionChanged += PositionChanged;
    }

    internal Cell Eat()
    {
        if (Body?.Count > 1) Body.Last().PositionChanged -= PositionChanged;
        else Head.PositionChanged -= PositionChanged;

        Body?.Enqueue(new Body(Body.Last().X, Body.Last().Y, (char)Symbols.Body, Body.Last()));
        Body.Last().PositionChanged += PositionChanged;

        return Track;
    }

    internal void StepRight() => Step(Head.X + 1, Head.Y);
    internal void StepLeft() => Step(Head.X - 1, Head.Y);
    internal void StepUp() => Step(Head.X, Head.Y + 1);
    internal void StepDown() => Step(Head.X, Head.Y - 1);
    private void Step(int x, int y)
    {
        Track = new(Body.Last().X, Body.Last().Y, Body.Last().View);
        Head.SetPosition(x, y);
    }

    private void PositionChanged(int x, int y)
    {
        StepFinished?.Invoke(x, y);
    }
}
