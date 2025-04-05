using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.UnitTests
{
    public class Calculator
    {
        public int Sum3Numbers(int n1,int n2,int n3)
        {
            return n1 + n2 + n3;
        }

        public bool IsEven(int n)
        {
            return n % 2 == 0;
        }
    }
}
