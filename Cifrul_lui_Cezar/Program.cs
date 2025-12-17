using System.Numerics;

namespace Cifrul_lui_Cezar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Introduceti textul: ");
            string text = Console.ReadLine();

            string encrypted = Encrypt(text, 3);
            Console.WriteLine("\nText criptat: "  + encrypted);

            int estimatedKey = Criptanaliza(encrypted);
            Console.WriteLine("\nCheia estimata prin criptanaliza: " + estimatedKey);

            string decrypted = Decrypt(encrypted, estimatedKey);
        }
        static string Encrypt(string text, int key)
        {
            return Shift(text, key);
        }

        static string Decrypt(string text,int key)
        {
            return Shift(text, -key);
        }
        
        static string Shift(string text, int key)
        {
            char[] result = new char[text.Length];

            for(int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    result[i] = (char)((((c - offset) + key + 26) % 26) + offset);
                }
                else
                {
                    result[i] = c;
                }
            }
            return new string(result);
        }
        static int Criptanaliza(string encryptedText)
        {
            int bestKey = 0;
            double bestScore = double.MaxValue;

            for (int key = 0; key < 26; key++)
            {
                string decrypted = Decrypt(encryptedText, key);
                var freq = LetterFrequency(decrypted);

                double score = 0;

                foreach (var kv in RomanianFreq)
                {
                    double observed = freq.ContainsKey(kv.Key) ? freq[kv.Key] : 0;
                    double expected = kv.Value;

                    score += Math.Pow(observed - expected, 2) / expected;
                }

                if (score < bestScore)
                {
                    bestScore = score;
                    bestKey = key;
                }
            }

            return bestKey;
        }

        static Dictionary<char, double> LetterFrequency(string text)
        {
            Dictionary<char, int> counts = new Dictionary<char, int>();
            int total = 0;

            foreach (char c in text.ToUpper())
            {
                if (c >= 'A' && c <= 'Z')
                {
                    if (!counts.ContainsKey(c))
                        counts[c] = 0;

                    counts[c]++;
                    total++;
                }
            }

            Dictionary<char, double> freq = new Dictionary<char, double>();

            foreach (var kv in counts)
                freq[kv.Key] = (kv.Value * 100.0) / total;

            return freq;
        }

        static readonly Dictionary<char, double> RomanianFreq = new Dictionary<char, double>
        {
            {'A', 9.65}, {'B', 1.30}, {'C', 4.12}, {'D', 3.65}, {'E', 10.10},
            {'F', 0.70}, {'G', 1.10}, {'H', 0.10}, {'I', 9.40}, {'J', 0.50},
            {'K', 0.10}, {'L', 5.60}, {'M', 3.40}, {'N', 6.90}, {'O', 5.30},
            {'P', 3.10}, {'Q', 0.01}, {'R', 8.30}, {'S', 5.40}, {'T', 7.10},
            {'U', 6.30}, {'V', 1.50}, {'W', 0.01}, {'X', 0.01}, {'Y', 0.01},
            {'Z', 0.01}
        };
    }
}
