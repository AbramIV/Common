using System;

Console.Title = "Exercises";
Console.ForegroundColor = ConsoleColor.Green;

int num = 1;
Console.WriteLine($"0b{num:b}");

int result = num << 2;
Console.WriteLine($"0b{result:b}");

Console.WriteLine("\nDone!");

Console.ReadKey();