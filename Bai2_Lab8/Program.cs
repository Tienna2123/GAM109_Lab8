using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bai2_Lab8
{
    internal class Program
    {
        static object lock1 = new object();
        static object lock2 = new object();

        public static void Thread1FunctionLab8()
        {
            if (Monitor.TryEnter(lock1, TimeSpan.FromMilliseconds(1000)))
            {
                try
                {
                    Console.WriteLine("Thread 1 da khoa lock1");
                    Thread.Sleep(100);

                    if (Monitor.TryEnter(lock2, TimeSpan.FromMilliseconds(1000)))
                    {
                        try
                        {
                            Console.WriteLine("Thread 1 da khoa lock2");
                        }
                        finally
                        {
                            Monitor.Exit(lock2);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Thread 1 khong the khoa lock2");
                    }
                }
                finally
                {
                    Monitor.Exit(lock1);
                }
            }
            else
            {
                Console.WriteLine("Thread 1 khong the khoa lock1");
            }
        }

        public static void Thread2FunctionLab8()
        {
            if (Monitor.TryEnter(lock2, TimeSpan.FromMilliseconds(1000)))
            {
                try
                {
                    Console.WriteLine("Thread 2 da khoa lock2");
                    Thread.Sleep(100);

                    if (Monitor.TryEnter(lock1, TimeSpan.FromMilliseconds(1000)))
                    {
                        try
                        {
                            Console.WriteLine("Thread 2 da khoa lock1");
                        }
                        finally
                        {
                            Monitor.Exit(lock1);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Thread 2 khong the khoa lock1");
                    }
                }
                finally
                {
                    Monitor.Exit(lock2);
                }
            }
            else
            {
                Console.WriteLine("Thread 2 khong the khoa lock2");
            }
        }

        public static void Main()
        {
            Thread thread1 = new Thread(Thread1FunctionLab8);
            Thread thread2 = new Thread(Thread2FunctionLab8);

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            Console.WriteLine("Ket thuc threads");
        }
    }
}
