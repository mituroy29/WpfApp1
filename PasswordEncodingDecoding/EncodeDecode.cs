using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtilities
{
    public class EncodeDecode
    {
        public string EncodePassword(string strPassword)
        {
            string str = string.Empty;
            if(strPassword != null)
            {
                byte[] encd = Encoding.UTF8.GetBytes(strPassword);
                str = Convert.ToBase64String(encd);
            }
            return str;
        }
        public string DecodePassword(string strPassword)
        {
            string str = string.Empty;
            if (strPassword != null)
            {
                byte[] encd = Convert.FromBase64String(strPassword);
                str = Encoding.UTF8.GetString(encd);
            }
            return str;
        }
    }
}
