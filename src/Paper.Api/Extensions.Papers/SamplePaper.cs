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
      public string Title { get; set; }
      public string Description { get; set; }
    }

    public class TaskFilter
    {
      public Var<int> Id { get; set; }
      public Var<string> Title { get; set; }
      public Var<string> Description { get; set; }
    }

    public class TaskForm
    {
      public string Title { get; set; }
      public string Description { get; set; }
    }

    public static class DB
    {
      public static List<Task> Tasks = new List<Task> {
        new Task{ Id = 1, Title = "1st", Description = "My 1st task." },
        new Task{ Id = 2, Title = "2nd", Description = "My 2nd task." },
        new Task{ Id = 3, Title = "3rd", Description = "My 3rd task." }
      };
    }

    [Expose]
    public class TasksPaper : IPaper
    {
      public Ret<Task[]> Index(Sort sort, Page page, Filter filter)
      {
        return DB.Tasks.FilterBy(filter).SortBy(sort).PaginateBy(page).ToArray();
      }

      public PaperLink Link()
      {
        return Links.Link("http://www.google.com", opt => opt.SetTitle("Google Search"));
      }

      public IEnumerable<PaperLink> Link(Task task)
      {
        yield return Links.Self<TaskPaper>(task.Id);
      }

      public Ret Edit(TaskForm form, Task[] tasks)
      {
        var currentTasks = DB.Tasks.Where(task => tasks.Any(x => x.Id == task.Id));

        if (form.Title != null)
        {
          currentTasks.ForEach(x => x.Title = form.Title);
        }

        if (form.Description != null)
        {
          currentTasks.ForEach(x => x.Description = form.Description);
        }

        return true;
      }

      public Ret Remove(Task[] tasks)
      {
        DB.Tasks.RemoveAll(task => tasks.Any(x => x.Id == task.Id));
        return true;
      }
    }

    [Expose]
    public class TaskPaper : IPaper
    {
      public Task Index(int taskId)
      {
        return DB.Tasks.FirstOrDefault(x => x.Id == taskId);
      }

      public IEnumerable<PaperLink> Link()
      {
        yield return Links.Link<TasksPaper>(link => link.SetTitle("Tarefas"));
      }

      public IEnumerable<PaperLink> Link(int taskId)
      {
        var prev = DB.Tasks.Where(x => x.Id < taskId).Select(x => x.Id).DefaultIfEmpty().Max();
        var next = DB.Tasks.Where(x => x.Id > taskId).Select(x => x.Id).DefaultIfEmpty().Min();
        if (prev > 0)
        {
          yield return Links.Link<TaskPaper>(prev, opt => opt.SetTitle("Anterior"));
        }
        if (next > 0)
        {
          yield return Links.Link<TaskPaper>(next, opt => opt.SetTitle("Próximo"));
        }
      }

      public IEnumerable<PaperLink> Link(Task task)
      {
        yield return Links.Link($"http://www.google.com?q={task.Title}", opt => opt.SetTitle("Google Search"));
      }

      public void Save(int taskId, TaskForm form)
      {
        var task = DB.Tasks.FirstOrDefault(x => x.Id == taskId);

        if (form.Title != null)
        {
          task.Title = form.Title;
        }

        if (form.Description != null)
        {
          task.Description = form.Description;
        }
      }

      public Ret Remove(int taskId)
      {
        DB.Tasks.RemoveAll(x => x.Id == taskId);
        return Redirect.To<TasksPaper>();
      }
    }
  }
}
