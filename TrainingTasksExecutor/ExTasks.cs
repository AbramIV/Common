using System.Text;

namespace TrainingTasksExecutor;

internal class ExTasks
{
    private static readonly Random Randomizer;

    static ExTasks() => Randomizer = new Random((int)DateTime.Now.Ticks);

    #region common solution (without Linq, static methods of Array)
    /// <summary>
    /// The method is reverse words letters in array of char. Order of words will the same.
    /// </summary>
    internal static void ReverseWords()
    {
        char[] array = new char[] { 's', 'i', 'h', 'T', ' ', 's', 'i', ' ', 'o', 'l', 'l', 'e', 'H', ' ', 'd', 'l', 'r', 'o', 'W' };
        char[] newArray = new char[array.Length];
        char[] buffer = new char[array.Length];
        int pos = 0;

        for (var i = 0; i < array.Length; i++)
        {
            if (i + 1 == array.Length)
            {
                newArray[i] = array[i];
                break;
            }

            if (array[i] == ' ')
            {
                newArray[i] = ' ';
                continue;
            }

            if (array[i + 1] == ' ')
            {
                newArray[i] = array[i];
                newArray[i + 1] = ' ';
                i++;
                continue;
            }

            for (var j = i; j < array.Length; j++)
            {
                buffer[pos] = array[j];
                if (j + 1 == array.Length || array[j + 1] == ' ')
                {
                    while (pos >= 0)
                    {
                        newArray[i] = buffer[pos];
                        pos--;
                        i++;
                    }

                    buffer = new char[array.Length];
                    pos = 0;
                    break;
                }
                pos++;
                if (j + 1 == array.Length) break;
            }
        }

        foreach (var chr in newArray)
            Console.Write(chr);
    }

    /// <summary>
    /// Output is sum of even numbers in array.
    /// </summary>
    internal static void EvenNumbersSum(int MaxValue)
    {
        int result = 0;
        int[] array = new int[Randomizer.Next(MaxValue)];

        for (var i = 0; i < array.Length; i++) array[i] = Randomizer.Next(MaxValue);

        foreach (var number in array) if (number % 2 == 0) result += number;

        Console.WriteLine(result);
    }

    /// <summary>
    /// Http request example
    /// </summary>
    internal static void WebRequestContentExample()
    {
        string data = ""; 
        string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");


        Stream stream = new MemoryStream();
        string formData = "application/json"; 
        byte[] byte1 = Encoding.UTF8.GetBytes("" + Environment.NewLine +
            "Content-Disposition: form-data; name=\"json\"; "
            + Environment.NewLine + "Content-Type: "
            + formData + Environment.NewLine + Environment.NewLine);
        stream.Write(byte1, 0, byte1.Length);
        byte[] byte2 = Encoding.UTF8.GetBytes(data + Environment.NewLine
        + Environment.NewLine + "--" + boundary + "--" + Environment.NewLine
        + Environment.NewLine);
        stream.Write(byte2, 0, byte2.Length);
    }

    /// <summary>
    /// Reverse number
    /// </summary>
    /// <param name="Number">Initial number</param>
    /// <param name="Limitation">Only 10,100,1000.....</param>
    /// <returns>Reversed initial number</returns>
    internal static short ReverseNumber(int Number, int Limitation)
    {
        if (Number < Limitation) throw new Exception($"The initial number exceed limitation: {Limitation}");

        return (short)((Number % 10 * 10) + (Number / 10));
    }

    #endregion
}

