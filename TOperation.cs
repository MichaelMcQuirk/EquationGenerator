using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BruteEquationGenerator
{
    public delegate double Operand(double left, double right);



    class TOperation : IComparable
    {
        public TOperation subOpLeft = null;    //if not suOpLeft then try varLeft. If not varLeft then use LeftConst.
        public TOperation subOpRight = null;
        public TVariable varLeft = null;
        public TVariable varRight = null;
        public double LeftConst = 0;
        public double RightConst = 0;

        public BasicOperation Operation;

        private string str = "";
        private bool brackets = false;
        private bool commutable = false;

        public double LastEvaluationValue = 0;
        public double DistanceToGivenData = double.MaxValue;

        public TOperation(BasicOperation operation)
        {
            Operation = operation;// (double left, double right) => { return operation.Invoke(left, right, out str, out brackets); };
        }

        public double Evaluate(int T)
        {
            double left;
            double right;


            if (subOpLeft != null)
                left = subOpLeft.Evaluate(T);
            else
                if (varLeft != null)
                    left = varLeft.GetValue(T);
                else
                    left = LeftConst;


            if (subOpRight != null)
                right = subOpRight.Evaluate(T);
            else
                if (varRight != null)
                    right = varRight.GetValue(T);
                else
                    right = RightConst;


            LastEvaluationValue = Operation.Invoke(left, right, out str, out brackets, out commutable);
            return LastEvaluationValue;
        }

        public List<TOperation> GetListOfAllSubOperations(bool includeParent = true)
        {
            List<TOperation> currentList = new List<TOperation>();
            if (includeParent) currentList.Add(this);
            if (subOpLeft != null) currentList.AddRange(subOpLeft.GetListOfAllSubOperations(true));
            if (subOpRight != null) currentList.AddRange(subOpRight.GetListOfAllSubOperations(true));
            return currentList;
        }

        public string toString()
        {
            //Act(0, 0);
            string result = "";

            if (subOpLeft != null) result += subOpLeft.toString();
            else if (varLeft != null) result += varLeft.Symbol;
            else result += LeftConst.ToString();

            result += str;
            if (brackets) str += "(";

            if (subOpRight != null) result += subOpRight.toString();
            else if (varRight != null) result += varRight.Symbol;
            else result += RightConst.ToString();

            if (brackets) str += ")";
            return result;
        }

        public TOperation Clone()
        {
            TOperation temp = new TOperation(Operation);
            temp.LeftConst = this.LeftConst;
            if (this.varLeft != null) temp.varLeft = this.varLeft.Clone();
            if (this.subOpLeft != null) temp.subOpLeft = this.subOpLeft.Clone();
            temp.RightConst = this.RightConst;
            if (this.varRight != null) temp.varRight = this.varRight.Clone();
            if (this.subOpRight != null) temp.subOpRight = this.subOpRight.Clone();
            return temp;
        }


        public int CompareTo(object obj)
        {
            if (((TOperation)obj).DistanceToGivenData < this.DistanceToGivenData) return -1;
            else if (((TOperation)obj).DistanceToGivenData > this.DistanceToGivenData) return 1;
            else return 0;
        }
    }


    
}
