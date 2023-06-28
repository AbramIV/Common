using System.Text;

namespace TrainingTasksExecutor;

internal class ExTasks
{
    private static readonly Random Randomizer;

    static ExTasks() => Randomizer = new Random((int)DateTime.Now.Ticks);

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

    internal static double? SmallestFullRoot(int[] unsortedArray)
    {
        int[] sortedArray = MergeSort(unsortedArray);

        foreach (int num in sortedArray)
        {
            var sqrt = Math.Sqrt(num);

            if ((sqrt - (int)sqrt) == 0) return sqrt;
        }

        return null;
    }

    internal static int[] MergeSort(int[] array)
    {
        if (array.Length == 1) return array;

        int middleIndex = array.Length / 2;
        int[] a = new int[array.Length - middleIndex];
        int[] b = new int[middleIndex];

        for (int i = 0; i < a.Length; i++)
        {
            a[i] = array[i];
            if (i < b.Length) b[i] = array[i + a.Length];
        }

        return MergeArrays(MergeSort(a), MergeSort(b));
    }

    internal static int[] MergeArrays(int[] a, int[] b)
    {
        int[] mergedArray = new int[a.Length + b.Length];

        int indexA = 0, indexB = 0;

        for (int i = 0; i < mergedArray.Length; i++)
        {
            if (indexA < a.Length && indexB < b.Length)
            {
                mergedArray[i] = a[indexA] > b[indexB] ? a[indexA++] : b[indexB++];
            }
            else
            {
                mergedArray[i] = indexB < b.Length ? b[indexB++] : a[indexA++];
            }
        }

        return mergedArray;
    }

    internal static bool IsSimpleNumber(int number)
    {
        for (int j = 2; j <= number / 2; j++)
            if (number % j == 0) return false;
        return true;
    }
}

