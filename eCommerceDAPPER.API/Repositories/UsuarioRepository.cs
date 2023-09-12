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
            Dictionary<int, Usuario> usuariosDict = new Dictionary<int, Usuario>();
            string sqlCMD = "SELECT U.*, C.*, EE.*, D.* " +
                            "FROM Usuarios AS U " +
                            "LEFT JOIN Contatos AS C ON C.UsuarioId = U.Id " +
                            "LEFT JOIN EnderecosEntrega AS EE ON EE.UsuarioId = U.Id " +
                            "LEFT JOIN UsuariosDepartamentos AS UD ON UD.UsuarioId = U.Id " +
                            "LEFT JOIN Departamentos AS D ON UD.DepartamentoId = D.Id";

            _connection.Query<Usuario, Contato, EnderecoEntrega, Departamento, Usuario>(
                sqlCMD,
                (usuario, contato, enderecoEntrega, departamento) =>
                {
                    if (!usuariosDict.TryGetValue(usuario.Id, out var existingUser))
                    {
                        existingUser = usuario;
                        existingUser.Departamentos = new List<Departamento>();
                        existingUser.EnderecosEntrega = new List<EnderecoEntrega>();
                        existingUser.Contato = contato;
                        usuariosDict.Add(existingUser.Id, existingUser);
                    }

                    // Verifique se enderecoEntrega (ee) não é nulo antes de acessá-lo
                    if (enderecoEntrega != null)
                    {
                        if (!existingUser.EnderecosEntrega.Any(ee => ee.Id == enderecoEntrega.Id))
                        {
                            existingUser.EnderecosEntrega.Add(enderecoEntrega);
                        }
                    }

                    if (!existingUser.Departamentos.Any(d => d.Id == departamento.Id))
                    {
                        existingUser.Departamentos.Add(departamento);
                    }

                    return usuario;
                },
                splitOn: "Id"
            );

            return usuariosDict.Values.ToList();
        }

        /// <summary>
        /// GET USUARIO POR ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Usuario Get(int id)
        {
            Dictionary<int, Usuario> usuariosDict = new Dictionary<int, Usuario>();
            string sqlCMD = "SELECT U.*, C.*, EE.*, D.* " +
                            "FROM Usuarios AS U " +
                            "LEFT JOIN Contatos AS C ON C.UsuarioId = U.Id " +
                            "LEFT JOIN EnderecosEntrega AS EE ON EE.UsuarioId = U.Id " +
                            "LEFT JOIN UsuariosDepartamentos AS UD ON UD.UsuarioId = U.Id " +
                            "LEFT JOIN Departamentos AS D ON UD.DepartamentoId = D.Id " +
                            "WHERE U.Id = @Id";

            _connection.Query<Usuario, Contato, EnderecoEntrega, Departamento, Usuario>(
                sqlCMD,
                (usuario, contato, enderecoEntrega, departamento) =>
                {
                    if (!usuariosDict.TryGetValue(usuario.Id, out var existingUser))
                    {
                        existingUser = usuario;
                        existingUser.Departamentos = new List<Departamento>();
                        existingUser.EnderecosEntrega = new List<EnderecoEntrega>();
                        existingUser.Contato = contato;
                        usuariosDict.Add(existingUser.Id, existingUser);
                    }

                    // Verifique se enderecoEntrega (ee) não é nulo antes de acessá-lo
                    if (enderecoEntrega != null)
                    {
                        if (!existingUser.EnderecosEntrega.Any(ee => ee.Id == enderecoEntrega.Id))
                        {
                            existingUser.EnderecosEntrega.Add(enderecoEntrega);
                        }
                    }

                    if (!existingUser.Departamentos.Any(d => d.Id == departamento.Id))
                    {
                        existingUser.Departamentos.Add(departamento);
                    }

                    return usuario;
                },
               new { Id = id});

            return usuariosDict.Values.SingleOrDefault();
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
                string sqlUsuario = "INSERT INTO Usuarios(Nome, Email, Sexo, RG, CPF, NomeMae, SituacaoCadastro, DataCadastro) " +
                                    "VALUES(@Nome, @Email, @Sexo, @RG, @CPF, @NomeMae, @SituacaoCadastro, @DataCadastro);";
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

                if (usuario.EnderecosEntrega != null && usuario.EnderecosEntrega.Count > 0)
                {
                    foreach (var enderecoEntrega in usuario.EnderecosEntrega)
                    { 
                        // Recupera o ID gerado
                        enderecoEntrega.UsuarioId = usuario.Id;

                        string sqlEndereco = "INSERT INTO EnderecosEntrega " +
                                             "(UsuarioId, NomeEndereco, CEP, Estado, Cidade, Bairro, Endereco, Numero, Complemento) " +
                                             "VALUES " +
                                             "(@UsuarioId, @NomeEndereco, @CEP, @Estado, @Cidade, @Bairro, @Endereco, @Numero, @Complemento);";

                        _connection.Execute(sqlEndereco, enderecoEntrega, transaction);
                    }
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

                string sqlDeletarEnderecosEntrega = "DELETE FROM EnderecosEntrega WHERE UsuarioId = @Id";
                _connection.Execute(sqlDeletarEnderecosEntrega, usuario, transaction);

                if (usuario.EnderecosEntrega != null && usuario.EnderecosEntrega.Count > 0)
                {
                    foreach (var enderecoEntrega in usuario.EnderecosEntrega)
                    {
                        // Recupera o ID gerado
                        enderecoEntrega.UsuarioId = usuario.Id;

                        string sqlEndereco = "INSERT INTO EnderecosEntrega " +
                                             "(UsuarioId, NomeEndereco, CEP, Estado, Cidade, Bairro, Endereco, Numero, Complemento) " +
                                             "VALUES " +
                                             "(@UsuarioId, @NomeEndereco, @CEP, @Estado, @Cidade, @Bairro, @Endereco, @Numero, @Complemento);";

                        _connection.Execute(sqlEndereco, enderecoEntrega, transaction);
                    }
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
            //DELETE CASCADE
            _connection.QuerySingleOrDefault<Usuario>("DELETE FROM Usuarios WHERE Id = @Id", new { Id = id });
        }
    }
}
