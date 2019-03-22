using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace CloudCellLib
{
    public class GetHttpRequestTest
    {
        [Fact]
        public void TestGetRequest()
        {
            string GetRequestString = "?Param1=1&Param2=2&Param3=3";
            string[] Params = new string[] { "Param1", "Param2", "Param3" };
            string[] Values = new string[] { "1", "2", "3" };
            var result = GetHttpRequest.GetParams(GetRequestString);
            Assert.Equal(Values, result.Values);
            Assert.Equal(Params, result.Keys);

        }
    }
}
