using Microsoft.EntityFrameworkCore;

var builder = new DbContextOptionsBuilder<BlogContext>()
        .UseNpgsql("Host=localhost:5432;Database=demo;Username=postgres;Password=pass");

var ctx = new BlogContext(builder.Options);

await ctx.Database.EnsureCreatedAsync();

//Inserta valores por defecto.
await Seeder.Seed(ctx);