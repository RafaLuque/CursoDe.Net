var builder = WebApplication.CreateBuilder(args);

//Antes del Build se configura los servicios.
builder.Services.AddGrpc();

var app = builder.Build();
//Despues del Build se configura la aplicación.

//Se mapea los endpoint para grpc.
app.MapGrpcService<WeatherServices>();

app.MapGet("/", () => "This is a grpc server!");




app.Run();
