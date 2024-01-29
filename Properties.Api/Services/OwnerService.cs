using Properties.Api.IServices;
using Properties.Domain.DTOs.Owner;
using Properties.Domain.IRepositories;

namespace Properties.Core.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _ownerRepository;
        public OwnerService(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }
        public OwnerResponse Create(CreateOwnerRequest owner)
        {
            return _ownerRepository.Create(owner);
        }

        public OwnerResponse Get(Guid ownerId)
        {
            return _ownerRepository.Get(ownerId);
        }

        public List<OwnerResponse> GetAll()
        {
            return _ownerRepository.GetAll();
        }
    }
}
