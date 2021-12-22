using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceChecker.Models
{
    [Table("asteroids")]
    public class Asteroid : SpaceObject
    {
        [Column("diameter")]
        [Required]
        [Range(1, 100)]
        public double Diameter { get; set; }
    }
}
