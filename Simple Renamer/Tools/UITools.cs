using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Renamer.Tools
{

    public class UITools
    {
        internal static List<string> LoadPatterns(string? patternType)
        {
            List<string> patterns = new List<string>();
            string patternsFile = String.Empty;

            if(!string.IsNullOrEmpty(patternType))
            {
                if (patternType == "Original")
                    patternsFile = "Patterns/original";
                else if (patternType == "Renamed")
                    patternsFile = "Patterns/renamed";
            }

            if (!string.IsNullOrEmpty(patternsFile))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(patternsFile))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            patterns.Add(line);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return patterns;
        }
    }
}
