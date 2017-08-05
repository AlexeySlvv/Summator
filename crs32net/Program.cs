using SummatorLib;
using System;
using System.IO;

namespace crc32net
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Utils.PrintUsage("crc32net");
                return;
            }

            foreach (string file in args)
            {
                try
                {
                    Console.WriteLine($"{file}\t{Files.Crc32Sum(file)}");
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine($"{file} not found");
                }
            }
        }
    }
}
