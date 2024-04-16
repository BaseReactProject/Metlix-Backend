﻿

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Genre : Entity<int>
{
    public Genre(int id, string name) : base(id)
    {
        Name = name;
    }
    public Genre()
    {
        Name = string.Empty;
    }

    public string Name { get; set; }
}
