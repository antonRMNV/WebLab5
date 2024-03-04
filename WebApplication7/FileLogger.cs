namespace ASP5
{
    public class FileLogger : ILogger, IDisposable
    {
        string path;
        static object locker = new object();

        public FileLogger(string path)
        {
            this.path = path;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }

        public void Dispose()
        {

        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            lock (locker)
            {
                File.AppendAllText(path, formatter(state, exception) + Environment.NewLine);
            }
        }
    }
}

