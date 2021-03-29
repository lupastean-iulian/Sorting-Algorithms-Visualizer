namespace SortingAlgorithmVisualizer
{
    interface ISortEngine
    {
        //Method called by the background worker to begin the sorting process
        void NextStep();

        //Method to check if the Array is sorted
        bool isSorted();
         
    }
}
