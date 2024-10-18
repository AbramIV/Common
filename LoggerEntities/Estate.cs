using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerEntities;

internal class Estate(int id, string name, string address, string description)
{
    public int Id;
    public string Name;
    public string Address;
    public string Description;
    public Device[] Devices = [];
}
