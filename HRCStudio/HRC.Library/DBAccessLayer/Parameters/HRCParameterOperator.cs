using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRC.Library.DBAccessLayer.Parameters
{
    public enum HRCParameterOperator
    {
        Equal = 0,
        GreaterThenSign = 1,
        LessThenSign = 2,
        NotEqual = 3,
        GreaterThenSignAndEqual = 4,
        LessThenSignAndEqual = 5
    }
}