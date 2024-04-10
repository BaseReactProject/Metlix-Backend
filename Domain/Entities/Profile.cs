

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Profile : Entity<int>//max count 5 in one account
{
    public Profile(string name, byte[] passwordHash, byte[] passwordSalt, int ımageId, int id) :base(id)
    {
        Name = name;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        ImageId = ımageId;
    }
    public Profile()
    {
        Name = string.Empty;
        PasswordHash = Array.Empty<byte>();
        PasswordSalt = Array.Empty<byte>();
        ImageId = 0;
    }

    public string Name { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public int ImageId { get; set; }
    public virtual Image? Image { get; set; }
    public virtual ICollection<AccountProfile>? AccountProfiles { get; set; }
}
