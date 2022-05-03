﻿using System.ComponentModel.DataAnnotations;

namespace SKINET.App.Dtos
{
    public class ShoppingCartDto
    {
        [Required]
        public string Id { get; set; }
        public int? DeliveryMethodId { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }
        public decimal ShippingPrice { get; set; }

        [Required]
        public List<ShoppingCartItemDto> Items { get; set; } = new List<ShoppingCartItemDto>();
    }
}
