using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Paper.Core;
using Paper.Media;
using Paper.Media.Design;
using Paper.Media.Design.Papers;
using Sandbox.Lib;

namespace Sandbox.Host.Core
{
  public static class SandboxEntities
  {
    public static Entity GetStatus(UriString requestUri, string prefix)
    {
      return HttpEntity.Create(requestUri, HttpStatusCode.OK);
    }

    public static Entity GetIndex(UriString requestUri, string prefix)
    {
      var entity = new Entity();
      entity.AddTitle("Início");
      entity.AddClass(Class.Single, Class.Data);
      entity.AddDataHeader("Etiam");
      entity.AddDataHeader("Curabitur");
      entity.AddDataHeader("Porta");
      entity.AddDataHeader("CrasAuctor");
      entity.AddDataHeader("Donec");
      entity.AddDataHeader("UtQuis");
      entity.AddProperties(new
      {
        Etiam = "Eu orci a erat feugiat sollicitudin.",
        Curabitur = "Id neque a mi tincidunt luctus.",
        Porta = "Ex vitae nisi rhoncus, ut tristique nisl sollicitudin.",
        CrasAuctor = "Tellus eu lacus porta finibus.",
        Donec = "Id turpis consectetur elit fermentum cursus.",
        UtQuis = "Lacus ultricies, ornare est ac, pretium lorem."
      });
      entity.AddLinkSelf(requestUri);

      var href = requestUri.Combine(prefix, "Blueprint");
      entity.AddLink(href, "Blueprint", Rel.Blueprint);

      return entity;
    }

    public static Entity GetBlueprint(UriString requestUri, string prefix)
    {
      var entity = new Entity();
      entity.AddTitle("Blueprint");
      entity.AddClass(Class.Blueprint);
      entity.AddProperties(new Blueprint
      {
        HasNavBox = true,
        Theme = "blue",
        Info = new Blueprint.Details
        {
          Name = "Tickets",
          Title = "Tickets",
          Description = "Sistema de atendimento a clientes.",
          Manufacturer = "KeepCoding",
          Copyright = "Copyleft (ɔ) All rights reversed",
          Guid = Guid.Parse("D2C9BA1E-0F97-4C93-9FA9-AB1D5EDA2000"),
          Version = Version.Parse("1.0.0")
        }
      });
      entity.AddLinkSelf(requestUri);

      var href = requestUri.Combine(prefix, "Index");
      entity.AddLink(href, "Início", Rel.Index);

      return entity;
    }
  }
}
