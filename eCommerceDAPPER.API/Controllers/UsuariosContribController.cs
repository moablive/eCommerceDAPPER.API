using eCommerceDAPPER.API.Models;
using eCommerceDAPPER.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceDAPPER.API.Controllers
{
    [Route("api/contrib/usuarios")]
    [ApiController]
    public class UsuariosContribController : ControllerBase
    {
        private IUsuarioRepository _repository;

        public UsuariosContribController(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// GET obter a lista de usuarios.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.Get()); //HTTP - 200 
        }

        /// <summary>
        /// GET obter o usuario passando o id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <returns></returns>
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
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update([FromBody] Usuario usuario)
        {
            _repository.Update(usuario);
            return Ok(usuario);
        }

        /// <summary>
        /// DELETE remover um usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok("Registro deletado com sucesso.");
        }
    }
}
