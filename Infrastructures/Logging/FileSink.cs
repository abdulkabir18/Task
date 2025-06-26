using Serilog.Core;
using Serilog.Events;

namespace Infrastructures.Logging
{
    public class FileSink : ILogEventSink
    {
        private readonly StreamWriter _writer;

        public FileSink(string filePath)
        {
            _writer = new StreamWriter(filePath, append: true) { AutoFlush = true };
        }

        public void Emit(LogEvent logEvent)
        {
            var message = $"{logEvent.Timestamp:u} [{logEvent.Level}] {logEvent.RenderMessage()}";
            if (logEvent.Exception != null)
            {
                message += Environment.NewLine + logEvent.Exception;
            }

            _writer.WriteLine(message);
        }
    }

}
