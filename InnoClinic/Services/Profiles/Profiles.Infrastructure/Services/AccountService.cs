using System.Net;
using System.Net.Http.Json;

public class AccountService : IAccountService
{
    private readonly HttpClient _httpClient;
    public AccountService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<CreateAccountResponse> CreateAccountAsync(CreateAccountRequest request)
    {
        using var response = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/create-profile", request);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            return await response.Content.ReadFromJsonAsync<CreateAccountResponse>();
        }

        return null; //change to errors
    }

    public async Task<AccountResponse> GetAccountByIdAsync(int AccountId)
    {
        var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/{AccountId}");

        if (response.StatusCode == HttpStatusCode.OK)
        {
            return await response.Content.ReadFromJsonAsync<AccountResponse>();
        }

        return null; //change to errors;
    }
}
