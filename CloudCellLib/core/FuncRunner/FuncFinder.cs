using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace CloudCellLib.FuncRunner
{
    public class FuncFinder
    {
        /// <summary>
        /// Load the library and find all the function names in the loaded library
        /// </summary>
        /// <param name="Path">Path to the library file</param>
        /// <returns>List names</returns>
        public static List<string> Get(string Path)
        {
            List<string> FuncNames = new List<string>();
            try
            {
                var asm = Assembly.LoadFile(Path);
                var asmType = asm.GetTypes();
                for (int i = 0; i < asmType.Length; i++)
                {
                    var Methods = asmType[i].GetMethods();
                    for (int j = 0; j < Methods.Length; j++)
                    {
                        if (Methods[j].Name != "ToString" && Methods[j].Name != "GetType" && Methods[j].Name != "GetHashCode" && Methods[j].Name != "Equals")
                        {
                            FuncNames.Add(Methods[j].Name);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                throw new InvalidOperationException("Library error loading", e);
            }
            return FuncNames;
        }

        /// <summary>
        /// Find type of the class that contains the function name
        /// </summary>
        /// <param name="FunctName">Function name</param>
        /// <param name="Path">Path to the library file</param>
        /// <returns></returns>
        public static Type GetParentType(string FunctName, string Path)
        {
            try
            {
                var asm = Assembly.LoadFile(Path);
                var asmType = asm.GetTypes();
                for (int i = 0; i < asmType.Length; i++)
                {
                    var Methods = asmType[i].GetMethods();
                    for (int j = 0; j < Methods.Length; j++)
                    {
                        if(FunctName == Methods[j].Name)
                        {
                            return asmType[i];
                        }
                    }
                }
            }
            catch(Exception e)
            {
                throw new InvalidOperationException("Library error loading", e);
            }
            return null;
        }
    }
}
