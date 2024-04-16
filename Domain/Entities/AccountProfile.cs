

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class AccountProfile : Entity<int>
{
    public int AccountId { get; set; }
    public int ProfileId { get; set; }
    public virtual Account? Account { get; set; }
    public virtual Profile? Profile { get; set; }
}