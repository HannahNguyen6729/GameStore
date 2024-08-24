
using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

// DbContext tracks changes to entity objects, allowing you to perform CRUD operations (Create, Read, Update, Delete) and persist these changes to the database.
public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>(); //defines a property in the DbContext that represents a collection of Game entities.
    public DbSet<Genre> Genres => Set<Genre>();
}

//DbSet<Game>: DbSet<T> is a special class in Entity Framework Core that represents a collection of entities of type T (in this case, Game). It allows you to perform CRUD operations (Create, Read, Update, Delete) on entities in the database.