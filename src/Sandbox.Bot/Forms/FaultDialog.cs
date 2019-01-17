using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Media;
using Toolset;
using Toolset.Xml;

namespace Sandbox.Bot.Forms
{
  public partial class FaultDialog : Form
  {
    public FaultDialog(Ret ret)
    {
      InitializeComponent();
      ExplainFault(ret);
    }

    private void ExplainFault(Ret ret)
    {
      object code = null;
      object status = null;
      object description = null;
      object stackTrace = null;

      var ln = Environment.NewLine;

      var entity = ret.Value as Entity;
      var exception = ret.Value as Exception ?? ret.Fault as Exception;

      var isErrorEntity = entity?.Class.Contains(ClassNames.Error) == true;
      if (isErrorEntity)
      {
        code = entity.Properties["Code"]?.Value;
        status = entity.Properties["Status"]?.Value?.ToString().ChangeCase(TextCase.ProperCase);
        description = entity.Properties["Description"]?.Value;
        stackTrace = entity.Properties["StackTrace"]?.Value;

        if (description != null)
        {
          description = Regex.Replace(description.ToString(), @"(.)\. (.)", $"$1.{ln}$2");
        }
      }
      else if (exception != null)
      {
        description = string.Join(ln, exception.GetCauseMessages().Select(x => $"• {x}"));
        stackTrace = exception.GetStackTrace();
      }

      if (code == null)
      {
        code = ret.Status;
      }
      if (status == null)
      {
        var statusCode = (HttpStatusCode)(code ?? ret.Status);
        status = statusCode.ToString().ChangeCase(TextCase.ProperCase);
      }

      lbMessage.Text = description?.ToString() ?? $"{code} - {status}";
      txDetail.Text = $"{code} - {status}{ln}- - -{ln}{description}{ln}- - -{ln}{stackTrace}";
      lnDetail.Visible = !string.IsNullOrWhiteSpace(txDetail.Text);
    }

    private void Detail()
    {
      lnDetail.Visible = false;
      txDetail.Visible = true;
      if (this.Height < 315)
      {
        this.Height = 400;
      }
    }

    private void lnDetail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Detail();
    }
  }
}
