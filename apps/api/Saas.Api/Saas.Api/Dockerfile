FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Saas.Api/Saas.Api.csproj", "Saas.Api/"]
RUN dotnet restore "./Saas.Api/Saas.Api.csproj"
COPY . .
WORKDIR "/src/Saas.Api"
RUN dotnet build "./Saas.Api.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "./Saas.Api.csproj" -c Debug -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Saas.Api.dll"]
