using System.Text;

namespace AMW.Shared.Extensioins.String
{
    public static partial class StringExtensions
    {

        public static string LowerCaseFirstCharacter(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            var strBuilder = new StringBuilder(value);

            //Lower the first character
            strBuilder.Replace(strBuilder[0], char.ToLower(strBuilder[0]), 0, 1);

            return strBuilder.ToString();
        }
    }
}
