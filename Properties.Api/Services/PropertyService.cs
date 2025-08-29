using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Properties.Api.IServices;
using Properties.Domain.DTOs.Property;
using Properties.Domain.Entities;
using Properties.Domain.IRepositories;

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

        public PropertyResponse Create(CreatePropertyRequest property)
        {
            return _propertyRepository.Create(property);
        }

        public PropertyResponse Get(Guid propertyId)
        {
            return _propertyRepository.Get(propertyId); 
        }

        public List<PropertyResponse> GetAll()
        {
            return _propertyRepository.GetAll();
        }

        public List<PropertyResponse> GetAllIntermediatePrices(decimal priceLow, decimal priceHigh)
        {
            return _propertyRepository.GetAllIntermediatePrices(priceLow, priceHigh);
        }

        public List<PropertyResponse> GetAllIntermediateYears(int yearLow, int yearHigh)
        {
            return _propertyRepository.GetAllIntermediateYears(yearLow, yearHigh);
        }

        public PropertyResponse Update(UpdatePropertyRequest property)
        {
            return _propertyRepository.Update(property);
        }
    }
}
