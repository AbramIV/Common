using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prompter;

internal class Hint
{
    public string Name { get; set; }

    public string Text { get; set; }

    public Hint() { }

    public Hint(string name, string text)
    {
        Name = name;
        Text = text;
    }
}
