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
using Paper.Media.Design;
using Toolset;
using System.Collections;
using Paper.Browser.Lib;
using Paper.Browser.Lib.Pages;

namespace Paper.Browser.Gui.Widgets
{
  public partial class SelectRecordFieldWidget : UserControl, IFieldWidget
  {
    public event EventHandler FieldChanged;
    public event EventHandler ValueChanged;

    private EntityCollection entities = new EntityCollection();

    private bool defaultIsEmpty;

    private Field _field;
    private Extent _gridExtent;

    public SelectRecordFieldWidget()
    {
      InitializeComponent();
      this.Enhance();
      this.EnhanceFieldWidget();
      GridExtent = new Extent(6, 1);
      FeedbackSelection();
      ValueChanged += (o, e) => FeedbackSelection();
    }

    public Window Window { get; set; }

    public UserControl Host => this;

    public Label Label => lbText;

    public IContainer Components => components ?? (components = new Container());

    public Entity Entity { get; set; }

    public Field Field
    {
      get => _field;
      set
      {
        _field = value;
        if (_field?.Provider != null)
        {
          var isSelfProvider = _field.Provider.Rel.Has(RelNames.Self);
          this.Href = isSelfProvider ? Entity.GetSelfHref() : _field?.Provider.Href;
          this.Keys = _field.Provider.Keys;
        }
        else
        {
          this.Href = null;
          this.Keys = null;
        }
        Value = _field.Value;
        FieldChanged?.Invoke(this, EventArgs.Empty);
      }
    }

    public bool Required => Field?.Required == true;

    public bool Multiple => Field?.Multiline == true;

    public Href Href { get; set; }

    public NameCollection Keys { get; set; }

    public bool HasChanges { get; private set; }

    public object Value
    {
      get => Multiple ? (object)entities : (object)entities.FirstOrDefault();
      set
      {
        entities.Clear();
        if (value is IEnumerable list)
        {
          entities.AddMany(list.OfType<Entity>());
        }
        else if (value is Entity entity)
        {
          entities.Add(entity);
        }
        defaultIsEmpty = entities.Count == 0;
        HasChanges = false;
        ValueChanged?.Invoke(this, EventArgs.Empty);
      }
    }

    public Extent GridExtent
    {
      get => _gridExtent;
      set
      {
        _gridExtent = value;
        this.Size = _gridExtent.ToSize(WidgetGridLayout.Metrics);
      }
    }

    public IEnumerable<string> GetErrors()
    {
      if (Required && entities.Count == 0)
        yield return "Campo requerido";
    }

    private void OpenDialog()
    {
      var window = Window.Desktop.CreateWindow();
      window.Host.Text = "Caixa de Seleção";
      window.Mode = Mode.SelectBox;
      window.RequestAsync(Href).RunAsync();
      var result = window.Host.ShowDialog(this);
      if (result == DialogResult.OK)
      {
        entities.Clear();
        entities.AddMany(window.GetSelection());
        HasChanges = !(defaultIsEmpty && entities.Count == 0);
        ValueChanged?.Invoke(this, EventArgs.Empty);
      }
    }

    private void FeedbackSelection()
    {
      switch (entities.Count)
      {
        case 0:
          txValue.Text = "Nenhum item selecionado";
          break;
        case 1:
          txValue.Text = "1 item selecionado";
          break;
        default:
          txValue.Text = $"{entities.Count} itens selecionados";
          break;
      }
    }

    private void btDialog_Click(object sender, EventArgs e)
    {
      OpenDialog();
    }
  }
}