namespace BaseConverter
{
    // Convert to and from any base between 2 and 36 using Horner's method.
    public static class Converter
    {
        private const string Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        // Convert a string representation of a number in the specified base to a string representation in another base.
        public static string Convert(string digits, int fromBase, int toBase)
        {
            long value = ParseFromBase(digits, fromBase);
            return ConvertToBase(value, toBase);
        }
        // Convert a long value to a string representation in the specified base.
        private static string ConvertToBase(long value, int toBase)
        {
            if (value == 0) return "0";
            string result = "";
            while (value > 0)
            {
                result = Digits[(int)(value % toBase)] + result;
                value /= toBase;
            }
            return result;
        }
        // Parse a string representation of a number in the specified base to a long value.
        private static long ParseFromBase(string digits, int fromBase)
        {
            long value = 0;
            for (int i=0; i<digits.Length; i++)
            {
                value = value * fromBase + Digits.IndexOf(digits[i]);
            }
            return value;
        }
    }
}