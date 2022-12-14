using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;


namespace Laba_1_A
{
    class Program
    {
        static Random rnd = new Random();//иницилизируешь генератор случайных чисел
        static int N;//перменная для рамера массива
        static int[] a = new int[N];//задаешь массив размерность Эн
        static int n;//переменная для кол-во поток
        static int[] returns;//массив для записи результатов потока

        static int ThreadFunc(int param)//функция потока
        {
            int i, nt, beg, h, end;
            int sum = 0;//сумма
            nt = param;//№ потока
            h = N / n;// "шаг", разбиваем размер массива на кол-во потоков
            beg = h * nt; end = beg + h;// сколько чисел в одном потоке
            if (nt == n - 1)
                end = N;//нужна для того, чтобы захватить все элементы в массиве
            for (i = beg; i < end; i++)//с начального №эл в потоке до конечного № эл. в потоке
            {
                if (i % 2 == 0)
                {
                    sum += a[i];//чет. индк +
                }
                else
                {
                    sum -= a[i];// вычитаешь -
                }
            }
            return sum;//возвр. сумму
        }

        static void InputN(out int N)
        {

            while (!int.TryParse(Console.ReadLine(), out N) || (N <= 0))// посмотри, что такое трай парс
            {
                Console.WriteLine("Вы ввели число меньше или равное нулю, или не число. Введите, пожалуйста, заново!");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите кол-во потоков:");//текст сообщение
            InputN(out n);//ввод потоков
            Console.WriteLine("Введите размер массива: ");//текстовое сообщение
            InputN(out N);   //ввод размера

            a = new int[N];//задаешь массив или объявляешь
            int rez = 0;// перменные для результатов
            int i;
            Thread[] thread = new Thread[n]; // массив нитий           

            for (i = 0; i < a.Length; i++)
            {
                a[i] = rnd.Next(100, 100000);// рандомное заполнение массива
            }

            returns = new int[n];//объявлем массив, в котором будут результат выполнения функции в нити, размером кол-во нитей
            for (i = 0; i < n; i++)
            {
                int tmp = i;//
                returns[tmp] = 0;// обнулешь результат
                thread[i] = new Thread(() => { returns[tmp] = ThreadFunc(tmp); });// счтитаешь результат функции  потоке
                thread[i].Start();// запускаешь нить
            }

            for (i = 0; i < n; i++)
            {
                thread[i].Join();// для того, чтоб гарантировать, что результат будет
                rez += returns[i];// складываешь результат выполннения в потоке
            }
            Console.WriteLine(rez);
            Console.ReadLine();// чтоб не закрылась консоль
        }
    }
}
