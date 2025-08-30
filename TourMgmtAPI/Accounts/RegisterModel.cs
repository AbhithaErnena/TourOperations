using System.ComponentModel.DataAnnotations;

namespace TourMgmtAPI.Accounts
{
    public class RegisterModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Role { get; set; } // "Admin" or "User"
    }
}
