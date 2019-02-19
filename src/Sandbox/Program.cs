using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.CSharp;
using Paper.Api.Extensions.Papers;
using Paper.Api.Rendering;
using Paper.Media;
using Paper.Media.Data;
using Paper.Media.Design;
using Paper.Media.Serialization;
using Toolset;
using Toolset.Collections;
using Toolset.Data;
using Toolset.Net;
using Toolset.Reflection;
using Toolset.Sequel;
using Toolset.Serialization;
using Toolset.Serialization.Csv;
using Toolset.Serialization.Graph;
using Toolset.Serialization.Json;
using Toolset.Serialization.Xml;
using Toolset.Xml;

namespace Sandbox
{
  [Expose]
  public class UsuarioPapers
  {
    public static TBusuario[] DB =
    {
      new TBusuario { DFid_usuario = 1, DFnome_usuario = "Fulano" },
      new TBusuario { DFid_usuario = 2, DFnome_usuario = "Beltrano" },
      new TBusuario { DFid_usuario = 2, DFnome_usuario = "Cicrano" }
    };

    public class TBusuario
    {
      public int DFid_usuario { get; set; }
      public string DFnome_usuario { get; set; }
    }

    public class UsuarioForm
    {
      public string DFnome_usuario { get; set; }
    }

    public class UsuarioFilter : IFilter
    {
      public Var<int> DFid_usuario { get; set; }
      public Var<string> DFnome_usuario { get; set; }
    }

    public class OlaPaper : IPaper
    {
      public object Index() => new { Texto = "Olá, mundo!" };
    }

    public class UsuariosPaper : IPaper
    {
      public void CreateSort(Sort sort) => sort.AddFieldsFrom<TBusuario>();
      public void CreateForm(UsuarioFilter filter) => new UsuarioFilter();

      public Ret<TBusuario[]> Index(Sort sort, Page page, UsuarioFilter filter)
      {
        try
        {
          var rows = DB.FilterBy(filter).SortBy(sort).PaginateBy(page).ToArray();
          if (rows.Length > 0)
          {
            return rows;
          }
          else
          {
            return Redirect.To<OlaPaper>();
          }
        }
        catch (Exception ex)
        {
          throw ex;
        }
      }

      public Ret Save(UsuarioForm form, IEnumerable<TBusuario> usuarios)
      {
        return Redirect.To<OlaPaper>();
      }
    }

    public class UsuarioPaper : IPaper
    {

      // métodos Create
      // métodos Format

      public void CreteForm(UsuarioFilter filter) => new UsuarioFilter();

      public Ret<TBusuario> Index(int id)
      {
        return Redirect.To<OlaPaper>();
      }

      public Ret Save(UsuarioForm form, TBusuario usuario)
      {
        return Redirect.To<OlaPaper>();
      }
    }
  }
}