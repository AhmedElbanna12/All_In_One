using System.ComponentModel.DataAnnotations;

namespace AllinOne.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Age { get; set; } = null!;

        [Required]
        public string Country { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
