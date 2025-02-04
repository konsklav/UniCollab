FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

ARG PROJ="Saas.Api/Saas.Api.csproj"

COPY . .
RUN dotnet restore $PROJ
RUN dotnet publish -c Release -o /app --no-restore $PROJ

FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "Saas.Api.dll"]