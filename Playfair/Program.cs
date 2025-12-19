using System.Text;

namespace Playfair
{
    internal class Program
    {
        static char[,] matrix = new char[5, 5];

        static void Main(string[] args)
        {
            Console.Write("Introdu cheia: ");
            string key = Console.ReadLine().ToUpper().Replace("J", "I");

            BuildMatrix(key);

            Console.Write("Introdu text: ");
            string plaintext = Console.ReadLine().ToUpper().Replace("J", "I");

            string preparedText = PrepareText(plaintext);
            Console.WriteLine("\n text pregatit: " + preparedText);

            string encrypted = Encrypt(preparedText);
            Console.WriteLine("\ncriptat: " + encrypted);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
        static void BuildMatrix(string key)
        {
            bool[] used = new bool[26];
            key = key.Replace(" ", "");

            int x = 0, y = 0;
            foreach (char c in key)
            {
                int index = c - 'A';
                if (index >= 0 && index < 26 && !used[index])
                {
                    matrix[x, y] = c;
                    used[index] = true;
                    y++;
                    if (y == 5)
                    {
                        y = 0;
                        x++;
                    }
                }
            }

            for (char c = 'A'; c <= 'Z'; c++)
            {
                if (c == 'J') continue;
                int index = c - 'A';
                if (!used[index])
                {
                    matrix[x, y] = c;
                    used[index] = true;
                    y++;
                    if (y == 5)
                    {
                        y = 0;
                        x++;
                    }
                }
            }
        }
        static string PrepareText(string input)
        {
            StringBuilder sb = new StringBuilder();
            input = input.Replace(" ", "");

            for (int i = 0; i < input.Length; i++)
            {
                char a = input[i];
                if (i + 1 < input.Length)
                {
                    char b = input[i + 1];
                    if (a == b)
                    {
                        sb.Append(a);
                        sb.Append('X');
                    }
                    else
                    {
                        sb.Append(a);
                        sb.Append(b);
                        i++;
                    }
                }
                else
                {
                    sb.Append(a);
                    sb.Append('X');
                }
            }
            return sb.ToString();
        }
        static string Encrypt(string text)
        {
            StringBuilder encrypted = new StringBuilder();

            for (int i = 0; i < text.Length; i += 2)
            {
                char a = text[i];
                char b = text[i + 1];

                (int row1, int col1) = FindPosition(a);
                (int row2, int col2) = FindPosition(b);

                if (row1 == row2)
                {
                    encrypted.Append(matrix[row1, (col1 + 1) % 5]);
                    encrypted.Append(matrix[row2, (col2 + 1) % 5]);
                }
                else if (col1 == col2)
                {
                    encrypted.Append(matrix[(row1 + 1) % 5, col1]);
                    encrypted.Append(matrix[(row2 + 1) % 5, col2]);
                }
                else
                {
                    encrypted.Append(matrix[row1, col2]);
                    encrypted.Append(matrix[row2, col1]);
                }
            }
            return encrypted.ToString();
        }
        static (int, int) FindPosition(char c)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (matrix[i, j] == c)
                        return (i, j);
                }
            }
            return (-1, -1);
        }
    }
}
