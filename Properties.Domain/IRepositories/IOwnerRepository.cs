using Properties.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Domain.IRepositories
{
    public interface IOwnerRepository
    {
        bool Create(Owner owner);
        Owner Get(Guid ownerId);
        List<Owner> GetAll();
    }
}
