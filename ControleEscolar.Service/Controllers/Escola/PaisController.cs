using ControleEscolar.Entities.Escola;
using ControleEscolar.Service.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ControleEscolar.Service.Controllers.Escola
{    
    [Authorize]
    [RoutePrefix("api/Pais")]
    public class PaisController : ApiControllerBase<Pais>
    {
        public PaisController()
        {            
        }
    }
}
