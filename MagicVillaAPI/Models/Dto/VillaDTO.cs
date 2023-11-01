using System.ComponentModel.DataAnnotations;

namespace MagicVillaAPI.Models.Dto
{
    public class VillaDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }        
    }
}
