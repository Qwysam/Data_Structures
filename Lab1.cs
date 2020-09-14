using System;

namespace Practise
{
    class Program
    {
        //User input to array
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
        //Output array
        static void ArrayOut(int[] arr)
        {
            foreach(int elem in arr)
            {
                Console.Write($"{elem} ");
            }
            Console.WriteLine();
        }
        //Sorts the array using Bubble Sort algorithm
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
        //Sorts the array using Insertion Sort algorithm
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
        //Uses both sorting algorithms
        static void Both(int[] arr)
        {
            Console.WriteLine("\nBuble Sort\n");
            BubbleSort(arr);
            Console.WriteLine("\nInsertion Sort\n");
            InsertionSort(arr);
        }

        //Generates an array using the parameters set by the user
        public static int[] GenerateArray(int size,int quality,int min, int max)
        {
            Random r = new Random();
            int[] arr = new int[size];
            for(int i = 0;i<size;i++)
                arr[i] = r.Next(min, max);
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
            //main program cycle
            for (; ; )
            {
                //User input of array size
                Console.WriteLine("Input array size:");
                string exit = Console.ReadLine();
                int.TryParse(exit, out size);
                //Checks the input for exit command
                if (exit == "e")
                        break;
                int[] array = new int[size];
                //User input of input type
                Console.WriteLine("Input '0' for manual input or '1' to generate array");
                int.TryParse(Console.ReadLine(), out generate);
                if (generate == 0)
                    //array input
                    array = ArrayIn(size);
                else
                {
                    //User input of max and min values
                    int min, max;
                    Console.WriteLine("Input minimal generated number: ");
                    int.TryParse(Console.ReadLine(), out min);
                    Console.WriteLine("Input maximal generated number: ");
                    int.TryParse(Console.ReadLine(), out max);
                    //User input of array quality
                    Console.WriteLine("Input '0' to generate unsorted array, '1' for sorted, '2' for sorted reversed");
                    int.TryParse(Console.ReadLine(), out quality);
                    //array generation
                    array = GenerateArray(size, quality,min,max);
                }
                //User input of sorting method
                Console.WriteLine("Input '1' to use Bubble Sort, '2' for Insertion Sort, '3' for both");
                int.TryParse(Console.ReadLine(), out sort_method);
                //Array output if it has ten or less elements
                if (size <= 10)
                {
                    Console.WriteLine("Array:");
                    ArrayOut(array);
                }
                //Choosing sorting method based on user input
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
