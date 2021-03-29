using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace SortingAlgorithmVisualizer
{
    public partial class MainForm : Form
    {
        // defining the array that will be sorted
        int[] unsortedArray; 

        // defining a graphics object that will provide a visual representation of the sorting algorithms 
        Graphics graphicsPanel; 

        // defining a Background Worker to run the sorting algorithm so we don't lock the main form 
        BackgroundWorker executionThread = new BackgroundWorker(); 

        // defining a boolean variable to check if the sorting process is paused or not
        private bool isPaused = false; 

        public MainForm()
        {
            InitializeComponent();

            // populate the DropDownList with the available sorting algorithms
            PopulateDropDown(); 

            // block the Pause/Resume button at the begining of the program
            btnPauseResume.Enabled = false;

            // Background Worker constructors
            executionThread.DoWork += new DoWorkEventHandler(bgw_DoWork);
            executionThread.RunWorkerCompleted += bgw_RunWorkerCompleted;

        }


        // get the name of classes that implement the ISortEngine, and add them to the algorithms combo Box
        private void PopulateDropDown()
        {
            // search through the current Domanin and get the assemblies that are implementations of the ISortInterface
            List<string> ClassList = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()) 

                // exclude the interface itself and also any abstract classes (which are not used but for completeness is verified)
                .Where(x => typeof(ISortEngine).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)  

                // casting the names as a list and putting them in the ClassList Variable
                .Select(x => x.Name).ToList(); 

            // sorting the list of algorithms names alphabetically
            ClassList.Sort();

            // filling the algorithms comboBox with the names of the algorithms available to use
            foreach (string entry in ClassList)
            {
                algorithmsBox.Items.Add(entry);
            }

            // the initial selected algorithm will be the first one in the comboBox
            algorithmsBox.SelectedIndex = 0;
        }


        // Group all the buttons event handlers in one region where it's easier to find
        #region Buttons

        // defining the event handler for the Sort Button 
        private void btnSort_Click(object sender, EventArgs e)
        {
            // clicking the Sort button before the Reset button will initiate the unsortedArray allows the sorting process to begin
            if (unsortedArray == null) btnReset_Click(null, null);

            //while the sorting proccess is going on the Sort and Reset buttons will become unavailable to use until the sorting process has finished
            btnSort.Enabled = false;
            btnReset.Enabled = false;

            // the Pause/Resume button will become available to use so we can change the sorting algorithm used even though the sorting process has not finished
            btnPauseResume.Enabled = true;
            
            // gives the Background Worker the propriety to be cancelled whilst the sorting process is still going on
            executionThread.WorkerSupportsCancellation = true;

            //Starts the Background Worker and provides it with the necessary algorithm
            executionThread.RunWorkerAsync(argument: algorithmsBox.SelectedItem); 
        }

        //defining the Pause/Resume event handler
        private void btnPauseResume_Click(object sender, EventArgs e)
        {

            // if the sorting process is not paused 
            if (!isPaused)
            {   
                //the Background Worker will be canceled
                executionThread.CancelAsync();

                // the boolean variable will indicate that the sorting process is on pause
                isPaused = true;
            }
            // if the sorting process is paused
            else
            {
                
                if (executionThread.IsBusy) return; 

                // declaring the number of elements in the unsorted Array as beeing the panel Width / 10 
                int numEntries = Graphics.Width / 10;

                // declaring the maximum value that an element in the unsorted Array can have
                int maxVal = Graphics.Height;

                // the boolean variable will indicate that the sorting process has resumed
                isPaused = false;

                // we will declare 2 pens that will be used to draw rectangles in the graphics object
                // the rectangles height will represent the value of the element height
                Pen whitePen = new Pen(Color.White, 10);
                Pen bluePen = new Pen(Color.MidnightBlue, 10);

                // drawing the rectangles in the graphics object
                int i, j;
                j = 0; i = 0;
                while (j < Graphics.Width)
                {
                    graphicsPanel.DrawRectangle(bluePen, j, 0, 1, maxVal);
                    graphicsPanel.DrawRectangle(whitePen, j, maxVal - unsortedArray[i], 1, maxVal);
                    j += 10;
                    i++;
                }


                // the Backgroud Worker will start the sorting process with the argument that it has been provided
                executionThread.RunWorkerAsync(argument: algorithmsBox.SelectedItem);
                
            }
                
        }

        
        // defining the Reset event handler
        private void btnReset_Click(object sender, EventArgs e)
        {
            // when the unsorted Array has been reseted the Pause/Resume button will become unavailable and the Sort button will become available 
            btnPauseResume.Enabled = false;
            btnSort.Enabled = true;

            // we will create a graphics pannel in the panel1 that exists on the main form
            graphicsPanel = Graphics.CreateGraphics();

            // declaring the number of elements in the unsorted Array as beeing the panel Width / 10 
            int numEntries = Graphics.Width / 10;

            // declaring the maximum value that an element in the unsorted Array can have
            int maxValue = Graphics.Height;

            // declaring the unsorted Array as having numEntries length
            unsortedArray = new int[numEntries];

            // declaring a white pen that will be used to draw rectangles in the graphics object
            Pen whitePen = new Pen(Color.White, 10);

            // filling the graphics Panel with the MidnightBlue color
            graphicsPanel.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.MidnightBlue), 0, 0, Graphics.Width, maxValue);

            // assigning random values to the unsorted Array
            Random rand = new Random();
            int i;
            for (i = 0; i < numEntries; i++)
            {
                unsortedArray[i] = rand.Next(0, maxValue);
            }

            // Drawing the white rectangles in the graphics panel
            // this rectangles represent the value of the unsorted Array elements
            int j = 0; i = 0;
            while (j < Graphics.Width)
            {
                graphicsPanel.DrawRectangle(whitePen, j, maxValue - unsortedArray[i], 1, maxValue);
                j += 10;
                i++;
            }

            // release all the resources that the Background Worker used
            executionThread.Dispose();
           
        }

        #endregion

        // Group all the background methods in one place were it's easier to find
        #region BackGroundRegion 

        public void bgw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            // we cast the sender as being the thing that invokes this method as being a background worker
            BackgroundWorker bw = sender as BackgroundWorker;

            // used to extract the name of the sort engine name from the argument e.Argument
            string SortEngineName = (string)e.Argument;
            
            Type type = Type.GetType("SortingAlgorithmVisualizer." + SortEngineName);

            var constructor = type.GetConstructors();
            try
            {
                ISortEngine se = (ISortEngine)constructor[0].Invoke(new object[] { unsortedArray, graphicsPanel, Graphics.Height });
                
                // while the sorting process has not been finished or cancelled we will execute the NextStep method to keep going
                while (!se.isSorted() && (!executionThread.CancellationPending))
                {
                    se.NextStep();
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        
        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            // if the sorting process has been cancelled then the Reset button will become available and the Pause/Resume button will become unavailable
            else if (!e.Cancelled && !isPaused)
            {
                btnReset.Enabled = true;
                btnPauseResume.Enabled = false;
            }
            
        }
        
       #endregion
    }
}
