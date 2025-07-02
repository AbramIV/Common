using CommonLib;
using static System.Console;

ForegroundColor = ConsoleColor.Green;

for (int i = 0; i <= 25; i++)
    WriteLine($"{i} = {Math.Round(Maths.ProportionalValue(i, 0, 25, 3, 100), 1)}");

WriteLine("Done!");
ReadLine();