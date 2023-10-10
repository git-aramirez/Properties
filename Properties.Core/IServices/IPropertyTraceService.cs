using Properties.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Core.IServices
{
    public interface IPropertyTraceService
    {
        bool Create(PropertyTrace propertyTrace);
        PropertyTrace Get(Guid propertyTraceId);
        List<PropertyTrace> GetAll();
    }
}
