using Exercise;
using System;

Console.Title = "Exercises";
Console.ForegroundColor = ConsoleColor.Green;

CancellationTokenSource cts = new();
var token = cts.Token;
token.Register(() => Console.WriteLine("Operation cancelled."));

Console.WriteLine("Start async operation...");
var result = Exercises.GetRequestAsync("https://template.postman-echo.com/info?id=1", token);

Thread.Sleep(1000);

//Console.WriteLine("Canceling operation...");
//cts.Cancel();

Console.WriteLine("\nDone!");

Console.ReadKey();