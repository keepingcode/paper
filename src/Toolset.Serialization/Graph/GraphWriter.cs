using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using Toolset.Collections;
using Toolset.Reflection;

namespace Toolset.Serialization.Graph
{
  public class GraphWriter<T> : GraphWriter
    where T : class, new()
  {
    public GraphWriter()
      : base(typeof(T))
    {
      // nada a fazer aqui. use o outro construtor.
    }

    public GraphWriter(SerializationSettings settings)
      : base(typeof(T), settings)
    {
      // nada a fazer aqui. use o outro construtor.
    }

    public GraphWriter(IEnumerable<Type> knownTypes, SerializationSettings settings)
      : base(typeof(T), knownTypes, settings)
    {
    }

    public new ICollection<T> Graphs => (ICollection<T>)base.Graphs;
  }

  public class GraphWriter : Writer
  {
    private readonly IList graphs;
    private readonly Stack<Scope> scopes;

    public GraphWriter(Type type)
      : this(type, null, new SerializationSettings())
    {
      // nada a fazer aqui. use o outro construtor.
    }

    public GraphWriter(Type type, SerializationSettings settings)
      : this(type, null, settings)
    {
      // nada a fazer aqui. use o outro construtor.
    }

    public GraphWriter(Type type, IEnumerable<Type> knownTypes, SerializationSettings settings)
      : base(settings is GraphSerializationSettings ? (GraphSerializationSettings)settings : new GraphSerializationSettings(settings))
    {
      this.GraphType = type;
      this.graphs = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(type));
      this.scopes = new Stack<Scope>();
    }

    public Type GraphType { get; }

    public ICollection Graphs => graphs;

    public new GraphSerializationSettings Settings => (GraphSerializationSettings)base.Settings;

    protected override void DoWrite(Node node)
    {
      switch (node.Type)
      {
        case NodeType.ObjectStart:
          {
            if (scopes.Count == 0)
            {
              var host = Activator.CreateInstance(GraphType);
              scopes.Push(new Scope(host, null));
            }
            else
            {
              var current = scopes.Peek();
              var host = current.NewValue();
              scopes.Push(new Scope(host, null));
            }
            break;
          }

        case NodeType.ObjectEnd:
          {
            var scope = scopes.Pop();
            if (scopes.Count == 0)
            {
              graphs.Add(scope.Host);
            }
            break;
          }

        case NodeType.PropertyStart:
          {
            var current = scopes.Peek();
            current.AddProperty(Change.To<string>(node.Value));
            break;
          }

        case NodeType.CollectionStart:
          {
            var current = scopes.Peek();
            var bag = current.NewBag();
            scopes.Push(new Scope(null, bag));
            break;
          }

        case NodeType.CollectionEnd:
          {
            var scope = scopes.Pop();
            var current = scopes.Peek();
            current.SetValue(scope.Bag);
            break;
          }

        case NodeType.Value:
          {
            var current = scopes.Peek();
            current.SetValue(node.Value);
            break;
          }
      }
    }

    protected override void DoWriteComplete()
    {
      // nada a fazer
    }

    protected override void DoFlush()
    {
      // nada a fazer
    }

    protected override void DoClose()
    {
      // nada a fazer
    }

    private class Scope
    {
      private readonly List<string> properties = new List<string>();
      private readonly MethodInfo[] adders;

      public Scope(object host, Bag bag)
      {
        this.Host = host;
        this.Bag = bag;
        if (host != null)
        {
          this.adders = (
            from method in host.GetType().GetMethods()
            let parameters = method.GetParameters()
            where method.Name == "Add"
               && parameters.Length == 2
               && typeof(string).IsAssignableFrom(parameters.First().ParameterType)
            select method
          ).ToArray();
        }
      }

      public object Host { get; }

      public Bag Bag { get; }

      public void AddProperty(string name)
      {
        if (name == null)
        {
          do
          {
            name = $"Field{properties.Count + 1}";
          } while (properties.Contains(name));
        }
        properties.Add(name);
      }

      public void SetValue(object value)
      {
        if (Bag != null)
        {
          Bag.Add(value);
          return;
        }

        if ((value is Bag) && (Host is IDictionary map))
        {
          var bag = (Bag)value;

          var type = TypeOf.DictionaryValue(map);
          if (type == typeof(object))
          {
            var attrs = Host._GetAttributes<KnownTypeAttribute>();
            type = (
              from attr in attrs
              where typeof(ICollection).IsAssignableFrom(attr.Type)
                 && !typeof(IDictionary).IsAssignableFrom(attr.Type)
              select attr.Type
            ).FirstOrDefault();

            if (type == null)
              throw new NotSupportedException("Tipo de mapeamento não suportado: " + Host.GetType().FullName);
          }
          value = Change.To(value, type);
        }

        var realName = properties.Last();

        if (adders != null)
        {
          object key, val;
          foreach (var adder in adders)
          {
            var keyType = adder.GetParameters().First().ParameterType;
            var valType = adder.GetParameters().Last().ParameterType;
            if (Change.Try(realName, keyType, out key) && Change.Try(value, valType, out val))
            {
              Host._Call("Add", key, val);
              return;
            }
          }
        }

        var propName = ResolvePropertyName(Host, realName);

        if (value is Bag)
        {
          var bag = (Bag)value;
          if (bag.BagType != null)
          {
            var list = (IList)Activator.CreateInstance(bag.BagType);
            foreach (var item in bag)
            {
              list.Add(item);
            }
            value = list;
          }
        }

        Host._Set(propName, value);
      }

      public object NewValue()
      {
        if (Bag != null)
        {
          var value = Activator.CreateInstance(Bag.ElementType);
          Bag.Add(value);
          return value;
        }
        else if (Host is IDictionary map)
        {
          var type = TypeOf.DictionaryValue(map);
          if (type == typeof(object))
          {
            var attrs = Host._GetAttributes<KnownTypeAttribute>();
            type = (
              from attr in attrs
              where typeof(IDictionary).IsAssignableFrom(attr.Type)
              select attr.Type
            ).FirstOrDefault();

            if (type == null)
              throw new NotSupportedException("Tipo de mapeamento não suportado: " + Host.GetType().FullName);
          }
          var instance = Activator.CreateInstance(type);
          var property = properties.LastOrDefault();
          map[property] = instance;
          return instance;
        }
        else
        {
          var property = properties.LastOrDefault();
          var propName = ResolvePropertyName(Host, property);
          return Host._SetNew(propName);
        }
      }

      public Bag NewBag()
      {
        var property = properties.LastOrDefault();

        var factory = Host as IListTypeFactory;
        var propertyType = factory?.CreateListType(property.ChangeCase(TextCase.PascalCase));
        if (propertyType == null)
        {
          propertyType = Host._GetPropertyType(property);
        }

        var elementType = TypeOf.CollectionElement(propertyType);
        return new Bag
        {
          BagType = propertyType,
          ElementType = elementType
        };
      }

      private string ResolvePropertyName(object host, string propertyName)
      {
        var alias = propertyName.Replace("@", "");
        var propName = (
          from property in host.GetType().GetProperties()
          from attribute in property.GetCustomAttributes<DataMemberAttribute>()
          where attribute.Name.EqualsIgnoreCase(alias)
          select property.Name
        ).FirstOrDefault() ?? alias;
        return propName;
      }
    }

    private class Bag : ArrayList
    {
      public Type BagType { get; set; }
      public Type ElementType { get; set; }
    }

    private class Map : HashMap
    {
    }
  }
}
