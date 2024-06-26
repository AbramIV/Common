using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using System.Transactions;
using TrainingTasksExecutor.JsonSamples;

namespace TrainingTasksExecutor.Grouping;

internal class GroupingSamples
{
    private readonly static string input = "C:\\Main\\Repositories\\Common\\TrainingTasksExecutor\\JsonSamples\\Input.json";
    private readonly static string output = "C:\\Main\\Repositories\\Common\\TrainingTasksExecutor\\JsonSamples\\Output.json";
    private readonly static Guid[] workersIds = Enumerable.Range(1, 1000).Select(w => Guid.NewGuid()).ToArray();
    private readonly static int[] skillsIds = [37514, 37515, 37977];
    private readonly static Tuple<string, int>[][] groups = GetGroupsArrays(skillsIds);

    internal static void GroupsDistribution()
    {
        GenerateWorkers();

        var workersSkills = JsonSerializer.Deserialize<IEnumerable<Worker>>(File.ReadAllText(input));

        var workers = from w in workersSkills
                      group w by w.WorkerId into g
                      select new
                      {
                          Id = g.Key,
                          Skills = (from p in g select Tuple.Create(p.SkillId, p.SkillValue)).ToArray()
                      };

        List<WorkerGroup> groupped = [];

        foreach(var worker in workers)
            for(int i = 0; i < groups.Length; i++)
                if (groups[i].SequenceEqual(worker.Skills))
                {
                    groupped.Add(new WorkerGroup(worker.Id, i + 1));
                    break;
                }

        var json = JsonSerializer.Serialize(groupped);

        File.Delete(output);
        File.WriteAllText(output, json);

        Console.WriteLine(File.ReadAllText(output));
    }

    private static Tuple<string, int>[][] GetGroupsArrays(int[] skillsIds)
    {
        return [[Tuple.Create($"{skillsIds[0]}", 95), Tuple.Create($"{skillsIds[1]}", 93), Tuple.Create($"{skillsIds[2]}", 93)],
                [Tuple.Create($"{skillsIds[0]}", 95), Tuple.Create($"{skillsIds[1]}", 93), Tuple.Create($"{skillsIds[2]}", 95)],
                [Tuple.Create($"{skillsIds[0]}", 95), Tuple.Create($"{skillsIds[1]}", 95), Tuple.Create($"{skillsIds[2]}", 95)],
                [Tuple.Create($"{skillsIds[0]}", 98), Tuple.Create($"{skillsIds[1]}", 95), Tuple.Create($"{skillsIds[2]}", 95)],
                [Tuple.Create($"{skillsIds[0]}", 98), Tuple.Create($"{skillsIds[1]}", 98), Tuple.Create($"{skillsIds[2]}", 98)]];
    }

    private static void GenerateWorkers()
    {
        List<Worker> workers = [];

        foreach (var workerId in workersIds)
            foreach (var skillsId in skillsIds)
                workers.Add(new(skillsId, workerId));

        var json = JsonSerializer.Serialize(workers);

        File.Delete(input);
        File.WriteAllText(input, json);
    }
}
