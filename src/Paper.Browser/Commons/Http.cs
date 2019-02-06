using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paper.Media;

namespace Paper.Browser.Commons
{
  public static class Http
  {
    public static async Task<Entity> RequestEntityAsync(string href)
    {
      try
      {
        await Task.Delay(3000);
        throw new NotImplementedException(nameof(RequestEntityAsync));
        //return await Task.FromResult(new Entity());
      }
      catch (Exception ex)
      {
        return await Task.FromResult(HttpEntity.Create(href, ex));
      }
    }
  }
}
