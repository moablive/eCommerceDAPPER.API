using System;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using eCommerceDAPPER.API.Models;
using MySql.Data.MySqlClient;
using Dapper;
using Dapper.FluentMap;
using eCommerceDAPPER.API.Mappers;


namespace eCommerceDAPPER.API.Controllers
{
    [Route("api/tips")]
    [ApiController]
    public class TipsController : ControllerBase
    {
        private readonly IDbConnection _connection;
        private readonly IConfiguration _configuration;

        public TipsController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    var usuario = connection.QueryFirstOrDefault<Usuario>("SELECT * FROM Usuarios WHERE Id = @Id", new { Id = id });
                    var contato = connection.QueryFirstOrDefault<Contato>("SELECT * FROM Contatos WHERE Id = @Id", new { Id = id });
                    var enderecos = connection.Query<EnderecoEntrega>("SELECT * FROM EnderecosEntrega WHERE UsuarioId = @Id", new { Id = id }).ToList();
                    var departamentos = connection.Query<Departamento>("SELECT D.* FROM UsuariosDepartamentos AS UD INNER JOIN Departamentos AS D ON UD.departamentoid = D.Id WHERE UsuarioId = @Id", new { Id = id }).ToList();

                    if (usuario != null)
                    {
                        usuario.Contato = contato;
                        usuario.EnderecosEntrega = enderecos;
                        usuario.Departamentos = departamentos;

                        return Ok(usuario);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }

            return NotFound();
        }

        [HttpGet("stored/usuarios")]
        public IActionResult storeGET()
        {
            //STOREDPROCEDURE => DB
            var usuarios = _connection.Query<Usuario>("SelecionarUsuarios", commandType: CommandType.StoredProcedure);
            return Ok(usuarios);
        }

        //Dapper.FluentMap.
        [HttpGet("fluentmap/usuarios")]
        public IActionResult fluentMap()
        {
            /*
             * Problema: Mapear colunas de diferentes nomes do objeto.
             * Solução: C#(POO) => Mapeamento por meio da LIB Dapper.FluentMap.
             */

            FluentMapper.Initialize(config =>
            {
                config.AddMap(new UsuariosFluentMAP());
            });
            
            var usuarios = _connection.Query<UsuariosFluent>("SELECT * FROM Usuarios;");
            return Ok(usuarios);
        }
    }
}
