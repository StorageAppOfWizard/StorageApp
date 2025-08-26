# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY StorageProject.sln .
COPY ["src/StorageProject.Api/StorageProject.Api.csproj", "src/StorageProject.Api/"]
COPY ["src/StorageProject.Application/StorageProject.Application.csproj", "src/StorageProject.Application/"]
COPY ["src/StorageProject.Domain/StorageProject.Domain.csproj", "src/StorageProject.Domain/"]
COPY ["src/StorageProject.Infrastructure/StorageProject.Infrastructure.csproj", "src/StorageProject.Infrastructure/"]
COPY ["tests/StorageProject.Tests.Controller/StorageProject.Tests.Controller.csproj", "tests/StorageProject.Tests.Controller/"]
COPY ["tests/StorageProject.Tests.Services/StorageProject.Tests.Services.csproj", "tests/StorageProject.Tests.Services/"]

RUN dotnet restore "./StorageProject.sln"
COPY . .

RUN dotnet test "StorageProject.sln" --no-restore --verbosity normal --collect:"XPlat Code Coverage"

WORKDIR "/src/src/StorageProject.Api"
RUN dotnet build "./StorageProject.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./StorageProject.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StorageProject.Api.dll"]