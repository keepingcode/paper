using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paper.Media;

namespace Paper.Browser.Base.Pages
{
  public interface IPage : IComponent
  {
    Window Window { get; }

    Entity Entity { get; }
  }
}
