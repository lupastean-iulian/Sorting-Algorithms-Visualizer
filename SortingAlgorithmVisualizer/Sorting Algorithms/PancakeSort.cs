using System.Linq;
using System.Drawing;

namespace SortingAlgorithmVisualizer
{
    class PancakeSortEngine : ISortEngine
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

        public PancakeSortEngine(int[] inArray, Graphics inObj, int inmaxVal)
        {
            Array = inArray;
            Obj = inObj;
            maxVal = inmaxVal;
        }

        public void NextStep()
        {
            PancakeSort(Array, Array.Count());
        }

        #region Sorting Methods
        private void PancakeSort(int[] arr, int n)
        {
            // Start from the complete
            // array and one by one
            // reduce current size by one
            for (int curr_size = n; curr_size > 1;--curr_size)
            {

                // Find index of the
                // maximum element in
                // arr[0..curr_size-1]
                int mi = findMax(arr, curr_size);

                // Move the maximum element
                // to end of current array
                // if it's not already at
                // the end
                if (mi != curr_size - 1)
                {
                    // To move at the end,
                    // first move maximum
                    // number to beginning
                    flip(arr, mi);

                    // Now move the maximum
                    // number to end by
                    // reversing current array
                    flip(arr, curr_size - 1);
                }
            }
        }

        private void flip(int []arr,int i)
        {
            int start = 0;
            while (start < i)
            {
                Swap(start, i);
                start++;
                i--;
            }
        }

        private int findMax(int []arr , int n)
        {
            int mi, i;
            for (mi = 0, i = 0; i < n; ++i)
                if (arr[i] > arr[mi])
                    mi = i;

            return mi;
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
