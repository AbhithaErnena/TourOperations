using Microsoft.AspNetCore.Identity;

namespace TourMgmtAPI.Accounts
{

    public class AuthUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
