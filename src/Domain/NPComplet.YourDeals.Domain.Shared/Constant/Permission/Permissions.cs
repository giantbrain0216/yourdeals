using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace NPComplet.YourDeals.Domain.Shared.Constant.Permission
{
    /// <summary>
    /// 
    /// </summary>
    public static class Permissions
    {
        /// <summary>
        /// 
        /// </summary>
        public static class Deals
        {
            /// <summary>
            /// 
            /// </summary>
            public const string View = "Permissions.Deals.View";
            /// <summary>
            /// 
            /// </summary>
            public const string Create = "Permissions.Deals.Create";
            /// <summary>
            /// 
            /// </summary>
            public const string Edit = "Permissions.Deals.Edit";
            /// <summary>
            /// 
            /// </summary>
            public const string Delete = "Permissions.Deals.Delete";
            /// <summary>
            /// 
            /// </summary>
            public const string Export = "Permissions.Deals.Export";
            /// <summary>
            /// 
            /// </summary>
            public const string Search = "Permissions.Deals.Search";
        }

    
       

       /// <summary>
       /// 
       /// </summary>

        public static class Users
        {
            /// <summary>
            /// 
            /// </summary>
            public const string View = "Permissions.Users.View";
            /// <summary>
            /// 
            /// </summary>
            public const string Create = "Permissions.Users.Create";
            /// <summary>
            /// 
            /// </summary>
            public const string Edit = "Permissions.Users.Edit";
            /// <summary>
            /// 
            /// </summary>
            public const string Delete = "Permissions.Users.Delete";
            /// <summary>
            /// 
            /// </summary>
            public const string Export = "Permissions.Users.Export";
            /// <summary>
            /// 
            /// </summary>
            public const string Search = "Permissions.Users.Search";
        }
        /// <summary>
        /// 
        /// </summary>
        public static class Roles
        {
            /// <summary>
            /// 
            /// </summary>
            public const string View = "Permissions.Roles.View";
            /// <summary>
            /// 
            /// </summary>
            public const string Create = "Permissions.Roles.Create";
            /// <summary>
            /// 
            /// </summary>
            public const string Edit = "Permissions.Roles.Edit";
            /// <summary>
            /// 
            /// </summary>
            public const string Delete = "Permissions.Roles.Delete";
            /// <summary>
            /// 
            /// </summary>
            public const string Search = "Permissions.Roles.Search";
        }
        /// <summary>
        /// 
        /// </summary>
        public static class RoleClaims
        {
            /// <summary>
            /// 
            /// </summary>
            public const string View = "Permissions.RoleClaims.View";
            /// <summary>
            /// 
            /// </summary>
            public const string Create = "Permissions.RoleClaims.Create";
            /// <summary>
            /// 
            /// </summary>
            public const string Edit = "Permissions.RoleClaims.Edit";
            /// <summary>
            /// 
            /// </summary>
            public const string Delete = "Permissions.RoleClaims.Delete";
            /// <summary>
            /// 
            /// </summary>
            public const string Search = "Permissions.RoleClaims.Search";
        }
        /// <summary>
        /// 
        /// </summary>
        public static class Communication
        {
            /// <summary>
            /// 
            /// </summary>
            public const string Chat = "Permissions.Communication.Chat";
        }

      /// <summary>
      /// 
      /// </summary>

        public static class Dashboards
        {
            /// <summary>
            /// 
            /// </summary>
            public const string View = "Permissions.Dashboards.View";
        }
        /// <summary>
        /// 
        /// </summary>
        public static class Hangfire
        {
            /// <summary>
            /// 
            /// </summary>
            public const string View = "Permissions.Hangfire.View";
        }
        /// <summary>
        /// 
        /// </summary>
        public static class AuditTrails
        {
            /// <summary>
            /// 
            /// </summary>
            public const string View = "Permissions.AuditTrails.View";
            /// <summary>
            /// 
            /// </summary>
            public const string Export = "Permissions.AuditTrails.Export";
            /// <summary>
            /// 
            /// </summary>
            public const string Search = "Permissions.AuditTrails.Search";
        }
        /// <summary>
        /// Returns a list of Permissions.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetRegisteredPermissions()
        {
            var permssions = new List<string>();
            foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
            {
                var propertyValue = prop.GetValue(null);
                if (propertyValue is not null)
                    permssions.Add(propertyValue.ToString());
            }
            return permssions;
        }
    }
}
