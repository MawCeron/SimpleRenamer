using Simple_Renamer.Explorer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Simple_Renamer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {   
            InitializeComponent();
            FolderTree.CurrentFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            PopulateFilesGrid(FolderTree.CurrentFolder);
        }

        private void FolderTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                DirectoryText.Text = FolderTree.CurrentFolder;
                PopulateFilesGrid(folderPath: FolderTree.CurrentFolder);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void PopulateFilesGrid(string folderPath)
        {
            var option = (ComboBoxItem)optionsCombo.SelectedItem;
            List<RenamableItem> items = new List<RenamableItem>();
            items.Add(new RenamableItem(RenamableItem.FileType.File, "Archivo.txt", ""));
            items.Add(new RenamableItem(RenamableItem.FileType.File, "Archivo 1.txt", ""));
            items.Add(new RenamableItem(RenamableItem.FileType.File, "Archivo 2.txt", ""));
            items.Add(new RenamableItem(RenamableItem.FileType.Folder, "Mas Archivos", ""));
            FilesDataGrid.ItemsSource = items;
        }
    }
}
