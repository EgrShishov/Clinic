using Microsoft.AspNetCore.Http;

public class CreateAccountRequest
{
    public IFormFile Photo { get; init; }
    public string Email {  get; init; }
    public string Password { get; init; }
    public string PhoneNumber {  get; init; }
    public string Role { get; init; }
    public int CreatedBy { get; init; }
}