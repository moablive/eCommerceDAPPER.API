using System;
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
            return _connection.Query<Usuario, Contato, Usuario>(
                "SELECT * FROM Usuarios AS u " +
                    "LEFT JOIN Contatos AS c " +
                    "ON c.UsuarioId = u.Id " +
                    "WHERE u.Id = @Id",

                (usuario, contato) =>
                {
                    usuario.Contato = contato;
                    return usuario;
                },
                new { Id = id }).SingleOrDefault();
        }

        /// <summary>
        /// INSERT USUARIO
        /// </summary>
        /// <param name="usuario"></param>
        public void Insert(Usuario usuario)
        {
            _connection.Open(); //ABRE DB
            var transaction = _connection.BeginTransaction(); //transaction
            try
            {
                string sqlUsuario = "INSERT INTO Usuarios(Nome, Email, Sexo, RG, CPF, SituacaoCadastro, DataCadastro) " +
                                    "VALUES(@Nome, @Email, @Sexo, @RG, @CPF, @SituacaoCadastro, @DataCadastro);";
                _connection.Execute(sqlUsuario, usuario, transaction);

                // Recupera o ID gerado para o usuário
                usuario.Id = _connection.Query<int>("SELECT LAST_INSERT_ID();", null, transaction).Single();

                if (usuario.Contato != null)
                {
                    // Recupera o ID gerado para o usuário 
                    usuario.Contato.UsuarioID = usuario.Id; 

                    string sqlContato = "INSERT INTO Contatos (UsuarioID, Telefone, Celular) " +
                                        "VALUES (@UsuarioID, @Telefone, @Celular);";
                    _connection.Execute(sqlContato, usuario.Contato, transaction);

                    // Recupera o ID gerado para o contato
                    usuario.Contato.ID = _connection.Query<int>("SELECT LAST_INSERT_ID();", null, transaction).Single();
                }

                transaction.Commit();
            }
            catch (Exception)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception)
                {
                    // Lida com erro de rollback (opcional)
                }
            }
            finally
            {
                _connection.Close(); //Fecha DB
            }
        }

        /// <summary>
        /// UPDADE USUARIO
        /// </summary>
        /// <param name="usuario"></param>
        public void Update(Usuario usuario)
        {
            _connection.Open(); //ABRE DB
            var transaction = _connection.BeginTransaction(); //transaction

            try
            {
                //Usuario
                string sqlUsuario = "UPDATE Usuarios SET " +
                                    "Nome = @Nome, Email = @Email," +
                                    "Sexo = @Sexo, RG = @RG," +
                                    "CPF = @CPF, SituacaoCadastro = @SituacaoCadastro," +
                                    "DataCadastro = @DataCadastro " +
                                    "WHERE Id = @Id";
                _connection.Execute(sqlUsuario, usuario, transaction);

                //Contato
                if (usuario.Contato != null)
                {
                    string sqlContato = "UPDATE Contatos SET " +
                                        "UsuarioId = @UsuarioId, Telefone = @Telefone, " +
                                        "Celular = @celular WHERE Id = @Id";
                    _connection.Execute(sqlContato, usuario.Contato, transaction);
                }
                
                transaction.Commit();
            }
            catch (Exception)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception)
                {
                    // Lida com erro de rollback (opcional)
                }
            }
            finally
            {
                _connection.Close(); // Fecha DB
            }
        }

        /// <summary>
        /// DELETE USUARIO POR ID
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            //ON DELETE CASCADE
            _connection.QuerySingleOrDefault<Usuario>("DELETE FROM Usuarios WHERE Id = @Id", new { Id = id });
        }
    }
}
