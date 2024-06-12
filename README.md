# Generate web api
```
dotnet new webapi --use-controllers -o TodoApi
cd TodoApi
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

# Prepare docker instance for DB:

1. Create docker instance:

```bash
docker pull mcr.microsoft.com/mssql/server:2019-latest
docker run -d --name mssql -e ACCEPT_EULA=Y -e MSSQL_SA_PASSWORD='Something$ecur3!' -v ~/docker/mssql:/var/opt/mssql -p 1433:1433 --restart unless-stopped mcr.microsoft.com/mssql/server:2019-latest
```

2. Add to appsettings: 
```json
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=localhost,1433;TrustServerCertificate=true;User ID=sa;Password=Something$ecur3!;"
  }
```
