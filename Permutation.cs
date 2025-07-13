using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MACEnumerator
{
    public class Permutation
    {
        private readonly string _charSet;
        private readonly int _length;

        public Permutation(string charSet, int length)
        {
            if (string.IsNullOrWhiteSpace(charSet))
                throw new ArgumentException("Character set cannot be null or empty.", nameof(charSet));

            if (length < 1)
                throw new ArgumentOutOfRangeException(nameof(length), "Length must be at least 1.");

            _charSet = charSet;
            _length = length;
        }

        /// <summary>
        /// Generates all k-length permutations with repetition from the given character set.
        /// </summary>
        public IEnumerable<string> Generate()
        {
            int n = _charSet.Length;
            var indices = new int[_length];

            while (true)
            {
                // Yield the current permutation
                yield return new string(indices.Select(i => _charSet[i]).ToArray());

                // Increment the "digits"
                int pos = _length - 1;
                while (pos >= 0)
                {
                    indices[pos]++;
                    if (indices[pos] < n)
                        break;

                    indices[pos] = 0;
                    pos--;
                }

                // Finished all permutations
                if (pos < 0)
                    break;
            }
        }

        /// <summary>
        /// Writes all permutations directly to a file with buffered streaming.
        /// </summary>
        public void WriteToFile(string filePath, Encoding? encoding = null, int bufferSize = 1048576)
        {
            encoding ??= Encoding.UTF8;

            using var writer = new StreamWriter(filePath, false, encoding, bufferSize);
            foreach (var permutation in Generate())
            {
                writer.WriteLine(permutation);
            }
        }
    }
}