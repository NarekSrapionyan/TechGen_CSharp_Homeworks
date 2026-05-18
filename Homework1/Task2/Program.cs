using System.Text;

namespace Task2;

class Program
{
    static void Main(string[] args)
    {
        // Step 1 — Demonstrate integer overflow
        int a = int.MaxValue;
        Console.WriteLine(unchecked(a + 1)); // -2147483648

        long b = long.MaxValue;
        Console.WriteLine(unchecked(b + 1)); // -9223372036854775808

        Console.WriteLine();

        // Step 2 — Custom arithmetic for very large numbers
        Console.WriteLine(BigNumber.Add("9999", "1")); // 10000
        Console.WriteLine(BigNumber.Subtract("10000", "1")); // 9999
        Console.WriteLine(BigNumber.Multiply("123", "456")); // 56088

        Console.WriteLine();

        // Bonus — negative numbers
        Console.WriteLine(BigNumber.Add("-6", "-1")); // -7
        Console.WriteLine(BigNumber.Subtract("-6", "-1")); // -5
        Console.WriteLine(BigNumber.Multiply("-6", "-2")); // 12
        Console.WriteLine(BigNumber.Multiply("-6", "2")); // -12

        Console.WriteLine();

        // Invalid input examples
        Console.WriteLine(BigNumber.Add(null, "5")); // 5
        Console.WriteLine(BigNumber.Add("", "5")); // 5
        Console.WriteLine(BigNumber.Add("12a", "5")); // 5
    }

    public static class BigNumber
    {
        public static string Add(string a, string b)
        {
            (bool negativeA, string absA) = Normalize(a);
            (bool negativeB, string absB) = Normalize(b);

            string result;
            bool resultIsNegative;

            if (negativeA == negativeB)
            {
                result = AddAbs(absA, absB);
                resultIsNegative = negativeA;
            }
            else
            {
                int compare = CompareAbs(absA, absB);

                if (compare >= 0)
                {
                    result = SubtractAbs(absA, absB);
                    resultIsNegative = negativeA;
                }
                else
                {
                    result = SubtractAbs(absB, absA);
                    resultIsNegative = negativeB;
                }
            }

            return ApplySign(result, resultIsNegative);
        }

        public static string Subtract(string a, string b)
        {
            return Add(a, Negate(b));
        }

        public static string Multiply(string a, string b)
        {
            (bool negativeA, string absA) = Normalize(a);
            (bool negativeB, string absB) = Normalize(b);

            if (absA == "0" || absB == "0")
            {
                return "0";
            }

            int[] result = new int[absA.Length + absB.Length];

            for (int i = absA.Length - 1; i >= 0; i--)
            {
                int digitA = absA[i] - '0';

                for (int j = absB.Length - 1; j >= 0; j--)
                {
                    int digitB = absB[j] - '0';

                    int positionLow = i + j + 1;
                    int positionHigh = i + j;

                    int multiplication = digitA * digitB + result[positionLow];

                    result[positionLow] = multiplication % 10;
                    result[positionHigh] += multiplication / 10;
                }
            }

            StringBuilder builder = new StringBuilder();

            int index = 0;
            while (index < result.Length - 1 && result[index] == 0)
            {
                index++;
            }

            while (index < result.Length)
            {
                builder.Append((char)('0' + result[index]));
                index++;
            }

            bool resultIsNegative = negativeA != negativeB;

            return ApplySign(builder.ToString(), resultIsNegative);
        }

        private static string AddAbs(string a, string b)
        {
            int i = a.Length - 1;
            int j = b.Length - 1;
            int carry = 0;

            StringBuilder builder = new StringBuilder();

            while (i >= 0 || j >= 0 || carry > 0)
            {
                int digitA = i >= 0 ? a[i] - '0' : 0;
                int digitB = j >= 0 ? b[j] - '0' : 0;

                int sum = digitA + digitB + carry;

                builder.Insert(0, (char)('0' + sum % 10));
                carry = sum / 10;

                i--;
                j--;
            }

            return TrimLeadingZeros(builder.ToString());
        }

        // This method assumes that a >= b.
        private static string SubtractAbs(string a, string b)
        {
            int i = a.Length - 1;
            int j = b.Length - 1;
            int borrow = 0;

            StringBuilder builder = new StringBuilder();

            while (i >= 0)
            {
                int digitA = a[i] - '0' - borrow;
                int digitB = j >= 0 ? b[j] - '0' : 0;

                if (digitA < digitB)
                {
                    digitA += 10;
                    borrow = 1;
                }
                else
                {
                    borrow = 0;
                }

                int difference = digitA - digitB;
                builder.Insert(0, (char)('0' + difference));

                i--;
                j--;
            }

            return TrimLeadingZeros(builder.ToString());
        }

        private static (bool isNegative, string absoluteValue) Normalize(string value)
        {
            if (!IsValidNumber(value))
            {
                return (false, "0");
            }

            bool isNegative = false;
            int startIndex = 0;

            if (value[0] == '-')
            {
                isNegative = true;
                startIndex = 1;
            }

            string absoluteValue = value.Substring(startIndex);
            absoluteValue = TrimLeadingZeros(absoluteValue);

            if (absoluteValue == "0")
            {
                return (false, "0");
            }

            return (isNegative, absoluteValue);
        }

        private static bool IsValidNumber(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            int startIndex = 0;

            if (value[0] == '-')
            {
                if (value.Length == 1)
                {
                    return false;
                }

                startIndex = 1;
            }

            for (int i = startIndex; i < value.Length; i++)
            {
                if (value[i] < '0' || value[i] > '9')
                {
                    return false;
                }
            }

            return true;
        }

        private static string Negate(string value)
        {
            (bool isNegative, string absoluteValue) = Normalize(value);

            if (absoluteValue == "0")
            {
                return "0";
            }

            return isNegative ? absoluteValue : "-" + absoluteValue;
        }

        private static string ApplySign(string number, bool isNegative)
        {
            number = TrimLeadingZeros(number);

            if (number == "0")
            {
                return "0";
            }

            return isNegative ? "-" + number : number;
        }

        private static string TrimLeadingZeros(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "0";
            }

            int index = 0;

            while (index < value.Length - 1 && value[index] == '0')
            {
                index++;
            }

            return value.Substring(index);
        }

        private static int CompareAbs(string a, string b)
        {
            a = TrimLeadingZeros(a);
            b = TrimLeadingZeros(b);

            if (a.Length > b.Length)
            {
                return 1;
            }

            if (a.Length < b.Length)
            {
                return -1;
            }

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] > b[i])
                {
                    return 1;
                }

                if (a[i] < b[i])
                {
                    return -1;
                }
            }

            return 0;
        }
    }
}