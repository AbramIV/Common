using AutoSnake.Enums;
using AutoSnake.Models.Cells;
using System.ComponentModel;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace AutoSnake.Models;

internal class Navigator
{
    private readonly Snake snake;
    private readonly Field field;
    private readonly Cell[] directions;
    internal bool Track { get; set; }

    internal Navigator(Field field, Snake snake)
    {
        this.snake = snake;
        this.field = field;
        //directions = [new(0, 1), new(0, -1), new(1, 0), new(-1, 0), new(1, 1), new(-1, -1), new(-1, 1), new(1, -1)];
        directions = [new(1, 0), new(-1, 0), new(0, 1), new(0, -1)];
    }

    /// <summary>
    /// Shortest way by BSF algorithm.
    /// </summary>
    /// <param name="end">Target point.</param>
    /// <returns>Cells from start point to target point.</returns>
    internal List<Cell> FindPath(Cell end)
    {
        List<Cell> path = [];
        List<Cell> visited = [];
        Queue<Cell> queue = new([snake.Body.First()]);
        Dictionary<Cell, Cell?> map = new() { { snake.Body.First(), null } };

        while (queue.Count > 0)
        {
            var vertex = queue.Dequeue();

            if (visited.Where(v => v.Equals(vertex)).Any()) continue;

            visited.Add(vertex);

            if (vertex.Equals(end)) break;

            if (!vertex.Equals(snake.Body.First()) && Track)
                Drawer.DrawCell(vertex, ConsoleColor.Blue);

            foreach (var direction in directions)
            {
                Cell neighbor = new(vertex.X + direction.X, vertex.Y + direction.Y, CellView.Path);

                if (field.BorderContains(neighbor) || snake.Contains(neighbor)) continue;

                queue.Enqueue(neighbor);
                map.Add(neighbor, vertex);
            }
        }

        var key = map.Where(kvp => kvp.Key.Equals(end)).First().Key.ChangeView(CellView.None); // null check

        while (!key.Equals(snake.Body.First()))
        {
            if (Track)
            {
                Drawer.DrawCell(key, ConsoleColor.White);
                Thread.Sleep(1);
            }

            path.Add(key);

            key = map[key];
        }

        path.Reverse();
        if (Track) Drawer.DrawCells(path, ConsoleColor.Green);

        foreach (var v in visited)
        {
            if (v.Equals(snake.Body.First()) || v.Equals(end) || path.Where(c => c.Equals(v)).Any())
                continue;

            Drawer.EraseCell(v);
        }

        return path;
    }
}