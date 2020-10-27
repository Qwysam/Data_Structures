using System;
using System.Collections;
using System.Linq;

namespace Data_Structures
{
    class Program
    {
        static int[] ArrayIn(int size)
        {
            int[] arr = new int[size];
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine($"Input a[{i}]: ");
                int.TryParse(Console.ReadLine(), out arr[i]);
            }
            return arr;

        }

        //Output array
        static void ArrayOut(int[] arr)
        {
            foreach (int elem in arr)
            {
                Console.Write($"{elem} ");
            }
            Console.WriteLine();
        }

        static void CountingSort(int[] arr, int min, int max)
        {
            int[] count = new int[max - min + 1];
            int z = 0;
            int comparisons = arr.Length - 1;
            int exchanges = 0;
            for (int i = 0; i < count.Length; i++)
            {
                count[i] = 0;
            }
            for (int i = 0; i < arr.Length; i++)
            {
                count[arr[i] - min]++;
            }

            for (int i = min; i <= max; i++)
            {
                while (count[i - min]-- > 0)
                {
                    arr[z] = i;
                    z++;
                }
            }
            exchanges = arr.Length + (arr.Max() - 1) + arr.Length * 2;
            double[] temp = IntToDouble(arr);
            Console.WriteLine("Sorted array:");
            ArrayOutDouble(temp);
            Console.WriteLine($"{comparisons} comparisons and {exchanges} exchanges have been made");
            Console.WriteLine($"{comparisons + exchanges} operations in total");
        }

        static void MSDSort(int[] arr)
        {
            int range = 10;
            int length = 2;
            int exchanges = arr.Length;
            int comparisons = 0;
            ArrayList[] lists = new ArrayList[range];
            for (int i = 0; i < range; ++i)
                lists[i] = new ArrayList();

            for (int step = 0; step < length; ++step)
            {
                //распределение по спискам
                for (int i = 0; i < arr.Length; ++i)
                {
                    int temp = (arr[i] % (int)Math.Pow(range, step + 1)) / (int)Math.Pow(range, step);
                    lists[temp].Add(arr[i]);
                    exchanges++;
                }
                //сборка
                int k = 0;
                for (int i = 0; i < range; ++i)
                {
                    for (int j = 0; j < lists[i].Count; ++j)
                    {
                        exchanges++;
                        arr[k++] = (int)lists[i][j];
                    }
                }
                for (int i = 0; i < range; ++i)
                    lists[i].Clear();
            }
            double[] temp_a = IntToDouble(arr);
            Console.WriteLine("Sorted array:");
            ArrayOutDouble(temp_a);
            Console.WriteLine($"{comparisons} comparisons and {exchanges} exchanges have been made");
            Console.WriteLine($"{comparisons + exchanges} operations in total");
        }
        //Uses both sorting algorithms
        static void Both(int[] arr)
        {
            int[] arr_copy = new int[arr.Length];
            Array.Copy(arr, arr_copy, arr.Length);
            Console.WriteLine("\nCount Sort\n");
            CountingSort(arr, arr.Min(), arr.Max());
            Console.WriteLine("\nMSD Sort\n");
            MSDSort(arr_copy);
        }

        //Generates an array using the parameters set by the user
        public static int[] GenerateArray(int size, int quality, int min, int max)
        {
            Random r = new Random();
            int[] arr = new int[size];
            for (int i = 0; i < size; i++)
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
                double[] t = new double[size];
                //User input of input type
                Console.WriteLine("Input '0' for manual input or '1' to generate array");
                int.TryParse(Console.ReadLine(), out generate);
                if (generate == 0)
                {
                    //array input
                    t = ArrayInDouble(size);
                    array = DoubleToInt(t);
                }
                else
                {
                    //User input of max and min values
                    int min = 3, max = 100;
                    //User input of array quality
                    Console.WriteLine("Input '0' to generate unsorted array, '1' for sorted, '2' for sorted reversed");
                    int.TryParse(Console.ReadLine(), out quality);
                    //array generation
                    array = GenerateArray(size, quality, min, max);
                }
                //User input of sorting method
                Console.WriteLine("Input '1' to use Counting Sort, '2' for MSD Sort, '3' for both");
                int.TryParse(Console.ReadLine(), out sort_method);
                //Array output if it has ten or less elements
                if (size <= 10)
                {
                    Console.WriteLine("Array:");
                    double[] temp = IntToDouble(array);
                    ArrayOutDouble(temp);
                }
                //Choosing sorting method based on user input
                if (sort_method == 1)
                    CountingSort(array, array.Min(), array.Max());
                if (sort_method == 2)
                    MSDSort(array);
                if (sort_method == 3)
                    Both(array);
            }
        }
    }
}
