using System;
using System.Text;

namespace CurrencyConversionService
{
    public class MoneyToStringConverter : IConvert
    {
        private readonly string[] _small = new string[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        private readonly string[] _tens = new string[] { "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
        private readonly string[] _scale = new string[] { "", "thousand", "million", "billion" };

        public string Convert(decimal money)
        {
            StringBuilder combined = new StringBuilder();
            var number = (int)Math.Truncate(money);
            var cents = (int)((money - number) * 100);

            if (number == 0)
            {
                var returnSentence = _small[0] + " " + "dollars";
                return CalculateCents(cents, returnSentence);
            }

            int[] groups = new int[4];
            int positive = Math.Abs(number);

            for (int i = 0; i < 4; i++)
            {
                groups[i] = positive % 1000;
                positive /= 1000;
            }

            string[] groupText = new string[4];

            for (int i = 0; i < 4; i++)
                groupText[i] = ThreeDigitGroupToWords(groups[i]);

            combined.Append(groupText[0]);

            for (int i = 1; i < 4; i++)
            {
                if (groups[i] != 0)
                {
                    StringBuilder prefixBuilder = new StringBuilder();
                    prefixBuilder.Append(groupText[i] + " " + _scale[i]);

                    combined.Insert(0, prefixBuilder.ToString() + " ");
                }
            }

            if (money < 0)
                combined.Insert(0, "negative ");

            var sentence = combined.ToString().Trim();

            if (number == 1)
                sentence += " dollar";
            else
                sentence += " dollars";

            sentence = CalculateCents(cents, sentence);

            return sentence;
        }

        private string CalculateCents(int cents, string sentence)
        {
            if (cents > 0)
            {
                sentence += " and " + ThreeDigitGroupToWords(cents) + " cents";
            }

            return sentence;
        }

        private string ThreeDigitGroupToWords(int threeDigits)
        {
            StringBuilder groupText = new StringBuilder();

            int hundreds = threeDigits / 100;
            int tensUnits = threeDigits % 100;

            if (hundreds != 0)
            {
                groupText.Append(_small[hundreds] + " hundred");

                if (tensUnits != 0)
                    groupText.Append(" ");
            }

            int tens = tensUnits / 10;
            int units = tensUnits % 10;

            if (tens >= 2)
            {
                groupText.Append(_tens[tens]);
                if (units != 0)
                    groupText.Append("-" + _small[units]);
            }
            else if (tensUnits != 0)
                groupText.Append(_small[tensUnits]);

            return groupText.ToString();
        }
    }
}