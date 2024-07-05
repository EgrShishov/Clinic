﻿
using System.Security.Claims;

namespace Auth.Application.Common.Abstractions
{
    public interface ITokenGenerator
    {
        string GenerateAccessToken(Account account);
        string GenerateRefreshToken(Account account);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
