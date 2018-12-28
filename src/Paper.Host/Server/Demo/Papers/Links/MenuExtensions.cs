using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paper.Media.Design.Papers;

namespace Paper.Host.Server.Demo.Papers.Links
{
  public static class MenuExtensions
  {
    public static IEnumerable<ILink> ExceptSelf<T>(T menu)
      where T : IEnumerable<ILink>
    {
      return
        from item in menu
        where item.GetType().IsGenericType
           && item.GetType().GetGenericTypeDefinition() == typeof(LinkSelf<>)
        select item;
    }
  }
}
