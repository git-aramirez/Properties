using Properties.Domain.DTOs.PropertyImage;
using Properties.Domain.Entities;

namespace Properties.Domain.IRepositories
{
    public interface IPropertyImageRepository
    {
        CreatePropertyImageResquest Create(CreatePropertyImageResquest propertyImage);
        PropertyImageResponse Get(Guid propertyImageId);
        List<PropertyImageResponse> GetAll();
    }
}
