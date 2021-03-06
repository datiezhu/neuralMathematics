﻿using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistantModel
{
    /// <summary>
    /// Classe stockant une valeur numérique
    /// </summary>
    [Serializable]
    public class NumericValue : Arithmetic
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// Empty data
        /// </summary>
        protected NumericValue()
        {

        }

        /// <summary>
        /// Constructor for a double precision number
        /// </summary>
        /// <param name="d">number</param>
        public NumericValue(double d)
        {
            this[valueName] = d;
            this[isCalculableName] = true;
            this[calculatedValueName] = d;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the numeric value
        /// </summary>
        public double Value
        {
            get
            {
                return this[valueName];
            }
            set
            {
                this[valueName] = value;
            } 
        }

        /// <summary>
        /// Gets binary switch test
        /// </summary>
        public override bool IsBinaryOperator
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets unary switch test
        /// </summary>
        public override bool IsUnaryOperator
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets multiple switch test
        /// </summary>
        public override bool IsMultipleOperator
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets true if it's not an operator
        /// </summary>
        public override bool IsNotOperator
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// This function tries to obtain a numerical value
        /// but if not returns only equations
        /// </summary>
        /// <returns>a numerical value or an equation</returns>
        public override IArithmetic Compute()
        {
            this[isCalculableName] = true;
            this[calculatedValueName] = Value;
            return this;
        }

        /// <summary>
        /// Computes the unique weight
        /// for this object
        /// </summary>
        /// <returns>weight</returns>
        protected override Weight ComputeOwnerWeight()
        {
            return Weight.ComputeWeight(this);
        }

        /// <summary>
        /// Create a new arithmetic class
        /// </summary>
        protected override IArithmetic Create()
        {
            return new NumericValue();
        }

        /// <summary>
        /// Select all terms accordingly with model
        /// </summary>
        /// <param name="model">model to search</param>
        /// <returns>list of elements</returns>
        public override IEnumerable<IArithmetic> Select(IArithmetic model)
        {
            if (this.Value.ToString() == model.ToString())
                yield return model;
            else
                yield break;
        }


        #endregion

    }
}
