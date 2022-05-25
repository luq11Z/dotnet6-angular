namespace SKINET.Business.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public int ProductBrandId { get; set; }
        public DateTime CreatedAt { get; set; }

        private readonly List<Picture> _pictures = new List<Picture>();
        public IReadOnlyList<Picture> Pictures => _pictures.AsReadOnly();

        public void AddPicutre(string pictureUrl, string fileName, bool isMain = false)
        {
            var picture = new Picture
            {
                FileName = fileName,
                PictureUrl = pictureUrl
            };

            if (_pictures.Count == 0)
            {
                picture.IsMain = true;
            }

            _pictures.Add(picture);
        }

        public void RemovePicture(int id)
        {
            var picture = _pictures.Find(p => p.Id == id);
            _pictures.Remove(picture);
        }

        public void SetMainPicture(int id)
        {
            var currentMain = _pictures.SingleOrDefault(item => item.IsMain);

            foreach (var item in _pictures.Where(item => item.IsMain))
            {
                item.IsMain = false;
            }

            var picture = _pictures.Find(p => p.Id == id);
            
            if (picture != null)
            {
                picture.IsMain = true;

                if (currentMain != null)
                {
                    currentMain.IsMain = false;
                }
            }
        }
    }
}
