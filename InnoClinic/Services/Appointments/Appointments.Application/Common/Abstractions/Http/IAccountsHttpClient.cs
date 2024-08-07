public interface IAccountsHttpClient
{
    public Task<AccountResponse> GetAccountInfoAsync(int AccountId);
}