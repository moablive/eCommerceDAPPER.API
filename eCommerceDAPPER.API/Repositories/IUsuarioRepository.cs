using System.Collections.Generic;
using eCommerceDAPPER.API.Models;

namespace eCommerceDAPPER.API.Repositories
{
    public interface IUsuarioRepository
    {
        public List<Usuario> Get();
        public Usuario Get(int id);
        public void Insert(Usuario usuario);
        public void Update(Usuario usuario);
        public void Delete(int id);
    }
}
