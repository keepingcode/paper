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

namespace Paper.Browser.Base.Pages
{
  public partial class DataPage : UserControl, IPage
  {
    public DataPage(Window window, Entity entity)
    {
      this.Window = window;
      this.Entity = entity;
      InitializeComponent();
    }

    public Window Window { get; }
    public Entity Entity { get; }
  }
}
