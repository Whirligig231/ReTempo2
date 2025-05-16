namespace Retempo2
{
    public static class BinarySearch
    {
        // Returns the index of the closest element to the given value in an array, by binary searching
        public static int Closest(List<float> list, float value)
        {
            int indexA = 0, indexB = list.Count - 1;
            if (list.Count == 0)
                return -1;
            if (list[indexA] >= value)
                return indexA;
            if (list[indexB] <= value)
                return indexB;
            while (indexB - indexA > 1)
            {
                int indexC = (indexA + indexB) / 2;
                if (list[indexC] > value)
                    indexB = indexC;
                else
                    indexA = indexC;
            }
            float diffA = value - list[indexA];
            float diffB = list[indexB] - value;
            if (diffA <= diffB)
                return indexA;
            return indexB;
        }
    }
}
