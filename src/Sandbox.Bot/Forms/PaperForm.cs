using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Media;
using Sandbox.Bot.Forms.Papers;

namespace Sandbox.Bot.Forms
{
  public partial class PaperForm : Form
  {
    public PaperForm(Entity entity)
    {
      InitializeComponent();

      this.Entity = entity;
      this.PaperControl = CreatePaperControl(entity);
      this.tsContainer.ContentPanel.Controls.Add(this.PaperControl);
    }

    public Entity Entity { get; set; }

    public Control PaperControl { get; set; }

    private Control CreatePaperControl(Entity entity)
    {
      if (entity == null)
        return new SandboxPaper();

      return new SinglePaper(entity);
    }
  }
}