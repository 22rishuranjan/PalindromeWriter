using Microsoft.Extensions.Logging;
using PalindromeDetector.interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace PalindromeDetector.implementation
{
    class Worker : IWorker
    {
        private readonly IReadWrite _readwrite;
        private readonly IPalindrome _palindrome;
        private readonly string _outputPath;
        private readonly string _inputPath;
        

        public Worker(IReadWrite readwrite, IPalindrome palindrome)
        {
            _readwrite = readwrite;
            _palindrome = palindrome;
            _inputPath= ConfigurationManager.AppSettings.Get("InputDirectoryPath");
            _outputPath = ConfigurationManager.AppSettings.Get("OutputDirectoryPath");


        }

        public async void DoWork()
        {

            //*** :if keys are not present in app.config then return!!
            if (CheckPreRequisite())
            {


                //***: If directory does not exist, give warning on console window.
                if (Directory.Exists($@"{_inputPath}"))
                {
                    DirectoryInfo drInfo = new DirectoryInfo($@"{_inputPath}");
                    foreach (var file in drInfo.GetFiles())
                    {
                        if (file != null)
                        {
                            //*** :Read input from the file
                            string readText = await _readwrite.ReadInputFile($@"{_inputPath}\{file.Name}");


                            string[] allTexts = GetInputWords(readText, "\r\n");

                            //***: LinQ to find all palindromes texts. 
                            var palindromeTexts = allTexts.Where(a => _palindrome.IsPalindrome(a) == true && !String.IsNullOrEmpty(a)).ToList();

                            //***: If directory does not exist then create one 
                            _readwrite.CreateDirectoryIfNotExist(_outputPath);

                            //***: write palindrome to the specified file
                            _readwrite.WriteOutput($@"{GetOutputPathToWrite(file.Name, file.Extension)}", palindromeTexts);

                            //*** :write success message on the console.
                            Console.WriteLine($@"Success: all Palindromes successfully written in the output directory - {GetOutputPathToWrite(file.Name, file.Extension)}!!");
                        }
                        else
                        {
                            Console.WriteLine($@"Error: No file exists in the input directory - {_inputPath}!!");
                            return;
                        }
                    }
                }
                else
                {
                    Console.WriteLine($@"Input Directory - {_inputPath} does not exist!!");
                }
            }
        }

        //*** :check input & output directory key
        private bool CheckPreRequisite()
        {
            bool success = true;
            if (_inputPath == null)
            {
                Console.WriteLine("Input directory key or value does not exist!!");
                success = false;
            }

            if (_outputPath == null)
            {
                Console.WriteLine("Output directory key or value does not exist!!");
                success = false;
            }
            return success;
        }

        //*** :to get the output filename. Output file format will be inputfilename+'-output'+extension.
        private string GetOutputFileName(string fileName, string extension)
        {
            var newName = fileName.Split(extension)[0] + "-output" + extension; ;
            return newName;
        }

        //*** :to get the complete output path, with filename.
        private string GetOutputPathToWrite(string fileName, string extension)
        {
            string fullfileName = GetOutputFileName(fileName, extension);
            
            return $@"{_outputPath}\{fullfileName}";
        }

        //*** :generalised function to get input array, 
        private string[] GetInputWords(string input,string splitter)
        {
            string[] allTexts = input.Split(splitter);
            return allTexts;
        }
    }
}
