using Properties.Core.IServices;
using Properties.Domain.IRepositories;
using Properties.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Core.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        public PropertyService(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public bool ChangePrice(Guid propertyId, decimal price)
        {
            return _propertyRepository.ChangePrice(propertyId, price);
        }

        public bool Create(Property property)
        {
            return _propertyRepository.Create(property);
        }

        public Property Get(Guid propertyId)
        {
            return _propertyRepository.Get(propertyId); 
        }

        public List<Property> GetAll()
        {
            return _propertyRepository.GetAll();
        }

        public List<Property> GetAllIntermediateYears(Guid propertyId, int yearLow, int yearHigh)
        {
            return _propertyRepository.GetAllIntermediateYears(propertyId, yearLow, yearHigh);
        }

        public bool Update(Property property)
        {
            return _propertyRepository.Update(property);
        }
    }
}
