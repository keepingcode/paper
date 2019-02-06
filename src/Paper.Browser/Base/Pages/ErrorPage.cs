using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Media;
using Paper.Media.Serialization;

namespace Paper.Browser.Base.Pages
{
  public partial class ErrorPage : UserControl, IPage
  {
    public ErrorPage(Window window, Entity entity)
    {
      this.Window = window;
      this.Entity = entity;

      InitializeComponent();

      txFault.Text = Entity.ToJson();
    }

    public Window Window { get; }
    public Entity Entity { get; }
  }
}
