using Microsoft.AspNetCore.Identity;

public class Account : IdentityUser<int>
{
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public int PhotoId { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string RefreshToken { get; set; }
}
