
namespace APICatalogo.Logging
{
    public class CustomerLogger : ILogger

    {
        readonly string loggerName;
        readonly CustomLoggerConfiguration loggerConfig;
        public CustomerLogger(string name, CustomLoggerConfiguration config)
        {
            
            loggerName = name;
            loggerConfig = config;  

        }


         

        public IDisposable BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == loggerConfig.LogLevel;  
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
             Exception exception, Func<TState, Exception, string> formatter)
        {
            string mensagem = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";

            EscreverTextoNoArquivo(mensagem);
        }

        private void EscreverTextoNoArquivo(string mensagem)
        {
            string caminhoArquivoLog = @"C:\Dados\Log\APICatalogo_Log.txt";
            using (StreamWriter streamWriter = new StreamWriter(caminhoArquivoLog, true))
            {
                try
                {
                    streamWriter.WriteLine(mensagem);
                    streamWriter.Close();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
