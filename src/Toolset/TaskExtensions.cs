using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Toolset
{
  public static class TaskExtensions
  {
    public static void NoAwait(this Task task)
    {
      // Nada a fazer. A tarefa será executada em paralelo.
    }
  }
}
