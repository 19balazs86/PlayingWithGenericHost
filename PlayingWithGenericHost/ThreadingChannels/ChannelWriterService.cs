using Serilog;
using System.Threading.Channels;

namespace PlayingWithGenericHost.ThreadingChannels;

public sealed class ChannelWriterService : BackgroundService
{
    private int _messageCounter = 0;

    private readonly ChannelWriter<Message> _channelWriter;

    private readonly IHostApplicationLifetime _hostApplicationLifetime;

    public ChannelWriterService(MessageChannel messageChannel, IHostApplicationLifetime hostApplicationLifetime)
    {
        _channelWriter = messageChannel.Writer;

        _hostApplicationLifetime = hostApplicationLifetime;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (Random.Shared.NextDouble() <= 0.05)
                    throw new Exception("Random exception.");

                await Task.Delay(1250, stoppingToken);

                int index = 0;
                Message[] messages = getMessages().ToArray();

                while (index < messages.Length &&
                    !stoppingToken.IsCancellationRequested &&
                    await _channelWriter.WaitToWriteAsync(stoppingToken))
                {
                    // TryWrite: Can happen the channel is full.
                    // If so, go back to the previous while loop and WaitToWriteAsync.
                    while (index < messages.Length && _channelWriter.TryWrite(messages[index]))
                        index++;
                }
            }
        }
        catch (OperationCanceledException)
        {
            // Swallow it.
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred.");

            _channelWriter.Complete(ex);

            _hostApplicationLifetime.StopApplication();
        }
        finally
        {
            // May have completed in the exception handling.
            _channelWriter.TryComplete();

            Log.Information("ChannelWriterService is stopped.");
        }
    }

    private IEnumerable<Message> getMessages()
    {
        for (int i = 0; i < Random.Shared.Next(1, 11); i++)
            yield return new Message { Id = ++_messageCounter };
    }
}
