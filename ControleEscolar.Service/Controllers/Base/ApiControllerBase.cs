using ControleEscolar.Data.Repository;
using ControleEscolar.Entities.Entity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ControleEscolar.Service.Controllers.Base
{
    public class ApiControllerBase<T> : ApiController where T : class, IEntityBase
    {
        protected IRepository DataStore { get; set; }

        protected string[] Includes { get; set; }

        public ApiControllerBase()
        {
            //TODO: USE DEPENDENCY INJECTION FOR DECOUPLING
            this.DataStore = new Repository();
        }

        // GET api/<controller>
        [HttpGet]
        public virtual IEnumerable<T> Get()
        {
            IQueryable<T> entidades = DataStore.All<T>(Includes);

            return entidades;
        }

        // GET api/<controller>/5
        [HttpGet]
        public virtual T Get(int id)
        { 
            var entidade = DataStore.Find<T>(t => t.Id == id, Includes);

            return entidade;
        }

        // POST api/<controller>
        [HttpPost]
        public virtual HttpResponseMessage Post([FromBody]T value)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            T entity;

            if (ModelState.IsValid)
            {
                try
                {                    
                    entity = DataStore.Create<T>(value, Includes);

                    response = Request.CreateResponse(HttpStatusCode.Created, entity);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
                }
            }

            return response;
        }

        // PUT api/<controller>
        [HttpPut]
        public virtual HttpResponseMessage Put([FromBody]T value)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            int id;

            if (ModelState.IsValid)
            {
                try
                {
                    id = DataStore.Update<T>(value, Includes);

                    response = Request.CreateResponse(HttpStatusCode.OK, id);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
                }
            }

            return response;
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public virtual HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage response;

            try
            {
                DataStore.Delete<T>(t => t.Id == id);

                response = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }

            return response;
        }

        [HttpDelete]
        public virtual HttpResponseMessage Delete([FromBody]T value)
        {
            HttpResponseMessage response;

            try
            {
                Delete(value.Id);

                response = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }

            return response;
        }

        protected IEnumerable GetModelErrors()
        {
            return this.ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }

        protected int GetUserId() 
        {
            var id = HttpContext.Current.User.Identity.GetUserId();

            return Convert.ToInt32(id);
        }
    }
}