namespace SKINET.App.Dtos
{
    public class OrderDto
    {
        public string shoppingCartId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDto ShipToAddress { get; set; }
    }
}
