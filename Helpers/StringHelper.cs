using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BankSystem.Helpers
{
    public class StringHelper
    {
        private string _value = string.Empty;

        public string Value
        {
            get => _value;
            set => _value = value;
        }

        public static int Length(string s1)
        {
            return s1.Length;
        }

        public int Length()
        {
            return _value.Length;
        }

        public static int CountWords(string s1)
        {
            if (string.IsNullOrWhiteSpace(s1))
                return 0;

            string[] words = s1.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }

        public int CountWords()
        {
            return CountWords(_value);
        }

        public static string UpperFirstLetterOfEachWord(string s1)
        {
            if (string.IsNullOrEmpty(s1))
                return s1;

            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(s1.ToLower());
        }

        public void UpperFirstLetterOfEachWord()
        {
            _value = UpperFirstLetterOfEachWord(_value);
        }

        public static string LowerFirstLetterOfEachWord(string s1)
        {
            if (string.IsNullOrEmpty(s1))
                return s1;

            StringBuilder result = new StringBuilder();
            bool newWord = true;

            foreach (char c in s1)
            {
                if (newWord && char.IsLetter(c))
                {
                    result.Append(char.ToLower(c));
                    newWord = false;
                }
                else
                {
                    result.Append(c);
                    newWord = c == ' ';
                }
            }

            return result.ToString();
        }

        public void LowerFirstLetterOfEachWord()
        {
            _value = LowerFirstLetterOfEachWord(_value);
        }

        public static string UpperAllString(string s1)
        {
            return s1?.ToUpper() ?? string.Empty;
        }

        public void UpperAllString()
        {
            _value = UpperAllString(_value);
        }

        public static string LowerAllString(string s1)
        {
            return s1?.ToLower() ?? string.Empty;
        }

        public void LowerAllString()
        {
            _value = LowerAllString(_value);
        }

        public static char InvertLetterCase(char char1)
        {
            return char.IsUpper(char1) ? char.ToLower(char1) : char.ToUpper(char1);
        }

        public static string InvertAllLettersCase(string s1)
        {
            if (string.IsNullOrEmpty(s1))
                return s1;

            char[] chars = s1.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = InvertLetterCase(chars[i]);
            }
            return new string(chars);
        }

        public void InvertAllLettersCase()
        {
            _value = InvertAllLettersCase(_value);
        }

        public enum WhatToCount { SmallLetters = 0, CapitalLetters = 1, All = 3 }

        public static int CountLetters(string s1, WhatToCount whatToCount = WhatToCount.All)
        {
            if (string.IsNullOrEmpty(s1))
                return 0;

            if (whatToCount == WhatToCount.All)
                return s1.Length;

            int counter = 0;
            foreach (char c in s1)
            {
                if (whatToCount == WhatToCount.CapitalLetters && char.IsUpper(c))
                    counter++;
                else if (whatToCount == WhatToCount.SmallLetters && char.IsLower(c))
                    counter++;
            }
            return counter;
        }

        public static int CountCapitalLetters(string s1)
        {
            return CountLetters(s1, WhatToCount.CapitalLetters);
        }

        public int CountCapitalLetters()
        {
            return CountCapitalLetters(_value);
        }

        public static int CountSmallLetters(string s1)
        {
            return CountLetters(s1, WhatToCount.SmallLetters);
        }

        public int CountSmallLetters()
        {
            return CountSmallLetters(_value);
        }

        public static int CountSpecificLetter(string s1, char letter, bool matchCase = true)
        {
            if (string.IsNullOrEmpty(s1))
                return 0;

            int counter = 0;
            foreach (char c in s1)
            {
                if (matchCase)
                {
                    if (c == letter) counter++;
                }
                else
                {
                    if (char.ToLower(c) == char.ToLower(letter)) counter++;
                }
            }
            return counter;
        }

        public int CountSpecificLetter(char letter, bool matchCase = true)
        {
            return CountSpecificLetter(_value, letter, matchCase);
        }

        public static bool IsVowel(char ch1)
        {
            ch1 = char.ToLower(ch1);
            return ch1 == 'a' || ch1 == 'e' || ch1 == 'i' || ch1 == 'o' || ch1 == 'u';
        }

        public static int CountVowels(string s1)
        {
            if (string.IsNullOrEmpty(s1))
                return 0;

            int counter = 0;
            foreach (char c in s1)
            {
                if (IsVowel(c)) counter++;
            }
            return counter;
        }

        public int CountVowels()
        {
            return CountVowels(_value);
        }

        public static List<string> Split(string s1, string delim)
        {
            if (string.IsNullOrEmpty(s1))
                return new List<string>();

            return s1.Split(new[] { delim }, StringSplitOptions.None).ToList();
        }

        public List<string> Split(string delim)
        {
            return Split(_value, delim);
        }

        public static string TrimLeft(string s1)
        {
            return s1?.TrimStart() ?? string.Empty;
        }

        public void TrimLeft()
        {
            _value = TrimLeft(_value);
        }

        public static string TrimRight(string s1)
        {
            return s1?.TrimEnd() ?? string.Empty;
        }

        public void TrimRight()
        {
            _value = TrimRight(_value);
        }

        public static string Trim(string s1)
        {
            return s1?.Trim() ?? string.Empty;
        }

        public void Trim()
        {
            _value = Trim(_value);
        }

        public static string JoinString(List<string> vString, string delim)
        {
            return string.Join(delim, vString);
        }

        public static string JoinString(string[] arrString, string delim)
        {
            return string.Join(delim, arrString);
        }

        public static string ReverseWordsInString(string s1)
        {
            if (string.IsNullOrEmpty(s1))
                return s1;

            string[] words = s1.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Array.Reverse(words);
            return string.Join(" ", words);
        }

        public void ReverseWordsInString()
        {
            _value = ReverseWordsInString(_value);
        }

        public static string ReplaceWord(string s1, string stringToReplace, string replaceTo, bool matchCase = true)
        {
            if (string.IsNullOrEmpty(s1))
                return s1;

            StringComparison comparison = matchCase
                ? StringComparison.CurrentCulture
                : StringComparison.CurrentCultureIgnoreCase;

            int pos = 0;
            while ((pos = s1.IndexOf(stringToReplace, pos, comparison)) >= 0)
            {
                s1 = s1.Remove(pos, stringToReplace.Length).Insert(pos, replaceTo);
                pos += replaceTo.Length;
            }

            return s1;
        }

        public string ReplaceWord(string stringToReplace, string replaceTo)
        {
            return ReplaceWord(_value, stringToReplace, replaceTo);
        }

        public static string RemovePunctuations(string s1)
        {
            if (string.IsNullOrEmpty(s1))
                return s1;

            return new string(s1.Where(c => !char.IsPunctuation(c)).ToArray());
        }

        public void RemovePunctuations()
        {
            _value = RemovePunctuations(_value);
        }
    }
}