using PalindromeDetector.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PalindromeDetector.implementation
{
    class Palindrome : IPalindrome
    {
        public bool IsPalindrome(string input)
        {
            int min = 0;
            int max = input.Length - 1;
            while (true)
            {
                if (min > max)
                {
                    return true;
                }
                char a = input[min];
                char b = input[max];
                if (char.ToLower(a) != char.ToLower(b))
                {
                    return false;
                }
                min++;
                max--;
            }
        }
    }
}
