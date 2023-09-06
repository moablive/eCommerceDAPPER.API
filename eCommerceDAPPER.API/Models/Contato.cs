namespace eCommerceDAPPER.API.Models
{
    public class Contato
    {
        public int ID { get; set; }
        public int UsuarioID { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }

        //RELACIONAMENTO
        public Usuario usuario { get; set; }
    }
}
