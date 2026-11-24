namespace Infrastructure.Persistent.EF.UserAgg;

public class OtpChallenge
{
    public string PhoneNumber { get; set; } = string.Empty;
    public string CodeHash { get; set; } = string.Empty;
    public DateTime ExpiresAtUtc { get; set; }
    public DateTime LastSentAtUtc { get; set; }
    public int Attempts { get; set; }
    public bool Consumed { get; set; }
}
