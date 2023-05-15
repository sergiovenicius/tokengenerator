using Microsoft.Extensions.Logging;
using System;

namespace testcardregister
{
    public class TestLoggerFake<T> : ILogger<T>
    {
        public string lastLogMessage { get; set; } = "";

        public IDisposable BeginScope<TState>(TState state) => default!;

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            lastLogMessage = formatter(state, exception);
        }
    }
}
