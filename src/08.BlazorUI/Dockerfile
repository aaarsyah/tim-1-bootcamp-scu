FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src
# Copy csproj and restore as distinct layers
COPY ["src/08.BlazorUI/MyApp.BlazorUI.csproj", "08.BlazorUI/"]
COPY ["src/01.Base/MyApp.Base.csproj", "01.Base/"]
COPY ["src/02.Domain/MyApp.Domain.csproj", "02.Domain/"]
COPY ["src/03.Shared/MyApp.Shared.csproj", "03.Shared/"]
COPY ["src/04.Application/MyApp.Application.csproj", "04.Application/"]
COPY ["src/05.Infrastructure/MyApp.Infrastructure.csproj", "05.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "08.BlazorUI/MyApp.BlazorUI.csproj"

COPY . /
RUN dotnet build "08.BlazorUI/MyApp.BlazorUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "08.BlazorUI/MyApp.BlazorUI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 80

# Install curl for health checks
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

COPY --from=publish /app/publish .

# Create logs directory
RUN mkdir -p /app/logs

# Environment variables for custom domain support
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true

# Support for AdminMode environment variable
ENV AdminMode=false

# Health check endpoint
HEALTHCHECK --interval=30s --timeout=10s --start-period=40s --retries=3 \
    CMD curl -f http://localhost/health || exit 1

ENTRYPOINT ["dotnet", "MyApp.BlazorUI.dll"]