using BaseConverter.Core;

namespace BaseConverter.Tests
{
    public class DataSizeConverterTests
    {
        [Theory]
        [InlineData(1, "byte", "bit", 8)]           // Test case: Byte to bits
        [InlineData(1, "kB", "byte", 1000)]         // Test case: Decimal kilo (1000)
        [InlineData(1, "KiB", "byte", 1024)]        // Test case: Binary kibi (1024)
        [InlineData(1, "GB", "MB", 1000)]           // Test case: Decimal step
        [InlineData(1, "MiB", "KiB", 1024)]         // Test case: Binary step
        public void ConvertToAll_KnownValues_ReturnsCorrectAmount(
            decimal inputValue, string inputUnitName, string targetUnitName, decimal expected)
        {
            // Arrange
            DataUnit inputUnit = DataSizeConverter.AllUnits.First(u => u.Name == inputUnitName);

            // Act
            DataSize[] results = DataSizeConverter.ConvertToAll(inputValue, inputUnit);
            DataSize target = results.First(r => r.Unit.Name == targetUnitName);

            // Assert
            Assert.Equal(expected, target.Amount);
        }
    }
}
