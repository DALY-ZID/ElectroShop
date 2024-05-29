using System;
using System.Collections.Generic;

namespace MiniProjet.Net.Models.Help
{
    public class ListeCart
    {
        // Add the PanierId property
        public int PanierId { get; set; }

        public List<Item> Items { get; private set; }

        // Singleton instance
        private static readonly ListeCart _instance = new ListeCart();

        // Public property to access the singleton instance
        public static ListeCart Instance => _instance;

        // Private constructor to prevent external instantiation
        private ListeCart()
        {
            Items = new List<Item>();
        }

        public void AddItem(Product prod)
        {
            // Check if the product is already in the cart
            Item existingItem = Items.Find(item => item.Prod.ProductId == prod.ProductId);

            if (existingItem != null)
            {
                existingItem.quantite++; // Increment quantity if item already exists
            }
            else
            {
                Item newItem = new Item(prod);
                newItem.quantite = 1;
                Items.Add(newItem);
            }
        }

        public void SetLessOneItem(Product produit)
        {
            Item itemToRemove = Items.Find(item => item.Prod.ProductId == produit.ProductId);

            if (itemToRemove != null)
            {
                if (itemToRemove.quantite <= 0)
                {
                    RemoveItem(produit);
                }
                else
                {
                    itemToRemove.quantite--;
                }
            }
        }

        public void SetItemQuantity(Product produit, int quantity)
        {
            Item itemToUpdate = Items.Find(item => item.Prod.ProductId == produit.ProductId);

            if (quantity <= 0)
            {
                RemoveItem(produit);
            }
            else if (itemToUpdate != null)
            {
                itemToUpdate.quantite = quantity;
            }
        }

        public void RemoveItem(Product produit)
        {
            Item itemToRemove = Items.Find(item => item.Prod.ProductId == produit.ProductId);

            if (itemToRemove != null)
            {
                Items.Remove(itemToRemove);
            }
        }

        public decimal GetSubTotal()
        {
            decimal subTotal = 0;
            foreach (Item i in Items)
            {
                subTotal += i.TotalPrice;
            }
            return subTotal;
        }
    }
}
