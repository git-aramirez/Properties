using Properties.Domain.DTOs.Property;
using Properties.Domain.Entities;

namespace Properties.Api.IServices
{
    public interface IPropertyService
    {
        PropertyResponse Create(CreatePropertyRequest property);
        PropertyResponse Get(Guid propertyId);
        List<PropertyResponse> GetAll();
        List<PropertyResponse> GetAllIntermediateYears(int yearLow, int yearHigh);
        List<PropertyResponse> GetAllIntermediatePrices(decimal priceLow, decimal priceHigh);
        PropertyResponse Update(UpdatePropertyRequest property);
        bool ChangePrice(Guid propertyId, decimal price);
    }
}
