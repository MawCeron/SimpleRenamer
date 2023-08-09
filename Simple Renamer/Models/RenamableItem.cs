namespace Simple_Renamer
{
    public class RenamableItem
    {

        public enum FileType
        {
            File,
            Folder
        }

        public FileType ItemType { get; set; }
        public string OriginalName { get; set; }
        public string NewName { get; set; }

        public RenamableItem(FileType itemType, string originalName, string newName)
        {
            ItemType = itemType;
            OriginalName = originalName;
            NewName = newName;
        }
    }
}
