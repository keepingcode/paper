//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Paper.Media;
//using Paper.Media.Data;
//using Toolset;
//using static Sandbox.UsersPaper;

//namespace Sandbox
//{
//  [PaperAttribute]
//  public class UsersPaper_SmallestSample : IPaper, ISortable, IPaginable, IFilterable<Filter>
//  {
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

//    public IEnumerable<User> Enumerate(Page page, Sort sort, Filter filter)
//    {
//      return UserDb.PaginateBy(page).SortBy(sort);
//    }

//    public IEnumerable<Post> EnumeratePosts(int id, Page page, Sort sort, Filter filter)
//    {
//      return PostDb.Where(x => x.Id == id).PaginateBy(page).SortBy(sort);
//    }

//    public User Detail(int id)
//    {
//      return UserDb.FirstOrDefault(x => x.Id == id);
//    }

//    public User DetailPassword(int id)
//    {
//      return UserDb.FirstOrDefault(x => x.Id == id);
//    }

//    public Post DetailPost(int userId, int postId)
//    {
//      return PostDb.FirstOrDefault(x => x.Id == postId);
//    }



//    public IEnumerable<Link> Link_Enumerate()
//    {
//      yield break;
//    }

//    public IEnumerable<Link> Link_Detail()
//    {
//      yield break;
//    }

//    public IEnumerable<Link> Link()
//    {
//      yield break;
//    }



//  }
//}
