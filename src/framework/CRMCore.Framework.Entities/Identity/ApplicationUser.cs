using System;
using Microsoft.AspNetCore.Identity;

namespace CRMCore.Framework.Entities.Identity
{
    public class ApplicationUser : IdentityUser<Guid>, IEntity
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
