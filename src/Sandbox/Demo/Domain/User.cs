using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.Spec;

namespace Sandbox.Demo.Domain
{
  [Text("Usuario")]
  class User
  {
    public string Login { get; set; }

    [Text("Nome")]
    public string Name { get; set; }

    [Text("Sobrenome")]
    public string Surname { get; set; }

    [Text("Senha")]
    public string Password { get; set; }

    [Text("Ativo")]
    public bool Active { get; set; }
  }
}
