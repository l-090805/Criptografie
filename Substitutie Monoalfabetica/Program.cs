namespace Substitutie_Monoalfabetica
{
    internal class Program
    {
        static readonly string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        static readonly char[] RomanianFreqOrder =
        {
            'E','A','I','R','T','N','U','L','O','S',
            'C','D','P','M','V','G','F','B','H','Z',
            'Y','X','W','K','Q','J'
        };
        static void Main(string[] args)
        {
            Console.Write("Introdu textul: ");
            string text = Console.ReadLine().ToUpper();

            string key = GenerateRandomKey();
            Console.WriteLine("\nCheia de criptare:");
            Console.WriteLine(Alphabet);
            Console.WriteLine(key);

            string encrypted = Encrypt(text, key);
            Console.WriteLine("\nText criptat:");
            Console.WriteLine(encrypted);

            string decrypted = Decrypt(encrypted, key);
            Console.WriteLine("\nText decriptat:");
            Console.WriteLine(decrypted);

            string analysis = Cryptanalysis(encrypted);
            Console.WriteLine("\nCriptanaliza (aproximare):");
            Console.WriteLine(analysis);
        }
        static string GenerateRandomKey()
        {
            char[] key = Alphabet.ToCharArray();
            Random rnd = new Random();

            for(int i = key.Length - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
                (key[i], key[j]) = (key[j], key[i]);
            }

            return new string(key);
        }
        static string Encrypt(string text, string key)
        {
            char[] result = new char[text.Length];

            for(int i = 0; i < text.Length; i++)
            {
                char c = char.ToUpper(text[i]);

                if (c >= 'A' && c <= 'Z')
                {
                    int index = c - 'A';
                    result[i] = key[index];
                }
                else
                {
                    result[i] = text[i];
                }
            }
            return new string(result);
        }
        static string Decrypt(string text,string key)
        {
            char[] result = new char[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                char c = char.ToUpper(text[i]);

                if (c >= 'A' && c <= 'Z')
                {
                    int index = key.IndexOf(c);
                    result[i] = Alphabet[index];
                }
                else
                {
                    result[i] = text[i];
                }
            }

            return new string(result);
        }
        static string Cryptanalysis(string encryptedText)
        {
            Dictionary<char, int> freq = new Dictionary<char, int>();

            foreach (char c in encryptedText)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    if (!freq.ContainsKey(c))
                        freq[c] = 0;
                    freq[c]++;
                }
            }

            var sortedCipher = freq
                .OrderByDescending(x => x.Value)
                .Select(x => x.Key)
                .ToArray();

            Dictionary<char, char> approxKey = new Dictionary<char, char>();

            for (int i = 0; i < sortedCipher.Length && i < RomanianFreqOrder.Length; i++)
            {
                approxKey[sortedCipher[i]] = RomanianFreqOrder[i];
            }

            char[] result = new char[encryptedText.Length];

            for (int i = 0; i < encryptedText.Length; i++)
            {
                char c = encryptedText[i];

                if (approxKey.ContainsKey(c))
                    result[i] = approxKey[c];
                else
                    result[i] = c;
            }

            return new string(result);
        }
    }
}
