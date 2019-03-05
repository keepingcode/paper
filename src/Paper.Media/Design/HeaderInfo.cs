using System.Linq;
using Paper.Media.Data;
using Toolset;

namespace Paper.Media.Design
{
  /// <summary>
  /// Coleção de propriedades de um cabeçalho.
  /// </summary>
  public class HeaderInfo : IHeaderInfo
  {
    private string _title;
    private string _dataType;

    public HeaderInfo()
    {
    }

    public HeaderInfo(string name)
    {
      this.Name = name;
    }

    public string Name { get; set; }

    public string Title
    {
      get => _title ?? Name?.ChangeCase(TextCase.ProperCase);
      set => _title = value;
    }

    public string DataType
    {
      get => _dataType ?? DataTypeNames.Text;
      set => _dataType = value;
    }

    public bool Hidden { get; set; }

    public SortOrder? Order { get; set; }
  }
}