using System.Drawing;
using System.Linq;


namespace SortingAlgorithmVisualizer
{
    class BubbleSortEngine : ISortEngine
    {

        #region Declaration of Variables and Objects used in the algorithm

        // Local variables used in the algorithm 
        private int[] Array;
        private Graphics Obj;
        private int maxVal;

        // Declaration of 2 pens and 1 brush for an easier color swap
        Pen whitePen = new Pen(Color.White, 10);
        Pen bluePen = new Pen(Color.MidnightBlue, 10);
        Brush BlueBrush = new System.Drawing.SolidBrush(System.Drawing.Color.MidnightBlue);

        #endregion
       
        //declaring the BubbleSortEngine constructor     
        public BubbleSortEngine(int[] inArray, Graphics inObj, int inmaxVal)
        {
            Array = inArray;
            Obj = inObj;
            maxVal = inmaxVal;
        }
      
        public void NextStep()
        {
            BubbleSort(Array);
        }

        #region Sorting Methods
        public void BubbleSort(int[] Array)
        {
           for (int j = 0; j < Array.Count() - 1; j++)
           {
              if (Array[j] > Array[j + 1])
              {
                 Swap(j, j + 1);
              }
           }
        }
        #endregion

        #region Functionality Methods for: isSorted(), Swap variables, Draw rectangles 
        // Method used to check if the Array is sorted or not
        public bool isSorted()
        {
            for (int i = 0; i < Array.Count() - 1; i++)
            {
                if (Array[i] > Array[i + 1]) return false;
            }
            return true;
        }

        // Method used to Swap and Redraw 2 rectangles
        private void Swap(int i, int j)
        {
            int temp = Array[i];
            Array[i] = Array[j];
            Array[j] = temp;
            DrawBar(i);
            DrawBar(j);
        }

        // Method used for the redraw a rectangle
        private void DrawBar(int position)
        {
            Obj.DrawRectangle(bluePen, position * 10, 0, 1, maxVal);
            Obj.DrawRectangle(whitePen, position * 10, maxVal - Array[position], 1, maxVal);
        }
        #endregion


    }
}
