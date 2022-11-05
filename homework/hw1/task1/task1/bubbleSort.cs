namespace task1
{
    public static class BubbleSort
    {
        public static IEnumerable<int> Sort(int[] sortedArray)
        {
            for (var i = 0; i < sortedArray.Length; i++) {
                for (var j = 0; j < sortedArray.Length - 1; j++) {
                    if (sortedArray[j] > sortedArray[j + 1]) {
                        (sortedArray[j + 1], sortedArray[j]) = (sortedArray[j], sortedArray[j + 1]);
                    }
                }
            }

            return sortedArray;
        }
    }
}
