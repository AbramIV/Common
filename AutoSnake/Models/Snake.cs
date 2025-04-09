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
    internal Body Neck;
    internal readonly Queue<Cell> Body = new();
    internal Body Tail { get; private set; }
    internal  Cell FirstTrack { get; private set; }
    internal Cell LastTrack { get; private set; }

    internal Snake(int x, int y)
    {
        Head = new Head(x, y, Views.Head);
        Head.PositionChanged += PositionChanged;
        Body.Enqueue(Head);
    }

    internal void Eat()
    {
        if (Neck is null)
        {
            Neck = new(FirstTrack.X, FirstTrack.Y, Views.Body, Head);
            Head.PositionChanged -= PositionChanged;
            Neck.PositionChanged += PositionChanged;
            Body.Enqueue(Neck);
        }

        if (Tail is null)
        {
            Tail = new(FirstTrack.X, FirstTrack.Y, Views.Body, Neck);
            Neck.PositionChanged -= PositionChanged;
            Tail.PositionChanged += PositionChanged;
            Body.Enqueue(Tail);
        }

        //else Tail.PositionChanged -= PositionChanged;

        ////Tail = new Body(Track.X, Track.Y, Views.Body, Body.Last());
        //Tail.PositionChanged += PositionChanged;
        //Body?.Enqueue(Tail);
    }

    internal void StepRight() => Step(Head.X + 1, Head.Y);
    internal void StepLeft() => Step(Head.X - 1, Head.Y);
    internal void StepUp() => Step(Head.X, Head.Y - 1);
    internal void StepDown() => Step(Head.X, Head.Y + 1);
    private void Step(int x, int y)
    {
        LastTrack = FirstTrack;
        FirstTrack = new(Body.Last().X, Body.Last().Y, Views.Body);
        Head.SetPosition(x, y);
    }

    private void PositionChanged(int x, int y)
    {
        StepFinished?.Invoke(x, y);
    }
}
