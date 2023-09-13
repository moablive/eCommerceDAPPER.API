using System.Collections.Generic;
using System.Data;
using eCommerceDAPPER.API.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace eCommerceDAPPER.API.Repositories
{
    public class UsuariosContribRepository : IUsuarioRepository
    {
        
        private IDbConnection _connection;
        private IConfiguration _configuration;

        public UsuariosContribRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public List<Usuario> Get()
        {
            throw new System.NotImplementedException();
        }

        public Usuario Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(Usuario usuario)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Usuario usuario)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
