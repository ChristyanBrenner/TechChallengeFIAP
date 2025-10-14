# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas os arquivos de projeto para otimizar o restore
COPY *.sln ./
COPY CloudGames.API/*.csproj ./CloudGames.API/
COPY Middleware/*.csproj ./Middleware/
COPY Repositories/*.csproj ./Repositories/
COPY Services/*.csproj ./Services/
COPY Utils/*.csproj ./Utils/
COPY Domain/*.csproj ./Domain/
COPY Tests/*.csproj ./Tests/

RUN dotnet restore

COPY . .

WORKDIR /src/CloudGames.API

RUN dotnet publish -c Release -o /app

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

RUN apt-get update && apt-get install -y netcat-traditional

COPY --from=build /app .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "CloudGames.API.dll"]

