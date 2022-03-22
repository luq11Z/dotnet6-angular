using System.ComponentModel.DataAnnotations;

namespace SKINET.App.Dtos
{
    public class AddressDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 3)]
        public string Street { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 3)]
        public string City { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 3)]
        public string State { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 3)]
        public string Zipcode { get; set; }
    }
}
