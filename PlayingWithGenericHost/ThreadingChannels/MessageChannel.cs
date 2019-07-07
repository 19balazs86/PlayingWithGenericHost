using System.Threading.Channels;

namespace PlayingWithGenericHost.ThreadingChannels
{
  public class MessageChannel
  {
    private const int _capacity = 100;

    private readonly Channel<Message> _channel;

    public ChannelWriter<Message> Writer => _channel.Writer;
    public ChannelReader<Message> Reader => _channel.Reader;

    public MessageChannel()
    {
      var options = new BoundedChannelOptions(_capacity)
      {
        SingleReader = true,
        SingleWriter = true
      };

      _channel = Channel.CreateBounded<Message>(options);
    }
  }
}
