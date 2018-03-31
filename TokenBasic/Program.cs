// Program.cs - 03/30/2018

using System;

namespace TokenBasic
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            string filename = "D:\\BIN\\PIRATE_TOKEN.TXT";
            LoadProgram(filename);
            bool firstLine = true;
            foreach (string s in tokens)
            {
                if (s.Equals("{LINE}"))
                {
                    if (!firstLine)
                    {
                        Console.WriteLine();
                    }
                    firstLine = false;
                }
                else
                {
                    Console.Write(" ");
                }
                Console.Write(s);
            }
            RunProgram();
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Press enter...");
            Console.ReadLine();
        }
    }
}
