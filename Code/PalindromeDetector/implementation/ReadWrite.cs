using Microsoft.Extensions.Logging;
using PalindromeDetector.interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PalindromeDetector.implementation
{
    class ReadWrite : IReadWrite
    {
        
        public async Task<string> ReadInputFile(string path)
        {
            var texts = await File.ReadAllTextAsync($@"{path}");
            //Console.WriteLine($@"read Successful for input file path:{path}");
            return texts;
        }

        public async void WriteOutput(string path,List<string> outputList)
        {
           await File.AppendAllLinesAsync($@"{path}", outputList);
            Console.WriteLine($"Write Successful for input file path:{path}");
        }

        public  void CreateDirectoryIfNotExist(string path)
        {
            if (!Directory.Exists($@"{path}"))
            {
                Directory.CreateDirectory(path);
               Console.WriteLine($"Directory created for path :{path}");
            }
        }



    }
}
