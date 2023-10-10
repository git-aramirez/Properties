using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Domain.Models
{
    public class Property
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PropertyId { get; set; }
        public Guid OwnerId { get; set; }
    
        [ForeignKey("OwnerId")]
        public virtual Owner? Owner { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public int CodeInternal { get; set; }
        public int Year { get; set; }
        public IEnumerable<PropertyImage>? PropertyImages { get; set; }
        public IEnumerable<PropertyTrace>? PropertyTraces { get; set; }
    }
}
