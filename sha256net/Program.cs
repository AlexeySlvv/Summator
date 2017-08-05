using SummatorLib;
using System;
using System.IO;

namespace sha256net
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Utils.PrintUsage("sha256net");
                return;
            }

            foreach (string file in args)
            {
                try
                {
                    Console.WriteLine($"{file}\t{Files.Sha256Sum(file)}");
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine($"{file} not found");
                }
            }
        }
    }
}
