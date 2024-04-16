

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Notice : Entity<int>
{
    public Notice(int id, string name) : base(id)
    {
        Name = name;
    }
    public Notice()
    {
        Name = string.Empty;
    }

    public string Name { get; set; }
}
