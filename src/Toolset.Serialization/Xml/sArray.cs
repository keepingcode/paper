namespace Toolset.Serialization.Xml
{
  public interface sArray
  {
    Node Current { get; }
    XmlSerializationSettings Settings { get; }

    void Close();
  }
}