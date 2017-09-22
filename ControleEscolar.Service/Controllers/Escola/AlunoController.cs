using ControleEscolar.Entities.Escola;
using ControleEscolar.Entities.Seguranca;
using ControleEscolar.Service.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ControleEscolar.Service.Controllers.Seguranca
{    
    //[Authorize]
    [RoutePrefix("api/Aluno")]    
    public class AlunoController : ApiControllerBase<Aluno>
    {
        public AlunoController()
        {
            Includes = new[] { "MunicipioAluno", "EstadoAluno", "MunicipioResponsavel", "EstadoResponsavel" };
        }
    }
}
