using Properties.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Domain.DTOs.PropertyImage
{
    public class CreatePropertyImageResquest
    {
        public Guid PropertyId { get; set; }
        public string File { get; set; }
        public bool Enabled { get; set; }
    }
}
