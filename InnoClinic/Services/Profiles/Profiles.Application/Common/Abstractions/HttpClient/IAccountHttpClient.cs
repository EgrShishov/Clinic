public interface IAccountHttpClient
{
    public Task<ErrorOr<AccountResponse>> CreateAccount(CreateAccountRequest request);
    public Task<ErrorOr<Unit>> DeleteAccount(int AccountId);
    public Task<ErrorOr<AccountResponse>> GetAccountInfo(int AccountId);
}
