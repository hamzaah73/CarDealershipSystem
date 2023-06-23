using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealershipSystem
{
    public class clsFacade
    {
        public void registeruser(string buyerid, string pass, string passconfirm) //SDA PROJECT FACADE PATTERN
        {
            Validator validate = new Validator();
            validate.validatepassword(buyerid,pass,passconfirm);
        }
    }
}