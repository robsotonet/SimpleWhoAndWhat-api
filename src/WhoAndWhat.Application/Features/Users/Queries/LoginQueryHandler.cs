using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WhoAndWhat.Application.Features.Users.Dtos;
using WhoAndWhat.Domain.Entities;

namespace WhoAndWhat.Application.Features.Users.Queries;

public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthTokensDto>
{
    private readonly IConfiguration _configuration;
    // private readonly IUserRepository _userRepository; // TODO: Inject repository

    public LoginQueryHandler(IConfiguration configuration /*, IUserRepository userRepository*/)
    {
        _configuration = configuration;
        // _userRepository = userRepository;
    }

    public Task<AuthTokensDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        // TODO: 1. Get user by email from repository
        // var user = await _userRepository.GetByEmailAsync(request.Email);
        // if (user is null)
        // {
        //     throw new UnauthorizedAccessException("Invalid credentials.");
        // }
        User user = new User { Email = request.Email, PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password) }; // Placeholder

        // 2. Verify password
        var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (!isPasswordValid)
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        // 3. Generate tokens
        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();

        // TODO: 4. Save refresh token to user in database
        // user.RefreshToken = refreshToken;
        // user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_configuration.GetValue<int>("JwtSettings:RefreshTokenExpiryDays"));
        // await _userRepository.UpdateAsync(user);

        return Task.FromResult(new AuthTokensDto(accessToken, refreshToken));
    }

    private string GenerateAccessToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("subscriptionTier", user.SubscriptionTier.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("JwtSettings:AccessTokenExpiryMinutes")),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
