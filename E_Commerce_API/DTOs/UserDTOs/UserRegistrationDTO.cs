
namespace E_Commerce_API.DTOs
{
    public class UserRegistrationDTO
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Username { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(10, MinimumLength = 10)]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;
        [Required]
        [StringLength(50, MinimumLength = 8)]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}