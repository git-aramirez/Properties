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
    public class PropertyImageService: IPropertyImageService
    {
        private readonly IPropertyImageRepository _propertyImageRepository;
        public PropertyImageService(IPropertyImageRepository propertyImageRepository)
        {
            _propertyImageRepository = propertyImageRepository;
        }

        public bool Create(PropertyImage propertyImage)
        {
            return _propertyImageRepository.Create(propertyImage);
        }

        public PropertyImage Get(Guid propertyImageId)
        {
            return _propertyImageRepository.Get(propertyImageId);
        }

        public List<PropertyImage> GetAll()
        {
            return _propertyImageRepository.GetAll();
        }
    }
}
