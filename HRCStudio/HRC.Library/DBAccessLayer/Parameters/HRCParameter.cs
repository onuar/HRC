using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRC.Library.DBAccessLayer.Parameters
{
    public class HRCParameter
    {
        public string Name { get; set; }

        private object _Value;
        public object Value
        {
            get
            {
                if (_Value == null)
                    _Value = DBNull.Value;
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        public int Size { get; set; }
        public HRCParameterType Type { get; set; }

        public HRCParameterOperator Operator { get; set; }

        public HRCParameter(string name, object value, HRCParameterType type)
        {
            Name = name;
            Value = value;
            Type = type;
            Operator = HRCParameterOperator.Equal;
        }

        public HRCParameter(string name, object value, HRCParameterType type, HRCParameterOperator operatr)
        {
            Name = name;
            Value = value;
            Type = type;
            Operator = operatr;
        }

        public string GetParameterOperator()
        {
            switch (Operator)
            {
                case HRCParameterOperator.Equal:
                    return " = ";
                case HRCParameterOperator.GreaterThenSign:
                    return " > ";
                case HRCParameterOperator.LessThenSign:
                    return " < ";
                case HRCParameterOperator.NotEqual:
                    return " <> ";
                case HRCParameterOperator.GreaterThenSignAndEqual:
                    return " >= ";
                case HRCParameterOperator.LessThenSignAndEqual:
                    return " <= ";
                default:
                    throw new Exception("Undefined parameter operator");
            }
        }
    }
}
