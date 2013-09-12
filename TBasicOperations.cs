using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BruteEquationGenerator
{
    delegate double BasicOperation(double left, double right, out string str, out bool brackets, out bool commutable);

    class TBasicOperations
    {

        public static double Add(double left, double right, out string str, out bool brackets, out bool commutable)
        {
            str = "+";
            brackets = false;
            commutable = true;
            return left + right;
        }

        public static double Subtract(double left, double right, out string str, out bool brackets, out bool commutable)
        {
            str = "-";
            brackets = false;
            commutable = false;
            return left - right;
        }

        public static double Multiply(double left, double right, out string str, out bool brackets, out bool commutable)
        {
            str = "*";
            brackets = false;
            commutable = true;
            return left * right;
        }

        public static double Divide(double left, double right, out string str, out bool brackets, out bool commutable)
        {
            str = "/";
            brackets = false;
            commutable = false;
            return left / right;
        }

        public static double Power(double left, double right, out string str, out bool brackets, out bool commutable)
        {
            str = "^";
            brackets = true;
            commutable = false;
            return Math.Pow(left, right);
        }

        public static double Sin(double left, double right, out string str, out bool brackets, out bool commutable)
        {
            str = "sin";
            brackets = true;
            commutable = false;
            return left * Math.Sin(right);
        }

        public static double Cos(double left, double right, out string str, out bool brackets, out bool commutable)
        {
            str = "cos";
            brackets = true;
            commutable = false;
            return left * Math.Cos(right);
        }

        public static double Tan(double left, double right, out string str, out bool brackets, out bool commutable)
        {
            str = "tan";
            brackets = true;
            commutable = false;
            return left * Math.Tan(right);
        }
    }
}
