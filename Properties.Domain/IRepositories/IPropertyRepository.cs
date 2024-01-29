using Properties.Domain.DTOs.Property;
using Properties.Domain.Entities;

namespace Properties.Domain.IRepositories
{
    public interface IPropertyRepository
    {
        PropertyResponse Create(CreatePropertyRequest property);
        PropertyResponse Get(Guid propertyId);
        List<PropertyResponse> GetAll();
        List<PropertyResponse> GetAllIntermediateYears(Guid propertyId, int yearLow, int yearHigh);
        PropertyResponse Update (UpdatePropertyRequest property);
        bool ChangePrice(Guid propertyId, decimal price);
    }
}
