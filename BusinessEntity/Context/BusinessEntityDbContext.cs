using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity.Context
{
    public class BusinessEntityDbContext : DbContext
    {
        public static BusinessEntityDbContext Create()
        {
            return new BusinessEntityDbContext();
        }

        public BusinessEntityDbContext()
            : base("Name=DefaultConnection")
        {                        
            Configuration.LazyLoadingEnabled = false;
            this.Database.Log = x => Debug.Write(x);
        }
    }
}
