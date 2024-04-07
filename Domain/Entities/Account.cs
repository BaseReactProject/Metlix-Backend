

using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Domain.Entities;
public class Account : Entity<int>
{
    public Account()
    {
        FakeId = string.Empty;
        UserId = 0;
    }
    public Account(string fakeId, int userId)
    {
        FakeId = fakeId;
        UserId = userId;
    }

    public string FakeId { get; set; }
    public int UserId { get; set; }
    public virtual User? User { get; set; }
}
