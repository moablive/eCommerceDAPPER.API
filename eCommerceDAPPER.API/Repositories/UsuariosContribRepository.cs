using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using eCommerceDAPPER.API.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Dapper.Contrib.Extensions;

namespace eCommerceDAPPER.API.Repositories
{
    public class UsuariosContribRepository : IUsuarioRepository
    {
        
        private readonly IDbConnection _connection;
        private readonly IConfiguration _configuration;

        public UsuariosContribRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }


        public List<Usuario> Get()
        {
            return _connection.GetAll<Usuario>().ToList();
        }

        public Usuario Get(int id)
        {
            return _connection.Get<Usuario>(id);
        }

        public void Insert(Usuario usuario)
        {
            usuario.Id = Convert.ToInt32(_connection.Insert(usuario));
        }

        public void Update(Usuario usuario)
        {
            _connection.Update(usuario);
        }

        public void Delete(int id)
        {
            _connection.Delete(Get(id));
        }
    }
}
