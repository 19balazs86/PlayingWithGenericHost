using System.Threading.Tasks.Dataflow;

namespace PlayingWithGenericHost.DataflowTPL;

public sealed class BufferBlockSource
{
    public const int MaxParallelConsume = 4;

    private readonly BufferBlock<string> _bufferBlock;

    public BufferBlockSource()
    {
        var options = new DataflowBlockOptions { BoundedCapacity = MaxParallelConsume };

        _bufferBlock = new BufferBlock<string>(options);
    }

    public ITargetBlock<string> GetProducer()
    {
        return _bufferBlock;
    }

    public ISourceBlock<string> GetConsumer()
    {
        return _bufferBlock;
    }
}
