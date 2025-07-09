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
    private readonly Snake snake;
    private readonly Cell begin;
    private readonly Cell finish;
    private readonly int width;
    private readonly int height;
    private readonly int[][] directions =
    [
        [0, 1],  // Up
        [1, 0],  // Right
        [0, -1], // Down
        [-1, 0]  // Left
    ];

    internal Navigator(Cell fieldBeginCell, Cell fieldEndCell, Snake currentSnake)
    {
        begin = fieldBeginCell;
        finish = fieldEndCell;
        snake = currentSnake;
        width = finish.X;
        height = finish.Y;
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

    internal List<Cell> Navigate1(Cell food)
    {
        List<Cell> route = [];

        return route;
    }

    internal List<Cell> Navigate(Cell end)
    {
        Cell start = snake.Head;
        var queue = new Queue<Cell>();
        var visited = new bool[width, height];
        var parentMap = new Dictionary<Cell, Cell>();

        queue.Enqueue(start);
        visited[start.X, start.Y] = true;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current.Equals(end))
                break;

            foreach (var dir in directions)
            {
                int nx = current.X + dir[0];
                int ny = current.Y + dir[1];

                if (nx >= 1 && nx < width && ny >= 1 && ny < height && !visited[nx, ny])
                {
                    var neighbor = new Cell(nx, ny);
                    queue.Enqueue(neighbor);
                    visited[nx, ny] = true;
                    parentMap[neighbor] = current;
                }
            }
        }

        // Reconstruct the path
        var path = new List<Cell>();
        if (!parentMap.Where(kvp => kvp.Key.X == end.X && kvp.Key.Y == end.Y).Any() && !(start.X == end.X && start.Y == end.Y))
            return path; // No path found

        for (var step = end; !(step.X == start.X && step.Y == start.Y); step = parentMap.Where(kvp => kvp.Key.X == step.X && kvp.Key.Y == step.Y).FirstOrDefault().Value)
            path.Add(step);

        path.Reverse();

        foreach(var p in path) p.ChangeView(CellView.Route);

        return path;
    }
}