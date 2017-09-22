using Microsoft.AspNet.Identity.EntityFramework;

namespace ControleEscolar.Entities.Seguranca
{
    public class Role : IdentityRole<int, UsuarioRole>
    {
        public Role() { }
        public Role(string name) { Name = name; }
    }
}
