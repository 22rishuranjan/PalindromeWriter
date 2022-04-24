using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PalindromeDetector.interfaces
{
    interface IReadWrite
     {
        public void CreateDirectoryIfNotExist(string path);
        public Task<string> ReadInputFile(string path);

        public void WriteOutput(string path,List<string> outputList);
    }
}
