using ControleEscolar.Entities.Seguranca;
using ControleEscolar.Service.Controllers.Base;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ControleEscolar.Service.Controllers.Seguranca
{
    [Authorize]
    [RoutePrefix("api/Usuario")]
    public class UsuarioController : ApiControllerBase<Usuario>
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;

        public UsuarioController()
        {
            
        }

        public UsuarioController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        public override HttpResponseMessage Post([FromBody]Usuario usuario)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            Usuario entity;

            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ModelState);
            }

            try
            {
                IdentityResult result;

                if (usuario.Id == 0)
                {
                    result = UserManager.Create(usuario, usuario.PasswordHash);
                }
                else
                {
                    entity = UserManager.FindById(usuario.Id);
                    entity.UserName = usuario.UserName;
                    entity.Email = usuario.Email;
                    entity.LockoutEnabled = usuario.LockoutEnabled;
                    result = UserManager.Update(entity);
                }

                if (!result.Succeeded)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, result.Errors);
                }

                response = Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }

            return response;
        }
    }
}