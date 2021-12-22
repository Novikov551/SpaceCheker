using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceChecker.Models
{
    [Table("planets")]
    public class Planet : SpaceObject
    {
        [Column("existence_of_life")]
        [Required]
        public bool ExistenceOfLife { get; set; }
    }
}
