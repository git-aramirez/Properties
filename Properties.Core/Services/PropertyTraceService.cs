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
    public class PropertyTraceService: IPropertyTraceService
    {
        private readonly IPropertyTraceRepository _propertyTraceRepository;
        public PropertyTraceService(IPropertyTraceRepository propertyTraceRepository)
        {
            _propertyTraceRepository = propertyTraceRepository;
        }

        public bool Create(PropertyTrace propertyTrace)
        {
            return _propertyTraceRepository.Create(propertyTrace);
        }

        public PropertyTrace Get(Guid propertyTraceId)
        {
            return _propertyTraceRepository.Get(propertyTraceId);
        }

        public List<PropertyTrace> GetAll()
        {
           return _propertyTraceRepository.GetAll();    
        }
    }
}
