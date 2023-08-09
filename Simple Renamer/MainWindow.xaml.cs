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
            var selectedItem = (ComboBoxItem)optionsCombo.SelectedItem;
            int option = Int32.Parse((string)selectedItem.Tag);

            List<RenamableItem> items = FileTools.GenerateRenamableItemsList(folderPath,option);            
            FilesDataGrid.ItemsSource = items;
        }
    }
}
