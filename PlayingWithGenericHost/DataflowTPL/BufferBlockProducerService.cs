using System.Threading.Tasks.Dataflow;

namespace PlayingWithGenericHost.DataflowTPL;

public sealed class BufferBlockProducerService : BackgroundService
{
    private readonly ITargetBlock<string> _producer;

    public BufferBlockProducerService(BufferBlockSource bufferBlockSource)
    {
        _producer = bufferBlockSource.GetProducer();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        for (int i = 1; i <= 20 && !stoppingToken.IsCancellationRequested; i++)
        {
            string item = $"Item {i}";

            Console.WriteLine($"Producing {item}");

            await Task.Delay(500); // Simulate some work to produce an item

            try
            {
                await _producer.SendAsync(item, stoppingToken);

                Console.WriteLine($"Produced {item}");
            }
            catch (OperationCanceledException) { }
        }

        Console.WriteLine("BufferBlock.Complete()");

        _producer.Complete();

        // await _producer.Completion;
    }
}
