# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /App
COPY . ./
RUN dotnet restore "StorageProject.sln"
RUN dotnet test "./tests/StorageProject.Tests.Controller/StorageProject.Tests.Controller.csproj"
RUN dotnet test "./tests/StorageProject.Tests.Services/StorageProject.Tests.Services.csproj"
RUN dotnet publish "./src/StorageProject.Api/StorageProject.Api.csproj" -c Release -o /App/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /App

# Instala New Relic
RUN apt-get update && apt-get install -y wget ca-certificates gnupg \
 && echo 'deb http://apt.newrelic.com/debian/ newrelic non-free' | tee /etc/apt/sources.list.d/newrelic.list \
 && wget https://download.newrelic.com/548C16BF.gpg \
 && apt-key add 548C16BF.gpg \
 && apt-get update \
 && apt-get install -y 'newrelic-dotnet-agent' \
 && rm -rf /var/lib/apt/lists/*


# Define no runtime
ENV NEW_RELIC_LICENSE_KEY="" \
    CORECLR_ENABLE_PROFILING=1 \
    CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A} \
    CORECLR_NEWRELIC_HOME=/usr/local/newrelic-dotnet-agent \
    CORECLR_PROFILER_PATH=/usr/local/newrelic-dotnet-agent/libNewRelicProfiler.so \
    NEW_RELIC_APP_NAME="StorageApp"

# Copia publicação
COPY --from=build-env /App/publish .

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:8000
EXPOSE 8000
ENTRYPOINT ["dotnet", "StorageProject.Api.dll"]
