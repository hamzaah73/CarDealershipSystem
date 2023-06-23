using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealershipSystem
{
    public class Validator
    {
        public void validatepassword(string buyerid, string pass, string confirmpass)
        {
            if (pass == confirmpass)
            {
                clsSave save = new clsSave();
                save.savedata(buyerid,pass);
            }
           
        }
    }
}
