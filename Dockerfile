# Stage 1: Build frontend
FROM node:20-alpine AS frontend

WORKDIR /src/Angular
COPY Angular/package*.json ./
RUN npm ci
COPY Angular .
RUN npm run build

# Stage 2: Build backend
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src
# Copy project files for restore
COPY ["Library.Api/Library.Api.csproj", "Library.Api/"]
COPY ["Library.Aplication/Library.Aplication.csproj", "Library.Aplication/"]
COPY ["Library.Domain/Library.Domain.csproj", "Library.Domain/"]
COPY ["Library.Persistence/Library.Persistence.csproj", "Library.Persistence/"]

RUN dotnet restore "Library.Api/Library.Api.csproj"

COPY . .
RUN dotnet publish "Library.Api/Library.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

WORKDIR /app
COPY --from=build /app/publish .
COPY --from=frontend /src/Angular/dist/desafio-teste ./wwwroot

EXPOSE 8080
HEALTHCHECK --interval=30s --timeout=10s --start-period=5s --retries=3 \
    CMD dotnet --version

ENTRYPOINT ["dotnet", "Library.Api.dll"]
