FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

ARG DB_CONN="Data Source=host.docker.internal,5433; Initial Catalog=UniCollab; User Id=sa; Password=YourStrong(!)Password; TrustServerCertificate=True; Encrypt=False; Connection Timeout=30;"
ENV UNICOLLAB_ConnectionStrings__Database=$DB_CONN

WORKDIR /src

COPY . .
RUN dotnet restore
RUN dotnet publish -c Debug -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "Saas.Api.dll"]