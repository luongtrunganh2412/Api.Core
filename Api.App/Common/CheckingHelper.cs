using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.App.Common
{
    public class CheckingHelper
    {
        #region Check_EMSCode
        public bool Check_EmsCode(string mae1)
        {
            bool result = true;
            #region Kiểm tra mã E1
            if (mae1.Length != 13)
            {
                result = false;
            }
            else
            {
                string f = mae1.Substring(0, 1);
                string a = mae1.Substring(1, 1);
                string b = mae1.Substring(11, 1);
                string c = mae1.Substring(12, 1);
                if ((IsCharacter(f) == false) || (IsCharacter(a) == false) || (IsCharacter(b) == false) || (IsCharacter(c) == false))
                {
                    result = false;
                }
                else
                {
                    if (f.ToUpper() != "E" && f.ToUpper() != "C" && f.ToUpper() != "V")
                    {
                        result = false;
                    }
                    else
                    {
                        int[] myArray = new int[9];
                        int mySum;
                        int myP;
                        int myPP;
                        for (int i = 2; i < 11; i++)
                        {
                            string d = mae1.Substring(i, 1);
                            if (IsNumeric(d) == false)
                            {
                                result = false;
                            }
                            else
                            {
                                myArray[i - 2] = Int32.Parse(d);
                            }
                        }
                        mySum = (myArray[0] * 8) + (myArray[1] * 6) + (myArray[2] * 4) + (myArray[3] * 2) + (myArray[4] * 3) + (myArray[5] * 5) + (myArray[6] * 9) + (myArray[7] * 7);
                        myP = 11 - (mySum % 11);
                        if (myP == 10)
                            myPP = 0;
                        else
                            if (myP == 11)
                            myPP = 5;
                        else
                            myPP = myP;
                        if (myPP != myArray[8])
                        {
                            result = false;
                        }
                    }
                }
            }
            #endregion
            return result;
        }
        #endregion

        #region Kiểm tra xem đây có phải là ký tự
        private Boolean IsCharacter(string a)
        {
            string alfa = "ABCDEFGHIJKMNLOPQRSTUVXYZW";
            int pos = alfa.IndexOf(a.ToUpper());
            if (pos > -1)
                return true;
            else
                return false;
        }
        #endregion

        #region Kiểm tra xem đây có phải là số không
        private Boolean IsNumeric(string a)
        {
            string alfa = "0123456789";
            int pos = alfa.IndexOf(a.ToUpper());
            if (pos > -1)
                return true;
            else
                return false;
        }
        #endregion
    }
}