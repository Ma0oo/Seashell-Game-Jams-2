namespace Plugins.HabObject.Customizable
{
    public static class CustomizableExtension
    {
        public static string DeleteBeginCharFromEnd(this string str, char target)
        {
            string result = "";
            for (int i = str.Length-1; i >= 0; i--)
            {
                if (str[i] == target)
                {
                    result = str.Substring(i);
                    break;
                }
            }

            return result;
        }
    }
}