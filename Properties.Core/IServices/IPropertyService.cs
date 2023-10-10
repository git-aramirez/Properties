using Properties.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Core.IServices
{
    public interface IPropertyService
    {
        bool Create(Property property);
        Property Get(Guid propertyId);
        List<Property> GetAll();
        List<Property> GetAllIntermediateYears(Guid propertyId, int yearLow, int yearHigh);
        bool Update(Property property);
        bool ChangePrice(Guid propertyId, decimal price);
    }
}
