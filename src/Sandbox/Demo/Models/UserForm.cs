using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.Spec;

namespace Sandbox.Demo.Models
{
  class UserForm
  {
    [Hidden]
    public int? Id { get; set; }

    public string Login { get; set; }

    [Text("Nome")]
    public string Name { get; set; }

    [Text("Sobrenome")]
    public string Surname { get; set; }
  }
}
