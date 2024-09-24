using System.Text.Json;

namespace PlayingWithGenericHost.PipelinesSystemIO;

public sealed class PipeReaderService(ILogger<PipeReaderService> logger, MessagePipe messagePipe) : BackgroundService
{
    private readonly Stream _readerStream = messagePipe.Reader.AsStream(leaveOpen: false);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            IAsyncEnumerable<Message> messages = JsonSerializer.DeserializeAsyncEnumerable<Message>(_readerStream);

            // Not passing cancellation into the method so that we can drain the stream on shutdown
            await foreach (Message message in messages.WithCancellation(CancellationToken.None))
            {
                await Task.Delay(500);

                logger.LogInformation($"Processed Pipe message: #{message.Id}.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError("PipeReaderService: {Message}", ex.Message);
        }

        logger.LogInformation("PipeReaderService is done");
    }
}
