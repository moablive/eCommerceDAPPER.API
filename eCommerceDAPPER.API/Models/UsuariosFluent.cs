using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System;

namespace eCommerceDAPPER.API.Models
{
    [Table("Usuarios")]
    public class UsuariosFluent
    {
            [Key] public int CodigoUsuario { get; set; }
            public string NomeCOMPLETOUsuario { get; set; }
            public string CoreioEletronico { get; set; }
            public string Sexo { get; set; }
            public string RG { get; set; }
            public string CPF { get; set; }
            public string NomeCOMPLETOMae { get; set; }
            public string Situacao { get; set; }
            public DateTimeOffset DataDeCadastro { get; set; }

            //RELACIONAMENTO
            [Write(false)] public Contato Contato { get; set; }

            [Write(false)] public ICollection<EnderecoEntrega> EnderecosEntrega { get; set; }

            [Write(false)] public ICollection<Departamento> Departamentos { get; set; }
        }
}
