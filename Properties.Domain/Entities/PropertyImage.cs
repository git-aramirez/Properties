using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Properties.Domain.Entities
{
    public class PropertyImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PropertyImageId { get; set; }
        public Guid PropertyId { get; set; }

        [ForeignKey("PropertyId")]
        public virtual Property? Property { get; set; }
        public string File { get; set; }
        public bool Enabled { get; set; }
    }
}
