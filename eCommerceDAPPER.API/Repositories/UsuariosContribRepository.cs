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

        /// <summary>
        /// GET obter a lista de usuarios.
        /// </summary>
        public List<Usuario> Get()
        {
            return _connection.GetAll<Usuario>().ToList();
        }

        /// <summary>
        /// GET obter o usuario passando o id
        /// </summary>
        /// <param name="id"></param>
        public Usuario Get(int id)
        {
            return _connection.Get<Usuario>(id);
        }

        /// <summary>
        /// POST INSERT Usuario
        /// </summary>
        /// <param name="usuario"></param>
        public void Insert(Usuario usuario)
        {
            usuario.Id = Convert.ToInt32(_connection.Insert(usuario));
        }

        /// <summary>
        /// PUT (Update) Usuario
        /// </summary>
        /// <param name="usuario"></param>
        public void Update(Usuario usuario)
        {
            _connection.Update(usuario);
        }

        /// <summary>
        /// DELETE Usuario Por ID
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            //DELETE CASCADE Definido no banco
            _connection.Delete(Get(id));
        }
    }
}
