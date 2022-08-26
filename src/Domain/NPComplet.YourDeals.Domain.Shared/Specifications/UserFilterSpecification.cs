using NPComplet.YourDeals.Domain.Shared.Specifications.Base;
using NPComplet.YourDeals.Domain.Shared.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPComplet.YourDeals.Domain.Shared.Specifications
{
    /// <summary>
    /// 
    /// </summary>
    public class UserFilterSpecification : NPCompletSpecification<User>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        public UserFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.FirstName.Contains(searchString) || p.LastName.Contains(searchString) || p.Email.Contains(searchString) || p.PhoneNumber.Contains(searchString) || p.UserName.Contains(searchString);
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}
