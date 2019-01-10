using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.Spec;

namespace Sandbox.Demo.Models
{
  class UserFormForActivation
  {
    [Hidden]
    public int? Id { get; set; }

    [Text("Ativo")]
    public bool Active { get; set; }
  }
}
