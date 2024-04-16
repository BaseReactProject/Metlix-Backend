

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Person : Entity<int>
{
    public Person(int id, string name) : base(id)
    {
        Name = name;
    }
    public Person()
    {
        Name = string.Empty;
    }

    public string Name { get; set; }
}
