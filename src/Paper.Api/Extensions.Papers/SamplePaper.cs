using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paper.Media;
using Paper.Media.Attributes;
using Paper.Media.Data;
using Paper.Media.Design;
using Toolset;
using Toolset.Collections;

namespace Paper.Api.Extensions.Papers
{
  public class SamplePaper
  {
    public class Task
    {
      public int Id { get; set; }
      public string Name { get; set; }
      public int Age { get; set; }
      public DateTime Date { get; set; }
      public decimal Price { get; set; }
      public bool Active { get; set; }
      public string Description { get; set; }
    }

    public class TaskFilter : IFilter
    {
      public Var<int?> Id { get; set; }
      public Var<string> Name { get; set; }
      public Var<int?> Age { get; set; }
      public Var<DateTime?> Date { get; set; }
      public Var<decimal?> Price { get; set; }
      public Var<bool?> Active { get; set; }
      public Var<string> Description { get; set; }
    }

    public class TaskForm : IForm
    {
      [Required]
      [DefaultValue("Exemplo")]
      public string Name { get; set; }

      [Required]
      [DefaultValue("Exemplo de formulário")]
      public string Description { get; set; }

      [DefaultValue(50)]
      [Option(10, "Dek jaroj")]
      [Option(20, "Dudek jaroj")]
      [Option(30, "Tridek jaroj")]
      [Option(40, "Kvardek jaroj")]
      [Option(50, "Kvindek jaroj")]
      [Option(60, "Sesdek jaroj")]
      [Option(70, "Sepdek jaroj")]
      [Option(80, "Okdek jaroj")]
      [Option(90, "Naŭdek jaroj")]
      [Option(100, "Cent jaroj")]
      public int Age { get; set; }

      [DefaultValue("2018-12-31T23:59:59")]
      public DateTime Date { get; set; }

      [DefaultValue(50.05)]
      public decimal Price { get; set; }

      [DefaultValue(true)]
      public bool Active { get; set; }
    }

    public static class DB
    {
      public static List<Task> Tasks = new List<Task> {
          new Task { Id = 1 , Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Lorem"              , Description = "Lorem ipsum dolor sit amet"         }
        , new Task { Id = 2 , Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "In a lorem"         , Description = "In a lorem id purus eleifend"       }
        , new Task { Id = 3 , Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Pellentesque"       , Description = "Pellentesque"                       }
        , new Task { Id = 4 , Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Pellentesque"       , Description = "Pellentesque venenatis leo"         }
        , new Task { Id = 5 , Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Sed"                , Description = "Sed in leo"                         }
        , new Task { Id = 6 , Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Duis"               , Description = "Duis accumsan lacus"                }
        , new Task { Id = 7 , Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Quisque"            , Description = "Quisque at ante"                    }
        , new Task { Id = 8 , Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Vivamus convallis"  , Description = "Vivamus a enim et tortor convallis" }
        , new Task { Id = 9 , Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Nullam"             , Description = "Nullam gravida"                     }
        , new Task { Id = 10, Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Phasellus"          , Description = "Phasellus a felis consequat"        }
        , new Task { Id = 11, Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Curabitur"          , Description = "Curabitur"                          }
        , new Task { Id = 12, Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Vivamus"            , Description = "Vivamus at felis"                   }
        , new Task { Id = 13, Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Integer"            , Description = "Integer auctor sapien"              }
        , new Task { Id = 14, Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Suspendisse"        , Description = "Suspendisse sed arcu"               }
        , new Task { Id = 15, Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Cras"               , Description = "Cras a felis"                       }
        , new Task { Id = 16, Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Vestibulum"         , Description = "Vestibulum"                         }
        , new Task { Id = 17, Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Nunc"               , Description = "Nunc rhoncus"                       }
        , new Task { Id = 18, Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Quisque"            , Description = "Quisque vel risus"                  }
        , new Task { Id = 19, Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "In non lectus"      , Description = "In non lectus egestas"              }
        , new Task { Id = 20, Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Aenean"             , Description = "Aenean interdum"                    }
        , new Task { Id = 21, Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Quisque"            , Description = "Quisque viverra arcu nec libero"    }
        , new Task { Id = 22, Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Nulla"              , Description = "Nulla a sem in lorem posuere"       }
      };
    }

    [Expose]
    public class TasksPaper : IPaper
    {
      public Ret<Task[]> Index(Sort sort, Page page, TaskFilter filter)
      {
        return DB.Tasks.FilterBy(filter).SortBy(sort).PaginateBy(page).ToArray();
      }

      public IEnumerable<IFormatter> Format(Task[] tasks)
      {
        yield return Links.Link("http://www.google.com").SetTitle("Google Search");
        yield return Tables.MakeTable(tasks);
      }

      public IEnumerable<Link> Link(Task task)
      {
        yield return Links.Self<TaskPaper>(task.Id);
      }

      public Ret Edit(TaskForm form, Task[] tasks)
      {
        var currentTasks = DB.Tasks.Where(task => tasks.Any(x => x.Id == task.Id));

        if (form.Name != null)
        {
          currentTasks.ForEach(x => x.Name = form.Name);
        }

        return true;
      }

      public Ret Remove(Task[] tasks)
      {
        DB.Tasks.RemoveAll(task => tasks.Any(x => x.Id == task.Id));
        return true;
      }

      public Sort CreateSort()
      {
        return new Sort()
          .AddField<Task>(x => x.Id)
          .AddField<Task>(x => x.Name)
          .AddField<Task>(x => x.Age)
          .AddField<Task>(x => x.Date)
          .AddField<Task>(x => x.Price)
          .AddField<Task>(x => x.Active)
          .AddField<Task>(x => x.Description);
      }
    }
    
    [Expose]
    public class TaskPaper : IPaper
    {
      public Task Index(int taskId)
      {
        return DB.Tasks.FirstOrDefault(x => x.Id == taskId);
      }

      public IEnumerable<Link> Link()
      {
        yield return Links.Link<TasksPaper>().SetTitle("Tarefas");
      }

      public IEnumerable<Link> Link(int taskId)
      {
        var prev = DB.Tasks.Where(x => x.Id < taskId).Select(x => x.Id).DefaultIfEmpty().Max();
        var next = DB.Tasks.Where(x => x.Id > taskId).Select(x => x.Id).DefaultIfEmpty().Min();
        if (prev > 0)
        {
          yield return Links.Link<TaskPaper>(prev).SetTitle("Anterior");
        }
        if (next > 0)
        {
          yield return Links.Link<TaskPaper>(next).SetTitle("Próximo");
        }
      }

      public IEnumerable<Link> Link(Task task)
      {
        yield return Links.Link($"http://www.google.com?q={task.Name}").SetTitle("Google Search");
      }

      public void Edit(TaskForm changes, Task task)
      {
      }

      public Ret Remove(int taskId)
      {
        DB.Tasks.RemoveAll(x => x.Id == taskId);
        return Redirect.To<TasksPaper>();
      }
    }
  }
}
