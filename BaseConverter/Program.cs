namespace BaseConverter.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test the Converter class
            Console.WriteLine("------- Base Converter Test -------");
            Console.WriteLine(Converter.Convert("1010", 2, 10)); // Output: 10
            Console.WriteLine(Converter.Convert("A", 16, 2)); // Output: 1010
            Console.WriteLine(Converter.Convert("Z", 36, 10)); // Output: 35

            // Test the DataSizeConverter class
            Console.WriteLine("------- Data Size Converter Test -------");
            var gb = DataSizeConverter.AllUnits.First(u => u.Name == "GB");
            foreach (var size in DataSizeConverter.ConvertToAll(1, gb))
            {
                Console.WriteLine($"{size.Unit.Name}: {size.Amount}");
            }
            var bit = DataSizeConverter.AllUnits.First(u => u.Name == "bit");
            foreach (var size in DataSizeConverter.ConvertToAll(10_000, bit))
            {
                Console.WriteLine($"{size.Unit.Name}: {size.Amount}");
            }
        }
    }
}