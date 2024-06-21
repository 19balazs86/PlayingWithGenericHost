using System.Threading.Tasks.Dataflow;

namespace PlayingWithGenericHost.DataflowTPL;

public sealed class BufferBlockConsumerService : BackgroundService
{
    private readonly ILogger<BufferBlockConsumerService> _logger;

    private readonly ActionBlock<string> _consumerBlock;

    public BufferBlockConsumerService(ILogger<BufferBlockConsumerService> logger, BufferBlockSource bufferBlockSource)
    {
        _logger = logger;

        var options = new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = BufferBlockSource.MaxParallelConsume };

        _consumerBlock = new ActionBlock<string>(consumeItem, options);

        var dlo = new DataflowLinkOptions { PropagateCompletion = true };

        ISourceBlock<string> consumer = bufferBlockSource.GetConsumer();

        consumer.LinkTo(_consumerBlock, dlo);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Wait for completion to drain the channel on shutdown
        await _consumerBlock.Completion;
    }

    private async Task consumeItem(string item)
    {
        _logger.LogInformation("Consuming '{Item}'", item);

        await Task.Delay(2_000);

        _logger.LogInformation("Consumed '{Item}'", item);
    }
}
