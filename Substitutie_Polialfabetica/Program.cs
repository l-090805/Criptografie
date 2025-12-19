namespace Substitutie_Polialfabetica
{
    internal class Program
    {
        static readonly string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        static readonly string[] Alphabets =
        {
            "FRQSPYMHNJOELKDVAGXTIWBUZC",
            "SWZCINJTELAFQUMKPXDOVBRGHY",
            "CITYWLNZEVOMQGUPJFXBRAHSKD"
        };
        static void Main(string[] args)
        {
            Console.Write("Introdu plaintextul: ");
            string plaintext = Console.ReadLine().ToUpper();

            string encrypted = Encrypt(plaintext);
            Console.WriteLine("\nText criptat:");
            Console.WriteLine(encrypted);

            string decrypted = Decrypt(encrypted);
            Console.WriteLine("\nText decriptat:");
            Console.WriteLine(decrypted);
        }
        static string Encrypt(string text)
        {
            char[] result = new char[text.Length];
            int n = Alphabets.Length;

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                if (c >= 'A' && c <= 'Z')
                {
                    string alphabet = Alphabets[i % n];
                    int index = Alphabet.IndexOf(c);
                    result[i] = alphabet[index];
                }
                else
                {
                    result[i] = c;
                }
            }

            return new string(result);
        }

        static string Decrypt(string text)
        {
            char[] result = new char[text.Length];
            int n = Alphabets.Length;

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                if (c >= 'A' && c <= 'Z')
                {
                    string alphabet = Alphabets[i % n];
                    int index = alphabet.IndexOf(c);
                    result[i] = Alphabet[index];
                }
                else
                {
                    result[i] = c;
                }
            }

            return new string(result);
        }
    }
}
