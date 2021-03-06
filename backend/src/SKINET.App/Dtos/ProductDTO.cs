namespace SKINET.App.Dtos
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string ProductType { get; set; }
        public string ProductBrand { get; set; }
        public List<PictureDto> Pictures { get; set; }
        public string CreatedAt { get; set; }
    }
}
