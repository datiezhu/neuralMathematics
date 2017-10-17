﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronalMathematics
{
    class Program
    {
        static void Main(string[] args)
        {
            PersistantModel.Term t = new PersistantModel.Term(1.0, "a", "x");
            PersistantModel.Term t2 = new PersistantModel.Term(2.0, "b", "y");
            PersistantModel.Addition a = new PersistantModel.Addition(t, t2);
            FileInfo fi = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.bin"));
            PersistantModel.PersistentDataObject.Save(fi, a);

            PersistantModel.PersistentDataObject o = a as PersistantModel.PersistentDataObject;
            PersistantModel.PersistentDataObject.Load(fi, out o);

            a = o as PersistantModel.Addition;

            foreach(Interfaces.IArithmetic i in a.Constants)
            {
                Console.WriteLine((i as PersistantModel.NumericValue).Value);
            }

            foreach (Interfaces.IArithmetic i in a.Coefficients)
            {
                Console.WriteLine((i as PersistantModel.Coefficient).Value);
            }

            foreach (Interfaces.IArithmetic i in a.UnknownTerms)
            {
                Console.WriteLine((i as PersistantModel.UnknownTerm).Name);
            }

            Console.WriteLine(a.ToString());

            Console.ReadKey();
        }
    }
}
