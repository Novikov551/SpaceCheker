using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceObjectsApi.Models
{
    public class SpaceObject
    {
        [Required]
        [Column("id")]
        [Key]
        public int Id { get; private set; }

        [Column("name")]
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Column("description")]
        [Required]
        [StringLength(2000, MinimumLength = 0)]
        public string Description { get; set; }
    }
}
