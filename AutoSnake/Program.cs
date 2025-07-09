using AutoSnake.Enums;
using AutoSnake.Models;
using System.Timers;
using static System.Console;
using StepTimer = System.Timers.Timer;

Title = "AutoSnake";
CursorVisible = false;
ForegroundColor = ConsoleColor.Magenta;
WriteLine("Set window size and press any key to start...");
ReadKey();
Clear();

Border border = new(WindowWidth, WindowHeight);
Snake snake = new((WindowWidth / 2) - 5, WindowHeight / 2); // snake start position
Feeder feeder = new(WindowWidth, WindowHeight); // cell generator
Navigator navigator = new(new(1, 1), new(border.Width-2, border.Height-2), snake);
Cell food = feeder.Spawn(snake.Body);
List<Cell>? route = navigator.Navigate(food); //.GetRoute(food);
snake.PositionChanged += Snake_PositionChanged;
int stepNumber = 0;

DrawCells(border.BorderCells, ConsoleColor.Blue);
DrawCell(snake.Head);
DrawCell(food);
DrawCells(route, ConsoleColor.Green);

while (snake.IsAlive)
{
    snake.Step(route[stepNumber++]);

    Thread.Sleep(100);
}

SetCursorPosition(WindowWidth / 2 - 5, WindowHeight / 2);
Write("GAME OVER");

ReadKey();

void Snake_PositionChanged()
{
    if (border.BorderCells.Where(c => c.X == snake.Head.X && c.Y == snake.Head.Y).Any()) snake.Suicide();

    if (snake.Head.X == food.X && snake.Head.Y == food.Y)
    {
        snake.Eat();

        food = feeder.Spawn(snake.Body);
        DrawCell(food);

        route = navigator.Navigate(food);
        DrawCells(route, ConsoleColor.Green);
        stepNumber = 0;
    }

    DrawCell(snake.Head);
    DrawCell(snake.Neck);
    DrawCell(snake.Tail);
    EraseCell(snake.Track);
}

static void DrawCells(IEnumerable<Cell> cells, ConsoleColor color)
{
    ConsoleColor last = ForegroundColor;
    ForegroundColor = color;

    if (!cells.Any()) return;

    _ = cells.All(c => { DrawCell(c); return true; });

    ForegroundColor = last;
}
static void DrawCell(Cell cell, char? symbol = null)
{
    if (cell is null || cell.View == CellView.None) return;

    SetCursorPosition(cell.X, cell.Y);
    Write(symbol ?? (char)cell.View);
}
static void EraseCell(Cell cell) => DrawCell(cell, ' ');
static void EraseCells(IEnumerable<Cell> cells) => _ = cells.All(c => { EraseCell(c); return true; });

static void Test()
{
    int width = WindowWidth - 2;
    int height = WindowHeight - 2;

    for (int i = 1; i <= width;  i++)
        for (int j = 1; j <= height; j++)
        {
            SetCursorPosition(i, j);
            Write("#");
        }
}