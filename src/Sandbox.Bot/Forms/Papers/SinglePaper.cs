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

namespace Sandbox.Bot.Forms.Papers
{
  public partial class SinglePaper : UserControl
  {
    private Entity entity;

    public SinglePaper(Entity entity)
    {
      this.entity = entity;
      InitializeComponent();
      LoadEntity();
    }
    
    private void LoadEntity()
    {
      Text = entity.Title;


    }
  }
}
