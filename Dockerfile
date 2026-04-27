# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

# Copy project files
COPY ["Library.Api/Library.Api.csproj", "Library.Api/"]
COPY ["Library.Aplication/Library.Aplication.csproj", "Library.Aplication/"]
COPY ["Library.Domain/Library.Domain.csproj", "Library.Domain/"]
COPY ["Library.Persistence/Library.Persistence.csproj", "Library.Persistence/"]

# Restore dependencies
RUN dotnet restore "Library.Api/Library.Api.csproj"

# Copy the entire project
COPY . .

# Build the application
RUN dotnet build "Library.Api/Library.Api.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish

RUN dotnet publish "Library.Api/Library.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

WORKDIR /app

COPY --from=publish /app/publish .

# Expose port
EXPOSE 8080

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=5s --retries=3 \
    CMD dotnet --version

# Run the application
ENTRYPOINT ["dotnet", "Library.Api.dll"]
