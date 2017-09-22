using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public interface IBusinessEntity
    {
        int Id { get; set; }

        IQueryable<T> All<T>(string[] includes = null) where T : class;
    }
}
