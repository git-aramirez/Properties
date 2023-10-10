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
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _ownerRepository;
        public OwnerService(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }
        public bool Create(Owner owner)
        {
            return _ownerRepository.Create(owner);
        }

        public Owner Get(Guid ownerId)
        {
            return _ownerRepository.Get(ownerId);
        }

        public List<Owner> GetAll()
        {
            return _ownerRepository.GetAll();
        }
    }
}
