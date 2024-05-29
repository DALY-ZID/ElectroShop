using System.ComponentModel.DataAnnotations;

namespace MiniProjet.Net.Models
{
    public class ContenuPanier
    {
        public int ContenuPanierId { get; set; }

        public int PanierId { get; set; }
        public virtual Panier Panier { get; set; }

        public int ProduitId { get; set; }
        public virtual Product Produit { get; set; }

        [Required]
        [Display(Name = "Quantité en unitée")]
        public int Quantite { get; set; }
    }
}
