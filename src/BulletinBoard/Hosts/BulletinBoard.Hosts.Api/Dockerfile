FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/BulletinBoard/Hosts/BulletinBoard.Hosts.Api/BulletinBoard.Hosts.Api.csproj", "src/BulletinBoard/Hosts/BulletinBoard.Hosts.Api/"]
COPY ["src/BulletinBoard/Application/BulletinBoard.Application/BulletinBoard.Application.AppServices.csproj", "src/BulletinBoard/Application/BulletinBoard.Application/"]
COPY ["src/BulletinBoard/Contracts/BulletinBoard.Contracts/BulletinBoard.Contracts.csproj", "src/BulletinBoard/Contracts/BulletinBoard.Contracts/"]
COPY ["src/BulletinBoard/Domain/BulletinBoard.Domain/BulletinBoard.Domain.csproj", "src/BulletinBoard/Domain/BulletinBoard.Domain/"]
COPY ["src/BulletinBoard/Infrastructure/BulletinBoard.Infrastructure.DataAccess/BulletinBoard.Infrastructure.DataAccess.csproj", "src/BulletinBoard/Infrastructure/BulletinBoard.Infrastructure.DataAccess/"]
COPY ["src/BulletinBoard/Infrastructure/BulletinBoard.Infrastructure/*.csproj", "src/BulletinBoard/Infrastructure/BulletinBoard.Infrastructure/"]
RUN dotnet restore "src/BulletinBoard/Hosts/BulletinBoard.Hosts.Api/BulletinBoard.Hosts.Api.csproj"
COPY . .
WORKDIR "/src/src/BulletinBoard/Hosts/BulletinBoard.Hosts.Api"
RUN dotnet build "BulletinBoard.Hosts.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BulletinBoard.Hosts.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BulletinBoard.Hosts.Api.dll"]