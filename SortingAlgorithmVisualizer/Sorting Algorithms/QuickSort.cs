using System.Drawing;
using System.Linq;

namespace SortingAlgorithmVisualizer
{
    class QuickSortEngine : ISortEngine
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

        //declaring the QuickSortEngine constructor  
        public QuickSortEngine(int[] inArray, Graphics inObj, int inmaxVal)
        {
            Array = inArray;
            Obj = inObj;
            maxVal = inmaxVal;
        }
        
        public void NextStep()
        {
            QuickSort(Array,0,Array.Count() - 1);
        }

        #region Sorting Methods
        private void QuickSort(int[] arr, int left, int right)
        {
            int pivot;
            if (left < right)
            {
                pivot = Partition(arr, left, right);
                QuickSort(arr, left, pivot-1);
                QuickSort(arr, pivot + 1, right);  
            }

        }
        private int Partition(int[] arr, int left, int right)
        {
            int pivot = arr[right];
            int pindex = left;
            for (int i = left; i < right; i++)
            {
                if (arr[i] <= pivot)
                {
                   Swap(i,pindex);
                   pindex++;
                }
            }
            Swap(right,pindex);
            return pindex;
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
