using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Reflection;

namespace ConsoleApp1
{
    class Program
    {
        private static string accessKeyId = string.Empty;
        private static string accessKeySecret = string.Empty;
        private static string endpoint = string.Empty;
        private static string region = string.Empty;

        public delegate void ShowValue();
        public delegate void ShowValueP1(string value);

        static void Main(string[] args)
        {
            //TestStudent();
            TestDelegate();
        }

        static void TestDelegate()
        {
            Student student = new Student("MartyZane");
            ShowValueP1 showValueP1;
            if (Environment.GetCommandLineArgs().Length > 1)
                showValueP1 = student.Print;
            else
                showValueP1 = Console.WriteLine;

            showValueP1("Hello World");
        }

        static void TestStudent()
        {
            Student student = new Student("MartyZane");

            List<String> names = new List<String>();
            names.Add("Bruce");
            names.Add("Alfred");
            names.Add("Tim");
            names.Add("Richard");

            names.ForEach(student.Print);
            names.ForEach(delegate (string name)
            {
                Console.WriteLine($"name --- {name}");
            });


            //TODO:委托任务
            ShowValue showMethodValue = student.ListenLesson;
            //Action showMethodValue = student.Gender;
            //Action showMethodValue = delegate() { student.ListenLesson(); };
            //Action showMethodValue = () => student.ListenLesson();
            showMethodValue();

            //TODO:获取委托的方法
            var result = showMethodValue.GetMethodInfo();
            Console.WriteLine(result.ToString());
            //TODO:循环调用方法
            for (int i = 0; i < 5; i++)
            {
                showMethodValue.GetMethodInfo().Invoke(student, new object[] { });
            }
        }

        static void TestCalcTime()
        {
            Console.WriteLine($"时间差：{(DateTime.Now - Convert.ToDateTime($"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day} 00:00:00")).TotalSeconds}");
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
