

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Quality : Entity<int>
{
    public Quality()
    {
        Name = string.Empty;
        Value= 0;
    }

    public Quality(string name, int value, int id) : base(id)
    {
        Name = name;
        Value = value;
    }

    public string Name { get; set; }
    public int Value { get; set; }
}
