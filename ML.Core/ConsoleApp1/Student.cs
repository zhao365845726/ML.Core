using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp1
{
    public class Student
    {
        private string instanceName;

        public Student(string name)
        {
            instanceName = name;
            Console.WriteLine("进入学生模块");
        }

        public void ListenLesson()
        {
            Console.WriteLine($"{instanceName} 正在听课");
        }

        public void Gender()
        {
            Console.WriteLine($"{instanceName} 是个男孩");
        }

        public void Print(string value)
        {
            Console.WriteLine($"name---{instanceName},value---{value}");
        }

        public void Print(string p1,string p2)
        {
            Console.WriteLine($"name---{instanceName},p1---{p1},p2---{p2}");
        }

        public void Print(string p1, string p2,string p3)
        {
            Console.WriteLine($"name---{instanceName},p1---{p1},p2---{p2},p3---{p3}");
        }

        public void Print(string p1, string p2, string p3,string p4)
        {
            Console.WriteLine($"name---{instanceName},p1---{p1},p2---{p2},p3---{p3},p4---{p4}");
        }

        public void Print(string p1, string p2, string p3, string p4,string p5)
        {
            Console.WriteLine($"name---{instanceName},p1---{p1},p2---{p2},p3---{p3},p4---{p4},p5---{p5}");
        }

        public void Print(string p1, string p2, string p3, string p4, string p5,string p6)
        {
            Console.WriteLine($"name---{instanceName},p1---{p1},p2---{p2},p3---{p3},p4---{p4},p5---{p5},p6---{p6}");
        }

        public void Print(string p1, string p2, string p3, string p4, string p5, string p6,string p7)
        {
            Console.WriteLine($"name---{instanceName},p1---{p1},p2---{p2},p3---{p3},p4---{p4},p5---{p5},p6---{p6},p7---{p7}");
        }


        public int ExpensiveOne()
        {
            Console.WriteLine("\nExpensiveOne() is executing.");
            return 1;
        }

        public long ExpensiveTwo(string input)
        {
            Console.WriteLine("\nExpensiveTwo() is executing.");
            return (long)input.Length;
        }

        public bool SendToFile()
        {
            try
            {
                string fn = Path.GetTempFileName();
                StreamWriter sw = new StreamWriter(fn);
                sw.WriteLine("Hello, World!");
                sw.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void End()
        {
            Console.WriteLine("退出学生模块");
            Console.ReadLine();
        }
    }
}
