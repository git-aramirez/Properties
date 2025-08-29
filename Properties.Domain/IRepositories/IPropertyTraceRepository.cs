using Properties.Domain.DTOs.PropertyTrace;
using Properties.Domain.Entities;

namespace Properties.Domain.IRepositories
{
    public interface IPropertyTraceRepository
    {
        CreatePropertyTraceRequest Create(CreatePropertyTraceRequest propertyTrace);
        PropertyTraceResponse Get(Guid propertyTraceId);
        List<PropertyTraceResponse> GetAll();
    }
}
