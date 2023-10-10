using Properties.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Core.IServices
{
    public interface IOwnerService
    {
        bool Create(Owner owner);
        Owner Get(Guid ownerId);
        List<Owner> GetAll();
    }
}
