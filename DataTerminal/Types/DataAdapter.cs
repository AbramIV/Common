using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTerminal.ViewModels;

namespace DataTerminal.Types;

internal class DataAdapter
{
    internal string Initializer { get; private set; }
    internal string Separator { get; private set; }
    internal string Finalizer { get; private set; }

    internal DataAdapter(string initializer = "!", string separator = "$", string finalizer = "#") 
    {
        Initializer = initializer;
        Separator = separator;
        Finalizer = finalizer;
    }

    internal bool Parse(string input)
    {


        return false;
    }
}
