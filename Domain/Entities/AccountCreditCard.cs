

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class AccountCreditCard : Entity<int>
{
    public AccountCreditCard()
    {
        AccountId = 0;
        NumberHash = Array.Empty<byte>();
        NumberSalt = Array.Empty<byte>();
        ExpiryDateHash = Array.Empty<byte>();
        ExpiryDateSalt = Array.Empty<byte>();
        CVVHash= Array.Empty<byte>();
        CVVSalt= Array.Empty<byte>();
        HolderNameHash= Array.Empty<byte>();
        HolderNameSalt= Array.Empty<byte>();
    }

    public AccountCreditCard(int accountId, byte[] numberHash, byte[] numberSalt, byte[] expiryDateHash, byte[] expiryDateSalt, byte[] cVVHash, byte[] cVVSalt, byte[] holderNameHash, byte[] holderNameSalt, int id) : base(id)
    {
        AccountId = accountId;
        NumberHash = numberHash;
        NumberSalt = numberSalt;
        ExpiryDateHash = expiryDateHash;
        ExpiryDateSalt = expiryDateSalt;
        CVVHash = cVVHash;
        CVVSalt = cVVSalt;
        HolderNameHash = holderNameHash;
        HolderNameSalt = holderNameSalt;
    }

    public int AccountId { get; set; }
    public byte[] NumberHash { get; set; }
    public byte[] NumberSalt { get; set; }
    public byte[] ExpiryDateHash { get; set; }
    public byte[] ExpiryDateSalt { get; set; }
    public byte[] CVVHash { get; set; }
    public byte[] CVVSalt { get; set; }
    public byte[] HolderNameHash { get; set; }
    public byte[] HolderNameSalt { get; set; }
    public virtual Account? Account { get; set; }

}
