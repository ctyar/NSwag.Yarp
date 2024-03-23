namespace Api2;

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
            // YARP for this sample is configured to set the X-Forwarded-Prefix header.
            // See https://github.com/ctyar/NSwag.Yarp/blob/main/src/samples/Yarp/appsettings.json#L24-L31
            // For explicitly setting the prefix see https://github.com/ctyar/NSwag.Yarp/blob/main/src/samples/Api1/Program.cs#L18
            config.AddYarpWithForwardedPrefix();
        });
        app.UseSwaggerUi(config =>
        {
            config.AddYarpWithForwardedPrefix();
        });

        app.MapControllers();

        app.Run();
    }
}
