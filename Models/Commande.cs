namespace MiniProjet.Net.Models
{
    public class Commande
    {
        public int CommandeId { get; set; }

        public int PanierId { get; set; }

        public string UserName { get; set; }

        public int status { get; set; } = 0;
        public decimal TotalAmount { get; set; } = 0;
        public virtual Panier Panier { get; set; }
    }
}
