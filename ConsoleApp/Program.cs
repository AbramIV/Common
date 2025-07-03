using CommonLib;
using static System.Console;

ForegroundColor = ConsoleColor.Green;

double signalMin = 4;
double signalMax = 20;
string signalLiteral = "mA";

double valueMin = -0.34;
double valueMax = 0.105;
string valueLiteral = "°C";

try
{
    for (double i = signalMin; i <= signalMax; i++)
        WriteLine($"{i} {signalLiteral} = {Math.Round(Maths.ProportionalValue(i, signalMin, signalMax, valueMin, valueMax), 3)} {valueLiteral}");
}
catch(Exception ex)
{
    ForegroundColor = ConsoleColor.Red;
    WriteLine(ex.Message);
    ForegroundColor = ConsoleColor.Green;
}

WriteLine("\nDone!");
ReadLine();