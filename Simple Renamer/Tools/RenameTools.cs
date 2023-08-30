using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Simple_Renamer.Tools
{
    internal class RenameTools
    {
        internal static string ReplaceSpaces(string fileName, int mode)
        {
            /*  if mode == 0: ' ' -> '_'
                if mode == 1: '_' -> ' '
                if mode == 2: '_' -> '.'
                if mode == 3: '.' -> ' '
                if mode == 4: ' ' -> '-'
                if mode == 5: '-' -> ' '  */
            
            string newName = String.Empty;

            if (mode == 0)
                newName = fileName.Replace(' ', '_');
            else if (mode == 1)
                newName = fileName.Replace('_', ' ');
            else if (mode == 2)
                newName = fileName.Replace(' ', '.');
            else if (mode == 3)
                newName = fileName.Replace('.', ' ');
            else if (mode == 4)
                newName = fileName.Replace(' ', '-');
            else if (mode == 5)
                newName = fileName.Replace('-', ' ');

            return newName;
        }

        internal static string ReplaceCapitalization(string fileName, int mode)
        {
            /*   0: all to uppercase
                 1: all to lowercase
                 2: first letter uppercase
                 3: first letter of each word uppercase  */

            string newName = String.Empty;

            if (mode == 0)
                newName = fileName.ToUpperInvariant();
            else if (mode == 1)
                newName = fileName.ToLowerInvariant();
            else if (mode == 2)
                newName = fileName.ToUpper().Substring(0, 1) + fileName.Substring(1);
            else if (mode == 3)
                newName = new CultureInfo("en-US", false).TextInfo.ToTitleCase(fileName);

            return newName;
        }

        internal static string ReplaceWith(string fileName, string originalChar, string newChar)
        {
            // Replace all ocurrences of originalChar with newChar

            return fileName.Replace(originalChar, newChar);
        }

        internal static string ReplaceAccents(string fileName)
        {
            // Remove accents, umlauts and other locale symbols from words

            string newName = fileName.Replace('á', 'a');
            newName = newName.Replace('à', 'a');
            newName = newName.Replace('ä', 'a');
            newName = newName.Replace('â', 'a');
            newName = newName.Replace('Á', 'A');
            newName = newName.Replace('À', 'A');
            newName = newName.Replace('Ä', 'A');
            newName = newName.Replace('Â', 'A');

            newName = newName.Replace('é', 'e');
            newName = newName.Replace('è', 'e');
            newName = newName.Replace('ë', 'e');
            newName = newName.Replace('ê', 'e');
            newName = newName.Replace('É', 'E');
            newName = newName.Replace('È', 'E');
            newName = newName.Replace('Ë', 'E');
            newName = newName.Replace('Ê', 'E');

            newName = newName.Replace('í', 'i');
            newName = newName.Replace('ì', 'i');
            newName = newName.Replace('ï', 'i');
            newName = newName.Replace('î', 'i');
            newName = newName.Replace('Í', 'I');
            newName = newName.Replace('Ì', 'I');
            newName = newName.Replace('Ï', 'I');
            newName = newName.Replace('Î', 'I');

            newName = newName.Replace('ó', 'o');
            newName = newName.Replace('ò', 'o');
            newName = newName.Replace('ö', 'o');
            newName = newName.Replace('ô', 'o');
            newName = newName.Replace('Ó', 'O');
            newName = newName.Replace('Ò', 'O');
            newName = newName.Replace('Ö', 'O');
            newName = newName.Replace('Ô', 'O');

            newName = newName.Replace('ú', 'u');
            newName = newName.Replace('ù', 'u');
            newName = newName.Replace('ü', 'u');
            newName = newName.Replace('û', 'u');
            newName = newName.Replace('Ú', 'U');
            newName = newName.Replace('Ù', 'U');
            newName = newName.Replace('Ü', 'U');
            newName = newName.Replace('Û', 'U');

            // Czech language accents replacement
            newName = newName.Replace('ě', 'e');
            newName = newName.Replace('š', 's');
            newName = newName.Replace('č', 'c');
            newName = newName.Replace('ř', 'r');
            newName = newName.Replace('ž', 'z');
            newName = newName.Replace('ý', 'y');
            newName = newName.Replace('ů', 'u');
            newName = newName.Replace('Ě', 'E');
            newName = newName.Replace('Š', 'S');
            newName = newName.Replace('Č', 'C');
            newName = newName.Replace('Ř', 'R');
            newName = newName.Replace('Ž', 'Z');
            newName = newName.Replace('Ý', 'Y');
            newName = newName.Replace('Ů', 'U');

            return newName;
        }

        internal static string ReplaceDuplicated(string fileName)
        {
            // Remove duplicated symbols

            HashSet<char> symbols = new HashSet<char> { '.', ' ', '-', '_' };

            StringBuilder newName = new StringBuilder();
            newName.Append(fileName[0]);
            
            for (int i=1; i<fileName.Length; i++)
            {
                char c = fileName[i];
                if (symbols.Contains(c))
                {
                    if (newName[newName.Length - 1] != c)
                        newName.Append(c);
                }                    
                else
                    newName.Append(c);
            }

            return newName.ToString();
        }
    }
}
