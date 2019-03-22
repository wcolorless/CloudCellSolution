using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCellLib.FuncRunner
{

    public interface IFuncRunner
    {
        object Run(string functionName, object[] paramsList);
    }

    [Serializable]
    public class SimpleFuncRunner : IFuncRunner
    {
        private Type ServiceType;

        private SimpleFuncRunner(Type ServiceType)
        {
            this.ServiceType = ServiceType;
        }

        public static IFuncRunner Create(Type ServiceType)
        {
            return new SimpleFuncRunner(ServiceType);
        }

        /// <summary>
        /// Execute selected the function and return result.
        /// </summary>
        /// <param name="functionName">Function name for execute</param>
        /// <param name="paramsList">Array of parameters for the executing function</param>
        /// <returns></returns>
        public object Run(string functionName, object[] paramsList)
        {
            try
            {
                var obj = Activator.CreateInstance(ServiceType, new object[] { });
                var methods = ServiceType.GetMethods().ToList();
                var method = methods.Find(x => x.Name == functionName);
                if (method != null)
                {

                    var paramsMethod = method.GetParameters();
                    for (int i = 0; i < paramsList.Length; i++)
                    {
                        paramsList[i] = ParamConverter.ConvertTo(paramsList[i], paramsMethod[i].ParameterType.Name);
                    }
                    var result = method.Invoke(obj, paramsList);
                    return result;
                }
            }
            catch(Exception e)
            {
                throw new InvalidOperationException("Function execution error", e);
            }
            return null;
        }
    }
}
