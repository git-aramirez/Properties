using Properties.Domain.DTOs.PropertyTrace;
using Properties.Domain.Entities;

namespace Properties.Api.IServices
{
    public interface IPropertyTraceService
    {
        CreatePropertyTraceRequest Create(CreatePropertyTraceRequest propertyTrace);
        PropertyTraceResponse Get(Guid propertyTraceId);
        List<PropertyTraceResponse> GetAll();
    }
}
