using Properties.Domain.DTOs.Owner;

namespace Properties.Domain.IRepositories
{
    public interface IOwnerRepository
    {
        OwnerResponse Create(CreateOwnerRequest owner);
        OwnerResponse Get(Guid ownerId);
        List<OwnerResponse> GetAll();
    }
}
