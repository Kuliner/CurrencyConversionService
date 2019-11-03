using System;

namespace CurrencyConversionService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class CurrencyConversionService : ICurrencyConversionService
    {
        private decimal _maxValue = 999999999.99M;
        public string ConvertCurrencyToText(decimal value)
        {
            if (Math.Abs(value) > _maxValue)
                return $"Cannot convert this number as it exceeds the maximal allowed value of {_maxValue}";

            var converter = new MoneyToStringConverter();
            return converter.Convert(value);
        }
    }
}
