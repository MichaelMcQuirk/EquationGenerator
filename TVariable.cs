using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BruteEquationGenerator
{
    class TVariable
    {
        public string Symbol;    //representation
        public List<Double> GivenValues;  //must line up with. If at position 3; T = 12; then for all variables at position 3, their value must correlate to the same T = 12.
    
        public TVariable(string symbol, List<double> values)
        {
            Symbol = symbol;
            GivenValues = values;
        }

        public TVariable(string symbol, double value)
        {
            GivenValues = new List<double>();
            GivenValues.Add(value);
        }

        public virtual double GetValue(int T)
        {
            if (T >= GivenValues.Count) throw new Exception("BOOOOM!!! There arn't even that many elements in the provided input data!!!");
            return GivenValues[T];
        }

        public TVariable Clone()
        {
            TVariable temp = new TVariable();
            temp.Symbol = this.Symbol;
            temp.GivenValues.AddRange(this.GivenValues);//WARNING: Might hand over references and not actual values...
            return temp;
        }


    }
}
