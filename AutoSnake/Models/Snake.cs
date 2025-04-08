using AutoSnake.Enums;
using AutoSnake.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSnake.Models;

internal class Snake
{
    internal readonly Head Head;

    private readonly Queue<Cell> body = new();
    private readonly bool isOnlyHead = true;

    internal Snake(int x, int y)
    {
        Head = new Head(x, y, (char)Symbols.Body);
        body.Enqueue(Head);

        Drawer.Draw(Head);
    }

    internal void StepLeft()
    {
        if (isOnlyHead) Drawer.Erase(Head);

        Head.SetView((char)Symbols.Left);
        Head.SetPosition(Head.X-1, Head.Y);

        Drawer.Draw(Head);
    }

    internal void Eat()
    {
        Body part = new(body.Last().X, body.Last().Y, (char)Symbols.Body, body.Last());
    }
}
