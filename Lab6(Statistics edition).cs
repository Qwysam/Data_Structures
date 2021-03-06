﻿using System;
using System.Diagnostics;
using System.Linq;

namespace SubstringSearch
{
    class Program
    {
        public class StringMatcher
        {
            public int NaiveStringMatching(string str, string substring)
            {
                int index = -1;
                if (substring.Length > str.Length)
                    return index;
                else
                {
                    for (int i = 0; i <= str.Length - substring.Length; i++)
                        if (str[i] == substring[0])
                        {
                            for (int j = 0; j < substring.Length; j++)
                            {
                                if (str[j + i] != substring[j])
                                    break;
                                if (j == substring.Length - 1)
                                    index = i;
                            }
                        }
                    return index;
                }
            }

            public int RK_Search(string text, string pattern)
            {
                //number of chars in text alphabet(ASCII)
                int d = 256;
                //number of chars in pattern alphabet(Uppercase and Lowercase latin letters)
                int q = 256;
                int M = pattern.Length;
                int N = text.Length;
                int i, j;
                int p = 0; // hash value for pattern  
                int t = 0; // hash value for text  
                int h = 1;

                // The value of h would be "pow(d, M-1)%q"  
                for (i = 0; i < M - 1; i++)
                    h = (h * d) % q;

                // Calculate the hash value of pattern and first  
                // window of text  
                for (i = 0; i < M; i++)
                {
                    p = (d * p + pattern[i]) % q;
                    t = (d * t + text[i]) % q;
                }

                // Slide the pattern over text one by one  
                for (i = 0; i <= N - M; i++)
                {

                    // Check the hash values of current window of text  
                    // and pattern. If the hash values match then only  
                    // check for characters on by one  
                    if (p == t)
                    {
                        /* Check for characters one by one */
                        for (j = 0; j < M; j++)
                        {
                            if (text[i + j] != pattern[j])
                                break;
                        }

                        // if p == t and pat[0...M-1] = text[i, i+1, ...i+M-1]  
                        if (j == M)
                            return i;
                    }

                    // Calculate hash value for next window of text: Remove  
                    // leading digit, add trailing digit  
                    if (i < N - M)
                    {
                        t = (d * (t - text[i] * h) + text[i + M]) % q;

                        // We might get negative value of t, converting it  
                        // to positive  
                        if (t < 0)
                            t = (t + q);
                    }
                }
                return -1;
            }

            //A utility function to get maximum of two integers  
            static int max(int a, int b) { return (a > b) ? a : b; }

            //The preprocessing function for Boyer Moore's  
            //bad character heuristic  
            static void badCharHeuristic(char[] str, int size, int[] badchar)
            {
                int i;

                // Initialize all occurrences as -1  
                for (i = 0; i < 256; i++)
                    badchar[i] = -1;

                // Fill the actual value of last occurrence  
                // of a character  
                for (i = 0; i < size; i++)
                    badchar[(int)str[i]] = i;
            }

            /* A pattern searching function that uses Bad  
            Character Heuristic of Boyer Moore Algorithm */
            public int BM_Search(char[] txt, char[] pat)
            {
                int m = pat.Length;
                int n = txt.Length;

                int[] badchar = new int[256];

                /* Fill the bad character array by calling  
                    the preprocessing function badCharHeuristic()  
                    for given pattern */
                badCharHeuristic(pat, m, badchar);

                int s = 0; // s is shift of the pattern with  
                           // respect to text  
                while (s <= (n - m))
                {
                    int j = m - 1;

                    /* Keep reducing index j of pattern while  
                        characters of pattern and text are  
                        matching at this shift s */
                    while (j >= 0 && pat[j] == txt[s + j])
                        j--;

                    /* If the pattern is present at current  
                        shift, then index j will become -1 after  
                        the above loop */
                    if (j < 0)
                    {
                        return s;

                        /* Shift the pattern so that the next  
                            character in text aligns with the last  
                            occurrence of it in pattern.  
                            The condition s+m < n is necessary for  
                            the case when pattern occurs at the end  
                            of text */
                        s += (s + m < n) ? m - badchar[txt[s + m]] : 1;

                    }

                    else
                        /* Shift the pattern so that the bad character  
                            in text aligns with the last occurrence of  
                            it in pattern. The max function is used to  
                            make sure that we get a positive shift.  
                            We may get a negative shift if the last  
                            occurrence of bad character in pattern  
                            is on the right side of the current  
                            character. */
                        s += max(1, j - badchar[txt[s + j]]);
                }
                return -1;
            }
        }

        static void PrintMenu()
        {
            //Console.WriteLine("Ввод выполняется на английском");
            Console.WriteLine("Команды: ");
            Console.WriteLine("'1' - Прямой поиск                    '2' - РК поиск");
            Console.WriteLine("'3' - БМ поиск                        '4' - встроенный метод C#");
            Console.WriteLine("Введите команду: ");
        }

        static void HandleInput(string text, string pattern, string option)
        {
            StringMatcher s = new StringMatcher();

            if (option == "1")
            {
                int index = s.NaiveStringMatching(text, pattern);
            }

            if (option == "2")
            {
                int index = s.RK_Search(text, pattern);
            }

            if (option == "3")
            {
                int index = s.BM_Search(text.ToCharArray(), pattern.ToCharArray());
            }

            if (option == "4")
            {
                int index = text.IndexOf(pattern);
            }

        }

        static string GenerateString(int size)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, size)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        static string SubString(string text, int size)
        {
            Random rand = new Random();
            int index = rand.Next(0, text.Length - size);
            return text.Substring(index, size);
        }

        static void Main(string[] args)
        {
            string text, pattern;
            Stopwatch sw = new Stopwatch();
            TimeSpan average = new TimeSpan();
            for (int i = 0;i<100 ;i++ )
            {
                text = GenerateString(5000);
                pattern = SubString(text, 5);
                sw.Start();
                HandleInput(text, pattern, "2");
                sw.Stop();
                average += sw.Elapsed;
                sw.Reset();
            }
            Console.WriteLine("Время выполнения : {0}", average.TotalMilliseconds/100);
        }
    }
}