# Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o /out

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /out . 

# Copia o .env para dentro do container
COPY .env . 

EXPOSE 8080
ENTRYPOINT ["dotnet", "Crypto.Monitoring.dll"]