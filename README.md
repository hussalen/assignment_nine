# Generate web api
```
dotnet new webapi --use-controllers -o TodoApi
cd TodoApi
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.InMemory
```

# Code-First

```
dotnet ef migrations add nameOfMigration
dotnet ef database update
```
(if `dotnet ef` doesn't work, try `dotnet tool install --global dotnet-ef`)

Don't forget to delete tables if needed when re-creating the migration. Use the following script (delimiter may either be ; or GO):

```sql
USE YOUR_DATABASE_NAME
-- Disable all referential integrity constraints
EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'
;

-- Drop all PKs and FKs
declare @sql nvarchar(max)
SELECT @sql = STUFF((SELECT '; ' + 'ALTER TABLE ' + Table_Name  +'  drop constraint ' + Constraint_Name  from Information_Schema.CONSTRAINT_TABLE_USAGE ORDER BY Constraint_Name FOR XML PATH('')),1,1,'')
EXECUTE (@sql)
;

-- Drop all tables
EXEC sp_MSforeachtable 'DROP TABLE ?'
;
```

# Database-First

Add new entry to appsettings.json
```json
"ConnectionStrings": {
    "DefaultConnection": "Data Source=localhost,1433;TrustServerCertificate=true;User ID=sa;Password=Something$ecur3!;"
  }
```
Then...
```
dotnet ef dbcontext scaffold "Name=ConnectionStrings:DefaultConnection" Microsoft.EntityFrameworkCore.SqlServer -o Model
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

# In case the container needs to be removed

1. docker container ls 
2. docker stop name-of-container
3. docker container prune
4. docker rm --volumes name-of-container
