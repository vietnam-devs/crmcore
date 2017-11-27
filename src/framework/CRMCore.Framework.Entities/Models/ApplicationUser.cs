using Microsoft.AspNetCore.Identity;

namespace CRMCore.Framework.Entities.Models
{
    public class ApplicationUser : IdentityUser, IEntity
    {
        public string LastName
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }
    }
}
