PROJ="Saas.Api"
API_PATH="../api/$PROJ/"

DB_PASS="YourStrong(!)Password"
DB_IMG="mcr.microsoft.com/mssql/server:2019-latest"

echo "🐳 Running SQL Server in Docker"
docker run --rm -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$DB_PASS" -p 5433:1433 -d $DB_IMG

echo "🛠️ Building .NET API"
dotnet build "$API_PATH/$PROJ.sln"

export UNICOLLAB_ConnectionStrings__Database="Data Source=localhost,5433; Initial Catalog=UniCollab; User Id=sa; Password=YourStrong(!)Password; TrustServerCertificate=True; Encrypt=False; Connection Timeout=30;"
echo "🚀 Launching .NET API"
dotnet run --launch-profile "http" --project "$API_PATH/$PROJ/$PROJ.csproj" --no-build -v n &

sleep 1

echo "⚛️ Launch React Application"
npm run dev