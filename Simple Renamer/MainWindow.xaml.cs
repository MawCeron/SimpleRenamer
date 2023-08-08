using Simple_Renamer.Explorer;
using System;
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
        }

        private void FolderTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {   
                PopulateFilesGrid(folderPath: FolderTree.CurrentFolder);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void PopulateFilesGrid(string folderPath)
        {
            throw new NotImplementedException();
        }
    }
}
