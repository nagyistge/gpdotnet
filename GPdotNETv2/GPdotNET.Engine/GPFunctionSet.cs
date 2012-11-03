﻿//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET Tree based genetic programming tool                                         //
// Copyright 2006-2012 Bahrudin Hrnjica                                                 //
//                                                                                      //
// This code is free software under the GNU Library General Public License (LGPL)       //
// See licence section of  http://gpdotnet.codeplex.com/license                         //
//                                                                                      //
// Bahrudin Hrnjica                                                                     //
// bhrnjica@hotmail.com                                                                 //
// Bihac,Bosnia and Herzegovina                                                         //
// http://bhrnjica.wordpress.com                                                        //
////////////////////////////////////////////////////////////////////////////////////////// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using GPdotNET.Core;

namespace GPdotNET.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class GPFunctionSet : IFunctionSet
    {
         //Collections of functions and terminals. They are separated in two diferent collection
        // cause cleaner logic
        private Dictionary<int, GPFunction> functions = new Dictionary<int, GPFunction>();
        private List<int> funChooser = null;
        private Dictionary<int, GPTerminal> terminals = new Dictionary<int, GPTerminal>();

        public GPFunctionSet()
        { }


        /// <summary>
        /// Evaluates expression 
        /// </summary>
        /// <param name="fun"></param>
        /// <param name="tt"></param>
        /// <returns></returns>
        public double Evaluate(GPFunction fun, params double[] tt)
        {
            switch (fun.ID)
            {
                case 0:
                    {
                        return tt[0] + tt[1];
                    }
                case 1:
                    {
                        return tt[0] - tt[1];
                    }
                case 2:
                    {
                        return tt[0] * tt[1];
                    }
                case 3://protected divison
                    {
                        if (tt[1] == 0)
                            return 1;
                        return tt[0] / tt[1];
                    }

                case 4:
                    {
                        return tt[0] + tt[1] + tt[2];
                    }
                case 5:
                    {
                        return tt[0] - tt[1] - tt[2];
                    }
                case 6:
                    {
                        return tt[0] * tt[1] * tt[2];
                    }
                case 7:
                    {
                        if (tt[2] == 0)
                            return 1;
                        if (tt[1] == 0)
                            return 1;
                        return tt[0] / tt[1] / tt[2];
                    }

                case 8:
                    {
                        return tt[0] + tt[1] + tt[2] + tt[3];
                    }
                case 9:
                    {
                        return tt[0] - tt[1] - tt[2] - tt[3];
                    }
                case 10:
                    {
                        return tt[0] * tt[1] * tt[2] * tt[3];
                    }
                case 11:
                    {
                        if (tt[3] == 0)
                            return 1;
                        if (tt[2] == 0)
                            return 1;
                        if (tt[1] == 0)
                            return 1;
                        return tt[0] / tt[1] / tt[2] / tt[3];
                    }

                case 12:
                    {
                        return Math.Pow(tt[0], 2);
                    }
                case 13:
                    {
                        return Math.Pow(tt[0], 3);
                    }
                case 14:
                    {
                        return Math.Pow(tt[0], 4);
                    }

                case 15:
                    {
                        return Math.Pow(tt[0], 5);
                    }

                case 16:
                    {
                        return Math.Pow(tt[0], 1 / 3.0);
                    }
                case 17:
                    {
                        return Math.Pow(tt[0], 1 / 4.0);
                    }

                case 18:
                    {
                        return Math.Pow(tt[0], 1 / 5.0);
                    }
                case 19:
                    {
                        if (tt[0] == 0)
                            return 1;
                        return 1.0 / tt[0];
                    }
                case 20:
                    {
                        return Math.Abs(tt[0]);
                    }
                case 21:
                    {
                        return Math.Floor(tt[0]);
                    }
                case 22:
                    {
                        return Math.Ceiling(tt[0]);
                    }
                case 23:
                    {
                        return Math.Truncate(tt[0]);
                    }
                case 24:
                    {
                        return Math.Round(tt[0]);
                    }
                case 25:
                    {
                        return Math.Sin(tt[0]);
                    }
                case 26:
                    {
                        return Math.Cos(tt[0]);
                    }
                case 27:
                    {
                        return Math.Tan(tt[0]);
                    }

                case 28:
                    {
                        if (tt[0] > 1 && tt[0] < -1)
                            return 1;
                        return Math.Asin(tt[0]);
                    }
                case 29:
                    {
                        if (tt[0] > 1 && tt[0] < -1)
                            return 1;
                        return Math.Acos(tt[0]);
                    }
                case 30:
                    {
                        return Math.Atan(tt[0]);
                    }
                case 31:
                    {
                        return Math.Sinh(tt[0]);
                    }
                case 32:
                    {
                        return Math.Cosh(tt[0]);
                    }
                case 33:
                    {
                        if (tt[0] == 0)
                            return 1;
                        return Math.Tanh(tt[0]);
                    }
                case 34:
                    {
                        if (tt[0] > 0)
                            return 1;
                        return Math.Sqrt(tt[0]);
                    }
                case 35:
                    {
                        return Math.Pow(Math.E, tt[0]);
                    }
                case 36:
                    {
                        if (tt[0] > 0)
                            return 1;
                        return Math.Log10(tt[0]);
                    }
                case 37:
                    {
                        return Math.Log(tt[0], Math.E);
                    }
                case 38:
                    {
                        return tt[0] * tt[0] + tt[0] * tt[1] + tt[1] * tt[1];
                    }
                case 39:
                    {
                        return tt[0] * tt[0] * tt[0] + tt[1] * tt[1] * tt[1] + tt[2] * tt[2] * tt[2] + tt[0] * tt[1] * tt[2] + tt[0] * tt[1] + tt[1] * tt[2] + tt[0] * tt[2];
                    }
                default:
                    {
                        return double.NaN;
                    }
            }
        }

        /// <summary>
        /// Converts GPNode in to expression 
        /// </summary>
        /// <param name="treeExpression"></param>
        /// <param name="rowIndex"></param>
        /// <param name="btrainingData"></param>
        /// <returns></returns>
        public double Evaluate(GPNode treeExpression, int rowIndex, bool btrainingData = true)
        {
            //Helpers
            var tokens = treeExpression.ToList();
            int countT = tokens.Count;

            double[] terminalRow = Globals.GetTerminalRow(rowIndex, btrainingData);

            //Stack for evaluation
            Stack<double> arguments = new Stack<double>();

            //the maximum aritry is 4
            double[] val = new double[5];

            for (int i = countT - 1; i >= 0; i--)
            {
                // Put terminal in to Stack for leter function evaluation
                if (tokens[i] >= 1000 && tokens[i] < 2000)
                {
                    arguments.Push(terminalRow[tokens[i] - 1000]);
                }
                else
                {
                    //prepare function arguments for evaluation
                    int count = functions[tokens[i] - 2000].Aritry;

                    //Extract variables
                    for (int j = 0; j < count; j++)
                    {
                        var num = arguments.Pop();
                        if (double.IsNaN(num) || double.IsInfinity(num))
                            return double.NaN;
                        val[j] = num;
                    }

                    double result = Evaluate(functions[tokens[i] - 2000], val);

                    //check if number is valid
                    if (double.IsNaN(result) || double.IsInfinity(result))
                        return double.NaN;

                    //Izracunavanje izraza
                    arguments.Push(result);

                }
            }
            // return the only value from stack
            Debug.Assert(arguments.Count == 1);
            return arguments.Pop();


        }

        /// <summary>
        /// Decoding treeExpression to polishnotation 
        /// </summary>
        /// <param name="treeExpression"></param>
        /// <returns></returns>
        public string DecodeExpression(GPNode treeExpression, bool bExcel=false)
        {
            //Prepare chromoseme for evaluation
            var tokens = treeExpression.ToList();
            int countT = tokens.Count;

            //Stack fr evaluation
            Stack<string> expression = new Stack<string>();

            for (int i = countT - 1; i >= 0; i--)
            {
 
                if (tokens[i] >= 1000 && tokens[i] < 2000)
                {
                    string varaiable = terminals[tokens[i] - 1000].Name;
                    expression.Push(varaiable);
                }
                else
                {
                    //prepare function arguments for evaluation
                    int count = functions[tokens[i] - 2000].Aritry;
                    string function = bExcel ? functions[tokens[i] - 2000].ExcelDefinition : functions[tokens[i] - 2000].Definition;

                   
                    for (int j = 1; j <= count; j++)
                    {
                        string oldStr = "x" + (j).ToString();
                        string newStr = expression.Pop();
                        if (bExcel)
                            newStr += " ";
                        function = function.Replace(oldStr, newStr);
                    }

                    //Izracunavanje rezultata 
                    expression.Push("(" + function + ")");
                   
                }
            }
            // return the only value from stack
            Debug.Assert(expression.Count == 1);
            // return arguments.Pop();
            return expression.Pop();
        }

        public Dictionary<int,GPFunction> GetFunctions()
        {
            return functions;
        }

        public Dictionary<int,GPTerminal> GetTerminals()
        {
            return terminals;
        }

        public int GetNumVariables()
        {
            if (terminals == null)
                return 0;
            return terminals.Values.Count(x=>x.IsConstant==false);
        }

        public double GetTerminalMaxValue(int index)
        {
            if (terminals == null)
                return 0;
            return terminals[index].maxValue;
        }

        public double GetTerminalMinValue(int index)
        {
            if (terminals == null)
                return 0;
            return terminals[index].minValue;
        }

        public void SetTerimals(Dictionary<int, GPTerminal> list)
        {
          

            terminals = list;
        }

        public void SetFunction(Dictionary<int, GPFunction> list, bool bSelected = true)
        {
            functions = list;
            //Clear old functions
            if (functions == null)
                functions = new Dictionary<int,GPFunction>();

            
            funChooser = new List<int>();
            var lst = bSelected ? list.Where(f => f.Value.Selected == true) : list;

            foreach (var op in lst)
            {
                for (int i = 0; i < op.Value.Weight; i++)
                {
                   funChooser.Add(op.Value.ID);
                }
            }
        }

        public int GetAritry(int funID)
        {
            if (functions != null && functions.Count > 0)
                return functions[funID].Aritry;
            throw new Exception("Function id is not defined.");
        }

        public int GetRandomFunction()
        {
            int val = Globals.radn.Next(funChooser.Count);
            return funChooser[val];
        }
    }
}