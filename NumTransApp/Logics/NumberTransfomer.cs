using System;
using System.Collections;

namespace NumTransApp.Logics
{
    public class NumberTransfomer
    {
        private static string[] scale = {
            "",
            " Thousand",
            " Million",
            " Billion",
            " Trillion",
            " Quadrillion",
            " Quintillion",
            " Sextillion",
            " Septillion",
            " Octillion",
            " Nonillion",
            " Decillion",
            " Undecillion",
            " Duodecillion",
            " Tredecillion",
            " Quattuordecillion",
            " Quinquadecillion",
            " Sedecillion",
            " Septendecillion",
            " Octodecillion",
            " Novendecillion",
            " Vigintillion"
        };

        private static string[] UnitDigits = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
        private static string[] TeenDigits = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        private static string[] TensDigits = { null, "", "Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        private static NumberTransfomer instance = null;

        public static string transform(string number)
        {
            if (instance == null)
            {
                instance = new NumberTransfomer();
            }
            return instance.doTransform(number);
        }

        private string doTransform(string number)
        {
            char c100 = '0', c010 = '0', c001 = '0';
            string sCents = "";
            char[] c = number.ToCharArray();

            int i = number.LastIndexOf(".");
            if (i != -1) // deal with cents
            {
                if (c.Length == i + 1) // dot at last case 100.
                {
                    throw new FormatException("Missing decimal digit");
                }
                if (c.Length > i + 2) // two or more decimals
                {
                    c010 = c[i + 1];
                    c001 = c[i + 2];

                    for (int j = i + 3; j < c.Length; j++) // validate all digits, even they are useless
                    {
                        validate(c[j]);
                    }
                }
                else // only one decimal
                {
                    c010 = c[i + 1];
                }
                sCents = convert3Digits(c100, c010, c001);
                if (sCents != "")
                {
                    sCents = sCents == "One" ? "One Cent" : (sCents + " Cents");
                }
                i--;
                if (i == -1) // no integer case .01
                {
                    return sCents == "" ? "Zero Dollar" : sCents;
                }
            }
            if (i == -1) // no decimal point case
            {
                i = c.Length - 1;
            }
            int idxScale = 0;
            Stack s = new Stack();
            do
            {
                c001 = c[i--];
                c010 = i >= 0 ? c[i--] : '0';
                c100 = i >= 0 ? c[i--] : '0';
                string txt = convert3Digits(c100, c010, c001);
                if (txt != "")
                {
                    if (s.Count > 0)
                    {
                        s.Push(" ");
                    }
                    s.Push(txt + scale[idxScale]);
                }
                if (i >= 0 && c[i] == ',')
                {
                    i--;
                }
                idxScale++;
            } while (i >= 0 && idxScale < scale.Length);
            if (i >= 0 && idxScale >= scale.Length) // overflow if chars remain but scale run out
            {
                throw new FormatException("Number too large");
            }
            if (s.Count > 0)
            {
                string dollar = String.Concat(s.ToArray());
                return (dollar == "One" ? "One Dollar" : (dollar + " Dollars")) + (sCents == "" ? "" : (" and " + sCents));
            }
            else // zero integer case 0.01
            {
                return sCents == "" ? "Zero Dollar" : sCents;
            }
        }

        private string convert3Digits(char c100, char c010, char c001)
        {
            validate(c100);
            validate(c010);
            validate(c001);
            string s100 = null, s010 = null, s001 = null;
            if (c100 > '0')
            {
                s100 = convertUnitDigits(c100) + " Hundred";
            }
            if (c010 == '1')
            {
                s010 = convertTeenDigits(c001);
            }
            else
            {
                s010 = convertTensDigits(c010);
                if (c001 > '0')
                {
                    s001 = convertUnitDigits(c001);
                }
            }

            string ret = s100 == null ? "" : (s100 + ((s010 != null || s001 != null)  ? " and " : ""));
            ret += s010 == null ? "" : (s010 + (s001 == null ? "" : " "));
            ret += s001 == null ? "" : s001;
            return ret;
        }

        private string convertUnitDigits(char c)
        {
            return UnitDigits[c - '0'];
        }

        private string convertTeenDigits(char c)
        {
            return TeenDigits[c - '0'];
        }

        private string convertTensDigits(char c)
        {
            return TensDigits[c - '0'];
        }

        private void validate(char c)
        {
            if (c == ',')
            {
                throw new FormatException("Invalid delimiter position");
            }
            if (c < '0' || c > '9')
            {
                throw new FormatException("Not a digit: " + c);
            }
        }
    }
}