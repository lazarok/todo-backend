## Migrations

**Development**

```export ASPNETCORE_ENVIRONMENT=Development```



``` dotnet ef migrations add <name> --startup-project ./src/ToDo.Api/ --project ./src/ToDo.Persistence/ --context ToDoDbContext```

```OR```

```dotnet ef migrations add <name>```




```dotnet ef database update --startup-project ./src/ToDo.Api/ --project ./src/ToDo.Persistence/ --context ToDoDbContext```

```OR```

```dotnet ef database update```

