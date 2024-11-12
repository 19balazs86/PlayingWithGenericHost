using Serilog;
using System.Threading.Channels;

namespace PlayingWithGenericHost.ThreadingChannels;

public sealed class ChannelReaderService(MessageChannel _messageChannel) : BackgroundService
{
    private readonly ChannelReader<Message> _channelReader = _messageChannel.Reader;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // We are not passing cancellation into the method to allow the channel to drain on shutdown
        await foreach (Message message in _channelReader.ReadAllAsync(CancellationToken.None))
        {
            await processMessage(message, CancellationToken.None);
        }

        // An easy way to process messages in parallel
        // await Parallel.ForEachAsync(_channelReader.ReadAllAsync(CancellationToken.None), CancellationToken.None, processMessage);

        // The ReadAllAsync method did not exist before .NET Core 3
        // while (await _channelReader.WaitToReadAsync(CancellationToken.None))
        // {
        //     while (_channelReader.TryRead(out Message message))
        //     {
        //         await processItem(message, CancellationToken.None);
        //     }
        // }
    }

    private static async ValueTask processMessage(Message message, CancellationToken ct)
    {
        await Task.Delay(300, ct);

        Log.Information($"Processed message: #{message.Id}.");
    }
}
