using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.Spec;
using Toolset.Data;

namespace Sandbox.Demo.Models
{
  [Text("Filtro de Usuário")]
  class UserFilter
  {
    [Hidden]
    public Var<int> Id { get; set; }

    public VarString Login { get; set; }

    [Text("Nome")]
    public VarString Name { get; set; }

    [Text("Sobrenome")]
    public VarString Surname { get; set; }

    [Text("Senha")]
    public VarString Password { get; set; }
  }
}
