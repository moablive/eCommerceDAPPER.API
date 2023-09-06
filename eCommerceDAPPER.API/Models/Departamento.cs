using System.Collections.Generic;

namespace eCommerceDAPPER.API.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nome { get; set;}

        //RELACIONAMENTO
        public ICollection<Usuario> usuarios { get; set; }
    }
}
