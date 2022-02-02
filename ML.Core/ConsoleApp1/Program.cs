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
        public delegate void ShowValueP2(string p1,string p2);
        public delegate void ShowValueP3(string p1,string p2,string p3);

        public delegate bool WriteMethod();

        static void Main(string[] args)
        {
            //TestStudent();
            //TestDelegate();
            //TestFunc();
            //TestTuple();
            //TestValueTuple();
            //TestSystemInfo();
            TestConsole();
        }

        static void TestConsole()
        {
            //Console.BackgroundColor = ConsoleColor.Green;
            //Console.ForegroundColor = ConsoleColor.Blue;
            Console.Title = "演示";
            Console.CursorLeft = 50;
            Console.CursorTop = 1;
            Console.CursorSize = 90;
            Console.CursorVisible = false;
            Console.WriteLine("Hello World");
            Console.WriteLine(" ______________");
            Console.WriteLine("|");
            Console.WriteLine("||");
            Console.WriteLine("||");
            Console.WriteLine("||");
            Console.WriteLine("||");
            Console.WriteLine("|______________");

            Console.ReadLine();
        }

        static void TestSystemInfo()
        {
            Assembly assembly = typeof(Program).Assembly;
            AssemblyName assemblyName = assembly.GetName();
            Version ver = assemblyName.Version;
            Console.WriteLine("This is version {0} of {1}.", ver, assemblyName.Name);
        }

        static void TestValueTuple()
        {
            var tuple = ValueTuple.Create(1);
            Console.WriteLine(tuple.ToTuple<int>().Item1);

            var tuple1 = ValueTuple.Create(1, 2, 3, 4, 5);
            Console.WriteLine(tuple1);

            var tuple2 = new ValueTuple<int, int, int, int, int, int>();
            tuple2.Item1 = 1;
            tuple2.Item5 = 10;
            Console.WriteLine(tuple2.Item1);
        }

        static void TestTuple()
        {
            var tuple2 = Tuple.Create(10,11,12);
            var tuple3 = new Tuple<Tuple<int,int,int>, int>(tuple2, 10);
            Console.WriteLine($"{tuple3.Item1.Item2}----{tuple3.Item2}");

            var tuple1 = Tuple.Create(12,13);
            Console.WriteLine(tuple1.Item2);     // Displays 12

            // Create a 7-tuple.
            var population1 = Tuple.Create("New York", 7891957, 7781984, 7894862, 7071639, 7322564, 8008278);
            // Display the first and last elements.
            Console.WriteLine("Population of {0} in 2000: {1:N0}",
                              population1.Item1, population1.Item7);
            // The example displays the following output:
            //       Population of New York in 2000: 8,008,278

            // Create a 7-tuple.
            var population = new Tuple<string, int, int, int, int, int, int>(
                                       "New York", 7891957, 7781984,
                                       7894862, 7071639, 7322564, 8008278);
            // Display the first and last elements.
            Console.WriteLine("Population of {0} in 2000: {1:N0}",
                              population.Item1, population.Item7);
            // The example displays the following output:
            //       Population of New York in 2000: 8,008,278

            var primes = Tuple.Create(1, 2, 3, 4, 5, 6, 7, 8);
            Console.WriteLine("Prime numbers less than 20: " +
                  "{0}, {1}, {2}, {3}, {4}, {5}, {6}, and {7}",
                  primes.Item1, primes.Item2, primes.Item3,
                  primes.Item4, primes.Item5, primes.Item6,
                  primes.Item7, primes.Rest.Item1);
        }

        static void TestFunc()
        {
            Student student = new Student("MartyZane");

            //LazyValue<string> lazyThree = new LazyValue<string>(() => student.ExpensiveThree("hahaha"));

            //TODO:Func之间嵌套使用
            student.PrintNesting();
            student.PrintNesting("嵌套使用的函数");
            student.PrintNesting("嵌套使用的函数","p2");
            student.PrintNesting("嵌套使用的函数","p2","p3");
            student.PrintNesting("嵌套使用的函数","p2","p3","p4");
            student.PrintNesting("嵌套使用的函数","p2","p3","p4","p5");

            //TODO:使用 Func<TResult> 委托时，无需显式定义用于封装无参数方法的委托
            //WriteMethod methodCall = student.SendToFile;
            //Func<bool> methodCall = student.SendToFile;
            //Func<bool> methodCall = delegate () { return student.SendToFile(); };
            Func<bool> methodCall = () => student.SendToFile();
            if (methodCall())
                Console.WriteLine("Success!");
            else
                Console.WriteLine("File write operation failed.");

            //TODO:https://docs.microsoft.com/zh-cn/dotnet/api/system.func-1?view=net-6.0
            LazyValue<int> lazyOne = new LazyValue<int>(() => student.ExpensiveOne());
            LazyValue<long> lazyTwo = new LazyValue<long>(() => student.ExpensiveTwo("Hello Func"));

            Console.WriteLine(lazyOne.Value);
            Console.WriteLine(lazyTwo.Value);
        }

        static void TestDelegate()
        {
            Student student = new Student("MartyZane");
            //TODO:委托传递3个参数
            ShowValueP3 showValueP3;
            showValueP3 = student.Print;
            showValueP3($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}]", "ShowValueP3","Hello");

            //TODO:委托传递2个参数
            ShowValueP2 showValueP2;
            showValueP2 = student.Print;
            showValueP2($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}]", "ShowValueP2");

            //TODO:委托传递一个参数
            //ShowValueP1 showValueP1;
            Action<string> showValueP1;
            //showValueP1 = student.Print;
            //showValueP1 = delegate (string value) { student.Print(value); };
            showValueP1 = value => student.Print(value);

            //if (Environment.GetCommandLineArgs().Length > 1)
            //    showValueP1 = student.Print;
            //else
            //    showValueP1 = Console.WriteLine;

            showValueP1($"Hello World {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
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
