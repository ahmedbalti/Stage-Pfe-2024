using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace User.Gestion.Service.Models.Authentication.Login
{
    [DataContract]
    public class LoginModel1
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "userName")]
        public string UserName { get; set; }
    }
}
