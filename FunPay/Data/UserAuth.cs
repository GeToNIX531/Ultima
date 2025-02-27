using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunPay.Data
{
    public class UserAuth
    {
        public string Login { get; private set; }
        public string Password { get; private set; }

        public UserAuth(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
