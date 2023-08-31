using Simple_Renamer.Explorer;
using Simple_Renamer.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private List<RenamableItem> items;

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

            items = FileTools.GenerateRenamableItemsList(folderPath,option);            
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
            MessageBox.Show(this, "The pattern was successfully saved.", "Simple Renamer", MessageBoxButton.OK, MessageBoxImage.Information);            
        }

        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            int selectedTab = tabOptions.SelectedIndex;

            if (selectedTab == 0)
            {
                PatternsPreview();
            }
            else if (selectedTab == 1)
            {
                SubstitutionsPreview();
            } else if (selectedTab == 2)
            {
                InsertDeletePreview();
            }
        }

        #region Preview Button Actions
        private void InsertDeletePreview()
        {
            List<RenamableItem> renamedItems = new List<RenamableItem>();

            foreach (RenamableItem item in items)
            {
                string newName = item.OriginalName;

                if ((bool)chkInsert.IsChecked)
                {
                    int position = (bool)chkAtEnd.IsChecked ? -1 : (int)numIndexInsert.Value;
                    newName = RenameTools.InsertAt(newName, txInsert.Text, position);
                }

                if ((bool)chkDelete.IsChecked)
                    newName = RenameTools.DeleteFrom(newName, (int)numStart.Value, (int)numEnd.Value);

                item.NewName = newName;

                renamedItems.Add(item);
            }

            FilesDataGrid.ItemsSource = renamedItems;
        }

        private void PatternsPreview()
        {
            
        }

        private void SubstitutionsPreview()
        {
            List<RenamableItem> renamedItems = new List<RenamableItem>();
            
            foreach(RenamableItem item in items)
            {
                string newName = item.OriginalName;

                if ((bool)chkSpaces.IsChecked)
                    newName = RenameTools.ReplaceSpaces(newName, cmbSpaces.SelectedIndex);

                if ((bool)chkReplace.IsChecked)
                    newName = RenameTools.ReplaceWith(newName, txReplaceOriginal.Text, txReplaceNew.Text);
                
                if ((bool)chkCapitalization.IsChecked)
                    newName = RenameTools.ReplaceCapitalization(newName, cmbCapitalization.SelectedIndex);

                if ((bool)chkAccents.IsChecked)
                    newName = RenameTools.ReplaceAccents(newName);

                if ((bool)chkDuplicated.IsChecked)
                    newName = RenameTools.ReplaceDuplicated(newName);

                item.NewName = newName;

                renamedItems.Add(item);
            }

            FilesDataGrid.ItemsSource = renamedItems;
        }
        #endregion

        private void CheckBox_Clicked(object sender, RoutedEventArgs e)
        {
            // Substitution Tab
            cmbSpaces.IsEnabled = (bool)chkSpaces.IsChecked;
            cmbCapitalization.IsEnabled = (bool)chkCapitalization.IsChecked;
            txReplaceOriginal.IsEnabled = (bool)chkReplace.IsChecked;
            txReplaceNew.IsEnabled = (bool)chkReplace.IsChecked;

            // Insert-Delete Tab
            txInsert.IsEnabled = (bool)chkInsert.IsChecked;
            numIndexInsert.IsEnabled = (bool)chkInsert.IsChecked;
            chkAtEnd.IsEnabled = (bool)chkInsert.IsChecked;
            numStart.IsEnabled = (bool)chkDelete.IsChecked;
            numEnd.IsEnabled = (bool)chkDelete.IsChecked;
        }
    }
}
