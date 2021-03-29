using System.Linq;
using System.Drawing;
namespace SortingAlgorithmVisualizer
{
    class CycleSortEngine : ISortEngine
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

        //declaring the CycleSortEngine constructor 
        public CycleSortEngine(int[] inArray, Graphics inObj, int inmaxVal)
        {
            Array = inArray;
            Obj = inObj;
            maxVal = inmaxVal;
        }
    
        public void NextStep()
        {
            CycleSort(Array, Array.Count());
        }

        #region CycleSort Methods
        private void CycleSort(int[] arr, int n)
        {
            // count number of memory writes
            int writes = 0;

            // traverse array elements and
            // put it to on the right place
            for (int cycle_start = 0; cycle_start <= n - 2; cycle_start++)
            {
                // initialize item as starting point
                int item = arr[cycle_start];

                // Find position where we put the item.
                // We basically count all smaller elements
                // on right side of item.
                int pos = cycle_start;
                for (int i = cycle_start + 1; i < n; i++)
                    if (arr[i] < item)
                        pos++;

                // If item is already in correct position
                if (pos == cycle_start)
                    continue;

                // ignore all duplicate elements
                while (item == arr[pos])
                    pos += 1;

                // put the item to it's right position
                if (pos != cycle_start)
                {
                    int temp = item;
                    item = arr[pos];
                    arr[pos] = temp;
                    DrawBar(pos);
                    writes++;
                }

                // Rotate rest of the cycle
                while (pos != cycle_start)
                {
                    pos = cycle_start;

                    // Find position where we put the element
                    for (int i = cycle_start + 1; i < n; i++)
                        if (arr[i] < item)
                            pos += 1;

                    // ignore all duplicate elements
                    while (item == arr[pos])
                        pos += 1;

                    // put the item to it's right position
                    if (item != arr[pos])
                    {
                        int temp = item;
                        item = arr[pos];
                        arr[pos] = temp;
                        DrawBar(pos);
                        writes++;
                    }
                }
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
