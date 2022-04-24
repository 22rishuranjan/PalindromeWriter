using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PalindromeDetector.interfaces;
using PalindromeDetector.implementation;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PalindromeDetector
{
    class Program
    {

        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            try
            {
                //*** :register dependencies
                RegisterServices();

                //*** :to get the instance of workerclass.
                var _worker = _serviceProvider.GetRequiredService<IWorker>();
                _worker.DoWork();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while finding palindrome:{ex.Message}");
            }
            Console.WriteLine("Press any Key to exit.");
            Console.ReadKey();
          
        }


        //*** dependency injection IOC
        private static void RegisterServices()
        {
            //*** :registering all services using singleton lifetime.
            var collection = new ServiceCollection();
            collection.AddSingleton<IReadWrite, ReadWrite>();
            collection.AddSingleton<IWorker, Worker>();
            
            collection.AddSingleton<IPalindrome, Palindrome>();
           
            var builder = new ContainerBuilder();
            builder.Populate(collection);
           

            var appContainer = builder.Build();
            _serviceProvider = new AutofacServiceProvider(appContainer);
        }



        //static void Main(string[] args)
        //{

        //    if (Directory.Exists(inputFilePath))
        //    {
        //        DirectoryInfo drInfo = new DirectoryInfo(inputFilePath);
        //        foreach (var file in drInfo.GetFiles())
        //        {

        //            string readText = File.ReadAllText($@"{ inputFilePath}\{ file.Name}");

        //            string[] allTexts = readText.Split("\r\n");

        //            //File.Create($@"{outputFilePath}\output.txt");

        //            List<string> palindromeList = new List<string>();
        //            foreach(string input in allTexts)
        //            {
        //                if (IsPalindrome(input))
        //                {
        //                    palindromeList.Add(input);

        //                }

        //            }
        //            WriteOutput(palindromeList);
        //            //string PalindromeAll = string.Join("\r\n", palindromeList.ToArray());
        //            //if (File.Exists($@"{outputFilePath}\output.txt"))
        //            //{
        //            //    File.AppendAllLinesAsync($@"{outputFilePath}\output.txt", palindromeList);
        //            //}

        //        }
        //    }

        //    Console.WriteLine("Hello World!");
        //    Console.ReadKey();
        //}


        //public static void WriteOutput(List<string> outputList)
        //{
        //    File.AppendAllLinesAsync($@"{outputFilePath}\output.txt", outputList);
        //}
        //public static bool IsPalindrome(string value)
        //{
        //    int min = 0;
        //    int max = value.Length - 1;
        //    while (true)
        //    {
        //        if (min > max)
        //        {
        //            return true;
        //        }
        //        char a = value[min];
        //        char b = value[max];
        //        if (char.ToLower(a) != char.ToLower(b))
        //        {
        //            return false;
        //        }
        //        min++;
        //        max--;
        //    }
        //}
    }
}

