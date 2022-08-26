namespace NPComplet.YourDeals.Client.Web.Admin.Infrastructure.Routes
{
    public static class UserEndpoints
    {
        public static string GetAll = "api/v1/user/GetAll";

        public static string Get(string userId)
        {
            return $"api/v1/user/GetById/{userId}";
        }

        public static string GetUserRoles(string userId)
        {
            return $"api/v1/user/GetRoles/{userId}";
        }

        public static string ExportFiltered(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }
        public static string UpdateRoles = "api/v1/user/UpdateRoles";
        public static string Export = "api/v1/user/export";
        public static string Register = "api/v1/user/Register";
        public static string ToggleUserStatus = "api/v1/user/ToggleUserStatus";
        public static string ForgotPassword = "api/v1/user/ForgetPassword";
        public static string ResetPassword = "api/v1/user/ResetPassword";
    }
}