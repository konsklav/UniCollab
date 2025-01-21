FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore
RUN dotnet publish -c Debug -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0

ARG DB_CONN="Data Source=host.docker.internal,5433; Initial Catalog=UniCollab; User Id=sa; Password=YourStrong(!)Password; TrustServerCertificate=True; Encrypt=False; Connection Timeout=30;"
WORKDIR /app
COPY --from=build /app ./

ENV UNICOLLAB_ConnectionStrings__Database=$DB_CONN

ENTRYPOINT ["dotnet", "Saas.Api.dll"]