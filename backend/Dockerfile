FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /App

# Copia tudo do projeto
COPY . ./

# Restaura pacotes
RUN dotnet restore "StorageProject.sln"

# Publica o projeto Api
RUN dotnet publish "./src/StorageProject.Api/StorageProject.Api.csproj" -c Release -o /App/publish
# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /App

# Copia a pasta publicada
COPY --from=build-env /App/publish .

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:5173
EXPOSE 5173
ENTRYPOINT ["dotnet", "StorageProject.Api.dll"]

