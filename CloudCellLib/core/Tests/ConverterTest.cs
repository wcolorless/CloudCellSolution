using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CloudCellLib.FuncRunner;

namespace CloudCellLib
{
    public class ConverterTest
    {
        [Fact]
        public void TestParamConverterString()
        {
            object obj = Int32.MaxValue;
            var result = ParamConverter.ConvertTo(obj, "String");
            Assert.Equal(Int32.MaxValue.ToString(), result);
        }

        [Fact]
        public void TestParamConverterInt32()
        {
            object obj = Int32.MaxValue.ToString();
            var result = ParamConverter.ConvertTo(obj, "Int32");
            Assert.Equal(Int32.MaxValue, result);
        }

        [Fact]
        public void TestParamConverterInt32ToInt32()
        {
            object obj = Int32.MaxValue;
            var result = ParamConverter.ConvertTo(obj, "Int32");
            Assert.Equal(Int32.MaxValue, result);
        }

        [Fact]
        public void TestParamConverterStringToString()
        {
            object obj = "Str";
            var result = ParamConverter.ConvertTo(obj, "String");
            Assert.Equal("Str", result);
        }
    }
}
