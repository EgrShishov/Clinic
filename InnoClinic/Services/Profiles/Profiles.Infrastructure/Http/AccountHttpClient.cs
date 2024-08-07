using ErrorOr;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

public class AccountHttpClient : IAccountHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    public AccountHttpClient(HttpClient httpClient, IConfiguration configuration)
    {  
        _httpClient = httpClient; 
        _configuration = configuration;
    }

    public async Task<ErrorOr<AccountResponse>> CreateAccount(CreateAccountRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_configuration["IdentityAPI"]}/create", request);
        if (!response.IsSuccessStatusCode)
        {

        }
        return await response.Content.ReadFromJsonAsync<AccountResponse>();
    }

    public async Task<ErrorOr<Unit>> DeleteAccount(int AccountId)
    {
        var response = await _httpClient.DeleteAsync($"{_configuration["IdentityAPI"]}/{AccountId}");
    }

    public async Task<ErrorOr<AccountResponse>> GetAccountInfo(int AccountId)
    {
        throw new NotImplementedException();
    }
}