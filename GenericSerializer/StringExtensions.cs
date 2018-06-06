namespace GenericSerializer
{
    static class StringExtensions
    {
        public static string FormatPath(this string name, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return name;
            }

            return $"{path}.{name}";
        }
    }
}
