namespace WhoAndWhat.Application.Features.Users.Dtos;

public record AuthTokensDto(
    string AccessToken,
    string RefreshToken
);
