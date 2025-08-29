using Properties.Domain.DTOs.Owner;

namespace Properties.Api.IServices
{
    public interface IOwnerService
    {
        OwnerResponse Create(CreateOwnerRequest owner);
        OwnerResponse Get(Guid ownerId);
        List<OwnerResponse> GetAll();
    }
}
