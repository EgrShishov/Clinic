public class IdentityMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RefreshTokenRequest, RefreshTokenCommand>()
            .Map(dest => dest.RefreshToken, src => src.refreshToken)
            .Map(dest => dest.AccessToken, src => src.accessToken);

        config.NewConfig<SignInRequest, SignInCommand>()
            .Map(dest => dest.Role, src => src.role)
            .Map(dest => dest.Email, src => src.email)
            .Map(dest => dest.Password, src => src.password);

        config.NewConfig<SignUpRequest, SignUpCommand>()
            .Map(dest => dest.Role, src => src.role)
            .Map(dest => dest.Email, src => src.email)
            .Map(dest => dest.Password, src => src.password)
            .Map(dest => dest.ReenteredPassword, src => src.reentered_password);
    }
}
