FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/BulletinBoard/Hosts/BulletinBoard.Hosts.Migrations/*.csproj", "src/BulletinBoard/Hosts/BulletinBoard.Hosts.Migrations/"]
COPY ["src/BulletinBoard/Application/BulletinBoard.Application/BulletinBoard.Application.AppServices.csproj", "src/BulletinBoard/Application/BulletinBoard.Application/"]
COPY ["src/BulletinBoard/Contracts/BulletinBoard.Contracts/BulletinBoard.Contracts.csproj", "src/BulletinBoard/Contracts/BulletinBoard.Contracts/"]
COPY ["src/BulletinBoard/Domain/BulletinBoard.Domain/BulletinBoard.Domain.csproj", "src/BulletinBoard/Domain/BulletinBoard.Domain/"]
COPY ["src/BulletinBoard/Infrastructure/BulletinBoard.Infrastructure.DataAccess/BulletinBoard.Infrastructure.DataAccess.csproj", "src/BulletinBoard/Infrastructure/BulletinBoard.Infrastructure.DataAccess/"]
COPY ["src/BulletinBoard/Infrastructure/BulletinBoard.Infrastructure/*.csproj", "src/BulletinBoard/Infrastructure/BulletinBoard.Infrastructure/"]
RUN dotnet restore "src/BulletinBoard/Hosts/BulletinBoard.Hosts.Migrations/BulletinBoard.Hosts.Migrations.csproj"
COPY . .
WORKDIR "/src/src/BulletinBoard/Hosts/BulletinBoard.Hosts.Migrations"
RUN dotnet build "BulletinBoard.Hosts.Migrations.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BulletinBoard.Hosts.Migrations.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BulletinBoard.Hosts.Migrations.dll"]