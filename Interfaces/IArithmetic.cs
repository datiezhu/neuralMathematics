﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IArithmetic : IEquation, ICloneable
    {
        IWeight OwnerWeight { get; }
        dynamic this[string name] { get; set; }
    }
}
