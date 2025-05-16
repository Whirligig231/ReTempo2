namespace Retempo2
{
    public static class BinarySearch
    {
        // Returns the index of the closest element to the given value in an array, by binary searching
        public static int Closest(float[] array, float value)
        {
            int indexA = 0, indexB = array.Length - 1;
            if (array.Length == 0)
                return -1;
            if (array[indexA] >= value)
                return indexA;
            if (array[indexB] <= value)
                return indexB;
            while (indexB - indexA > 1)
            {
                int indexC = (indexA + indexB) / 2;
                if (array[indexC] > value)
                    indexB = indexC;
                else
                    indexA = indexC;
            }
            float diffA = value - array[indexA];
            float diffB = array[indexB] - value;
            if (diffA <= diffB)
                return indexA;
            return indexB;
        }
    }
}
