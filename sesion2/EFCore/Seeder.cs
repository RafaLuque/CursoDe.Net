public static class Seeder{
    public static async Task Seed(BlogContext ctx){
        if(!ctx.Blogs.Any()){
            Console.WriteLine("Insertando blgs...");

            var blog= new Blog(){
                Name ="Test blog",
                Url ="https://escuela.it/blogs/edu"
            };
            var post = new Post(){
                PublishedDate = DateTime.UtcNow,
                Text ="Loren ipsum...."
            };

            blog.Posts.Add(post);

            ctx.Blogs.Add(blog);
            await ctx.SaveChangesAsync();
        }


    }
}