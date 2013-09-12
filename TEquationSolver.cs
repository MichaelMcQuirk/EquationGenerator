using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BruteEquationGenerator
{
    class TEquationSolver
    {
        public List<TOperation> BasicOperations = new List<TOperation>();
        public List<TVariable> IndependantVariables = new List<TVariable>();
        public List<TVariable> DependantVariables = new List<TVariable>();
        public List<TConstant> Constants = new List<TConstant>();

        List<TOperation> Top5Equations = new List<TOperation>();

        Thread ProcessingThread;

        public TEquationSolver()
        {
            BasicOperations.Add(new TOperation((double left, double right, out string str, out bool brackets) => TBasicOperations.Add(left, right, out str, out brackets)));
            BasicOperations.Add(new TOperation((double left, double right, out string str, out bool brackets) => TBasicOperations.Subtract(left, right, out str, out brackets)));
            BasicOperations.Add(new TOperation((double left, double right, out string str, out bool brackets) => TBasicOperations.Multiply(left, right, out str, out brackets)));
            BasicOperations.Add(new TOperation((double left, double right, out string str, out bool brackets) => TBasicOperations.Divide(left, right, out str, out brackets)));
            BasicOperations.Add(new TOperation((double left, double right, out string str, out bool brackets) => TBasicOperations.Sin(left, right, out str, out brackets)));
            BasicOperations.Add(new TOperation((double left, double right, out string str, out bool brackets) => TBasicOperations.Cos(left, right, out str, out brackets)));
            BasicOperations.Add(new TOperation((double left, double right, out string str, out bool brackets) => TBasicOperations.Tan(left, right, out str, out brackets)));
            BasicOperations.Add(new TOperation((double left, double right, out string str, out bool brackets) => TBasicOperations.Power(left, right, out str, out brackets)));
        
            Constants.AddRange(new TConstant[] {new TConstant("pi",Math.PI), new TConstant("e",Math.E), new TConstant("1",1)});

            LoadInputDataFromUser();

        }

        private void LoadInputDataFromUser()
        {
        
        }

        private void BeginFormingEquations()
        {
            ProcessingThread = new Thread(() => Solve());
            ProcessingThread.Start();
        }

        private void Solve()
        {
            //b o d m a s

            //do everything in anouther loop that calculates a new operation for each dependant variable (eg Y and Z)

            //Loop through Top Equations:
            int iDependantVariableID = 0;

            for (int iTopOp = 0; iTopOp < Top5Equations.Count; iTopOp++)
            {
                TOperation TopOp = Top5Equations[iTopOp];

                //Loop through Basic Operators:
                for (int iOp = 0; iOp < BasicOperations.Count; iOp++)
                {
                    TOperation Op = BasicOperations[iOp];
                    List<TOperation> Candidates = new List<TOperation>();//all possible candidates for next top set of equations. Is later sorted on order of distance.

                    TOperation temp = TopOp.Clone();
                    List<TOperation> allSubOperations = temp.GetListOfAllSubOperations();

                    //Loop through possible locations:
                    for (int i = 0; i < allSubOperations.Count; i++)
                    {
                        temp = TopOp.Clone();
                        allSubOperations = temp.GetListOfAllSubOperations();
                        //attempt for adding to left op, attempt for adding to right op

                        //right - make prev operations "left" new operation's left. create some new function for right.
                        TOperation cur = allSubOperations[i];
                        TOperation newOp = Op.Clone();

                        newOp.LeftConst = cur.LeftConst;
                        newOp.subOpLeft = cur.subOpLeft;
                        newOp.varLeft = cur.varLeft;

                        cur.subOpLeft = newOp;
                        
                        newOp.subOpRight = null;
                        newOp.varRight = null;
                        newOp.RightConst = 0;//remember, it checks in order of subOp -> var -> const

                        /////////////////   USER DEFINED CONSTANTS   /////////////////////
                        int iMinConst = -1;
                        double minConstDist = double.MaxValue;
                        for (int iConst = 0; iConst < Constants.Count; i++)
                        {
                            newOp.varRight = Constants[iConst];  //remember, user defined constants are a subset of variables
                            double dist = DistanceToGivenData(temp, iDependantVariableID);
                            if (dist < minConstDist)
                            {
                                iMinConst = iConst;
                                minConstDist = dist;
                            }
                        }
                        newOp.varRight = Constants[iMinConst];
                        Candidates.Add(temp);

                        /////////////////   USER DEFINED VARIABLES   /////////////////////
                        temp = TopOp.Clone();
                        allSubOperations = temp.GetListOfAllSubOperations();
                        
                        cur = allSubOperations[i];
                        newOp = Op.Clone();

                        newOp.LeftConst = cur.LeftConst;
                        newOp.subOpLeft = cur.subOpLeft;
                        newOp.varLeft = cur.varLeft;

                        cur.subOpLeft = newOp;

                        int iMinVar = -1;
                        double minVarDist = double.MaxValue;
                        for (int iVar = 0; iVar < IndependantVariables.Count; i++)
                        {
                            newOp.varRight = IndependantVariables[iVar];
                            double dist = DistanceToGivenData(temp, iDependantVariableID);
                            if (dist < minVarDist)
                            {
                                iMinVar = iVar;
                                minVarDist = dist;
                            }
                        }
                        newOp.varRight = IndependantVariables[iMinVar];
                        Candidates.Add(temp);


                        /////////////////   COMPUTER GENERATED NUMBERS   /////////////////////

                        temp = TopOp.Clone();
                        allSubOperations = temp.GetListOfAllSubOperations();

                        cur = allSubOperations[i];
                        newOp = Op.Clone();

                        newOp.LeftConst = cur.LeftConst;
                        newOp.subOpLeft = cur.subOpLeft;
                        newOp.varLeft = cur.varLeft;

                        cur.subOpLeft = newOp;

                        //Select three numbers (1,2,3), calculate each's distance, calculate the rate of change (first derivative) in distance,
                        //   predict which number should yield a distance of 0, using the fact your X rate of change is 1.
                        //   select the middle number to be that position, and the left and right to be 1 unit apart (respectively).
                        //   keep track of each iteration. After 3 iterations, stop iterating the moment the current iteration yields a difference higher than the prior.
                        //   once 6 iterations as been reached or the iterations have stopped prematurely: select the value which yielded the lowest distance.
                        




                        //if not commutable, try again on right

                        


                        //loop through variables, constants and basic functions (one more time) and find which one gives the best match.
                        //variables:
                        //constants:
                        //basic functions, again:
                    }
                }
            }
        }

        public double DistanceToGivenData(TOperation op, int iDependantVariableID)
        {
            if (IndependantVariables.Count == 0) throw new Exception("No independent variables where specified...");
            double distance = 0;
            for (int T = 0; T < IndependantVariables[0].GivenValues.Count(); T++)
            {
                distance += Math.Abs(op.Evaluate(T) - DependantVariables[iDependantVariableID].GivenValues[T]);
            }
            return distance;
        }




    }
}
