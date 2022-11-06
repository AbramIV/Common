using System.Xml.Linq;

Title = "Snake Game";
bool play = true;
bool hit = false;
bool stepGone = true;
Field field = new();

WriteLine("Set window size and press any key to start...");
ReadKey();

var borders = field.Init(ConsoleColor.Magenta);
Snake snake = new(borders, (WindowWidth / 2) - 5, WindowHeight / 2);
Food food = new(borders);
food.Generate(snake.Points);
System.Timers.Timer timer = new(50);
timer.Elapsed += Timer_Elapsed;
timer.Start();
CursorVisible = false;

while (play)
{
    if (!stepGone) continue;

    switch (ReadKey().Key)
    {
        case ConsoleKey.RightArrow:
            if (snake.Direction != Directions.Left)
                snake.Direction = Directions.Right;
            break;
        case ConsoleKey.LeftArrow:
            if (snake.Direction != Directions.Right)
                snake.Direction = Directions.Left;
            break;
        case ConsoleKey.UpArrow:
            if (snake.Direction != Directions.Down)
                snake.Direction = Directions.Up;
            break;
        case ConsoleKey.DownArrow:
            if (snake.Direction != Directions.Up)
                snake.Direction = Directions.Down;
            break;
        case ConsoleKey.Escape:
            play = false;
            break;
        default:
            break;
    }

    stepGone = false;

    if (hit)
    {
        ReadKey();
        Environment.Exit(0);
    }
}

timer.Stop();
SetCursorPosition(WindowWidth / 2 - 5, WindowHeight / 2);
Write("GAME EXIT");

ReadKey();

void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
{
    if (food.X.Equals(snake.Head.X) && food.Y.Equals(snake.Head.Y))
    {
        hit = snake.Step(true);
        food.Generate(snake.Points);
    }
    else
    {
        hit = snake.Step(false);
    }

    if (hit)
    {
        timer.Stop();
        SetCursorPosition((WindowWidth / 2) - 5, WindowHeight / 2);
        Write("GAME OVER!");
    }

    stepGone = true;
}

