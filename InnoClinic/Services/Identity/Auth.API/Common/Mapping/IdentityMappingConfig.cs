public class IdentityMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RefreshTokenRequest, RefreshTokenCommand>()
            .Map(dest => dest.RefreshToken, src => src.RefreshToken)
            .Map(dest => dest.AccessToken, src => src.AccessToken);

        config.NewConfig<SignInRequest, SignInCommand>()
            .Map(dest => dest.Role, src => src.Role)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Password, src => src.Password);

        config.NewConfig<SignUpRequest, SignUpCommand>()
            .Map(dest => dest.Role, src => src.Role)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Password, src => src.Password)
            .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
            .Map(dest => dest.CreatedBy, src => src.CreatedBy)
            .Map(dest => dest.ReenteredPassword, src => src.ReenteredPassword);
    }
}
