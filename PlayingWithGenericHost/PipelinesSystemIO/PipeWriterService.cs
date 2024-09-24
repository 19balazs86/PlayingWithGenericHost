using System.IO.Pipelines;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace PlayingWithGenericHost.PipelinesSystemIO;

// Example: https://github.com/chaseaucoin/AsyncStreamDemo/blob/master/SimpleProtocol/Program.cs
public sealed class PipeWriterService(ILogger<PipeWriterService> logger, MessagePipe messagePipe) : BackgroundService
{
    private int _messageCounter = 0;

    private readonly PipeWriter _pipeWriter = messagePipe.Writer;
    private readonly Stream _writerStream   = messagePipe.Writer.AsStream(leaveOpen: false);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Exception exception = null;

        try
        {
            await JsonSerializer.SerializeAsync(_writerStream, getMessages(stoppingToken));
        }
        catch (Exception ex)
        {
            logger.LogError("PipeWriterService: {Message}", ex.Message);

            exception = ex;
        }
        finally
        {
            await _pipeWriter.CompleteAsync(exception);

            logger.LogInformation("PipeWriterService is done");
        }
    }

    private async IAsyncEnumerable<Message> getMessages([EnumeratorCancellation] CancellationToken ct)
    {
        int numberOfMessages = Random.Shared.Next(10, 21);

        logger.LogInformation("Sending {Number} of messages via Pipe", numberOfMessages);

        for (int i = 1; i <= numberOfMessages && !ct.IsCancellationRequested; i++)
        {
            if (Random.Shared.Next(0, 100) <= 5)
                throw new Exception("Random PipeWriter exception");

            await Task.WhenAny(Task.Delay(300, ct));

            yield return new Message(Id: ++_messageCounter);
        }
    }
}
