public interface IPasswordGenerator
{
    public string GeneratePassword(int Length, int NumberOfNonAlphanumericCharacters);
}
