using System.Threading.Tasks.Dataflow;

namespace PlayingWithGenericHost.DataflowTPL;

public sealed class BufferBlockProducerService : BackgroundService
{
    private readonly ILogger<BufferBlockProducerService> _logger;

    private readonly ITargetBlock<string> _producer;

    public BufferBlockProducerService(ILogger<BufferBlockProducerService> logger, BufferBlockSource bufferBlockSource)
    {
        _logger   = logger;
        _producer = bufferBlockSource.GetProducer();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        for (int i = 1; i <= 20 && !stoppingToken.IsCancellationRequested; i++)
        {
            string item = $"Item {i}";

            _logger.LogInformation("Producing '{Item}'", item);

            await Task.Delay(500); // Simulate some work to produce an item

            try
            {
                await _producer.SendAsync(item, stoppingToken);

                _logger.LogInformation("Produced '{Item}'", item);
            }
            catch (OperationCanceledException) { }
        }

        _logger.LogInformation("BufferBlock.Complete()");

        _producer.Complete();

        // await _producer.Completion;
    }
}
