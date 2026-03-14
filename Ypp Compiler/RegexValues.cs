using System.Text.RegularExpressions;

namespace Ypp_Compiler
{
    public static class RegexValues
    {
        public static string keywords = @"\b(int|text|incase|else|repeat|until)\b";
        public static string identifiers = @"\b[a-zA-Z_][a-zA-Z0-9]*\b";
        public static string numbers = @"\b[0-9]+\b";
        public static string operators = @"==|<>|\+|\-|\*|/";
        public static string Assignment = @"\+=|-=|\*=|=";
        public static string symbols = @"[{}()]";
        public static string semicolumn = ";";

        public static string MasterPattern = $"{keywords}|{identifiers}|{numbers}|{operators}|{Assignment}|{symbols}|{semicolumn}";

        public static MatchCollection GetMatch(this string text) {
            return Regex.Matches(text, MasterPattern);
        }

        public static string IsMatch(this string text) {
            return Regex.IsMatch(text, keywords) ? "Keyword" : 
                   Regex.IsMatch(text, identifiers) ? "Identifier" :
                   Regex.IsMatch(text, numbers) ? "Number" :
                   Regex.IsMatch(text, operators) ? "Operator" :
                   Regex.IsMatch(text, Assignment) ? "Assignment" :
                   Regex.IsMatch(text, symbols) ? "Symbol" :
                   Regex.IsMatch(text, semicolumn) ? "Semicolon" : 
                   "Unknown";
        }
    }
}
