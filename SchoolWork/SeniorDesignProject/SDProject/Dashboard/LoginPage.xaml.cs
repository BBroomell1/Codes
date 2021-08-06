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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {

            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            /// this opens up new page
            /// TODO: implement secure login, currently just moving between pages without using username / password info
            /// Previous code from when it was all in MainWindow / Non-Navigation window
            /// ImportPage importpage = new ImportPage();
            ///this.Content = importpage;
            ///
            ImportPage importpage = new ImportPage();
            this.NavigationService.Navigate(importpage);

        }
    }
}
