using Simple_Renamer.Explorer;
using Simple_Renamer.Tools;
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
            PupulatePatternCombos();
        }

        private void PupulatePatternCombos()
        {
            cmbOriginal.ItemsSource = PatternTools.LoadPatterns("Original");
            cmbRenamed.ItemsSource = PatternTools.LoadPatterns("Renamed");
            cmbOriginal.SelectedIndex= 0;
            cmbRenamed.SelectedIndex = 0;
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

        private void btnSavePattern_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string pattern = String.Empty;
            string? patternType = button.Tag.ToString();

            if (patternType == "Original")
                pattern = cmbOriginal.Text;
            else if (patternType == "Renamed")
                pattern = cmbRenamed.Text;

            PatternTools.SavePattern(pattern, patternType);
        }
    }
}
