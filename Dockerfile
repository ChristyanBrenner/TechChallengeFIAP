# Etapa 1 — build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia a solução e projetos
COPY *.sln ./
COPY CloudGames.API/*.csproj ./CloudGames.API/
COPY Middleware/*.csproj ./Middleware/
COPY Repositories/*.csproj ./Repositories/
COPY Services/*.csproj ./Services/
COPY Utils/*.csproj ./Utils/
COPY Domain/*.csproj ./Domain/
COPY Tests/*.csproj ./Tests/

# ---------------------------
# Mock: ignora o restore para Contracts não existente
# RUN dotnet restore
# ---------------------------

COPY . .

WORKDIR /src/CloudGames.API

# Publica (não vai rodar se não compilar, mas mantemos para consistência)
# RUN dotnet publish -c Release -o /app

# Etapa 2 — runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["sleep", "infinity"]
