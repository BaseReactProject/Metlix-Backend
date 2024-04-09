

using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Domain.Entities;
public class Account : Entity<int>
{
    public Account()
    {
        FakeId = string.Empty;
        UserId = 0;
        PhoneNumber = string.Empty;
        PlanId = 0;
    }
    public Account(string fakeId, int userId, string phoneNumber, int planId)
    {
        FakeId = fakeId;
        UserId = userId;
        PhoneNumber = phoneNumber;
        PlanId = planId;
    }

    public string FakeId { get; set; }
    public int UserId { get; set; }
    public int PlanId { get; set; }
    public string PhoneNumber { get; set; }
    public virtual Plan? Plan { get; set; }
    public virtual User? User { get; set; }
    public virtual ICollection<AccountProfile>? AccountProfiles { get; set; }
    public virtual ICollection<AccountCreditCard>? AccountCreditCards { get; set; }
}
