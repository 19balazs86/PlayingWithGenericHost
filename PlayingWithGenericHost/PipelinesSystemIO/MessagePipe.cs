using System.IO.Pipelines;

namespace PlayingWithGenericHost.PipelinesSystemIO;

public sealed class MessagePipe
{
    private readonly Pipe _pipe;

    public PipeWriter Writer => _pipe.Writer;

    public PipeReader Reader => _pipe.Reader;

    public MessagePipe()
    {
        _pipe = new Pipe();
    }
}
