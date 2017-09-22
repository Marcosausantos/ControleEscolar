using ControleEscolar.Entities.Escola;
using ControleEscolar.Service.Controllers.Base;
using ControleEscolar.Service.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ControleEscolar.Service.Controllers.Seguranca
{
    [Authorize]
    [RoutePrefix("api/SensoMec")]    
    public class SensoMecController : ApiControllerBase<SensoMec>
    {
        public SensoMecController()
        {
            Includes = new[] { "SensoMecItens", "SensoMecItens.Aluno" };
        }

        [Route("getAlunos")]
        [HttpGet]
        public SensoMecItem[] getAlunos()
        {
            IQueryable<Aluno> _aluno = DataStore.All<Aluno>();

            Aluno[] _alunos = _aluno.Select(x => x).ToArray<Aluno>();

            IList<SensoMecItem> _sensoMecItens = new List<SensoMecItem>();

            foreach (var item in _alunos)
            {
                _sensoMecItens.Add(new SensoMecItem() { Aluno = item });
            }

            return _sensoMecItens.ToArray();
        }

        [Route("PostEnviarAlunos")]
        [HttpPost]
        public HttpResponseMessage PostEnviarAlunos(List<SensoMecViewModals> values)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                var json = JsonConvert.SerializeObject(values, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented });

                var body = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("alunos", json)
                };

                var client = new HttpClient();
                var content = new FormUrlEncodedContent(body);
                HttpResponseMessage res = client.PostAsync("http://localhost:3000/api/SensosMec/EnviarAlunos", content).Result;
                var responseJson = res.Content.ReadAsStringAsync();
                var jObject = JObject.Parse(responseJson.Result);
                var protocolo = jObject.GetValue("protocolo").ToString();

                if (res.StatusCode == HttpStatusCode.OK)
                    response = Request.CreateResponse(HttpStatusCode.Created, protocolo);
                else
                    response = Request.CreateResponse(HttpStatusCode.NotFound, "Erro ao chamar webapi SensoMec.");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }

            return response;
        }

        [HttpPost]
        public override HttpResponseMessage Post([FromBody]SensoMec value)
        {
            ControleEscolar.Infraestructure.Context.ControleEscolarDbContext ctx = new ControleEscolar.Infraestructure.Context.ControleEscolarDbContext();

            if (value.Id == default(int))
                ctx.Entry(value).State = EntityState.Added;
            else
                ctx.Entry(value).State = EntityState.Modified;

            foreach (var item in value.SensoMecItens)
            {
                if (item.Aluno.Id == default(int))
                    ctx.Entry(item.Aluno).State = EntityState.Added;
                else
                    ctx.Entry(item.Aluno).State = EntityState.Modified;
            }

            if (value.Id == default(int))
                ctx.SensoMecs.Add(value);
            else
                ctx.SensoMecs.Attach(value);

            ctx.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, value);
        }

        public override HttpResponseMessage Delete(int id)
        {
            ControleEscolar.Infraestructure.Context.ControleEscolarDbContext ctx = new ControleEscolar.Infraestructure.Context.ControleEscolarDbContext();

            var obj = ctx.SensoMecs.Find(id);

            if (obj != null)
            {
                foreach (var item in ctx.SensoMecItens.Where(x => x.SensoMec.Id == id))
                {
                    ctx.SensoMecItens.Remove(item);
                }

                ctx.SensoMecs.Remove(obj);
                ctx.SaveChanges();
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
