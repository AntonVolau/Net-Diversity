using System;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        public int Parse(string stringValue)
        {
            try
            {

                stringValue = stringValue.Trim();
                int result = 0;
                int multiplier = 1;
                checked
                {
                    if (stringValue[0] == '-')
                    {
                        multiplier = -1;
                    }
                    for (int i = stringValue.Length - 1; i >= 0; i--)
                    {
                        switch (stringValue[i])
                        {
                            case '0':
                                if (multiplier != 1000000000 && multiplier != -1000000000)
                                    multiplier *= 10;
                                break;
                            case '1':
                                result += 1 * multiplier;
                                if (multiplier != 1000000000 && multiplier != -1000000000)
                                    multiplier *= 10;
                                break;
                            case '2':
                                result += 2 * multiplier;
                                if (multiplier != 1000000000 && multiplier != -1000000000)
                                    multiplier *= 10;
                                break;
                            case '3':
                                result += 3 * multiplier;
                                if (multiplier != 1000000000 && multiplier != -1000000000)
                                    multiplier *= 10;
                                break;
                            case '4':
                                result += 4 * multiplier;
                                if (multiplier != 1000000000 && multiplier != -1000000000)
                                    multiplier *= 10;
                                break;
                            case '5':
                                result += 5 * multiplier;
                                if (multiplier != 1000000000 && multiplier != -1000000000)
                                    multiplier *= 10;
                                break;
                            case '6':
                                result += 6 * multiplier;
                                if (multiplier != 1000000000 && multiplier != -1000000000)
                                    multiplier *= 10;
                                break;
                            case '7':
                                result += 7 * multiplier;
                                if (multiplier != 1000000000 && multiplier != -1000000000)
                                    multiplier *= 10;
                                break;
                            case '8':
                                result += 8 * multiplier;
                                if (multiplier != 1000000000 && multiplier != -1000000000)
                                    multiplier *= 10;
                                break;
                            case '9':
                                result += 9 * multiplier;
                                if (multiplier != 1000000000 && multiplier != -1000000000)
                                    multiplier *= 10;
                                break;
                            case '-':
                                if (i == 0)
                                {
                                }
                                else
                                {
                                    throw new FormatException();
                                }
                                break;
                            case '+':
                                if (i == 0)
                                {
                                }
                                else
                                {
                                    throw new FormatException();
                                }
                                break;
                            default:
                                throw new FormatException();
                        }
                    }
                    return result;
                }
            }
            catch (OverflowException)
            {
                throw new OverflowException();
            }
            catch (IndexOutOfRangeException)
            {
                throw new FormatException();
            }
            catch (NullReferenceException)
            {
                throw new ArgumentNullException();
            }
        }
    }
}