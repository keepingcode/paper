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
  class UsersPage
  {
    [Route(":read")]
    public Target Read(UserFilter filter, Sort sort, Page page)
    {
      return null;
    }

    [Route(":create"), Hint(Hints.Add | Hints.Primary)]
    public Target Create(UserForm[] users)
    {
      return null;
    }

    [Route(":activate"), Hint(Hints.Save)]
    public Target Activate(UserFormForActivation[] users)
    {
      return null;
    }

    [Route(":delete"), Hint(Hints.Delete)]
    public Target Delete(User[] users)
    {
      return null;
    }
  }
}