using System.Linq;
using System.Drawing;

namespace SortingAlgorithmVisualizer
{
    class MergeSortEngine: ISortEngine
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

        //declaring the MergeSortEngine constructor 
        public MergeSortEngine(int[] inArray, Graphics inObj, int inmaxVal)
        {
            Array = inArray;
            Obj = inObj;
            maxVal = inmaxVal;
        }
      
        public void NextStep()
        {
            MergeSort(Array, 0, Array.Count() - 1);
        }

        #region Sorting Methods

        private void MergeSort(int[] arr, int p, int r)
        {
            if (p < r)
            {
                int q = (p + r) / 2;
                MergeSort(arr, p, q);
                MergeSort(arr, q + 1, r);
                merge(arr, p, q, r);
            }
        }

        private void merge(int[] arr, int p, int q, int r)
        {
            int i, j, k;
            int n1 = q - p + 1;
            int n2 = r - q;
            int[] L = new int[n1];
            int[] R = new int[n2];
            for (i = 0; i < n1; i++)
            {
                L[i] = arr[p + i];
            }
            for (j = 0; j < n2; j++)
            {
                R[j] = arr[q + 1 + j];
            }
            i = 0;
            j = 0;
            k = p;
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    arr[k] = L[i];
                    DrawBar(k);
                    i++;
                }
                else
                {
                    arr[k] = R[j];
                    DrawBar(k);
                    j++;
                }
                k++;
            }
            while (i < n1)
            {
                arr[k] = L[i];
                DrawBar(k);
                i++;
                k++;
            }
            while (j < n2)
            {
                arr[k] = R[j];
                DrawBar(k);
                j++;
                k++;
            }
        }
        #endregion

        #region Functionality Methods for: isSorted(), Draw rectangles 
        // Method used to check if the Array is sorted or not
        public bool isSorted()
        {
            for (int i = 0; i < Array.Count() - 1; i++)
            {
                if (Array[i] > Array[i + 1]) return false;
            }
            return true;
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
