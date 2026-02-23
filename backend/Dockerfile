# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /App
COPY . ./
RUN dotnet restore "StorageProject.sln"
RUN dotnet test "tests/StorageProject.Tests.Controller/StorageProject.Tests.Controller.csproj"
#RUN dotnet test "tests/StorageProject.Tests.Services/StorageProject.Tests.Services.csproj"
RUN dotnet publish "./src/StorageProject.Api/StorageProject.Api.csproj" -c Release -o /App/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /App


# Copia publicação
COPY --from=build-env /App/publish .

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:8000
EXPOSE 8000
ENTRYPOINT ["dotnet", "StorageProject.Api.dll"]
