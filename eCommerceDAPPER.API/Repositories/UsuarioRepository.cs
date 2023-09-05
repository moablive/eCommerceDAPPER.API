using eCommerceDAPPER.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace eCommerceDAPPER.API.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private static List<Usuario> _db = new List<Usuario>()
        {
            new Usuario() { Id = 1, Nome = "Guilherme", Email = "guilherme@teste.com" },
            new Usuario() { Id = 2, Nome = "Pedro", Email = "pedro@teste.com" },
            new Usuario() { Id = 3, Nome = "Jesica", Email = "jesica@teste.com" }
        };

        public List<Usuario> Get()
        {
            return _db;
        }

        public Usuario Get(int id)
        {
            return _db.FirstOrDefault(i => i.Id == id);
        }

        public void Insert(Usuario usuario)
        {
            var ultimoUsuario = _db.LastOrDefault();

            if (ultimoUsuario == null)
            {
                usuario.Id = 1;
            }
            else
            {
                usuario.Id = ultimoUsuario.Id;
                usuario.Id++;
            }

            _db.Add(usuario);
        }

        public void Update(Usuario usuario)
        {
            _db.Remove(_db.FirstOrDefault(i => i.Id == usuario.Id));
            _db.Add(usuario);
        }

        public void Delete(int id)
        {
            _db.Remove(_db.FirstOrDefault(i => i.Id == id));
        }
    }
}
