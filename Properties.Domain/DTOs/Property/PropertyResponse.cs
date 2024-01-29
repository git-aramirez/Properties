using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Domain.DTOs.Property
{
    public  class PropertyResponse
    {
        public Guid PropertyId { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public int CodeInternal { get; set; }
        public int Year { get; set; }
    }
}
