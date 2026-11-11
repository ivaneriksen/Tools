
namespace BaseConverter.Core
{
    public readonly record struct DataUnit(string Name, decimal Bits, string Group);
    public readonly record struct DataSize(DataUnit Unit, decimal Amount);

    public static class DataSizeConverter
    {
        private static readonly DataUnit[] Units =
        {
            // Bits and bytes
            new DataUnit("bit", 1m, "Bits"),
            new DataUnit("byte", 8m, "Bits"),

            // Network units
            new DataUnit("kbit", 1_000m, "Network"),
            new DataUnit("Mbit", 1_000m * 1_000, "Network"),
            new DataUnit("Gbit", 1_000m * 1_000 * 1_000, "Network"),
            new DataUnit("Tbit", 1_000m * 1_000 * 1_000 * 1_000, "Network"),

            // Decimal units
            new DataUnit("kB", 8m * 1_000, "Decimal"),
            new DataUnit("MB", 8m * 1_000 * 1_000, "Decimal"),
            new DataUnit("GB", 8m * 1_000 * 1_000 * 1_000, "Decimal"),
            new DataUnit("TB", 8m * 1_000 * 1_000 * 1_000 * 1_000, "Decimal"),
            new DataUnit("PB", 8m * 1_000 * 1_000 * 1_000 * 1_000 * 1_000, "Decimal"),
            new DataUnit("EB", 8m * 1_000 * 1_000 * 1_000 * 1_000 * 1_000 * 1_000, "Decimal"),

            // Binary units
            new DataUnit("KiB", 8m * 1024, "Binary"),
            new DataUnit("MiB", 8m * 1024 * 1024, "Binary"),
            new DataUnit("GiB", 8m * 1024 * 1024 * 1024, "Binary"),
            new DataUnit("TiB", 8m * 1024 * 1024 * 1024 * 1024, "Binary"),
            new DataUnit("PiB", 8m * 1024 * 1024 * 1024 * 1024 * 1024, "Binary"),
            new DataUnit("EiB", 8m * 1024 * 1024 * 1024 * 1024 * 1024 * 1024, "Binary")
        };

        public static IReadOnlyList<DataUnit> AllUnits => Units;

        public static DataSize[] ConvertToAll(decimal amount, DataUnit fromUnit)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative.");

            decimal totalBits = amount * fromUnit.Bits;
            DataSize[] results = new DataSize[Units.Length];

            for (int i = 0; i < Units.Length; i++)
            {
                results[i] = new DataSize(Units[i], totalBits / Units[i].Bits);
            }

            return results;
        }
    }
}
