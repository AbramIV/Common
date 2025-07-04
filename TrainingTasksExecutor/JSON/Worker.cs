// Worker worker = new("293950142", "37977", "8f418962aa7bd9730c1cba15822286d3", 82, 82, 1631831285748, 1632175943570);

namespace Exercise.JSON;

public class Worker
{
    //public string? WorkerSkillId { get; set; } = workerSkillId;
    //public string SkillId { get; set; } = skillId;
    //public string WorkerId { get; set; } = workerId;
    //public int SkillValue { get; set; } = skillValue;
    //public int? SkillExactValue { get; set; } = skillExactValue;
    //public long? CreateTs { get; set; } = createTs;
    //public long? UpdateTs { get; set; } = updateTs;

    public string? WorkerSkillId { get; set; }
    public string SkillId { get; set; }
    public string WorkerId { get; set; }
    public int SkillValue { get; set; }
    public int? SkillExactValue { get; set; }
    public long? CreateTs { get; set; }
    public long? UpdateTs { get; set; }

    public Worker() { }

    public Worker(int skillId, Guid workerId)
    {
        WorkerSkillId = null;
        SkillId = skillId.ToString(); // Random.Shared.Next(37900, 37903).ToString();
        WorkerId = workerId.ToString(); // Guid.NewGuid().ToString();
        SkillValue = Random.Shared.Next(93, 100);
        SkillExactValue = null;
        CreateTs = null;
        UpdateTs = null;
    }

    public Worker(string workerSkillId, string skillId, string workerId, int skillValue, int skillExactValue, long createTs, long updateTs)
    {
        //WorkerSkillId = workerSkillId;
        SkillId = skillId;
        WorkerId = workerId;
        SkillValue = skillValue;
        //SkillExactValue = skillExactValue;
        //CreateTs = createTs;
        //UpdateTs = updateTs;
    }
}