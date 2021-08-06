/* Dashboard Page contains main dashboard initialization and functionality */

using System;
using System.Data;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Windows.Controls.DataVisualization.Charting;


namespace Dashboard
{

    public partial class DashboardPage : System.Windows.Controls.Page
    {

        //======================================================================================================================//
        //================================================== Global Variables ==================================================//
        //======================================================================================================================//

        // Create SQLite Temp Database Connection and import file to DB
        public static SQLiteConnection sqlite_conn;

        // Holds selected pipeline header strings
        ArrayList pipeline_courses = new ArrayList();
        //Holds Completed Pipeline array
        ArrayList pipeline = new ArrayList();

        // Hold Full Data stat field items
        Stats newStat = new Stats();

        // Hold Pathway stat field items
        Stats newPathwayStat = new Stats();

        //Current views datatable used for saving
        DataTable mainTable = new DataTable();
        DataTable pipelineTable = new DataTable();
        DataTable filterTable = new DataTable();

        //Used to show the current datatable in the viewbox
        DataView view = new DataView();

        //Global vars for filters
        TextBox tblBox;
        string a="a";
        ObservableCollection<string>[] fill;
        int viewchoice = 1;
        ArrayList fullids=new ArrayList();
        string[] dates=new string[2];
        List <string> categories = new List<string>();

        // scrollD is used in results_ScrollChanged() below
        Dictionary<ListBox, ScrollViewer> scrollD = new Dictionary<ListBox, ScrollViewer>();

        // filtering -----------------------------------------
        ObservableCollection<string> l = new ObservableCollection<string>();

        //====================================================================================================================//
        //================================================== Dashboard Main ==================================================//
        //====================================================================================================================//
        public DashboardPage()
        {
            InitializeComponent();

            // Ask user to select a file to import
            ArrayList filename = (App.Current as App).FileImported;

            // Create SQLite Temp Database Connection and import file to DB
            sqlite_conn = TempDB.CreateConnection();

            // Initiate import of selected files
            foreach (String file in filename)
                TempDB.ImportData(sqlite_conn, file);

            //Unions tables if more than one table was imported
            TempDB.TableUnion(sqlite_conn);


            // Get full table with all students
            String cmd = QueryLib.AllStudents();

            // Submits query to temp database
            mainTable = TempDB.FetchData(sqlite_conn, cmd);

            // Creates full data table
            view = mainTable.DefaultView;

            // Send table to DataGrid
            mydatagrid.ItemsSource = view;
            filterTable = mainTable;
            viewchoice = 1;
            resetfill();
            
            // Get column headers for filter-menu
            Menus filterMenu = new Menus();
            ArrayList courses = new ArrayList();
            courses = Pipeline.GetCourses(sqlite_conn);

            foreach (string col in courses)
            {
                // add string to collection
                filterMenu.MenusAdd(col);
            }
            // set this collection as itemsource for menu
            listofSteps.ItemsSource = filterMenu;

            
            // Get total count of courses
            string cmd1 = QueryLib.CourseCount();
            Object number1 = TempDB.StatResult(sqlite_conn, cmd1);

            // Get count of students
            string cmd2 = QueryLib.StudentCount();
            Object number2 = TempDB.StatResult(sqlite_conn, cmd2);

            // Get overall pass rate
            string cmd3 = QueryLib.TotalPassRate();
            Object number3 = TempDB.StatResult(sqlite_conn, cmd3);
            number3 = String.Format("{0:P1}", number3);

            // Get count of retaken courses
            string cmd4 = QueryLib.TotalRetaken();
            Object number4 = TempDB.StatResult(sqlite_conn, cmd4);

            // Makes data source available for binding
            Data res = new Data { TotalCourses = number1, UniqueStudents = number2, OverallRate = number3, RetakenCourses = number4 };
            this.DataContext = res;

            // Add binded data to stat field
            newStat.StatsAdd("Total Courses: ", res.TotalCourses);
            newStat.StatsAdd("Number of Unique Students: ", res.UniqueStudents);
            newStat.StatsAdd("Overall Pass Rate: ", res.OverallRate);
            newStat.StatsAdd("Total Courses Retaken: ", res.RetakenCourses);
            // listAllStats.SelectionMode = SelectionMode.Single;
            listAllStats.ItemsSource = newStat;
            
        }

        //===============================================================================================================//
        //================================================== Filtering ==================================================//
        //===============================================================================================================//

        // Resets filter attributes, part of logic for reset button
        private void resetfill()
        {
            int c;
            fill = new ObservableCollection<string>[mainTable.Columns.Count];
            for (c = 0; c < mainTable.Columns.Count; c++)
                fill[c] = new ObservableCollection<string>();
            fullids.Clear();
        }

        // removes text from search box once it's selected
        public void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
            System.Diagnostics.Debug.WriteLine("In GotFocus");
        }

        // Builds query for multi filter
        private string filterbuild()
        {
            string tabs;
            if (viewchoice == 1) { tabs = "TempTableFinal "; }
            else { tabs = "PipelineTable "; }
            string cmdd = "SELECT * FROM " + tabs;
            int i;
            if (viewchoice == 2)
            {
                cmdd = "SELECT DISTINCT * FROM TempTableFinal WHERE " + QueryLib.SSN + " IN ( ";
                for (i = 0; i < fullids.Count; i++)
                {
                    cmdd += "'" + fullids[i] + "'";
                    if (i < fullids.Count - 1)
                    {
                        cmdd += ", ";
                    }
                }
                cmdd += ") ";
            }

            if (viewchoice == 1 && dates[0] != null && dates[1] != null)
                cmdd += "WHERE " +QueryLib.startDt+ " BETWEEN '" + dates[0] + "' AND '" + dates[1] + "' " +
                    "AND " + QueryLib.endDt + " BETWEEN '" + dates[0] + "' AND '" + dates[1] + "' ";

            for (i = 0; i < fill.Length; i++)
            {
                int j;
                ObservableCollection<string> catfilter = new ObservableCollection<string>();
                catfilter = fill[i];
                for (j = 0; j < catfilter.Count; j++)
                {
                    if (catfilter[j] != null)
                    {
                        if (viewchoice == 2)
                        {
                            if (catfilter[j].Length != 0)
                            {
                                if (catfilter[j][catfilter[j].Length - 1] == '^') { cmdd += "OR " + categories[i + 1] + " LIKE '%" + catfilter[j].Substring(0, catfilter[j].Length - 1) + "%' "; }
                                else { cmdd += "AND " + categories[i + 1] + " LIKE '%" + catfilter[j] + "%' "; }
                            }
                        }
                        else if (catfilter[j].Length != 0)
                        {
                            if (cmdd.Equals("SELECT * FROM " + tabs))
                            {
                                if (catfilter[j][catfilter[j].Length - 1] == '^')
                                    cmdd += "WHERE " + categories[i + 1] + " LIKE '%" + catfilter[j].Substring(0, catfilter[j].Length - 1) + "%' ";
                                else
                                    cmdd += "WHERE " + categories[i + 1] + " LIKE '%" + catfilter[j] + "%' ";
                            }
                            else if (catfilter[j][catfilter[j].Length - 1] == '^') { cmdd += "OR " + categories[i + 1] + " LIKE '%" + catfilter[j].Substring(0, catfilter[j].Length - 1) + "%' "; }
                            else { cmdd += "AND " + categories[i + 1] + " LIKE '%" + catfilter[j] + "%' "; }
                        }

                    }
                }

            }
            return cmdd;
        }

        // Date Filtering
        private void dp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // Retrieves entered dates from date boxes and applies filter
            DatePicker r = (sender as DatePicker);
            if(r.Name.ToString().Equals("StartDate"))
            {
                dates[0]=TempDB.dateConvert(r.SelectedDate.Value);
            }
            else
                dates[1]=TempDB.dateConvert(r.SelectedDate.Value);
            if(viewchoice==1 && dates[0] != null && dates[1] != null)
            {
                DataTable table6 = TempDB.FetchData(sqlite_conn, filterbuild());

                for (int i = 0; i < table6.Rows.Count; i++)
                {
                    l.Add(table6.Rows[i][0].ToString());
                }
                
                // Creates full data table
                view = table6.DefaultView;
                // Send table to DataGrid
                mydatagrid.ItemsSource = view;
            }
            
        }

        //every time a textbox is loaded name changes and acts as a dynamic counter
        private void tbTest_Loaded(object sender, RoutedEventArgs e)
        {
            tblBox = (sender as TextBox);
            tblBox.Name=a;
            a+="a";
        }

        //Textbox event which activates on enter key press for filter
        private void tbTest_TextChanged(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                tblBox = (sender as TextBox);
                List <string> strArray = new List<string>();
                strArray.Add("");
                foreach (DataColumn header in filterTable.Columns)
                {
                    if (header.ColumnName.Contains(" "))
                        strArray.Add("\"" + header.ColumnName +"\"");
                    else
                        strArray.Add(header.ColumnName);
                }
                ObservableCollection<String> l = new ObservableCollection<String>();
                int len=(tblBox.Name.Length-2)%filterTable.Columns.Count;
                string cat=strArray[len+1];
                string attribute=tblBox.Text;
                categories=strArray;
                //fill[len]=attribute;
                fill[len].Add(attribute);
                string cmdd=filterbuild();
                DataTable table6 = TempDB.FetchData(sqlite_conn, cmdd);

                for (int i = 0; i < table6.Rows.Count; i++)
                {
                    l.Add(table6.Rows[i][0].ToString());
                }
                
                // Creates full data table
                view = table6.DefaultView;
                // Send table to DataGrid
                mydatagrid.ItemsSource = view;
                }
        }

        //===================================================================================================================//
        //================================================== Chart Binding ==================================================//
        //===================================================================================================================//

        // Binding for Column Chart
        // Currently set to display the pass rate for courses selected in pipeline
        private void LoadColumnChartData(DataTable data)
        {
            ArrayList Cols = new ArrayList();
            ArrayList Rows = new ArrayList();

            for (int i = 0; i < data.Columns.Count; i++)
            {
                Cols.Add(data.Columns[i].ColumnName);
            }

            foreach(DataRow row in data.Rows)
              foreach (object dc in row.ItemArray)
                Rows.Add(dc);

            List<KeyValuePair<object, double>> chart = new List<KeyValuePair<object,double>>();

            for (int i = 0; i < data.Columns.Count; i++)
            {
                chart.Add(new KeyValuePair<object, double>(Cols[i], Convert.ToDouble(Rows[i])));
            }

            ((ColumnSeries)mcChart.Series[0]).ItemsSource = chart;

            ((ColumnSeries)mcChart.Series[0]).Title = " Pass Rate";
        }

        //===================================================================================================================//
        //================================================== Stats Binding ==================================================//
        //===================================================================================================================//

        // Creates Data Binding Objects for Front-End display of statistics
        public class Data
        {
            public ObservableCollection<string> _collection;
            public ObservableCollection<string> Collection
            {
                get { return _collection; }
            }

            private Object students;
            public Object UniqueStudents
            {
                get { return students; }
                set { students = value; }
            }

            private Object rate;
            public Object OverallRate
            {
                get { return rate; }

                set
                {
                    if (value != rate)
                        rate = value;
                }
            }

            private Object totalcourses;
            public Object TotalCourses
            {
                get { return totalcourses; }

                set
                {
                    if (value != totalcourses)
                        totalcourses = value;
                }
            }

            private Object retaken;
            public Object RetakenCourses
            {
                get { return retaken; }

                set
                {
                    if (value != retaken)
                        retaken = value;
                }
            }

            private Object totpipeline;
            public Object TotalPipelineStudents
            {
                get { return totpipeline; }

                set
                {
                    if (value != totpipeline)
                        totpipeline = value;
                }
            }

            private Object transfers;
            public Object PipelineTransferStudents
            {
                get { return transfers; }

                set
                {
                    if (value != transfers)
                        transfers = value;
                }
            }

            private Object success;
            public Object PipelineFullCompletion
            {
                get { return success; }

                set
                {
                    if (value != success)
                        success = value;
                }
            }

            private Object partial_success;
            public Object PipelinePartialCompletion
            {
                get { return partial_success; }

                set
                {
                    if (value != partial_success)
                        partial_success = value;
                }
            }

            private Object first_attempt;
            public Object PipelineFirstAttemptCompletion
            {
                get { return first_attempt; }

                set
                {
                    if (value != first_attempt)
                        first_attempt = value;
                }
            }

            private Object completion;
            public Object PipelineCompletion
            {
                get { return completion; }

                set
                {
                    if (value != completion)
                        completion = value;
                }
            }

            private Object nonTransfer;
            public Object NonPipelineTransferStudents
            {
                get { return nonTransfer; }

                set
                {
                    if (value != nonTransfer)
                        nonTransfer = value;
                }
            }

            private Object nonTransferComplete;
            public Object PipelineNonTransferCompletion
            {
                get { return nonTransferComplete; }

                set
                {
                    if (value != nonTransferComplete)
                        nonTransferComplete = value;
                }
            }

        }


        //=====================================================================================================================//
        //================================================== Pathway Builder ==================================================//
        //=====================================================================================================================//

        // Once pipeline is selected and "Apply Pipeline" is click, execute all pipeline related features
        private void executePipeline_Click(object sender, RoutedEventArgs e)
        {
            
            if (createPipeline.HasItems == false)
            {
                System.Windows.MessageBox.Show("Please apply a pathway selection first", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            newPathwayStat.Clear();
            pipeline.Clear();
            listAllStats2.UnselectAll();

            foreach (object item in createPipeline.Items)
            {
                pipeline.Add(item.ToString());
            }
                

            // Query - Creates a new Table that holds each student's status in the 3 pipeline courses
            // selected returning a 1 if they have passed the pipeline course and a 0 if they have failed
            QueryLib.CreatePipelineTable(sqlite_conn, pipeline);

            String cmd = QueryLib.GetPipelineTable(); // ** Pipeline View **

            // Submits query to temp database
            pipelineTable = TempDB.FetchData(sqlite_conn, cmd);
            // Creates full data table
            view = pipelineTable.DefaultView;
            // Send table to DataGrid
            mydatagrid.ItemsSource = view;

            // Submits Pipeline chart query
            string cmd0 = QueryLib.PipeLineChart(pipeline);
            DataTable table0 = TempDB.FetchData(sqlite_conn, cmd0);

            LoadColumnChartData(table0);

            // Query Temp Database - Get total pipeline students
            string cmd5 = QueryLib.TotalPipelineStudents();
            Object number5 = TempDB.StatResult(sqlite_conn, cmd5);

            // Query Temp Database - Get transfer students
            string cmd6 = QueryLib.PipelineTransferStudents(pipeline);
            Object number6 = TempDB.StatResult(sqlite_conn, cmd6);

            // Query Temp Database - Get students that successfully completed pipeline
            string cmd7 = QueryLib.PipelineFullCompletion(pipeline);
            Object number7 = TempDB.StatResult(sqlite_conn, cmd7);

            // Query Temp Database - Get students that transferred into pipeline
            // and successfully completed final course of pipeline
            string cmd8 = QueryLib.PipelinePartialCompletion(pipeline);
            Object number8 = TempDB.StatResult(sqlite_conn, cmd8);

            // Query Temp Database - Get students that successfully completed pipeline
            // on their first attemp
            string cmd9 = QueryLib.PipelineFirstAttemptCompletion(pipeline);
            Object number9 = TempDB.StatResult(sqlite_conn, cmd9);

            // Query Temp Database - Get students that successfully completed pipeline
            // whether or not it was their first attempt
            string cmd10 = QueryLib.PipelineCompletion(pipeline);
            Object number10 = TempDB.StatResult(sqlite_conn, cmd10);

            // Query Temp Database - Get non- transfer students
            string cmd11 = QueryLib.NonPipelineTransferStudents(pipeline);
            Object number11 = TempDB.StatResult(sqlite_conn, cmd11);

            // Query Temp Database - Get non- transfer students that completed pathway
            string cmd12 = QueryLib.PipelineNonTransferCompletion(pipeline);
            Object number12 = TempDB.StatResult(sqlite_conn, cmd12);

            viewchoice =0;
            a="aa";
            filterTable=pipelineTable;
            resetfill();

            // Makes data source available for binding
            Data res = new Data
            {
                TotalPipelineStudents = number5,
                PipelineTransferStudents = number6,
                PipelineFullCompletion = number7,
                PipelinePartialCompletion = number8,
                PipelineFirstAttemptCompletion = number9,
                PipelineCompletion = number10,
                NonPipelineTransferStudents = number11,
                PipelineNonTransferCompletion = number12

            };
            this.DataContext = res;

            // Calculate pathway stat percentages
            string pctPipelineCompletion = String.Format("{0:P1}", (Convert.ToDouble(res.PipelineCompletion) / Convert.ToDouble(res.TotalPipelineStudents)));
            string pctTransferCompletion = String.Format("{0:P1}", (Convert.ToDouble(res.PipelinePartialCompletion) / Convert.ToDouble(res.PipelineTransferStudents)));
            string pctNonTransferCompletion = String.Format("{0:P1}", (Convert.ToDouble(res.PipelineNonTransferCompletion) / Convert.ToDouble(res.NonPipelineTransferStudents)));
            string pctUninterruptedCompletion = String.Format("{0:P1}", (Convert.ToDouble(res.PipelineFullCompletion) / Convert.ToDouble(res.TotalPipelineStudents)));
            string pctFirstAttemptCompletion = String.Format("{0:P1}", (Convert.ToDouble(res.PipelineFirstAttemptCompletion) / Convert.ToDouble(res.TotalPipelineStudents)));

            // Add pipeline stats to stat field
            newPathwayStat.StatsAdd("Pathway Students: ", res.TotalPipelineStudents);
            newPathwayStat.StatsAdd("Transfer-In Students: ", res.PipelineTransferStudents);
            newPathwayStat.StatsAdd("Non-Transfer Students: ", res.NonPipelineTransferStudents);
            newPathwayStat.StatsAdd("Pathway Completions: ", res.PipelineCompletion + " (" + pctPipelineCompletion + " of Pathway Students)");
            newPathwayStat.StatsAdd("Transfer-In Pathway Completions: ", res.PipelinePartialCompletion + " (" + pctTransferCompletion + " of Transfer-In Students)");
            newPathwayStat.StatsAdd("Non-Transfer Pathway Completions: ", res.PipelineNonTransferCompletion + " (" + pctNonTransferCompletion + " of Non-Transfer Students)"); 
            newPathwayStat.StatsAdd("Uninterrupted Pathway Completions: ", res.PipelineFullCompletion + " (" + pctUninterruptedCompletion + " of Pathway Students)");
            newPathwayStat.StatsAdd("1st Attempt Uninterrupted Pathway Completions: ", res.PipelineFirstAttemptCompletion + " (" + pctFirstAttemptCompletion + " of Pathway Students)");
            // listAllStats.SelectionMode = SelectionMode.Single;
            listAllStats2.ItemsSource = newPathwayStat;

            showHideListBox.Items.Clear();
            foreach (DataGridColumn c in mydatagrid.Columns)
            {
                CheckBox temp = new CheckBox();
                temp.Content = c.Header.ToString();
                showHideListBox.Items.Add(temp);
            }

        }

        // Pipeline functionality, toggles visible components after first step
        private void OnClick_Next(object sender, RoutedEventArgs e)
        {
            Menus newMenu = new Menus();
            Pathways newPathway = new Pathways();
            ArrayList courses = new ArrayList();
            courses = Pipeline.GetPipeLineOptions(sqlite_conn, pipeline_courses);

            foreach (string col in courses)
            {
                // add string to collection
                newMenu.MenusAdd(col);
               
                System.Diagnostics.Debug.WriteLine("Adding to collection: " + col);
                Boolean afterSplit = false;
                string course = null;
                string iteration = null;

                for (int i = 0; i < col.Length; i++)
                {
                    if (col[i] == '_')
                    {
                        afterSplit = true;
                    }
                    else if (!afterSplit)
                    {
                        course += col[i];
                    }
                    else
                    {
                        iteration += col[i];
                    }
                }

                newPathway.PathwaysAdd(course, iteration);
            }

            // every element in hiddenlist matches up to corresponding element in listofSteps2
            // if hiddenlist contains "1_200" as first item in list, then listofSteps2 has "Course 1 Iteration 200" as first item in list
            hiddenlist.ItemsSource = newMenu;
            listofSteps2.ItemsSource = newPathway;         

            listofSteps.Visibility = Visibility.Hidden;
            listofSteps2.Visibility = Visibility.Visible;

            endResultList.Visibility = Visibility.Visible;
            nextButton.Visibility = Visibility.Hidden;
            backButton.Visibility = Visibility.Visible;
            step1.Visibility = Visibility.Hidden;
            step2.Visibility = Visibility.Visible;
            nextButton2.Visibility = Visibility.Visible;
            ClosePathwayBuilder.Visibility = Visibility.Collapsed;
        }

        private void OnClick_Next2(object sender, RoutedEventArgs e)
        {
            Boolean presentinlist = false;
            int indexOfSelected = listofSteps2.SelectedIndex;

            if (listofSteps2.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("Please select an item before clicking 'Add'", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // only add item if not already in list
            foreach (object item in createPipeline2.Items)
            {
                if (listofSteps2.SelectedItem == item)
                {
                    presentinlist = true;
                    break;
                }
            }

            // if confirmed that item is not already in list, go through process to add to list
            if (presentinlist == false)
            {
                string element = listofSteps2.SelectedItem.ToString();

                // Problem: need to keep format of data as 1_200, 2_200, etc. for executePipeline_Click() to work as is
                // however, the user needs to see the data in the format "Course 1 Iteration 200"
                // we also cannot retrieve both 1 and 200 when using listofSteps2.SelectedItem(), so cannot manually change format that way
                // Solution: the second step in creating a pathway will have two listboxes,
                // one, "listofsteps2" is the one the user sees and interacts with
                // the other, "hiddenlist" has all the same items as the first but in a different format (1_200, 2_200)
                // the order of the elements in the list is the same for both lists, so their indices match up also
                // meaning we can find the old format (1_200, 2_200) version of each item in the list using the index of the item selected in listofsteps2
                // this value is then added to createPipeline (hidden), while its visible counterpart is added to createPipeline2 (visible)
                // when executePipeline_Click() runs, it can then use the value from createPipeline as it did in the previous version of the application
                createPipeline.Items.Add(hiddenlist.Items[indexOfSelected]);      

                createPipeline2.Items.Add(listofSteps2.SelectedItem);
                indexPipeline.Items.Add(indexPipeline.Items.Count + 1);
                step2.Text = step2.Text = "Step 2: select item #" + (indexPipeline.Items.Count + 1) + " in pathway and click add";
            }
            else
            {
                System.Windows.MessageBox.Show("Please select an item not already in pathway list", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                listofSteps2.UnselectAll();
                return;
            }

            listofSteps2.UnselectAll();

        }

        private void OnClick_Back(object sender, RoutedEventArgs e)
        {
            if (createPipeline.Items.Count > 0)
            {
                step2.Text = "Step 2: select item #" + (createPipeline.Items.Count) + " in pathway and click add";

                createPipeline.Items.RemoveAt(createPipeline.Items.Count - 1);
                indexPipeline.Items.RemoveAt(indexPipeline.Items.Count - 1);
                
                createPipeline2.Items.RemoveAt(createPipeline2.Items.Count - 1);
            }
            else
            {   // going back to first step / clearing everything
                createPipeline.Items.Clear();
                indexPipeline.Items.Clear();

                listofSteps.Visibility = Visibility.Visible;
                listofSteps2.Visibility = Visibility.Hidden;

                endResultList.Visibility = Visibility.Hidden;
                nextButton.Visibility = Visibility.Visible;
                backButton.Visibility = Visibility.Hidden;
                step1.Visibility = Visibility.Visible;
                step2.Visibility = Visibility.Hidden;
                nextButton2.Visibility = Visibility.Hidden;
                ClosePathwayBuilder.Visibility = Visibility.Visible;
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            createPipeline.Items.Clear();
            indexPipeline.Items.Clear();
            createPipeline2.Items.Clear();

            openButton.Visibility = Visibility.Visible;
            showSteps.Visibility = Visibility.Collapsed;
            listofSteps.Visibility = Visibility.Visible;
            listofSteps2.Visibility = Visibility.Hidden;

            endResultList.Visibility = Visibility.Hidden;
            nextButton.Visibility = Visibility.Visible;
            backButton.Visibility = Visibility.Hidden;
            step1.Visibility = Visibility.Visible;
            step2.Visibility = Visibility.Hidden;

            // clear checked boxes in listofsteps
            listofSteps.UnselectAll();
          
            nextButton2.Visibility = Visibility.Hidden;
            step2.Text = "Step 2: select item #1 in pathway and click add";

        }

        private void openEditor(object sender, RoutedEventArgs e)
        {
            openButton.Visibility = Visibility.Collapsed;
            showSteps.Visibility = Visibility.Visible;
            ClosePathwayBuilder.Visibility = Visibility.Visible;

            if (showHideListBox.Visibility == Visibility.Visible)
                showHideListBox.Visibility = Visibility.Collapsed;
            if (CloseHideColumns.Visibility == Visibility.Visible)
                CloseHideColumns.Visibility = Visibility.Collapsed;
            if (hideButton.Visibility == Visibility.Visible)
                hideButton.Visibility = Visibility.Collapsed;
            if (UnCheckAllButton.Visibility == Visibility.Visible)
                UnCheckAllButton.Visibility = Visibility.Collapsed;
            
        }

        // listofsteps listbox behaviour when checking a check box in list
        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            Boolean presentinlist = false;

            // loop through to send to collection / add to listbox, not adding if already present
            foreach (object item1 in listofSteps.SelectedItems)
            {
                if (item1 != null)
                {
                    for (int i = 0; i < pipeline_courses.Count; i++)
                    {
                        object item2 = pipeline_courses[i];

                        if ((item1.ToString()).Equals(item2.ToString()))
                        {
                            // already in listbox, dont add
                            presentinlist = true;
                        }
                    }

                    if (presentinlist == false)
                    {
                        // not in listbox, add to list
                        string value = item1.ToString();
                        pipeline_courses.Add(value);

                        break;
                    }
                    else
                    {
                        // reset value for next iteration of loop
                        presentinlist = false;
                    }
                }
                else
                {
                    // if list is empty
                    string value = item1.ToString();
                    pipeline_courses.Add(value);

                }
            }
        }

        // listofsteps listbox behaviour when unchecking a check box
        private void HandleUnchecked(object sender, RoutedEventArgs e)
        {

            Boolean presentinlist = false;

            for (int i = 0; i < pipeline_courses.Count; i++)
            {
                object draftPipelineItem = pipeline_courses[i];

                // look to see if current element is selected in listofSteps
                foreach (object selectedItem in listofSteps.SelectedItems)
                {
                    if ((draftPipelineItem.ToString()).Equals(selectedItem.ToString()))
                    {
                        // item is present in both lists
                        presentinlist = true;
                        break;
                    }
                }
                if (presentinlist == false)
                {
                    // item not present in selectedItems
                    // remove from pipeline 
                    pipeline_courses.RemoveAt(i);
                    indexPipeline.Items.Clear();
                }

                // reset values for next loop
                presentinlist = false;
            }
        }

        // This allows the createPipeline2 listbox & the indexPipeline listbox to scroll together / share one scrollbar
        private void results_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {

            ScrollViewer scrollViewer = (ScrollViewer)e.OriginalSource;

            ListBox mylist = (ListBox)e.Source;

            if (!scrollD.ContainsKey(mylist))
            {
                scrollD.Add(mylist, scrollViewer);
            }

            foreach (ScrollViewer sv in scrollD.Values)
            {
                if (sv != scrollViewer)
                {
                    sv.ScrollToVerticalOffset(e.VerticalOffset);
                }
            }
        }

        //======================================================================================================================================//
        //================================================== DataGrid Views and Styling ========================================================//
        //======================================================================================================================================//

        // View individual student data by double clicking DataGrid Cell
        private void DataGridCell_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (mydatagrid.SelectedCells.Count != 1)
                return;

            // Then fetch the column header
            string selectedColumnHeader = (string)mydatagrid.SelectedCells[0].Column.Header;

            if (selectedColumnHeader != QueryLib.SSN)
                return;

            var dataGridCellTarget = (DataGridCell)sender;
            var cellcontent = (TextBlock)dataGridCellTarget.Content;
            string cellvalue = cellcontent.Text;


            string cmd = "Select * FROM TempTableFinal WHERE " + QueryLib.SSN + " = '" + cellvalue + "' ORDER BY " + QueryLib.Name + ", " + QueryLib.SSN + ", " + QueryLib.courseCD + ", " + QueryLib.classNo + "";

            // Submits query to temp database
            view = TempDB.FetchData(sqlite_conn, cmd).DefaultView;

            // Send table to DataGrid
            mydatagrid.ItemsSource = view;
            viewchoice = 2;
            a = "aa";
            filterTable = view.ToTable();
            resetfill();
            fullids.Add(cellvalue);
        }

        // Applies a cell style to cells only in the specified columns
        void mydatagrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {          
            // if cell is in the SSN column, use "CellStyle2" as style (defined in DashboardPage.xaml)
            if (e.PropertyName == "" + QueryLib.SSN + "")
            {
                e.Column.CellStyle = (sender as DataGrid).FindResource("CellStyle2") as Style;
            }
        }

        // Toggles the current DataGrid to a Pipeline View
        private void Toggle_Pipeline_Click(object sender, RoutedEventArgs e)
        {
            // Makes sure that PipelineTable exists in the database before trying to apply Pipeline View
            var tableFlag = 1;
            string check = "SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name = 'PipelineTable'";
            DataTable table = TempDB.FetchData(sqlite_conn, check);
            tableFlag = int.Parse(table.Rows[0][0].ToString());
            if (tableFlag == 0)
            {
                System.Windows.MessageBox.Show("Please apply a pathway selection first", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DataTable tempTable = new DataTable();
            ArrayList uniqueIDs = new ArrayList();
            

            tempTable = view.ToTable(true, QueryLib.SSN);
            foreach (DataRow row in tempTable.Rows)
            {
                uniqueIDs.Add(row[QueryLib.SSN]);
            }

            // Run query to toggle pipeline view
            String cmd = QueryLib.TogglePipeline(uniqueIDs); 

            // Submits query to temp database
            view = TempDB.FetchData(sqlite_conn, cmd).DefaultView;

            // Send table to DataGrid
            mydatagrid.ItemsSource = view;
            viewchoice=0;
            a="aa";
            filterTable=pipelineTable;
            resetfill();

            if (showHideListBox.Items.Count != mydatagrid.Columns.Count)
            {
                showHideListBox.Items.Clear();
                foreach (DataGridColumn c in mydatagrid.Columns)
                {
                    CheckBox temp = new CheckBox();
                    temp.Content = c.Header.ToString();
                    showHideListBox.Items.Add(temp);
                }
            }

            foreach (CheckBox cb in showHideListBox.Items)
            {
                cb.IsChecked = false;
            }

        }

        // Toggles the current DataGrid to a Full View
        private void Toggle_Full_Click(object sender, RoutedEventArgs e)
        {
            DataTable tempTable = new DataTable();
            ArrayList uniqueIDs = new ArrayList();


            tempTable = view.ToTable(true,  QueryLib.SSN);
            foreach (DataRow row in tempTable.Rows)
            {
                uniqueIDs.Add(row[QueryLib.SSN]);
            }

            // Run query to toggle full view
            String cmd = QueryLib.ToggleFull(uniqueIDs);

            // Submits query to temp database
            view = TempDB.FetchData(sqlite_conn, cmd).DefaultView;

            // Send table to DataGrid
            mydatagrid.ItemsSource = view;
            viewchoice=2;
            a="aa";
            filterTable=view.ToTable();
            resetfill();
            fullids=uniqueIDs;

            if (showHideListBox.Items.Count != mydatagrid.Columns.Count)
            {
                showHideListBox.Items.Clear();
                foreach (DataGridColumn c in mydatagrid.Columns)
                {
                    CheckBox temp = new CheckBox();
                    temp.Content = c.Header.ToString();
                    showHideListBox.Items.Add(temp);
                }
            }
            foreach (CheckBox cb in showHideListBox.Items)
            {
                cb.IsChecked = false;
            }
        }

        // Toggles the current DataGrid to a Full View
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            // Run query to toggle full view
            String cmd = QueryLib.AllStudents();

            // Submits query to temp database
            view = TempDB.FetchData(sqlite_conn, cmd).DefaultView;
            viewchoice=1;
            a="aa";
            filterTable=mainTable;
            resetfill();
            // Send table to DataGrid
            mydatagrid.ItemsSource = view;

            if (showHideListBox.Items.Count != mydatagrid.Columns.Count)
            {
                showHideListBox.Items.Clear();
                foreach (DataGridColumn c in mydatagrid.Columns)
                {
                    CheckBox temp = new CheckBox();
                    temp.Content = c.Header.ToString();
                    showHideListBox.Items.Add(temp);
                }
            }
            foreach (CheckBox cb in showHideListBox.Items)
            {
                cb.IsChecked = false;
            }    

        }

        // Changes Data Grid View to display selected stat data
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Whenever a stat is click this will select the statement and query the data for those students that are in each category
            // Must match stat field names in PathwayBuilder section
            if(listAllStats2.SelectedItem != null)
            {
                String selection = listAllStats2.SelectedItem.ToString();

                if (selection == "Pathway Students: ")
                {
                    view = StatsSelection.TotalPipelineStudents(sqlite_conn);
                    mydatagrid.ItemsSource = view;
                }
                else if(selection == "Transfer-In Students: ")
                {
                    view = StatsSelection.TotalTransferInStudents(sqlite_conn, pipeline);
                    mydatagrid.ItemsSource = view;
                }
                else if (selection == "Non-Transfer Students: ")
                {
                    view = StatsSelection.NonPipelineTransferStudents(sqlite_conn, pipeline);
                    mydatagrid.ItemsSource = view;
                }
                else if (selection == "Uninterrupted Pathway Completions: ")
                {
                    view = StatsSelection.PipelineFullCompletion(sqlite_conn, pipeline);
                    mydatagrid.ItemsSource = view;
                }
                else if(selection == "Transfer-In Pathway Completions: ")
                {
                    view = StatsSelection.PipelinePartialCompletion(sqlite_conn, pipeline);
                    mydatagrid.ItemsSource = view;
                }
                else if (selection == "Non-Transfer Pathway Completions: ")
                {
                    view = StatsSelection.PipelineNonTransferCompletion(sqlite_conn, pipeline);
                    mydatagrid.ItemsSource = view;
                }
                else if(selection == "1st Attempt Uninterrupted Pathway Completions: ")
                {
                    view = StatsSelection.PipelineFirstAttemptCompletion(sqlite_conn, pipeline);
                    mydatagrid.ItemsSource = view;
                }
                else if (selection == "Pathway Completions: ")
                {
                    view = StatsSelection.PipelineCompletion(sqlite_conn, pipeline);
                    mydatagrid.ItemsSource = view;
                }

            }

        }

        // changes color of row based on 1) pathway pass/fail or 2) course pass/fail (depending on type of datagrid)
        void mydatagrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            // changes row color depending on cell value in last column of row
            // endIndex represents the last column in the datagrid
            int endIndex = mydatagrid.Columns.Count - 1;

            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
           
            // didn't pass last class in pathway / failed
            if (((System.Data.DataRowView)(e.Row.DataContext)).Row.ItemArray[endIndex].ToString() == "RF" ||
                ((System.Data.DataRowView)(e.Row.DataContext)).Row.ItemArray[endIndex].ToString() == "NF" ||
                (((System.Data.DataRowView)(e.Row.DataContext)).Row.ItemArray[endIndex].ToString() == "") ||
                ((System.Data.DataRowView)(e.Row.DataContext)).Row.ItemArray[endIndex].ToString() == " "
                )
            {
                // initial datagrid has 12 columns
                if ((mydatagrid.Columns.Count >= 18))
                {
                    // default datagrid can have " " in end index
                    // default datagrid will fail this case and not change row color, ensuring that this if() only changes color if this is a pathway datagrid
                    if (((System.Data.DataRowView)(e.Row.DataContext)).Row.ItemArray[16].ToString() != "P" &&
                    ((System.Data.DataRowView)(e.Row.DataContext)).Row.ItemArray[16].ToString() != "F")
                    {
                        // row color for failing: "#EEAAAA"
                        e.Row.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#EEAAAA"));
                        return;
                    }
                }
                // less than 12 columns, must be pathway datagrid
                else
                {
                    // row color for failing: "#EEAAAA"
                    e.Row.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#EEAAAA"));
                    return;
                }
            }
            // completed pathway successfully / pass final course in pathway
            if (((System.Data.DataRowView)(e.Row.DataContext)).Row.ItemArray[endIndex].ToString() == "RP" ||
                ((System.Data.DataRowView)(e.Row.DataContext)).Row.ItemArray[endIndex].ToString() == "NP")
            {
                // row color for passing: "LightGreen"
                e.Row.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("LightGreen"));
                return;
            }
            // default datagrid pass
            if (mydatagrid.Columns.Count >= 16 && ((System.Data.DataRowView)(e.Row.DataContext)).Row.ItemArray[16].ToString() == "P")
            {
                // row color for passing: "LightGreen"
                e.Row.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("LightGreen"));
                return;
            }
            // default datagrid fail
            if (mydatagrid.Columns.Count >= 16 && ((System.Data.DataRowView)(e.Row.DataContext)).Row.ItemArray[16].ToString() == "F")
            {
                // row color for failing: "#EEAAAA"
                e.Row.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#EEAAAA"));
                return;
            }
        }

        // Hide DataGrid Columns
        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            HideMenu hm = new HideMenu();
            hm.Show();
            
            foreach(DataGridColumn c in mydatagrid.Columns)
            {
                CheckBox temp = new CheckBox();
                temp.Content = c.Header;
                temp.IsChecked = false;
                hm.HeaderList.Items.Add(temp);              
            }
         
        }

        private void openHideColumns(object sender, RoutedEventArgs e)
        {
            if (showHideListBox.Visibility == Visibility.Visible)
                showHideListBox.Visibility = Visibility.Collapsed;
            else
            {
                showHideListBox.Visibility = Visibility.Visible;
                if (showHideListBox.Items.Count != mydatagrid.Columns.Count)
                {
                    showHideListBox.Items.Clear();
                    foreach (DataGridColumn c in mydatagrid.Columns)
                    {
                        CheckBox temp = new CheckBox();
                        temp.Content = c.Header.ToString();
                        showHideListBox.Items.Add(temp);
                    }
                }
            }
            if (hideButton.Visibility == Visibility.Visible)
                hideButton.Visibility = Visibility.Collapsed;
            else
                hideButton.Visibility = Visibility.Visible;

            if (CloseHideColumns.Visibility == Visibility.Visible)
                CloseHideColumns.Visibility = Visibility.Collapsed;
            else
                CloseHideColumns.Visibility = Visibility.Visible;
            if (UnCheckAllButton.Visibility == Visibility.Visible)
                UnCheckAllButton.Visibility = Visibility.Collapsed;
            else
                UnCheckAllButton.Visibility = Visibility.Visible;
        }

        private void CloseHideColumns_Click(object sender, RoutedEventArgs e)
        {
            hideButton.Visibility = Visibility.Collapsed;
            CloseHideColumns.Visibility = Visibility.Collapsed;
            showHideListBox.Visibility = Visibility.Collapsed;
            UnCheckAllButton.Visibility = Visibility.Collapsed;
            foreach (CheckBox cb in showHideListBox.Items)
                cb.IsChecked = false;
        }

        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            List<string> headerlist = new List<string>();
            List<string> visList = new List<string>();
            foreach(CheckBox cb in showHideListBox.Items)
            {
                if (cb.IsChecked == true)
                    headerlist.Add(cb.Content.ToString());
                else
                    visList.Add(cb.Content.ToString());
            }
            
            foreach(DataGridColumn c in mydatagrid.Columns)
            {
                
                foreach (string s in headerlist)
                {
                    if (c.Header.ToString() == s)
                    {
                        c.Visibility = Visibility.Collapsed;
                    }                                         
                }

                foreach (string s in visList)
                {
                    if (c.Header.ToString() == s)
                    {
                        c.Visibility = Visibility.Visible;
                    }
                }
            }          
        }

        // Unchecks all checked boxes on Column Hider selection field
        private void UnCheckAllButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (CheckBox cb in showHideListBox.Items)
                cb.IsChecked = false;
        }

        //=====================================================================================================================//
        //================================================== Datagrid Saving ==================================================//
        //=====================================================================================================================//

        // Takes the unique students that are in the current view and saves them in the original format to use for import later
        // Using the overall tempTableFinal will select all the students that are in the current view and save a CSV file
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DataTable tempTable = new DataTable();
            DataTable saveTable = new DataTable();
            ArrayList uniqueIDs = new ArrayList();
            tempTable = view.ToTable(true, "" + QueryLib.SSN + "");
            foreach (DataRow row in tempTable.Rows)
            {
                uniqueIDs.Add(row["" + QueryLib.SSN + ""]);
            }

            saveTable = TempDB.FetchData(sqlite_conn, SaveFile.commandForSave(uniqueIDs));
            SaveFile.Makeworksheet(saveTable);

        }

        // Saves the current datatable view with current columns and rows in a CSv file
        private void SaveView_Click(object sender, RoutedEventArgs e)
        {
            SaveFile.Makeworksheet(view.Table);
        }

        //==================================================================================================================//
        //================================================== Re-Importing ==================================================//
        //==================================================================================================================//

        // Navigates back to import page for re- importing
        private void BackImport_Click(object sender, RoutedEventArgs e)
        {
            sqlite_conn.Close();
            (App.Current as App).FileImported.Clear();
            TempDB.tablenum = 0;
            ImportPage import = new ImportPage();
            this.NavigationService.Navigate(import);
        }


    }
}
