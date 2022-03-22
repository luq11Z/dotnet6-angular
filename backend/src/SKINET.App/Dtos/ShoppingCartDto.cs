using System.ComponentModel.DataAnnotations;

namespace SKINET.App.Dtos
{
    public class ShoppingCartDto
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public List<ShoppingCartItemDto> Items { get; set; } = new List<ShoppingCartItemDto>();
    }
}
