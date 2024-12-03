using System.Diagnostics;

namespace Lab_4
{
    internal class Program
    {
        static void PrintArray(int[] array)
        {
            foreach (int item in array)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
        }

        static void InputRandom(ref int[] array)
        {
            Console.WriteLine("Скільки елементів буде у масиві?");
            int length = int.Parse(Console.ReadLine());

            int minRange;
            int maxRange;
            do
            {
                Console.WriteLine("Введіть мінімальну межу:");
                minRange = int.Parse(Console.ReadLine());

                Console.WriteLine("Введіть максимальну межу:");
                maxRange = int.Parse(Console.ReadLine());

                if (minRange > maxRange)
                    Console.WriteLine("Мінімальна межа не може бути більшою за максимальну. Спробуйте знову.");

            } while (minRange > maxRange);

            array = new int[length];
            Random rndGen = new Random();

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rndGen.Next(minRange, maxRange + 1);
            }

            Console.WriteLine("Ось згенерований масив:");
            PrintArray(array);
        }

        static void InputArrayLineByLine(ref int[] array)
        {
            Console.WriteLine("Введіть кількість елементів:");
            int n = int.Parse(Console.ReadLine());

            array = new int[n];

            Console.WriteLine($"Введіть {n} елементів, кожен в окремому рядку:");
            for (int i = 0; i < n; i++)
            {
                array[i] = int.Parse(Console.ReadLine());
            }
            Console.WriteLine();
        }

        static void InputArrayInSingleLine(ref int[] array)
        {
            Console.WriteLine("Введіть елементи масиву через пробіли або табуляції:");
            string input = Console.ReadLine();

            array = Array.ConvertAll(input.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries), int.Parse);
        }

        static int[]? LimitsForAverage(int[] array) //  повертає null in int[]? в разі помилки
        {
            if (array.Length == 0 || array == null) 
            {
                Console.WriteLine("Масив порожній.");
                return null; 
            }

            int firstPositiveIndex = -1;
            int lastNegativeIndex = -1;

            
            for (int i = 0; i < array.Length; i++)
            {
                if (firstPositiveIndex == -1 && array[i] > 0)
                {
                    firstPositiveIndex = i; // Присвоїмо тільки перше додатнє
                }
                if (array[i] < 0)
                {
                    lastNegativeIndex = i; // Присвоїмо останнє від'ємне
                }
            }

            if (firstPositiveIndex == -1)
            {
                Console.WriteLine("Масив не має додатніх чисел.");
                return null; 
            }
            if (lastNegativeIndex == -1)
            {
                Console.WriteLine("Масив не має від'ємних чисел.");
                return null; 
            }

            // Визначаємо межі для обчислення середнього
            int startForAverage = Math.Min(firstPositiveIndex, lastNegativeIndex);
            int endForAverage = Math.Max(firstPositiveIndex, lastNegativeIndex);

            if (endForAverage - startForAverage <= 1)
            {
                Console.WriteLine("Проміжок між першим додатнім і останнім від'ємним відсутній.");
                return null; 
            }

            return new int[] { startForAverage, endForAverage }; 
        }

        static double? CalculateAverage(int[] array, int start, int end)
        {
           
            int sum = 0;
            int count = 0;

            for (int i = start + 1; i < end; i++) // не включаємо значення меж при прогоні
            {
                sum += array[i];
                count++;
            }

            

            double average = (double)sum / count;
            return average;
        }

        static void Main(string[] args)
        {
            int[] array = null; 

            string choice_1;

            do
            {
                Console.WriteLine($"Виберіть спосіб заповнення масиву випадково чи вручну?");
                Console.WriteLine("1) Випадково");
                Console.WriteLine("2) Вручну");
                Console.WriteLine("0) Закрити програму");
                Console.WriteLine();
                choice_1 = (Console.ReadLine());
                Console.WriteLine();
                switch (choice_1)
                {
                    case "1":
                        InputRandom(ref array);
                        break;

                    case "2":
                        string choice_1_2;
                        do
                        {
                            Console.WriteLine($"Бажаєте вводити кожен елемент у окремому рядку, чи всі разом одним рядком через пробіли та/або табуляції?");
                            Console.WriteLine("1) Кожен елемент окремо");
                            Console.WriteLine("2) Через пробіли та/або табуляції");
                            Console.WriteLine("0) Повернутися в попереднє меню");


                            choice_1_2 = (Console.ReadLine());

                            Console.WriteLine();

                            switch (choice_1_2)
                            {
                                case "1":
                                    Console.WriteLine();
                                    InputArrayLineByLine(ref array);
                                    break;

                                case "2":
                                    Console.WriteLine();
                                    InputArrayInSingleLine(ref array);
                                    break;

                                case "0":
                                    Console.WriteLine("Повертаємось до попереднього меню...");
                                    break;

                                default:
                                    Console.WriteLine("Команда '{0}' не розпiзнана\nЗробiть, будь ласка, вибiр iз 1, 2, 0\n", choice_1_2);
                                    break;
                            }
                            break;

                        } while (choice_1_2 != "0");

                        break;

                    case "0":
                        Console.WriteLine("Зараз завершимо, тiльки натиснiть будь ласка ще раз Enter");
                        Console.ReadLine();
                        break;

                    default:
                        Console.WriteLine("Команда '{0}' не розпiзнана\n" +
                            "Зробiть, будь ласка, вибiр iз 1, 2, 0\n", choice_1);
                        break;
                }

                if (array != null) 
                {
                    int[]? limits = LimitsForAverage(array); 

                    if (limits != null) 
                    {
                        double? average = CalculateAverage(array, limits[0], limits[1]);

                        if (average != null)
                        {
                            Console.WriteLine($"Середнє арифметичне елементів в діапазоні індексів від {limits[0] + 1} до {limits[1] + 1} : {average}");
                        }
                    }
                }

            } while (choice_1 != "0");
        }
    }
}
