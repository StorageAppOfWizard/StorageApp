FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /App

# Copia tudo do projeto
COPY . ./

# Restaura pacotes
RUN dotnet restore "StorageProject.sln"

# Roda os testes
RUN dotnet test "./tests/StorageProject.Tests.Controller/StorageProject.Tests.Controller.csproj" -c Release --no-restore
RUN dotnet test "./tests/StorageProject.Tests.Services/StorageProject.Tests.Services.csproj" -c Release --no-restore

# Publica o projeto API
RUN dotnet publish "./src/StorageProject.Api/StorageProject.Api.csproj" -c Release -o /App/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /App

# Copia a pasta publicada
COPY --from=build-env /App/publish .

ENV ASPNETCORE_ENVIRONMENT=Development

# Porta que realmente está sendo usada
EXPOSE 8080

ENTRYPOINT ["dotnet", "StorageProject.Api.dll"]
