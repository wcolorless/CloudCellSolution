using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServiceLibrary
{
    public class TestFunctions
    {
        public int Mul(int num1, int num2)
        {
            return num1 * num2;
        }

        public int Sum(int num1, int num2)
        {
            return num1 + num2;
        }

        public int SymCount(string str)
        {
            return str.Length;
        }

        public int FibNum(int num)
        {
            if (num == 0) return 0;
            if (num == 1) return 0;
            if (num == 2) return 1;
            return FibNum(num - 1) + FibNum(num - 2);
        }
    }
}
