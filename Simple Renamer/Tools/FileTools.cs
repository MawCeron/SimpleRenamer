using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Renamer
{
    internal class FileTools
    {
        internal static List<RenamableItem> GenerateRenamableItemsList(string folderPath, int option)
        {
            List<RenamableItem> renamableItems = new List<RenamableItem>();

            switch (option)
            {
                case 0:
                    AddItems(renamableItems, folderPath, RenamableItem.FileType.File);
                    break;
                case 1:
                    AddItems(renamableItems, folderPath, RenamableItem.FileType.Folder);
                    break;
                case 2:
                    AddItems(renamableItems, folderPath, RenamableItem.FileType.Folder);
                    AddItems(renamableItems, folderPath, RenamableItem.FileType.File);
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }

            return renamableItems;
        }

        private static void AddItems(List<RenamableItem> items, string folderPath, RenamableItem.FileType itemType, string newName = "")
        {
            try
            {
                string[] names = itemType == RenamableItem.FileType.File ?
                                    Directory.GetFiles(folderPath) :
                                    Directory.GetDirectories(folderPath);

                foreach (var name in names)
                {
                    string itemName = Path.GetFileName(name);
                    items.Add(new RenamableItem(itemType, itemName, ""));
                }
            }
            catch (Exception)
            {
                
            }
        }

    }

}
