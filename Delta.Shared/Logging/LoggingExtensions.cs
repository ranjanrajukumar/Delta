using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Delta.Shared.Logging;

public static class LoggingExtensions
{
    public static IHostBuilder UseDeltaLogging(this IHostBuilder host)
    {
        return host.UseSerilog((ctx, lc) =>
        {
            lc.MinimumLevel.Information()
              .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
              .Enrich.FromLogContext()
              .Enrich.WithMachineName()
              .Enrich.WithThreadId()
              .WriteTo.File(
                  path: "Logs/delta-error-.log",
                  rollingInterval: RollingInterval.Day)
              .WriteTo.MSSqlServer(
                  connectionString: ctx.Configuration.GetConnectionString("DefaultConnection"),
                  sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
                  {
                      TableName = "TblErrorLog",
                      AutoCreateSqlTable = false
                  },
                  restrictedToMinimumLevel: LogEventLevel.Error
              );
        });
    }
}
