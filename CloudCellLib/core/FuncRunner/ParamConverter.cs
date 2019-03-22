using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCellLib.FuncRunner
{
    public class ParamConverter
    {
        /// <summary>
        /// Convert the parameter value from the Get Request to another type
        /// </summary>
        /// <param name="param">Object from Get Request string</param>
        /// <param name="TargetType">Target type</param>
        /// <returns></returns>
        public static object ConvertTo(object param, string TargetType)
        {
            if(TargetType == "Int32")
            {
                var typeName =  param.GetType();
                if (typeName.Name == "String")
                {
                    return Convert.ToInt32((string)param);
                }
                else if (typeName.Name == "Int32")
                {
                    return param;
                }
                else return null; 
            }
            else if(TargetType == "String")
            {
                var typeName = param.GetType();
                if (typeName.Name == "String")
                {
                    return param;
                }
                else if (typeName.Name == "Int32")
                {
                    return param.ToString();
                }
                else return null;
            }
            return null;
        }
    }
}
