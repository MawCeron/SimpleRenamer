using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Simple_Renamer.Tools
{
    internal class RenameTools
    {
        internal static string Rename(string fileName, string originalPattern, string renamePattern)
        {
            string regexPattern = ConvertPatterToRegex(pattern: originalPattern);
            Regex regex = new Regex(regexPattern);
            Match match = regex.Match(fileName);

            if (!match.Success) return fileName;

            string renamedFileName = renamePattern;

            for (int i = 1; i < match.Groups.Count; i++)
            {
                renamedFileName = renamedFileName.Replace("{" + i + "}", match.Groups[i].Value);
            }

            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string fileExtension = Path.GetExtension(fileName);
            renamedFileName = renamedFileName.Replace("{name}", fileNameWithoutExtension).Replace("{ext}", fileExtension);

            renamedFileName = renamedFileName.Replace("{#}", fileName);


            return renamedFileName;
        }

        private static string ConvertPatterToRegex(string pattern)
        {
            Dictionary<string, string> conversions = new Dictionary<string, string>
            {
                { "{#}", @"\d+" },
                { "{L}", @"[a-zA-Z]+" },
                { "{C}", @"[a-zA-Z0-9]+" },
                { "{X}", @"[a-zA-Z0-9\s]+" },
                { "{@}", ".*?" }
            };

            foreach (var kvp in conversions)
            {
                pattern = pattern.Replace(kvp.Key, $"({kvp.Value})");
            }

            return $"^{pattern}$";
        }
    }
}
