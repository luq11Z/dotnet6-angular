namespace SKINET.Business.Models
{
    /* This class will not be persisted in the SQL db */
    public class ShoppingCart
    {
        public string Id { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        public ShoppingCart()
        {
        }

        public ShoppingCart(string id)
        {
            Id = id;
        } 
    }
}
