using ControleEscolar.Entities.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ControleEscolar.Entities.Seguranca
{
    public class Usuario : IdentityUser<int, UsuarioLogin, UsuarioRole, UsuarioClaim>, IEntityBase
    {        
        public bool Inativo { get; set; }

        public Usuario()
        {
        }
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Usuario, int> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }
    }    
}

