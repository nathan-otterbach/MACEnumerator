using System;
using System.IO;
using System.Text;

namespace MACEnumerator
{
    class Program
    {
        static void printAllKLength(char[] set, int k, StreamWriter writer)
        {
            int n = set.Length;
            StringBuilder prefix = new StringBuilder(k);

            for (int i = 0; i < k; i++)
            {
                prefix.Append(set[0]);
            }

            while (true)
            {
                writer.WriteLine(prefix);

                int i = k - 1;
                while (i >= 0 && prefix[i] == set[n - 1])
                {
                    i--;
                }

                if (i < 0)
                {
                    break;
                }

                prefix[i] = set[Array.IndexOf(set, prefix[i]) + 1];

                for (int j = i + 1; j < k; j++)
                {
                    prefix[j] = set[0];
                }
            }
        }

        static void Main()
        {
            char[] set1 = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            int k;

            while (true)
            {
                Console.Write("Enter the value of k (between 1 and {0}): ", set1.Length);
                string? input = Console.ReadLine();
                if (int.TryParse(input, out k) && k >= 1 && k <= set1.Length)
                {
                    break;
                }
                Console.WriteLine("Invalid input. Please enter a number between 1 and {0}", set1.Length);
            }
            using (var writer = new StreamWriter("mac.txt", false, Encoding.ASCII, 65536)) // set the buffer size to 64KB
            {
                printAllKLength(set1, k, writer);
            }
        }
    }
}