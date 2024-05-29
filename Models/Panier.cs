namespace MiniProjet.Net.Models
{
    public class Panier
    {
        public int PanierId { get; set; }
        public virtual List<ContenuPanier> ContenuPaniers { get; set; }
    }
}
