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

        
    }
}
