using System.Collections.Generic;
using Outlook.Calander.Import.Models;

namespace Outlook.Calander.Import.Mappers
{
    public abstract class Mapper<T>
    {
        public IEnumerable<IModel> Map(T model)
        {
            return DoMap(model);
        }

        protected abstract IEnumerable<IModel> DoMap(T model);
    }
}