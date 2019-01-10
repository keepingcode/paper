using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paper.Media;
using Sandbox.Demo.Domain;
using Sandbox.Demo.Models;
using Sandbox.Spec;
using Toolset;
using Toolset.Data;

namespace Sandbox.Demo.Pages
{
  class UserPage
  {
    [Route(":read")]
    public Target Read(int id)
    {
      return null;
    }

    [Route(":update"), Hint(Hints.Save | Hints.Primary)]
    public Target Update(UserForm user)
    {
      return null;
    }

    [Route(":create"), Hint(Hints.Add | Hints.Secondary)]
    public Target Create(UserForm user)
    {
      return null;
    }

    [Route(":activate"), Hint(Hints.Check | Hints.Secondary)]
    public Target Activate(UserFormForActivation[] users)
    {
      return null;
    }

    [Route(":delete"), Hint(Hints.Delete | Hints.Secondary)]
    public Target Delete(User user)
    {
      return null;
    }
  }
}