using eCommerceDAPPER.API.Models;
using eCommerceDAPPER.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceDAPPER.API.Controllers
{
    /*
     * CRUD
     * - GET => obter a lista de usuarios.
     * - GET => obter o usuario passando o id
     * - POST => Cadastrar um usuario
     * - PUT Atualizar um usuario
     * - DELETE remover um usuario
     *
     * www.minhaapi.com/api/Usuarios
     * www.minhaapi.com/api/Usuarios/1
     */

    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IUsuarioRepository _repository;
        public UsuariosController()
        {
            _repository = new UsuarioRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.Get()); //HTTP - 200 
        }

        [HttpGet("{id}")]
        public IActionResult GetID(int id)
        {
            var usuario = _repository.Get(id);
            if (usuario == null)
            {
                return NotFound(); //ERROR HTTP: 404 - Not Found
            }
            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult Insert([FromBody] Usuario usuario)
        {
            _repository.Insert(usuario);
            return Ok(usuario);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Usuario usuario)
        {
            _repository.Update(usuario);
            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok("Registro deletado com sucesso.");
        }
    }
}
