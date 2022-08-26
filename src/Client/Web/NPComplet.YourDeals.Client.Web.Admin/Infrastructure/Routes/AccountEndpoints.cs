namespace NPComplet.YourDeals.Client.Web.Admin.Infrastructure.Routes
{
    public static class AccountEndpoints
    {
        public static string Register = "api/v1/Accounts/Register";
        public static string ChangePassword = "api/v1/Accounts/Changepassword";
        public static string UpdateProfile = "api/v1/Accounts/updateprofile";

        public static string GetProfilePicture(string userId)
        {
            return $"api/v1/profile/profile-picture/{userId}";
        }

        public static string UpdateProfilePicture(string userId)
        {
            return $"api/v1/profile/profile-picture/{userId}";
        }
    }
}