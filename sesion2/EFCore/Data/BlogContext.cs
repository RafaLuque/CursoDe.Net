using Microsoft.EntityFrameworkCore;

public class BlogContext:  DbContext
{
    public DbSet<Blog> Blogs { get; set; }

    public BlogContext(DbContextOptions<BlogContext> options):base(options){
        
    }
}