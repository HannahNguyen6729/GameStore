using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Endpoints;



var builder = WebApplication.CreateBuilder(args);

//connect to db
var connectionString = builder.Configuration.GetConnectionString("MyGameStoreDb");
builder.Services.AddSqlite<GameStoreContext>(connectionString);

var app = builder.Build();

app.MapGameEndpoints();
app.Run();
