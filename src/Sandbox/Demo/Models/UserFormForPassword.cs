using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.Spec;

namespace Sandbox.Demo.Models
{
  class UserFormForPassword
  {
    [Hidden]
    public int? Id { get; set; }

    [Text("Senha")]
    public string Password { get; set; }
  }
}
