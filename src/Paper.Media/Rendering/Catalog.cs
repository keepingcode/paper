using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Rendering
{
  public class Catalog : IEnumerable<IPaper>
  {
    private readonly List<IPaper> papers = new List<IPaper>();

    public Catalog(Guid id)
    {
      this.Id = id;
    }

    public Guid Id { get; }

    public void AddPaper(IPaper paper)
    {
      papers.Add(paper);
    }

    public IEnumerator<IPaper> GetEnumerator()
    {
      return papers.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return papers.GetEnumerator();
    }
  }
}
