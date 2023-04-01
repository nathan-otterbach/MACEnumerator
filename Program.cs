using System.Runtime.InteropServices;
using System.Text;

namespace MACEnumerator
{
    class Program
    {
        static unsafe void printAllKLength(char[] set, int k, StreamWriter writer)
        {
            int n = set.Length;

            // create a lookup table to find the index of each character in the set
            int* lookupTable = (int*)Marshal.AllocHGlobal(n * sizeof(int));
            for (int i = 0; i < n; i++)
            {
                lookupTable[set[i]] = i;
            }

            // create an array to store the prefix string
            char[] prefix = new char[k];
            for (int i = 0; i < k; i++)
            {
                prefix[i] = set[0];
            }

            // generate all combinations of k-length
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

                prefix[i] = set[lookupTable[prefix[i]] + 1];

                for (int j = i + 1; j < k; j++)
                {
                    prefix[j] = set[0];
                }
            }

            // free the memory used by the lookup table
            Marshal.FreeHGlobal((IntPtr)lookupTable);
        }

        static void Main()
        {
            char[] set1 = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            int k;
            DateTime startTime, endTime;

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

            startTime = DateTime.Now;

            using (var writer = new StreamWriter("mac.txt", false, Encoding.ASCII, 65536)) // set the buffer size to 64KB
            {
                printAllKLength(set1, k, writer);
            }

            endTime = DateTime.Now;
            Console.WriteLine("Time taken: {0} seconds", (endTime - startTime).TotalSeconds);

            // Wait for user input before closing the console
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}