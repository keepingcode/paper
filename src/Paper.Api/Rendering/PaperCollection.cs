using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace Paper.Api.Rendering
{
  [DataContract(Namespace = "")]
  public class PaperCollection
  {
    private List<IPaper> _papers;

    public PaperCollection(string name)
    {
      this.Name = name;
    }

    [DataMember]
    public string Name { get; }

    [DataMember]
    public List<IPaper> Papers
    {
      get => _papers ?? (_papers = new List<IPaper>());
      set => _papers = value;
    }
  }
}