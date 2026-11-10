namespace BaseConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Converter.Convert("1010", 2, 10)); // Output: 10
            Console.WriteLine(Converter.Convert("A", 16, 2)); // Output: 1010
            Console.WriteLine(Converter.Convert("Z", 36, 10)); // Output: 35
        }
    }
}