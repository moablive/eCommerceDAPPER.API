using eCommerceDAPPER.API.Models;
using eCommerceDAPPER.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceDAPPER.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IUsuarioRepository _repository;

        public UsuariosController(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// GET obter a lista de usuarios.
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.Get()); //HTTP - 200 
        }

        /// <summary>
        /// GET obter o usuario passando o id
        /// </summary>
        /// <param name="id"></param>
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

        /// <summary>
        /// POST Cadastrar um usuario
        /// </summary>
        /// <param name="usuario"></param>
        [HttpPost]
        public IActionResult Insert([FromBody] Usuario usuario)
        {
            _repository.Insert(usuario);
            return Ok(usuario);
        }

        /// <summary>
        /// PUT Atualizar um usuario
        /// </summary>
        /// <param name="usuario"></param>
        [HttpPut]
        public IActionResult Update([FromBody] Usuario usuario)
        {
            _repository.Update(usuario);
            return Ok(usuario);
        }

        /// <summary>
        /// DELETE usuario
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok("Registro deletado com sucesso.");
        }
    }
}
