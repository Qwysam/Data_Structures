using System;
using System.Collections;
using System.Linq;

namespace Data_Structures
{
    class Program
    {
        static int InterpolationSearch(int[] arr, int key,out int Sr,out int Pr)
        {
            // Find indexes of 
            // two corners
            int lo = 0, hi = (arr.Length - 1);
            Pr = 2;
            Sr = 0;
            // Since array is sorted,  
            // an element present in 
            // array must be in range 
            // defined by corner
            Sr += 3;
            while (lo <= hi &&key >= arr[lo] && key <= arr[hi])
            {
                Sr++;
                if (lo == hi)
                {
                    Sr++;
                    if (arr[lo] == key) return lo;
                    return -1;
                }

                // Probing the iition  
                // with keeping uniform  
                // distribution in mind.
                double k = (double)(key - arr[lo]) / (arr[hi] - arr[lo]);
                Pr++;
                int i = (int)(lo + (hi-lo)*k);

                Pr++;
                // Condition of  
                // target found
                Sr++;
                if (arr[i] == key)
                    return i;
                Sr++;
                // If x is larger, x 
                // is in upper part 
                if (arr[i] < key)
                    lo = i + 1;

                // If x is smaller, x  
                // is in the lower part 
                else {
                    hi = i - 1;
                    Pr++;
                }
            }
            return -1;
        }

        static int BinarySearch(int[] arr,int key,out int Sr, out int Pr)
        {
            int min = 0;
            int max = arr.Length - 1;
            Pr = 2;
            Sr = 0;
            while (min <= max)
            {
                Sr++;
                int mid = (min + max) / 2;
                Pr++;
                Sr++;
                if (key == arr[mid])
                {
                    return mid;
                }
                else {
                    if (key < arr[mid])
                    {
                        max = mid - 1;
                    }
                    else
                    {
                        min = mid + 1;
                    }
                    Sr++;
                    Pr++;
                }
            }
            return -1;
        }
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

        //Generates an array using the parameters set by the user
        public static int[] GenerateArray(int size, int min, int max)
        {
            Random r = new Random();
            int[] arr = new int[size];
            for (int i = 0; i < size; i++)
                arr[i] = r.Next(min, max);
            return arr;
        }

        static void Main(string[] args)
        {
            int size, search_method, generate,key;
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
                {
                    //array input
                    array = ArrayIn(size);
                }
                else
                {
                    //User input of max and min values
                    int min = 100, max = 900;
                    //Console.WriteLine("Input minimal generated number: ");
                    //int.TryParse(Console.ReadLine(), out min);
                    //Console.WriteLine("Input maximal generated number: ");
                    //int.TryParse(Console.ReadLine(), out max);
                    array = GenerateArray(size, min, max);
                    ArrayOut(array);
                }
                int[] copy = new int[array.Length];
                Array.Copy(array, copy, array.Length);
                Array.Sort(copy);
                if (size <= 10)
                {
                    Console.WriteLine("Array after sorting:");
                    ArrayOut(copy);
                }
                Console.WriteLine("Input number to search for: ");
                int.TryParse(Console.ReadLine(), out key);
                //User input of search method
                Console.WriteLine("Input '1' to use Binary Search, '2' for Interpolation Search, '3' for C# default search");
                int.TryParse(Console.ReadLine(), out search_method);
                //Array output if it has ten or less elements
                //Choosing sorting method based on user input
                if (search_method == 1)
                {
                    int Sr, Pr;
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    int i = BinarySearch(copy, key,out Sr,out Pr);
                    watch.Stop();
                    Console.WriteLine($"i = {i}, execution time = "+ watch.Elapsed);
                    Console.WriteLine($"Compasrisons = {Sr}\tAssignments = {Pr}\t\tSum = {Sr+Pr}");
                }
                if(search_method == 2)
                {
                    int Sr, Pr;
                    copy = copy.Distinct().ToArray();
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    int i = InterpolationSearch(copy,key,out Sr,out Pr);
                    watch.Stop();
                    Console.WriteLine($"i = {i}, execution time = " + watch.Elapsed);
                    Console.WriteLine($"Compasrisons = {Sr}\tAssignments = {Pr}\t\tSum = {Sr + Pr}");
                }
                if(search_method == 3)
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    int i = Array.IndexOf(copy,key);
                    watch.Stop();
                    Console.WriteLine($"i = {i}, execution time = " + watch.Elapsed);
                }
            }
        }
    }
}
