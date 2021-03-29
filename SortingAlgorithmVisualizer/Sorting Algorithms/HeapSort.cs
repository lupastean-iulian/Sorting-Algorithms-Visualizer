using System.Linq;
using System.Drawing;

namespace SortingAlgorithmVisualizer
{
    class HeapSortEngine : ISortEngine
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

        //declaring the HeapSortEngine constructor   
        public HeapSortEngine(int[] inArray, Graphics inObj, int inmaxVal)
        {
            Array = inArray;
            Obj = inObj;
            maxVal = inmaxVal;
        }
         
        public void NextStep()
        {
            HeapSort(Array, Array.Count());
        }

        #region Sorting Methods

        private void HeapSort(int[] arr, int n)
        {
            for (int i = n / 2 - 1; i >= 0; i--)
                heapify(arr, n, i);
            for (int i = n - 1; i >= 0; i--)
            {
                Swap(i, 0);
                heapify(arr, i, 0);
            }
        }

        private void heapify(int[] arr, int n, int x)
        {
            int largest = x;
            int left = 2 * x + 1;
            int right = 2 * x + 2;
            if (left < n && arr[left] > arr[largest])
                largest = left;
            if (right < n && arr[right] > arr[largest])
                largest = right;
            if (largest != x)
            {
                Swap(x, largest);
                heapify(arr, n, largest);
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