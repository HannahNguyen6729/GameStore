create gitignore file: dotnet new gitignore

Command to run project: dotnet run

//MinimalApis.Extensions
dotnet add package MinimalApis.Extensions

//entity framework core: a technique for converting data between relational database and object-oriented program

//install new package by using this command: dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 9.0.0

configure database connection in appsettings.json file

//press F5 to start debugging session, now connectionString = "Data Source=GameStore.db" as the same value in appsettings.json file

// generating the database:
go to nuget.org page to install a package/ a tool (dotnet-ef) with this command below:
dotnet tool install --global dotnet-ef --version 8.0.8

//install new package: Microsoft.EntityFrameworkCore.Design
--> this package helps to generate entity framework migration
command: dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.8
