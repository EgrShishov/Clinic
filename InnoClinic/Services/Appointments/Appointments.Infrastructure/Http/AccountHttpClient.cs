
public class AccountHttpClient : IAccountsHttpClient
{
    private readonly HttpClient _httpClient;
    public AccountHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<AccountResponse> GetAccountInfoAsync(int AccountId)
    {
        throw new NotImplementedException();
    }
}
