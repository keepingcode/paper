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
using Paper.Media.Design;
using Toolset;
using Paper.Media.Serialization;

namespace Paper.Browser.Gui.Papers
{
  public partial class StatusPaper : UserControl, IPaper
  {
    public StatusPaper(Window window, Content content)
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
      var entity = (Entity)Content.Data;

      var code = entity.GetProperty("Code") ?? 500;
      var status = entity.GetProperty("Status") ?? "Internal Server Error";
      var message = entity.GetProperty("Description");
      var detail = entity.GetProperty("StackTrace");

      lbStatus.Text = $"{code} - {status}";
      lbMessage.Text = message?.ToString() ?? status?.ToString().ChangeCase(TextCase.ProperCase);

      txDetail.Text = detail?.ToString();
      lnDetail.Visible = !string.IsNullOrEmpty(txDetail.Text);
    }

    private void Detail()
    {
      lnDetail.Visible = false;
      txDetail.Visible = true;
    }

    private void lnDetail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Detail();
    }
  }
}
