Database First Approach

```bash
dotnet ef dbcontext scaffold "Server=Aung-Myat-Min\SQL;Database=SnakeLadderGame;User Id=sa;Password=sasa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDBContext -f
```