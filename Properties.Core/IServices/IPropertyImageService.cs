using Properties.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Core.IServices
{
    public interface IPropertyImageService
    {
        bool Create(PropertyImage propertyImage);
        PropertyImage Get(Guid propertyImageId);
        List<PropertyImage> GetAll();
    }
}
