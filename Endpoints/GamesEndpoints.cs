using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

// use static class because no need to create new object from class later. it's just class defining endpoints
public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGameById";
    private static readonly List<GameDto> games = [
    new(
     1,
     "Final Fantasy XIV",
    "RolePlaying",
    9.99M,
    new DateOnly(1992,7,14)),
    new(
     2,
     "Street fighter XIV",
    "Fighting",
    19.85M,
    new DateOnly(1998,9,24)),
     new(
     3,
     "Chicken Island",
    "Fighting",
    34.9M,
    new DateOnly(2009,10,14)),
     new(
     4,
     "Dragon Balls",
    "Action",
    300,
    new DateOnly(2016,2,16))

    ];

    //this WebApplication app: The this keyword here indicates that MapGameEndpoints is an extension method for the WebApplication class. Extension methods allow you to "add" methods to existing types without modifying the original type or creating a new derived type.
    public static RouteGroupBuilder MapGameEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();
        //GET /games
        group.MapGet("/", async (GameStoreContext dbContext) =>
        {
            return await dbContext.Games
            .Include(game => game.Genre)
            .Select(game => game.ToDto())
            .AsNoTracking()
            .ToListAsync()
            ;
        });

        //GET  /games/2
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            Game? foundGameInDB = await dbContext.Games.FindAsync(id);

            return foundGameInDB is null ? Results.NotFound() : Results.Ok(foundGameInDB);
        }).WithName(GetGameEndpointName);

        //POST
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();
            game.Genre = await dbContext.Genres.FindAsync(newGame.GenreId);

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync(); //save new entity to the db


            return Results.CreatedAtRoute(
                GetGameEndpointName,
                new { id = game.Id },
                game.ToDto()
                );
        }).WithParameterValidation();

        //UPDATE - PUT   /games
        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame)
                     .CurrentValues
                     .SetValues(updatedGame.ToEntity(id));

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // DELETE  /games/2
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();
            return Results.NoContent();
        });
        return group;
    }

}