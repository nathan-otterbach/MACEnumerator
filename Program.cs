using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace MACEnumerator
{
    class Program
    {
        static void Main()
        {
            string charSet;
            do
            {
                // Prompt the user to enter the character set
                Console.Write("Enter the character set: ");
                charSet = Console.ReadLine();
                if (string.IsNullOrEmpty(charSet))
                {
                    Console.WriteLine("Invalid character set. Please enter at least one character.");
                }
            } while (string.IsNullOrEmpty(charSet));

            // Prompt the user to enter the value of k
            int k;
            do
            {
                Console.Write("Enter the value of k (must be between 1 and {0}): ", charSet.Length);
                k = int.Parse(Console.ReadLine());
                if (k < 1 || k > charSet.Length)
                {
                    Console.WriteLine("Invalid value of k. Please enter a value between 1 and {0}.", charSet.Length);
                }
            } while (k < 1 || k > charSet.Length);

            // Initialize variables for timing
            DateTime startTime, endTime;

            // Create a concurrent queue to store the combinations
            var combinations = new ConcurrentQueue<string>();

            // Start timing
            startTime = DateTime.Now;

            // Generate all combinations of k-length in parallel
            Parallel.ForEach(Combinations(charSet, k), combination =>
            {
                combinations.Enqueue(combination);
            });

            // Write the combinations to the file using a StreamWriter with a large buffer size
            using (var writer = new StreamWriter("mac.txt", false, System.Text.Encoding.UTF8, 1048576))
            {
                foreach (var combination in combinations)
                {
                    writer.WriteLine(combination);
                }
            }

            // End timing
            endTime = DateTime.Now;

            Console.WriteLine("Combinations generated in {0} seconds", (endTime - startTime).TotalSeconds);

            // Wait for user input before closing the console
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static IEnumerable<string> Combinations(string charSet, int k)
        {
            int n = charSet.Length;
            int[] indices = new int[k];
            for (int i = 0; i < k; i++)
            {
                indices[i] = i;
            }

            while (indices[0] <= n - k)
            {
                yield return new string(indices.Select(i => charSet[i]).ToArray());

                int t = k - 1;
                while (t != 0 && indices[t] == n - k + t)
                {
                    t--;
                }
                indices[t]++;
                for (int j = t + 1; j < k; j++)
                {
                    indices[j] = indices[j - 1] + 1;
                }
            }
        }
    }
}