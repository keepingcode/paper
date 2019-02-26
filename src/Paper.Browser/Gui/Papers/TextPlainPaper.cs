using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Browser.Lib;
using Paper.Media;
using Paper.Media.Serialization;
using System.IO;

namespace Paper.Browser.Gui.Papers
{
  public partial class TextPlainPaper : UserControl, IPaper
  {
    public TextPlainPaper(Window window, Content content)
    {
      this.Window = window;
      this.Content = content;
      InitializeComponent();
      InitializeData();
    }

    public Control Control => this;

    public Window Window { get; }

    public Content Content { get; }

    private void InitializeData()
    {
      if (Content.Data is Entity entity)
      {
        txContent.Text = entity.ToJson();
      }
      else if (Content.Data is byte[] data)
      {
        using (var memory = new MemoryStream())
        {
          memory.Write(data, 0, data.Length);
          memory.Position = 0;
          txContent.Text = new StreamReader(memory).ReadToEnd();
        }
      }
      else
      {
        txContent.Text = Content.Data?.ToString() ?? "Não há dados para exibição.";
      }
    }
  }
}