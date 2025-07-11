using AutoSnake.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AutoSnake.Models;

internal class Navigator
{
    private readonly Border border;
    private readonly Snake snake;
    private readonly int[][] directions =
    [
        [0, 1],
        [1, 0],
        [0, -1],
        [-1, 0]
    ];

    internal Navigator(Border currentBorder, Snake currentSnake)
    {
        border = currentBorder;
        snake = currentSnake;
    }

    internal List<Cell> GetRoute(Cell cell)
    {
        List<Cell> route = [];

        if (snake.Head.X > cell.X)
            for (int i = snake.Head.X - 1; i >= cell.X; i--) route.Add(new(i, snake.Head.Y, CellView.Route));
        else
            for (int i = snake.Head.X + 1; i <= cell.X; i++) route.Add(new(i, snake.Head.Y, CellView.Route));

        Cell last = route.Last();

        if (snake.Head.Y > cell.Y)
            for (int i = snake.Head.Y - 1; i >= cell.Y; i--) route.Add(new(last.X, i, CellView.Route));
        else
            for (int i = snake.Head.Y + 1; i <= cell.Y; i++) route.Add(new(last.X, i, CellView.Route));

        route.Last().ChangeView(CellView.None);

        return route;
    }

    internal List<Cell> Navigate(Cell end)
    {
        var queue = new Queue<Cell>([snake.Head]);
        var visited = new bool[border.Width, border.Height];
        var parentMap = new Dictionary<Cell, Cell>();

        visited[snake.Head.X, snake.Head.Y] = true;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current.Equals(end))
                break;

            foreach (var dir in directions)
            {
                int x = current.X + dir[0];
                int y = current.Y + dir[1];

                if (border.BorderCells.Where(c => c.X == x && c.Y == y).Any()) continue;
                if (snake.Body.Where(c => c.X == x && c.Y == y).Any()) continue;

                if (!visited[x, y])
                {
                    var neighbor = new Cell(x, y);
                    queue.Enqueue(neighbor);
                    visited[x, y] = true;
                    parentMap[neighbor] = current;
                }
            }
        }

        var path = new List<Cell>();
        if (!parentMap.Where(kvp => kvp.Key.X == end.X && kvp.Key.Y == end.Y).Any() && !(snake.Head.X == end.X && snake.Head.Y == end.Y))
            return path;

        for (var step = end; !(step.X == snake.Head.X && step.Y == snake.Head.Y); step = parentMap.Where(kvp => kvp.Key.X == step.X && kvp.Key.Y == step.Y).FirstOrDefault().Value)
            path.Add(step);

        path.Reverse();

        foreach (var p in path) p.ChangeView(CellView.Route);
        path.Last().ChangeView(CellView.None);

        return path;
    }

    internal List<Cell> Navigate1(Cell end)
    {
        (int, int)[] directions =
        [
            (0, 1),
            (1, 0),
            (0, -1),
            (-1, 0)
        ];
        var queue = new Queue<Cell>([snake.Head]);
        var visited = new List<Cell>();
        var map = new Dictionary<Cell, Cell>();
        var route = new List<Cell>();

        while (queue.Count > 0)
        {
            Cell cell = queue.Dequeue();

            if (cell.X == end.X && cell.Y == end.Y) break;

            foreach (var direction in directions)
            {
                int x = cell.X + direction.Item1;
                int y = cell.Y + direction.Item2;

                if (border.BorderCells.Where(c => c.X == x && c.Y == y).Any()) continue;
                if (snake.Body.Where(c => c.X == x && c.Y == y).Any()) continue;
                if (queue.Where(c => c.X == x && c.Y == y).Any()) continue;

                queue.Enqueue(new(x, y));
            }

            _ = visited.Any();
        }

        return route;
    }
}