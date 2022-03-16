using Microsoft.EntityFrameworkCore;

namespace WebBlazorServer.Data;

public class TodoContext: DbContext
{
    public DbSet<TodoTask> Todos {get; set;}

    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }
}