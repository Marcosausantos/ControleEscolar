using ControleEscolar.Infraestructure.Db;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(ControleEscolar.Service.Startup))]

namespace ControleEscolar.Service
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {            
            System.Data.Entity.Database.SetInitializer(new ControleEscolarDbInitializer());            

            GlobalConfiguration.Configure(WebApiConfig.Register);
                        
            ConfigureAuth(app);
        }
    }
}
