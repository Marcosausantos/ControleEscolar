using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity.Entity
{
    public class EntityBase<T> : EntityBase where T : new(), EntityBase 
    { 
        public EntityBase()
        { 

        }
    }

    [SerializableAttribute]
    public class EntityBase : ICustomTypeDescriptor, ICloneable 
    {
        public EntityBase()
        { 
        }
    }
}
