using OpenTelemetry.Logs;
using Serilog;
using Serilog.Sinks.OpenTelemetry;

namespace StorageProject.Api.Configurations
{
    public static class LogConfigurarion
    {

        public static void AddLogConfiguration(this IServiceCollection service, WebApplicationBuilder builder)
        {
            var OUTPUTTEMPLATE = "{Timestamp: [dd/MM/yyyy HH:mm:ss]} [{Level}] {SourceContext} - {Message:lj}{NewLine}{Exception}";


            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .MinimumLevel.Information()
            .WriteTo.Console(outputTemplate: OUTPUTTEMPLATE)
            .WriteTo.OpenTelemetry(
                endpoint: "localhost:4317",
                protocol: OtlpProtocol.HttpProtobuf)
            .CreateLogger();


            builder.Host.UseSerilog();

            builder.Logging.AddOpenTelemetry(o =>
            {
                o.IncludeFormattedMessage = true;
                o.AddOtlpExporter();
            });
        }
    }
}
