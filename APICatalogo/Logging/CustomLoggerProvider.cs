using System.Collections.Concurrent;

namespace APICatalogo.Logging
{
    public class CustomLoggerProvider : ILoggerProvider
    {

        readonly CustomLoggerConfiguration loggerconfig;
        readonly  ConcurrentDictionary<string, CustomerLogger> loggers = new ConcurrentDictionary<string, CustomerLogger> ();
        public CustomLoggerProvider(CustomLoggerConfiguration config )
        {
            loggerconfig = config;
        }


        public ILogger CreateLogger(string categoryName)
        {
            return loggers.GetOrAdd(categoryName, name => new CustomerLogger(name, loggerconfig));  
        }

        public void Dispose()
        {
            loggers.Clear();
        }
    }
}
