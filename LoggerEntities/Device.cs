namespace LoggerEntities;

public class Device(int id, int estateId, string name, int address)
{
    public readonly int Id = id;
    public readonly int EstateId = estateId;
    public readonly string Name = name;
    public readonly int Address = address;
    public Quantity[] Quantities = [];
}
