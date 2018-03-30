// LoadRoutines.cs - 03/30/2018

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TokenBasic
{
    public partial class Program
    {
        internal static string[] programLines;
        internal static Dictionary<int, int> lineNumberIndex;
        internal static string[,] tokens;

        internal static void LoadProgram(string filename)
        {
            programLines = File.ReadAllLines(filename);
            CreateLineNumberIndex();
            CreateTokenList();
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

        internal static void CreateTokenList()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder token = new StringBuilder();
            for (int lineIndex = 0; lineIndex < programLines.Count(); lineIndex++)
            {
                sb.Clear();
                token.Clear();
                bool inToken = false;
                bool inQuote = false;
                bool inWord = false;
                bool inData = false;
                foreach (char c in programLines[lineIndex])
                {
                    if (c == '"')
                    {
                        if (!inQuote)
                        {
                            if (inWord)
                            {
                                sb.Append("\t");
                                inWord = false;
                            }
                            sb.Append(c);
                            inQuote = true;
                        }
                        else
                        {
                            sb.Append(c);
                            sb.Append("\t");
                            inQuote = false;
                        }
                        continue;
                    }
                    if (inQuote)
                    {
                        sb.Append(c);
                        continue;
                    }
                    if (inData)
                    {
                        if (c == ',')
                        {
                            sb.Append("\t"); // separate data by tabs
                        }
                        else
                        {
                            sb.Append(c);
                        }
                        continue;
                    }
                    if (c == ' ')
                    {
                        if (inQuote)
                        {
                            sb.Append(c);
                        }
                        else if (inWord)
                        {
                            sb.Append("\t");
                            inWord = false;
                        }
                        else if (inToken)
                        {
                            Console.WriteLine("Error!");
                        }
                        continue;
                    }
                    if ((c >= 'A' && c <= 'Z')
                        || (c >= 'a' && c <= 'z')
                        || (c >= '0' && c <= '9'))
                    {
                        sb.Append(c);
                        if (inToken)
                        {
                            token.Append(c);
                        }
                        else
                        {
                            inWord = true;
                        }
                        continue;
                    }
                    if (c == '$')
                    {
                        sb.Append(c);
                        if (inToken)
                        {
                            token.Append(c);
                        }
                        else if (inWord)
                        {
                            sb.Append("\t");
                            inWord = false;
                        }
                        continue;
                    }
                    if (c == '{')
                    {
                        if (inQuote)
                        {
                            sb.Append(c);
                            continue;
                        }
                        if (inWord)
                        {
                            sb.Append("\t");
                            inWord = false;
                        }
                        inToken = true;
                        sb.Append(c);
                        token.Clear();
                        token.Append(c);
                        continue;
                    }
                    if (c == '}')
                    {
                        if (inQuote)
                        {
                            sb.Append(c);
                            continue;
                        }
                        sb.Append(c);
                        sb.Append("\t");
                        token.Append(c);
                        if (token.ToString().Equals("{DATA}"))
                        {
                            inData = true;
                        }
                        inToken = false;
                        continue;
                    }
                    if (inQuote || inToken)
                    {
                        sb.Append(c);
                        continue;
                    }
                    if (inWord)
                    {
                        sb.Append("\t");
                        inWord = false;
                    }
                    sb.Append(c);
                    sb.Append("\t");
                }
                Console.WriteLine(sb.ToString()); // todo
            }
        }
    }
}
