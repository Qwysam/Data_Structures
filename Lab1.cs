using System;

namespace Practise
{
    class Program
    {
        static int[] ArrayIn(int size)
        {
            int[] arr = new int[size];
            for(int i = 0; i < size; i++)
            {
                Console.WriteLine($"Input a[{i}]: ");
                int.TryParse(Console.ReadLine(), out arr[i]);
            }
            return arr;

        }
        static void ArrayOut(int[] arr)
        {
            foreach(int elem in arr)
            {
                Console.Write($"{elem} ");
            }
            Console.WriteLine();
        }
        static void BubbleSort(int[] arr)
        {
            int temp, comparisons = (arr.Length*(arr.Length-1))/2, exchanges = 0;
            for (int j = 0; j < arr.Length - 1; j++)
            {
                for (int i = 0; i <arr.Length - 1; i++)
                {
                    if (arr[i] > arr[i + 1])
                    {
                        temp = arr[i + 1];
                        arr[i + 1] = arr[i];
                        arr[i] = temp;
                        exchanges++;
                    }
                }
            }
            Console.WriteLine("Sorted array:");
            ArrayOut(arr);
            Console.WriteLine($"{comparisons} comparisons and {exchanges} exchanges have been made");
            Console.WriteLine($"{comparisons + exchanges} operations in total");
        }

        static void InsertionSort(int[] arr)
        {
            int comparisons = 0, exchanges = 0;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    comparisons++;
                    if (arr[j - 1] > arr[j])
                    {
                        exchanges++;
                        int temp = arr[j - 1];
                        arr[j - 1] = arr[j];
                        arr[j] = temp;
                    }
                }
            }
            Console.WriteLine("Sorted array:");
            ArrayOut(arr);
            Console.WriteLine($"{comparisons} comparisons and {exchanges} exchanges have been made");
            Console.WriteLine($"{comparisons + exchanges} operations in total");
        }

        static void Both(int[] arr)
        {
            Console.WriteLine("\nBuble Sort\n");
            BubbleSort(arr);
            Console.WriteLine("\nInsertion Sort\n");
            InsertionSort(arr);
        }

        //quality=0 unsorted, =1 sorted, =2 sorted reversed
        public static int[] GenerateArray(int size,int quality)
        {
            Random r = new Random();
            int[] arr = new int[size];
            for(int i = 0;i<size;i++)
                arr[i] = r.Next(100, 300);
            if (quality != 0)
                Array.Sort(arr);
            if (quality == 2)
                Array.Reverse(arr);
            return arr;
        }
        static void Main(string[] args)
        {
            int size, sort_method, quality, generate;
            Console.Write("Input 'e' to exit\n");
            for (; ; )
            {
                Console.WriteLine("Input array size:");
                string exit = Console.ReadLine();
                int.TryParse(exit, out size);
                if (exit == "e")
                        break;
                int[] array = new int[size];
                Console.WriteLine("Input '0' for manual input or '1' to generate array");
                int.TryParse(Console.ReadLine(), out generate);
                if (generate == 0)
                    array = ArrayIn(size);
                else
                {
                    Console.WriteLine("Input '0' to generate unsorted array, '1' for sorted, '2' for sorted reversed");
                    int.TryParse(Console.ReadLine(), out quality);
                    array = GenerateArray(size, quality);
                }
                Console.WriteLine("Input '1' to use Bubble Sort, '2' for Insertion Sort, '3' for both");
                int.TryParse(Console.ReadLine(), out sort_method);
                if (size <= 10)
                {
                    Console.WriteLine("Unsorted array:");
                    ArrayOut(array);
                }
                if (sort_method == 1)
                    BubbleSort(array);
                if (sort_method == 2)
                    InsertionSort(array);
                if (sort_method == 3)
                    Both(array);
            }

        }
    }
}
