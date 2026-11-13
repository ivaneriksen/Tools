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
        public void ConvertToAll_KnownValue_ReturnsCorrectAmount(
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

        [Fact]                                      // Test case: Negative value
        public void ConvertToAll_NegativeValue_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            DataUnit inputUnit = DataSizeConverter.AllUnits.First(u => u.Name == "bit");
            decimal inputValue = -1m;
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => DataSizeConverter.ConvertToAll(inputValue, inputUnit));
        }

        [Fact]                                      // Test case: Huge value causing overflow
        public void ConvertToAll_HugeValue_ThrowsOverflowException()
        {
            // Arrange
            DataUnit inputUnit = DataSizeConverter.AllUnits.First(u => u.Name == "EB");
            decimal inputValue = decimal.MaxValue;
            // Act & Assert
            Assert.Throws<OverflowException>(() => DataSizeConverter.ConvertToAll(inputValue, inputUnit));
        }

        [Fact]                                      // Test case: Conversion returns one result per unit
        public void ConvertToAll_AnyInput_ReturnsOneResultPerUnit()
        {
            // Arrange
            DataUnit inputUnit = DataSizeConverter.AllUnits.First(u => u.Name == "bit");
            decimal inputValue = 1m;
            // Act
            DataSize[] results = DataSizeConverter.ConvertToAll(inputValue, inputUnit);
            // Assert
            Assert.Equal(DataSizeConverter.AllUnits.Count, results.Length);
        }

        [Fact]                                      // Test case: Conversion to self (should return the same amount)
        public void ConvertToAll_ConversionToSelf_ReturnsSameAmount()
        {
            // Arrange
            DataUnit inputUnit = DataSizeConverter.AllUnits.First(u => u.Name == "MB");
            decimal inputValue = 123.45m;
            // Act
            DataSize[] results = DataSizeConverter.ConvertToAll(inputValue, inputUnit);
            DataSize target = results.First(r => r.Unit.Name == inputUnit.Name);
            // Assert
            Assert.Equal(inputValue, target.Amount);
        }

        [Fact]                                      // Test case: Conversion to a unit and back (should return the original amount)
        public void ConvertToAll_ConversionToUnitAndBack_ReturnsOriginalAmount()
        {
            // Arrange
            DataUnit inputUnit = DataSizeConverter.AllUnits.First(u => u.Name == "GB");
            decimal inputValue = 123.45m;
            // Act
            DataSize[] results = DataSizeConverter.ConvertToAll(inputValue, inputUnit);
            DataSize middle = results.First(r => r.Unit.Name == "MiB");
            DataSize[] backResults = DataSizeConverter.ConvertToAll(middle.Amount, middle.Unit);
            DataSize back = backResults.First(r => r.Unit.Name == "GB");
            // Assert
            Assert.Equal(inputValue, back.Amount);
        }
    }
}
