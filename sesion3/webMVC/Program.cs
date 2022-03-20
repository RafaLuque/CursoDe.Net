using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Hay que registrar la cadena de conexión
var conn = builder.Configuration["data:conn"];
builder.Services.AddDbContext<TodoContext>(opt=> opt.UseNpgsql(conn));

//Hay que registrar los servicios que se crean.
//Es buenas practicas que se cree una interfaz, se usa sobre todo para las pruebas unitarias.
//AddSingleton -> siempre se ejecuta la misma instancia del servicio en todas las peticiones.
//AddTransient -> cada vez se ejecuta una instancia nueva del servicio en cada peticiones.
//AddScoped -> dentro de la misma petición, el servicio se instancia una sola vez.

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
