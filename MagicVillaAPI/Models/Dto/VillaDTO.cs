using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace MagicVillaAPI.Models.Dto
{
    public class VillaDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        public int Occupancy { get; set; }
        public int Sqft { get; set; }
    }
}
