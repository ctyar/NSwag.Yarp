namespace Api1;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddOpenApiDocument();

        var app = builder.Build();

        app.UseOpenApi(config =>
        {
            config.AddYarp("/api1");
        });

        app.UseSwaggerUi(config =>
        {
            config.AddYarp("/api1");
        });

        app.MapControllers();

        app.Run();
    }
}
