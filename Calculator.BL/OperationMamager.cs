using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.BL
{
    public interface IOperationManager
    {
        double Pluse(double firstNumber, double secondNumber);
        double Minus(double firstNumber, double secondNumber);
        double Multiply(double firstNumber, double secondNumber);
        double Divide(double firstNumber, double secondNumber);
        double RemainderOfTheDivision(double firstNumber, double secondNumber);
    }

    public class OperationManager : IOperationManager
    {
        public double RemainderOfTheDivision(double firstNumber, double secondNumber)
        {
            return firstNumber % secondNumber;
        }
        public double Divide(double firstNumber, double secondNumber)
        {
            return firstNumber / secondNumber;
        }

        public double Minus(double firstNumber, double secondNumber)
        {
            return firstNumber - secondNumber;
        }

        public double Multiply(double firstNumber, double secondNumber)
        {
            return firstNumber * secondNumber;
        }

        public double Pluse(double firstNumber, double secondNumber)
        {
            return firstNumber + secondNumber;
        }
    }
}
