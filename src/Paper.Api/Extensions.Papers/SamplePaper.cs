using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paper.Media;
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
      //public string Field1 { get; set; }
      //public string Field2 { get; set; }
      //public string Field3 { get; set; }
      //public string Field4 { get; set; }
      //public string Field5 { get; set; }
      //public string Field6 { get; set; }
      //public string Field7 { get; set; }
      //public string Field8 { get; set; }
      //public string Field9 { get; set; }
      //public string Field10 { get; set; }
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
      //public Var<string> Field1 { get; set; }
      //public Var<string> Field2 { get; set; }
      //public Var<string> Field3 { get; set; }
      //public Var<string> Field4 { get; set; }
      //public Var<string> Field5 { get; set; }
      //public Var<string> Field6 { get; set; }
      //public Var<string> Field7 { get; set; }
      //public Var<string> Field8 { get; set; }
      //public Var<string> Field9 { get; set; }
      //public Var<string> Field10 { get; set; }
    }

    public class TaskForm : IForm
    {
      public string Name { get; set; }
      public string Description { get; set; }
      public int Age { get; set; }
      public DateTime Date { get; set; }
      public decimal Price { get; set; }
      public bool Active { get; set; }
      //public string Field1 { get; set; }
      //public string Field2 { get; set; }
      //public string Field3 { get; set; }
      //public string Field4 { get; set; }
      //public string Field5 { get; set; }
      //public string Field6 { get; set; }
      //public string Field7 { get; set; }
      //public string Field8 { get; set; }
      //public string Field9 { get; set; }
      //public string Field10 { get; set; }
    }

    public static class DB
    {
      public static List<Task> Tasks = new List<Task> {
          new Task { Id = 1 , Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Lorem"              , Description = "Lorem ipsum dolor sit amet"         } // , Field1 = "consectetur"      , Field2 = "adipiscing" , Field3 = "elit." }
        , new Task { Id = 2 , Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "In a lorem"         , Description = "In a lorem id purus eleifend"       } // , Field1 = "convallis"        , Field2 = "id"         , Field3 = "tincidunt"    , Field4 = "ex." }
        , new Task { Id = 3 , Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Pellentesque"       , Description = "Pellentesque"                       } // , Field1 = "at"               , Field2 = "nisl"       , Field3 = "id"           , Field4 = "dolor"      , Field5 = "efficitur"    , Field6 = "convallis", Field7 = "vitae"      , Field8 = "sit", Field9 = "amet", Field10 = "arcu." }
        , new Task { Id = 4 , Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Pellentesque"       , Description = "Pellentesque venenatis leo"         } // , Field1 = "non"              , Field2 = "aliquet"    , Field3 = "fermentum." }
        , new Task { Id = 5 , Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Sed"                , Description = "Sed in leo"                         } // , Field1 = "vel"              , Field2 = "felis"      , Field3 = "ullamcorper"  , Field4 = "hendrerit"  , Field5 = "a"            , Field6 = "quis"     , Field7 = "orci." }
        , new Task { Id = 6 , Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Duis"               , Description = "Duis accumsan lacus"                } // , Field1 = "quis"             , Field2 = "magna"      , Field3 = "placerat"     , Field4 = "id"         , Field5 = "malesuada"    , Field6 = "turpis"   , Field7 = "ultricies." }
        , new Task { Id = 7 , Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Quisque"            , Description = "Quisque at ante"                    } // , Field1 = "at"               , Field2 = "nisl"       , Field3 = "accumsan"     , Field4 = "dignissim." }
        , new Task { Id = 8 , Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Vivamus convallis"  , Description = "Vivamus a enim et tortor convallis" } // , Field1 = "hendrerit"        , Field2 = "ut"         , Field3 = "id"           , Field4 = "dolor." }
        , new Task { Id = 9 , Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Nullam"             , Description = "Nullam gravida"                     } // , Field1 = "mi tristique"     , Field2 = "ipsum"      , Field3 = "pulvinar"     , Field4 = "iaculis." }
        , new Task { Id = 10, Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Phasellus"          , Description = "Phasellus a felis consequat"        } // , Field1 = "tristique"        , Field2 = "tortor"     , Field3 = "vel"          , Field4 = "blandit"    , Field5 = "ante." }
        , new Task { Id = 11, Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Curabitur"          , Description = "Curabitur"                          } // , Field1 = "sed nunc"         , Field2 = "a"          , Field3 = "magna"        , Field4 = "tristique"  , Field5 = "pulvinar"     , Field6 = "eu"       , Field7 = "eu"         , Field8 = "velit." }
        , new Task { Id = 12, Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Vivamus"            , Description = "Vivamus at felis"                   } // , Field1 = "non nisl"         , Field2 = "gravida"    , Field3 = "imperdiet"    , Field4 = "a"          , Field5 = "vitae"        , Field6 = "massa." }
        , new Task { Id = 13, Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Integer"            , Description = "Integer auctor sapien"              } // , Field1 = "a egestas"        , Field2 = "faucibus." }
        , new Task { Id = 14, Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Suspendisse"        , Description = "Suspendisse sed arcu"               } // , Field1 = "at felis"         , Field2 = "tempor"     , Field3 = "mattis." }
        , new Task { Id = 15, Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Cras"               , Description = "Cras a felis"                       } // , Field1 = "non dui"          , Field2 = "dictum"     , Field3 = "vestibulum." }
        , new Task { Id = 16, Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Vestibulum"         , Description = "Vestibulum"                         } // , Field1 = "eget turpis"      , Field2 = "non"        , Field3 = "erat"         , Field4 = "hendrerit"  , Field5 = "vulputate." }
        , new Task { Id = 17, Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Nunc"               , Description = "Nunc rhoncus"                       } // , Field1 = "sapien ac"        , Field2 = "erat"       , Field3 = "tempor"       , Field4 = "ac"         , Field5 = "condimentum"  , Field6 = "magna"    , Field7 = "malesuada." }
        , new Task { Id = 18, Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Quisque"            , Description = "Quisque vel risus"                  } // , Field1 = "quis"             , Field2 = "felis"      , Field3 = "vulputate"    , Field4 = "ornare"     , Field5 = "sed"          , Field6 = "nec"      , Field7 = "nulla." }
        , new Task { Id = 19, Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "In non lectus"      , Description = "In non lectus egestas"              } // , Field1 = "bibendum mauris"  , Field2 = "eu"         , Field3 = "ultricies"    , Field4 = "augue." }
        , new Task { Id = 20, Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Aenean"             , Description = "Aenean interdum"                    } // , Field1 = "justo"            , Field2 = "non"        , Field3 = "urna"         , Field4 = "sagittis"   , Field5 = "ac"           , Field6 = "fermentum", Field7 = "velit"      , Field8 = "volutpat." }
        , new Task { Id = 21, Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Quisque"            , Description = "Quisque viverra arcu nec libero"    } // , Field1 = "mattis"           , Field2 = "fringilla." }
        , new Task { Id = 22, Age = 22, Date = DateTime.Now, Price = 2.86M, Active = true, Name = "Nulla"              , Description = "Nulla a sem in lorem posuere"       } // , Field1 = "dictum"           , Field2 = "at"         , Field3 = "sit"          , Field4 = "amet"       , Field5 = "lacus." }
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
