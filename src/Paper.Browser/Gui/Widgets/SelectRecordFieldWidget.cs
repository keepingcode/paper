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
  public partial class SelectRecordFieldWidget : UserControl, IFieldWidget, tem
  {
    public event EventHandler FieldChanged;
    public event EventHandler ValueChanged;

    private List<PropertyMap> items = new List<PropertyMap>();

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
      get => Multiple ? (object)items : (object)items.FirstOrDefault();
      set
      {
        items.Clear();
        if (value is IEnumerable list)
        {
          items.AddRange(list.OfType<PropertyMap>().Select(CreateRecord));
        }
        else if (value is PropertyMap item)
        {
          items.Add(CreateRecord(item));
        }
        defaultIsEmpty = items.Count == 0;
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

    public bool ReadOnly
    {
      get => btChoose.Visible;
      set => btChoose.Visible = !value;
    }

    public IEnumerable<string> GetErrors()
    {
      if (Required && items.Count == 0)
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
        SetSelection(window.GetSelection());
      }
    }

    private void SetSelection(ICollection<Entity> selection)
    {
      var selectedItems =
        from entity in selection
        select CreateRecord(entity.Properties);

      items.Clear();
      items.AddRange(selectedItems);
      HasChanges = !(defaultIsEmpty && items.Count == 0);
      ValueChanged?.Invoke(this, EventArgs.Empty);
    }

    private PropertyMap CreateRecord(PropertyMap source)
    {
      var target = new PropertyMap();

      var hasFilter = (Keys?.Any() == true);
      if (hasFilter)
      {
        target.AddMany(source.Where(x => x.Key.EqualsAnyIgnoreCase(Keys)));
      }
      else
      {
        target.AddMany(source);
      }

      return target;
    }

    private void FeedbackSelection()
    {
      switch (items.Count)
      {
        case 0:
          txValue.Text = "Nenhum item selecionado";
          break;
        case 1:
          txValue.Text = "1 item selecionado";
          break;
        default:
          txValue.Text = $"{items.Count} itens selecionados";
          break;
      }
    }

    private void btDialog_Click(object sender, EventArgs e)
    {
      OpenDialog();
    }
  }
}