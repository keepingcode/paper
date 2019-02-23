//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using Paper.Browser.Base.Forms;

//namespace Paper.Browser.Base
//{
//  public static class Navigator
//  {
//    public static NavigatorForm Form { get; } = new NavigatorForm();

//    public static Window NewWindow()
//    {
//      var form = new WindowForm();
//      form.Text = "Página em Branco";
//      //form.MdiParent = Form;
//      form.Show(Form);

//      var window = new Window(form);
//      return window;
//    }
//  }
//}