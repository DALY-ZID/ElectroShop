using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MiniProjet.Net.Models
{
    public class CreateViewModel
    {
        [Required]
        public string ProductName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être supérieur à 0.")]
        public decimal Price { get; set; }

        public IFormFile ImageFile { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Le stock doit être supérieur ou égal à 0.")]
        public int Stock { get; set; }
    }
}
