using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paper.Media;
using Paper.Media.Data;
using Toolset;
using static Sandbox.UsersPaper;

namespace Sandbox
{
  class Draft
  {
    class TBusuario { }
    class TBpost { }

    interface IRecordPaper<TRecord>
    {
      TRecord Get();

      IEnumerable<Link> Link();

      IEnumerable<Link> LinkPage();
    }

    interface IRecordListPaper<TRecord, TKey>
    {
      IEnumerable<TRecord> List();

      TRecord Get(TKey id);

      IEnumerable<Link> Link(TRecord record);

      IEnumerable<Link> LinkPage();
    }

    interface IFormPaper<TRecord> : IForm
    {
      void Prepare();

      void Prepare(TRecord record);

      void Prepare(IEnumerable<TRecord> records);
    }

    // Rotas
    // Acoes
    // Links

    [Expose]
    class UserPaper : IPaper
    {
      [Index]
      [Title("Usuários")]
      [Class(Class.List, Class.Cards)]
      IEnumerable<TBusuario> ListUsuarios() { yield break; }

      TBusuario GetUsuario(int userId) { return null; }

      IEnumerable<Link> LinkUsuarios() { yield break; }
      IEnumerable<Link> LinkUsuarios(IEnumerable<TBusuario> users) { yield break; }
      IEnumerable<Link> LinkUsuario() { yield break; }
      IEnumerable<Link> LinkUsuario(TBusuario user) { yield break; }

      IEnumerable<Link> LinkPageUsuarios() { yield break; }
      IEnumerable<Link> LinkPageUsuarios(IEnumerable<TBusuario> users) { yield break; }
      IEnumerable<Link> LinkPageUsuario() { yield break; }
      IEnumerable<Link> LinkPageUsuario(TBusuario user) { yield break; }

      IEnumerable<TBpost> ListPosts(int userId) { yield break; }
      TBpost GetPost(int userId, int postId) { return null; }
      IEnumerable<Link> LinkPost(int userId, TBpost post) { yield break; }

      object GetSenha(int userId) { return null; }
      IEnumerable<Link> LinkSenha(object user) { yield break; }

      //Format.Card FormatCard(User user) { return null; }
      //Format.Item FormatItem(User user) { return null; }
      //Format.Row FormatRow(User user) { return null; }

      // formularios customizados
      UserForm CreateForm(User user) { return null; }
      UserPasswordForm CreateFormPassword(int userId, User user) { return null; }
      PostForm CreateFormPost(int userId, Post post) { return null; }




      // ESTA FORMA NAO FUNCIONA:
      // - NAO PARECE SER POSSIVEL ASSOCIAR ROTAS USANDO APENAS ARGUMENTOS
      // - O NOME DO METODO DEVE DE ALGUMA FORMA INDICAR A ROTA ?!!!
      //
      // metodos que retornam Ret<Target> sao considerados como acoes
      // o primeiro argumento deve extender IForm contendo os parametros recebidos
      // classes especiais podem ser usadas para coleta dos dados enviados pelo form
      // acoes enumeradas se aplicam a plural e singular
      Target Save(UserPasswordForm form, IEnumerable<User> users) { return null; }
      // acoes com somente o form se aplicam aplicam apenas ao plural
      Target Save(UserPasswordForm form) { return null; }
      // acoes com form e um objeto simples se aplicam apenas ao singular
      Target Save(UserPasswordForm form, User user) { return null; }
      // acoes roteadas devem corresponder em argumentos, com argumentos sempre antes do form
      Target SavePassword(int userId, UserPasswordForm form, User user) { return null; }
      Target SavePosts(int userId, UserPasswordForm form, IEnumerable<Post> posts) { return null; }
      Target SavePost(int userId, int postId, UserPasswordForm form, Post post) { return null; }

      //// acoes da pagina de indice
      //Ret<Target> RemoveAllUsers() { return null; }
      //Ret<Target> Save(IEnumerable<User> users) { return null; }
      //Ret<Target> SavePosts(int userId, IEnumerable<User> users) { return null; }
      //// acoes das paginas de detalhe
      //Ret<Target> Save(int userId, User user) { return null; }
      //Ret<Target> SavePassword(int userId, User user) { return null; }
      //Ret<Target> SavePost(int userId, int postId, Post post) { return null; }
      //// formularios
      //Ret<Target> Update(IEnumerable<User> users, UserForm form) { return null; }
      //Ret<Target> Update(int userId, User user, UserForm form) { return null; }
    }
  }
}