using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;

namespace GenericWebAPI.Helpers
{
    /// <summary>
    /// the class provides all application policies
    /// </summary>
    public static class PolicyStore
    {
        /// <summary>
        /// policy collection
        /// </summary>
        public static class Policies
        {
            public static int AdministrativeAccount = 0;
        }

        /// <summary>
        /// policy builders
        /// </summary>
        public static Dictionary<int, Action<AuthorizationPolicyBuilder>> PolicyBuilders = new Dictionary<int, Action<AuthorizationPolicyBuilder>>
        {
            { Policies.AdministrativeAccount, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(System.Security.Claims.ClaimTypes.Role, "Administrator");
                }
            }
        };
    }
}
