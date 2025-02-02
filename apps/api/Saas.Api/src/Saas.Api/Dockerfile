FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

ARG DB_CONN="Data Source=host.docker.internal,5433; Initial Catalog=UniCollab; User Id=sa; Password=YourStrong(!)Password; TrustServerCertificate=True; Encrypt=False; Connection Timeout=30;"

WORKDIR /src

COPY . .
RUN dotnet restore "Saas.Api.csproj"
RUN dotnet publish -c Release -o /app --no-restore "Saas.Api.csproj"

FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "Saas.Api.dll"]