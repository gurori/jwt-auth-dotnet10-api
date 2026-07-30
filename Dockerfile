FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Application/Application.csproj Application/
COPY Core/Core.csproj Core/
COPY DataAccess/DataAccess.csproj DataAccess/
COPY Infrastructure/Infrastructure.csproj Infrastructure/
COPY Server/Server.csproj Server/


RUN dotnet restore Server/Server.csproj

COPY . .
WORKDIR /src/Server
RUN dotnet publish -c Release --no-restore -o /app

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=build /app/ ./

ENTRYPOINT ["dotnet", "Server.dll"]