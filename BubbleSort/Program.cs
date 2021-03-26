using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubbleSort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 11, 93, 45, 98, 13, 55 };

            var data = bubbleSort(arr);

            Console.WriteLine("Sorted Data => " + string.Join(",", data));
            Console.ReadLine();
        }

        public static int[] bubbleSort(int[] arr, int startIndex = 1)
        {
            int arrLength = arr.Length;
            if (startIndex < arrLength)
            {
                arr = sortArray(arr, arrLength - 1);

                startIndex += 1;
                bubbleSort(arr, startIndex);
            }
            return arr;
        }

        public static int[] sortArray(int[] arr, int index)
        {
            int value;
            if (index >= 1)
            {
                if (arr[index - 1] > arr[index])
                {
                    value = arr[index];
                    arr[index] = arr[index - 1];
                    arr[index - 1] = value;
                }
                index -= 1;
                sortArray(arr, index);
            }
            return arr;
        }
    }
}
