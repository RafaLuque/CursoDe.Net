using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Hay que registrar la cadena de conexión
var conn = builder.Configuration["data:conn"];
builder.Services.AddDbContext<TodoContext>(opt=> opt.UseNpgsql(conn));

//Hay que registrar los servicios que se crean.
//Es buenas practicas que se cree una interfaz, se usa sobre todo para las pruebas unitarias.
//AddSingleton -> se genera una instancia a nivel de programa.
//AddCors -> se genera una instancia a nivel de request.
//AddTransient -> se genere una instanca cada vez que se pida el objeto.
builder.Services.AddTransient<ITodoServices,TodoServices>();


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
