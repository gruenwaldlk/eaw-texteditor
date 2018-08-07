using System;
using System.Text.RegularExpressions;

namespace eaw_texteditor.shared.common.util.search
{
    internal static class SearchUtility
    {
        public static bool VerifyRegEx(string testPattern)
        {
            bool isValid = true;
            if ((testPattern != null) && (testPattern.Trim().Length > 0))
            {
                try
                {
                    Regex.Match("", testPattern);
                }
                catch (ArgumentException)
                {
                    isValid = false;
                }
            }
            else
            {
                isValid = false;
            }
            return (isValid);
        }

        public static bool RegExMatch(string regEx, string patternToMatch, RegexOptions option = RegexOptions.None)
        {
            regEx = "^" + regEx + "$";
            return VerifyRegEx(regEx) && new Regex(regEx, option).Match(patternToMatch).Success;
        }

        public static bool PatternMatch(string pattern, string patterntoMatch, RegexOptions option = RegexOptions.None)
        {
            return RegExMatch(GenerateRegExFromPattern(pattern), patterntoMatch, option);
        }

        public static string GenerateRegExFromPattern(string pattern)
        {
            string regEx = pattern.Replace("?", ".");
            regEx = regEx.Replace("*", ".*");
            return regEx;
        }
    }
}
