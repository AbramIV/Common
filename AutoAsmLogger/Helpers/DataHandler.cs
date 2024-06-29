using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAsmLogger.Helpers
{
    internal class DataHandler
    {
        DataHandler() { }

        public void Handle(string path, char terminator)
        {
            if (File.Exists(path))
            {
                foreach (string value in File.ReadAllText(path).Replace('.', ',').Replace(" ", "").Trim().Split(terminator))
                {
                    if (value.StartsWith('A')) File.AppendAllText($"{path}\\Aramid.txt", value.Replace("A", "") + "\r\n");
                    else if (value.StartsWith('P')) File.AppendAllText($"{path}\\Polyamide.txt", value.Replace("P", "") + "\r\n");
                    else if (value.StartsWith('T')) File.AppendAllText($"{path}\\Temperature.txt", value.Replace("T", "") + "\r\n");
                    else if (value.StartsWith('H')) File.AppendAllText($"{path}\\Humidity.txt", value.Replace("H", "") + "\r\n");
                    else if (value.StartsWith('D')) File.AppendAllText($"{path}\\Difference.txt", value.Replace("D", "") + "\r\n");
                    else File.AppendAllText($"{path}\\Unrecognized.txt", value + "\r\n");
                }
            }

            //if (File.Exists(currentFile))
            //{
            //    foreach (string value in File.ReadAllText(currentFile).Replace('.', ',').Replace(" ", "").Trim().Split(splitter))
            //    {
            //        if (value.StartsWith('A')) File.AppendAllText($"{currentFolder}\\Aramid.txt", value.Replace("A", "") + "\r\n");
            //        else if (value.StartsWith('P')) File.AppendAllText($"{currentFolder}\\Polyamide.txt", value.Replace("P", "") + "\r\n");
            //        else if (value.StartsWith('T')) File.AppendAllText($"{currentFolder}\\Temperature.txt", value.Replace("T", "") + "\r\n");
            //        else if (value.StartsWith('H')) File.AppendAllText($"{currentFolder}\\Humidity.txt", value.Replace("H", "") + "\r\n");
            //        else if (value.StartsWith('D')) File.AppendAllText($"{currentFolder}\\Difference.txt", value.Replace("D", "") + "\r\n");
            //        else File.AppendAllText($"{currentFolder}\\Unrecognized.txt", value + "\r\n");
            //    }
            //}
        }
    }
}
