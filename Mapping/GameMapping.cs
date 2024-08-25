using GameStore.Api.Dtos;
using GameStore.Api.Entities;

namespace GameStore.Api.Mapping;

public static class GameMapping
{
    //convert object newGame from  CreateGameDto type to Game Entity in Database
    public static Game ToEntity(this CreateGameDto newGame)
    {
        return new()
        {
            Name = newGame.Name,
            GenreId = newGame.GenreId,
            Price = newGame.Price,
            ReleaseDate = newGame.ReleaseDate
        };
    }

    public static GameDto ToDto(this Game newGame)
    {
        return new(
                newGame.Id,
                newGame.Name,
                newGame.Genre!.Name,
                newGame.Price,
                newGame.ReleaseDate
        );
    }
}