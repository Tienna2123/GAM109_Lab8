using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab8
{
    internal class Program
    {
        static Random random = new Random();
        static object lockObj = new object();
        static int randomNumber;
        static bool completed = false;

        

        static void Thread1()
        {
            for (int i = 0; i < 100; i++)
            {
                lock (lockObj)
                {
                    randomNumber = random.Next(1, 11);
                    Console.WriteLine($"Thread1: Sình số ngẫu nhiên: {randomNumber}");
                    Monitor.Pulse(lockObj);
                    Monitor.Wait(lockObj);
                }
                Thread.Sleep(2000);
            }
            completed = true;
        }

        static void Thread2()
        {
            for (int i = 0; i < 100; i++)
            {
                lock (lockObj)
                {
                    Monitor.Wait(lockObj);
                    int squaredNumber = randomNumber * randomNumber;
                    Console.WriteLine($"Thread2: Bình phương của số: {squaredNumber}");
                    Monitor.Pulse(lockObj);
                }
                Thread.Sleep(1000);
            }
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Thread thread1 = new Thread(Thread1);
            Thread thread2 = new Thread(Thread2);
            thread1.Start();
            thread2.Start();
            while (!completed)
            {
                Thread.Sleep(100);
            }

            Console.WriteLine("Kết thúc chương trình ");
        }

    }
}
