using SummatorLib;
using System;
using System.IO;

namespace md5net
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Utils.PrintUsage("md5net");
                return;
            }

            foreach (string file in args)
            {
                try
                {
                    Console.WriteLine($"{file}\t{Files.Md5Sum(file)}");
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine($"{file} not found");
                }
            }
        }
    }
}
