using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceObjectsApi.Models
{
    [Table("black_holes")]
    public class BlackHole : SpaceObject
    {
        [Column("weight")]
        [Required]
        [Range(1, 100)]
        public double Weight { get; set; }
    }
}
