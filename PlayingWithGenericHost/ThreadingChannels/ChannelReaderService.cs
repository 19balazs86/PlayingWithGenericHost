using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace PlayingWithGenericHost.ThreadingChannels
{
  public class ChannelReaderService : BackgroundService
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

      // There was not ReadAllAsync method before .NET Core 3.
      //while(await _channelReader.WaitToReadAsync())
      //{
      //  while (_channelReader.TryRead(out Message message))
      //  {
      //    await Task.Delay(300);

      //    Log.Information($"Processed message: '{message.Id}'.");
      //  }
      //}
    }
  }
}
