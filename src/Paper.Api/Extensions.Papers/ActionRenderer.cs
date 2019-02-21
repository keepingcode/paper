using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Paper.Api.Rendering;
using Paper.Media;

namespace Paper.Api.Extensions.Papers
{
  internal class ActionRenderer
  {
    private readonly IObjectFactory objectFactory;

    public ActionRenderer(IObjectFactory objectFactory)
    {
      this.objectFactory = objectFactory;
    }

    public void Render(PaperContext context, Entity entity)
    {
      
    }
  }
}
