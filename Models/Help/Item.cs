namespace MiniProjet.Net.Models.Help
{
    public class Item
    {

            public int quantite { get; set; }
            private int _ProduitId;
            public Product _produit = null;

            public Product Prod
            {
                get { return _produit; }
                set { _produit = value; }
            }
            public string Description
            {
                get { return _produit.ProductName; }
            }
            public decimal UnitPrice
            {
                get { return _produit.Price; }
            }
            public decimal TotalPrice
            {
                get { return _produit.Price * quantite; }
            }
            public Item(Product p)
            {
                this.Prod = p;
            }
            public bool Equals(Item item)
            {
                return item.Prod.ProductId == this.Prod.ProductId;
            }
        }
}
