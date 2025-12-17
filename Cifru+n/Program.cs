using System.Numerics;

namespace Cifru_n
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Intrdu text: ");
            string text = Console.ReadLine();

            Console.WriteLine("Introdu cheia n(0..25): ");
            int n = int.Parse(Console.ReadLine());

            string encrypted = Encrypt(text, n);
            Console.WriteLine("\nText criptat: " + encrypted);

            string decrypted = Decrypt(encrypted, n);
            Console.WriteLine("\nText decriptat: " + decrypted);

            Console.WriteLine("\nCriptanaliza (fara a cunoaste cheia): ");
            Criptanaliza(encrypted);
        }
        static string Encrypt(string text, int key)
        {
            return Shift(text, key);
        }
        static string Decrypt(string text, int key)
        {
            return Shift(text, -key);
        }

        static string Shift(string text, int key)
        {
            char[] result = new char[text.Length];

            for (int i = 0; i < text.Length; i++)
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
        static void Criptanaliza(string encryptedText)
        {
            for (int key = 0; key < 26; key++)
            {
                string decrypted = Decrypt(encryptedText, key);
                Console.WriteLine($"n = {key,2} -> {decrypted}");
            }
        }
    }
}
