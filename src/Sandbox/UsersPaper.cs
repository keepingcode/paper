//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Paper.Media;
//using Paper.Media.Data;
//using Toolset;

//namespace Sandbox
//{
//  public interface ISortable { Sort CreateSort(); }
//  public interface IPaginable { }
//  public interface IFilterable<T> { T CreateFilter(); }
//  public interface IForm { }
//  public class Target { }

//  public class PaperAttribute : Attribute { }
//  public class IndexAttribute : Attribute { }
//  public class TitleAttribute : Attribute { public TitleAttribute(string title) { } }
//  public class ClassAttribute : Attribute { public ClassAttribute(params Class[] classes) { } }
//  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
//  public class PluralAttribute : Attribute { public PluralAttribute(string singular, string plural) { } }

//  [Paper]
//  [Plural("Password", "Passwords")]
//  //[Route("/Users")]
//  public class UsersPaper : IPaper, ISortable, IPaginable, IFilterable<UsersPaper.Filter>
//  {
//    //
//    // Escolha da página de índice:
//    // -  1º - Enumerate()
//    // -  2º - Detail()
//    //
//    // Se o método for custommizado deve-se usar o atributo [Index]:
//    // -  [Index] EnumerateTalz()
//    // -  [Index] DetailTalz()
//    //
//    // Ou alternativamente definir uma propriedade ou método "Index":
//    // -  public string Index => "EnumerateUsers";
//    // -  public string Index() { return "Users"; }
//    //

//    [Plural("User", "Users")]
//    public class User
//    {
//      public int Id { get; set; }
//      public string Name { get; set; }
//      public string Password { get; set; }

//      public override string ToString()
//      {
//        return $"{{{nameof(Id)}={Id},{nameof(Name)}={Name},{nameof(Password)}=({(Password != null ? "Yes" : "No")})}}";
//      }
//    }

//    public class Post
//    {
//      public int Id { get; set; }
//      public int UserId { get; set; }
//      public string Body { get; set; }
//    }

//    public class UserForm { }
//    public class UserPasswordForm { }
//    public class PostForm { }

//    public class Filter
//    {
//      public Var<string> Name { get; set; }
//    }

//    public Sort CreateSort()
//    {
//      return new Sort()
//        .AddField<User>(user => user.Id)
//        .AddField<User>(user => user.Name);
//    }

//    public Filter CreateFilter()
//    {
//      return new Filter();
//    }

//    // /Users
//    [Index]
//    public IEnumerable<User> EnumerateUsers(Page page, Sort sort, Filter filter)
//    {
//      return UserDb.PaginateBy(page).SortBy(sort);
//    }

//    // /Users/{id}/Posts
//    [Plural("Post", "Posts")]
//    public IEnumerable<Post> EnumeratePosts(int id, Page page, Sort sort, Filter filter)
//    {
//      return PostDb.Where(x => x.Id == id).PaginateBy(page).SortBy(sort);
//    }

//    // /Users/{id}
//    public User DetailUser(int id)
//    {
//      return UserDb.FirstOrDefault(x => x.Id == id);
//    }

//    // /Users/{id}/Password
//    public User DetailPassword(int id)
//    {
//      return UserDb.FirstOrDefault(x => x.Id == id);
//    }

//    // /Users/{userId}/Posts/{id}
//    public Post DetailPost(int userId, int postId)
//    {
//      return PostDb.FirstOrDefault(x => x.Id == postId);
//    }



//    //
//    // COMO LINCAR AS PAGINAS ?
//    //
//    public IEnumerable<Link> LinkUser(User user)
//    {
//      yield break;
//    }
//    public IEnumerable<Link> LinkPassword(User user)
//    {
//      yield break;
//    }
//    public IEnumerable<Link> LinkPost(Post post)
//    {
//      yield break;
//    }




//    ////
//    //// COMO DESENHAR ACTIONS ?
//    ////
//    //public Ret<Target> SaveUser(int id)
//    //{
//    //}



//    public static IEnumerable<User> UserDb
//    {
//      get
//      {
//        yield return new User { Id = 1, Name = "Fulano", Password = "123" };
//        yield return new User { Id = 2, Name = "Beltrano", Password = "123" };
//        yield return new User { Id = 3, Name = "Cicrano", Password = "123" };
//      }
//    }

//    public static IEnumerable<Post> PostDb
//    {
//      get
//      {
//        yield return new Post { Id = 1, UserId = 1, Body = "Some text..." };
//        yield return new Post { Id = 2, UserId = 3, Body = "Some text..." };
//        yield return new Post { Id = 3, UserId = 1, Body = "Some text..." };
//        yield return new Post { Id = 4, UserId = 2, Body = "Some text..." };
//        yield return new Post { Id = 5, UserId = 1, Body = "Some text..." };
//        yield return new Post { Id = 6, UserId = 2, Body = "Some text..." };
//        yield return new Post { Id = 7, UserId = 2, Body = "Some text..." };
//        yield return new Post { Id = 8, UserId = 3, Body = "Some text..." };
//        yield return new Post { Id = 9, UserId = 3, Body = "Some text..." };
//      }
//    }
//  }
//}
