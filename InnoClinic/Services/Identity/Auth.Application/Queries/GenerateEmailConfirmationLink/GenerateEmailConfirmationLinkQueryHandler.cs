using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

public class GenerateEmailConfirmationLinkQueryHandler(
        UserManager<Account> userManager,
        IEmailSender emailSender,
        IConfiguration configuration)
        : IRequestHandler<GenerateEmailConfirmationLinkQuery, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(GenerateEmailConfirmationLinkQuery request, CancellationToken cancellationToken)
    {
        var confirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(request.Account);

        var baseUrl = configuration["AppSettings:BaseUrl"];
        var emailConfirmationLink = $"{baseUrl}/confirm-email?AccountId={request.Account.Id}&Token={Uri.EscapeDataString(confirmationToken)}";
        return emailConfirmationLink;
    }
}
