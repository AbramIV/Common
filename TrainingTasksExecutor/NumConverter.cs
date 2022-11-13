using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingTasksExecutor;

internal static class NumConverter
{ 
    internal static string DecimalToHex(int number)
    {
        int whole = 16 * (number / 16);
        int result = number - whole;

        if (result == 0)
        {
            // 1258
        }

        if (number / 16 >= 16)
        {
            return HexComparer(result) + DecimalToHex(whole);
        }

        return HexComparer(result).ToString();
    }

    private static string HexComparer(int number)
    {
        return number switch
        {
            10 => "A",
            11 => "B",
            12 => "C",
            13 => "D",
            14 => "E",
            15 => "F",
            _ => number.ToString(),
        };
    }
}