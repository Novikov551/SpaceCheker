using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceObjectsApi.Models
{
    [Table("stars")]
    public class Star : SpaceObject
    {
        [Column("distance_to_earth")]
        [Required]
        [Range(1, 100)]
        public double DistanceToEarth { get; set; }
    }
}
