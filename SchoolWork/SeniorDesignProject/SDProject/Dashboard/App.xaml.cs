using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Collections;
using System.Data.SQLite;


using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Dashboard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ArrayList fileImported = new ArrayList();

        public ArrayList FileImported 
        { 
            get
            {
                return fileImported;
            }
            set
            {
                fileImported = value;
            }
        }
        // Uri associated with XML file
        // public Uri uriReference { get; set; }
        public string WorkSheetName { get; set; }
        public object StatVal1 { get; set; }

    }
}
