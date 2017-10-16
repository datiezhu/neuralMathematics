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

        #region Fields

        /// <summary>
        /// Index name to store value
        /// </summary>
        private static string valueName = "value";

        #endregion

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
        /// Gets the operator ID
        /// </summary>
        public override string Operator
        {
            get
            {
                throw new NotSupportedException();
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
        /// Gets true if it's not an operator
        /// </summary>
        public override bool IsNotOperator
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets the left operand
        /// </summary>
        public override IArithmetic LeftOperand
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Gets the right operand
        /// </summary>
        public override IArithmetic RightOperand
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create a new arithmetic class
        /// </summary>
        protected override IArithmetic Create()
        {
            return new NumericValue();
        }

        #endregion

    }
}