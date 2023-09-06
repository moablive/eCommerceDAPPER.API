using eCommerceDAPPER.API.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;
using Dapper;

namespace eCommerceDAPPER.API.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private IDbConnection _connection;

        //Construtor
        public UsuarioRepository()
        {
            _connection = new MySqlConnection("Server=localhost;Port=3306;Database=eCommerceDAPPER;Uid=root;Pwd=1234;");
        }
        /// <summary>
        /// GET TODOS USUARIOS
        /// </summary>
        /// <returns></returns>
        public List<Usuario> Get()
        {
            return _connection.Query<Usuario>("SELECT * FROM Usuarios").ToList();
        }

        /// <summary>
        /// GET USUARIO POR ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Usuario Get(int id)
        {
            return _connection.QuerySingleOrDefault<Usuario>("SELECT * FROM Usuarios WHERE Id = @Id", new {Id = id});
        }

        /// <summary>
        /// INSERT USUARIO
        /// </summary>
        /// <param name="usuario"></param>
        public void Insert(Usuario usuario)
        {
            string sqlINSERT = "INSERT INTO Usuarios(Nome, Email, Sexo, RG, CPF, SituacaoCadastro, DataCadastro) " +
                               "VALUES(@Nome, @Email, @Sexo, @RG, @CPF, @SituacaoCadastro, @DataCadastro); " +
                               "SELECT LAST_INSERT_ID();";

            usuario.Id = _connection.Query<int>(sqlINSERT, usuario).Single();
        }

        /// <summary>
        /// UPDADE USUARIO
        /// </summary>
        /// <param name="usuario"></param>
        public void Update(Usuario usuario)
        {
            string sqlUPDATE = "UPDATE Usuarios SET " +
                               "Nome = @Nome, Email = @Email," +
                               "Sexo = @Sexo, RG = @RG," +
                               "CPF = @CPF, SituacaoCadastro = @SituacaoCadastro," +
                               "DataCadastro = @DataCadastro " +
                               "WHERE Id = @Id";

            _connection.Execute(sqlUPDATE, usuario);
        }

        /// <summary>
        /// DELETE USUARIO POR ID
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            _connection.QuerySingleOrDefault<Usuario>("DELETE FROM Usuarios WHERE Id = @Id", new { Id = id });
        }


    }
}
