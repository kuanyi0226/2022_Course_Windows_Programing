using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c34104032_practice_3_1
{
    class Account
    {
        public string link, user, password;


        public Account(string _link, string _user, string _password)
        {
            link = _link;
            user = _user;
            password = _password;
        }
        public string returnLink()
        {
            return link;
        }
        public string returnUser()
        {
            return user;
        }
        public string returnPassword()
        {
            return password;
        }
    }
}
