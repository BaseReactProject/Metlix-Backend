

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class AccountProfile : Entity<int>
{
    public AccountProfile()
    {
        AccountId = 0;
        ProfileId = 0;
    }

    public AccountProfile(int accountId, int profileId, int id) : base(id)
    {
        AccountId = accountId;
        ProfileId = profileId;
    }

    public int AccountId { get; set; }
    public int ProfileId { get; set; }
    public virtual Account Account { get; set; } = new Account();
    public virtual Profile Profile { get; set; } = new Profile();
}