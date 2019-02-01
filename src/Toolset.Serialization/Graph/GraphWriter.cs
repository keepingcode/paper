using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Toolset.Reflection;

namespace Toolset.Serialization.Graph
{
  public class GraphWriter2<T> : GraphWriter
    where T : class
  {
    public GraphWriter2()
      : base(typeof(T))
    {
      // nada a fazer aqui. use o outro construtor.
    }

    public GraphWriter2(SerializationSettings settings)
      : base(typeof(T), settings)
    {
      // nada a fazer aqui. use o outro construtor.
    }

    public GraphWriter2(IEnumerable<Type> knownTypes, SerializationSettings settings)
      : base(typeof(T), knownTypes, settings)
    {
    }
  }

  public class GraphWriter : Writer
  {
    private ArrayList graphs = new ArrayList();
    private Stack<Scope> scopes = new Stack<Scope>();
    private Stack<NodeType> depth = new Stack<NodeType>();

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

        var property = properties.Last();

        if (adders != null)
        {
          object key, val;
          foreach (var adder in adders)
          {
            var keyType = adder.GetParameters().First().ParameterType;
            var valType = adder.GetParameters().Last().ParameterType;
            if (Change.TryTo(property, keyType, out key) && Change.TryTo(value, valType, out val))
            {
              Host._Call("Add", key, val);
              return;
            }
          }
        }

        Host._Set(property, value);
      }

      public object NewValue()
      {
        if (Bag != null)
        {
          var value = Activator.CreateInstance(Bag.ElementType);
          Bag.Add(value);
          return value;
        }
        else
        {
          var property = properties.LastOrDefault();
          return Host._SetNew(property);
        }
      }

      public Bag NewBag()
      {
        var property = properties.LastOrDefault();
        var propertyType = Host._GetPropertyType(property);
        var elementType = TypeOf.CollectionElement(propertyType);
        return new Bag { ElementType = elementType };
      }
    }

    private class Bag : ArrayList
    {
      public Type ElementType { get; set; }
    }
  }
}
