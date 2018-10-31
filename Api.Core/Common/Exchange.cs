using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace Api.Core.Common
{
    public class Exchange
    {
        #region Convert date to int
        public int DateToInt()
        {
            try
            {
                string yyyy = DateTime.Now.Year.ToString();
                string mm = DateTime.Now.Month.ToString("00");
                string dd = DateTime.Now.Day.ToString("00");
                return Convert.ToInt32(yyyy + mm + dd);
            }
            catch
            {
                return 0;
            }

        }
        #endregion
        
       
        #region Convert time to int
        public int TimeToInt()
        {
            try
            {
                string hh = DateTime.Now.Hour.ToString("00");
                string mm = DateTime.Now.Minute.ToString("00");
                return Convert.ToInt32(hh + mm);
            }
            catch
            {
                return 0;
            }

        }
        #endregion

        #region Convert int to date
        public string IntToDate(int date)
        {
            try
            {
                string yyyy = date.ToString().Substring(0, 4);
                string mm = date.ToString().Substring(4, 2);
                string dd = date.ToString().Substring(6, 2);
                return dd + "/" + "/" + mm + "/" + yyyy;
            }
            catch
            {
                return date.ToString();
            }

        }
        #endregion  

        #region Convert int to time
        public string IntToTime(int time)
        {
            try
            {
                string hh = time.ToString("0000").Substring(0, 2);
                string mm = time.ToString("0000").Substring(2, 2);
                return hh + ":" + mm;
            }
            catch
            {
                return time.ToString();
            }

        }
        #endregion  

        #region Convert to UTF8
        public string ToUTF8(string original_string)
        {
            try
            {
                byte[] utf8Bytes = Encoding.UTF8.GetBytes(original_string);
                return Encoding.UTF8.GetString(utf8Bytes);
            }
            catch
            {
                return original_string;
            }

        }
        #endregion     
    }
}