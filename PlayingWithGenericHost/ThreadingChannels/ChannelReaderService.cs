using Serilog;
using System.Threading.Channels;

namespace PlayingWithGenericHost.ThreadingChannels;

public sealed class ChannelReaderService : BackgroundService
{
    private readonly ChannelReader<Message> _channelReader;

    public ChannelReaderService(MessageChannel messageChannel)
        => _channelReader = messageChannel.Reader;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Not passing cancellation into the method so that we can drain the channel on shutdown.
        await foreach (Message message in _channelReader.ReadAllAsync())
        {
            await Task.Delay(300);

            Log.Information($"Processed message: #{message.Id}.");
        }

        // ReadAllAsync method was not exists before .NET Core 3.
        //while (await _channelReader.WaitToReadAsync())
        //{
        //    while (_channelReader.TryRead(out Message message))
        //    {
        //        await Task.Delay(300);

        //        Log.Information($"Processed message: '{message.Id}'.");
        //    }
        //}
    }
}
