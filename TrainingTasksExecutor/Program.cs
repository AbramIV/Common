using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml;
using TrainingTasksExecutor;
using TrainingTasksExecutor.Grouping;
using TrainingTasksExecutor.Http;
using TrainingTasksExecutor.Inheritance;
using TrainingTasksExecutor.JsonSamples;
using TrainingTasksExecutor.OOP;

Console.Title = "Exercises";
Console.ForegroundColor = ConsoleColor.Green;

int num = 1;
Console.WriteLine($"0b{num:b}");

int result = num << 2;
Console.WriteLine($"0b{result:b}");

Console.WriteLine("\nDone!");

Console.ReadKey();