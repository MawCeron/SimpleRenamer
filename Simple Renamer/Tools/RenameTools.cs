using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

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

        internal static string RenameUsingPatterns(string fileName, string path, string patternOriginal, string patternRename, string count)
        {
            /*  This method parses te patterns given by the user. Posibble patterns are:

                {#} Numbers
                {L} Letters
                {C} Characters (Numbers & letters, not spaces)
                {X} Numbers, letters, and spaces
                {@} Trash  */

            string regexPattern = patternOriginal;
            string newName = patternRename;

            regexPattern = regexPattern.Replace(".", @"\.");
            regexPattern = regexPattern.Replace("[", @"\[");
            regexPattern = regexPattern.Replace("]", @"\]");
            regexPattern = regexPattern.Replace("(", @"\(");
            regexPattern = regexPattern.Replace(")", @"\)");
            regexPattern = regexPattern.Replace("?", @"\?");
            regexPattern = regexPattern.Replace("{#}", @"([0-9]*)");
            regexPattern = regexPattern.Replace("{L}", @"([a-zA-Z]*)");
            regexPattern = regexPattern.Replace("{C}", @"([\S]*)");
            regexPattern = regexPattern.Replace("{X}", @"([\S\s]*)");
            regexPattern = regexPattern.Replace("{@}", @"(.*)");

            try
            {
                Match matchPattern = Regex.Match(fileName, regexPattern);
                for (int i = 1; i < matchPattern.Groups.Count; i++)
                {
                    newName = newName.Replace("{" + i + "}", matchPattern.Groups[i].Value);
                }
            }
            catch (Exception)
            {
                return String.Empty;
            }

            /* Replace {num} with item number
             * If {num2} the number will be 02
             * If {num3+10} the number will be 010 */

            Regex cr = new Regex(@"{(num)([0-9]*)}|{(num)([0-9]*)(\+)([0-9]*)}");

            try
            {
                Match match = cr.Match(newName);
                if (match.Success)
                {
                    GroupCollection groups = match.Groups;

                    if (groups[1].Value == "num")
                    {
                        // Handle {numX}
                        if (!string.IsNullOrEmpty(groups[2].Value))
                        {
                            count = count.PadLeft(int.Parse(groups[2].Value), '0');
                        }
                        newName = cr.Replace(newName, count);
                    }
                    else if (groups[3].Value == "num" && groups[5].Value == "+")
                    {
                        // Handle {numX+Y}
                        if (!string.IsNullOrEmpty(groups[6].Value))
                        {
                            count = (int.Parse(count) + int.Parse(groups[6].Value)).ToString();
                        }
                        if (!string.IsNullOrEmpty(groups[4].Value))
                        {
                            count = count.PadLeft(int.Parse(groups[4].Value), '0');
                        }

                        newName = cr.Replace(newName, count);
                    }
                }
            }
            catch
            {
                // You might want to log or handle the exception in a more specific way
            }

            // Replace {dir} with directory name            
            string dir = Path.GetFileName(path);
            newName = newName.Replace("{dir}", dir);

            // Some date replacements
            DateTime now = DateTime.Now;
            newName = newName.Replace("{date}", now.ToString("ddMMMyyyy"));
            newName = newName.Replace("{datedelim}", now.ToString("dd-MM-yyyy"));
            newName = newName.Replace("{year}", now.ToString("yyyy"));
            newName = newName.Replace("{month}", now.ToString("MM"));
            newName = newName.Replace("{monthname}", now.ToString("MMMM"));
            newName = newName.Replace("{monthsimp}", now.ToString("MMM"));
            newName = newName.Replace("{day}", now.ToString("dd"));
            newName = newName.Replace("{dayname}", now.ToString("dddd"));
            newName = newName.Replace("{daysimp}", now.ToString("ddd"));


            /* 
             * Replace {rand} with random number between 0 and 100.
             * If {rand500} the number will be between 0 and 500
             * If {rand10-20} the number will be between 10 and 20
             * If you add ,5 the number will be padded with 5 digits
             * ie. {rand20,5} will be a number between 0 and 20 of 5 digits (00012)             
             */

            string rnd = string.Empty;
            Regex randPattern = new Regex(@"{(rand)([0-9]*)}|" +
                                          @"{(rand)([0-9]*)(\-)([0-9]*)}|" +
                                          @"{(rand)([0-9]*)(\,)([0-9]*)}|" +
                                          @"{(rand)([0-9]*)(\-)([0-9]*)(\,)([0-9]*)}");

            Random random = new Random();
            Match matchRand = randPattern.Match(newName);

            try
            {
                if (matchRand.Success)
                {
                    GroupCollection groups = matchRand.Groups;

                    if (groups[1].Value == "rand" && string.IsNullOrEmpty(groups[2].Value))
                    {
                        rnd = random.Next(0, 101).ToString();
                    }
                    else if (groups[1].Value == "rand")
                    {
                        rnd = random.Next(0, int.Parse(groups[2].Value) + 1).ToString();
                    }
                    else if (groups[3].Value == "rand" && groups[5].Value == "-")
                    {
                        rnd = random.Next(int.Parse(groups[4].Value), int.Parse(groups[6].Value) + 1).ToString();
                    }
                    else if (groups[7].Value == "rand" && groups[9].Value == ",")
                    {
                        int value = string.IsNullOrEmpty(groups[8].Value) ? random.Next(0, 101) : random.Next(0, int.Parse(groups[8].Value) + 1);
                        rnd = value.ToString().PadLeft(int.Parse(groups[10].Value), '0');
                    }
                    else if (groups[11].Value == "rand" && groups[13].Value == "-" && groups[15].Value == ",")
                    {
                        int value = random.Next(int.Parse(groups[12].Value), int.Parse(groups[14].Value) + 1);
                        rnd = value.ToString().PadLeft(int.Parse(groups[16].Value), '0');
                    }

                    newName = randPattern.Replace(newName, rnd);
                }
            }
            catch (Exception)
            {

            }

            // Returns the new name
            return newName;
        }

        internal static string InsertAt(string fileName, string text, int position)
        {
            // Append text at given position
            
            string newName = String.Empty;
            position = position - 1;

            if (position >= 0)
            {
                string start = fileName.Substring(0, position);
                string end = fileName.Substring(position);
                newName = start + text + end;
            }
            else
                newName = fileName + text;

            return newName;
        }

        internal static string DeleteFrom(string fileName, int startIndex, int finalIndex)
        {
            // Delete chars from startIndex till finalIndex
            startIndex = startIndex - 1;
            finalIndex = finalIndex - 1;

            string initialText = fileName.Substring(0, startIndex);
            string finalText = fileName.Substring(finalIndex);

            return initialText + finalText;
        }
    }
}
