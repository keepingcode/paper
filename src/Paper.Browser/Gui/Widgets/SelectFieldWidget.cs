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
using Toolset;
using System.Collections;
using Toolset.Collections;
using static System.Windows.Forms.ListBox;

namespace Paper.Browser.Gui.Widgets
{
  public partial class SelectFieldWidget : UserControl, IFieldWidget
  {
    public event EventHandler FieldChanged;
    public event EventHandler ValueChanged;

    private object sourceValue;

    private Field _field;
    private Extent _gridExtent;

    public SelectFieldWidget()
    {
      InitializeComponent();
      this.Enhance();
      this.EnhanceFieldWidget();
      cbValue.SelectedIndexChanged += (o, e) => ValueChanged?.Invoke(this, EventArgs.Empty);
      lbValue.SelectedIndexChanged += (o, e) => ValueChanged?.Invoke(this, EventArgs.Empty);
    }

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

        var options = value?.Value as FieldValueCollection;

        Options = new OptionCollection(options);
        Value = options?
          .Where(x => x.Selected == true)
          .Select(x => x.Value)
          .ToArray();

        UpdateLayout();

        FieldChanged?.Invoke(this, EventArgs.Empty);
      }
    }

    public bool Required
    {
      get => Field?.Required == true;
    }

    public bool Multiline
    {
      get => Field?.Multiline == true;
    }

    public OptionCollection Options
    {
      get => cbValue.DataSource as OptionCollection;
      set
      {
        cbValue.DataSource = lbValue.DataSource = value;
        cbValue.DisplayMember = lbValue.DisplayMember = "Title";
        cbValue.ValueMember = lbValue.ValueMember = "Value";
      }
    }

    public bool HasChanges
    {
      get
      {
        if (Multiline)
        {
          var sourceArray = (object[])sourceValue;
          var targetArray = (object[])Value;

          if ((sourceArray == null) != (targetArray == null))
            return false;

          return sourceArray.All(x => targetArray.Contains(x));
        }
        else
        {
          return sourceValue != Value;
        }
      }
    }

    public object Value
    {
      get
      {
        if (Multiline)
        {
          var values = (
            from item in lbValue.SelectedItems.OfType<Option>()
            select item.Value
          ).ToArray();
          return values.Length == 0 ? null : values;
        }
        else
        {
          return (cbValue.SelectedItem as Option)?.Value;
        }
      }
      set
      {
        if (value == null)
        {
          sourceValue = null;
          lbValue.SelectedItems.Clear();
          cbValue.SelectedItem = null;
          return;
        }

        if (Multiline)
        {
          var items = Is.Collection(value)
            ? ((IEnumerable)value).Cast<object>().ToArray()
            : new[] { value };

          var options = this.Options;
          var selectedOptions =
            from item in items
            select options.FirstOrDefault(x => x.Value == item);

          if (Required)
          {
            selectedOptions = Option.Empty.AsSingle().Concat(selectedOptions);
          }

          foreach (var option in selectedOptions)
          {
            lbValue.SelectedItems.Add(option);
          }

          sourceValue = selectedOptions.Select(x => x.Value).ToArray();
        }
        else
        {
          if (Is.Collection(value))
          {
            value = ((IEnumerable)value).Cast<object>().FirstOrDefault();
          }

          var options = this.Options;
          var option = options.FirstOrDefault(x => x.Value == value);

          cbValue.SelectedItem = option;

          sourceValue = option?.Value;
        }

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
      if (Field == null)
        yield break;

      if (Field.Required == true && Value == null)
        yield return "Campo requerido";
    }

    private void UpdateLayout()
    {
      if (Multiline)
      {
        lbValue.Visible = true;
        cbValue.Visible = false;
        GridExtent = new Extent(6, 5);
      }
      else
      {
        lbValue.Visible = false;
        cbValue.Visible = true;
        GridExtent = new Extent(6, 1);
      }
    }

    public class OptionCollection : List<Option>
    {
      public OptionCollection(FieldValueCollection options)
      {
        if (options == null)
          return;

        this.AddRange(
          from opt in options
          select new Option
          {
            Value = opt.Value,
            Title = opt.Title ?? opt.Value?.ToString()
          }
        );
      }
    }

    public class Option
    {
      public static readonly Option Empty = new Option { Title = "(Nenhum)" };

      public string Title { get; set; }
      public object Value { get; set; }
    }
  }
}
