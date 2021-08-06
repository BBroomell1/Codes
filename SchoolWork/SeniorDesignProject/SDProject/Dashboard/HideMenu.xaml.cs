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
using System.Windows.Shapes;

namespace Dashboard
{
    /// <summary>
    /// Interaction logic for HideMenu.xaml
    /// </summary>
    public partial class HideMenu : Window
    {
        public HideMenu()
        {
            InitializeComponent();
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            List<String> hl = new List<string>();
            string temp;
            foreach(CheckBox cb in HeaderList.Items)
            {
                if (cb.IsChecked == true)
                {
                    temp = cb.Content.ToString();
                    hl.Add(temp);
                }
            }
            
            
            //
            //(Window.GetWindow(this)).mydatagrid.Columns[0].Visibility = Visibility.Collapsed;
            

           
            Close();
          
        }
    }
}
