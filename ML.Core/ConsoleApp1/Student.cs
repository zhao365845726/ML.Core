using System;
using System.Collections.Generic;
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

        public void Print(string s)
        {
            Console.WriteLine(s);
        }

        public void End()
        {
            Console.WriteLine("退出学生模块");
            Console.ReadLine();
        }
    }
}
