namespace NPComplet.YourDeals.Client.Shared.Extensions
{
    public static class IntegerExtensions
    {
        public static string ToStringMask(this int source, int start, int maskLength)
        {
            return source.ToString().Mask(start, maskLength, 'X');
        }

        public static string ToStringMask(this int source, int start, int maskLength, char maskCharacter)
        {
            return source.ToString().Mask(start, maskLength, maskCharacter);
        }
    }
}