using Xunit;

namespace CurrencyConversionService.Tests
{
    public class CurrencyConversionServiceTests
    {
        [Theory()]
        [InlineData(5, "five dollars")]
        [InlineData(50, "fifty dollars")]
        [InlineData(55, "fifty-five dollars")]
        [InlineData(500, "five hundred dollars")]
        [InlineData(555, "five hundred fifty-five dollars")]
        [InlineData(5000, "five thousand dollars")]
        [InlineData(5555, "five thousand five hundred fifty-five dollars")]
        [InlineData(55555, "fifty-five thousand five hundred fifty-five dollars")]
        [InlineData(555555, "five hundred fifty-five thousand five hundred fifty-five dollars")]
        [InlineData(0, "zero dollars")]
        [InlineData(1, "one dollar")]
        [InlineData(25.1, "twenty-five dollars and ten cents")]
        [InlineData(0.1, "zero dollars and ten cents")]
        [InlineData(45100, "forty-five thousand one hundred dollars")]
        [InlineData(999999999.99, "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents")]
        [InlineData(-45100, "negative forty-five thousand one hundred dollars")]
        public void ConvertCurrencyToTextConverterTest(decimal value, string expectedText)
        {
            var moneyToStringConverter = new MoneyToStringConverter();
            var convertedText = moneyToStringConverter.Convert(value);

            Assert.Equal(expectedText, convertedText);
        }

        [Fact()]
        public void ConvertCurrencyToTextServiceTest()
        {
            var service = new CurrencyConversionService();
            var convertedText1 = service.ConvertCurrencyToText(-1000000000);
            var convertedText2 = service.ConvertCurrencyToText(1000000000);

            Assert.Equal($"Cannot convert this number as it exceeds the maximal allowed value of {999999999.99M}", convertedText1);
            Assert.Equal($"Cannot convert this number as it exceeds the maximal allowed value of {999999999.99M}", convertedText2);
        }
    }
}