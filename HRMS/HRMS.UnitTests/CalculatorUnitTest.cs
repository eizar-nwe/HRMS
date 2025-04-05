using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.UnitTests
{
    public class CalculatorUnitTest
    {
        [Fact]
        public void ShouldReturnTrueSum3Numbers()
        {
            //1) Arrange
            Calculator calculator = new Calculator();
            int n1 = 1;
            int n2 = 2;
            int n3 = 3;
            int expectedResult = 6;

            //2) Act
            int ActualResult = calculator.Sum3Numbers(n1, n2, n3);

            //3) Assert
            Assert.Equal(expectedResult, ActualResult);
        }
        [Fact]
        public void ShouldReturnFalseSum3Numbers()
        {
            //1) Arrange
            Calculator calculator = new Calculator();
            int n1 = 1;
            int n2 = 2;
            int n3 = 3;
            int expectedResult = 10;

            //2) Act
            int ActualResult = calculator.Sum3Numbers(n1, n2, n3);

            //3) Assert
            Assert.NotEqual(expectedResult, ActualResult);
        }
        [Fact]
        public void ShouldReturnTrueIsEven()
        {
            //1) Arrange
            Calculator calculator = new Calculator();
            int n = 2;
            bool expectedResult = true;

            //2) Act
            bool ActualResult = calculator.IsEven(n);

            //3) Assert
            Assert.Equal(expectedResult, ActualResult);
        }
    }
}
