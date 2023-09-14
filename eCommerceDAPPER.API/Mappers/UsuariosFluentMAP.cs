using Dapper.FluentMap.Mapping;
using eCommerceDAPPER.API.Models;

namespace eCommerceDAPPER.API.Mappers
{
    public class UsuariosFluentMAP : EntityMap<UsuariosFluent>
    {
        public UsuariosFluentMAP()
        {
            Map(m => m.CodigoUsuario).ToColumn("Id"); //FluentMAP <=>
            Map(m => m.NomeCOMPLETOUsuario).ToColumn("Nome"); // FluentMAP <=>
            Map(m => m.CoreioEletronico).ToColumn("Email"); // FluentMAP <=>
            Map(m => m.Sexo).ToColumn("Sexo");
            Map(m => m.RG).ToColumn("RG");
            Map(m => m.CPF).ToColumn("CPF");
            Map(m => m.NomeCOMPLETOMae).ToColumn("NomeMae"); // FluentMAP <=>
            Map(m => m.Situacao).ToColumn("SituacaoCadastro"); // FluentMAP <=>
            Map(m => m.DataDeCadastro).ToColumn("DataCadastro");
        }
    }
}
