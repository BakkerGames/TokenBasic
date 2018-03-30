// LoadRoutines.cs - 03/30/2018

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TokenBasic
{
    public partial class Program
    {
        internal static string[] programLines;
        internal static Dictionary<int, int> lineNumberIndex;

        internal static void LoadProgram(string filename)
        {
            programLines = File.ReadAllLines(filename);
            CreateLineNumberIndex();
        }

        internal static void CreateLineNumberIndex()
        {
            // create line number index for fast access
            lineNumberIndex = new Dictionary<int, int>();
            for (int lineIndex = 0; lineIndex < programLines.Count(); lineIndex++)
            {
                if (programLines[lineIndex][0] >= '0' && programLines[lineIndex][0] <= '9')
                {
                    int lineNumber = 0;
                    int charNumber = 0;
                    while (programLines[lineIndex][charNumber] >= '0'
                        && programLines[lineIndex][charNumber] <= '9')
                    {
                        lineNumber = (lineNumber * 10) + programLines[lineIndex][charNumber] - '0';
                        charNumber++;
                    }
                    lineNumberIndex.Add(lineNumber, lineIndex);
                    //Console.WriteLine(programLines[lineIndex]);
                    //Console.WriteLine($"{lineIndex} {lineNumber}");
                }
            }
        }
    }
}
