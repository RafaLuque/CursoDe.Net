//Tiene que heredar de la calse WeatherForecastsBase que se crea.
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using WeatherForeCast;


public class WeatherServices : WeatherForeCast.WeatherForecasts.WeatherForecastsBase
{
	private readonly ILogger _logger;

	private static readonly string[] Summaries = new[]
    	{
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    	};

	public WeatherServices(ILogger<WeatherServices> logger)
	{
		_logger=logger;
	}

	public override async Task<WeatherReply> GetWeather(Empty request, ServerCallContext context)
	{
		var rng = new Random();
		var now = DateTime.UtcNow;

		_logger.LogInformation("Entrando en GetWeather");

		var forecasts = Enumerable.Range(1,100).Select(index => new WeatherData
		{		
			DateTimestamp = Timestamp.FromDateTime(now.AddDays(index)),
			TemperatureC = rng.Next(-20,45),
			Summary = Summaries[rng.Next(Summaries.Length)]
		}).ToArray();

		//Simula durante 2 segundo que se está trabajando
		await Task.Delay(2000);

		_logger.LogInformation("Saliendo de GetWeather");

		return new WeatherReply{
			WeatherData={forecasts}
		};
	}

	public override async Task GetWeatherStream(Empty request, IServerStreamWriter<WeatherData> responseStream, ServerCallContext context)
	{
		var rng = new Random();
		var now = DateTime.UtcNow;
		var i=0;

		while(!context.CancellationToken.IsCancellationRequested &&i<20)
		{
		var forecast = new WeatherData
		{
			DateTimestamp = Timestamp.FromDateTime(now.AddDays(i++)),
			TemperatureC = rng.Next(-20,45),
			Summary = Summaries[rng.Next(Summaries.Length)]
		};

		_logger.LogInformation("Sending WeatherData response");

		await responseStream.WriteAsync(forecast);

		await Task.Delay(500); // Gotta look busy
		}

		if(context.CancellationToken.IsCancellationRequested)
		{
			_logger.LogInformation("The client cancelled their request");
		}
	}
}

