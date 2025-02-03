FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY . .
RUN dotnet restore "Saas.Api/Saas.Api.csproj"
RUN dotnet publish -c Release -o /app --no-restore "Saas.Api/Saas.Api.csproj"

FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "Saas.Api.dll"]