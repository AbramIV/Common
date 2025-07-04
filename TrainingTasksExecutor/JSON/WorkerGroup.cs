using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.JSON;

public class WorkerGroup
{
    public string WorkerId { get; set; }

    public int Group { get; set; }

    public WorkerGroup() { }

    public WorkerGroup(string workerId, int group) 
    {
        WorkerId = workerId;
        Group = group;
    }
}
