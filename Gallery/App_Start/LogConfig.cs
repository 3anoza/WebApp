
using Serilog;
using Serilog.Sinks.File;

namespace Gallery
{
    public class LogConfig
    {
        public static void WriteToLogs(string LogContent)
        {
             Log.Logger = new LoggerConfiguration()
                 .WriteTo.File("log.txt")
                .CreateLogger();

            Log.Information("Hello<SUKA>");

            Log.CloseAndFlush();
        }
    }
}