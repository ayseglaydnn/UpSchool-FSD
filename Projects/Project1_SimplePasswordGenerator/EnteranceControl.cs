using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PasswordCreator.Console
{
    public class EnteranceControl
    {
        public bool IsDigit(string passwordLength)
        {
            try
            {
                int.Parse(passwordLength);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
