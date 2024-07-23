public interface IAccountService
{
    public Task<AccountResponse> GetAccountByIdAsync(int AccountId);
    public Task<CreateAccountResponse> CreateAccountAsync(CreateAccountRequest request);
}
