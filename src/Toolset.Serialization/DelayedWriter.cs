using System;
using System.Collections.Generic;
using System.Text;

namespace Toolset.Serialization
{
  public class DelayedWriter : Writer, DelayedWriter.ISetter
  {
    public event EventHandler<ReadEventArgs> Intercept;

    private readonly Queue<Node> queue = new Queue<Node>();

    private Writer writer;

    public DelayedWriter()
    {
    }

    public DelayedWriter(Action<Node, ISetter> interceptor)
    {
      Intercept += (o, e) => interceptor.Invoke(e.Node, e.Setter);
    }

    public void SetWriter(Writer writer)
    {
      if (this.writer != null)
        throw new InvalidOperationException("A instância de Writer somente pode ser definida uma vez.");

      this.writer = writer;
      foreach (var node in queue)
      {
        this.writer.Write(node);
      }
    }

    protected override void DoWrite(Node node)
    {
      if (this.writer == null)
      {
        queue.Enqueue(node);
        Intercept?.Invoke(this, new ReadEventArgs { Node = node, Setter = this });
      }
      else
      {
        this.writer?.Write(node);
      }
    }

    protected override void DoWriteComplete()
    {
      this.writer?.WriteComplete();
    }

    protected override void DoFlush()
    {
      this.writer?.Flush();
    }

    protected override void DoClose()
    {
      this.writer?.Close();
    }

    public interface ISetter
    {
      void SetWriter(Writer writer);
    }

    public class ReadEventArgs : EventArgs
    {
      public Node Node { get; internal set; }

      public ISetter Setter { get; internal set; }
    }
  }
}
