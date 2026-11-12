
using BaseConverter.Core;

namespace BaseConverter.Tests
{
    public class ConverterTests
    {
        [Theory]
        [InlineData("2A", 16, 2, "101010")]         // Test case: Hexadecimal to binary
        [InlineData("1010", 2, 16, "A")]            // Test case: Binary to hexadecimal
        [InlineData("255", 10, 16, "FF")]           // Test case: Decimal to hexadecimal
        [InlineData("Z", 36, 10, "35")]             // Test case: Maximum base
        [InlineData("0", 10, 2, "0")]               // Test case: Zero
        [InlineData("a", 16, 2, "1010")]            // Test case: Lowercase input
        [InlineData(" 2A ", 16, 2, "101010")]       // Test case: Input with whitespace
        public void Convert_ValidInputs_ReturnsCorrectResult(string input, int fromBase, int toBase, string expected)
        {
            // Act
            string result = Converter.Convert(input, fromBase, toBase);
            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("2G", 16, 10)]                  // Test case: Invalid digit for base 16
        [InlineData("9", 8, 10)]                    // Test case: Digit too big for base 8
        [InlineData("", 10, 2)]                     // Test case: Empty string 1
        [InlineData("   ", 10, 2)]                  // Test case: Empty string 2
        [InlineData(null, 10, 2)]                   // Test case: Null string
        [InlineData("-5", 10, 2)]                   // Test case: Negative number
        public void Convert_InvalidInput_ThrowsArgumentException(string? input, int fromBase, int toBase)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => Converter.Convert(input!, fromBase, toBase));
        }

        [Theory]
        [InlineData("0", 1, 10)]                    // Test case: Invalid fromBase (less than 2)
        [InlineData("4", 37, 10)]                   // Test case: Invalid fromBase (greater than 36)
        [InlineData("7", 10, 1)]                    // Test case: Invalid toBase (less than 2)
        [InlineData("A", 10, 37)]                   // Test case: Invalid toBase (greater than 36)
        public void Convert_InvalidBases_ThrowsArgumentOutOfRangeException(string input, int fromBase, int toBase)
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Converter.Convert(input, fromBase, toBase));
        }

        [Fact]
        public void Convert_LargeNumber_ThrowsOverflowException()
        {
            // Arrange
            string largeNumber = new string('F', 20); // A very large hexadecimal number
            // Act & Assert
            Assert.Throws<OverflowException>(() => Converter.Convert(largeNumber, 16, 10));
        }
    }
}
