// Task 1:
// Manual conversion between float and its 32-bit IEEE 754 binary representation.
// BitConverter and Convert.* are not used.

namespace Task1;

class Program
{
    static void Main(string[] args)
    {
        float originalNumber = 12.375f;

        string bits = FloatToBinary(originalNumber);
        Console.WriteLine(bits);

        string prettyBits = FloatToBinary(originalNumber, true);
        Console.WriteLine(prettyBits);

        float restoredNumber = BinaryToFloat(bits);
        Console.WriteLine(restoredNumber.ToString("G9"));

        float restoredFromPretty = BinaryToFloat(prettyBits);
        Console.WriteLine(restoredFromPretty.ToString("G9"));
    }

    static string FloatToBinary(float number, bool pretty = false)
    {
        int signBit = GetSignBit(number);
        int exponentBits;
        int mantissaBits;

        if (float.IsNaN(number))
        {
            exponentBits = 255;
            mantissaBits = 1;
        }
        else if (float.IsPositiveInfinity(number) || float.IsNegativeInfinity(number))
        {
            exponentBits = 255;
            mantissaBits = 0;
        }
        else
        {
            double absoluteNumber = number;

            if (absoluteNumber < 0)
            {
                absoluteNumber = -absoluteNumber;
            }

            if (absoluteNumber == 0)
            {
                exponentBits = 0;
                mantissaBits = 0;
            }
            else
            {
                double minNormal = Math.Pow(2, -126);

                if (absoluteNumber >= minNormal)
                {
                    int exponent = 0;
                    double normalized = absoluteNumber;

                    while (normalized >= 2)
                    {
                        normalized /= 2;
                        exponent++;
                    }

                    while (normalized < 1)
                    {
                        normalized *= 2;
                        exponent--;
                    }

                    exponentBits = exponent + 127;

                    double mantissaReal = (normalized - 1) * Math.Pow(2, 23);
                    mantissaBits = RoundToNearestEven(mantissaReal);

                    if (mantissaBits == 8388608)
                    {
                        mantissaBits = 0;
                        exponentBits++;
                    }

                    if (exponentBits >= 255)
                    {
                        exponentBits = 255;
                        mantissaBits = 0;
                    }
                }
                else
                {
                    exponentBits = 0;

                    double mantissaReal = absoluteNumber / Math.Pow(2, -149);
                    mantissaBits = RoundToNearestEven(mantissaReal);
                }
            }
        }

        string signPart = signBit.ToString();
        string exponentPart = IntToBinaryString(exponentBits, 8);
        string mantissaPart = IntToBinaryString(mantissaBits, 23);

        if (pretty)
        {
            return signPart + " | " + exponentPart + " | " + mantissaPart;
        }

        return signPart + exponentPart + mantissaPart;
    }

    static float BinaryToFloat(string bits)
    {
        string cleanBits = CleanBits(bits);

        if (cleanBits.Length != 32)
        {
            throw new ArgumentException("Input must contain exactly 32 bits.");
        }

        int signBit = cleanBits[0] - '0';
        int exponentBits = ReadBitsToInt(cleanBits, 1, 8);
        int mantissaBits = ReadBitsToInt(cleanBits, 9, 23);

        if (exponentBits == 255)
        {
            if (mantissaBits == 0)
            {
                return signBit == 1 ? float.NegativeInfinity : float.PositiveInfinity;
            }

            return float.NaN;
        }

        double result;

        if (exponentBits == 0)
        {
            result = mantissaBits * Math.Pow(2, -149);
        }
        else
        {
            double mantissa = 1 + mantissaBits / Math.Pow(2, 23);
            int exponent = exponentBits - 127;

            result = mantissa * Math.Pow(2, exponent);
        }

        if (signBit == 1)
        {
            result = -result;
        }

        return (float)result;
    }

    static int GetSignBit(float number)
    {
        if (number < 0)
        {
            return 1;
        }

        if (number == 0 && 1f / number == float.NegativeInfinity)
        {
            return 1;
        }

        return 0;
    }

    static string IntToBinaryString(int number, int length)
    {
        char[] result = new char[length];

        for (int i = length - 1; i >= 0; i--)
        {
            int bit = number % 2;
            result[i] = bit == 0 ? '0' : '1';
            number /= 2;
        }

        return new string(result);
    }

    static int ReadBitsToInt(string bits, int start, int count)
    {
        int result = 0;

        for (int i = start; i < start + count; i++)
        {
            result = result * 2 + (bits[i] - '0');
        }

        return result;
    }

    static string CleanBits(string text)
    {
        if (text == null)
        {
            throw new ArgumentException("Input cannot be null.");
        }

        string result = "";

        for (int i = 0; i < text.Length; i++)
        {
            char symbol = text[i];

            if (symbol == '0' || symbol == '1')
            {
                result += symbol;
            }
            else if (symbol == ' ' || symbol == '|')
            {
                continue;
            }
            else
            {
                throw new ArgumentException("Input can contain only 0, 1, spaces and |.");
            }
        }

        return result;
    }

    static int RoundToNearestEven(double number)
    {
        int integerPart = (int)number;
        double fractionPart = number - integerPart;

        if (fractionPart > 0.5)
        {
            return integerPart + 1;
        }

        if (fractionPart < 0.5)
        {
            return integerPart;
        }

        if (integerPart % 2 == 0)
        {
            return integerPart;
        }

        return integerPart + 1;
    }
}