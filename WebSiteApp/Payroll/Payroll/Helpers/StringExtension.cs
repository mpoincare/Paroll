using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.Helpers
{
    public static class StringExtension
    {
        public static string ToTitleCase(this string str)
        {
            string result = str.Trim();
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

                // Traiter l'éventuel "trait d'union" '-' utilisé souvent en Français
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