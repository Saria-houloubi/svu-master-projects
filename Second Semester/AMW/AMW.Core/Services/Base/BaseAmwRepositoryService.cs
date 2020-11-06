using AMW.Data.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AMW.Core.Services.Base
{
    public abstract class BaseAmwRepositoryService
    {
        protected virtual Dictionary<string, object> GetEntityProperties(object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Unable to read properties of null entity");
            }
            return entity.GetType()?
               .GetProperties()?
               .Where(prop =>
               Attribute.IsDefined(prop, typeof(SqlParamAttribute)))
               ?.ToDictionary(prop => prop.Name, prop => prop.GetValue(entity));
        }
    }
}
