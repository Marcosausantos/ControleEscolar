using BusinessEntity.Context;
using BusinessEntity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class BusinessEntity<T> where T : class, IBusinessEntity
    {
        private BusinessEntityDbContext dbContext;

        public BusinessEntity()
        {
            dbContext = new BusinessEntityDbContext();
        }

        public IQueryable<T> All<T>(string[] includes = null) where T : class
        {            
            if (includes != null && includes.Count() > 0)
            {
                var query = dbContext.Set<T>().Include(includes.First());

                foreach (var include in includes.Skip(1))
                    query = query.Include(include);

                return query.AsQueryable();
            }

            IQueryable<T> entidade;

            entidade = dbContext.Set<T>().AsQueryable();

            return entidade;
        }
    }
}
