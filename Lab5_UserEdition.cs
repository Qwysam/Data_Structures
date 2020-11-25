using System;
using System.Collections.Generic;

namespace Hashing
{
    class Program
    {
        public static int[] GenerateArray(int size, int min, int max)
        {
            Random r = new Random();
            int[] arr = new int[size];
            for (int i = 0; i < size; i++)
                arr[i] = r.Next(min, max);
            return arr;
        }

        void PrintDictionary(ref Dictionary <int, int> dictionary)
        {
            foreach (KeyValuePair<int, int> kvp in dictionary)
            {
                Console.WriteLine("Ключ = {0},\tЗначение = {1}", kvp.Key, kvp.Value);
            }
        }

        void LinearProbing(ref Dictionary<int, int> dictionary, int value)
        {
            int access_count = 1;
            int k = value % 79;
            if (dictionary.ContainsKey(k))
            {
                int tmp = k;
                while (dictionary.ContainsKey(k))
                {
                    access_count++;
                    k = (tmp + 1) % 79;
                    tmp++;
                }
            }
                dictionary.Add(k, value);
            Console.WriteLine($"Значение добавлено по индексу {k}");
            Console.WriteLine($"Обращение к таблице было совершено {access_count} раз(а)");
        }

        void LinearProbingSearch(ref Dictionary<int, int> dictionary, int value)
        {
            int access_count = 1;
            int k = value % 79;
            if (dictionary.ContainsKey(k)&&dictionary[k]==value)
            {
                Console.WriteLine($"Значение найдено по индексу {k}");
            }
            if (dictionary.ContainsKey(k) && dictionary[k] != value)
            {
                int tmp = k;
                while (dictionary.ContainsKey(k))
                {
                    access_count++;
                    if (dictionary[k] == value)
                        break;
                    k = (tmp + 1) % 79;
                    tmp++;

                }
                if (dictionary[k] == value)
                    Console.WriteLine($"Значение найдено по индексу {k}");
            }
            if (!dictionary.ContainsKey(k))
            {
                Console.WriteLine("Значение отсутствует в таблице ");
            }
            Console.WriteLine($"Обращение к таблице было совершено {access_count} раз(а)");
        }

        void SquareProbing(ref Dictionary<int, int> dictionary, int value)
        {
            int access_count = 1;
            int k = value % 79;
            if (dictionary.ContainsKey(k))
            {
                int i = 1;
                while (dictionary.ContainsKey(k))
                {
                    access_count++;
                    k = (k + i * i) % 79;
                    i++;
                }
            }
            dictionary.Add(k, value);
            Console.WriteLine($"Значение добавлено по индексу {k}");
            Console.WriteLine($"Обращение к таблице было совершено {access_count} раз(а)");
        }

        void SquareProbingSearch(ref Dictionary<int, int> dictionary, int value)
        {
            int access_count = 1;
            int k = value % 79;
            if (dictionary.ContainsKey(k) && dictionary[k] == value)
            {
                Console.WriteLine($"Значение найдено по индексу {k}");
            }
            if (dictionary.ContainsKey(k) && dictionary[k] != value)
            {
                int i = 1;
                while (dictionary.ContainsKey(k))
                {
                    access_count++;
                    if (dictionary[k] == value)
                        break;
                    k = (k + i * i) % 79;
                    i++;

                }
                if (dictionary[k] == value)
                    Console.WriteLine($"Значение найдено по индексу {k}");
            }
            if (!dictionary.ContainsKey(k))
            {
                Console.WriteLine("Значение отсутствует в таблице ");
            }
            Console.WriteLine($"Обращение к таблице было совершено {access_count} раз(а)");
        }

        void DualArguments(ref Dictionary<int, int> dictionary, int value)
        {
            int access_count = 1;
            int k = value % 79;
            int tmp = k;
            if (dictionary.ContainsKey(k))
            {
                int i = 1;
                while (dictionary.ContainsKey(tmp))
                {
                    access_count++;
                    tmp = k;
                    tmp = (tmp + i) % 79;
                    k = tmp + i;
                    i++;
                }
            }
            dictionary.Add(tmp, value);
            Console.WriteLine($"Значение добавлено по индексу {tmp}");
            Console.WriteLine($"Обращение к таблице было совершено {access_count} раз(а)");
        }

        void DualArgumentSearch(ref Dictionary<int, int> dictionary, int value)
        {
            int access_count = 1;
            int k = value % 79;
            int tmp = k;
            if (dictionary.ContainsKey(k) && dictionary[k] == value)
            {
                Console.WriteLine($"Значение найдено по индексу {k}");
            }
            if (dictionary.ContainsKey(k) && dictionary[k] != value)
            {
                int i = 1;
                while (dictionary.ContainsKey(k))
                {
                    access_count++;
                    if (dictionary[k] == value)
                        break;
                    tmp = k;
                    tmp = (tmp + i) % 79;
                    k = tmp + i;
                    i++;

                }
                if (dictionary[k] == value)
                    Console.WriteLine($"Значение найдено по индексу {k}");
            }
            if (!dictionary.ContainsKey(k))
            {
                Console.WriteLine("Значение отсутствует в таблице ");
            }
            Console.WriteLine($"Обращение к таблице было совершено {access_count} раз(а)");
        }

        void Hash(ref Dictionary<int,int> dictionary,int option, int value)
        {
            if (dictionary.Count > 79)
                throw new IndexOutOfRangeException("Table can not have more than 79 elements");
            else {
                if (option == 1)
                    LinearProbing(ref dictionary, value);
                if (option == 2)
                    SquareProbing(ref dictionary, value);
                if (option == 3)
                    DualArguments(ref dictionary, value);
            }

        }

        void PrintMenu()
        {
            Console.WriteLine("Ввод выполняется на английском");
            Console.WriteLine("Команды: ");
            Console.WriteLine("'e' - выход                           'p' - вывод таблицы");
            Console.WriteLine("'c' - смена метода открытой адресации 'i' - ввод числа");
            Console.WriteLine("'f' - поиск числа                     'g' - заполнение таблицы случайными числами");
            Console.WriteLine("Введите команду: ");
        }
        void HandleInput(string input, ref Dictionary<int, int> dictionary, ref int option)
        {
            if (input == "p")
                PrintDictionary(ref dictionary);
            if(input == "c")
            {
                Console.WriteLine("Методы открытой адресации:");
                Console.WriteLine("1 - метод линейных проб\n2 - метод квадратичных проб\n5 - метод двух аргументов");
                Console.WriteLine("Выберите метод открытой адресации:");
                int.TryParse(Console.ReadLine(), out option);
            }
            if(input == "i")
            {
                if (dictionary.Count > 79)
                    Console.WriteLine("Максимальное количество элеементов в таблице");
                else
                {
                    int value;
                    Console.WriteLine("Введите число: ");
                    int.TryParse(Console.ReadLine(), out value);
                    Hash(ref dictionary, option, value);
                }
            }
            if (input == "f")
            {
                int value;
                Console.WriteLine("Введите число: ");
                int.TryParse(Console.ReadLine(), out value);
                if (option == 1)
                    LinearProbingSearch(ref dictionary, value);
                if (option == 2)
                    SquareProbingSearch(ref dictionary, value);
                if (option == 3)
                    DualArgumentSearch(ref dictionary, value);
            }

            if (input == "g")
            {
                Console.WriteLine("Введите процент заполнения таблицы: ");
                int percentage;
                int.TryParse(Console.ReadLine(), out percentage);
                int size = (79 * percentage) / 100;
                if (size == 0)
                    size ++;
                int[] randarr = GenerateArray(size, 1000, 3000);
                for (int i = 0; i < randarr.Length; i++)
                {
                    Hash(ref dictionary, option, randarr[i]);
                    Console.Clear();
                }
                Console.Clear();
                PrintMenu();
                string crutch = Console.ReadLine();
                HandleInput(crutch, ref dictionary, ref option);
            }
        }

        static void Main(string[] args)
        {
            //hashing = int % N (N - size of table)
            Program p = new Program();
            int option;
            Dictionary<int, int> hash = new Dictionary<int, int>();
            Console.WriteLine("Методы открытой адресации:");
            Console.WriteLine("1 - метод линейных проб\n2 - метод квадратичных проб\n3 - метод двух аргументов");
            Console.WriteLine("Выберите метод открытой адресации:");
            int.TryParse(Console.ReadLine(), out option);
            for (; ; )
            {
                string input;
                p.PrintMenu();
                input = Console.ReadLine();
                if (input == "e")
                    break;
                else
                    p.HandleInput(input, ref hash, ref option);
            }
        }
    }
}
