using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Properties.Domain.Entities
{
    public class PropertyTrace
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PropertyTraceId { get; set; }
        public Guid PropertyId { get; set; }

        [ForeignKey("PropertyId")]
        public virtual Property? Property { get; set; }
        public DateTime DateSale { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public decimal Tax { get; set; }
    }
}
