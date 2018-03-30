using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenBasic
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            string filename = "D:\\BIN\\PIRATE_TOKEN.TXT";
            LoadProgram(filename);
            RunProgram();
            Console.Write("Press enter...");
            Console.ReadLine();
        }
    }
}
