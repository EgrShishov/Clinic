using Microsoft.AspNetCore.Identity;

public class UpdateAccountCommandHandler(
    IMediator mediator,
    IUnitOfWork unitOfWork,
    UserManager<Account> manager,
    ITokenGenerator tokenService
    )
    : IRequestHandler<UpdateAccountCommand, ErrorOr<UpdateAccountResponse>>
{
    public async Task<ErrorOr<UpdateAccountResponse>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        if (!await unitOfWork.AccountRepository.EmailExistsAsync(request.Email))
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var account = await unitOfWork.AccountRepository.GetByEmailAsync(request.Email);

        if (request.NewPassword != request.ReenteredNewPassword)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var emailChangeToken = "";
        var phoneNumberChangeToken = "";

        await manager.ChangeEmailAsync(account, request.Email, emailChangeToken);
        await manager.ChangePasswordAsync(account, request.OldPassword, request.NewPassword);
        await manager.ChangePhoneNumberAsync(account, request.PhoneNumber, phoneNumberChangeToken);

        var accessToken = tokenService.GenerateAccessToken(account);
        var refreshToken = tokenService.GenerateRefreshToken(account);

        account.RefreshToken = refreshToken;
        account.UpdatedBy = request.ReceptionistId;
        account.UpdatedAt = DateTime.UtcNow;

        await manager.UpdateAsync(account);

        var roles = await manager.GetRolesAsync(account);
        var role = roles.Contains("Doctor") ? "Doctor" :
                roles.Contains("Receptionist") ? "Receptionist" : "Patient";

        return new UpdateAccountResponse(
            account.PhoneNumber,
            account.Email,
            account.PhotoId,
            account.UpdatedAt,
            account.UpdatedBy,
            accessToken, 
            refreshToken,
            role);
    }
}
