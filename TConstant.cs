using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BruteEquationGenerator
{
    class TConstant : TVariable
    {
        public TConstant(string symbol, double value):base(symbol,value)
        {
            
        }

        public override double GetValue(int T)
        {
            return base.GetValue(0);
        }


    }
}
