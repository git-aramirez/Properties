using Properties.Api.IServices;
using Properties.Domain.DTOs.PropertyImage;
using Properties.Domain.Entities;
using Properties.Domain.IRepositories;

namespace Properties.Core.Services
{
    public class PropertyImageService: IPropertyImageService
    {
        private readonly IPropertyImageRepository _propertyImageRepository;
        public PropertyImageService(IPropertyImageRepository propertyImageRepository)
        {
            _propertyImageRepository = propertyImageRepository;
        }

        public CreatePropertyImageResquest Create(CreatePropertyImageResquest propertyImage)
        {
            return _propertyImageRepository.Create(propertyImage);
        }

        public PropertyImageResponse Get(Guid propertyImageId)
        {
            return _propertyImageRepository.Get(propertyImageId);
        }

        public List<PropertyImageResponse> GetAll()
        {
            return _propertyImageRepository.GetAll();
        }
    }
}
