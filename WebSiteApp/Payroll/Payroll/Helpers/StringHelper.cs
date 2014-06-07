using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.Helpers
{
    public static class StringHelper
    {
        public static string ToTitleCase(string str)
        {
            string result = str;
            if (!string.IsNullOrEmpty(str))
            {
                var words = str.Split(' ');
                for (int index = 0; index < words.Length; index++)
                {
                    var s = words[index];
                    if (s.Length > 0)
                    {
                        words[index] = s[0].ToString().ToUpper() + s.Substring(1).ToLower();
                    }
                }
                result = string.Join(" ", words);

                // Processing for the "trait d'union" '-' used often in French
                words = result.Split('-');
                for (int index = 0; index < words.Length; index++)
                {
                    var s = words[index];
                    if (s.Length > 0)
                    {
                        words[index] = s[0].ToString().ToUpper() + s.Substring(1);
                    }
                }
                result = string.Join("-", words);

            }
            return result;
        }
    }
}