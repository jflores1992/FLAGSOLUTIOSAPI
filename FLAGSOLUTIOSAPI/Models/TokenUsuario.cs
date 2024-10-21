namespace FLAGSOLUTIOSAPI.Models
{
    public class TokenUsuario
    {

        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime Expira { get; set; }
        public int SucursalId { get; set; }
        public int UsuarioId { get; set; }
        public int EmpresaId { get; set; }
        public int PerfilId { get; set; }
        public string NameEmpleado { get; set; }
        public virtual List<Menu> Menus { get; set; }
    }
}
