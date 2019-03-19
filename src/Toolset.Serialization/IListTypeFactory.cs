using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Toolset.Collections;

namespace Toolset.Serialization.Graph
{
  public interface IGraphFactory
  {
    object CreateItem(string property, HashMap graph, Mapper mapper);
    object CreateList(string property, ICollection items);
  }
}