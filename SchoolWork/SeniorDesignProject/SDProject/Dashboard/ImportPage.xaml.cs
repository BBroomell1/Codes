using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes; 

namespace Dashboard
{
    /// <summary>
    /// Interaction logic for ImportPage.xaml
    /// </summary>
    public partial class ImportPage : Page
    {
        public ImportPage()
        {

            InitializeComponent();
        }

        // Import File Button - Initiates file importing
        private void openfile_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // dlg.FileName = ""; // Default file name
            // dlg.DefaultExt = ".xlsx"; // Default file extension
            dlg.Filter = "Excel documents (.xlsx)|*.xlsx|" + // Filter files by extension
                         "CSV documents (*.csv)|*.csv";

            // Allow for multiple files to be selected
            dlg.Multiselect = true;

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Adds files to Frontend List
                foreach (String file in dlg.FileNames)
                {
                    FileStack.Items.Add(file);
                }
            }
        }

        // Continue Button - Adds remaining selected files to Backend file list
        // then navigates to Dashboard Page
        private void gotoDashboard_Click(object sender, RoutedEventArgs e)
        {
            // Does not allow user to proceed if no files are selected
            if (FileStack.Items.Count == 0)
            {
                MessageBox.Show("Please select at least one file", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Adds files to Backend List
            foreach (String file in FileStack.Items)
            {
                (App.Current as App).FileImported.Add(file);
            }

            // Navigates to Dashboard Page
            DashboardPage dash = new DashboardPage();
            this.NavigationService.Navigate(dash);

        }

        // Remove Button - Removes selected files from Frontend List
        private void RemoveButton(object sender, RoutedEventArgs e)
        {
            // Case if all files are selected for removal
            // Clears entire Frontend List
            if (FileStack.Items.Count == FileStack.SelectedItems.Count)
                FileStack.Items.Clear();

            // Removes n-1 files from the Frontend List
            for (int i = 0; i < FileStack.Items.Count; i++)
            {
                FileStack.Items.Remove(FileStack.SelectedItem);
            }
        }
    }
}
