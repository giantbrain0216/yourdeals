namespace NPComplet.YourDeals.Domain.Shared.Constant.Localization
{
    /// <summary>
    /// 
    /// </summary>
    public static class LocalizationConstants
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly LanguageCode[] SupportedLanguages = {
            new LanguageCode
            {
                Code = "en-US",
                DisplayName= "English"
            },
            new LanguageCode
            {
                Code = "fr-FR",
                DisplayName = "French"
            },
            new LanguageCode
            {
                Code = "ar",
                DisplayName= "Arabic"
            },
          
        };
    }
}
