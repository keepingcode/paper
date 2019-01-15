using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Media.Design.Papers;

namespace Sandbox.Bot.Forms
{
  partial class AboutDialog : Form
  {
    public AboutDialog(Blueprint blueprint)
    {
      InitializeComponent();
      lbProductName.Text = blueprint.Info?.Title;
      lbManufacturer.Text = blueprint.Info?.Manufacturer;
      lbVersion.Text = blueprint.Info?.Version?.ToString();
      lbGuid.Text = blueprint.Info?.Guid.ToString("D").ToUpper();
      lbCopyright.Text = blueprint.Info?.Copyright;
      txDescription.Text = blueprint.Info?.Description;
    }
  }
}
