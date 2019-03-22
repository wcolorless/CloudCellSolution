using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCellLib
{
    public class GetHttpRequest
    {
        /// <summary>
        /// Extracts parameters and values from get request string
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetParams(string Request)
        {
            Dictionary<string, object> Params = new Dictionary<string, object>();
            var step1 = Request.ToArray();
            int index = 0;
            for(int i = 0; i < step1.Length; i++)
            {
                if(step1[i] == '?')
                {
                    index = i + 1;
                }
            }
            if(index > 0)
            {
                var step2 = Request.Substring(index);
                var step3 = step2.Split(new char[] { '&' });
                for(int i = 0; i < step3.Length; i++)
                {
                    var step4 = step3[i].Split(new char[] { '=' });
                    Params.Add(step4[0], step4[1]);
                }
            }
            return Params;
        }
    }
}
