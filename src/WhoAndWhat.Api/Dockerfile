#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/WhoAndWhat.Api/WhoAndWhat.Api.csproj", "src/WhoAndWhat.Api/"]
COPY ["src/WhoAndWhat.Application/WhoAndWhat.Application.csproj", "src/WhoAndWhat.Application/"]
COPY ["src/WhoAndWhat.Domain/WhoAndWhat.Domain.csproj", "src/WhoAndWhat.Domain/"]
COPY ["src/WhoAndWhat.Infrastructure/WhoAndWhat.Infrastructure.csproj", "src/WhoAndWhat.Infrastructure/"]
RUN dotnet restore "src/WhoAndWhat.Api/WhoAndWhat.Api.csproj"
COPY . .
WORKDIR "/src/src/WhoAndWhat.Api"
RUN dotnet build "WhoAndWhat.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WhoAndWhat.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WhoAndWhat.Api.dll"]
