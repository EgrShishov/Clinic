using Microsoft.Extensions.Configuration;
using Auth.Application.Common.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Auth.Application.Queries.GenerateEmailConfirmationLink
{
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
            var emailConfirmationLink = $"{baseUrl}/api/account/confirm-email?userId={request.Account.Id}&token={Uri.EscapeDataString(confirmationToken)}";
            return emailConfirmationLink;
        }
    }
}
