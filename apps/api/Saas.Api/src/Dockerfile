FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

ARG DIR="Saas.Api"
ARG PROJ="$DIR/Saas.Api.csproj"

COPY $PROJ "./$DIR/"
RUN dotnet restore $PROJ
COPY . .
RUN dotnet publish -c Release -o /app --no-restore $PROJ

FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "Saas.Api.dll"]