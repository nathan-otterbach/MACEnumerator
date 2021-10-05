using System;

namespace MACEnumerator
{
    class Programm
    {
        static void printAllKLength(char[] set, int k)
        {
            int n = set.Length;
            printAllKLengthRec(set, "", n, k);
        }

        static void printAllKLengthRec(char[] set,String prefix,int n, int k)
        {
            if (k == 0)
            {
                Console.WriteLine(prefix);
                return;
            }
            for (int i = 0; i < n; ++i)
            {
                String newPrefix = prefix + set[i];
                printAllKLengthRec(set, newPrefix,n, k - 1);
            }
        }

        static public void Main()
        {
            char[] set1 = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            int k = 2;

            FileStream filestream = new FileStream("mac.txt", FileMode.Create);
            var streamwriter = new StreamWriter(filestream);
            streamwriter.AutoFlush = true;
            Console.SetOut(streamwriter);
            Console.SetError(streamwriter);
            
            printAllKLength(set1, k);
        }
    }
}