using Simple_Renamer.Explorer;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Simple_Renamer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal static string currentFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        public MainWindow()
        {   
            InitializeComponent();
            FolderTree.CurrentFolder = currentFolder;
            PopulateFilesGrid(FolderTree.CurrentFolder);
        }

        private void FolderTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                currentFolder = FolderTree.CurrentFolder;
                DirectoryText.Text = currentFolder;
                PopulateFilesGrid(folderPath: currentFolder);
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

        private void optionsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(currentFolder))
                return;

            PopulateFilesGrid(folderPath: currentFolder);
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnEditPattern_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            PatternEditor editor = new PatternEditor(patternType: button.Tag.ToString());
            editor.Owner = this;
            editor.Show();
        }
    }
}
