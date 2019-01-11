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
      lbCopyright.Text = blueprint.Info.Copyright;
      lbManufacturer.Text = blueprint.Info.Manufacturer;
      lbProductName.Text = blueprint.Info.Title;
      lbVersion.Text = blueprint.Info.Version.ToString();
      txDescription.Text = blueprint.Info.Description;
    }
  }
}
