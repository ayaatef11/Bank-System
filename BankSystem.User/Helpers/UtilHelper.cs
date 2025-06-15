using System.Text;

namespace BankSystem.Helpers;
    public static class UtilHelper
    {
        private static readonly Random _random = new Random();

        public enum CharType
        {
            SmallLetter = 1,
            CapitalLetter = 2,
            SpecialCharacter = 3,
            Digit = 4,
            MixChars = 5
        }

        public static int RandomNumber(int from, int to)
        {
            return _random.Next(from, to + 1);
        }

        public static string NumberToText(int number)
        {
            return number.ToString();
        }

        public static char GetRandomCharacter(CharType charType)
        {
            if (charType == CharType.MixChars)
            {
                charType = (CharType)RandomNumber(1, 4);
            }

            switch (charType)
            {
                case CharType.SmallLetter:
                    return (char)RandomNumber(97, 122);
                case CharType.CapitalLetter:
                    return (char)RandomNumber(65, 90);
                case CharType.SpecialCharacter:
                    return (char)RandomNumber(33, 47);
                case CharType.Digit:
                    return (char)RandomNumber(48, 57);
                default:
                    return (char)RandomNumber(65, 90);
            }
        }

        public static string GenerateWord(CharType charType, short length)
        {
            var word = new StringBuilder();
            for (int i = 1; i <= length; i++)
            {
                word.Append(GetRandomCharacter(charType));
            }
            return word.ToString();
        }

        public static string GenerateKey(CharType charType = CharType.CapitalLetter)
        {
            return $"{GenerateWord(charType, 4)}-{GenerateWord(charType, 4)}-{GenerateWord(charType, 4)}-{GenerateWord(charType, 4)}";
        }

        public static string[] GenerateKeys(short numberOfKeys, CharType charType)
        {
            var keys = new string[numberOfKeys];
            for (int i = 0; i < numberOfKeys; i++)
            {
                keys[i] = GenerateKey(charType);
            }
            return keys;
        }

        public static void FillArrayWithRandomNumbers(int[] arr, int from, int to)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = RandomNumber(from, to);
            }
        }

        public static void FillArrayWithRandomWords(string[] arr, CharType charType, short wordLength)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = GenerateWord(charType, wordLength);
            }
        }

        public static void FillArrayWithRandomKeys(string[] arr, CharType charType)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = GenerateKey(charType);
            }
        }

        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        public static void ShuffleArray<T>(T[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                int randomIndex1 = RandomNumber(0, arr.Length - 1);
                int randomIndex2 = RandomNumber(0, arr.Length - 1);
                Swap(ref arr[randomIndex1], ref arr[randomIndex2]);
            }
        }

        public static string Tabs(short numberOfTabs)
        {
            return new string('\t', numberOfTabs);
        }

        public static string EncryptText(string text, short encryptionKey)
        {
            var result = new StringBuilder(text.Length);
            foreach (char c in text)
            {
                result.Append((char)(c + encryptionKey));
            }
            return result.ToString();
        }

        public static string DecryptText(string text, short encryptionKey)
        {
            var result = new StringBuilder(text.Length);
            foreach (char c in text)
            {
                result.Append((char)(c - encryptionKey));
            }
            return result.ToString();
        }
    }
