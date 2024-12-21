# Project Overview

- This project is a simple snake and ladder game API.
- It has 3 layers: *API, Domain and Database*
- But there are 4 prjects: the remaining project  **Console App** is for creating boards.
- I used Efcore and Database First Approach for Database and Domain Layer.
- Result Pattern and Dependency Injection are used in the API Layer.

## Database First Approach

```bash
dotnet ef dbcontext scaffold "Server=Aung-Myat-Min\SQL;Database=SnakeLadderGame;User Id=sa;Password=sasa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDBContext -f
```

