
using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

// DbContext tracks changes to entity objects, allowing you to perform CRUD operations (Create, Read, Update, Delete) and persist these changes to the database.
public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>(); //defines a property in the DbContext that represents a collection of Game entities.
    public DbSet<Genre> Genres => Set<Genre>();

    //after migration, this code below will be executed: OnModelCreating
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { // seed data for Genre entity
        modelBuilder.Entity<Genre>().HasData(
            new { Id = 1, Name = "Fighting" },
            new { Id = 2, Name = "Roleplaying" },
            new { Id = 3, Name = "Sports" },
            new { Id = 4, Name = "Racing" },
            new { Id = 5, Name = "Kid & family" }
        );
    }
}

//DbSet<Game>: DbSet<T> is a special class in Entity Framework Core that represents a collection of entities of type T (in this case, Game). It allows you to perform CRUD operations (Create, Read, Update, Delete) on entities in the database.