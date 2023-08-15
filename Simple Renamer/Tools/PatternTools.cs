using System;
using System.Collections.Generic;
using System.IO;

namespace Simple_Renamer.Tools
{

    public class PatternTools
    {
        private static void CheckPatternFilesExists()
        {
            string patternsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Patterns");

            if (!Directory.Exists(patternsFolderPath))
                Directory.CreateDirectory(patternsFolderPath);

            string originalFilePath = Path.Combine(patternsFolderPath, "original");
            string renamedFilePath = Path.Combine(patternsFolderPath, "renamed");

            if (!File.Exists(originalFilePath))
                File.WriteAllText(originalFilePath, "{X}" + Environment.NewLine);

            if (!File.Exists(renamedFilePath))
                File.WriteAllText(renamedFilePath, "{1}" + Environment.NewLine);
        }

        internal static List<string> LoadPatterns(string? patternType)
        {
            List<string> patterns = new List<string>();
            string patternsFile = String.Empty;

            CheckPatternFilesExists();

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

        internal static void SavePattern(string pattern, string? patternType)
        {
            CheckPatternFilesExists();

            if (!string.IsNullOrEmpty(patternType) && !string.IsNullOrEmpty(pattern))
            {
                string patternsFile = String.Empty;

                if (patternType == "Original")
                    patternsFile = "Patterns/original";
                else if (patternType == "Renamed")
                    patternsFile = "Patterns/renamed";

                if (!string.IsNullOrEmpty(patternsFile))
                {
                    try
                    {
                        using (StreamWriter writer = new StreamWriter(patternsFile, true))
                        {
                            writer.WriteLine(pattern);
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }
    }
}
