using AutoSnake.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AutoSnake.Models;

internal class Navigator
{
    private readonly Border _border;
    private readonly Snake _snake; 

    internal Navigator(Border border, Snake snake)
    {
        _border = border;
        _snake = snake;
    }

    internal List<Cell> BuildRoute(Cell cell)
    {
        List<Cell> route = [];

        if (_snake.Head.X > cell.X)
            for (int i = _snake.Head.X - 1; i >= cell.X; i--) route.Add(new(i, _snake.Head.Y, CellView.Route));
        else
            for (int i = _snake.Head.X + 1; i <= cell.X; i++) route.Add(new(i, _snake.Head.Y, CellView.Route));

        Cell last = route.Last();

        if (_snake.Head.Y > cell.Y)
            for (int i = _snake.Head.Y-1; i > cell.Y; i--) route.Add(new(last.X, i, CellView.Route));
        else
            for (int i = _snake.Head.Y+1; i < cell.Y; i++) route.Add(new(last.X, i, CellView.Route));

        return route;
    }

    internal Direction NextCellDirection(Cell cell)
    {
        return Direction.Down;
    }
}
