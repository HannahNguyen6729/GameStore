using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;

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
        //GET
        group.MapGet("/", () => games);
        group.MapGet("/{id}", (int id) =>
        {
            GameDto? foundGame = games.Find(game => game.Id == id);
            return foundGame is null ? Results.NotFound() : Results.Ok(foundGame);
        }).WithName(GetGameEndpointName);

        //POST
        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = new()
            {
                Name = newGame.Name,
                GenreId = newGame.GenreId,
                Genre = dbContext.Genres.Find(newGame.GenreId),
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };
            dbContext.Games.Add(game);
            dbContext.SaveChanges(); //save new entity to the db

            GameDto gameReturnedToClient = new(
              game.Id,
                game.Name,
                game.Genre!.Name,
                game.Price,
                game.ReleaseDate
        );
            return Results.CreatedAtRoute(
                GetGameEndpointName,
                new { id = game.Id },
                gameReturnedToClient
                );
        }).WithParameterValidation();

        //UPDATE GAME/ PUT
        group.MapPut("/{id}", (int id, UpdateGameDto updateGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);

            if (index == -1) return Results.NotFound();
            games[index] = new GameDto(
               id,
               updateGame.Name,
               updateGame.Genre,
               updateGame.Price,
               updateGame.ReleaseDate
               );

            return Results.NoContent();
        });

        // DELETE
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });
        return group;
    }

}