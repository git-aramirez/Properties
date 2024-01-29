using Properties.Api.IServices;
using Properties.Domain.DTOs.PropertyTrace;
using Properties.Domain.Entities;
using Properties.Domain.IRepositories;

namespace Properties.Core.Services
{
    public class PropertyTraceService: IPropertyTraceService
    {
        private readonly IPropertyTraceRepository _propertyTraceRepository;
        public PropertyTraceService(IPropertyTraceRepository propertyTraceRepository)
        {
            _propertyTraceRepository = propertyTraceRepository;
        }

        public CreatePropertyTraceRequest Create(CreatePropertyTraceRequest propertyTrace)
        {
            return _propertyTraceRepository.Create(propertyTrace);
        }

        public PropertyTraceResponse Get(Guid propertyTraceId)
        {
            return _propertyTraceRepository.Get(propertyTraceId);
        }

        public List<PropertyTraceResponse> GetAll()
        {
           return _propertyTraceRepository.GetAll();    
        }
    }
}
