using Properties.Domain.DTOs.PropertyImage;
using Properties.Domain.Entities;

namespace Properties.Api.IServices
{
    public interface IPropertyImageService
    {
        CreatePropertyImageResquest Create(CreatePropertyImageResquest propertyImage);
        PropertyImageResponse Get(Guid propertyImageId);
        List<PropertyImageResponse> GetAll();
    }
}
