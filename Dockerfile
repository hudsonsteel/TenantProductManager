# Use a imagem base do .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Defina o diretório de trabalho
WORKDIR /app

# Copie os arquivos do projeto
COPY *.sln ./

COPY src/TenantProductManager.Api/*.csproj src/TenantProductManager.Api/
COPY src/TenantProductManager.Application/*.csproj src/TenantProductManager.Application/
COPY src/TenantProductManager.Domain/*.csproj src/TenantProductManager.Domain/
COPY src/TenantProductManager.Infrastructure/*.csproj src/TenantProductManager.Infrastructure/
COPY test/TenantProductManager.UnitTest/*.csproj test/TenantProductManager.UnitTest/

# Restore dependencies
RUN dotnet restore

# Copie o restante dos arquivos e construa o projeto
COPY . ./
RUN dotnet publish -c Release -o /app/out

# Defina o diretório de trabalho e o ponto de entrada
WORKDIR /app/out
ENTRYPOINT ["dotnet"]
