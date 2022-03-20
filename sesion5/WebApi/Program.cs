using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//AddSingleton -> siempre se ejecuta la misma instancia del servicio en todas las peticiones.
//AddTransient -> cada vez se ejecuta una instancia nueva del servicio en cada peticiones.
//AddScoped -> dentro de la misma petición, el servicio se instancia una sola vez.
// Add services to the container.
builder.Services.AddScoped<GuidService>();
builder.Services.AddTransient<FooService>();
var constr = builder.Configuration["data:conn"];
builder.Services.AddDbContext<TodoContext>(opt => opt.UseNpgsql(constr));
builder.Services.AddTransient<ITodoService,TodoService>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
builder.Logging.ClearProviders(); 
builder.Logging.AddConsole();
*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //https://localhost:7188/swagger/index.html
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/hello", () => "Hello World");
app.MapGet("/pendingtodos", async (ITodoService svc) => await svc.GetPendingTodos());

app.MapControllers();

app.Run();
