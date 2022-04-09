using System;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
    class Program
    {
        private int bulls;
        private int cows;

        private List<int[]> allList;

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Start();
        }

        private void Start()
        {
            allList = CreateListOfValues();
            Random random = new Random();
            List<int[]> usedArrays = new List<int[]>();
            while (bulls != 4)
            {
                int[] arr = new int[4];
                if (allList.Count > 0)
                {
                    arr = allList[random.Next((allList.Count - 1))];
                }
                else
                {
                    arr = allList[0];
                }

                usedArrays.Add(arr);
                allList.Remove(arr);
                PrintSingleArr(arr);
                AskAboutBulls();
                if (bulls == 4)
                {
                    Console.Write("Я выйграл! Ваша последовательность чисел: ");
                    PrintSingleArr(arr);
                    Console.WriteLine("Нажмите любую кнопку");
                    Console.ReadKey();
                    break;
                }

                AskAboutCows();


                if (bulls + cows == 0)
                    allList = AllListAdjustmentIfZero(arr);
                else
                    allList = AllListAdjustmentByBullsAndCows(arr);
                  
                //PrintList(allList);
                //Console.ReadKey();
                Console.Clear();
                PrintList(usedArrays);
                Console.WriteLine("____________________________________");
            }
        }

        private List<int[]> CreateListOfValues()
        {
            List<int[]> list = new List<int[]>();
            int value = 123;

            for (int i = 0; i < 9877; i++)
            {
                int x = value;
                int[] arr = new int[4];
                for (int j = arr.Length - 1; j >= 0; j--)
                {
                    arr[j] = x % 10;
                    x /= 10;
                }

                value++;
                if (ChekArr(arr))
                {
                    list.Add(arr);
                }
            }

            return list;
        }

        private bool ChekArr(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[i] == arr[j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void PrintList(List<int[]> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write($"{(i + 1)}: ");
                foreach (var val in list[i])
                {
                    Console.Write($"{val}");
                }

                Console.WriteLine();
            }
        }

        private void PrintSingleArr(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                Console.Write(arr[i] + ", ");
            }

            Console.WriteLine(arr[arr.Length - 1]);
        }

        private void AskAboutBulls()
        {
            Console.WriteLine("Сколько быков в этой комбинации?");

            bool sucsess = int.TryParse(Console.ReadLine(), out bulls);

            if (!sucsess || bulls > 4)
            {
                Console.WriteLine("Вы ввели некорректное число. Пожалуйста, повнимательней!");
                AskAboutBulls();
            }
        }

        private void AskAboutCows()
        {
            Console.WriteLine("Сколько коров в этой комбинации?");

            bool sucsess = int.TryParse(Console.ReadLine(), out cows);

            if (!sucsess || cows > (4 - bulls))
            {
                Console.WriteLine("Вы ввели некорректное число. Пожалуйста, повнимательней!");
                AskAboutCows();
            }
        }


        private List<int[]> AllListAdjustmentIfZero(int[] arr)
        {
            List<int[]> ajustmentList = new List<int[]>();

            for (int i = 0; i < allList.Count; i++)
            {
                int[] tempArr = allList[i];
                int coincidence = 0;

                for (int j = 0; j < arr.Length; j++)
                {
                    if (tempArr.Contains(arr[j]))
                    {
                        coincidence++;
                        break;
                    }
                }

                if (coincidence == 0)
                {
                    ajustmentList.Add(tempArr);
                }
            }

            return ajustmentList;
        }

        private List<int[]> AllListAdjustmentByBullsAndCows(int[] arr)
        {
            List<int[]> ajustmentList = new List<int[]>();

            for (int i = 0; i < allList.Count; i++)
            {
                List<int> bullsCoincidenceList = new List<int>();
                int[] tempArr = allList[i];
                int bullsCoincidence = 0;
                int cowsCoincidence = 0;


                for (int j = 0; j < arr.Length; j++)
                {
                    if (tempArr[j] == arr[j])
                    {
                        bullsCoincidence++;
                        bullsCoincidenceList.Add(arr[j]);
                        if (bullsCoincidence == bulls)
                        {
                            break;
                        }
                    }
                }


                if (bullsCoincidence == bulls)
                {
                    for (int j = 0; j < arr.Length; j++)
                    {
                        int temp = arr[j];

                        if (!bullsCoincidenceList.Contains(temp) && tempArr.Contains(temp))
                        {
                            cowsCoincidence++;
                        }
                    }
                }

                if (bullsCoincidence == bulls && cowsCoincidence == cows)
                    ajustmentList.Add(tempArr);
            }

            return ajustmentList;
        }
    }
}