using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestalt.Mongo
{
    public abstract class DataAccessBase<T> : IDataAccessBase<T>
   { 
        public DataAccessBase() {
        }

        public virtual string GetCollectionName()
        {
            ///this can eventually be overridden by the 
            return typeof(T).GetType().Name;
        }
    }
}
