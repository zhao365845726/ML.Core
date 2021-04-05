using System;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string strInfo = string.Empty;
            string[] saDrives = Directory.GetLogicalDrives();
            foreach(string item in saDrives)
            {
                WriteLog(item);
            }
            Console.ReadLine();
        }

        static void WriteLog(string strInfo)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine($"{strInfo}");
            Console.WriteLine($"");
            
        }
    }
}
