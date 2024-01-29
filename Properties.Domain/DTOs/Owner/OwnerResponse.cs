using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Domain.DTOs.Owner
{
    public class OwnerResponse
    {
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        //public IEnumerable<Property>? Properties { get; set; }
    }
}
